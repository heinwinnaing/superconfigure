using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperConfigure.SqlHelper;

namespace SuperConfigure.Test
{
    class Program
    {
        static Query command = new Query("Server=USER\\MSSQL2012;Database=shwecrm;User Id=sa;Password=sa;");
        static void Main(string[] args)
        {
            try
            {
                DataTable dataTable = command.ExecuteDataTable("SELECT * FROM [shweaccount]", CommandType.Text, null);
                if (dataTable != null)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Console.WriteLine(dr["username"].ToString());
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.Read();
            }
        }
    }
}
