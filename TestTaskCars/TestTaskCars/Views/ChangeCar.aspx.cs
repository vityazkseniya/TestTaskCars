using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestTask.Models;
using System.Data.Common;

namespace TestTask.Views
{
    public partial class ChangeCar : System.Web.UI.Page
    {
        
        CarList cl = new CarList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
            string str = Request.Url.Query.Remove(0, 1);
            Car c = new Car();
            String[] substrings = str.Split('I');
            int CardId = Convert.ToInt32(substrings[0]);
            int CarId = Convert.ToInt32(substrings[1]);
            cl.GetCards(CardId);
            
            for (int i = 0; i < cl.Cars.Count; ++i)
                if (cl.Cars[i].CarId == CarId)
                    c = cl.Cars[i];
            if (!IsPostBack)
            {
                for (int i = 2016; i >= 1950; --i)
                    DropDownList1.Items.Add(i.ToString());
                TextBox1.Text = c.CarId.ToString();
                TextBox2.Text = c.CardParentId.ToString();
                TextBox3.Text = c.Make;
                TextBox4.Text = c.Model;
                DropDownList1.SelectedValue = c.Year.ToString();
                TextBox5.Text = c.Vin;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string str = Request.Url.Query.Remove(0, 1);
            Car c = new Car();
            String[] substrings = str.Split('I');
            int CardId = Convert.ToInt32(substrings[0]);
            int CarId = Convert.ToInt32(substrings[1]);

            c.CarId = CarId;
            c.CardParentId = CardId;
            c.Make = TextBox3.Text;
            c.Model = TextBox4.Text;
            c.Year = Convert.ToInt32(DropDownList1.SelectedValue);
            c.Vin = TextBox5.Text;

            ChangeCar1(c);

            Response.Redirect("../Views/Cars.aspx?" + CardId.ToString());
        }

        protected void ChangeCar1(Car c)
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

                String str = String.Format("UPDATE CarTable set Make = \'{0}\', Model = \'{1}\', ", c.Make, c.Model);
                str += String.Format("Year = {0}, Vin = \'{1}\' where CarId = {2};",
                    c.Year, c.Vin, c.CarId);

                cmd.CommandText = str;

                cmd.ExecuteNonQuery();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string str = Request.Url.Query.Remove(0, 1);
            Car c = new Car();
            String[] substrings = str.Split('I');
            int CardId = Convert.ToInt32(substrings[0]);
            int CarId = Convert.ToInt32(substrings[1]);

            OrderList ol = new OrderList();
            ol.GetCards(CarId);

            if (ol.Orders.Count == 0)
            {
                DeleteCard(CarId);
                Response.Redirect("../Views/Cars.aspx?" + CardId.ToString());
            }
            else Label7.Text = "Sorry, but orders not empty";
        }

        protected void DeleteCard(int id)
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

                String str = String.Format("DELETE FROM CarTable WHERE CarId = {0};", id);

                cmd.CommandText = str;

                cmd.ExecuteNonQuery();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string str = Request.Url.Query.Remove(0, 1);
            String[] substrings = str.Split('I');
            int CardId = Convert.ToInt32(substrings[0]);
            int CarId = Convert.ToInt32(substrings[1]);
            Response.Redirect("../Views/Cars.aspx?" + CardId.ToString());
        }
    }
}