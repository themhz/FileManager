using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace FileManager
{
    public class DatabaseExporter
    {
        public DatabaseExporter()
        {
            void ExportTable(string table)
            {
                string connectionString = "server=localhost;user=root;password=;database=mywebshop;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM table", connection))
                    {
                        adapter.Fill(table);
                    }
                    string filePath = "export.csv";
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        WriteDataTable(table, writer, true);
                    }
                }
            }

            static void ExportTables()
            {
                string connectionString = "--user=root --password= --host=localhost --port=3306 mywebshop";
                string filePath = "database.sql";
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "mysqldump";
                    process.StartInfo.Arguments = connectionString;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(process.StandardOutput.ReadToEnd());
                    }
                    process.WaitForExit();
                }
            }

            void WriteDataTable(DataTable table, TextWriter writer, bool includeHeaders)
            {
                if (includeHeaders)
                {
                    string[] headers = new string[table.Columns.Count];
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        headers[i] = table.Columns[i].ColumnName;
                    }
                    writer.WriteLine(string.Join(",", headers));
                }
                foreach (DataRow row in table.Rows)
                {
                    string[] fields = new string[table.Columns.Count];
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        fields[i] = row[i].ToString();
                    }
                    writer.WriteLine(string.Join(",", fields));
                }
            }
        }
    }
}
