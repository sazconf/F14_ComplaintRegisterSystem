using System;
using System.Data.SqlClient;
using System.IO;

namespace RestoreDatabaseTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting database restore...");

                // Get installation folder
                string applicationFolder =
                    AppDomain.CurrentDomain.BaseDirectory;

                string installFolder =
                    Directory.GetParent(applicationFolder).FullName;

                // Backup file location
                string backupFile =
                    Path.Combine(
                        installFolder,
                        "Database",
                        "ComplaintDB_Full.bak");

                if (!File.Exists(backupFile))
                {
                    Console.WriteLine("Backup file not found:");
                    Console.WriteLine(backupFile);
                    Console.ReadKey();
                    return;
                }

                // Data folder
                string dataFolder =
                    @"C:\Users\Public\Documents\ComplaintRegisterSystem";

                Directory.CreateDirectory(dataFolder);

                string mdfFile =
                    Path.Combine(dataFolder, "ComplaintDB.mdf");

                string ldfFile =
                    Path.Combine(dataFolder, "ComplaintDB_log.ldf");

                string connectionString =
                    @"Data Source=(localdb)\MSSQLLocalDB2022;
                  Initial Catalog=master;
                  Integrated Security=True;";

                string sql = $@"

IF DB_ID('ComplaintDB') IS NOT NULL
BEGIN
ALTER DATABASE ComplaintDB
SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

DROP DATABASE ComplaintDB;

END;

RESTORE DATABASE ComplaintDB
FROM DISK = N'{backupFile}'
WITH
MOVE N'ComplaintDB'
TO N'{mdfFile}',

MOVE N'ComplaintDB_log'
TO N'{ldfFile}',

REPLACE,
RECOVERY;
";

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Database restored successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("RESTORE FAILED:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

}