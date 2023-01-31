using Aspose.Cells.Tables;
using Core.Domain.DI;
using Core.Domain.Postgre.Base;
using Core.Domain.Repos;
using Core.Domain.Shared.Cruds;
using Core.Domain.Shared.CustomProperty;
using Core.Domain.Shared.Enums;
using Core.Export.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Core.Export.Services
{
    public class ExportService : IExportService
    {
        private IBaseRepo _baseRepo;
        public ExportService(IBaseRepo baseRepo)
        {
            _baseRepo= baseRepo;
        }
        public Task ExportExcel(Guid id,Type type)
        {
            try
            {
                IDbConnection cnn = _baseRepo.GetOpenConnection(DatabaseSide.ReadSide);
                //string table = _baseRepo.GetTableName(type, "");
                
                // Lấy dư liệu
                Task<ExportResult> entities = _baseRepo.GetAsync(type);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var stream = new MemoryStream();

                using (ExcelPackage excel = new ExcelPackage(stream))
                {

                    var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                    workSheet.TabColor = System.Drawing.Color.Black;
                    workSheet.DefaultRowHeight = 20;
                    // title danh sách nhân viên
                    workSheet.Cells["B1"].Value = "Danh Sách";
                    workSheet.Cells["B1"].Style.Font.Name = "B Zar";
                    workSheet.Cells["B1"].Style.Font.Size = 16;
                    workSheet.Cells["B1"].Style.Font.Bold = true;
                    workSheet.Cells["B1:D1"].Merge = true;
                    //Header of table  
                    workSheet.Row(2).Height = 20;
                    workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(2).Style.Font.Bold = true;
                    var properties = type.GetProperties();
                    int count = 1;
                    foreach (var property in properties)
                    {
                        var propName = property.GetCustomAttributes(typeof(PropertyName), true);
                        var exportExcel = property.GetCustomAttributes(typeof(ExportExcel), true);
                        var propertyDisplay = "";
                        // lấy tên PropertyName
                        if (propName.Length > 0)
                        {
                            propertyDisplay = (propName[0] as PropertyName)._name;
                        }
                        if (exportExcel.Length > 0)
                        {
                            workSheet.Cells[2, count].Value = propertyDisplay;
                            workSheet.Cells[2, count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[2, count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[2, count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[2, count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            count++;
                        }

                    }
                    int length = count;
                    //Body of table  
                    //  
                    int recordIndex = 3;
                    foreach (var entity in entities.Result.PageData)
                    {
                        count = 1;
                        foreach (var property in properties)
                        {
                            var propertyName = property.Name;
                            var exportExcel = property.GetCustomAttributes(typeof(ExportExcel), true);
                            var propertyDisplay = "";
                            if (exportExcel.Length > 0)
                            {
                                var fields = entity as IDictionary<string, object>;
                                workSheet.Cells[recordIndex, count].Value = fields[property.Name];
                                if (property.PropertyType == typeof(DateTime?))
                                {
                                    workSheet.Cells[recordIndex, count].Style.Numberformat.Format = "dd/mm/yyyy";
                                    // căn giữa
                                    workSheet.Cells[recordIndex, count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                }
                                workSheet.Cells[recordIndex, count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[recordIndex, count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[recordIndex, count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[recordIndex, count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                count++;
                            }


                        }
                        recordIndex++;
                    }
                    for (int i = 1; i < length; i++)
                    {
                        // format chiều ngang của cột
                        workSheet.Column(i).AutoFit();
                    }
                    String folder = System.Guid.NewGuid().ToString();
                    // Lưu file excel
                    //excel.SaveAs(new FileInfo($@"D:\{folder}.xlsx"));
                    excel.SaveAs(new FileInfo($@"D:\Coding\BE\CoreAPI\Core.Api\Core.Template\Temp\DanhSach{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx"));
                    stream.Position = 0;
                    return Task.CompletedTask;
                }

            }
            catch (HttpResponseException ex)
            {

                throw new HttpResponseException(statusCode: System.Net.HttpStatusCode.BadRequest);
            }
        }
       

    }

    
}
