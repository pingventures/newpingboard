using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

public partial class Analytic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BoardLogin.checkMasterlogin();
        if (!IsPostBack)
        {
            string ID = (string)Session["ID"];
            string pf = (string)Session["PF"];
            string pt = (string)Session["PT"];
            //string totpost = "";
            //int uniqueuser = 0;
            //int post=0;

            if (ID != null)
            {
                DataTable dt1 = BoardAccess.GetBoard("0", BoardLogin.getid(), "GetBoardID", "True","True",ID);
                if (dt1.Rows.Count > 0)
                {
                    if (pf == null) { pf = "FTI"; }
                    if (pt == null) { pt = "PVT"; }

                   
                    //DataTable dt = BoardLogic.GetData(ID, pf, pt);
                    //DataView dv = new DataView(dt);
                    //int res = dt.Rows.Count;
                    //dv.RowFilter = "ContPlatform = 'Twitter'";
                    //plat1.Value = (BoardAccess.GetContent(ID, "0", "Twitter", "GetByPlatform")).Rows.Count.ToString();
                    //plat1l.Text = (BoardAccess.GetContent(ID, "0", "Twitter", "GetByPlatform")).Rows.Count.ToString();
                    ////dv.RowFilter = "ContPlatform = 'Facebook'";
                    //plat2.Value = (BoardAccess.GetContent(ID, "0", "Facebook", "GetByPlatform")).Rows.Count.ToString();
                    //plat2l.Text = (BoardAccess.GetContent(ID, "0", "Facebook", "GetByPlatform")).Rows.Count.ToString();
                    ////dv.RowFilter = "ContPlatform = 'Instagram'";
                    //plat3.Value = (BoardAccess.GetContent(ID, "0", "Instagram", "GetByPlatform")).Rows.Count.ToString();
                    //plat3l.Text = (BoardAccess.GetContent(ID, "0", "Instagram", "GetByPlatform")).Rows.Count.ToString();
                    //dv.RowFilter = "ContMsgType = 'Photo'";
                    //fm1.Value = (BoardAccess.GetContent(ID, "0", "Photo", "GetByType")).Rows.Count.ToString();
                    //fm1l.Text = (BoardAccess.GetContent(ID, "0", "Photo", "GetByType")).Rows.Count.ToString();
                    ////dv.RowFilter = "ContMsgType = 'Video'";
                    //fm2.Value = (BoardAccess.GetContent(ID, "0", "Video", "GetByType")).Rows.Count.ToString();
                    //fm2l.Text = (BoardAccess.GetContent(ID, "0", "Video", "GetByType")).Rows.Count.ToString();
                    ////dv.RowFilter = "ContMsgType = 'Content'";
                    //fm3.Value = (BoardAccess.GetContent(ID,"0", "Content", "GetByType")).Rows.Count.ToString();
                    //fm3l.Text = (BoardAccess.GetContent(ID, "0", "Content", "GetByType")).Rows.Count.ToString();
                    
                    //chartgp.Value = "d";
                    //DataTable dt = BoardAccess.GetContent(ID, "0", "0", "GetAnalyticContent");
                    //string retString = "";
                    //string values = "";
                    //int count = 0;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    string msg = dt.Rows[i]["ContMsg"].ToString().Trim();
                    //    string[] msgs = msg.Split(' ');
                    //    foreach (string ms in msgs)
                    //    {
                    //        if (ms.Contains('#'))
                    //        {
                    //            string newms = ms.Substring(1, ms.Length - 1);
                    //            if (newms.Contains('#')) { newms = newms.Replace("#", ""); }
                    //            if (!ID.ToLower().Contains(newms.ToLower()))
                    //            {
                    //                if (!values.ToLower().Contains(newms.ToLower()))
                    //                {
                    //                    count++;
                    //                    if (count < 10)
                    //                    {
                    //                        values += newms + ",";
                    //                        retString += "<tr><td>" + newms + "</td></tr>";
                    //                        //td.InnerText = newms;
                    //                        //tr.Controls.Add(td);
                    //                        //AnotherTags.Controls.Add(tr);
                    //                    }
                    //                    else {
                    //                        break;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    //DataTable dt2 = BoardAccess.GetContent(ID, "0", "0", "GetPost");
                    //if (dt2.Rows.Count > 0)
                    //{                        
                    //    for (int i = 0; i < dt2.Rows.Count; i++)
                    //    {
                    //        DateTime Condatetime = Convert.ToDateTime(dt2.Rows[i]["Condate"]);
                    //        string cDate = Condatetime.ToString("dd MMM");
                    //        totpost = totpost + Convert.ToString(dt2.Rows[i]["posts"]) + "-" + cDate + ",";
                    //        post = post + Convert.ToInt32(dt2.Rows[i]["posts"]);
                    //    }
                    //    totpost = totpost.Substring(0, totpost.Length - 1);
                    //    chartgp.Value = totpost;
                    //}                    

                    //totalpost.Value = post.ToString();
                    //uniqueuser = uniqueuser + (BoardAccess.GetContent(ID, "0", "Twitter", "GetUniqueUser")).Rows.Count
                    //    + (BoardAccess.GetContent(ID, "0", "Facebook", "GetUniqueUser")).Rows.Count
                    //    + (BoardAccess.GetContent(ID, "0", "Instagram", "GetUniqueUser")).Rows.Count;
                    //totuni.Value = uniqueuser.ToString();
                    //TwitterUniUser.Value =(BoardAccess.GetContent(ID, "0", "Twitter", "GetUniqueUser")).Rows.Count.ToString();
                    //TwitterUniUserl.Text = (BoardAccess.GetContent(ID, "0", "Twitter", "GetUniqueUser")).Rows.Count.ToString();
                    //FbUniUser.Value = (BoardAccess.GetContent(ID, "0", "Facebook", "GetUniqueUser")).Rows.Count.ToString();
                    //FbUniUserl.Text = (BoardAccess.GetContent(ID, "0", "Facebook", "GetUniqueUser")).Rows.Count.ToString();
                    //InstUniUser.Value =(BoardAccess.GetContent(ID, "0", "Instagram", "GetUniqueUser")).Rows.Count.ToString();
                    //InstUniUserl.Text = (BoardAccess.GetContent(ID, "0", "Instagram", "GetUniqueUser")).Rows.Count.ToString();
                    //sentiment(ID, "Twitter");
                    //sentiment(ID, "Facebook");
                    //sentiment(ID, "Instagram");
                }
            }
        }
    }   
    //protected void sentiment(string ID,string platform)
    //{
    //    string twpost = "",twneg="",twneu="";
    //    string facepost = "", faceneg = "", faceneu = "";
    //    string instpost = "", instneg = "", instneu = "";
    //    if (platform == "Twitter")
    //    {
    //        //DataTable dt1 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentPositive");
    //        //if (dt1.Rows.Count > 0)
    //        //{
    //        //    for (int i = 0; i < dt1.Rows.Count; i++)
    //        //    {
    //        //        DateTime Condatetime = Convert.ToDateTime(dt1.Rows[i]["ContDate"]);
    //        //        string cDate = Condatetime.ToString("dd MMM");
    //        //        twpost = twpost + Convert.ToString(dt1.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //        //    }
    //        //    twpost = twpost.Substring(0, twpost.Length - 1);
    //        //    twitterpost.Value = twpost;
    //        //}
    //        //DataTable dt2 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNegative");
    //        //if (dt2.Rows.Count > 0)
    //        //{                
    //        //    for (int i = 0; i < dt2.Rows.Count; i++)
    //        //    {
    //        //        DateTime Condatetime = Convert.ToDateTime(dt2.Rows[i]["ContDate"]);
    //        //        string cDate = Condatetime.ToString("dd MMM");
    //        //        twneg = twneg + Convert.ToString(dt2.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //        //    }
    //        //    twneg = twneg.Substring(0, twneg.Length - 1);
    //        //    twitterneg.Value = twneg;
    //        //}
    //        //DataTable dt3 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNeutral");
    //        //if (dt3.Rows.Count > 0)
    //        //{                
    //        //    for (int i = 0; i < dt3.Rows.Count; i++)
    //        //    {
    //        //        DateTime Condatetime = Convert.ToDateTime(dt3.Rows[i]["ContDate"]);
    //        //        string cDate = Condatetime.ToString("dd MMM");
    //        //        twneu = twneu + Convert.ToString(dt3.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //        //    }
    //        //    twneu = twneu.Substring(0, twneu.Length - 1);
    //        //    twitterneu.Value = twneu;
    //        //}
    //    }
    //    if (platform == "Facebook")
    //    {
    //        DataTable dt1 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentPositive");
    //        if (dt1.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt1.Rows.Count; i++)
    //            {
    //                DateTime Condatetime = Convert.ToDateTime(dt1.Rows[i]["ContDate"]);
    //                string cDate = Condatetime.ToString("dd MMM");
    //                facepost = facepost + Convert.ToString(dt1.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //            }
    //            facepost = facepost.Substring(0, facepost.Length - 1);
    //            fbpost.Value = facepost;
    //        }
    //        DataTable dt2 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNegative");
    //        if (dt2.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt2.Rows.Count; i++)
    //            {
    //                DateTime Condatetime = Convert.ToDateTime(dt2.Rows[i]["ContDate"]);
    //                string cDate = Condatetime.ToString("dd MMM");
    //                faceneg = faceneg + Convert.ToString(dt2.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //            }
    //            faceneg = faceneg.Substring(0, faceneg.Length - 1);
    //            fbneg.Value = faceneg;
    //        }
    //        DataTable dt3 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNeutral");
    //        if (dt3.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt3.Rows.Count; i++)
    //            {
    //                DateTime Condatetime = Convert.ToDateTime(dt3.Rows[i]["ContDate"]);
    //                string cDate = Condatetime.ToString("dd MMM");
    //                faceneu = faceneu + Convert.ToString(dt3.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //            }
    //            faceneu = faceneu.Substring(0, faceneu.Length - 1);
    //            fbneu.Value = faceneu;
    //        }
    //    }
    //    if (platform == "Instagram")
    //    {
    //        DataTable dt1 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentPositive");
    //        if (dt1.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt1.Rows.Count; i++)
    //            {
    //                DateTime Condatetime = Convert.ToDateTime(dt1.Rows[i]["ContDate"]);
    //                string cDate = Condatetime.ToString("dd MMM");
    //                instpost = instpost + Convert.ToString(dt1.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //            }
    //            instpost = instpost.Substring(0, instpost.Length - 1);
    //            inpost.Value = instpost;
    //        }
    //        DataTable dt2 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNegative");
    //        if (dt2.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt2.Rows.Count; i++)
    //            {
    //                DateTime Condatetime = Convert.ToDateTime(dt2.Rows[i]["ContDate"]);
    //                string cDate = Condatetime.ToString("dd MMM");
    //                instneg = instneg + Convert.ToString(dt2.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //            }
    //            instneg = instneg.Substring(0, instneg.Length - 1);
    //            inneg.Value = instneg;
    //        }
    //        DataTable dt3 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNeutral");
    //        if (dt3.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt3.Rows.Count; i++)
    //            {
    //                DateTime Condatetime = Convert.ToDateTime(dt3.Rows[i]["ContDate"]);
    //                string cDate = Condatetime.ToString("dd MMM");
    //                instneu = instneu + Convert.ToString(dt3.Rows[i]["sentiment"]) + "-" + cDate + ",";
    //            }
    //            instneu = instneu.Substring(0, instneu.Length - 1);
    //            inneu.Value = instneu;
    //        }
    //    }
        
    //}
    [WebMethod]
    public static int AbandonSession()
    {
        HttpContext.Current.Session.Abandon();
        return 1;
    }
    [WebMethod]
    public static string canvas1()
    {
        string ID = (string)HttpContext.Current.Session["ID"];
        string s = "";
        string ct = (BoardAccess.GetContent(ID, "0","Twitter", "GetByPlatform")).Rows.Count.ToString();        
        s = s + ct + "-";
        ct = (BoardAccess.GetContent(ID, "0", "Facebook", "GetByPlatform")).Rows.Count.ToString();
        s = s + ct + "-";
        ct = (BoardAccess.GetContent(ID, "0", "Instagram", "GetByPlatform")).Rows.Count.ToString();
        s = s + ct;
        return s;
    }
    [WebMethod]
    public static string canvas2()
    {
        string ID = (string)HttpContext.Current.Session["ID"];
        string s = "";
        string ct = (BoardAccess.GetContent(ID, "0", "Photo", "GetByType")).Rows.Count.ToString();
        s = s + ct + "-";
        ct = (BoardAccess.GetContent(ID, "0", "Video", "GetByType")).Rows.Count.ToString();
        s = s + ct + "-";
        ct = (BoardAccess.GetContent(ID, "0", "Content", "GetByType")).Rows.Count.ToString();
        s = s + ct;
        return s;
    }
    [WebMethod]
    public static string canvas3()
    {
        //System.Threading.Thread.Sleep(5000);
        string ID = (string)HttpContext.Current.Session["ID"];
        string s = "";
        string ct = (BoardAccess.GetContent(ID, "0", "Twitter", "GetUniqueUser")).Rows.Count.ToString();
        s = s + ct + "-";
        ct = (BoardAccess.GetContent(ID, "0", "Facebook", "GetUniqueUser")).Rows.Count.ToString();
        s = s + ct + "-";
        ct = (BoardAccess.GetContent(ID, "0", "Instagram", "GetUniqueUser")).Rows.Count.ToString();
        s = s + ct;
        return s;
    }
    //Total post and unique user
    [WebMethod]
    public static string canvas4()
    {
        //System.Threading.Thread.Sleep(5000);
        string ID = (string)HttpContext.Current.Session["ID"];
        string s = "";
        DataTable dt = BoardAccess.GetContent(ID, "0", "0", "GetAnalyticContent");
        int totpost = dt.Rows.Count;
        int uni = (BoardAccess.GetContent(ID, "0", "Twitter", "GetUniqueUser")).Rows.Count;                
        uni += (BoardAccess.GetContent(ID, "0", "Facebook", "GetUniqueUser")).Rows.Count;        
        uni += (BoardAccess.GetContent(ID, "0", "Instagram", "GetUniqueUser")).Rows.Count;
        s = uni + "-" + totpost;
        return s;
    }
    //Another Hash Tag
    [WebMethod]
    public static string AnoHash()
    {
        //System.Threading.Thread.Sleep(5000);
        string ID = (string)HttpContext.Current.Session["ID"];
        DataTable dt = BoardAccess.GetContent(ID, "0", "0", "GetAnalyticContent");
        string retString = "";
        string values = "";
        int count = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string msg = dt.Rows[i]["ContMsg"].ToString().Trim();
            string[] msgs = msg.Split(' ');
            foreach (string ms in msgs)
            {
                if (ms.Contains('#'))
                {
                    string newms = ms.Substring(1, ms.Length - 1);
                    if (newms.Contains('#')) { newms = newms.Replace("#", ""); }
                    if (!ID.ToLower().Contains(newms.ToLower()))
                    {
                        if (!values.ToLower().Contains(newms.ToLower()))
                        {
                            count++;
                            if (count < 10)
                            {
                                values += newms + ",";
                                retString += "<tr><td>" + newms + "</td></tr>";
                                //td.InnerText = newms;
                                //tr.Controls.Add(td);
                                //AnotherTags.Controls.Add(tr);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        return retString;
    }
    //Positive Sentiments
    [WebMethod]
    public static string positive(string platform)
    {
        string post = "";
        string ID = (string)HttpContext.Current.Session["ID"];
        DataTable dt1 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentPositive");
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                DateTime Condatetime = Convert.ToDateTime(dt1.Rows[i]["ContDate"]);
                string cDate = Condatetime.ToString("dd MMM");
                post = post + Convert.ToString(dt1.Rows[i]["sentiment"]) + "-" + cDate + ",";
            }
            post = post.Substring(0, post.Length - 1);
            //twitterpost.Value = twpost;
        }
        return post;        
    }
    //Negative Sentiments
    [WebMethod]
    public static string negative(string platform)
    {
        string neg = "";
        string ID = (string)HttpContext.Current.Session["ID"];        
        DataTable dt2 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNegative");
        if (dt2.Rows.Count > 0)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DateTime Condatetime = Convert.ToDateTime(dt2.Rows[i]["ContDate"]);
                string cDate = Condatetime.ToString("dd MMM");
                neg = neg + Convert.ToString(dt2.Rows[i]["sentiment"]) + "-" + cDate + ",";
            }
            neg = neg.Substring(0, neg.Length - 1);
            //twitterneg.Value = twneg;
        }
        return neg;        
    }
    //Neutral Sentiments
    [WebMethod]
    public static string neutral(string platform)
    {
        string neu = "";
        string ID = (string)HttpContext.Current.Session["ID"];        
        DataTable dt3 = BoardAccess.GetContent(ID, "0", platform, "GetSentimentNeutral");
        if (dt3.Rows.Count > 0)
        {
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                DateTime Condatetime = Convert.ToDateTime(dt3.Rows[i]["ContDate"]);
                string cDate = Condatetime.ToString("dd MMM");
                neu = neu + Convert.ToString(dt3.Rows[i]["sentiment"]) + "-" + cDate + ",";
            }
            neu = neu.Substring(0, neu.Length - 1);            
        }
        return neu;
    }   
    //post volume per day
    [WebMethod]
    public static string chartgrp()
    {
        string ID = (string)HttpContext.Current.Session["ID"];
        string totpost = "";
        DataTable dt2 = BoardAccess.GetContent(ID, "0", "0", "GetPost");
        if (dt2.Rows.Count > 0)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DateTime Condatetime = Convert.ToDateTime(dt2.Rows[i]["Condate"]);
                string cDate = Condatetime.ToString("dd MMM");
                totpost = totpost + Convert.ToString(dt2.Rows[i]["posts"]) + "-" + cDate + ",";                
            }
            totpost = totpost.Substring(0, totpost.Length - 1);
            return totpost;
        }
        else        
        return totpost;
    }   

}