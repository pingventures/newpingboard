using System;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;

/// <summary>
/// Link factory class
/// </summary>
public class BoardLink
{
    // regular expression that removes characters that aren't a-z, 0-9, dash, 
    // underscore or space
    private static Regex purifyUrlRegex = new Regex("[^-a-zA-Z0-9_ ]", RegexOptions.Compiled);

    // regular expression that changes dashes, underscores and spaces to dashes
    private static Regex dashesRegex = new Regex("[-_ ]+", RegexOptions.Compiled);

    //creating paging

    // prepares a string to be included in an URL
    public static string PrepareUrlText(string urlText)
    {
        // remove all characters that aren't a-z, 0-9, dash, underscore or space
        urlText = purifyUrlRegex.Replace(urlText, "");

        // remove all leading and trailing spaces
        urlText = urlText.Trim();

        // change all dashes, underscores and spaces to dashes
        urlText = dashesRegex.Replace(urlText, "-");

        // return the modified string    
        return urlText;
    }
    // prepares a string to be Use as UserName
    public static string PrepareUserText(string userText)
    {
        // remove all characters that aren't a-z, 0-9, dash, underscore or space
        userText = purifyUrlRegex.Replace(userText, "");

        // remove all leading and trailing spaces
        userText = userText.Trim();

        // change all dashes, underscores and spaces to blank
        userText = dashesRegex.Replace(userText, "");

        // return the modified string
        return userText;
    }
}