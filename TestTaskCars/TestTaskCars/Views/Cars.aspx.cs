using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using TestTask.Models;

namespace TestTask.Views
{
    public partial class Cars : System.Web.UI.Page
    {
        CarList cl = new CarList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("../Default.aspx");
            string str = Request.Url.Query.Remove(0, 1);

            System.Text.StringBuilder dv = new System.Text.StringBuilder();
            cl.GetCards(Convert.ToInt32(str));
            dv.Append("<table align = \"center\" width = \"100%\" border=\"1\"><tr><th> Make </th><th> Model </th>");
            dv.Append("<th> Year </th><th> Vin </th><th> Orders </th><th> Change Car </th></tr>");
            for (int i = 0; i < cl.Cars.Count; ++i)
            {
                dv.Append("<tr><td> ");
                dv.Append(cl.Cars[i].Make);
                dv.Append(" </td><td> ");
                dv.Append(cl.Cars[i].Model);
                dv.Append(" </td><td> ");
                dv.Append(cl.Cars[i].Year.ToString());
                dv.Append(" </td><td> ");
                dv.Append(cl.Cars[i].Vin);
                dv.Append("</td><td><a href=\"../Views/Orders.aspx?");
                dv.Append(cl.Cars[i].CardParentId+"I"+cl.Cars[i].CarId.ToString());
                dv.Append("\">Orders</a></td>");
                dv.Append("<td><a href=\"../Views/ChangeCar.aspx?");
                dv.Append(cl.Cars[i].CardParentId.ToString() + "I" + cl.Cars[i].CarId.ToString());
                dv.Append("\">Change Parameters</a></td></tr>");
            }

            dv.Append("</table>");
            Label1.Text = dv.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNewCar.aspx?"+Request.Url.Query.Remove(0, 1));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}