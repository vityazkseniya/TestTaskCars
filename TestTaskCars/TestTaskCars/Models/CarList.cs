using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace TestTask.Models
{
    public class CarList
    {
        public List<Car> Cars = new List<Car>();

        public void GetCards(int id)
        {
            Cars.Clear();

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
                        if (Convert.ToInt32(dr["CardParentId"]) == id)
                        {
                            Car tmp = new Car();
                            tmp.CarId = Convert.ToInt32(dr["CarId"]);
                            tmp.CardParentId = id;
                            tmp.Make = dr["Make"].ToString();
                            tmp.Model = dr["Model"].ToString();
                            tmp.Year = Convert.ToInt32(dr["Year"]);
                            tmp.Vin = dr["Vin"].ToString();

                            Cars.Add(tmp);

                            if (Car.NextId <= tmp.CarId)
                                Car.NextId = tmp.CarId + 1;
                        }
                    }
                }
            }
        }
    }
}