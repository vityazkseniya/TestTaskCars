using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace TestTask.Models
{
    public class OrderList
    {
        public List<Order> Orders = new List<Order>();

        public void GetCards(int id)
        {
            Orders.Clear();

            DbProviderFactory df =
      DbProviderFactories.GetFactory("System.Data.SqlClient");

            using (DbConnection cn = df.CreateConnection())
            {
                cn.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\CardTables.mdf;Integrated Security=True;User Instance=True";
                cn.Open();

                // Создание объекта команды
                DbCommand cmd = df.CreateCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From OrderTable";

                // Вывод данных с помощью объекта чтения данных
                using (DbDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        if (Convert.ToInt32(dr["CarParentId"]) == id)
                        {
                            Order tmp = new Order();
                            tmp.OrderId = Convert.ToInt32(dr["OrderId"]);
                            tmp.CarParentId = id;
                            tmp.Date = Convert.ToDateTime(dr["Date"]);
                            tmp.Order_Amount = Convert.ToDouble(dr["Order_Amount"]);
                            tmp.Order_Status = dr["Order_Status"].ToString();

                            Orders.Add(tmp);

                            if (Order.NextId <= tmp.OrderId)
                                Order.NextId = tmp.OrderId + 1;
                        }
                    }
                }
            }
        }
    }
}