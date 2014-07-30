using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Dynamic;
using Facebook;
using Newtonsoft.Json.Linq;
using Tweetinvi;

public partial class Addsocial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BoardLogin.checkMasterlogin();
        if (!IsPostBack)
        {
            string ID = (string)Session["ID"];
            if (Request.QueryString["code"] != null)
            {
                if (Convert.ToString(Session["code"]) == "facebook")
                {
                    string authurl = HttpContext.Current.Request.Url.AbsoluteUri;
                    if (authurl.Contains("?")) { authurl = authurl.Substring(0, authurl.IndexOf('?')); }

                    string token = BoardLogin.ExchangeCode(Request.QueryString["code"].ToString(), authurl);
                    string ntoken = BoardLogin.RefreshAccessToken(token);
                    DataTable tt = BoardAccess.GetBoard("", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                    if (tt.Rows.Count > 0)
                    {
                        string bID = tt.Rows[0]["BoardID"].ToString();
                        DataTable dt = BoardAccess.GetBoardSocial(bID, "Facebook", "GetBoardSocial");
                        if (dt.Rows.Count == 0)
                        {
                            BoardAccess.InsertBoardSocial(bID, "Facebook", ntoken, "", "", "Insert");
                        }
                        else { BoardAccess.InsertBoardSocial(bID, "Facebook", ntoken, "", "", "Update"); }
                    }
                    Response.Redirect("AddSocial.aspx");
                }
            }
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
                    //setCredentials(newCredentials.AccessToken, newCredentials.AccessTokenSecret);
                    DataTable tt = BoardAccess.GetBoard("", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
                    if (tt.Rows.Count > 0)
                    {
                        string bID = tt.Rows[0]["BoardID"].ToString();
                        DataTable dt = BoardAccess.GetBoardSocial(bID, "Twitter", "GetBoardSocial");
                        if (dt.Rows.Count == 0)
                        {
                            BoardAccess.InsertBoardSocial(bID, "Twitter", newCredentials.AccessToken, 
                                newCredentials.AccessTokenSecret, "", "Insert");
                        }
                        else { BoardAccess.InsertBoardSocial(bID, "Twitter", newCredentials.AccessToken, 
                            newCredentials.AccessTokenSecret, "", "Update"); }
                    }
                    Response.Redirect("AddSocial.aspx");
                }
            }
            DataTable dt1 = BoardAccess.GetBoard("", BoardLogin.getid(), "GetBoardID", "True", "True",ID);
            if (dt1.Rows.Count > 0)
            {
                string bID = dt1.Rows[0]["BoardID"].ToString();
                DataTable dt = BoardAccess.GetBoardSocial(bID, "Twitter", "GetBoardSocial");
                if (dt.Rows.Count > 0)
                {
                    //setCredentials(dt.Rows[0]["AccessToken"].ToString(), dt.Rows[0]["AccessSecret"].ToString());
                    var credentials = TwitterCredentials.CreateCredentials(dt.Rows[0]["AccessToken"].ToString(), dt.Rows[0]["AccessSecret"].ToString(), BoardConfig.ConsumerKey, BoardConfig.ConsumerSecret);
                    TwitterCredentials.ExecuteOperationWithCredentials(credentials, () =>
                    {
                        var user = Tweetinvi.User.GetLoggedUser();
                        if (user != null)
                        {
                            twit.Text = "Current Twitter Account : <strong>" + user.ScreenName + "</strong>";
                            twit.Visible = true;
                        }
                        else { Res.Text = "Twitter having problem giving details right now."; }
                    });
                }
                else {
                    twit.Visible = false;
                }
                dt = BoardAccess.GetBoardSocial(bID, "Facebook", "GetBoardSocial");
                if (dt.Rows.Count > 0)
                {
                    fbPages.Visible = true;
                    Button3.Visible = true;
                    JArray data = BoardLogin.GetFBSearch(dt.Rows[0]["AccessToken"].ToString(), "/me/accounts");
                    foreach (var account in data)
                    {
                        fbPages.Items.Add(new ListItem(account["name"].ToString(), account["id"].ToString()));
                    }
                    fbPages.Items.Insert(0, "--Select Page--");
                    if (dt.Rows[0]["fbPage"].ToString() != "")
                    {
                        fbPages.SelectedValue = dt.Rows[0]["fbPage"].ToString();
                    }
                }
                else {
                    fbPages.Visible = false;
                    Button3.Visible = false;
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        setSession();
        var applicationCredentials = CredentialsCreator.GenerateApplicationCredentials(BoardConfig.ConsumerKey, BoardConfig.ConsumerSecret);
        var url = CredentialsCreator.GetAuthorizationURLForCallback(applicationCredentials, HttpContext.Current.Request.Url.AbsoluteUri);
        Session["akey"] = applicationCredentials.AuthorizationKey;
        Session["aSecret"] = applicationCredentials.AuthorizationSecret;
        Response.Redirect(url);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        setSession();
        string authurl = HttpContext.Current.Request.Url.AbsoluteUri;
        if (authurl.Contains("?")) { authurl = authurl.Substring(0, authurl.IndexOf('?')); }
        Session["code"] = "facebook";
        Response.Redirect("https://www.facebook.com/dialog/oauth?" +
               "client_id=" + BoardConfig.FbAppKey +
               "&redirect_uri=" + authurl +
               "&scope=publish_actions,manage_pages,read_insights,read_stream");
    }
    protected void setSession()
    {
        string ID = (string)Session["ID"];
        string pf = (string)Session["PF"];
        string pt = (string)Session["PT"];
        Session["ID"] = ID;
        Session["PF"] = pf;
        Session["PT"] = pt;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (fbPages.SelectedValue != "--Select Page--")
        {
            string ID = (string)Session["ID"];
            DataTable dt = BoardAccess.GetBoard("", BoardLogin.getid(), "GetBoardID", "True", "True", ID);
            if (dt.Rows.Count > 0)
            {
                string bID = dt.Rows[0]["BoardID"].ToString();
                DataTable dt1 = BoardAccess.GetBoardSocial(bID, "Facebook", "GetBoardSocial");
                if (dt1.Rows.Count > 0)
                {
                    BoardAccess.InsertBoardSocial(bID, "Facebook", dt1.Rows[0]["AccessToken"].ToString(), "", 
                        fbPages.SelectedValue, "UpdateFBPage");
                    Res.Text = "Page Udpated Successfully";
                }
                else
                {
                    Res.Text = "Must Select Page";
                }
            }
        }
    }
}