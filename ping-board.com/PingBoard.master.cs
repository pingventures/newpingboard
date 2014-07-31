using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web.Services;

public partial class PingBoard : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ID = (string)Session["ID"];
        // string boardid = Convert.ToString(dt1.Rows[0]["BoardID"]);
        if (!IsPostBack)
        {
            hideRes();
            BoardLogin.checkMasterlogin();
            //ID = Request.QueryString["ID"];

            string PF = (string)Session["PF"];
            string PT = (string)Session["PT"];
            if (ID != null)
            {
                if (PF != null)
                {
                    if (PF.Contains("F")) { CBox1.Checked = true; ChBox1.Checked = true; }
                    if (PF.Contains("T")) { CBox2.Checked = true; ChBox2.Checked = true; }
                    if (PF.Contains("I")) { CBox3.Checked = true; ChBox3.Checked = true; }
                }
                if (PT != null)
                {
                    if (PT.Contains("P")) { CBox5.Checked = true; ChBox4.Checked = true; }
                    if (PT.Contains("V")) { CBox6.Checked = true; ChBox5.Checked = true; }
                    if (PT.Contains("T")) { CBox7.Checked = true; ChBox6.Checked = true; }
                }
                else
                {
                    CBox5.Checked = true;
                    CBox6.Checked = true;
                    CBox7.Checked = true;
                    ChBox4.Checked = true;
                    ChBox5.Checked = true;
                    ChBox6.Checked = true;
                }
                analy.Attributes.Add("href", "Analytic.aspx");
                aSocial.Attributes.Add("href", "AddSocial.aspx");

                DataTable dt = BoardAccess.GetBoard("", BoardLogin.getid(), "GetTop", "True", "True", "0");
                Myboard.DataSource = dt;
                Myboard.DataBind();
                DataTable dt2 = new DataTable();
                dt2 = BoardAccess.GetKey(ID, "KeyDetails", "0");
                if (dt2.Rows.Count > 0)
                {
                    HtmlTable tbl = new HtmlTable();
                    tbl.ID = "DataDel";
                    tbl.Attributes.Add("class", "key");
                    tbl.CellSpacing = 0;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        HtmlTableRow tr = new HtmlTableRow();
                        HtmlTableCell td = new HtmlTableCell();
                        HtmlGenericControl ul = new HtmlGenericControl("ul");
                        ul.ID = "KI-" + dt2.Rows[i]["KeyID"].ToString();
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        HtmlImage img = new HtmlImage();
                        img.Src = "images/popup-board-icon.png";
                        img.Alt = "Board";
                        li.Controls.Add(img);
                        HtmlGenericControl span = new HtmlGenericControl("span");
                        span.InnerText = dt2.Rows[i]["Keywords"].ToString();
                        li.Controls.Add(span);
                        HtmlAnchor a = new HtmlAnchor();
                        a.HRef = "#";
                        a.Attributes.Add("alt", dt2.Rows[i]["KeyID"].ToString());
                        a.Attributes.Add("class", "DelExclude");
                        HtmlImage img1 = new HtmlImage();
                        img1.Src = "images/delete-box-icon.png";
                        img1.Alt = "Delete Keyword";
                        a.Controls.Add(img1);
                        li.Controls.Add(a);
                        ul.Controls.Add(li);
                        td.Controls.Add(ul);
                        tr.Controls.Add(td);
                        tbl.Controls.Add(tr);
                    }
                    KeyList.Controls.Add(tbl);
                }
                else {
                    HtmlTable tbl = new HtmlTable();
                    tbl.ID = "DataDel";
                    tbl.Attributes.Add("class", "key");
                    tbl.CellSpacing = 0;

                    HtmlTableRow tr = new HtmlTableRow();
                    HtmlTableCell td = new HtmlTableCell();
                    tr.Controls.Add(td);
                    tbl.Controls.Add(tr);
                    KeyList.Controls.Add(tbl);
                }
                DataTable addhash = new DataTable();
                addhash.Columns.Add("HashTag", typeof(string));
                addhash.Columns.Add("ID", typeof(string));
                DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                if (dt1.Rows.Count > 0)
                {
                    MyboardName.Text = Convert.ToString(dt1.Rows[0]["BoardName"]);
                    string hashtag = Convert.ToString(dt1.Rows[0]["BoardName"]);
                    string[] tothashtag = hashtag.Split('-');
                    if (tothashtag.Length > 0)
                    {
                        foreach (string HashWord in tothashtag)
                        {
                            addhash.Rows.Add(HashWord, ID);
                        }
                    }
                    TagList.DataSource = addhash;
                    TagList.DataBind();
                }
            }
        }
    }
    //Board Logout
    protected void Link1_Click(object sender, EventArgs e)
    {
        BoardLogin.logoutwebsite();
    }
    //Create Another Hashtag
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (prBoard.Text.Trim() != "")
        {
            string txt = prBoard.Text.Trim();

            txt = BoardLink.PrepareUserText(txt);
            string stat = BoardLogin.getuserstat();
            if (txt != "" && txt.Length > 2)
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
                            else { showRes("Hashtag Limitation Over", "error"); }
                        }
                        else if (stat == "Pro")
                        {
                            DataTable dt2 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardDetails", "True", "True", "0");
                            if (dt2.Rows.Count < 3)
                            {
                                creatboard(txt);
                            }
                            else { showRes("Hashtag Limitation Over", "error"); }
                        }
                        else
                        {
                            creatboard(txt);
                        }
                    }
                    else { showRes("Submit Valid Hashtag", "error"); }
                }
                else { showRes("Submit Valid Hashtag", "error"); }
            }
            else { showRes("Submit Valid Hashtag", "error"); }
        }
    }
    //Add Another Hashtag
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (prExBoard.Text.Trim() != "")
        {
            string addhash = "";
            addhash = BoardLink.PrepareUserText(prExBoard.Text.Trim());
            if (addhash != "" && addhash.Length > 2)
            {
                if (!Regex.IsMatch(addhash, @"^[0-9]+$"))
                {
                    string subnum = addhash.Substring(0, 3);
                    if (!Regex.IsMatch(subnum, @"^[0-9]+$"))
                    {
                        string ID = (string)Session["ID"];
                        string stat = BoardLogin.getuserstat();
                        DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                        if (dt1.Rows.Count > 0)
                        {

                            string hashtag = dt1.Rows[0]["BoardName"].ToString();
                            string[] tothash = hashtag.Split('-');
                            if (tothash.Length < 6)
                            {
                                int flag = 0;
                                foreach (string word in tothash)
                                {
                                    if (addhash == word)
                                    {
                                        flag = 1;
                                    }
                                }
                                if (flag == 0)
                                {
                                    hashtag = hashtag + "-" + addhash;
                                    BoardAccess.InsertBoard(ID, hashtag, "0", "0", "True", "True", "Update");
                                    Session["Created"] = "NO";
                                    Response.Redirect("Home.aspx");
                                }
                                else { showRes("HashTag Exist", "error"); }
                            }
                            else { showRes("HashTag Limitation Over", "error"); }
                        }
                        else
                        {
                            BoardAccess.InsertBoard(ID, addhash, "0", "0", "True", "True", "Update");
                            Session["Created"] = "NO";
                            Response.Redirect("Home.aspx");
                        }
                    }
                    else { showRes("Fill Valid Hashtag", "error"); }
                }
                else { showRes("Fill Valid Hashtag", "error"); }
            }
            else { showRes("Fill The Valid Hashtag Or Should be 3 characters long", "error"); }
        }
        else { showRes("Fill Hashtag Before Submit", "error"); }
    }
    //Sort Content
    protected void Button3_Click(object sender, EventArgs e)
    {
        string PF = "";
        if (CBox1.Checked) { PF += "F"; }
        if (CBox2.Checked) { PF += "T"; }
        if (CBox3.Checked) { PF += "I"; }
        string PT = "";
        if (CBox5.Checked) { PT += "P"; }
        if (CBox6.Checked) { PT += "V"; }
        if (CBox7.Checked) { PT += "T"; }
        Session["RM"] = null;
        Session["PF"] = PF;
        Session["PT"] = PT;
        Session["Created"] = "NO";
        Response.Redirect("Home.aspx");
    }
    //Randomiser with Sort
    protected void Button6_Click(object sender, EventArgs e)
    {
        string PF = "";
        if (ChBox1.Checked) { PF += "F"; }
        if (ChBox2.Checked) { PF += "T"; }
        if (ChBox3.Checked) { PF += "I"; }
        string PT = "";
        if (ChBox4.Checked) { PT += "P"; }
        if (ChBox5.Checked) { PT += "V"; }
        if (ChBox6.Checked) { PT += "T"; }
        string RM = Drop1.SelectedValue;
        Session["RM"] = RM;
        Session["PF"] = PF;
        Session["PT"] = PT;
        Session["Created"] = "NO";
        Response.Redirect("Home.aspx");
    }
    //Hide Message
    protected void hideRes()
    {
        TopResult.Visible = false;
    }
    //Show Message
    protected void showRes(string msg, string cl)
    {
        TopResult.Visible = true;
        TopResult.Attributes.Add("class", "error-area " + cl);
        TopResult.InnerText = msg;
    }
    //For Board
    protected void Myboard_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "MyBoardLink")
        {
            string ID = e.CommandArgument.ToString();
            if (!Request.Url.ToString().Contains("Home"))
            {
                DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                Session["ID"] = ID;
                Session["PF"] = "FTI";
                Session["PT"] = "PVT";
                Session["RM"] = null;
                Session["Created"] = "NO";
                Response.Redirect("Home.aspx");

            }
            else if (Session["ID"].ToString() != ID)
            {
                DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                Session["ID"] = ID;
                Session["PF"] = "FTI";
                Session["PT"] = "PVT";
                Session["RM"] = null;
                Session["Created"] = "NO";
                Response.Redirect("Home.aspx");
            }
        }
        //if (e.CommandName == "BoardDelete")
        //{
        //    string ID = e.CommandArgument.ToString();
        //    string boardid = (string)Session["ID"];
        //    BoardAccess.InsertBoard(ID, "0", BoardLogin.getid(), "0", "True", "True", "Delete");
        //    BoardAccess.InsertContent("0", ID, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "Delete", "0", "0", "0", "0", "0");
        //    DataTable dt = BoardAccess.GetBoard("", BoardLogin.getid(), "GetTop", "True", "True", "0");
        //    if (dt.Rows.Count > 0)
        //    {
        //        Session["ID"] = Convert.ToString(dt.Rows[0]["BoardID"]);
        //        Session["Created"] = "NO";
        //        Response.Redirect("Home.aspx");
        //    }
        //    else
        //        Response.Redirect("Default.aspx");
        //}
    }
    //Delete ANother Hashtag
    protected void TagList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "SortHash")
        {
            Session["Hash"] = e.CommandArgument.ToString();
            Session["RM"] = null;
            Session["PF"] = "FTI";
            Session["PT"] = "PVT";
            Session["Created"] = "NO";
            Response.Redirect("Home.aspx");
        }
        if (e.CommandName == "HashDelete")
        {
            string hashtag = e.CommandArgument.ToString();
            string ID = (string)Session["ID"];
            DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
            string origashtag = Convert.ToString(dt1.Rows[0]["BoardName"]);
            string updatehashtag = "";
            string[] tothash = origashtag.Split('-');
            if (tothash.Length > 0)
            {
                int flag = 0;
                foreach (string HashWord in tothash)
                {
                    if (HashWord != hashtag)
                    {
                        if (flag == 0)
                        {
                            updatehashtag = updatehashtag + HashWord;
                            flag = 1;
                        }
                        else
                        {
                            updatehashtag = updatehashtag + "-" + HashWord;
                        }
                    }
                }
            }
            BoardAccess.InsertBoard(ID, updatehashtag, "0", "0", "True", "True", "Update");
            bool res = BoardAccess.InsertContent("0", ID, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "HashDelete", "01-01-1900", "True", hashtag, "0", "0");
            Session["Created"] = "NO";
            Response.Redirect("Home.aspx");
        }
    }
    //Create Board
    protected void creatboard(string txt)
    {
        if (txt.Length > 2)
        {
            string platform = "";
            string pf = "";
            if (Check1.Checked) { platform += "Facebook,"; pf += "F"; }
            if (Check2.Checked) { platform += "Twitter,"; pf += "T"; }
            if (Check4.Checked) { platform += "Instagram,"; pf += "I"; }
            if (platform != "") { platform = platform.Substring(0, platform.Length - 1); }
            DataTable dt = BoardAccess.GetBoard(txt, BoardLogin.getid(), "GetBoard", "True", "True", "0");
            if (dt.Rows.Count == 0)
            {
                if (BoardAccess.InsertBoard("0", txt, BoardLogin.getid(), platform, "True", "True", "Insert"))
                {
                    DataTable dt1 = BoardAccess.GetBoard(txt, BoardLogin.getid(), "GetBoard", "True", "True", "0");
                    if (dt1.Rows.Count > 0)
                    {
                        Session["PF"] = pf;
                        Session["ID"] = dt1.Rows[0]["BoardID"].ToString();
                        Session["Created"] = "Yes";
                        Response.Redirect("Home.aspx");
                    }
                    else { showRes("Hashtag Not Created! Inform Administrator", "error"); }
                }
                else { showRes("Hashtag Not Created!", "error"); }
            }
            else
            {
                Session["PF"] = pf;
                Response.Redirect("Home.aspx");
            }
        }
        else { showRes("Hashtag Should Be Equal Or More than 3 Characters", "error"); }

    }   
}