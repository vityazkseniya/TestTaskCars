using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestTask.Models;

namespace TestTask
{
    public partial class _Default : System.Web.UI.Page
    {
        CardList cl = new CardList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                System.Text.StringBuilder dv = new System.Text.StringBuilder();
                cl.GetCards();
                dv.Append("<table align = \"center\" width = \"100%\" border=\"1\"><tr><th> First Name </th><th> Last Name </th>");
                dv.Append("<th> Date of Birth </th><th> Address </th><th> Phone </th><th> Email </th><th> Cars </th><th> Change Profile </th></tr>");
                for (int i = 0; i < cl.Cards.Count; ++i)
                {
                    dv.Append("<tr><td> ");
                    dv.Append(cl.Cards[i].First_Name);
                    dv.Append(" </td><td> ");
                    dv.Append(cl.Cards[i].Last_Name);
                    dv.Append(" </td><td> ");
                    dv.Append(cl.Cards[i].Date_Of_Birth.ToString("dd.MM.yyyy"));
                    dv.Append(" </td><td> ");
                    dv.Append(cl.Cards[i].Address);
                    dv.Append(" </td><td> ");
                    dv.Append(cl.Cards[i].Phone);
                    dv.Append(" </td><td> <a href=\"mailto:");
                    dv.Append(cl.Cards[i].Email);
                    dv.Append("\">");
                    dv.Append(cl.Cards[i].Email);
                    //dv.Append("</a></td><td><a href=\"Views/Cars.aspx?id=\">Cars</a></td></tr>");
                    dv.Append("</a></td><td><a href=\"Views/Cars.aspx?");
                    dv.Append(cl.Cards[i].CardId.ToString());
                    dv.Append("\">Cars</a></td>");
                    dv.Append("</td><td><a href=\"Views/ChangeCard.aspx?");
                    dv.Append(cl.Cards[i].CardId.ToString());
                    dv.Append("\">Profile</a></td></tr>");
                }

                dv.Append("</table>");
                Label1.Text = dv.ToString();

            }

            else Label1.Text = "<h1>Please Log In!</h1>";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "click";
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Label1.Text = "click";
        }
    }
}
