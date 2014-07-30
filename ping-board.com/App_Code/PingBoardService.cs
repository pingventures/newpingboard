using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for PingBoardService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class PingBoardService : System.Web.Services.WebService
{

    public PingBoardService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

    }
    [WebMethod]
    public void InsertContentDetails(string content)
    {
        BoardLogic.InsertLog(content);
        if (content == "Another Time")
            getData();
    }
    [WebMethod]
    public void LaysService()
    {
        
    }
    protected void getData()
    {
        DataTable dt = BoardAccess.GetBoard("0", "0", "GetBoardContent", "True", "True", "0");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string platform = Convert.ToString(dt.Rows[i]["BoardPlatform"]);            
            string BId = Convert.ToString(dt.Rows[i]["BoardID"]);
            string HashTag = Convert.ToString(dt.Rows[i]["BoardName"]);
            string[] tothash = HashTag.Split('-');
            if (tothash.Length > 0)
            {
                foreach (string HashWord in tothash)                
                {
                    if(platform.Contains("Instagram"))
                        BoardLogic.InsertInstaData(HashWord, BId);
                    if (platform.Contains("Twitter"))
                        BoardLogic.InsertTwitterData(HashWord, BId);
                    if (platform.Contains("Facebook"))
                        BoardLogic.InsertFBData(HashWord, BId);
                }
            }
        }
    }
}
