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
    public partial class AddNewOrder : System.Web.UI.Page
    {
        int CardId, CarId;
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 31; ++i)
                DropDownList1.Items.Add(i.ToString());
            for (int i = 1; i <= 12; ++i)
                DropDownList2.Items.Add(i.ToString());
            for (int i = 2016; i >= 1920; --i)
                DropDownList3.Items.Add(i.ToString());

            DropDownList4.Items.Add("Completed");
            DropDownList4.Items.Add("In_Progress");
            DropDownList4.Items.Add("Cancelled");
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                Label4.Text = "Please, Enter all data";
            }
            else
            {
                Label4.Text = "";
                Order c = new Order();
                try
                {
                    c.SetNextId();
                    c.OrderId = Order.NextId;
                    String str = Request.Url.Query.Remove(0, 1);
                    String[] substrings = str.Split('I');
                    CardId = Convert.ToInt32(substrings[0]);
                    CarId = Convert.ToInt32(substrings[1]);
                    c.CarParentId = CarId;

                    DateTime dt = new DateTime(Convert.ToInt32(DropDownList3.SelectedValue),
                        Convert.ToInt32(DropDownList2.SelectedValue),
                        Convert.ToInt32(DropDownList1.SelectedValue));
                    c.Date = dt;
                    c.Order_Amount = Convert.ToDouble(TextBox1.Text);
                    c.Order_Status = DropDownList4.SelectedValue;
                    AddOrder(c);
                    Response.Redirect("Orders.aspx" + Request.Url.Query);
                }
                catch
                {
                    Label4.Text = "Your data is incorrect";
                }         
            }
        }


        protected void AddOrder(Order c)
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

                String str = "INSERT INTO OrderTable(OrderId, CarParentId, Date, Order_Amount, Order_Status) ";
                str += String.Format("Values({0}, {1}, \'{2}\', convert(float, \'{3}\'), \'{4}\');",
                    c.OrderId, c.CarParentId, c.Date.ToString("MM/dd/yyyy"),
                    c.Order_Amount.ToString().Replace(",","."), c.Order_Status);
                cmd.CommandText = str;

                cmd.ExecuteNonQuery();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx" + Request.Url.Query);
        }
    }
}