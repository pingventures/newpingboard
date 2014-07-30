using System.Configuration;

/// <summary>
/// Repository for PingBoard site configuration settings
/// </summary>
public static class BoardConfig
{
    // Caches the connection string
    private static string dbConnectionString;
    // Caches the data provider name 
    private static string dbProviderName;
    // Store the name of your website
    private readonly static string siteName;
    // Store the facebook scope of your website
    private readonly static string fbScope;
    // Store the facebook app id of your website
    private readonly static string fbAppKey;
    // Store the facebook app secret of your website
    private readonly static string fbAppSecret;
    // Store the twitter access token of your website
    private readonly static string accessToken;
    // Store the twitter access secret of your website
    private readonly static string accessSecret;
    // Store the twitter consumer key of your website
    private readonly static string consumerKey;
    // Store the twitter consumer secret of your website
    private readonly static string consumerSecret;
    // Store the instagram key of your website
    private readonly static string instaKey;

	static BoardConfig()
	{
        dbConnectionString = ConfigurationManager.ConnectionStrings["PingBoardConString"].ConnectionString;
        dbProviderName = ConfigurationManager.ConnectionStrings["PingBoardConString"].ProviderName;
        siteName = ConfigurationManager.AppSettings["SiteName"];
        accessToken = ConfigurationManager.AppSettings["Boardtoken_AccessToken"];
        accessSecret = ConfigurationManager.AppSettings["Boardtoken_AccessTokenSecret"];
        consumerKey = ConfigurationManager.AppSettings["BoardconsumerKey"];
        consumerSecret = ConfigurationManager.AppSettings["BoardconsumerSecret"];
        instaKey = ConfigurationManager.AppSettings["BoardInstaKey"];
        fbScope = ConfigurationManager.AppSettings["BoardFBScope"];
        fbAppKey = ConfigurationManager.AppSettings["BoardFBKey"];
        fbAppSecret = ConfigurationManager.AppSettings["BoardFBSecret"];
	}
    // Returns the facebook Scope for the website
    public static string FbScope
    {
        get
        {
            return fbScope;
        }
    }
    // Returns the facebook App Secret for the website
    public static string FbAppSecret
    {
        get
        {
            return fbAppSecret;
        }
    }
    // Returns the facebook App key for the website
    public static string FbAppKey
    {
        get {
            return fbAppKey;
        }
    }
    // Returns the Instagram key for the website
    public static string InstaKey
    {
        get
        {
            return instaKey;
        }
    }
    // Returns the Twitter Access Token for the website
    public static string AccessToken
    {
        get
        {
            return accessToken;
        }
    }
    // Returns the Twitter Access Secret for the website
    public static string AccessSecret
    {
        get
        {
            return accessSecret;
        }
    }
    // Returns the Twitter consumer key for the website
    public static string ConsumerKey
    {
        get
        {
            return consumerKey;
        }
    }
    // Returns the Twitter consumer Secret for the website
    public static string ConsumerSecret
    {
        get
        {
            return consumerSecret;
        }
    }
    // Returns the connection string for the database
    public static string DbConnectionString
    {
        get
        {
            return dbConnectionString;
        }
    }
    // Returns the data provider name
    public static string DbProviderName
    {
        get
        {
            return dbProviderName;
        }
    }
    // Returns the address of the mail server
    public static string MailServer
    {
        get
        {
            return ConfigurationManager.AppSettings["MailServer"];
        }
    }
    // Returns the email username
    public static string MailUsername
    {
        get
        {
            return ConfigurationManager.AppSettings["MailUserName"];
        }
    }

    // Returns the email password
    public static string MailPassword
    {
        get
        {
            return ConfigurationManager.AppSettings["MailPassword"];
        }
    }
    // Returns the email password
    public static string MailFrom
    {
        get
        {
            return ConfigurationManager.AppSettings["MailFrom"];
        }
    }

    // Send error log emails?
    public static bool EnableErrorLogEmail
    {
        get
        {
            return bool.Parse(ConfigurationManager.AppSettings["EnableErrorLogEmail"]);
        }
    }

    // Returns the email address where to send error reports
    public static string ErrorLogEmail
    {
        get
        {
            return ConfigurationManager.AppSettings["ErrorLogEmail"];
        }
    }

    // Returns the name of Website
    public static string SiteName
    {
        get
        {
            return siteName;
        }
    }
}