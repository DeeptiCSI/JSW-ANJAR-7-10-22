using CSI_PPMS.ColdLevellerDb;
using CSI_PPMS.Entity;
using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using CSI_PPMS_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSI_PPMS.Services
{
    public class SAPServices : ISAPServices
    {
        private readonly IConfiguration _configuration;
        private readonly PPMSContext _context;
        private readonly ColdLevellerContext _coldLevellerContext;

        public SAPServices(IConfiguration configuration,
                           PPMSContext context,
                           ColdLevellerContext coldLevellerContext)
        {
            _configuration = configuration;
            _context = context;
            _coldLevellerContext = coldLevellerContext;
        }


        public async Task<ServiceResponse<item>> GetPlateDataFromSAP(SAPInputModel model)
        {
            var usermodule = await _context.UserRole.Where(x => x.RoleId == model.roleId && x.ModuleId == model.moduleId).FirstOrDefaultAsync();
            var SAPCredentials = await _context.SapCredential.Where(x => x.ModuleId == model.moduleId).FirstOrDefaultAsync();
            if (model.plateNo.Length == 0)
                return new ServiceResponse<item>("invalid plate no", 400, null);
            var plateRequest = new PlateRequest()
            {
                CHARG = model.plateNo.ToUpper()
            };
            HttpClient client = new();
            var myContent = JsonConvert.SerializeObject(plateRequest);
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
                    return new ServiceResponse<item>("SAP API not responded", 400, null);
                result = await response.Content.ReadAsStringAsync();

            }
            catch
            {
                return new ServiceResponse<item>("error", 400, null);
            }
            var plateResponse = JsonConvert.DeserializeObject<PlateResponse>(result);
            if (plateResponse.T_ZPLATE_MARKING == null)
                return new ServiceResponse<item>("no data found", StatusCodes.Status404NotFound, null);
            var data = await SavePlateDetail(plateResponse, model.moduleId, usermodule.UserId);
            return new ServiceResponse<item>(data.Messege, 200, plateResponse.T_ZPLATE_MARKING.item);
        }

        public async Task<ServiceResponse<ColdLevellerItem>> GetCLPlateDataFromSAP(SAPInputModel model)
        {
            var usermodule = await _context.UserRole.Where(x => x.RoleId == model.roleId && x.ModuleId == model.moduleId).FirstOrDefaultAsync();
            var SAPCredentials = await _context.SapCredential.Where(x => x.ModuleId == model.moduleId).FirstOrDefaultAsync();
            if (model.plateNo.Length == 0)
                return new ServiceResponse<ColdLevellerItem>("invalid plate no", 400, null);
            var plateRequest = new PlateRequest()
            {
                CHARG = model.plateNo.ToUpper()
            };
            HttpClient client = new();
            var myContent = JsonConvert.SerializeObject(plateRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var authToken = Encoding.ASCII.GetBytes($"{SAPCredentials.SAPUserName}:{SAPCredentials.SAPPassword}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));
            string result = "";
            var plateResponse = new ColdLevellerItem();
            try
            {
                var response = await client.PostAsync(SAPCredentials.SAPLink, byteContent);
                if (!response.IsSuccessStatusCode)
                    return new ServiceResponse<ColdLevellerItem>("SAP API not responded", 400, null);
                result = await response.Content.ReadAsStringAsync();
                plateResponse = JsonConvert.DeserializeObject<ColdLevellerItem>(result);
            }
            catch
            {
                return new ServiceResponse<ColdLevellerItem>("error", 400, null);
            }
            if (plateResponse == null)
                return new ServiceResponse<ColdLevellerItem>("no data found", StatusCodes.Status404NotFound, null);
            plateResponse.CHARG = model.plateNo.ToUpper();
            var data = await SavePlateDetail(plateResponse, model.moduleId, usermodule.UserId);
            return new ServiceResponse<ColdLevellerItem>(data.Messege, 200, plateResponse);
        }




        public async Task<APIResponse> SavePlateDetail(PlateResponse plateResponse, long moduleId, long userid)
        {
            var plateinfo = new PlateInfoFromSAP()
            {
                PlateNumber = plateResponse.T_ZPLATE_MARKING.item.CHARG,
                ActualWeight = plateResponse.T_ZPLATE_MARKING.item.WEIGHT,
                HeatNumber = plateResponse.T_ZPLATE_MARKING.item.HEAT_NO,
                Grade = plateResponse.T_ZPLATE_MARKING.item.GRADE,
                GradeDuel = plateResponse.T_ZPLATE_MARKING.item.GRADE_D,
                Length = plateResponse.T_ZPLATE_MARKING.item.LENGTH,
                ProjectName = plateResponse.T_ZPLATE_MARKING.item.PROJ_NAME,
                MaterialDescription = plateResponse.T_ZPLATE_MARKING.item.ARKTX,
                PurchaseOrder = plateResponse.T_ZPLATE_MARKING.item.P_ORDER,
                PurchaseOrderNumber = plateResponse.T_ZPLATE_MARKING.item.PO_NUMBER,
                Thickness = plateResponse.T_ZPLATE_MARKING.item.THICK,
                Weight = plateResponse.T_ZPLATE_MARKING.item.WEIGHT,
                CustomerName = plateResponse.T_ZPLATE_MARKING.item.CUST_NAME,
                CustomerReference = plateResponse.T_ZPLATE_MARKING.item.CUSTOMER_REF,
                DataFromSAPDate = DateTime.Now,
                ModuleId = moduleId,
                UserId = userid,
                Width = plateResponse.T_ZPLATE_MARKING.item.WIDTH,
            };
            await _context.AddAsync(plateinfo);
            await _context.SaveChangesAsync();
            return new APIResponse(plateinfo.PlateId.ToString(), 200);
        }

        private async Task<APIResponse> SavePlateDetail(item item, long moduleId, long userid)
        {
            var plateinfo = new PlateInfoFromSAP()
            {
                PlateNumber = item.CHARG,
                ActualWeight = item.WEIGHT,
                HeatNumber = item.HEAT_NO,
                Grade = item.GRADE,
                GradeDuel = item.GRADE_D,
                Length = item.LENGTH,
                ProjectName = item.PROJ_NAME,
                MaterialDescription = item.ARKTX,
                PurchaseOrder = item.P_ORDER,
                PurchaseOrderNumber = item.PO_NUMBER,
                Thickness = item.THICK,
                Weight = item.WEIGHT,
                CustomerName = item.CUST_NAME,
                CustomerReference = item.CUSTOMER_REF,
                DataFromSAPDate = DateTime.Now,
                ModuleId = moduleId,
                UserId = userid,
                Width = item.WIDTH,
            };
            await _context.AddAsync(plateinfo);
            await _context.SaveChangesAsync();
            return new APIResponse(plateinfo.PlateId.ToString(), 200);
        }

        private async Task<APIResponse> SavePlateDetail(ColdLevellerItem item, long moduleId, long userid)
        {
            var plateinfo = new PlateDataFromSapColdLeveller()
            {
                PlateNumber = item.CHARG,
                Grade = item.GRADE,
                Length = item.LNTH,
                Thickness = item.THK,
                Weight = item.WEIGHT,
                ModuleId = moduleId,
                UserId = userid,
                Width = item.WDTH,
                Return1 = item.RETURN1,
                YST = item.YS_T
            };
            await _context.AddAsync(plateinfo);
            await _context.SaveChangesAsync();
            return new APIResponse(plateinfo.Id.ToString(), 200);
        }


        public async Task<ServiceResponse<ColdLevellerItem>> GetCLSamplePlateData(SAPInputModel model)
        {
            var plateid = new APIResponse();
            var LstItem = GetCLItems();
            var usermodule = await _context.UserRole.Where(x => x.RoleId == model.roleId && x.ModuleId == model.moduleId).FirstOrDefaultAsync();
            var data = LstItem.Where(x => x.CHARG == model.plateNo).FirstOrDefault();
            if (data != null)
            {
                plateid = await SavePlateDetail(data, model.moduleId, usermodule.UserId);
                return new ServiceResponse<ColdLevellerItem>(plateid.Messege, 200, data);
            }
            else
            {
                return new ServiceResponse<ColdLevellerItem>("error", 400, null);
            }
        }





        public async Task<ServiceResponse<item>> GetSamplePlateData(SAPInputModel model)
        {
            var plateid = new APIResponse();
            var LstItem = GetItemList();
            var usermodule = await _context.UserRole.Where(x => x.RoleId == model.roleId && x.ModuleId == model.moduleId).FirstOrDefaultAsync();
            var data = LstItem.Where(x => x.CHARG == model.plateNo).FirstOrDefault();
            if (data != null)
            {
                plateid = await SavePlateDetail(data, model.moduleId, usermodule.UserId);
                return new ServiceResponse<item>(plateid.Messege, 200, data);
            }
            else
            {
                return new ServiceResponse<item>("error", 400, null);
            }
        }

        public List<NewPlateDataModel> GetLineWiseData(item item, long moduleId, int templateId = 0)
        {
            if (item == null)
                return null;
            var checks = _context.CheckBoxTable.ToList();
            templateId = (int)_context.TemplateMaster.Where(x => x.ModuleId == moduleId && x.IsDefault == true).Select(x => x.Id).FirstOrDefault();

            List<NewPlateDataModel> Lstmodel = new();
            List<NewPlateDataModel> response = new();


            var thick = Math.Round((Convert.ToDecimal(item.THICK)), 1).ToString();
            if (thick.Split(".").Length == 2)
            {
                if (thick.Split(".")[1].Contains("0"))
                {
                    thick = thick.Split(".")[0];
                }
            }
            var width = Math.Round((Convert.ToDecimal(item.WIDTH)), 1).ToString();
            if (width.Split(".").Length == 2)
            {
                if (width.Split(".")[1].Contains("0"))
                {
                    width = width.Split(".")[0];
                }
            }
            var length = Math.Round((Convert.ToDecimal(item.LENGTH)), 1).ToString();
            if (length.Split(".").Length == 2)
            {
                if (length.Split(".")[1].Contains("0"))
                {
                    length = length.Split(".")[0];
                }
            }
            if (moduleId != 5)
            {
                var ItemDisplay = new ItemPreview()
                {
                    PLATE_NO = item.CHARG,
                    HEAT_NO = item.HEAT_NO,
                    GRADE_NO_1 = item.GRADE,
                    GRADE_NO_2 = item.GRADE_D,
                    SIZE = thick + "X" + width + "X" + length,
                    CUSTOMER_REF = item.CUSTOMER_REF,
                    PO_ORDER = item.P_ORDER.ToString(),
                    PO_NUMBER = item.PO_NUMBER.ToString(),
                    WEIGHT = item.WEIGHT,
                    CUSTOMER_NAME = item.CUST_NAME
                };

                PropertyInfo[] properties = typeof(ItemPreview).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    NewPlateDataModel model = new()
                    {
                        Punching = false,
                        Prefix_Text = property.Name.Replace("_", " ") + " -" + property.GetValue(ItemDisplay, null)
                    };
                    if (property.Name.Contains("JSW"))
                    {
                        model.Prefix_Text = property.Name.Replace("_", " ");
                    }
                    if (property.Name.Contains("GRADE_NO_1"))
                    {
                        model.Prefix_Text = "GRADE -";
                        var grad = property.GetValue(ItemDisplay, null).ToString();
                        if (grad.Length > 0)
                        {
                            model.Prefix_Text = model.Prefix_Text + grad;
                        }
                    }
                    if (property.Name.Contains("GRADE_NO_2"))
                    {
                        var grad = property.GetValue(ItemDisplay, null) == null ? "" : property.GetValue(ItemDisplay, null).ToString();
                        if (grad.Length > 0)
                        {
                            model.Prefix_Text = "GRADE2 -" + grad;
                        }
                        else
                        {
                            model.Prefix_Text = "GRADE2 -" + grad;
                        }
                    }

                    Lstmodel.Add(model);
                }
            }
            else
            {
                var ItemDisplay = new ItemDownCoilerPreview()
                {
                    COIL_NO = item.CHARG,
                    HEAT_NO = item.HEAT_NO,
                    WIDTH = item.WIDTH,
                    THICK = item.THICK,
                    P_ORDER = item.P_ORDER.ToString(),
                    GRADE = item.GRADE,
                    CUST_NAME = item.CUST_NAME,
                    PO_NUMBER = item.PO_NUMBER.ToString(),
                    ACT_WEIGHT = item.ACT_WEIGHT.ToString()
                };

                PropertyInfo[] properties = typeof(ItemDownCoilerPreview).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    NewPlateDataModel model = new()
                    {
                        Punching = false,
                        Prefix_Text = property.Name.Replace("_", " ") + " -" + property.GetValue(ItemDisplay, null)
                    };
                    if (property.Name.Contains("JSW"))
                    {
                        model.Prefix_Text = property.Name.Replace("_", " ");
                    }

                    //model.Prefix_Text = "GRADE";
                    //var grad = property.GetValue(ItemDisplay, null) == null ? "" : property.GetValue(ItemDisplay, null).ToString();
                    //if (grad.Length > 0)
                    //{
                    //    model.Prefix_Text = model.Prefix_Text + " -" + grad;
                    //}

                    Lstmodel.Add(model);
                }
            }

            foreach (var transaction in Lstmodel)
            {
                transaction.LineNo = Lstmodel.IndexOf(transaction) + 1;
            }


            var templateRows = _context.TemplateRows.Where(x => x.TemplateMasterId == templateId).ToList();
            foreach (var row in templateRows)
            {
                row.Id = templateRows.IndexOf(row);
                //row.Row = moduleId == 5 ? row.Row.Contains("data") ? row.Row.Substring(0, row.Row.Length - 7).TrimEnd().TrimStart().Replace("PLATE NO", "COIL NO") : row.Row : row.Row.Contains("data") ? row.Row.Substring(0, row.Row.Length - 7).TrimEnd().TrimStart() : row.Row;
            }
            var added = false;


            foreach (var row in templateRows)
            {
                foreach(var data in Lstmodel)
                {
                    //var abc = row.Row.Substring((row.Row.IndexOf("<") + 1), (row.Row.Length - (row.Row.IndexOf("<") + 2)));
                    if (data.Prefix_Text.Contains(row.Row.Substring((row.Row.IndexOf("<") + 1), (row.Row.Length - (row.Row.IndexOf("<") + 2)))) && added == false)
                    {
                        if (moduleId == 5)
                        {
                            data.Marking = row.ShellMarking != null ? row.ShellMarking.Value : checks[(int)row.Id].IsMarked;
                            data.Punching = row.DiscMarking != null ? row.DiscMarking.Value : checks[(int)row.Id].IsPunched;
                        }
                        else
                        {
                            data.Marking = /*row.ShellMarking != null ? row.ShellMarking.Value : */checks[(int)row.Id].IsMarked;
                            data.Punching = /*row.DiscMarking != null ? row.DiscMarking.Value : */checks[(int)row.Id].IsPunched;
                        }
                        var prefix = data.Prefix_Text;
                        data.Prefix_Text = row.Row.Contains("<") ? row.Row.Substring(0, row.Row.IndexOf("<")) + (prefix.Split("-").Length > 1 ? prefix.Split("-")[1] : prefix) : row.Row;
                        response.Add(data);
                        added = true;
                    }
                }
                if(added == false)
                {
                    var data = new NewPlateDataModel();
                    if (moduleId == 5)
                    {
                        data.Marking = row.ShellMarking != null ? row.ShellMarking.Value : checks[(int)row.Id].IsMarked;
                        data.Punching = row.DiscMarking != null ? row.DiscMarking.Value : checks[(int)row.Id].IsPunched;
                    }
                    else
                    {
                        data.Marking = /*row.ShellMarking != null ? row.ShellMarking.Value : */checks[(int)row.Id].IsMarked;
                        data.Punching = /*row.DiscMarking != null ? row.DiscMarking.Value : */checks[(int)row.Id].IsPunched;
                    }
                    data.Prefix_Text = row.Row;
                    response.Add(data);
                }
                added = false;

            }
            return response;
        }


        public List<NewPlateDataModel> GetLineWiseData(ColdLevellerItem item, long moduleId)
        {
            var checks = _context.CheckBoxTable.ToList();

            List<NewPlateDataModel> Lstmodel = new();
            List<NewPlateDataModel> response = new();
            if (item == null)
                return null;

            var thick = Math.Round((Convert.ToDecimal(item.THK)), 1).ToString();
            if (thick.Split(".").Length == 2)
            {
                if (thick.Split(".")[1].Contains("0"))
                {
                    thick = thick.Split(".")[0];
                }
            }
            var width = Math.Round((Convert.ToDecimal(item.WDTH)), 1).ToString();
            if (width.Split(".").Length == 2)
            {
                if (width.Split(".")[1].Contains("0"))
                {
                    width = width.Split(".")[0];
                }
            }
            var length = Math.Round((Convert.ToDecimal(item.LNTH)), 1).ToString();
            if (length.Split(".").Length == 2)
            {
                if (length.Split(".")[1].Contains("0"))
                {
                    length = length.Split(".")[0];
                }
            }
            var ItemDisplay = new ColdLevellerItemPreview()
            {
                WEIGHT = item.WEIGHT,
                LENGTH = length,
                WIDTH = width,
                THICKNESS = thick,
                YTS = item.YS_T
            };



            PropertyInfo[] properties = typeof(ColdLevellerItemPreview).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                NewPlateDataModel model = new()
                {
                    Punching = false,
                    Prefix_Text = property.Name.Replace("_", " ") + " - " + property.GetValue(ItemDisplay, null)
                };

                if (property.Name.Contains("YTS"))
                {
                    model.Prefix_Text = "STEEL GRADE -";
                    var grad = property.GetValue(ItemDisplay, null).ToString();
                    if (grad.Length > 0)
                    {
                        model.Prefix_Text = model.Prefix_Text + " " + grad;
                    }
                }

                Lstmodel.Add(model);
            }

            foreach (var transaction in Lstmodel)
            {
                transaction.LineNo = Lstmodel.IndexOf(transaction) + 1;
            }

            foreach (var x in checks)
            {
                var data = Lstmodel.Where(c => c.LineNo == x.Id).FirstOrDefault();
                if (data != null)
                {
                    data.Punching = x.IsPunched;
                    response.Add(data);
                }
            }

            return response;
        }




        public async Task<YSValueModel> GetYSValueFromOracleDb(string plateNo)
        {
            var plateid = Convert.ToInt32(plateNo);
            var data = await _context.PlateDataFromSapColdLeveller.Where(x => x.Id == plateid).FirstOrDefaultAsync();
            var YsData = await _coldLevellerContext.TableMassGrade.Where(x => x.GradeName == data.Grade).FirstOrDefaultAsync();
            if (YsData != null)
            {
                if (YsData.GradeName != null)
                {
                    var res = new YSValueModel
                    {
                        Grade = data.Grade,
                        YS = (int)YsData.YS
                    };
                    var ysrecord = new YsDataRecords()
                    {
                        Grade = YsData.GradeName,
                        PlateId = plateid,
                        YSValue = res.YS.ToString(),
                    };

                    _context.Add(ysrecord);
                    _context.SaveChanges();

                    return res;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }


        public async Task<string> GetGrade(string plateNo)
        {
            var plateid = Convert.ToInt32(plateNo);
            var grade = await _context.PlateDataFromSapColdLeveller.Where(x => x.Id == plateid).Select(x => x.Grade).FirstOrDefaultAsync();
            return grade;
        }

        public async Task<APIResponse> UpdateYsValue(YSUpdateModel model)
        {
            var plateNo = Convert.ToInt32(model.PlateNo);
            //var grade = await _context.PlateDataFromSapColdLeveller.Where(x => x.Id == plateNo).Select(x => x.Grade).FirstOrDefaultAsync();
            var datainystable = _coldLevellerContext.TableMassGrade.Where(x => x.GradeName == model.Grade).FirstOrDefault();
            if (datainystable == null)
            {
                var data = new TableMassGrade()
                {
                    GradeName = model.Grade,
                    MaximumThickNess = Convert.ToInt32(model.MaximumThickness),
                    MinimumThickNess = Convert.ToInt32(model.MininumThickness),
                    YS = Convert.ToInt32(model.YSValue)
                };
                await _coldLevellerContext.AddAsync(data);
                await _coldLevellerContext.SaveChangesAsync();
                return new APIResponse("Ys Value added in TableMassGrade Table", 200);
            }
            else if(string.IsNullOrEmpty(model.MaximumThickness) && string.IsNullOrEmpty(model.MininumThickness))
            {
                var data = _coldLevellerContext.TableMassGrade.Where(x => x.GradeName == model.Grade).FirstOrDefault();
                if(data != null)
                {
                    data.YS = Convert.ToInt32(model.YSValue);
                    _coldLevellerContext.Update(data);
                    await _coldLevellerContext.SaveChangesAsync();
                    return new APIResponse("Ys Value Updated in TableMassGrade Table", 200);
                }
                else
                {
                    return new APIResponse("no data found in TableMassGrade to update", 400);
                }
            }
            else
            {
                return new APIResponse("data found for this grade in table mass grade table Please update the value if intended", 400);
            }
        }



        public List<item> GetItemList()
        {
            List<item> LstItem = new();

            for (int i = 0; i < 20; i++)
            {
                var item = new item
                {
                    CHARG = "PA31980B" + (i + 1),
                    HEAT_NO = "A05550",
                    //LENGTH = "2",
                    WIDTH = "2.27387",
                    THICK = "2.98563",
                    //WEIGHT = "40",
                    P_ORDER = 0,
                    PO_NUMBER = "0",
                    //ARKTX = "",
                    CUST_NAME = "Test",
                    //PROJ_NAME = "",
                    GRADE = "dyytd",
                    ACT_WEIGHT = 0,
                    GRADE_D = "mgcjc",
                    //CUSTOMER_REF = ""
                };

                LstItem.Add(item);
            }

            return LstItem;
        }

        protected List<ColdLevellerItem> GetCLItems()
        {
            List<ColdLevellerItem> listitems = new List<ColdLevellerItem>();
            for (int i = 0; i < 20; i++)
            {
                var item = new ColdLevellerItem
                {
                    CHARG = "PA31980B" + (i + 1),
                    YS_T = i == 0 ? "1" : i.ToString(),
                    GRADE = "123456789" + i.ToString(),
                    LNTH = "50",
                    WDTH = "50",
                    RETURN1 = "",
                    THK = "10" + (i + 1),
                    WEIGHT = "25"
                };
                listitems.Add(item);
            }
            return listitems;
        }



    }
}
