using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace TestTask.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CarParentId { get; set; }

        private static String[] arr = { "Completed", "In_Progress", "Cancelled" };

        public DateTime Date { get; set; }

        private double order_amount;
        public double Order_Amount { 
        get 
            {return order_amount;} 
        set
            {
                if(value>0 && value < 10000)
                    order_amount = value;
                else throw new ArgumentOutOfRangeException();
            } }

        private String order_status;
        public String Order_Status {
        get 
            {return order_status;} 
        set
            {
                if(arr.Contains(value))
                    order_status = value;
                else throw new ArgumentOutOfRangeException();
            } 
        }
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
                cmd.CommandText = "Select * From OrderTable";

                // Вывод данных с помощью объекта чтения данных
                using (DbDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        int tmp = Convert.ToInt32(dr["OrderId"]);

                        if (Card.NextId <= tmp)
                            Card.NextId = tmp + 1;
                    }
                }
            }
        }
    }
}