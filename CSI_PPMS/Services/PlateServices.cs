using CSI_PPMS.ColdLevellerDb;
using CSI_PPMS.DownCoilerDb;
using CSI_PPMS.DowncoilerModels;
using CSI_PPMS.Entity;
using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSI_PPMS.Services
{
    public class PlateServices : IPlateServices
    {
        private readonly ITCPServices _tCPServices;
        private readonly PPMSContext _context;
        private readonly ColdLevellerContext _coldLevellerContext;
        private readonly DownCoilerContext _downCoilerContext;
        private readonly ISAPServices _services;

        public PlateServices(ITCPServices tCPServices,
                             PPMSContext context,
                             ColdLevellerContext coldLevellerContext,
                             DownCoilerContext downCoilerContext,
                             ISAPServices services)
        {
            _tCPServices = tCPServices;
            _context = context;
            _coldLevellerContext = coldLevellerContext;
            _downCoilerContext = downCoilerContext;
            _services = services;
        }


        public async Task<APIResponse> SendToPunching(FL1PunchingModel model)
        {
            var plateId = Convert.ToInt32(model.plateNo);
            var PlateDetails = await _context.PlateInfoFromSAP.Where(x => x.PlateId == plateId).FirstOrDefaultAsync();
            PlateDetails.SentToPunchingTime = DateTime.Now;
            _context.Update(PlateDetails);
            var tcpconf = await _context.TCPConfig.Where(x => x.ModuleId == PlateDetails.ModuleId).FirstOrDefaultAsync();
            if (PlateDetails.ModuleId == 1 || PlateDetails.ModuleId == 3)
            {

                var tcpClient = _tCPServices.ConnectTechofor(tcpconf.TechniforIPAddress, tcpconf.TechniforPortNo);

                var data = _tCPServices.ConvertHexaFL1(model);
                NetworkStream stream = tcpClient.GetStream();
                foreach (var line in data)
                {
                    stream.Write(line);
                }
                stream.Close();

                var loadcmd = "#1";
                var log1 = new AppLogs()
                {
                    Log = "data updated in DB",
                    LogType = "normal",
                    ModuleId = PlateDetails.ModuleId,
                    UserId = PlateDetails.UserId,
                };
                _context.Add(log1);
                _context.SaveChanges();
                byte[] loadbyt = new byte[loadcmd.Length];
                for (int i = 0, j = 0; i < loadcmd.Length; i++, j++)
                {
                    loadbyt[j] = Convert.ToByte(loadcmd[i]);
                }
                var plcclient = _tCPServices.ConnectPLC(tcpconf.PLCIPAddress, tcpconf.PLCPortNo);
                NetworkStream plcstream = plcclient.GetStream();
                plcstream.Write(loadbyt);
                var log = new AppLogs()
                {
                    Log = "ready for punching",
                    LogType = "normal",
                    ModuleId = PlateDetails.ModuleId,
                    UserId = PlateDetails.UserId,
                };
                _context.Add(log);
                _context.SaveChanges();



                var response = "";
                var bytes = new byte[256];
                var data1 = plcstream.Read(bytes, 0, bytes.Length);
                response = System.Text.Encoding.ASCII.GetString(bytes, 0, data1);
                plcstream.Close();
                if (response.Contains("#2"))
                {
                    var punchingrecord = new PlatePunchingRecord()
                    {
                        PlateId = plateId,
                        PlateNumber = PlateDetails.PlateNumber,
                        ModuleId = PlateDetails.ModuleId,
                        TimeStamp = DateTime.Now,
                        Line1 = model.Line1,
                        Line2 = model.Line2,
                        Line3 = model.Line3,
                        Line4 = model.Line4,
                        Line5 = model.Line5,
                        Line6 = model.Line6
                    };
                    await _context.AddAsync(punchingrecord);
                    await _context.SaveChangesAsync();
                    return new APIResponse("Punching is success", 200);
                }
                else
                {
                    return new APIResponse("Punching not successfull", 200);
                }
            }
            else if (PlateDetails.ModuleId == 2)
            {
                //var client = new S7Client();
                //var connectionStatus = client.ConnectTo("192.168.0.1", 0, 1);
                //if(connectionStatus == 0)
                //{
                //    var databytes = new byte[350];
                //    var databytes1 = new byte[1900];
                //    client.DBRead(19, 0, databytes.Length, databytes);
                //    client.DBRead(1, 0, databytes1.Length, databytes1);

                //    var string1 = S7.GetStringAt(databytes, 0);
                //    var string2 = S7.GetStringAt(databytes, 256);
                //    var string3 = S7.GetStringAt(databytes, 512);


                //    var string4 = S7.GetByteAt(databytes1, 6);
                //    var string5 = S7.GetStringAt(databytes1, 10);

                //}

                _context.RemoveRange(_context.PlatePunchingDataForReceipe);
                _context.SaveChanges();
                var PunchingData = new PlatePunchingDataForReceipe()
                {
                    PlateId = plateId,
                    PlateNumber = PlateDetails.PlateNumber,
                    ModuleId = PlateDetails.ModuleId,
                    TimeStamp = DateTime.Now,
                    Line1 = model.Line1 == null ? " " : model.Line1,
                    Line2 = model.Line2 == null ? " " : model.Line2,
                    Line3 = model.Line3 == null ? " " : model.Line3,
                    Line4 = model.Line4 == null ? " " : model.Line4,
                    Line5 = model.Line5 == null ? " " : model.Line5,
                    Line6 = model.Line6 == null ? " " : model.Line6,
                    PlcAck = 0
                };
                _context.Add(PunchingData);
                _context.SaveChanges();
                var res = 1;
                if (UpdatePunchingOrMarking())
                {
                    res = 2;
                    var reportData = new PlatePunchingRecord()
                    {
                        PlateId = plateId,
                        ModuleId = PlateDetails.ModuleId,
                        PlateNumber = PlateDetails.PlateNumber,
                        TimeStamp = DateTime.Now,
                        Line1 = model.Line1,
                        Line2 = model.Line2,
                        Line3 = model.Line3,
                        Line4 = model.Line4,
                        Line5 = model.Line5,
                        Line6 = model.Line6,
                    };
                    await _context.AddAsync(reportData);
                }
                else
                {
                    return new APIResponse("error", 400);
                }
                await _context.SaveChangesAsync();
                if (res == 2)
                {
                    return new APIResponse("Data sent to Punching Successfully", 200);
                }
                else
                {
                    return new APIResponse("Failed to send details to Punching", 200);
                }
            }
            else
            {
                return new APIResponse("something went wrong", 400);
            }

        }

        public bool UpdatePunchingOrMarking()
        {
            bool Result = true;
            try
            {
                var Currentpunching = _context.PunchingCycleStatus.FirstOrDefault();
                if (Currentpunching == null)
                {
                    Currentpunching = new PunchingCycleStatus
                    {
                        StartPunching = 0,
                    };

                    _context.Add(Currentpunching);
                    _context.SaveChanges();
                }
                else
                {
                    Currentpunching.StartPunching = 1;
                    Currentpunching.MarkingStatusACK = 0;
                    _context.Update(Currentpunching);
                    _context.SaveChanges();


                    Thread.Sleep(30000);
                    Currentpunching.StartPunching = 0;
                    //Currentpunching.MarkingStatusACK = 1;
                    //Currentpunching.DataRequestACK = 1;
                    _context.Update(Currentpunching);
                    _context.SaveChanges();
                }


            }
            catch (Exception)
            {
                Result = false;
            }
            return Result;
        }


        public async Task<APIResponse> SendToMarking(FL1MarkingModel model)
        {
            var oldData = await _context.PlateMarkingDataForReceipe.ToListAsync();
            _context.RemoveRange(oldData);

            var plateId = Convert.ToInt32(model.plateNo);
            var PlateDetails = await _context.PlateInfoFromSAP.Where(x => x.PlateId == plateId).FirstOrDefaultAsync();
            PlateDetails.SentToMarkingTime = DateTime.Now;
            _context.Update(PlateDetails);

            var markingData = new PlateMarkingDataForReceipe()
            {
                PlateId = plateId,
                PlateNumber = PlateDetails.PlateNumber,
                ModuleId = PlateDetails.ModuleId,
                TimeStamp = DateTime.Now,
                Line1 = model.Line1,
                Line2 = model.Line2,
                Line3 = model.Line3,
                Line4 = model.Line4,
                Line5 = model.Line5,
                Line6 = model.Line6,
                MarkingPosition = 400,
            };
            var reportData = new PlateMarkingRecord()
            {
                PlateId = plateId,
                ModuleId = PlateDetails.ModuleId,
                PlateNumber = PlateDetails.PlateNumber,
                TimeStamp = DateTime.Now,
                Line1 = model.Line1,
                Line2 = model.Line2,
                Line3 = model.Line3,
                Line4 = model.Line4,
                Line5 = model.Line5,
                Line6 = model.Line6,
            };
            await _context.AddAsync(markingData);
            await _context.AddAsync(reportData);
            await _context.SaveChangesAsync();
            return new APIResponse("Data sent to Marking Successfully", 200);
        }


        public bool SaveCheckMark(long id, bool status)
        {
            var xx = _context.CheckBoxTable.Where(x => x.Id == id).FirstOrDefault();
            xx.IsMarked = status;
            _context.Update(xx);
            _context.SaveChanges();
            return true;
        }

        public bool SaveCheckPunch(long id, bool status)
        {
            var xx = _context.CheckBoxTable.Where(x => x.Id == id).FirstOrDefault();
            xx.IsPunched = status;
            _context.Update(xx);
            _context.SaveChanges();
            return true;
        }

        public void RefreshChecks()
        {
            var data = _context.CheckBoxTable.ToList();
            foreach (var x in data)
            {
                x.IsMarked = false;
                x.IsPunched = false;
            }
            _context.UpdateRange(data);
            _context.SaveChanges();
        }



        public async Task<APIResponse> LoadDataColdLeveller(ColdLevellerMarkingModel model)
        {
            var data1 = await _context.PlateDataFromSapColdLeveller.Where(x => x.Id == Convert.ToInt32(model.plateNo)).FirstOrDefaultAsync();
            var ys = "";
            if (string.IsNullOrWhiteSpace(data1.YST))
            {
                var yst = await _coldLevellerContext.TableMassGrade.Where(x => x.GradeName == data1.Grade).FirstOrDefaultAsync();
                if (yst == null)
                    return new APIResponse("YS value not found", 400);
                ys = yst.YS.ToString();
            }
            else
            {
                ys = data1.YST.ToString();
            }
            try
            {
                var oldData = _coldLevellerContext.CoilDataForLoading.Where(x => x.PlateNumber.ToUpper() == data1.PlateNumber.ToUpper()).FirstOrDefault();
                if (oldData == null)
                {
                    var data = new CoilDataForLoading()
                    {
                        DateTimeInsert = DateTime.Now,
                        PlateNumber = data1.PlateNumber == null ? data1.PlateNumber : data1.PlateNumber.ToUpper(),
                        Length = (int)Convert.ToDecimal(data1.Length),
                        SteelGrade = (int)Convert.ToDecimal(ys),
                        Thickness = Convert.ToDouble(data1.Thickness),
                        Weight = Convert.ToDouble(data1.Weight),
                        Width = Convert.ToDouble(data1.Width),
                        PlateStatus = 1,
                    };
                    await _coldLevellerContext.AddAsync(data);
                }
                else
                {
                    oldData.DateTimeInsert = DateTime.Now;
                    oldData.Length = (int)Convert.ToDecimal(data1.Length);
                    oldData.SteelGrade = (int)Convert.ToDecimal(ys);
                    oldData.Thickness = Convert.ToDouble(data1.Thickness);
                    oldData.Weight = Convert.ToDouble(data1.Weight);
                    oldData.Width = Convert.ToDouble(data1.Width);
                    oldData.PlateStatus = 1;
                    _coldLevellerContext.Update(oldData);
                }
                await _coldLevellerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new APIResponse("ERROR in CONNECTING DB" + ex.Message, 400);
            }

            var record = new ColdLevellerRecords()
            {
                PlateId = Convert.ToInt32(data1.Id),
                PlateNumber = data1.PlateNumber,
                Length = Convert.ToDecimal(data1.Length),
                Thickness = Convert.ToDecimal(data1.Thickness),
                SteelGrade = model.SteelGrade.Replace("STEEL GRADE -", ""),
                TimeStamp = DateTime.Now,
                CreatedDate = DateTime.Now,
                Weight = Convert.ToDecimal(data1.Weight),
                Width = Convert.ToDecimal(data1.Width)
            };

            await _context.AddAsync(record);
            await _context.SaveChangesAsync();

            return new APIResponse("Data Loaded Successfully", 200);
        }


        public DownCoilerAutoModeModel DCAutoModeData()
        {
            var response = new DownCoilerAutoModeModel();
            var dbSize = 12;
            var DbData = new byte[dbSize];
            var client = new S7Client();
            var connectionStatus = client.ConnectTo("10.10.2.33", 0, 2);
            if (connectionStatus == 0)
            {
                var dataReadResult = client.DBRead(501, 0, DbData.Length, DbData);
                if (dataReadResult == 0)
                {
                    response.MachineMode = S7.GetBitAt(DbData, 8, 3);
                    response.MarkerHomePosition = S7.GetBitAt(DbData, 9, 1);
                    response.MarkerFault = S7.GetBitAt(DbData, 9, 2);
                    response.MarkerReady = S7.GetBitAt(DbData, 9, 0);
                    response.MarkerActive = S7.GetBitAt(DbData, 9, 3);
                    response.MarkingCycleStatus = S7.GetBitAt(DbData, 9, 4);
                    response.MarkingAbortStatus = S7.GetBitAt(DbData, 9, 5);
                    var abc = GetCoilPositionFromOracle();
                    response.MatId = abc.MatId;
                    response.Position = abc.Position;
                    response.LiveWeight = abc.Weight != null ? abc.Weight.Value : 0;
                }
            }
            client.Disconnect();
            //response.MachineMode = true;
            //response.MarkerHomePosition = true;
            //response.MarkerFault = true;
            //response.MarkerReady = true;
            //response.MarkerActive = true;
            //response.MarkingCycleStatus = true;
            //response.MarkingAbortStatus = true;
            //var abc = GetCoilPositionFromOracle();
            //response.MatId = 100;
            //response.Position = 17;
            //response.LiveWeight = 12568;
            Thread.Sleep(1000);
            return response;
        }



        public CheckAutoModeResponse CheckAutoMode()
        {
            var response = new CheckAutoModeResponse();
            var dbSize = 12;
            var DbData = new byte[dbSize];
            var client = new S7Client();
            var connectionStatus = client.ConnectTo("10.10.2.33", 0, 2);
            if (connectionStatus == 0)
            {
                response.ConnectionStatus = true;
                var dataReadResult = client.DBRead(501, 0, DbData.Length, DbData);
                if (dataReadResult == 0)
                {
                    response.DataReadStatus = true;
                    response.AutoModeStatus = S7.GetBitAt(DbData, 8, 3);
                }
                else
                {
                    response.DataReadStatus = false;
                }
            }
            else
            {
                response.ConnectionStatus = false;
            }
            return response;
        }


        public string GetWeighingData()
        {
            var responseWeight = "";
            var ips = _context.TCPConfig.Where(x => x.ModuleId == 5).FirstOrDefault();

            try
            {
                var weightPLC = _tCPServices.ConnectPLC(ips.TechniforIPAddress, ips.TechniforPortNo);
                var buffer = new byte[256];
                var weight = weightPLC.GetStream();
                var actweight = weight.Read(buffer, 0, buffer.Length);
                var resWeight = System.Text.Encoding.ASCII.GetString(buffer, 0, actweight);
                var abc = JsonConvert.DeserializeObject(responseWeight);
                var array = resWeight.Split("   ");
                foreach (var x in array)
                {
                    if (responseWeight == "" && x.Contains("kg"))
                    {
                        var z = x.Replace("kg", "").Split(" ");
                        if (z.Count() > 0)
                        {
                            responseWeight = z.Where(x => x != "").Any() ? z.Where(x => x != "").Last() : "0";
                        }
                    }
                }
                weightPLC.Close();
                var abx = responseWeight.Replace(" ", "");
                Thread.Sleep(500);
                return abx;
            }
            catch (Exception ex)
            {
                Thread.Sleep(500);
                return ex.Message;
            }

        }



        public async Task<ServiceResponse<DCWeightResponse>> UpdateWeightInOracleAndSAP(string Weight)
        {
            var SAPCredentials = _context.SapCredential.Where(x => x.ModuleId == 5 && x.Type == 2).FirstOrDefault();
            var oracleDbDataList = _downCoilerContext.Coil.ToList();
            var downCoilerData = oracleDbDataList.OrderByDescending(x => x.MatId).FirstOrDefault();
            //downCoilerData.Weight = (int)(Convert.ToInt32(Weight)/1000);
            //_downCoilerContext.Update(downCoilerData);
            var weightinTon = (Convert.ToInt32(Weight) / 1000).ToString();

            var sapInput = new UpdateWeightDataModel()
            {
                CHARG = downCoilerData.ProductionId,
                WEIGHT = weightinTon
            };
            var jsonSap = JsonConvert.SerializeObject(sapInput);
            HttpClient client = new();
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonSap);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var authToken = Encoding.ASCII.GetBytes($"{SAPCredentials.SAPUserName}:{SAPCredentials.SAPPassword}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));
            string result = "";
            var plateResponse = new DownCoilerWeightResponseModel();
            try
            {
                var response = await client.PostAsync(SAPCredentials.SAPLink, byteContent);
                result = await response.Content.ReadAsStringAsync();
                plateResponse = JsonConvert.DeserializeObject<DownCoilerWeightResponseModel>(result);
            }
            catch
            {

            }
            if (plateResponse.RETURN1.Item.MESSAGE.Contains("Data sucessfully Updated"))
            {
                _downCoilerContext.SaveChanges();
                return new ServiceResponse<DCWeightResponse>($"Weight data updated in {downCoilerData.ProductionId}", 200, new DCWeightResponse { CoilId = downCoilerData.ProductionId, MatId = downCoilerData.MatId.ToString() });
            }
            else if (plateResponse.RETURN1.Item.MESSAGE.Contains("Batch already exist"))
            {
                _downCoilerContext.SaveChanges();
                return new ServiceResponse<DCWeightResponse>($"Weight data already updated in {downCoilerData.ProductionId}", 200, new DCWeightResponse { CoilId = downCoilerData.ProductionId, MatId = downCoilerData.MatId.ToString() });
            }
            else if (plateResponse.RETURN1.Item.MESSAGE.Contains("Batch Does Not exist"))
            {
                return new ServiceResponse<DCWeightResponse>($"Coil id {downCoilerData.ProductionId}  is not valid", 400, new DCWeightResponse { CoilId = downCoilerData.ProductionId, MatId = downCoilerData.MatId.ToString() });
            }
            else
            {
                return new ServiceResponse<DCWeightResponse>("some thing went wrong", 400, null);
            }
        }


        public CheckAutoModeResponse CheckMarkerReady()
        {
            var response = new CheckAutoModeResponse();
            var dbSize = 12;
            var DbData = new byte[dbSize];
            var client = new S7Client();
            var connectionStatus = client.ConnectTo("10.10.2.33", 0, 2);
            if (connectionStatus == 0)
            {
                response.ConnectionStatus = true;
                var dataReadResult = client.DBRead(501, 0, DbData.Length, DbData);
                if (dataReadResult == 0)
                {
                    response.DataReadStatus = true;
                    response.MarkerActive = S7.GetBitAt(DbData, 9, 0);
                    response.MarkerInHomePosition = S7.GetBitAt(DbData, 9, 1);
                }
                else
                {
                    response.DataReadStatus = false;
                }
            }
            else
            {
                response.ConnectionStatus = false;
            }
            return response;
        }



        public async Task<ServiceResponse<List<NewPlateDataModel>>> GetSapDataDownCoiler(DownCoilerModel model)
        {
            var data = new List<Coil>();
            var dbdata = new Coil();

            //var weight = Convert.ToInt32(GetWeighingData());
            //var loopcount = 0;
            //while (weight <= 1000 && loopcount < 10)
            //{
            //    weight = Convert.ToInt32(GetWeighingData());
            //    loopcount++;
            //}
            var roleId = Convert.ToInt32(model.RoleId);
            var moduleId = Convert.ToInt32(model.ModuleId);
            var user = await _context.UserRole.Where(x => x.RoleId == roleId && x.ModuleId == moduleId).FirstOrDefaultAsync();
            if (model.AutoMode)
            {
                if (!string.IsNullOrWhiteSpace(model.matid) && model.matid != "0")
                {
                    var matid = Convert.ToInt64(model.matid);
                    dbdata = _downCoilerContext.Coil.Where(x => x.MatId == matid).FirstOrDefault();
                }
                else
                {
                    data = _downCoilerContext.Coil.ToList();
                    dbdata = data.Where(x => x.Position == 17).FirstOrDefault();
                }
                //abc.Weight = x;
                //_downCoilerContext.Update(abc);
                //_downCoilerContext.SaveChanges();
            }
            else
            {
                //matid = Convert.ToInt32(model.matid);
                //abc = _downCoilerContext.Coil.Where(x => x.MatId == matid).FirstOrDefault();
            }

            var SAPCredentials = await _context.SapCredential.Where(x => x.ModuleId == 5 && x.Type == 1).OrderBy(x => x.Id).FirstOrDefaultAsync();
            var requestData = new DownCoilerPlateRequest()
            {
                CHARG = dbdata.ProductionId,
            };

            HttpClient client = new();
            var myContent = JsonConvert.SerializeObject(requestData);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var authToken = Encoding.ASCII.GetBytes($"{SAPCredentials.SAPUserName}:{SAPCredentials.SAPPassword}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));
            string result = "";
            try
            {
                var response = await client.PostAsync(SAPCredentials.SAPLink, byteContent);
                if (!response.IsSuccessStatusCode)
                    return new ServiceResponse<List<NewPlateDataModel>>("SAP API not responded", 400, null);
                result = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return new ServiceResponse<List<NewPlateDataModel>>("error", 400, null);
            }
            var plateResponse = JsonConvert.DeserializeObject<PlateResponse>(result);
            if (plateResponse.T_ZPLATE_MARKING == null)
                return new ServiceResponse<List<NewPlateDataModel>>("error", 400, null);
            var saveResponse = await _services.SavePlateDetail(plateResponse, moduleId, user.UserId);
            //var items = _services.GetItemList();
            var resData = _services.GetLineWiseData(plateResponse.T_ZPLATE_MARKING.item, moduleId);
            //var resData = _services.GetLineWiseData(items.FirstOrDefault(), moduleId);
            return new ServiceResponse<List<NewPlateDataModel>>("success", 200, resData);
        }




        public APIResponse SendToPunchingDownCoiler(DownCoilerMarkingModel model)
        {
            var coilList = _downCoilerContext.Coil.ToList();
            var plateId = Convert.ToInt32(model.plateNo);
            var sapData = _context.PlateInfoFromSAP.Where(x => x.PlateId == plateId).FirstOrDefault();
            var coilData = coilList.OrderByDescending(x => x.MatId).FirstOrDefault();
            var coilDia = Convert.ToInt16(coilData.CoilDiameter.Value);
            var client = new S7Client();
            var DB504Data = new byte[116];
            var DB21Data = new byte[120];
            var coilbytes = new byte[4];
            var shelLineLength = 22;
            var discLineLength = 14;
            var APIResponse = 0;

            int result = client.ConnectTo("10.10.2.33", 0, 2);
            client.DBWrite(504, 8, DB504Data.Length, DB504Data);
            client.DBWrite(504, 134, coilbytes.Length, coilbytes);
            client.DBWrite(21, 0, DB21Data.Length, DB21Data);
            client.DBWrite(21, 146, DB21Data.Length, DB21Data);


            if (result == 0)
            {
                if (string.IsNullOrWhiteSpace(model.shellLine1))
                {
                    S7.SetStringAt(DB504Data, 28, shelLineLength, model.shellLine1);
                    S7.SetStringAt(DB504Data, 50, shelLineLength, model.shellLine2);
                    S7.SetStringAt(DB504Data, 72, shelLineLength, model.shellLine3);
                    S7.SetStringAt(DB504Data, 94, shelLineLength, model.shellLine4);
                    S7.SetIntAt(coilbytes, 0, 23);
                    S7.SetIntAt(coilbytes, 2, coilDia);

                    S7.SetStringAt(DB21Data, 28, shelLineLength, model.shellLine1);
                    S7.SetStringAt(DB21Data, 50, shelLineLength, model.shellLine2);
                    S7.SetStringAt(DB21Data, 72, shelLineLength, model.shellLine3);
                    S7.SetStringAt(DB21Data, 94, shelLineLength, model.shellLine4);
                    S7.SetIntAt(DB21Data, 116, 23);
                    S7.SetIntAt(DB21Data, 118, coilDia);
                }
                if (string.IsNullOrWhiteSpace(model.discLine1))
                {
                    S7.SetStringAt(DB504Data, 0, 14, model.discLine1 == null ? "" : model.discLine1);
                    S7.SetStringAt(DB504Data, 14, 14, model.discLine2 == null ? "" : model.discLine2);

                    S7.SetStringAt(DB21Data, 0, 14, model.discLine1 == null ? "" : model.discLine1);
                    S7.SetStringAt(DB21Data, 14, 14, model.discLine2 == null ? "" : model.discLine2);
                }
                client.DBWrite(504, 8, DB504Data.Length, DB504Data);
                client.DBWrite(504, 134, coilbytes.Length, coilbytes);
                var res = SemiAutoModeCheck(client);
                if (res == 0)
                {
                    client.DBWrite(21, 0, DB21Data.Length, DB21Data);
                }
                else if (res == 2)
                {
                    client.DBWrite(21, 146, DB21Data.Length, DB21Data);
                }

                var compare = CompareData(client, res);
                if (compare)
                {
                    var start = new byte[1];
                    S7.SetBitAt(ref start, 0, 1, true);
                    client.DBWrite(501, 9, 1, start);
                    Thread.Sleep(2000);
                    S7.SetBitAt(ref start, 0, 4, true);
                    client.DBWrite(501, 9, 1, start);
                }
            }
            var counter = 0;
            do
            {
                var completeSequence = new byte[1];
                bool finish;
                bool stop;
                var compRead = client.DBRead(501, 9, 1, completeSequence);
                if (compRead == 0)
                {
                    finish = S7.GetBitAt(completeSequence, 9, 4);
                    stop = S7.GetBitAt(completeSequence, 9, 5);
                    if (finish)
                    {
                        APIResponse = 1;
                        var reportData = new DownCoilerReportsData()
                        {
                            PlateId = sapData.PlateId,
                            DiscLine1 = model.discLine1,
                            DiscLine2 = model.discLine2,
                            ShellLine1 = model.shellLine1,
                            ShellLine2 = model.shellLine2,
                            ShellLine3 = model.shellLine3,
                            ShellLine4 = model.shellLine4,
                            MatId = coilData.MatId.ToString(),
                            CoilDiameter = coilData.CoilDiameter.ToString(),
                            CoilWidth = coilData.SlabWidth.ToString(),
                            TimeStamp = DateTime.Now
                        };
                        _context.Add(reportData);
                        _context.SaveChanges();
                    }
                    if (stop)
                    {
                        APIResponse = 2;
                    }
                    if (finish || stop)
                    {
                        counter = 100;
                    }
                }
                counter++;
            }
            while (counter < 100);


            // Disconnect the client
            client.Disconnect();
            return new APIResponse("", 200);
        }


        private bool CompareData(S7Client client, int autoMode)
        {
            bool compare = false;
            var DB504Data = new byte[116];
            var DB505Data = new byte[116];
            var DB504Res = client.DBRead(504, 8, DB504Data.Length, DB504Data);
            var DB505Res = client.DBRead(505, 8, DB505Data.Length, DB505Data);
            if (DB504Res == 0 && DB505Res == 0)
            {
                compare = S7.GetStringAt(DB505Data, 8) == S7.GetStringAt(DB504Data, 8);
                compare = S7.GetStringAt(DB505Data, 22) == S7.GetStringAt(DB504Data, 22);
                compare = S7.GetStringAt(DB505Data, 36) == S7.GetStringAt(DB504Data, 36);
                compare = S7.GetStringAt(DB505Data, 58) == S7.GetStringAt(DB504Data, 58);
                compare = S7.GetStringAt(DB505Data, 80) == S7.GetStringAt(DB504Data, 80);
                compare = S7.GetStringAt(DB505Data, 102) == S7.GetStringAt(DB504Data, 102);
            }
            return compare;
        }


        private int SemiAutoModeCheck(S7Client client)
        {
            var data = new byte[2];
            var res = client.DBRead(1, 0, 2, data);
            if (res == 0)
            {
                var autoMode = S7.GetBitAt(data, 1, 2);
                var semiAutoMode = S7.GetBitAt(data, 1, 3);
                if (autoMode)
                {
                    return 1;
                }
                else if (semiAutoMode)
                {
                    return 2;
                }
            }
            return 0;
        }

        public async Task<List<TemplateDropDown>> GetTemplateDropDown(long ModuleId)
        {
            var data = await _context.TemplateMaster.Where(x => x.ModuleId == ModuleId).Select(x => new TemplateDropDown { Id = x.Id, TemplateName = x.TemplateName, IsDefault = x.IsDefault }).ToListAsync();
            var response = new List<TemplateDropDown>();
            var response1 = new List<TemplateDropDown>();
            foreach (var res in data)
            {
                if (res.IsDefault == true)
                {
                    response.Add(res);
                }
                else
                {
                    response1.Add(res);
                }
            }
            response.AddRange(response1);


            return response;
        }


        public async Task<List<TableRows>> GetTemplateRows(long TemplateId)
        {
            var data = await _context.TemplateRows.Where(x => x.TemplateMasterId == TemplateId).Select(x => new TableRows { Id = x.Id, Row = x.Row, IsDisc = x.DiscMarking, IsShell = x.ShellMarking }).ToListAsync();
            return data;
        }


        public async Task AddTemplate(AddTemplateModel model)
        {
            if (model.templateId == 0)
            {
                if (model.isDefault == true)
                {
                    var existingDefault = _context.TemplateMaster.Where(x => x.ModuleId == model.moduleId && x.IsDefault == true).ToList();
                    foreach (var x in existingDefault)
                    {
                        x.IsDefault = false;
                    }
                    _context.UpdateRange(existingDefault);
                    _context.SaveChanges();
                }
                var templateRows = new List<TemplateRows>();
                var data = new TemplateRows();
                var newTemplate = new TemplateMaster()
                {
                    TemplateName = model.templateName,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    IsDefault = model.isDefault,
                    IsDeleted = false,
                    ModuleId = model.moduleId,
                    UpdatedDate = DateTime.Now
                };
                await _context.TemplateMaster.AddAsync(newTemplate);
                await _context.SaveChangesAsync();

                foreach (var line in model.Lines)
                {
                    var newLine = new TemplateRows()
                    {
                        TemplateMasterId = newTemplate.Id,
                        Row = line,
                        CreatedDate = DateTime.Now,
                    };
                    templateRows.Add(newLine);
                }

                await _context.AddRangeAsync(templateRows);
                await _context.SaveChangesAsync();
            }
            else
            {
                var lines = _context.TemplateRows.Where(x => x.TemplateMasterId == model.templateId).ToList();
                _context.RemoveRange(lines);
                _context.SaveChanges();

                lines.Clear();

                foreach (var line in model.Lines)
                {
                    var newLine = new TemplateRows()
                    {
                        TemplateMasterId = model.templateId,
                        Row = line,
                        CreatedDate = DateTime.Now,
                    };
                    lines.Add(newLine);
                }

                await _context.AddRangeAsync(lines);
                await _context.SaveChangesAsync();
            }

        }


        public async Task AddDCTemplate(AddDCTemplateModel model)
        {
            if (model.templateId == 0)
            {
                if (model.isDefault == true)
                {
                    var existingDefault = _context.TemplateMaster.Where(x => x.ModuleId == model.moduleId && x.IsDefault == true).ToList();
                    foreach (var x in existingDefault)
                    {
                        x.IsDefault = false;
                    }
                    _context.UpdateRange(existingDefault);
                    _context.SaveChanges();
                }
                var templateRows = new List<TemplateRows>();
                var data = new TemplateRows();
                var newTemplate = new TemplateMaster()
                {
                    TemplateName = model.templateName,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    IsDefault = model.isDefault,
                    IsDeleted = false,
                    ModuleId = model.moduleId,
                    UpdatedDate = DateTime.Now
                };
                await _context.TemplateMaster.AddAsync(newTemplate);
                await _context.SaveChangesAsync();

                foreach (var line in model.Lines)
                {
                    var newLine = new TemplateRows()
                    {
                        TemplateMasterId = newTemplate.Id,
                        Row = line.row,
                        ShellMarking = line.isShell,
                        DiscMarking = line.isDisc,
                        CreatedDate = DateTime.Now,
                    };
                    templateRows.Add(newLine);
                }

                await _context.AddRangeAsync(templateRows);
                await _context.SaveChangesAsync();
            }
            else
            {
                var lines = _context.TemplateRows.Where(x => x.TemplateMasterId == model.templateId).ToList();
                _context.RemoveRange(lines);
                _context.SaveChanges();

                lines.Clear();

                foreach (var line in model.Lines)
                {
                    var newLine = new TemplateRows()
                    {
                        TemplateMasterId = model.templateId,
                        Row = line.row,
                        ShellMarking = line.isShell,
                        DiscMarking = line.isDisc,
                        CreatedDate = DateTime.Now,
                    };
                    lines.Add(newLine);
                }

                await _context.AddRangeAsync(lines);
                await _context.SaveChangesAsync();
            }

        }



        public async Task MakeTemplateDefault(DefaultTemplateModel model)
        {
            var template = await _context.TemplateMaster.Where(x => x.ModuleId == model.ModeuleId).ToListAsync();
            foreach (var x in template)
            {
                x.IsDefault = false;
            }
            template.Where(x => x.Id == model.TemplateId).FirstOrDefault().IsDefault = true;
            _context.UpdateRange(template);
            await _context.SaveChangesAsync();

        }



        public MechineModeResponse GetMarkerSequence()
        {
            var response = new MechineModeResponse();
            var client = new S7Client();
            var ips = _context.TCPConfig.Where(x => x.ModuleId == 2).FirstOrDefault();
            var connectionResponse = client.ConnectTo(ips.TechniforIPAddress, ips.TechniforRack.Value, ips.TechniforSlot.Value);
            if (connectionResponse == 0)
            {
                var databytes = new byte[10];
                client.DBRead(1, 0, databytes.Length, databytes);
                client.Disconnect();
                response.Mode = (S7.GetByteAt(databytes, 1).ToString());
                response.Sequence = (S7.GetByteAt(databytes, 5)).ToString();
            }
            else
            {
                response.Mode = "100";
                response.Sequence = "100";
            }
            var dbRecord = _context.MarkerSequenceRecord.FirstOrDefault();
            if (dbRecord == null)
            {
                dbRecord = new MarkerSequenceRecord()
                {
                    Mode = response.Mode,
                    Sequence = response.Sequence
                };
                _context.Add(dbRecord);
            }
            else
            {
                dbRecord.Mode = response.Mode;
                dbRecord.Sequence = response.Sequence;
                _context.Update(dbRecord);
            }
            _context.SaveChanges();
            return response;
        }

        public void SetMarkingBit()
        {
            var client = new S7Client();
            var connectionResponse = client.ConnectTo("192.168.0.1", 0, 1);
            if (connectionResponse == 0)
            {
                var data = new byte[1];
                S7.SetByteAt(data, 0, 1);
                client.DBWrite(1, 6, data.Length, data);
                client.Disconnect();
            }
        }


        public void DeleteTemplate(int templateId)
        {
            _context.RemoveRange(_context.TemplateRows.Where(x => x.TemplateMasterId == templateId));
            _context.Remove(_context.TemplateMaster.Where(x => x.Id == templateId).FirstOrDefault());
            _context.SaveChanges();
        }



        public void SaveLogs(string userId, string moduleId, string log, string type)
        {
            var userid = _context.User.Where(x => x.UserName == userId).Select(x => x.UserId).FirstOrDefault();


            _context.Add(new AppLogs()
            {
                UserId = userid,
                ModuleId = Convert.ToInt64(moduleId),
                Log = log,
                LogType = type,
            });
            _context.SaveChanges();
        }



        public async Task<ServiceResponse<string>> UpdateWeightdata(string weight)
        {
            var intweight = Convert.ToInt32(weight);
            var coilsdata = _downCoilerContext.Coil.ToList();
            var coildata = coilsdata.Where(x => x.Position == 17 || x.Position == 16).FirstOrDefault();
            if (coildata == null)
                return new ServiceResponse<string>("No Coil in Position 16 or 17", 200, null);
            coildata.Weight = intweight;
            _downCoilerContext.Update(coildata);
            _downCoilerContext.SaveChanges();
            var sapcredentials = _context.SapCredential.Where(x => x.ModuleId == 5 && x.Type == 2).OrderByDescending(x => x.Id).FirstOrDefault();
            var data = new WeightUpdateModel
            {
                CHARG = coildata.ProductionId.ToUpper(),
                WEIGHT = (intweight / 1000).ToString(),
            };
            HttpClient client = new();
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var authToken = Encoding.ASCII.GetBytes($"{sapcredentials.SAPUserName}:{sapcredentials.SAPPassword}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));
            string result = "";
            var DCresponse = new DownCoilerWeightResponseModel();
            try
            {
                var response = await client.PostAsync(sapcredentials.SAPLink, byteContent);
                if (!response.IsSuccessStatusCode)
                    return new ServiceResponse<string>("SAP API not responded", 400, null);
                result = await response.Content.ReadAsStringAsync();
                DCresponse = JsonConvert.DeserializeObject<DownCoilerWeightResponseModel>(result);
            }
            catch
            {
                return new ServiceResponse<string>("error", 400, null);
            }
            if (DCresponse == null)
                return new ServiceResponse<string>("no data found", StatusCodes.Status404NotFound, null);
            if (DCresponse.RETURN1.Item.MESSAGE.Contains("Data sucessfully Updated"))
            {
                var newEntry = new CoilWeightUpdateData
                {
                    CoilId = coildata.ProductionId.ToUpper(),
                    Weight = weight,
                    CreatedDate = DateTime.Now
                };
                _context.Add(newEntry);
                _context.SaveChanges();
                return new ServiceResponse<string>("Weight Data Updated to SAP successfully", 200, coildata.ProductionId);
            }
            if (DCresponse.RETURN1.Item.MESSAGE.Contains("Batch already exist"))
                return new ServiceResponse<string>("Batch already exist in SAP", 200, coildata.ProductionId);
            if (DCresponse.RETURN1.Item.MESSAGE.Contains("Batch Does Not exist"))
                return new ServiceResponse<string>("Batch Does Not exist in SAP", 200, coildata.ProductionId);
            return new ServiceResponse<string>("unknown error", 400, coildata.ProductionId);
            //return new ServiceResponse<string>("unknown error", 200, "PA31980B1");
        }



        public OracleWeightDataResponse GetCoilPositionFromOracle()
        {
            var res = new OracleWeightDataResponse();

            var numbWeight = 0;
            var numbWeight2 = 0;
            var counter = 0;
            var counter2 = 0;
            var coils = _downCoilerContext.Coil.ToList();
            var latestDatas = _downCoilerContext.Coil.Where(x => x.Position == 17 || x.Position == 16).ToList();
            var latestData = new Coil();
            if (latestDatas.Count < 2)
            {
                latestData = latestDatas.FirstOrDefault();
            }
            else
            {
                res.Weight = -1;
            }


            while (counter < 6)
            {
                //if (latestData != null)
                //{
                var weight1 = GetWeighingData();
                if (numbWeight == 0)
                {
                    try
                    {
                        numbWeight = Convert.ToInt32(weight1);
                    }
                    catch
                    {
                        numbWeight = 0;
                    }
                }
                if (numbWeight == numbWeight2)
                {
                    counter++;
                }
                else
                {
                    numbWeight2 = numbWeight;
                    counter = 0;
                }
                //}
                counter2++;
                if (counter2 > 50)
                {
                    counter = 6;
                }
            }
            if (counter2 > 50)
            {
                res.Weight = null;
            }
            else
            {
                res.Weight = numbWeight2;
            }
            if (latestData == null)
            {
                res.Position = 0;
                res.MatId = 0;
                res.CoilId = "0";
                res.Weight = res.Weight == null ? 0 : res.Weight;
                return res;
            }
            res.Position = latestData.Position == null ? 0 : (int)latestData.Position;
            res.MatId = latestData.MatId;
            res.CoilId = latestData.ProductionId;
            Thread.Sleep(500);
            return res;


        }


        public async Task<ServiceResponse<string>> ManualWeightUpdate(ManualWeightUpdate model)
        {
            var intweight = Convert.ToInt64(model.Weight);
            var sapcredentials = _context.SapCredential.Where(x => x.ModuleId == 5 && x.Type == 2).OrderByDescending(x => x.Id).FirstOrDefault();
            var data = new WeightUpdateModel
            {
                CHARG = model.CoilId.ToUpper(),
                WEIGHT = (intweight / 1000).ToString(),
            };
            HttpClient client = new();
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var authToken = Encoding.ASCII.GetBytes($"{sapcredentials.SAPUserName}:{sapcredentials.SAPPassword}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));
            string result = "";
            var DCresponse = new DownCoilerWeightResponseModel();
            try
            {
                var response = await client.PostAsync(sapcredentials.SAPLink, byteContent);
                if (!response.IsSuccessStatusCode)
                    return new ServiceResponse<string>("SAP API not responded", 400, null);
                result = await response.Content.ReadAsStringAsync();
                DCresponse = JsonConvert.DeserializeObject<DownCoilerWeightResponseModel>(result);
            }
            catch
            {
                return new ServiceResponse<string>("error", 400, null);
            }
            if (DCresponse == null)
                return new ServiceResponse<string>("no data found", StatusCodes.Status404NotFound, null);
            if (DCresponse.RETURN1.Item.MESSAGE.Contains("Data sucessfully Updated"))
            {
                var newEntry = new CoilWeightUpdateData
                {
                    CoilId = model.CoilId.ToUpper(),
                    Weight = model.Weight,
                    CreatedDate = DateTime.Now
                };
                _context.Add(newEntry);
                _context.SaveChanges();
                return new ServiceResponse<string>("Weight Data Updated to SAP successfully", 200, model.CoilId);
            }
            if (DCresponse.RETURN1.Item.MESSAGE.Contains("Batch already exist"))
                return new ServiceResponse<string>("Batch already exist in SAP", 200, model.CoilId);
            if (DCresponse.RETURN1.Item.MESSAGE.Contains("Batch Does Not exist"))
                return new ServiceResponse<string>("Batch Does Not exist in SAP", 200, model.CoilId);
            return new ServiceResponse<string>("unknown error", 400, model.CoilId);
        }
    }

}

