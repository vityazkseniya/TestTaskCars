using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestTask.Models;

namespace TestTask.Views
{
    public partial class Orders : System.Web.UI.Page
    {
        OrderList cl = new OrderList();
        int CardId;
        int CarId;
        protected void Page_Load(object sender, EventArgs e)
        {

            string str = Request.Url.Query.Remove(0, 1);
            String[] substrings = str.Split('I');
            CardId = Convert.ToInt32(substrings[0]);
            CarId = Convert.ToInt32(substrings[1]);
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
            System.Text.StringBuilder dv = new System.Text.StringBuilder();


            cl.GetCards(CarId);
            dv.Append("<table align = \"center\" width = \"100%\" border=\"1\"><tr><th> Date </th><th> Order Amount </th>");
            dv.Append("<th> Order Status </th><th> Change Settings </th></tr>");
            for (int i = 0; i < cl.Orders.Count; ++i)
            {
                dv.Append("<tr><td> ");
                dv.Append(cl.Orders[i].Date.ToString("dd.MM.yyyy"));
                dv.Append(" </td><td> ");
                dv.Append("US$ "+cl.Orders[i].Order_Amount.ToString("N2"));
                dv.Append(" </td><td> ");
                dv.Append(cl.Orders[i].Order_Status);
                dv.Append("</td>");
                dv.Append("<td><a href=\"../Views/ChangeOrder.aspx?");
                dv.Append(cl.Orders[i].CarParentId.ToString() + "I" + cl.Orders[i].OrderId.ToString() + "I" + CardId.ToString());
                dv.Append("\">Change Parameters</a></td></tr>");
            }

            dv.Append("</table>");
            Label1.Text = dv.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewOrder.aspx" + Request.Url.Query);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cars.aspx?" + CardId);
        }
    }
}