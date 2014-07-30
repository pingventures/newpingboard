using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string x = BoardLogic.InsertLays("0", "22", "45", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", Convert.ToDateTime("01-01-1990"), "0", "0", "0", "0");
        Label1.Text=x;
    }
}