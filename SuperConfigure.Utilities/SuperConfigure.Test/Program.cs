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
        static Query command = new Query();
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("starting ...");
                command.Connection = new System.Data.SqlClient.SqlConnection("Server=USER\\MSSQL2012;Database=shwecrm;User Id=sa;Password=sa;");
                if (command.ExecuteBoolen("SELECT * FROM [shweaccount]", CommandType.Text, null))
                {
                    Console.WriteLine("success");
                }
                else
                {
                    throw new Exception("error: something went wrong!");
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
