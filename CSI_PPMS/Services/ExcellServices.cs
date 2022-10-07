using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSI_PPMS.Services
{
    public class ExcellServices : IExcellServices
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IReportServices _reportServices;

        public ExcellServices(IWebHostEnvironment environment,
                              IReportServices reportServices)
        {
            _environment = environment;
            _reportServices = reportServices;
        }

        public async Task<byte[]> GenerateMarkingReport(FilterModel model)
        {
            Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath, "Export"));
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string excelFileName = Path.Combine(_environment.ContentRootPath, "Export") + "\\MarkingReports" + dateTime + ".xlsx";

            var data = await _reportServices.PlateMarkingReportData(model);
            var counter = 1;
            foreach (var x in data.ReportData)
            {
                x.SLNO = counter;
                counter++;
            }

            using (SpreadsheetDocument package = SpreadsheetDocument.Create(excelFileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = package.AddWorkbookPart();
                GenerateWorkbookPartContent(workbookPart, "MarkingReports");

                WorksheetPart worksheetParts = workbookPart.AddNewPart<WorksheetPart>("rId1");
                SheetData partSheetData = GenerateCellsForMarkingReport(data.ReportData, "MarkingReports");
                GenerateWorksheetPartContent(worksheetParts, partSheetData);
                package.Close();
            }

            var excelFile = File.ReadAllBytes(excelFileName);
            try
            {
                System.IO.File.Delete(excelFileName);
            }
            catch
            {

            }
            return excelFile;
        }


        public async Task<byte[]> GeneratePunchingingReport(FilterModel model)
        {
            Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath, "Export"));
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string excelFileName = Path.Combine(_environment.ContentRootPath, "Export") + "\\MarkingReports" + dateTime + ".xlsx";

            var data = await _reportServices.PlatePunchingReportData(model);
            var counter = 1;
            foreach (var x in data.ReportData)
            {
                x.SLNO = counter;
                counter++;
            }

            using (SpreadsheetDocument package = SpreadsheetDocument.Create(excelFileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = package.AddWorkbookPart();
                GenerateWorkbookPartContent(workbookPart, "MarkingReports");

                WorksheetPart worksheetParts = workbookPart.AddNewPart<WorksheetPart>("rId1");
                SheetData partSheetData = GenerateCellsForPunchingReport(data.ReportData, "MarkingReports");
                GenerateWorksheetPartContent(worksheetParts, partSheetData);
                package.Close();
            }

            var excelFile = File.ReadAllBytes(excelFileName);
            try
            {
                System.IO.File.Delete(excelFileName);
            }
            catch
            {

            }
            return excelFile;
        }



        public async Task<byte[]> GenerateColdLevellerReport(FilterModel model)
        {
            Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath, "Export"));
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string excelFileName = Path.Combine(_environment.ContentRootPath, "Export") + "\\MarkingReports" + dateTime + ".xlsx";

            var data = await _reportServices.ColdLevellerReportData(model);
            var counter = 1;
            foreach (var x in data.ReportData)
            {
                x.SLNO = counter;
                counter++;
            }

            using (SpreadsheetDocument package = SpreadsheetDocument.Create(excelFileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = package.AddWorkbookPart();
                GenerateWorkbookPartContent(workbookPart, "ColdLevellerReports");

                WorksheetPart worksheetParts = workbookPart.AddNewPart<WorksheetPart>("rId1");
                SheetData partSheetData = GenerateCellsForColdLevellerReport(data.ReportData, "ColdLevellerReports");
                GenerateWorksheetPartContent(worksheetParts, partSheetData);
                package.Close();
            }

            var excelFile = File.ReadAllBytes(excelFileName);
            try
            {
                System.IO.File.Delete(excelFileName);
            }
            catch
            {

            }
            return excelFile;
        }



        public async Task<byte[]> GenerateDowncoilerReport(FilterModel model)
        {
            Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath, "Export"));
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string excelFileName = Path.Combine(_environment.ContentRootPath, "Export") + "\\MarkingReports" + dateTime + ".xlsx";

            var data = await _reportServices.DownCoilerReportData(model);
            var counter = 1;
            foreach (var x in data.ReportData)
            {
                x.SLNO = counter;
                counter++;
            }

            using (SpreadsheetDocument package = SpreadsheetDocument.Create(excelFileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = package.AddWorkbookPart();
                GenerateWorkbookPartContent(workbookPart, "DowncoilerReport");

                WorksheetPart worksheetParts = workbookPart.AddNewPart<WorksheetPart>("rId1");
                SheetData partSheetData = GenerateCellsForDowncoilerReport(data.ReportData, "DowncoilerReport");
                GenerateWorksheetPartContent(worksheetParts, partSheetData);
                package.Close();
            }

            var excelFile = File.ReadAllBytes(excelFileName);
            try
            {
                System.IO.File.Delete(excelFileName);
            }
            catch
            {

            }
            return excelFile;
        }


        public async Task<byte[]> GenerateAuditReport(FilterModel model)
        {
            Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath, "Export"));
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string excelFileName = Path.Combine(_environment.ContentRootPath, "Export") + "\\AuditReports" + dateTime + ".xlsx";

            var data = await _reportServices.AuditReportData(model);
            var counter = 1;
            foreach (var x in data.ReportData)
            {
                x.SLNO = counter;
                counter++;
            }

            using (SpreadsheetDocument package = SpreadsheetDocument.Create(excelFileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = package.AddWorkbookPart();
                GenerateWorkbookPartContent(workbookPart, "AuditReport");

                WorksheetPart worksheetParts = workbookPart.AddNewPart<WorksheetPart>("rId1");
                SheetData partSheetData = GenerateCellsForAuditReport(data.ReportData, "AuditReport");
                GenerateWorksheetPartContent(worksheetParts, partSheetData);
                package.Close();
            }

            var excelFile = File.ReadAllBytes(excelFileName);
            try
            {
                System.IO.File.Delete(excelFileName);
            }
            catch
            {

            }
            return excelFile;
        }


        public async Task<byte[]> GenerateWeightDataUpdate(FilterModel model)
        {
            Directory.CreateDirectory(Path.Combine(_environment.ContentRootPath, "Export"));
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string excelFileName = Path.Combine(_environment.ContentRootPath, "Export") + "\\ManualWeightUpdateReports" + dateTime + ".xlsx";

            var data = await _reportServices.WeightUpdateReportData(model);
            var counter = 1;
            foreach (var x in data.ReportData)
            {
                x.SlNo = counter;
                counter++;
            }

            using (SpreadsheetDocument package = SpreadsheetDocument.Create(excelFileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = package.AddWorkbookPart();
                GenerateWorkbookPartContent(workbookPart, "ManualWeightUpdateReport");

                WorksheetPart worksheetParts = workbookPart.AddNewPart<WorksheetPart>("rId1");
                SheetData partSheetData = GenerateCellsForManualWeightUpdateReport(data.ReportData, "ManualWeightUpdateReport");
                GenerateWorksheetPartContent(worksheetParts, partSheetData);
                package.Close();
            }

            var excelFile = File.ReadAllBytes(excelFileName);
            try
            {
                System.IO.File.Delete(excelFileName);
            }
            catch
            {

            }
            return excelFile;
        }




        private void GenerateWorkbookPartContent(WorkbookPart _workbookPart, string sheetName)
        {
            Workbook workbook = new();
            Sheets sheets1 = new();
            Sheet sheet1 = new()
            {
                Name = sheetName,
                SheetId = (UInt32Value)1U,
                Id = "rId1"
            };
            sheets1.Append(sheet1);
            workbook.Append(sheets1);
            _workbookPart.Workbook = workbook;
        }

        private Row CreateHeaderRowForMarkingReport(string Type)
        {
            Row workRow = new Row();
            switch (Type)
            {
                case "MarkingReports":
                    workRow.Append(CreateCell("Sl No", 2U));
                    workRow.Append(CreateCell("Marking Time", 2U));
                    workRow.Append(CreateCell("Plate Number", 2U));
                    workRow.Append(CreateCell("Heat Number", 2U));
                    workRow.Append(CreateCell("Size", 2U));
                    workRow.Append(CreateCell("Weight", 2U));
                    workRow.Append(CreateCell("PurchaseOrder", 2U));
                    workRow.Append(CreateCell("PurchaseOrderNumber", 2U));
                    workRow.Append(CreateCell("MaterialDescription", 2U));
                    workRow.Append(CreateCell("CustomerName", 2U));
                    workRow.Append(CreateCell("CustomerReference", 2U));
                    workRow.Append(CreateCell("Grade", 2U));
                    workRow.Append(CreateCell("GradeDuel", 2U));
                    workRow.Append(CreateCell("Line1", 2U));
                    workRow.Append(CreateCell("Line2", 2U));
                    workRow.Append(CreateCell("Line3", 2U));
                    workRow.Append(CreateCell("Line4", 2U));
                    workRow.Append(CreateCell("Line5", 2U));
                    workRow.Append(CreateCell("Line6", 2U));
                    break;
            }

            return workRow;
        }


        private Row CreateHeaderRowForColdLevellerReport(string Type)
        {
            Row workRow = new Row();
            switch (Type)
            {
                case "MarkingReports":
                    workRow.Append(CreateCell("Sl No", 2U));
                    workRow.Append(CreateCell("Marking Time", 2U));
                    workRow.Append(CreateCell("Plate Number", 2U));
                    workRow.Append(CreateCell("Grade", 2U));
                    workRow.Append(CreateCell("Length", 2U));
                    workRow.Append(CreateCell("Thickness", 2U));
                    workRow.Append(CreateCell("Width", 2U));
                    workRow.Append(CreateCell("Weight", 2U));
                    workRow.Append(CreateCell("YS_T", 2U));
                    workRow.Append(CreateCell("Plate Number", 2U));
                    workRow.Append(CreateCell("YS_T from DB", 2U));
                    workRow.Append(CreateCell("Data Load Date", 2U));
                    workRow.Append(CreateCell("Plate Number", 2U));
                    workRow.Append(CreateCell("Steel Grade", 2U));
                    workRow.Append(CreateCell("Length", 2U));
                    workRow.Append(CreateCell("Thickness", 2U));
                    workRow.Append(CreateCell("Width", 2U));
                    workRow.Append(CreateCell("Weight", 2U));
                    break;
            }

            return workRow;
        }


        private Row CreateHeaderRowForDowncoilerReport(string Type)
        {
            Row workRow = new Row();
            switch (Type)
            {
                case "DowncoilerReport":
                    workRow.Append(CreateCell("Sl No", 2U));
                    workRow.Append(CreateCell("Sap Fetch Time", 2U));
                    workRow.Append(CreateCell("MAT ID", 2U));
                    workRow.Append(CreateCell("Coil Id", 2U));
                    workRow.Append(CreateCell("Heat No", 2U));
                    workRow.Append(CreateCell("Grade", 2U));
                    workRow.Append(CreateCell("Width", 2U));
                    workRow.Append(CreateCell("Thickness", 2U));
                    workRow.Append(CreateCell("Customer Name", 2U));
                    workRow.Append(CreateCell("Purchase order", 2U));
                    workRow.Append(CreateCell("Purchase Order Number", 2U));
                    workRow.Append(CreateCell("Actual Weight", 2U));
                    workRow.Append(CreateCell("Record Id", 2U));
                    workRow.Append(CreateCell("Data Update Time", 2U));
                    workRow.Append(CreateCell("Disc Line 1", 2U));
                    workRow.Append(CreateCell("Disc Line 2", 2U));
                    workRow.Append(CreateCell("Shell Line 1", 2U));
                    workRow.Append(CreateCell("Shell Line 2", 2U));
                    workRow.Append(CreateCell("Shell Line 3", 2U));
                    workRow.Append(CreateCell("Shell Line 4", 2U));
                    workRow.Append(CreateCell("Logo Status", 2U));
                    workRow.Append(CreateCell("Coil Width", 2U));
                    workRow.Append(CreateCell("Coil Diameter", 2U));
                    break;
            }

            return workRow;
        }


        private Row CreateHeaderRowForAuditReport(string Type)
        {
            Row workRow = new Row();
            switch (Type)
            {
                case "AuditReport":
                    workRow.Append(CreateCell("Sl No", 2U));
                    workRow.Append(CreateCell("User Name", 2U));
                    workRow.Append(CreateCell("Log", 2U));
                    workRow.Append(CreateCell("Log Type", 2U));
                    workRow.Append(CreateCell("Log Time", 2U));
                    break;
            }

            return workRow;
        }

        private Row CreateHeaderRowForManualWeightUpdateReport(string Type)
        {
            Row workRow = new Row();
            switch (Type)
            {
                case "ManualWeightUpdateReport":
                    workRow.Append(CreateCell("Sl No", 2U));
                    workRow.Append(CreateCell("Coil Id", 2U));
                    workRow.Append(CreateCell("Weight", 2U));
                    workRow.Append(CreateCell("Created Date", 2U));
                    break;
            }

            return workRow;
        }




        private Row CreateHeaderRowForPunchingReport(string Type)
        {
            Row workRow = new Row();
            switch (Type)
            {
                case "MarkingReports":
                    workRow.Append(CreateCell("Sl No", 2U));
                    workRow.Append(CreateCell("Punching Time", 2U));
                    workRow.Append(CreateCell("Plate Number", 2U));
                    workRow.Append(CreateCell("Heat Number", 2U));
                    workRow.Append(CreateCell("Size", 2U));
                    workRow.Append(CreateCell("Weight", 2U));
                    workRow.Append(CreateCell("PurchaseOrder", 2U));
                    workRow.Append(CreateCell("PurchaseOrderNumber", 2U));
                    workRow.Append(CreateCell("MaterialDescription", 2U));
                    workRow.Append(CreateCell("CustomerName", 2U));
                    workRow.Append(CreateCell("CustomerReference", 2U));
                    workRow.Append(CreateCell("Grade", 2U));
                    workRow.Append(CreateCell("GradeDuel", 2U));
                    workRow.Append(CreateCell("Line1", 2U));
                    workRow.Append(CreateCell("Line2", 2U));
                    workRow.Append(CreateCell("Line3", 2U));
                    workRow.Append(CreateCell("Line4", 2U));
                    workRow.Append(CreateCell("Line5", 2U));
                    workRow.Append(CreateCell("Line6", 2U));
                    break;
            }

            return workRow;
        }

        private SheetData GenerateCellsForMarkingReport(List<MarkingReportModel> data, string Type)
        {
            SheetData sheetData = new SheetData();
            sheetData.Append(CreateHeaderRowForMarkingReport(Type));
            foreach (var fetchData in data)
            {
                Row tRow = new Row();

                tRow.Append(CreateCell(fetchData.SLNO.ToString()));
                tRow.Append(CreateCell(fetchData.MarkingTime));
                tRow.Append(CreateCell(fetchData.PlateNumber));
                tRow.Append(CreateCell(fetchData.HeatNumber));
                tRow.Append(CreateCell(fetchData.Size));
                tRow.Append(CreateCell(fetchData.Weight));
                tRow.Append(CreateCell(fetchData.PurchaseOrder.ToString()));
                tRow.Append(CreateCell(fetchData.PurchaseOrderNumber));
                tRow.Append(CreateCell(fetchData.MaterialDescription));
                tRow.Append(CreateCell(fetchData.CustomerName));
                tRow.Append(CreateCell(fetchData.CustomerReference));
                tRow.Append(CreateCell(fetchData.Grade));
                tRow.Append(CreateCell(fetchData.GradeDuel));
                tRow.Append(CreateCell(fetchData.Line1));
                tRow.Append(CreateCell(fetchData.Line2));
                tRow.Append(CreateCell(fetchData.Line3));
                tRow.Append(CreateCell(fetchData.Line4));
                tRow.Append(CreateCell(fetchData.Line5));
                tRow.Append(CreateCell(fetchData.Line6));
                sheetData.Append(tRow);
            }

            return sheetData;
        }

        private SheetData GenerateCellsForColdLevellerReport(List<ColdLevellerReport> data, string Type)
        {
            SheetData sheetData = new SheetData();
            sheetData.Append(CreateHeaderRowForColdLevellerReport(Type));
            foreach (var fetchData in data)
            {
                Row tRow = new Row();

                tRow.Append(CreateCell(fetchData.SLNO.ToString()));
                tRow.Append(CreateCell(fetchData.SAPFetchTime));
                tRow.Append(CreateCell(fetchData.PlateNo));
                tRow.Append(CreateCell(fetchData.Grade));
                tRow.Append(CreateCell(fetchData.Length));
                tRow.Append(CreateCell(fetchData.Thick));
                tRow.Append(CreateCell(fetchData.Width));
                tRow.Append(CreateCell(fetchData.Weight));
                tRow.Append(CreateCell(fetchData.YST));
                tRow.Append(CreateCell(fetchData.PlateNo1));
                tRow.Append(CreateCell(fetchData.DBYST));
                tRow.Append(CreateCell(fetchData.LDDate));
                tRow.Append(CreateCell(fetchData.LDPlateNo));
                tRow.Append(CreateCell(fetchData.LDSteelGrade));
                tRow.Append(CreateCell(fetchData.LDLength));
                tRow.Append(CreateCell(fetchData.LDThick));
                tRow.Append(CreateCell(fetchData.LDWidth));
                tRow.Append(CreateCell(fetchData.LDWeight));
                sheetData.Append(tRow);
            }

            return sheetData;
        }

        private SheetData GenerateCellsForDowncoilerReport(List<DownCoilerReport> data, string Type)
        {
            SheetData sheetData = new SheetData();
            sheetData.Append(CreateHeaderRowForDowncoilerReport(Type));
            foreach (var fetchData in data)
            {
                Row tRow = new Row();

                tRow.Append(CreateCell(fetchData.SLNO.ToString()));
                tRow.Append(CreateCell(fetchData.TimeStamp));
                tRow.Append(CreateCell(fetchData.MatId));
                tRow.Append(CreateCell(fetchData.CoilId));
                tRow.Append(CreateCell(fetchData.HeatNo));
                tRow.Append(CreateCell(fetchData.Grade));
                tRow.Append(CreateCell(fetchData.Width));
                tRow.Append(CreateCell(fetchData.Thickness));
                tRow.Append(CreateCell(fetchData.Cust_name));
                tRow.Append(CreateCell(fetchData.p_order));
                tRow.Append(CreateCell(fetchData.P_Number));
                tRow.Append(CreateCell(fetchData.AOT_Weight));
                tRow.Append(CreateCell(fetchData.RecordId));
                tRow.Append(CreateCell(fetchData.DataLoadDate));
                tRow.Append(CreateCell(fetchData.DiscLine1));
                tRow.Append(CreateCell(fetchData.DiscLine2));
                tRow.Append(CreateCell(fetchData.ShellLine1));
                tRow.Append(CreateCell(fetchData.ShellLine2));
                tRow.Append(CreateCell(fetchData.ShellLine3));
                tRow.Append(CreateCell(fetchData.ShellLine4));
                tRow.Append(CreateCell(fetchData.LogoStatus));
                tRow.Append(CreateCell(fetchData.CoilWidth));
                tRow.Append(CreateCell(fetchData.CoilDiameter));
                sheetData.Append(tRow);
            }

            return sheetData;
        }



        private SheetData GenerateCellsForAuditReport(List<ReportData> data, string Type)
        {
            SheetData sheetData = new SheetData();
            sheetData.Append(CreateHeaderRowForAuditReport(Type));
            foreach (var fetchData in data)
            {
                Row tRow = new Row();

                tRow.Append(CreateCell(fetchData.SLNO.ToString()));
                tRow.Append(CreateCell(fetchData.User));
                tRow.Append(CreateCell(fetchData.Log));
                tRow.Append(CreateCell(fetchData.LogType));
                tRow.Append(CreateCell(fetchData.LogTime));
                sheetData.Append(tRow);
            }

            return sheetData;
        }


        private SheetData GenerateCellsForManualWeightUpdateReport(List<WeightUpadteReport> data, string Type)
        {
            SheetData sheetData = new SheetData();
            sheetData.Append(CreateHeaderRowForManualWeightUpdateReport(Type));
            foreach (var fetchData in data)
            {
                Row tRow = new Row();

                tRow.Append(CreateCell(fetchData.SlNo.ToString()));
                tRow.Append(CreateCell(fetchData.CoilId));
                tRow.Append(CreateCell(fetchData.Weight));
                tRow.Append(CreateCell(fetchData.CreatedDate));
                sheetData.Append(tRow);
            }

            return sheetData;
        }




        private SheetData GenerateCellsForPunchingReport(List<PunchingReportModel> data, string Type)
        {
            SheetData sheetData = new SheetData();
            sheetData.Append(CreateHeaderRowForPunchingReport(Type));
            foreach (var fetchData in data)
            {
                Row tRow = new Row();

                tRow.Append(CreateCell(fetchData.SLNO.ToString()));
                tRow.Append(CreateCell(fetchData.PunchingTime));
                tRow.Append(CreateCell(fetchData.PlateNumber));
                tRow.Append(CreateCell(fetchData.HeatNumber));
                tRow.Append(CreateCell(fetchData.Size));
                tRow.Append(CreateCell(fetchData.Weight));
                tRow.Append(CreateCell(fetchData.PurchaseOrder.ToString()));
                tRow.Append(CreateCell(fetchData.PurchaseOrderNumber));
                tRow.Append(CreateCell(fetchData.MaterialDescription));
                tRow.Append(CreateCell(fetchData.CustomerName));
                tRow.Append(CreateCell(fetchData.CustomerReference));
                tRow.Append(CreateCell(fetchData.Grade));
                tRow.Append(CreateCell(fetchData.GradeDuel));
                tRow.Append(CreateCell(fetchData.Line1));
                tRow.Append(CreateCell(fetchData.Line2));
                tRow.Append(CreateCell(fetchData.Line3));
                tRow.Append(CreateCell(fetchData.Line4));
                tRow.Append(CreateCell(fetchData.Line5));
                tRow.Append(CreateCell(fetchData.Line6));
                sheetData.Append(tRow);
            }

            return sheetData;
        }

        private Cell CreateCell(string text)
        {
            Cell cell = new()
            {
                StyleIndex = (UInt32Value)1U
            };

            var DataType = ResolveCellDataTypeOnValue(text);
            //cell.DataType = DataType;

            if (DataType == DocumentFormat.OpenXml.Spreadsheet.CellValues.String)
            {
                cell.DataType = CellValues.InlineString;
                cell.InlineString = new InlineString() { Text = new Text(text) };
            }
            else
            {
                cell.DataType = CellValues.Number;
                cell.CellValue = new CellValue(text);
            }
            return cell;
        }

        private Cell CreateCell(string text, uint styleIndex)
        {
            Cell cell = new Cell();
            cell.StyleIndex = styleIndex;
            var DataType = ResolveCellDataTypeOnValue(text);
            //cell.DataType = DataType;
            if (DataType == DocumentFormat.OpenXml.Spreadsheet.CellValues.String)
            {
                cell.DataType = CellValues.InlineString;
                cell.InlineString = new InlineString() { Text = new Text(text) };
            }
            else
            {
                cell.DataType = CellValues.Number;
                cell.CellValue = new CellValue(text);
            }
            return cell;
        }

        private EnumValue<CellValues> ResolveCellDataTypeOnValue(string text)
        {
            int intVal;
            double doubleVal;
            if (int.TryParse(text, out intVal) || double.TryParse(text, out doubleVal))
            {
                return CellValues.Number;
            }
            else
            {
                return CellValues.String;
            }
        }


        private void GenerateWorksheetPartContent(WorksheetPart _worksheetPart, SheetData _sheetData)
        {
            Worksheet worksheet = new Worksheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            worksheet.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            worksheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            worksheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");
            SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1" };

            SheetViews sheetViews = new SheetViews();

            SheetView sheetViewSelected = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection = new Selection() { ActiveCell = "A1", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A1" } };

            sheetViewSelected.Append(selection);

            sheetViews.Append(sheetViewSelected);
            SheetFormatProperties sheetFormatProperties = new SheetFormatProperties() { DefaultRowHeight = 15D, DyDescent = 0.25D };

            PageMargins pageMargins = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
            worksheet.Append(sheetDimension1);
            worksheet.Append(sheetViews);
            worksheet.Append(sheetFormatProperties);
            worksheet.Append(_sheetData);
            worksheet.Append(pageMargins);
            _worksheetPart.Worksheet = worksheet;
        }
    }
}
