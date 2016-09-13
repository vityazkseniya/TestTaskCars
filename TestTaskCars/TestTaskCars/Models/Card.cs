using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace TestTask.Models
{
    public class Card
    {
        public int CardId { get; set; }
        public String First_Name {get; set;}
        public String Last_Name {get; set;}
        public DateTime Date_Of_Birth {get; set;}
        public String Address {get; set;}
        public String Phone {get; set;}
        public String Email {get; set;}
        public static int NextId { get; set; }

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
                cmd.CommandText = "Select * From CardTable";

                // Вывод данных с помощью объекта чтения данных
                using (DbDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        int tmp = Convert.ToInt32(dr["CardId"]);
                        
                        if (Card.NextId <= tmp)
                            Card.NextId = tmp + 1;
                    }
                }
            }
        }
    }
}