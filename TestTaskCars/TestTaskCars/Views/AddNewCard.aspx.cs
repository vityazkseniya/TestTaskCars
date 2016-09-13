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
    public partial class AddNewCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 31; ++i)
                DropDownList1.Items.Add(i.ToString());
            for (int i = 1; i <= 12; ++i)
                DropDownList2.Items.Add(i.ToString());
            for (int i = 2000; i >= 1920; --i)
                DropDownList3.Items.Add(i.ToString());

            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" ||
                TextBox4.Text == "" || TextBox5.Text == "")
            {
                Label7.Text = "Please, Enter all data";
            }
            else
            {
                Label7.Text = "";
                Card c = new Card();
                try
                {                    
                    c.SetNextId();
                    c.CardId = Card.NextId;
                    c.First_Name = TextBox1.Text;
                    c.Last_Name = TextBox2.Text;
                    DateTime dt = new DateTime(Convert.ToInt32(DropDownList3.SelectedValue),
                        Convert.ToInt32(DropDownList2.SelectedValue),
                        Convert.ToInt32(DropDownList1.SelectedValue));
                    c.Date_Of_Birth = dt;
                    c.Address = TextBox3.Text;
                    c.Phone = TextBox4.Text;
                    c.Email = TextBox5.Text;
                    AddCard(c);
                    Response.Redirect("../Default.aspx");
                }
                catch
                {
                    Label7.Text = "Your data is incorrect";
                }                
            }
        }

        protected void AddCard(Card c)
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

                String str = "INSERT INTO CardTable(CardId, First_Name, Last_Name, Date_Of_Birth, Address, Phone, Email) ";
                str += String.Format("Values({0}, \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', \'{6}\');",
                    c.CardId, c.First_Name, c.Last_Name, c.Date_Of_Birth.ToString("MM/dd/yyyy"),
                    c.Address, c.Phone, c.Email);
                cmd.CommandText = str;


                cmd.ExecuteNonQuery();
             }
        }
    }
}