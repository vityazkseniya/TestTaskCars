using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace TestTask.Models
{
    public class CardList
    {
        public List<Card> Cards = new List<Card>();

        public void GetCards()
        {
            Cards.Clear();

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
                        Card tmp = new Card();
                        tmp.CardId = Convert.ToInt32(dr["CardId"]);
                        tmp.First_Name = dr["First_Name"].ToString();
                        tmp.Last_Name = dr["Last_Name"].ToString();
                        tmp.Date_Of_Birth = Convert.ToDateTime(dr["Date_Of_Birth"]);
                        tmp.Address = dr["Address"].ToString();
                        tmp.Phone = dr["Phone"].ToString();
                        tmp.Email = dr["Email"].ToString();

                        Cards.Add(tmp);

                        if (Card.NextId <= tmp.CardId)
                            Card.NextId = tmp.CardId + 1;
                    }
                }
            }
        }
    }
}