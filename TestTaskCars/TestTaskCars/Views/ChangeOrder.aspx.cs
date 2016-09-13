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
    public partial class ChangeOrder : System.Web.UI.Page
    {
        OrderList cl = new OrderList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
            string str = Request.Url.Query.Remove(0, 1);
            Order c = new Order();
            String[] substrings = str.Split('I');
            int CarId = Convert.ToInt32(substrings[0]);
            int OrderId = Convert.ToInt32(substrings[1]);
            cl.GetCards(CarId);

            for (int i = 0; i < cl.Orders.Count; ++i)
                if (cl.Orders[i].OrderId == OrderId)
                    c = cl.Orders[i];
            if (!IsPostBack)
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

                TextBox1.Text = c.OrderId.ToString();
                TextBox2.Text = c.CarParentId.ToString();
                TextBox3.Text = c.Order_Amount.ToString("N2");

                DropDownList1.SelectedValue = c.Date.Day.ToString();
                DropDownList2.SelectedValue = c.Date.Month.ToString();
                DropDownList3.SelectedValue = c.Date.Year.ToString();
                DropDownList4.SelectedValue = c.Order_Status;

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = Request.Url.Query.Remove(0, 1);
            Order c = new Order();
            String[] substrings = str.Split('I');
            int CarId = Convert.ToInt32(substrings[0]);
            int OrderId = Convert.ToInt32(substrings[1]);
            int CardId = Convert.ToInt32(substrings[2]);
            c.OrderId = OrderId;
            c.CarParentId = CarId;
            c.Order_Amount = Convert.ToDouble(TextBox3.Text);
            c.Order_Status = DropDownList4.SelectedValue;
            DateTime dt = new DateTime(Convert.ToInt32(DropDownList3.SelectedValue),
                Convert.ToInt32(DropDownList2.SelectedValue),
                Convert.ToInt32(DropDownList1.SelectedValue));
            c.Date = dt;

            ChangeOrder1(c);

            Response.Redirect("../Views/Orders.aspx?" + CardId.ToString() + "I" + CarId.ToString());
        }

        protected void ChangeOrder1(Order c)
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

                String str = String.Format("UPDATE OrderTable set Order_Amount = convert(float, \'{0}\'), Order_Status = \'{1}\', ", c.Order_Amount.ToString().Replace(",", "."), c.Order_Status);
                str += String.Format("Date = \'{0}\' where OrderId = {1};",
                    c.Date.ToString("MM/dd/yyyy"), c.OrderId);

                cmd.CommandText = str;

                cmd.ExecuteNonQuery();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string str = Request.Url.Query.Remove(0, 1);
            Order c = new Order();
            String[] substrings = str.Split('I');
            int CarId = Convert.ToInt32(substrings[0]);
            int OrderId = Convert.ToInt32(substrings[1]);
            int CardId = Convert.ToInt32(substrings[2]);

            DeleteOrder(OrderId);
            Response.Redirect("../Views/Orders.aspx?" + CardId.ToString() + "I" + CarId.ToString());

        }

        protected void DeleteOrder(int id)
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

                String str = String.Format("DELETE FROM OrderTable WHERE OrderId = {0};", id);

                cmd.CommandText = str;

                cmd.ExecuteNonQuery();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string str = Request.Url.Query.Remove(0, 1);
            String[] substrings = str.Split('I');
            int CarId = Convert.ToInt32(substrings[0]);
            int OrderId = Convert.ToInt32(substrings[1]);
            int CardId = Convert.ToInt32(substrings[2]);
            Response.Redirect("../Views/Orders.aspx?" + CardId.ToString() + "I" + CarId.ToString());
        }
    }
}