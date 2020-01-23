using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace CapInnovativeIdia.BusinessRepository.CommanFunctions
{
    public class CommanFunction
    {
        public static void  WriteDateIntoExcel(string filePath,DataSet dataSet)
        {
            ExcelPackage excelPackage = new ExcelPackage();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(dataTable.TableName);

                for (int i = 1; i <= dataTable.Columns.Count; i++)
                {
                    excelWorksheet.SetValue(1, i, dataTable.Columns[i-1]);
                }

                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    for (int k = 0; k < dataTable.Columns.Count; k++)
                    {
                        excelWorksheet.SetValue(j + 2, k + 1, dataTable.Rows[j].ItemArray[k].ToString());
                    }
                }
                excelWorksheet.Protection.IsProtected = false;
                excelWorksheet.Protection.AllowSelectLockedCells = false;
            }
            excelPackage.SaveAs(new FileInfo(filePath));            
        }
    }
}
