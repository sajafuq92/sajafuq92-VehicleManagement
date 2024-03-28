using OfficeOpenXml;
using System.Data;
using System.Reflection;

namespace VehicleManagement.Common.Helpers
{
    public static class ExcelReadHelper
    {
        public static List<T> ReadExcelFile<T>(string brandFilePath) where T : new()
        {
            FileInfo existingFile = new FileInfo(brandFilePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorkbook workbook = package.Workbook;
                if (workbook != null)
                {
                    ExcelWorksheet worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet != null)
                    {
                        return ReadExcelToList<T>(worksheet);
                    }
                }
            }

            return [];
        }

        private static List<M> ReadExcelToList<M>(ExcelWorksheet worksheet) where M : new()
        {
            List<M> collection = new();

            DataTable dt = new DataTable();
            foreach (var firstRowCell in new M().GetType().GetProperties().ToList())
            {
                //Add table colums with properties of M
                dt.Columns.Add(firstRowCell.Name);
            }
            for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
            {
                var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                DataRow row = dt.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
            }

            //Get the colums of table
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

            //Get the properties of M
            List<PropertyInfo> properties = new M().GetType().GetProperties().ToList();

            collection = dt.AsEnumerable().Select(row =>
            {
                M item = Activator.CreateInstance<M>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name) || columnNames.Contains(pro.Name.ToUpper()))
                    {
                        PropertyInfo pI = item.GetType().GetProperty(pro.Name);
                        pro.SetValue(item, (row[pro.Name] == DBNull.Value) ? null : Convert.ChangeType(row[pro.Name], (Nullable.GetUnderlyingType(pI.PropertyType) == null) ? pI.PropertyType : Type.GetType(pI.PropertyType.GenericTypeArguments[0].FullName)));
                    }
                }
                return item;
            }).ToList();

            return collection;
        }
    }
}
