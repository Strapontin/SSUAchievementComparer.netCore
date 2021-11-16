using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SSUAchievementComparer.Core.Shared
{
    public static class HtmlCommon
    {
        public static string GetPageContent(string link)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(link);
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                string content = sr.ReadToEnd();
                return content;
            }
        }
    }
}
