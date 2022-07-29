using DutyManager.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DutyManager.Services
{
    public static class FileExport
    {
        public static void WriteToExcel(IEnumerable<MainTableModel> report, string path)
        {
            try
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    var worksheet = excel.Workbook.Worksheets.Add("Лист1");
                    worksheet.Cells["A1"].LoadFromCollection(report, true);
                    worksheet.DeleteColumn(1);
                    worksheet.DeleteColumn(7);

                    var headerCells = worksheet.Cells["A1:" + (char)('A' + report.First().GetType().GetProperties().Count() - 3) + "1"];
                    headerCells.AutoFilter = true;
                    headerCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                    var allCells = worksheet.Cells["A1:" + (char)('A' + report.First().GetType().GetProperties().Count() - 1) + (report.ToList().Count + 1).ToString()];
                    allCells.AutoFitColumns();
                    worksheet.Cells[$"B2:C{(report.ToList().Count + 1).ToString()}"].Style.Numberformat.Format = "dd/MM/yyyy hh:mm";
                    excel.SaveAs(new System.IO.FileInfo(path));
                }
            }
            catch (OutOfMemoryException)
            {
                throw new OutOfMemoryException("Недостаточно памяти для выполнения операции");
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}