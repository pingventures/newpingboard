<%@ WebHandler Language="C#" Class="FetchRecordsHandler" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class FetchRecordsHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //Delay to create loading effect.
        //System.Threading.Thread.Sleep(500);
        string lastProductId = Convert.ToString(context.Request.QueryString["lastProductId"]);
        context.Response.Write(GetProductsRows(lastProductId));
    }
    private string GetProductsRows(string lastProductId)
    {
        int lastId = Convert.ToInt32(lastProductId);
        string ID = Convert.ToString(HttpContext.Current.Session["ID"]);
        string pt = Convert.ToString(HttpContext.Current.Session["PT"]);
        string pf = Convert.ToString(HttpContext.Current.Session["PF"]);
        string hash = Convert.ToString(HttpContext.Current.Session["Hash"]);
        StringBuilder sb = new StringBuilder();
        string p1 = "Facebook", p2 = "Twitter", p3 = "Instagram", m1 = "Photo", m2 = "Video", m3 = "Content";
        string f1 = "0", f2 = "0", f3 = "0", f4 = "0";
        if (pf == null || pf == "") { }
        else
        {
            if (pf.Contains("F"))
            {
                f1 = "1";
                p3 = "Facebook";
                p2 = "Facebook";
            }
            if (pf.Contains("T"))
            {
                f2 = "1";
                p2 = "Twitter";
                if (f1 == "0")
                {
                    p1 = "Twitter";
                    p3 = "Twitter";
                }
            }
            if (pf.Contains("I"))
            {
                p3 = "Instagram";
                if (f2 == "0") { p2 = "Instagram"; }
                if (f1 == "0") { p1 = "Instagram"; }
            }
        }
        if (pt == null || pt == "") { }
        else
        {
            if (pt.Contains("P"))
            {
                f3 = "1";
                m2 = "Photo";
                m3 = "Photo";
            }
            if (pt.Contains("V"))
            {
                f4 = "1";
                m2 = "Video";
                if (f3 == "0")
                {
                    m1 = "Video";
                    m3 = "Video";
                }
            }
            if (pt.Contains("T"))
            {
                m3 = "Content";
                if (f4 == "0") { m2 = "Content"; }
                if (f3 == "0") { m1 = "Content"; }
            }
        }
        DataTable dt = new DataTable();
        if (hash == null || hash == "")
            dt = BoardAccess.GetSortContent(ID, "GetSortContent", lastId + 1, 5, p1, p2, p3, m1, m2, m3);
        else
            dt = BoardAccess.GetSortBoard(ID, "GetSortContent", lastId + 1, 5, hash, p1, p2, p3, m1, m2, m3);
        if (dt.Rows.Count > 0)
        {
            //dt = GetDataView(dt);
            int ct = dt.Rows.Count;
            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                string cLink = dt.Rows[rowIndex]["ContLink"].ToString();
                string cPlat = dt.Rows[rowIndex]["ContPlatform"].ToString();
                //string[] spitplatform = pf.Split('');
               //bool platform= cPlat.Substring(0, 1).Contains(pf);
                string cMsg = dt.Rows[rowIndex]["ContMsgType"].ToString();
                lastId += rowIndex;
                sb.Append("<div class='panel'>");
                sb.Append("<input name='ctl00$ContentPlaceHolder1$Data1$ctl01$cID' type='hidden' id='ContentPlaceHolder1_Data1_cID_" + lastId + "' value='" + dt.Rows[rowIndex]["ContID"].ToString() + "'>");
                sb.Append("<input name='ctl00$ContentPlaceHolder1$Data1$ctl01$cMsg' type='hidden' id='ContentPlaceHolder1_Data1_cMsg_" + lastId + "' value='Test Text'>");
                sb.Append("<input name='ctl00$ContentPlaceHolder1$Data1$ctl01$cUID' type='hidden' id='ContentPlaceHolder1_Data1_cUID_" + lastId + "' value='" + dt.Rows[rowIndex]["ContUserID"].ToString() + "'>");
                sb.Append("<div id='ContentPlaceHolder1_Data1_topData_" + lastId + "'>");
                if (cMsg == "Photo")
                {
                    sb.Append("<img width='252px' height='252px' alt='" + cPlat + "' src='" + cLink + "'>");
                }
                if (cMsg == "Video")
                {
                    if (cLink.Contains("vine")) {
                        sb.Append("<iframe src='" + cLink + "/card' width='200' height='200' frameborder='0'></iframe>");
                    }
                    else if (cLink.Contains("twitrpix")) {
                        sb.Append("");
                    }
                    else {
                        sb.Append("<a target='_blank' href='" + cLink + "'><img alt='video' width='100%' src='images/video-player.jpg' /></a>");
                    }
                }
                string iCon = "";
                if (cPlat == "Twitter")
                {
                    iCon = "images/twitter-corner-icon.png";
                }
                if (cPlat == "Facebook")
                {
                    iCon = "images/facebook-corner-icon.png";
                }
                if (cPlat == "Instagram")
                {
                    iCon = "images/instagram-corner-icon.png";
                }
                if (cPlat == "Pinterest")
                {
                    iCon = "images/pintrest-corner-icon.png";
                }
                sb.Append("</div><div class='panel-header'><div class='panel-header-left'>");
                sb.Append("<div class='p-img'>");
                sb.Append("<img id='ContentPlaceHolder1_Data1_ContUserPic_" + lastId + "' src='" + dt.Rows[rowIndex]["ContUserPic"].ToString() + "' alt='" + dt.Rows[rowIndex]["ContUserName"].ToString() + "' style='height:48px;width:48px;'>");
                sb.Append("</div><div class='p-txt'>");
                sb.Append("<span id='ContentPlaceHolder1_Data1_ContDate_" + lastId + "' class='datetxt'>" + Convert.ToDateTime(dt.Rows[rowIndex]["ContDate"]).ToString("dd MMM hh:mm tt") + "</span><br>");
                sb.Append("<b>" + dt.Rows[rowIndex]["ContUserName"].ToString() + "</b><br>");
                sb.Append("<span>");
                if (cPlat != "Facebook")
                {
                    sb.Append("<span id='ContentPlaceHolder1_Data1_ContUserID_" + lastId + "'>" + dt.Rows[rowIndex]["ContUserID"].ToString() + "</span>");
                }
                sb.Append("</span>");
                sb.Append("</div>");
                sb.Append("<div class='corner-icon'>");
                sb.Append("<img id='ContentPlaceHolder1_Data1_ContPlatform_" + lastId + "' src='" + iCon + "' alt='Icon'>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("<div class='panel-mid'>");
                sb.Append("</div>");
                sb.Append("<div class='panel-bottom'>");
                sb.Append("<div class='panel-bottom-left'><img src='images/delete-box-icon.png' id='ContentPlaceHolder1_Data1_delImg_" + lastId + "' style='cursor:pointer' alt='delete-icon'></div>");
                if (cPlat == "Twitter")
                {
                    sb.Append("<div id='ContentPlaceHolder1_Data1_botLayout_" + lastId + "' class='panel-bottom-right'>");
                    sb.Append("<div style='float:left; margin:3px 3px 0px 3px;'> <span id='ContentPlaceHolder1_Data1_L1_" + lastId + "' class='rpTweet'><img src='images/share.png' alt='Reply'></span></div>");
                    //sb.Append("<div id='ContentPlaceHolder1_Data1_botLK_" + lastId + "' class='share__count'>0</div>");
                    sb.Append("<div style='float:left;margin:3px 5px 0px 5px;'> <span id='ContentPlaceHolder1_Data1_L2_" + lastId + "'><img src='images/retweet2.png' alt='Retweet'></span></div>");
                    sb.Append("<div id='ContentPlaceHolder1_Data1_botCM_" + lastId + "' class='share__count'>" + dt.Rows[rowIndex]["ContLike"].ToString() + "</div>");
                    sb.Append("<div style='float:left;margin:3px 5px 0px 5px;'> <span id='ContentPlaceHolder1_Data1_L3_" + lastId + "'><img src='images/favourite.png' alt='Favourite'></span></div>");
                    sb.Append("<div id='ContentPlaceHolder1_Data1_botSH_" + lastId + "' class='share__count'>" + dt.Rows[rowIndex]["ContComment"].ToString() + "</div>");
                    sb.Append("</div>");
                }
                sb.Append("</div>");
                sb.Append("</div>");
            }
        }
        return sb.ToString();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}