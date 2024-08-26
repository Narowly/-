using ClosedXML.Excel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class ExcelHelper
    {
        private ExcelHelper() { }
        public static void AddMultiRowHeader(IXLWorksheet worksheet, List<List<string>> headers, int startRow = 1, int startColumn = 1)
        {
            int rowIndex = startRow; 
            foreach (var headerRow in headers)
            {
                int columnIndex = startColumn;  
                foreach (var header in headerRow)
                {
                    worksheet.Cell(rowIndex, columnIndex).Value = header;
                    columnIndex++;
                }
                rowIndex++;
            }
        }
    }
}
