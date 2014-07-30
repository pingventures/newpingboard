using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //<add name="PingBoardConString" connectionString="Data Source=.;Initial Catalog=PingBoard;Integrated Security=True" providerName="System.Data.SqlClient"/>
        BoardLogin.checkMasterlogin();
        if (!IsPostBack)
        {
            string rm = (string)Session["RM"];
            string pt = (string)Session["PT"];
            string ID = (string)Session["ID"];
            string pf = (string)Session["PF"];
            string hash = (string)Session["Hash"];
            string creat = (string)Session["Created"];

            if (ID != null)
            {
                DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                if (creat == "Yes" && dt1.Rows.Count > 0)
                {
                    string HashTag = Convert.ToString(dt1.Rows[0]["BoardName"]);
                    if (pf.Contains("F"))
                        BoardLogic.InsertFBData(HashTag, ID);
                    if (pf.Contains("I"))
                        BoardLogic.InsertInstaData(HashTag, ID);
                    if (pf.Contains("T"))
                        BoardLogic.InsertTwitterData(HashTag, ID);
                }
                //dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                if (dt1.Rows.Count > 0)
                {
                    string p1 = "Facebook", p2 = "Twitter", p3 = "Instagram", m1 = "Photo", m2 = "Video", m3 = "Content";
                    string f1 = "0", f2 = "0", f3 = "0", f4 = "0";
                    if (pf == null) { }
                    else
                    {
                        if (pf.Contains('F'))
                        {
                            f1 = "1";
                            p3 = "Facebook";
                            p2 = "Facebook";
                        }
                        if (pf.Contains('T'))
                        {
                            f2 = "1";
                            p2 = "Twitter";
                            if (f1 == "0")
                            {
                                p1 = "Twitter";
                                p3 = "Twitter";
                            }
                        }
                        if (pf.Contains('I'))
                        {
                            p3 = "Instagram";
                            if (f2 == "0") { p2 = "Instagram"; }
                            if (f1 == "0") { p1 = "Instagram"; }
                        }
                    }
                    if (pt == null) { pt = "PVT"; }
                    else
                    {
                        if (pt.Contains('P'))
                        {
                            f3 = "1";
                            m2 = "Photo";
                            m3 = "Photo";
                        }
                        if (pt.Contains('V'))
                        {
                            f4 = "1";
                            m2 = "Video";
                            if (f3 == "0")
                            {
                                m1 = "Video";
                                m3 = "Video";
                            }
                        }
                        if (pt.Contains('T'))
                        {
                            m3 = "Content";
                            if (f4 == "0") { m2 = "Content"; }
                            if (f3 == "0") { m1 = "Content"; }
                        }
                    }
                    if (hash == null)
                    {
                        DataTable dt = BoardAccess.GetSortContent(ID, "GetSortCount", 0, 0, p1, p2, p3, m1, m2, m3);
                        int ct = dt.Rows.Count;
                        TotPost.Value = dt.Rows[0]["rCount"].ToString();
                    }
                    else {
                        DataTable dt = BoardAccess.GetSortBoard(ID, "GetSortCount", 0, 0, hash, p1, p2, p3, m1, m2, m3);
                        int ct = dt.Rows.Count;
                        TotPost.Value = dt.Rows[0]["rCount"].ToString();
                    }
                    if (TotPost.Value != "0")
                    {
                        DataTable dt = new DataTable();
                        if (hash == null)
                            dt = BoardAccess.GetSortContent(ID, "GetSortContent", 1, 29, p1, p2, p3, m1, m2, m3);
                        else
                            dt = BoardAccess.GetSortBoard(ID, "GetSortContent", 1, 29, hash, p1, p2, p3, m1, m2, m3);
                        if (rm != null)
                        {
                            TotPost.Value = rm;
                            dt.Columns.Add(new DataColumn("RandomNum", Type.GetType("System.Int32")));
                            Random random = new Random();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dt.Rows[i]["RandomNum"] = random.Next(1000);
                            }
                            DataView dv = new DataView(dt);
                            dv.Sort = "RandomNum";
                            DataTable newtt = dv.ToTable();
                            if (newtt.Rows.Count != 0)
                            {
                                dt = newtt.AsEnumerable().Take(Convert.ToInt16(rm)).CopyToDataTable();
                            }
                        }
                        Data1.DataSource = dt;
                        Data1.DataBind();
                        hideRes();
                    }
                    else
                    {
                        string message = "No Result Found For Your Hashtag Or On Your Filter.";
                        showRes(message, "error");
                    }
                }
                else
                {
                    string message = "This Board is not Created By You. Create or Access your board.";
                    showRes(message, "error");
                }
            }
        }
    }
    protected void Data1_Bound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Image ContPlatform = (Image)e.Item.FindControl("ContPlatform");
            Label cLK = (Label)e.Item.FindControl("ContLK");
            Label cLink = (Label)e.Item.FindControl("ContLink");
            Label cmsgType = (Label)e.Item.FindControl("ContMsgType");
            Label cID = (Label)e.Item.FindControl("ContUserID");

            HtmlGenericControl tData = (HtmlGenericControl)e.Item.FindControl("topData");
            HtmlGenericControl bL = (HtmlGenericControl)e.Item.FindControl("botLayout");
            HtmlGenericControl LK = (HtmlGenericControl)e.Item.FindControl("botLK");
            HtmlGenericControl CM = (HtmlGenericControl)e.Item.FindControl("botCM");
            HtmlGenericControl SH = (HtmlGenericControl)e.Item.FindControl("botSH");
            string platform = ContPlatform.AlternateText;
            if (cmsgType.Text == "Photo")
            {
                tData.InnerHtml = "<img width='252px' height='252px' alt='" + platform + "' src='" + cLink.Text + "' />";
            }
            if (cmsgType.Text == "Video")
            {
                if (cLink.Text.Contains("vine"))
                {
                    tData.InnerHtml = "<iframe src='" + cLink.Text + "/card' width='200' height='200' frameborder='0'></iframe>";
                }
                else if (cLink.Text.Contains("twitrpix"))
                {
                    tData.InnerHtml = "";
                }
                else
                {
                    tData.InnerHtml = "<a target='_blank' href='" + cLink.Text + "'><img alt='video' width='100%' src='images/video-player.jpg' /></a>";
                }
            }

            ContPlatform.AlternateText = "Icon";
            if (platform == "Twitter")
            {
                ContPlatform.ImageUrl = "images/twitter-corner-icon.png";
                LK.Visible = false;
            }
            if (platform == "Facebook")
            {
                ContPlatform.ImageUrl = "images/facebook-corner-icon.png";
                cID.Visible = false;
                bL.Visible = false;
            }
            if (platform == "Instagram")
            {
                ContPlatform.ImageUrl = "images/instagram-corner-icon.png";
                bL.Visible = false;
            }
            if (platform == "Pinterest")
            {
                ContPlatform.ImageUrl = "images/pintrest-corner-icon.png";
                bL.Visible = false;
            }
        }
    }
    protected void hideRes()
    {
        Result.Visible = false;
    }
    protected void showRes(string msg, string cl)
    {
        Result.Visible = true;
        Result.Attributes.Add("class", "error-area " + cl);
        Result.InnerText = msg;
    }
    [WebMethod]
    public static string TweetReply(string ID, string uID, string msg)
    {
        return "Tweet Replied Successfully";
    }
    [WebMethod]
    public static int AbandonSession()
    {
        HttpContext.Current.Session.Abandon();
        return 1;
    }
    [WebMethod]
    public static void ExcludeContent(string cID)
    {
        string ID = (string)HttpContext.Current.Session["ID"];
        BoardAccess.InsertContent("0", ID, "0", "0", "0", "0", "0", "0", "0", cID, "0", "0", "0", "ExcludeContent", "01-01-1900", "True", "0", "0", "0");        
    }
    [WebMethod]
    public static string BoardDelete(string ID)
    {
        //string boardid = (string) Session["ID"];
        if (BoardAccess.InsertBoard(ID, "0", BoardLogin.getid(), "0", "True", "True", "Delete"))
        {
            BoardAccess.InsertContent("0", ID, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "Delete", "01-01-1990", "False", "0", "0", "0");
            DataTable dt = BoardAccess.GetBoard("", BoardLogin.getid(), "GetTop", "True", "True", "0");
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Session["ID"] = Convert.ToString(dt.Rows[0]["BoardID"]);
                HttpContext.Current.Session["Created"] = "NO";
                //HttpContext.Current.Response.Redirect("Home.aspx");
            }
            return ID;
        }
        else
            return "Fail";
        //HttpContext.Current.Response.Redirect("Home.aspx");
    }
    [WebMethod]
    public static string AddExclude(string key)
    {
        string txt = key.Trim();
        txt = BoardLink.PrepareUserText(txt);
        if (txt != "")
        {
            string ID = (string)HttpContext.Current.Session["ID"];
            DataTable dt = BoardAccess.GetKey(ID, "Search", key);
            if (dt.Rows.Count == 0)
            {
                if (BoardAccess.InsertKey(key, ID))
                {
                    DataTable dt1 = BoardAccess.GetKey(ID, "Search", key);
                    if (dt1.Rows.Count > 0)
                    {
                        string kID = dt1.Rows[0]["KeyID"].ToString();
                        return kID;
                    }
                    else { return "File Update Fail!"; }
                }
                else { return "Exclude Keyword Failed"; }
            }
            else { return "Keyword Already Excluded"; }
        }
        else { return "Fill Valid Keyword Before Submit"; }
    }
    [WebMethod]
    public static string DelExclude(string KeyID)
    {
        if (BoardAccess.DeleteKey(KeyID)) { return KeyID; }
        else { return "Fail"; }
        //DelExclude
    }
}