using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Dynamic;
using Newtonsoft.Json.Linq;
using Tweetinvi;
using Facebook;
using System.Text.RegularExpressions;

public partial class web : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            regis.Visible = true;
            user1.Visible = false;
            string id = BoardLogin.getid();
            if (id != "")
            {
                regis.Visible = false;
                user1.Visible = true;
                userName.Text = BoardLogin.getname();
                uID.Value = BoardLogin.getid();
            }
            hideResult();
            string ab = Request.Url.AbsoluteUri;
            if (ab.ToLower().Contains("default.aspx")) { webFoot.Visible = false; }            
            string reg = Request.QueryString["reg"];
            if (reg == "Success") { showResult("Registration Completed! Now Create Board", "success"); }
            string res = Request.QueryString["session"];
            if (res == "logout")
            { showResult("Successfully Logout!", "success"); }
            if (res == "fail")
            { showResult("Session Fail! Login Again", "error"); }

            if (Request.QueryString["oauth_verifier"] != null)
            {
                var callbackURL = HttpContext.Current.Request.Url.AbsoluteUri;
                var applicationCredentials = CredentialsCreator.GenerateApplicationCredentials(BoardConfig.ConsumerKey, BoardConfig.ConsumerSecret);
                applicationCredentials.AuthorizationKey = Session["akey"].ToString();
                applicationCredentials.AuthorizationSecret = Session["aSecret"].ToString();
                // Here we provide the entire URL where the user has been redirected
                var newCredentials = CredentialsCreator.GetCredentialsFromVerifierCode(Request.QueryString["oauth_verifier"].ToString(), applicationCredentials);
                if (newCredentials != null)
                {
                    setCredentials(newCredentials.AccessToken, newCredentials.AccessTokenSecret);
                    var user = Tweetinvi.User.UserFactory.GetLoggedUser();
                    DataTable twUser = BoardAccess.GetUser("0", "NA", Convert.ToString(user.Id), "CheckSocial");
                    if (twUser.Rows.Count == 0)
                    {
                        if (BoardAccess.InsertUser("0", user.Name, "", "", "Standard", user.ScreenName, "Twitter",
                            Convert.ToString(user.Id), newCredentials.AccessToken, newCredentials.AccessTokenSecret, "Insert"))
                        {
                            DataTable dt = BoardAccess.GetUser("0", "NA", Convert.ToString(user.Id), "CheckSocial");
                            if (dt.Rows.Count > 0)
                            {
                                Session["PingBoardID"] = dt.Rows[0]["UserID"].ToString();
                                Session["PingBoardName"] = dt.Rows[0]["UserName"].ToString();
                                Session["PingBoardStatus"] = dt.Rows[0]["UserStatus"].ToString();
                                userName.Text = dt.Rows[0]["UserName"].ToString();
                                uID.Value = BoardLogin.getid();
                                user1.Visible = true;
                                regis.Visible = false;
                                showResult("Registration Completed! Now Create Board", "success");
                            }
                        }
                    }
                    else {
                        Session["PingBoardID"] = twUser.Rows[0]["UserID"].ToString();
                        Session["PingBoardName"] = twUser.Rows[0]["UserName"].ToString();
                        Session["PingBoardStatus"] = twUser.Rows[0]["UserStatus"].ToString();
                        userName.Text = twUser.Rows[0]["UserName"].ToString();
                        DataTable dt1 = BoardAccess.GetBoard("", twUser.Rows[0]["UserID"].ToString(), "GetAllByUser", "False","True","0");
                        if (dt1.Rows.Count > 0)
                        {
                            Session["ID"] = dt1.Rows[0]["BoardID"].ToString();
                            Session["PF"] = "FTI";
                            Response.Redirect("Home.aspx");
                            
                        }
                    }
                    Response.Redirect("Default.aspx?reg=Success");
                }
            }
            if (Request.QueryString["code"] != null)
            {
                if (Convert.ToString(Session["code"]) == "facebook")
                {
                    //string url = Request.Url.AbsolutePath;
                    string code = Request.QueryString["code"].ToString();
                    //Response.Write(url + ", " + code);
                    string authurl = HttpContext.Current.Request.Url.AbsoluteUri;
                    if (authurl.Contains("?")) { authurl = authurl.Substring(0, authurl.IndexOf('?')); }
                    string token = BoardLogin.ExchangeCode(code, authurl);
                    //Response.Write(token);
                    string ntoken = BoardLogin.RefreshAccessToken(token);
                    dynamic data = BoardLogin.GetFBData(ntoken, "me/");
                    if (data.id != null)
                    {
                        string email = Convert.ToString(data.email);
                        if (email == null || email == "") { email = "NA"; }
                        DataTable twUser = BoardAccess.GetUser("0", email, Convert.ToString(data.id), "CheckSocial");
                        if (twUser.Rows.Count == 0)
                        {
                            if (BoardAccess.InsertUser("0", data.name, "", data.email, "Standard", data.first_name, "Facebook",
                                Convert.ToString(data.id), ntoken, "", "Insert"))
                            {
                                DataTable dt = BoardAccess.GetUser("0", email, data.id, "CheckSocial");
                                if (dt.Rows.Count > 0)
                                {
                                    Session["PingBoardID"] = dt.Rows[0]["UserID"].ToString();
                                    Session["PingBoardName"] = dt.Rows[0]["UserName"].ToString();
                                    Session["PingBoardStatus"] = dt.Rows[0]["UserStatus"].ToString();
                                    userName.Text = dt.Rows[0]["UserName"].ToString();
                                    uID.Value = BoardLogin.getid();
                                    user1.Visible = true;
                                    regis.Visible = false;
                                    showResult("Registration Completed! Now Create Board", "success");
                                }
                            }
                        }
                        else
                        {
                            Session["PingBoardID"] = twUser.Rows[0]["UserID"].ToString();
                            Session["PingBoardName"] = twUser.Rows[0]["UserName"].ToString();
                            Session["PingBoardStatus"] = twUser.Rows[0]["UserStatus"].ToString();
                            userName.Text = twUser.Rows[0]["UserName"].ToString();
                            DataTable dt1 = BoardAccess.GetBoard("0", twUser.Rows[0]["UserID"].ToString(), "GetAllByUser", "True", "True","0");
                            if (dt1.Rows.Count > 0)
                            {
                                Session["ID"] = dt1.Rows[0]["BoardID"].ToString();
                                Session["PF"] = "FTI";
                                Response.Redirect("Home.aspx");
                                
                            }
                        }
                    }
                    Response.Redirect("Default.aspx?reg=Success");
                }
            }
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (BoardAccess.InsertUser("0", crName.Text.Trim(), "", crEmail.Text.Trim(), "Standard", crPass.Text.Trim(),
            "", "", "", "", "Insert"))
        {
            DataTable dt = BoardAccess.GetUser("0", crEmail.Text.Trim(), "", "CheckUser");
            if (dt.Rows.Count > 0)
            {
                Session["PingBoardID"] = dt.Rows[0]["UserID"].ToString();
                Session["PingBoardName"] = dt.Rows[0]["UserName"].ToString();
                Session["PingBoardStatus"] = dt.Rows[0]["UserStatus"].ToString();
                userName.Text = dt.Rows[0]["UserName"].ToString();
                uID.Value = BoardLogin.getid();
                user1.Visible = true;
                regis.Visible = false;
                crEmail.Text = "";
                crName.Text = "";
                crPass.Text = "";
                showResult("Registration Completed! Now Create Board", "success");
                checkBoard();
            }
        }
        else { showResult("Got Some Error! Registration Fail", "error"); }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        DataTable dt = BoardAccess.GetUser("0", userEmail.Text.Trim(), userPass.Text.Trim(), "CheckLogin");
        if (dt.Rows.Count > 0)
        {
            Session["PingBoardID"] = dt.Rows[0]["UserID"].ToString();
            Session["PingBoardName"] = dt.Rows[0]["UserName"].ToString();
            Session["PingBoardStatus"] = dt.Rows[0]["UserStatus"].ToString();
            userName.Text = dt.Rows[0]["UserName"].ToString();
            user1.Visible = true;
            regis.Visible = false;
            userEmail.Text = "";
            userPass.Text = "";
            uID.Value = BoardLogin.getid();
            hideResult();
            checkBoard();
            if (!topResult.Visible)
            {
                DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetAllByUser", "True", "True", "0");
                if (dt1.Rows.Count > 0)
                {
                    Session["ID"] = dt1.Rows[0]["BoardID"].ToString();
                    Session["PF"] = "FTI";
                    Session["Created"] = "NO";
                    Response.Redirect("Home.aspx");                    
                }
            }
        }
        else { showResult("Kindly check Login Details", "error"); }
    }
    protected void checkBoard()
    {
        string val = pBoard.Value;
        if (val == "Yes")
        {
            TextBox txtBoard = (TextBox)ContentPlaceHolder1.FindControl("txtBoard");
            if (txtBoard != null)
            {
                string txt = txtBoard.Text.Trim();
                txt = BoardLink.PrepareUserText(txt);
                string stat = BoardLogin.getuserstat();
                if (txt != "")
                {
                    if (txt.Length > 2)
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
                                    else { showResult("HashTag Limitation Over", "error"); }
                                }
                                else if (stat == "Standard")
                                {
                                    DataTable dt2 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardDetails", "True", "True", "0");
                                    if (dt2.Rows.Count < 3)
                                    {
                                        creatboard(txt);
                                    }
                                    else { showResult("HashTag Limitation Over", "error"); }
                                }
                                else
                                {
                                    creatboard(txt);
                                }
                            }
                            else { showResult("Submit Valid HashTag", "error"); }
                        }
                        else { showResult("Submit Valid HashTag", "error"); }
                    }
                    else { showResult("Hashtag Should Be Equal Or More than 3 Characters", "error"); }
                }
                else { showResult("Submit Valid HashTag", "error"); }
            }
        }
    }
    protected void Link1_Click(object sender, EventArgs e)
    {
        BoardLogin.logoutwebsite();
    }
    protected void Link2_Click(object sender, EventArgs e)
    {
        string authurl = HttpContext.Current.Request.Url.AbsoluteUri;
        if (authurl.Contains("?")) { authurl = authurl.Substring(0, authurl.IndexOf('?')); }

        Session["code"] = "facebook";
        Response.Redirect("https://www.facebook.com/dialog/oauth?" +
               "client_id=" + BoardConfig.FbAppKey +
               "&redirect_uri=" + authurl +
               "&scope=" + BoardConfig.FbScope);
    }
    protected void Link3_Click(object sender, EventArgs e)
    {
        string authurl = HttpContext.Current.Request.Url.AbsoluteUri;
        if (authurl.Contains("?")) { authurl = authurl.Substring(0, authurl.IndexOf('?')); }
        //string val = pBoard.Value;
        //if (val == "Yes")
        //{
        //    TextBox txtBoard = (TextBox)ContentPlaceHolder1.FindControl("txtBoard");
        //    if (txtBoard != null)
        //    {
        //        authurl = authurl + "?ID=" + txtBoard.Text.Trim();
        //    }
        //}
        var applicationCredentials = CredentialsCreator.GenerateApplicationCredentials(BoardConfig.ConsumerKey, BoardConfig.ConsumerSecret);
        var url = CredentialsCreator.GetAuthorizationURLForCallback(applicationCredentials, authurl);
        Session["akey"] = applicationCredentials.AuthorizationKey;
        Session["aSecret"] = applicationCredentials.AuthorizationSecret;
        Response.Redirect(url);
        //showResult(authurl);
    }
    protected void setCredentials(string aToken, string aSecret)
    {
        TwitterCredentials.Credentials = TwitterCredentials.CreateCredentials(
            aToken,
            aSecret,
            BoardConfig.ConsumerKey,
            BoardConfig.ConsumerSecret);
    }
    protected void showResult(string Msg, string cls)
    {
        topResult.Visible = true;
        topResult.Attributes.Add("class", "error-area " + cls);
        topResult.InnerText = Msg;
    }
    protected void hideResult()
    {
        topResult.Visible = false;
    }
    protected void creatboard(string txt)
    {
        if (txt.Length > 2)
        {
            DataTable dt = BoardAccess.GetBoard(txt, BoardLogin.getid(), "GetBoard", "True", "True", "0");
            if (dt.Rows.Count == 0)
            {
                string platform = "Facebook,Twitter,Instagram";
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
                    else { showResult("Hashtag Not Created! Inform Administrator", "error"); }
                }
                else { showResult("Hashtag Not Created!", "error"); }
            }
            else
            {
                Session["ID"] = dt.Rows[0]["BoardID"].ToString();
                Session["PF"] = "FTI";
                Session["Created"] = "NO";
                Response.Redirect("Home.aspx");
            }
        }
        else { showResult("Hashtag Should Be Equal Or More than 3 Characters", "error"); }
        
    }
    protected void HomePage_Click(object sender, EventArgs e)
    {
        DataTable dt = BoardAccess.GetBoard("", BoardLogin.getid(), "GetTop", "True", "True", "0");
        if (dt.Rows.Count > 0)
        {
            string ID = Convert.ToString(dt.Rows[0]["BoardID"]);
            if (ID != null)
            {
                Session["ID"] = ID;
                Response.Redirect("Home.aspx");
            }
            else { showResult("NO Board is Active", "error"); }
        }
        else { showResult("No BoardCreated", "error"); }
    }
}
