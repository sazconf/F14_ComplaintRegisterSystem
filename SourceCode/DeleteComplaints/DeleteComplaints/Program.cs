using System;
using System.Data.SqlClient;

namespace DeleteComplaintsTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Delete Complaints Tool";

            Console.WriteLine("================================");
            Console.WriteLine(" DELETE ALL COMPLAINT RECORDS");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.Write("Type YES to continue: ");

            string confirm = Console.ReadLine();

            if (!confirm.Equals(
                "YES",
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine();
                Console.WriteLine("Operation cancelled.");
                Console.ReadKey();
                return;
            }

            try
            {
                string connStr =
                    @"Server=(localdb)\MSSQLLocalDB2022;" +
                    @"Database=ComplaintDB;" +
                    @"Integrated Security=True;";

                using (SqlConnection conn =
                    new SqlConnection(connStr))
                {
                    conn.Open();

                    string sql =
                        @"DELETE FROM complaints;
                          DBCC CHECKIDENT ('complaints', RESEED, 0);";

                    SqlCommand cmd =
                        new SqlCommand(sql, conn);

                    int rows =
                        cmd.ExecuteNonQuery();

                    Console.WriteLine();
                    Console.WriteLine(
                        "Complaints deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}