using Microsoft.Data.SqlClient;
using System;

namespace tds.core
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.WriteLine("Hello World!");
        }

        static void Test()
        {
            using (var conn = new SqlConnection("Data Source=tcp:localhost,1433;Initial Catalog=Test;MultipleActiveResultSets=False;user=guest;pwd=sybase;Encrypt=false;trustservercertificate=false"))
            using (var com = new SqlCommand("Select * From Table", conn))
            {
                conn.Open();
                com.ExecuteNonQuery();
            }
        }
    }
}
