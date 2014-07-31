using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Common;
using System.Text.RegularExpressions;

using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tweetinvi;
using Facebook;
using Tweetinvi.Core.Interfaces;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Tweetinvi.Core.Interfaces.Models.Parameters;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text;
using System.Net;


/// <summary>
/// business tier component
/// </summary>
public static class BoardLogic
{
    static BoardLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // Get Days in a month
    public static List<int> DaysInMonth()
    {
        List<int> dList = new List<int>();
        for (int i = 1; i <= 31; i++)
        {
            dList.Add(i);
        }
        return dList;
    }
    // Get Hours in a day
    public static List<string> GetHours()
    {
        List<string> dList = new List<string>();
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
                dList.Add("0" + Convert.ToString(i));
            else
                dList.Add(Convert.ToString(i));
        }
        return dList;
    }
    // Get Minutes in Hour
    public static List<string> GetMinutes()
    {
        List<string> dList = new List<string>();
        for (int i = 0; i < 60; i++)
        {
            if (i < 10)
                dList.Add("0" + Convert.ToString(i));
            else
                dList.Add(Convert.ToString(i));
        }
        return dList;
    }
    // Get Months in a Year
    public static List<string> MonthsInYear()
    {
        string[] mName = { "January", "February", "March", "April", "May", "June", "July", "August", "September", 
                             "October", "November", "December" };
        List<string> dList = new List<string>();
        for (int i = 0; i < mName.Length; i++)
        {
            dList.Add(mName[i]);
        }
        return dList;
    }
    public static string diffdate(DateTime dat)
    {
        string res ="";
        DateTime curr = DateTime.Now;
        double d = (curr - dat).Days;
        res = Convert.ToString(d) + " D";
        if (d < 1) {
            d = (curr - dat).Hours;
            res = Convert.ToString(d) + " H";
            if (d < 1) {
                d = (curr - dat).Minutes;
                res = Convert.ToString(d) + " M";
            }
        }
        return res;
    }
    // Month using Number
    public static string getMonth(int val)
    {
        string[] mName = { "January", "February", "March", "April", "May", "June", "July", "August", "September", 
                             "October", "November", "December" };
        return mName[val - 1];
    }
    // Get Years
    public static List<int> YearsForBirth()
    {
        int dYear = Convert.ToInt32(DateTime.Now.Year.ToString());
        int diffYear = dYear - 89;
        List<int> dList = new List<int>();
        for (int i = dYear; i >= diffYear; i--)
        {
            dList.Add(i);
        }
        return dList;
    }

    //return registration id for new user.
    public static string getregisterid()
    {
        string date = Convert.ToString(DateTime.Today.Month);
        string date2 = Convert.ToString(DateTime.Today.Year);
        string date1 = Convert.ToString(DateTime.Today.Day);
        Random rand = new Random();
        string id = date + date1 + rand.Next(100, 99999) + date2;
        return id;
    }
    //return random Number for passwrd
    public static string getrandomNum()
    {
        Random rand = new Random();
        string id = rand.Next(1000, 99999).ToString();
        return id;
    }
    //return Age from today's date
    public static string CalculateAge(string dob)
    {
        if (dob == "1/1/1900 12:00:00 AM")
        {
            return "Set Your Age.";
        }
        else
        {
            int age = 0;
            if (!string.IsNullOrEmpty(dob))
            {
                DateTime now = DateTime.Today;
                DateTime birthDate = DateTime.Parse(dob);
                age = now.Year - birthDate.Year;
                if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            }
            return age.ToString();
        }
    }
    public static string upFile(FileUpload fuImage, string path, string fName)
    {
        if (fuImage.HasFile)
        {
            string ComfileNames = Path.GetFileName(fuImage.PostedFile.FileName);
            string extension = Path.GetExtension(ComfileNames);
            string ComfileContentTypes = fuImage.PostedFile.ContentType;
            string ComuploadedFilePaths = HttpContext.Current.Server.MapPath(path + fName + extension);
            if (fuImage.PostedFile.ContentLength > 0)
            {
                fuImage.PostedFile.SaveAs(ComuploadedFilePaths);
                return "Success";
            }
            else { return "Invalid File"; }
        }
        else
        {
            return "Must Select File before Upload.";
        }
    }
    public static string upImage(FileUpload brNew, string path, string brFileName, float maxWidth, float minWidth)
    {
        System.Drawing.Image image_file;
        byte[] data = null;
        if (brNew.HasFile)
        {
            string fileName = HttpContext.Current.Server.HtmlEncode(brNew.FileName);
            string extension = Path.GetExtension(fileName);
            if ((extension.ToUpper().ToString() == ".JPG") || (extension.ToUpper().ToString() == ".GIF") ||
                (extension.ToUpper().ToString() == ".JPEG") || (extension.ToUpper().ToString() == ".GIFF"))
            {
                image_file = System.Drawing.Image.FromStream(brNew.PostedFile.InputStream);
                data = sendimage(image_file, maxWidth, minWidth);
                image_file.Dispose();
                if (data != null)
                {
                    string brFullPath = HttpContext.Current.Server.MapPath(path + brFileName + ".jpg");
                    if (File.Exists(brFullPath)) { File.Delete(brFullPath); }
                    try
                    {
                        brFileName += ".jpg";
                        File.WriteAllBytes(HttpContext.Current.Server.MapPath(path + brFileName), data);
                        return "Image Uploaded Successfully";
                    }
                    catch { return "Image Not Uploaded! Error."; }
                }
                else { return "Image is less than Minimum set value! Kindly upload it again."; }
            }
            else { return "Plz upload jpg or gif files Only"; }
        }
        else { return "Image not uploaded."; }
    }
    //Function to send byte value of image from here to insertimage function call.
    public static byte[] sendimage(System.Drawing.Image image_file, float max_width, float min_width)
    {
        int image_height = image_file.Height;
        int image_width = image_file.Width;
        byte[] data = null;

        if (image_width < Convert.ToInt32(min_width)) { return data; }

        if (image_width > Convert.ToInt32(max_width))
        {
            float sngratio = (float)image_width / (float)image_height;
            float max_height = max_width / sngratio;

            image_width = Convert.ToInt32(max_width);
            image_height = Convert.ToInt32(max_height);
        }

        // Create a new bitmap which will hold the previous resized bitmap
        Bitmap bitmap_file = new Bitmap(image_file, image_width, image_height);
        // Create a graphic based on the new bitmap
        Graphics oGraphics = Graphics.FromImage(bitmap_file);
        // Set the properties for the new graphic file
        oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        oGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        oGraphics.DrawImage(image_file, 0, 0, image_width, image_height);

        System.IO.MemoryStream stream = new System.IO.MemoryStream();

        bitmap_file.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        stream.Position = 0;

        data = new byte[stream.Length + 1];
        stream.Read(data, 0, data.Length);
        oGraphics.Dispose();
        bitmap_file.Dispose();
        return data;
    }
    //public static DataTable GetLiveData(string ID, string pf, string pt)
    //{
    //    DataTable dt = new DataTable("ContentDetails");
    //    dt.Columns.Add("ContUserID", typeof(string));
    //    dt.Columns.Add("ContUserName", typeof(string));
    //    dt.Columns.Add("ContDate", typeof(DateTime));
    //    dt.Columns.Add("ContUserPic", typeof(string));
    //    dt.Columns.Add("ContMsg", typeof(string));
    //    dt.Columns.Add("ContLink", typeof(string));
    //    dt.Columns.Add("ContPlatform", typeof(string));
    //    dt.Columns.Add("ContMsgType", typeof(string));
    //    dt.Columns.Add("ContID", typeof(string));
    //    dt.Columns.Add("ContLK", typeof(string));
    //    string a = "";
    //    string[] ids = ID.Split('-');
    //    foreach (string idss in ids)
    //    {
    //        if (pf.Contains("T"))
    //        {
    //            dt = GetTwitterData(dt, idss, pt);
    //            a = "Twitter : " + dt.Rows.Count.ToString();
    //        }
    //        if (pf.Contains("F"))
    //        {
    //            dt = GetFBData(dt, idss, pt);
    //            a += ", Facebook : " + dt.Rows.Count.ToString();
    //        }
    //        if (pf.Contains("I"))
    //        {
    //            dt = GetInstaData(dt, idss, pt);
    //            a += ", Instagram : " + dt.Rows.Count.ToString();
    //        }
    //    }
    //    DataView dv = dt.DefaultView;
    //    dv.Sort = "ContDate desc";
    //    DataTable sortedDT = dv.ToTable();
    //    //Response.Write(a);
    //    return sortedDT;
    //}
    //public static DataTable GetTwitterData(DataTable dt, string hash, string pt)
    //{
    //    crCredentials();
    //    var user = Tweetinvi.User.UserFactory.GetLoggedUser();
    //    var searchParameter = Search.GenerateSearchTweetParameter("#" + hash);
    //    searchParameter.MaximumNumberOfResults = 30;
    //    searchParameter.TweetSearchFilter = TweetSearchFilter.OriginalTweetsOnly;
    //    var homeTimelineTweets = Search.SearchTweets(searchParameter);
    //    //Response.Write("Total Tweets : " + homeTimelineTweets.Count.ToString());
    //    foreach (var tweet in homeTimelineTweets)
    //    {
    //        IUser cust = tweet.Creator;
    //        DateTime msgDate = tweet.CreatedAt;
    //        string uPic = "";
    //        var Med = tweet.Media;
    //        if (Med != null)
    //        {
    //            foreach (var m in Med)
    //            {
    //                uPic = m.MediaURL;
    //            }
    //            if (uPic != "")
    //            {
    //                if (pt.Contains("P"))
    //                {
    //                    dt.Rows.Add(cust.ScreenName, cust.Name, msgDate, cust.ProfileImageUrl, tweet.Text,
    //                        uPic, "Twitter", "Photo", tweet.Id, Convert.ToString(tweet.RetweetCount));
    //                }
    //            }
    //        }
    //        else
    //        {
    //            var twurl = tweet.Urls;
    //            if (twurl != null)
    //            {
    //                string ur = "";
    //                foreach (var u in twurl)
    //                {
    //                    ur = u.ExpandedURL;
    //                }
    //                if (ur != "")
    //                {
    //                    if (pt.Contains("V"))
    //                    {
    //                        dt.Rows.Add(cust.ScreenName, cust.Name, msgDate, cust.ProfileImageUrl, tweet.Text,
    //                            ur, "Twitter", "Video", tweet.Id, Convert.ToString(tweet.RetweetCount));
    //                    }
    //                }
    //                else
    //                {
    //                    if (pt.Contains("T"))
    //                    {
    //                        dt.Rows.Add(cust.ScreenName, cust.Name, msgDate, cust.ProfileImageUrl, tweet.Text,
    //                            "", "Twitter", "Content", tweet.Id, Convert.ToString(tweet.RetweetCount));
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return dt;
    //}
    //public static DataTable GetFBData(DataTable dt, string hash, string pt)
    //{
    //    string uToken = "231093070413818" + "|" + "feb721f2486ba0c3500d7d067987b0d8";
    //    JArray data = BoardLogin.GetFBSearch(uToken, "/Search?q=%23" + hash + "&limit=20");
    //    //Response.Write("Facebook Content : " + data.Count + " Hashtag : " + hash);
    //    foreach (var account in data)
    //    {
    //        DateTime dat = DateTime.Parse(Convert.ToString(account["created_time"]));
    //        string type = account["type"].ToString();
    //        var from = account["from"];
    //        string imgUrl = BoardLogin.GetPictureUrl(Convert.ToString(from["id"]));
    //        if (type == "video" && pt.Contains("V"))
    //        {
    //            dt.Rows.Add(Convert.ToString(from["id"]), Convert.ToString(from["name"]), dat, imgUrl,
    //                Convert.ToString(account["description"]), Convert.ToString(account["source"]), "Facebook",
    //                "Video", Convert.ToString(account["id"]), "0");
    //        }
    //        else if (type == "photo" && pt.Contains("P"))
    //        {
    //            dt.Rows.Add(Convert.ToString(from["id"]), Convert.ToString(from["name"]), dat, imgUrl,
    //                Convert.ToString(account["caption"]), Convert.ToString(account["picture"]), "Facebook",
    //                "Photo", Convert.ToString(account["id"]), "0");
    //        }
    //        else
    //        {
    //            if (pt.Contains("T"))
    //            {
    //                dt.Rows.Add(Convert.ToString(from["id"]), Convert.ToString(from["name"]), dat, imgUrl,
    //                    Convert.ToString(account["message"]), "", "Facebook", "Content", Convert.ToString(account["id"]), "0");
    //            }
    //        }
    //    }
    //    return dt;
    //}
    //public static DataTable GetInstaData(DataTable dt, string hash, string pt)
    //{
    //    var w = new WebClient();
    //    string url = "https://api.instagram.com/v1/tags/" + hash + "/media/recent?client_id=" + BoardConfig.InstaKey;
    //    var jsondata = string.Empty;
    //    jsondata = w.DownloadString(url);
    //    dynamic stuff = JsonConvert.DeserializeObject(jsondata);
    //    int count = (int)stuff.data.Count;
    //    string name = "";
    //    string picurl = "";
    //    string imgurl = "";
    //    string msg = "";
    //    string fullname = "";
    //    string id = "";
    //    DateTime msgDate;
    //    for (int i = 0; i < count; i++)
    //    {
    //        name = (string)stuff.data[i].user.username;
    //        picurl = (string)stuff.data[i].user.profile_picture;
    //        fullname = (string)stuff.data[i].user.full_name;
    //        msg = (string)stuff.data[i].caption.text;
    //        imgurl = (string)stuff.data[i].images.low_resolution.url;
    //        msgDate = UnixTimeStampToDateTime((double)stuff.data[i].created_time);
    //        id = (string)stuff.data[i].id;
    //        if (pt.Contains("P"))
    //        {
    //            dt.Rows.Add(name, fullname, msgDate, picurl, msg, imgurl, "Instagram", "Photo", id, "0");
    //        }
    //    }
    //    return dt;
    //}


    public static DataTable GetData(string ID, string pf, string pt, DataTable dt)
    {
        //"GetBoardContent"
        DataView dv = new DataView(dt);
        string rowFilter = "";
        string contype = "";
        string Exkeys = "";
        int flag = 0;
        if (pf.Contains("F"))
        {
            rowFilter += "ContPlatform = 'Facebook'";
            flag = 1;
        }
        if (pf.Contains("I"))
        {
            if (flag == 1)
            {
                rowFilter += " or ContPlatform='Instagram'";
                flag = 1;
            }
            else
            {
                rowFilter += "ContPlatform='Instagram'";
                flag = 1;
            }
        }
        if (pf.Contains("T"))
        {
            if (flag == 1)
            {
                rowFilter += " or ContPlatform='Twitter'";
                flag = 1;
            }
            else
            {
                rowFilter += "ContPlatform='Twitter'";
                flag = 1;
            }
        }
        if (flag == 1)
        {
            rowFilter = " ( " + rowFilter + " ) ";
            if (pt.Contains("P"))
            {
                contype = "ContMsgType='Photo'";
                flag = 2;
            }
            if (pt.Contains("V"))
            {
                if (flag == 2)
                {
                    contype += " or ContMsgType='Video'";
                    flag = 2;
                }
                else
                {
                    contype += "ContMsgType='Video'";
                    flag = 2;
                }
            }
            if (pt.Contains("T"))
            {
                if (flag == 2)
                {
                    contype += " or ContMsgType='Content'";
                    flag = 2;
                }
                else
                {
                    contype += "ContMsgType='Content'";
                    flag = 2;
                }
            }

        }
        if (flag == 2)
        {
            contype = " and ( " + contype + " )";
            rowFilter = rowFilter + contype;
        }
        if (rowFilter != "")
        {
            DataTable kw = BoardAccess.GetKey(ID, "KeyDetails", "0");
            if (kw.Rows.Count != 0)
            {

                for (int i = 0; i < kw.Rows.Count; i++)
                {
                    if (i == 0)
                        Exkeys += " ContMsg Not Like '%" + kw.Rows[i]["Keywords"] + "%' ";
                    else
                        Exkeys += " and ContMsg Not Like '%" + kw.Rows[i]["Keywords"] + "%' ";
                }
                rowFilter = rowFilter + " and " + Exkeys;
            }

        }
        else
        {
            DataTable kw = BoardAccess.GetKey(ID, "KeyDetails", "0");
            if (kw.Rows.Count != 0)
            {

                for (int i = 0; i < kw.Rows.Count; i++)
                {
                    if (i == 0)
                        Exkeys += " ContMsg Not Like '%" + kw.Rows[i]["Keywords"] + "%'";
                    else
                        Exkeys += " and ContMsg Not Like '%" + kw.Rows[i]["Keywords"] + "%' ";
                }
                rowFilter = Exkeys;
            }

        }
        dv.RowFilter = rowFilter;

        return dv.ToTable();
    }
    

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
    public static void crCredentials()
    {
        TwitterCredentials.Credentials = TwitterCredentials.CreateCredentials(
            BoardConfig.AccessToken,
            BoardConfig.AccessSecret,
            BoardConfig.ConsumerKey,
            BoardConfig.ConsumerSecret);
    }
    //Insert Keywords in Exclude
    public static void InsertKeywords(string keyword, string boardid)
    {
        BoardAccess.InsertKey(keyword, boardid);
    }
    public static DataTable GetKeywords(string boardid,string type,string keywords)
    {
        return BoardAccess.GetKey(boardid, type, keywords);
    }
    //Delete Keywords in Exclude
    public static void DeleteKeywords(string keyID)
    {
        BoardAccess.DeleteKey(keyID);
    }
    //Method for WebService

    public static void InsertTwitterData(string hash, string boardid)
    {
        try
        {
            crCredentials();
            var user = Tweetinvi.User.UserFactory.GetLoggedUser();
            var searchParameter = Search.GenerateSearchTweetParameter("#" + hash);
            searchParameter.MaximumNumberOfResults = 100;
            searchParameter.TweetSearchFilter = TweetSearchFilter.OriginalTweetsOnly;
            var homeTimelineTweets = Search.SearchTweets(searchParameter);
            string sentiment = "";
            //Response.Write("Total Tweets : " + homeTimelineTweets.Count.ToString());
            DataTable dt1 = BoardAccess.GetContent(boardid, "0", "Twitter", "GetLast");
            foreach (var tweet in homeTimelineTweets)
            {
                IUser cust = tweet.Creator;
                DateTime msgDate = tweet.CreatedAt;
                string loc = cust.Location;
                sentiment = confidence(tweet.Text);
                if (dt1.Rows.Count != 0)
                {
                    if (msgDate > Convert.ToDateTime(dt1.Rows[0]["ContDate"]))
                    {

                        string uPic = "";
                        var Med = tweet.Media;
                        if (Med != null)
                        {
                            foreach (var m in Med)
                            {
                                uPic = m.MediaURL;
                            }
                            if (uPic != "")
                            {

                                if (BoardAccess.InsertContent("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                    tweet.Text, uPic, "Twitter", "Photo", Convert.ToString(tweet.Id),
                                    Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", Convert.ToString(msgDate), "True",
                                    hash, sentiment, loc))
                                {
                                    if (boardid == "66")
                                    {
                                        string check = InsertLays("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                        tweet.Text, uPic, "Twitter", "Photo", Convert.ToString(tweet.Id),
                                        Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", msgDate, "True",
                                        hash, sentiment, loc);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var twurl = tweet.Urls;
                            if (twurl != null)
                            {
                                string ur = "";
                                foreach (var u in twurl)
                                {
                                    ur = u.ExpandedURL;
                                }
                                if (ur != "")
                                {
                                    BoardAccess.InsertContent("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                    tweet.Text, ur, "Twitter", "Video", Convert.ToString(tweet.Id),
                                    Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", Convert.ToString(msgDate),
                                    "True", hash, sentiment, loc);

                                }
                                else
                                {
                                    BoardAccess.InsertContent("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                    tweet.Text, "", "Twitter", "Content", Convert.ToString(tweet.Id),
                                    Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", Convert.ToString(msgDate),
                                    "True", hash, sentiment, loc);
                                }
                            }
                        }
                    }
                }
                else
                {

                    string uPic = "";
                    var Med = tweet.Media;
                    if (Med != null)
                    {
                        foreach (var m in Med)
                        {
                            uPic = m.MediaURL;
                        }
                        if (uPic != "")
                        {
                            if (BoardAccess.InsertContent("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                tweet.Text, uPic, "Twitter", "Photo", Convert.ToString(tweet.Id),
                                Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", Convert.ToString(msgDate), "True",
                                hash, sentiment, loc))
                            {
                                if (boardid == "66")
                                {
                                    string check = InsertLays("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                    tweet.Text, uPic, "Twitter", "Photo", Convert.ToString(tweet.Id),
                                    Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", msgDate, "True",
                                    hash, sentiment, loc);
                                }
                            }
                        }
                    }
                    else
                    {
                        var twurl = tweet.Urls;
                        if (twurl != null)
                        {
                            string ur = "";
                            foreach (var u in twurl)
                            {
                                ur = u.ExpandedURL;
                            }
                            if (ur != "")
                            {
                                BoardAccess.InsertContent("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                tweet.Text, ur, "Twitter", "Video", Convert.ToString(tweet.Id),
                                Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", Convert.ToString(msgDate), "True",
                                hash, sentiment, loc);
                            }
                            else
                            {
                                BoardAccess.InsertContent("0", boardid, cust.ScreenName, cust.Name, cust.ProfileImageUrl,
                                tweet.Text, "", "Twitter", "Content", Convert.ToString(tweet.Id),
                                Convert.ToString(tweet.RetweetCount), Convert.ToString(tweet.FavouriteCount), "0", "Insert", Convert.ToString(msgDate), "True",
                                hash, sentiment, loc);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BoardUtilities.LogError(ex);
        }
    }
    public static void InsertFBData(string hash, string boardid)
    {
        try
        {
            string uToken = "231093070413818" + "|" + "feb721f2486ba0c3500d7d067987b0d8";
            JArray data = BoardLogin.GetFBSearch(uToken, "/Search?q=%23" + hash + "&limit=50");
            //Response.Write("Facebook Content : " + data.Count + " Hashtag : " + hash);
            DataTable dt1 = BoardAccess.GetContent(boardid, "0", "Facebook", "GetLast");
            string sentiment = "";
            int LikeCount = 0;
            int CommCount = 0;
            int ShareCount = 0;
            foreach (var account in data)
            {
                DateTime dat = DateTime.Parse(Convert.ToString(account["created_time"]));
                //string ContDate = dat.ToString("yyyyMMddHHmmss");
                string type = account["type"].ToString();
                var from = account["from"];
                string imgUrl = BoardLogin.GetPictureUrl(Convert.ToString(from["id"]));
                string loc = "fb";
                string msg = "";
                if (Convert.ToString(account["description"]) != "")
                {
                    if (Convert.ToString(account["description"]).Contains(hash))
                    {
                        msg = Convert.ToString(account["description"]);
                    }
                }
                if (Convert.ToString(account["message"]) != "" && msg != "")
                {
                    if (Convert.ToString(account["message"]).Contains(hash))
                    {
                        msg = Convert.ToString(account["message"]);
                    }
                }
                if (Convert.ToString(account["caption"]) != "" && msg != "")
                {
                    if (Convert.ToString(account["caption"]).Contains(hash))
                    {
                        msg = Convert.ToString(account["caption"]);
                    }
                }
                if (msg != "")
                {
                    if (dt1.Rows.Count != 0)
                    {
                        if (dat > Convert.ToDateTime(dt1.Rows[0]["ContDate"]))
                        {
                            string uID = Convert.ToString(from["id"]);
                            dynamic fbVal = BoardLogin.GetFBData(uToken, uID + "/");
                            dynamic res = BoardLogin.GetFBData(uToken, "/" + Convert.ToString(from["id"]) + "/likes?summary=1");
                            dynamic summ = res.summary;
                            if (summ != null)
                                LikeCount = Convert.ToInt32(summ.total_count);
                            res = BoardLogin.GetFBData(uToken, "/" + Convert.ToString(from["id"]) + "/comments?summary=1");
                            summ = res.summary;
                            if (summ != null)
                                CommCount = Convert.ToInt32(summ.total_count);

                            dynamic data1 = BoardLogin.GetFBData(uToken, "/" + Convert.ToString(from["id"]));
                            if (data1.id != null)
                            {
                                summ = data1.shares;
                                if (summ != null)
                                    ShareCount = Convert.ToInt32(summ.count);
                                else
                                    ShareCount = 0;
                            }

                            if (type == "video")
                            {
                                sentiment = confidence(Convert.ToString(account["description"]));
                                //(Convert.ToString(from["id"]), Convert.ToString(from["name"]), dat, imgUrl,
                                //    Convert.ToString(account["description"]), Convert.ToString(account["source"]), "Facebook",
                                //    "Video", Convert.ToString(account["id"]), "0");
                                BoardAccess.InsertContent("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                                    imgUrl, Convert.ToString(account["description"]), Convert.ToString(account["source"]), "Facebook",
                                    "Video", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", Convert.ToString(dat), "True", hash, sentiment, loc);



                            }
                            else if (type == "photo")
                            {
                                sentiment = confidence(Convert.ToString(account["message"]));                                

                                BoardAccess.InsertContent("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                                    imgUrl, Convert.ToString(account["message"]), Convert.ToString(account["picture"]), "Facebook",
                                    "Photo", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", Convert.ToString(dat), "True", hash, sentiment, loc);
                                string contuserid = Convert.ToString(from["id"]);
                                string contusername = Convert.ToString(from["name"]);
                                if (boardid == "66")
                                {                                    
                                    //string query = "Insert Into ContentDetails(id,BoardID,ContUserID,ContUserName,ContDate,ContUserPic,ContMsg,ContLink,ContPlatform,ContMsgType,ContID,ContLike,ContComment,ContShare,ExcludeContent,HashTag,Sentiment,Location) Values(NULL,'" + boardid + "','" + Convert.ToString(from["id"]) + "','" + Convert.ToString(from["name"]) + "','" + dat.ToString("yyyyMMddHHmmss") + "','" + imgUrl + "','" + Convert.ToString(account["message"]) + "','" + Convert.ToString(account["picture"]) + "','" + "Facebook" + "','" + "Photo" + "','" + Convert.ToString(account["id"]) + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + hash + "','" + sentiment + "','" + loc + "')";
                                   string check=InsertLays("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                                    imgUrl, Convert.ToString(account["message"]), Convert.ToString(account["picture"]), "Facebook",
                                    "Photo", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", dat, "True", hash, sentiment, loc);
                                    //insert(query);
                                }

                            }
                            else
                            {
                                sentiment = confidence(Convert.ToString(account["message"]));
                                //(Convert.ToString(from["id"]), Convert.ToString(from["name"]), dat, imgUrl,
                                //Convert.ToString(account["message"]), "", "Facebook", "Content", Convert.ToString(account["id"]), "0");
                                BoardAccess.InsertContent("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                                   imgUrl, Convert.ToString(account["message"]), Convert.ToString(account["picture"]), "Facebook",
                                   "Content", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", Convert.ToString(dat), "True", hash, sentiment, loc);

                            }
                        }
                    }
                    else
                    {
                        dynamic res = BoardLogin.GetFBData(uToken, "/" + Convert.ToString(from["id"]) + "/likes?summary=1");
                        dynamic summ = res.summary;
                        if (summ != null)
                            LikeCount = Convert.ToInt32(summ.total_count);
                        res = BoardLogin.GetFBData(uToken, "/" + Convert.ToString(from["id"]) + "/comments?summary=1");
                        summ = res.summary;
                        if (summ != null)
                            CommCount = Convert.ToInt32(summ.total_count);

                        dynamic data1 = BoardLogin.GetFBData(uToken, "/" + Convert.ToString(from["id"]));
                        if (data1.id != null)
                        {
                            summ = data1.shares;
                            if (summ != null)
                                ShareCount = Convert.ToInt32(summ.count);
                            else
                                ShareCount = 0;
                        }

                        if (type == "video")
                        {
                            sentiment = confidence(Convert.ToString(account["description"]));
                            BoardAccess.InsertContent("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                                imgUrl, Convert.ToString(account["description"]), Convert.ToString(account["source"]), "Facebook",
                                "Video", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", Convert.ToString(dat), "True", hash, sentiment, loc);



                        }
                        else if (type == "photo")
                        {
                            sentiment = confidence(Convert.ToString(account["message"]));
                            BoardAccess.InsertContent("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                            imgUrl, Convert.ToString(account["message"]), Convert.ToString(account["picture"]), "Facebook",
                                "Photo", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", Convert.ToString(dat), "True", hash, sentiment, loc);
                            if (boardid == "66")
                            {
                                string check = InsertLays("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                                   imgUrl, Convert.ToString(account["message"]), Convert.ToString(account["picture"]), "Facebook",
                                   "Photo", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", dat, "True", hash, sentiment, loc);
                            }

                        }
                        else
                        {
                            sentiment = confidence(Convert.ToString(account["message"]));
                            //(Convert.ToString(from["id"]), Convert.ToString(from["name"]), dat, imgUrl,
                            //        Convert.ToString(account["message"]), "", "Facebook", "Content", Convert.ToString(account["id"]), "0");
                            BoardAccess.InsertContent("0", boardid, Convert.ToString(from["id"]), Convert.ToString(from["name"]),
                               imgUrl, Convert.ToString(account["message"]), Convert.ToString(account["picture"]), "Facebook",
                               "Content", Convert.ToString(account["id"]), LikeCount.ToString(), CommCount.ToString(), ShareCount.ToString(), "Insert", Convert.ToString(dat), "True", hash, sentiment, loc);

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BoardUtilities.LogError(ex);
        }
    }
    public static void InsertInstaData(string hash, string boardid)
    {
        try
        {
            var w = new WebClient();
            string url = "https://api.instagram.com/v1/tags/" + hash + "/media/recent?client_id=" + BoardConfig.InstaKey;
            var jsondata = string.Empty;
            jsondata = w.DownloadString(url);
            dynamic stuff = JsonConvert.DeserializeObject(jsondata);
            int count = (int)stuff.data.Count;
            string name = "";
            string picurl = "";
            string imgurl = "";
            string msg = "";
            string fullname = "";
            string id = "";
            string sent = "";
            string loc = "";
            string like = "";
            string comment = "";
            DateTime msgDate;
            DataTable dt1 = BoardAccess.GetContent(boardid, "0", "Instagram", "GetLast");
            for (int i = 0; i < count; i++)
            {
                name = (string)stuff.data[i].user.username;
                picurl = (string)stuff.data[i].user.profile_picture;
                fullname = (string)stuff.data[i].user.full_name;
                if (stuff.data[i].caption != null)
                {
                    msg = Convert.ToString(stuff.data[i].caption.text);
                }
                imgurl = (string)stuff.data[i].images.low_resolution.url;
                like = (string)stuff.data[i].likes.count;
                comment = (string)stuff.data[i].comments.count;
                msgDate = UnixTimeStampToDateTime((double)stuff.data[i].created_time);
                id = (string)stuff.data[i].id;
                if (msg != "") { sent = confidence(msg); }
                else { sent = "0"; }
                if (dt1.Rows.Count != 0)
                {
                    if (msgDate > Convert.ToDateTime(dt1.Rows[0]["ContDate"]))
                    {
                        //dt.Rows.Add(name, fullname, msgDate, picurl, msg, imgurl, "Instagram", "Photo", id, "0");
                        BoardAccess.InsertContent("0", boardid, name, fullname, picurl, msg, imgurl, "Instagram", "Photo", id, like, comment, "0", "Insert", Convert.ToString(msgDate), "True", hash, sent, loc);
                    }
                }
                else
                {
                    BoardAccess.InsertContent("0", boardid, name, fullname, picurl, msg, imgurl, "Instagram", "Photo", id, like, comment, "0", "Insert", Convert.ToString(msgDate), "True", hash, sent, loc);
                }
            }
        }
        catch (Exception ex)
        {
            BoardUtilities.LogError(ex);
        }
    }
    //Method to insert App Log

    public static void InsertLog(string content)
    {
        BoardAccess.InsertAppLog(content);
    }
    public static string confidence(string data)
    {
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["txt"] = data;
            var response = client.UploadValues("http://sentiment.vivekn.com/api/text/", values);

            var responseString = Encoding.Default.GetString(response);
            dynamic senti = JsonConvert.DeserializeObject(responseString);
            if (data.Trim() == "")
            {
                return "0";
            }
            else
            {
                return (string)senti.result.confidence;
            }
        }
    }

    public static string InsertLays(string ID, string bID, string cuID, string cuName, string cuPic, string cMsg,
        string cLink, string cPlatform, string cMsgType, string cID, string cLike, string cComment, string cShare,
        string type, DateTime condate, string ExcludeContent, string HashTag, string Sentiment, string location)
    {
        var request = (HttpWebRequest)WebRequest.Create("http://pepsi.in/backtoschool/insertwebdata.php");        
        var postData="BoardID=" + bID;
        postData += "&ContUserID=" + cuID;
        postData += "&ContUserName=" + cuName;        
        postData += "&ContDate=" + condate.ToString("yyyyMMddHHmmss");
        postData += "&ContUserPic=" + cuPic;
        postData += "&ContMsg=" + cMsg;
        postData += "&ContLink=" + cLink;        
        postData += "&ContPlatform=" + cPlatform;
        postData += "&ContMsgType=" + cMsgType;
        postData += "&ContID=" + cID;
        postData += "&ContLike="+cLike;
        postData += "&ContComment="+ cComment;
        postData += "&ContShare="+cShare;
        postData += "&HashTag="+HashTag;
        postData += "&ExcludeContent="+ExcludeContent;
        postData += "&Sentimente="+Sentiment;
        postData += "&Location="+location;
        //postData += "&selfie=" + selfie;

        var data = Encoding.ASCII.GetBytes(postData);

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //jRes result = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<jRes>(responseString);
        return responseString.ToString();

    }
//    public static void insert(string cmdText)
//    {
//        try
//        {
//            //string MyConString = "SERVER=localhost;DATABASE=sale;UID=root;PASSWORD=alimola;";
////            216.245.196.100
//            string MyConString = "SERVER=216.245.196.100; DATABASE=sdlclabs_ping; UID=school; PASSWORD=Ffa%z760;";
//            MySqlConnection connection = new MySqlConnection(MyConString);
//            //string cmdText = "INSERT INTO customer(C_Name ,E-mail ,Password ,Address)VALUES ('C_Name.Text','E-mail.Text','Password.Text','Address.Text')";
//            MySqlCommand cmd = new MySqlCommand(cmdText, connection);

//           // ConnString = "DRIVER={MySQL ODBC 3.51 Driver}; SERVER=181.224.157.43;Port=3306; DATABASE=sdlclabs_ping; UID=sdlclabs_ping; PASSWORD=AiSTiz1mlerv"

//            connection.Open();

//            int result = cmd.ExecuteNonQuery();
//            connection.Close();

//            //lblError.Text = "Data Saved";

//        }
//        catch (Exception)
//        {
//            Console.Write("not entered");
//            //lblError.Text = ex.Message;
//        }
//    }

}