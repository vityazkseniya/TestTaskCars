using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace TestTask.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int CardParentId { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public int Year { get; set; }
        public String Vin { get; set; }
        public static int NextId { get; set;}

        public void SetNextId()
        {
            DbProviderFactory df =
     DbProviderFactories.GetFactory("System.Data.SqlClient");

            using (DbConnection cn = df.CreateConnection())
            {
                cn.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\CardTables.mdf;Integrated Security=True;User Instance=True";
                cn.Open();

                // Создание объекта команды
                DbCommand cmd = df.CreateCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From CarTable";

                // Вывод данных с помощью объекта чтения данных
                using (DbDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        int tmp = Convert.ToInt32(dr["CarId"]);

                        if (Card.NextId <= tmp)
                            Card.NextId = tmp + 1;
                    }
                }
            }
        }
    }
}