using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using TestTask.Models;

namespace TestTask.Views
{
    public partial class AddNewCar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 2016; i >= 1950; --i)
                DropDownList1.Items.Add(i.ToString());
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "")
            {
                Label5.Text = "Please, Enter all data";
            }
            else
            {
                Label5.Text = "";
                Car c = new Car();
                try
                {
                    c.SetNextId();
                    c.CarId = Card.NextId;
                    c.CardParentId = Convert.ToInt32(Request.Url.Query.Remove(0, 1));
                    c.Make = TextBox1.Text;
                    c.Model = TextBox2.Text;
                    c.Year = Convert.ToInt32(DropDownList1.SelectedValue);
                    c.Vin = TextBox3.Text;
                    AddCar(c);
                    Response.Redirect("Cars.aspx" + Request.Url.Query);
                }
                catch
                {
                    Label5.Text = "Your data is incorrect";
                }           
            }
        }

        protected void AddCar(Car c)
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

                String str = "INSERT INTO CarTable(CarId, CardParentId, Make, Model, Year, Vin) ";
                str += String.Format("Values({0}, {1}, \'{2}\', \'{3}\', {4}, \'{5}\');",
                    c.CarId, c.CardParentId, c.Make, c.Model,
                    c.Year, c.Vin);
                cmd.CommandText = str;

               // Label7.Text = str;

                cmd.ExecuteNonQuery();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cars.aspx" + Request.Url.Query);
        }
    }
}