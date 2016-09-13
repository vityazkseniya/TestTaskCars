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
    public partial class ChangeCard : System.Web.UI.Page
    {
        CardList cl = new CardList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
            cl.GetCards();
            string str = Request.Url.Query.Remove(0, 1);
            Card c = new Card();
            int id = Convert.ToInt32(str);
            for (int i = 1; i <= 31; ++i)
                DropDownList1.Items.Add(i.ToString());
            for (int i = 1; i <= 12; ++i)
                DropDownList2.Items.Add(i.ToString());
            for (int i = 2000; i >= 1920; --i)
                DropDownList3.Items.Add(i.ToString());

            for (int i = 0; i < cl.Cards.Count; ++i)
                if (cl.Cards[i].CardId == id)
                    c = cl.Cards[i];
            if (!IsPostBack)
            {
                TextBox1.Text = c.CardId.ToString();
                TextBox3.Text = c.First_Name;
                TextBox4.Text = c.Last_Name;
                DropDownList1.SelectedValue = c.Date_Of_Birth.Day.ToString();
                DropDownList2.SelectedValue = c.Date_Of_Birth.Month.ToString();
                DropDownList3.SelectedValue = c.Date_Of_Birth.Year.ToString();
                TextBox5.Text = c.Address;
                TextBox6.Text = c.Phone;
                TextBox7.Text = c.Email;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Card c = new Card();
            string str = Request.Url.Query.Remove(0, 1);
            int id = Convert.ToInt32(str);
            c.CardId = id;
            c.First_Name = TextBox3.Text;
            c.Last_Name = TextBox4.Text;
            DateTime dt = new DateTime(Convert.ToInt32(DropDownList3.SelectedValue),
                Convert.ToInt32(DropDownList2.SelectedValue),
                Convert.ToInt32(DropDownList1.SelectedValue));
            c.Date_Of_Birth = dt;
            c.Address = TextBox5.Text;
            c.Phone = TextBox6.Text;
            c.Email = TextBox7.Text;

            ChangeCard1(c);

            Response.Redirect("../Default.aspx");
        }

        protected void ChangeCard1(Card c)
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

                String str = String.Format("UPDATE CardTable set First_Name = \'{0}\', Last_name = \'{1}\', ", c.First_Name, c.Last_Name);
                str += String.Format("Date_Of_Birth = \'{0}\', Address = \'{1}\', Phone = \'{2}\', Email = \'{3}\' where CardId = {4};",
                    c.Date_Of_Birth.ToString("MM/dd/yyyy"), c.Address, c.Phone, c.Email, c.CardId);

                cmd.CommandText = str;

                Label8.Text = str;
                cmd.ExecuteNonQuery();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Card c = new Card();
            string str = Request.Url.Query.Remove(0, 1);
            int id = Convert.ToInt32(str);
            CarList car = new CarList();
            car.GetCards(id);

            if(car.Cars.Count == 0)
            {
                DeleteCard(id);
                Response.Redirect("../Default.aspx");
            }
            else Label8.Text = "Sorry, but cars not empty";

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

                String str = String.Format("DELETE FROM CardTable WHERE CardId = {0};", id);
                
                cmd.CommandText = str;

                Label8.Text = str;
                cmd.ExecuteNonQuery();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}