using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Dynamic;
using Newtonsoft.Json.Linq;
using Facebook;
using System.Collections.Generic;
using System.Net;

/// <summary>
/// Summary description for login
/// </summary>
public static class BoardLogin
{
	static BoardLogin()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //return session object as string
    public static string getid()
    {
        //the value to be returned
        string id = "";
        object obj = HttpContext.Current.Session["PingBoardID"];
        if (obj == null)
            return id; //return defualt value
        else
            return (string)obj; //return Session value

    }
    //return session object as string
    public static string getname()
    {
        //the value to be returned
        string name = "";
        object obj = HttpContext.Current.Session["PingBoardName"];
        if (obj == null)
            return name; //return defualt value
        else
            return (string)obj; //return Session value
    }
    //return session object as string
    public static string getuserstat()
    {
        //the value to be returned
        string name = "";
        object obj = HttpContext.Current.Session["PingBoardStatus"];
        if (obj == null)
            return name; //return defualt value
        else
            return (string)obj; //return Session value
    }
    //return session object as string
    public static void checkMasterlogin()
    {
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (HttpContext.Current.Session["PingBoardID"] == null)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect("Default.aspx?session=fail");
        }
    }
    //logout from website
    public static void logoutwebsite()
    {
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Response.Redirect("Default.aspx?session=logout");
    }
    public static string RefreshAccessToken(string currentAccessToken)
    {
        FacebookClient fbClient = new FacebookClient();
        Dictionary<string, object> fbParams = new Dictionary<string, object>();
        fbParams["client_id"] = BoardConfig.FbAppKey;
        fbParams["grant_type"] = "fb_exchange_token";
        fbParams["client_secret"] = BoardConfig.FbAppSecret;
        fbParams["fb_exchange_token"] = currentAccessToken;
        JsonObject publishedResponse = fbClient.Get("/oauth/access_token", fbParams) as JsonObject;
        return publishedResponse["access_token"].ToString();
    }
    public static string ExchangeCode(string code, string url)
    {
        FacebookClient client = new FacebookClient();
        dynamic result = client.Get("oauth/access_token", new
        {
            client_id = BoardConfig.FbAppKey,
            redirect_uri = url,
            client_secret = BoardConfig.FbAppSecret,
            code = code
        });
        return result.access_token;
    }
    public static JArray GetFBSearch(string userAccessToken, string url)
    {
        FacebookClient fbClient = new FacebookClient();
        fbClient.AppId = BoardConfig.FbAppKey;
        fbClient.AppSecret = BoardConfig.FbAppSecret;
        fbClient.AccessToken = userAccessToken;
        Dictionary<string, object> fbParams = new Dictionary<string, object>();
        JsonObject publishedResponse = fbClient.Get(url, fbParams) as JsonObject;
        JArray data = JArray.Parse(publishedResponse["data"].ToString());
        return data;
    }
    public static dynamic GetFBData(string userAccessToken, string url)
    {
        FacebookClient fbClient = new FacebookClient();
        fbClient.AppId = BoardConfig.FbAppKey;
        fbClient.AppSecret = BoardConfig.FbAppSecret;
        fbClient.AccessToken = userAccessToken;
        Dictionary<string, object> fbParams = new Dictionary<string, object>();
        dynamic data = fbClient.Get(url, fbParams);
        return data;
    }
    public static string GetPictureUrl(string fbId)
    {
        WebResponse response = null;
        string pictureUrl = string.Empty;
        try
        {
            WebRequest request = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture", fbId));
            response = request.GetResponse();
            pictureUrl = response.ResponseUri.ToString();
        }
        catch (Exception ex)
        {
            //? handle
        }
        finally
        {
            if (response != null) response.Close();
        }
        return pictureUrl;
    }
}