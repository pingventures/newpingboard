using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{    
                    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hideMsg();
        }        
    }
    protected void Link1_Click(object sender, EventArgs e)
    {
        if (txtBoard.Text.Trim() != "")
        {
            string txt = txtBoard.Text.Trim();
            
            txt = BoardLink.PrepareUserText(txt);
            string stat = BoardLogin.getuserstat();
            if (txt != "" && txt.Length>2)
            {
                if (!Regex.IsMatch(txt, @"^[0-9]+$"))
                {
                    string subnum = txt.Substring(0, 3);
                    if (!Regex.IsMatch(subnum, @"^[0-9]+$"))
                    {
                        if (stat == "Standard")
                        {
                            DataTable dt2 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardDetails", "True", "True", "0");
                            if (dt2.Rows.Count < 1)
                            {
                                creatboard(txt);
                            }
                            else
                            {
                                showMsg("HashTag Limitation Over");
                            }
                        }
                        else if (stat == "Pro")
                        {
                            DataTable dt2 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardDetails", "True", "True", "0");
                            if (dt2.Rows.Count < 3)
                            {
                                creatboard(txt);
                            }
                            else
                            {
                                showMsg("HashTag Limitation Over");
                            }
                        }
                        else
                        {
                            creatboard(txt);
                        }
                    }
                    else
                    {
                        showMsg("Submit Valid Hashtag");
                    }
                }
                else
                {
                    showMsg("Submit Valid Hashtag");
                }
               
            }
            else {
                HtmlGenericControl Divid = (HtmlGenericControl)Page.Master.FindControl("TopResult");
                Divid.Visible = false;
                showMsg("Submit Valid Hashtag"); 
            }
        }
        else
        { showMsg("Fill HashTag Before Submit!");         
        }
    }
    protected void showMsg(string msg)
    {
        errorBox.Visible = true;
        Result.Text = msg;
    }
    protected void hideMsg()
    {
        errorBox.Visible = false;
        Result.Text = "";
    }
    protected void creatboard(string txt)
    {
        if (txt.Length > 2)
        {
            DataTable dt = BoardAccess.GetBoard(txt, BoardLogin.getid(), "GetBoard", "True", "True", "0");
            if (dt.Rows.Count == 0)
            {
                string platform = "Facebook,Twitter,Instagram,Pinterest";
                if (BoardAccess.InsertBoard("0", txt, BoardLogin.getid(), platform, "True", "True", "Insert"))
                {
                    DataTable dt1 = BoardAccess.GetBoard(txt, BoardLogin.getid(), "GetBoard", "True", "True", "0");
                    if (dt1.Rows.Count > 0)
                    {
                        Session["ID"] = dt1.Rows[0]["BoardID"].ToString();
                        Session["PF"] = "FTI";
                        Session["Created"] = "Yes";
                        Response.Redirect("Home.aspx");
                    }
                    else { showMsg("Hashtag Not Found! Inform Administrator"); }
                }
                else { showMsg("Hashtag Not Created! Got Some Error"); }
            }
            else
            {
                Session["ID"] = dt.Rows[0]["BoardID"].ToString();
                Session["PF"] = "FTI";
                Session["Created"] = "NO";
                Response.Redirect("Home.aspx");
            }
        }
        else { showMsg("Hashtag Should Be Equal Or More than 3 Characters"); }
    }
}