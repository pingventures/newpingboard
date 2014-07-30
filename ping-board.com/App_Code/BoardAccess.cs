using System;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Web.Security;

/// <summary>
/// Supports Master Panel functionality
/// </summary>
public class BoardAccess
{
    public BoardAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // Insert/Update/Delete User Details
    public static bool InsertUser(string uID, string uName, string uCompany, string uEmail, string uStat, string uPass,
        string sType, string sID, string aToken, string aSecret, string type)
    {
        //Type : 1.Insert to Insert details of User
        //Type : 2.Update to Update user details
        //Type : 3.UpdatePass to update password of user
        //Type : 4.UpdateStatus to update status of user
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "InsertUser";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserID";
        param.Value = uID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserName";
        param.Value = uName;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserCompany";
        param.Value = uCompany;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserEmail";
        param.Value = uEmail;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserStatus";
        param.Value = uStat;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserPass";
        param.Value = uPass;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@SocialType";
        param.Value = sType;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@SocialID";
        param.Value = sID;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@AToken";
        param.Value = aToken;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ASecret";
        param.Value = aSecret;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ignore them here
        }
        // result will be 1 in case of success
        return (result != -1);
    }
    // retrieve user details
    public static DataTable GetUser(string uID, string uEmail, string uPass, string type)
    {
        //Type : 1.GetUser to get single user by ID
        //Type : 2.CheckLogin to check login details
        //Type : 3.CheckUser to get user by emailID
        //Type : 4.CheckSocial to get user by social id
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetUser";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserEmail";
        param.Value = uEmail;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserID";
        param.Value = uID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserPass";
        param.Value = uPass;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    // Insert/Udpate Board Social Details
    public static bool InsertBoardSocial(string bID, string sType, string aToken, string aSecret, string fbPage, string type)
    {
        //Type : 1.Insert to Insert details of Board Social
        //Type : 2.Update to Update Board Social Details
        //Type : 3.UpdateFbPage to update board fb page
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "InsertBoardSocial";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@SocialType";
        param.Value = sType;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@fbPage";
        param.Value = fbPage;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@AToken";
        param.Value = aToken;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ASecret";
        param.Value = aSecret;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ignore them here
        }
        // result will be 1 in case of success
        return (result != -1);
    }
    // retrieve board social details
    public static DataTable GetBoardSocial(string bID, string stype, string type)
    {
        //Type : 1.GetBoardSocial to get board social by type
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetBoardSocial";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@SocialType";
        param.Value = stype;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    // Insert Board Details
    public static bool InsertBoard(string bID, string bName, string bCreator, string bPlatform, string isActive,
        string isContent, string type)
    {
        //Type : 1.Insert to Insert details of Board
        //Type : 2.Delete to delete board details from database
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "InsertBoard";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@BoardName";
        param.Value = bName;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@BoardPlatform";
        param.Value = bPlatform;
        param.DbType = DbType.String;
        param.Size = 150;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@BoardCreator";
        param.Value = bCreator;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@isActive";
        param.Value = isActive;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@isContent";
        param.Value = isContent;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ignore them here
        }
        // result will be 1 in case of success
        return (result != -1);
    }
    // retrieve board details
    public static DataTable GetBoard(string bName, string cID, string type, string Iscon, string IsAct, string bID)
    {
        //Type : 1.GetBoard to get single board by ID
        //Type : 2.GetAllByUser to get all brands by order brand date desc
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetBoard";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Board";
        param.Value = bName;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Creator";
        param.Value = cID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@isContent";
        param.Value = Iscon;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@isActive";
        param.Value = IsAct;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static bool InsertContent(string ID, string bID, string cuID, string cuName, string cuPic, string cMsg,
        string cLink, string cPlatform, string cMsgType, string cID, string cLike, string cComment, string cShare,
        string type, string condate, string ExcludeContent, string HashTag, string Sentiment, string location)
    {
        //Type : 1.Insert to Insert details of brand Social
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "InsertContent";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@ID";
        param.Value = ID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContLike";
        param.Value = cLike;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContComment";
        param.Value = cComment;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContShare";
        param.Value = cShare;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContUserID";
        param.Value = cuID;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContUserName";
        param.Value = cuName;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContUserPic";
        param.Value = cuPic;
        param.DbType = DbType.String;
        param.Size = 150;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContMsg";
        param.Value = cMsg;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContLink";
        param.Value = cLink;
        param.DbType = DbType.String;
        param.Size = 150;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContPlatform";
        param.Value = cPlatform;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContMsgType";
        param.Value = cMsgType;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContID";
        param.Value = cID;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@condate";
        param.Value = condate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ExcludeContent";
        param.Value = ExcludeContent;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@HashTag";
        param.Value = HashTag;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Sentiment";
        param.Value = Sentiment;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Location";
        param.Value = location;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
            //make connection to mysql database
            //
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ignore them here
        }
        // result will be 1 in case of success
        return (result != -1);
    }
    // retrieve Content details
    public static DataTable GetContent(string bID, string cID, string platform, string type)
    {
        //Type : 1.GetContent to get content by Board
        //Type : 2.GetByPlatform to get all content by Board and Platform
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetContent";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Platform";
        param.Value = platform;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ContID";
        param.Value = cID;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable GetSortContent(string bID, string type, int index, int increment, string platform1, string platform2,
        string platform3, string Msgtype1, string Msgtype2, string Msgtype3)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetSortContent";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Index";
        param.Value = index;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Increment";
        param.Value = increment;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Platform1";
        param.Value = platform1;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Platform2";
        param.Value = platform2;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Platform3";
        param.Value = platform3;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@MsgType1";
        param.Value = Msgtype1;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@MsgType2";
        param.Value = Msgtype2;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@MsgType3";
        param.Value = Msgtype3;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable GetSortBoard(string bID, string type, int index, int increment, string hashtag, string platform1, 
        string platform2, string platform3, string Msgtype1, string Msgtype2, string Msgtype3)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetSortBoard";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = bID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@HashTag";
        param.Value = hashtag;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Index";
        param.Value = index;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Increment";
        param.Value = increment;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Platform1";
        param.Value = platform1;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Platform2";
        param.Value = platform2;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Platform3";
        param.Value = platform3;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@MsgType1";
        param.Value = Msgtype1;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@MsgType2";
        param.Value = Msgtype2;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@MsgType3";
        param.Value = Msgtype3;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static void InsertAppLog(string LogType)
    {
        //Type : 1.Insert to Insert details of brand Social
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "InsertLog";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@LogType";
        param.Value = LogType;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        //// create a new parameter
        //param.ParameterName = "@Type";
        //param.Value = Type;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
        int result = GenericDataAccess.ExecuteNonQuery(comm);
    }
    //Insert Keyword in ExcludeKey Table
    public static bool InsertKey(string Keyword, string boardid)
    {
        //Type : 1.Insert to Insert details of brand Social
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "InsertKey";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = boardid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Keywords";
        param.Value = Keyword;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
            //make connection to mysql database
            //
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ignore them here
        }
        // result will be 1 in case of success
        return (result != -1);
    }
    //Insert Keyword in ExcludeKey Table
    public static DataTable GetKey(string boardid, string type, string keywords)
    {
        //Type : 1.Insert to Insert details of brand Social
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetKey";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@BoardID";
        param.Value = boardid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@Type";
        param.Value = type;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Keywords";
        param.Value = keywords;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    //Delete Keyword in ExcludeKey Table
    public static bool DeleteKey(string KeyID)
    {
        //Type : 1.Insert to Insert details of brand Social
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "DeleteKey";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@KeyID";
        param.Value = KeyID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
            //make connection to mysql database
            //
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ignore them here
        }
        // result will be 1 in case of success
        return (result != -1);
    }
}