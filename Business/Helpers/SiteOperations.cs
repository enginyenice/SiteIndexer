//Created By Engin Yenice
//enginyenice2626@gmail.com

// Created By Engin Yenice
// enginyenice2626@gmail.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Helper
{
    public static class SiteOperations
    {
        public static IDataResult<WebSite> GetWebSite(WebSite webSite, List<HtmlTag> htmlTags)
        {
            WebRequest request = WebRequest.Create(webSite.Url); //2
            WebResponse response = request.GetResponse(); //4
            StreamReader responseData = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()); //5
            string siteAllData = responseData.ReadToEnd(); //6
            webSite.SiteTitle = Title(siteAllData);

            foreach (var tag in htmlTags)
            {
                webSite.Content += $"[{tag.Tag}]" + H1List(siteAllData, tag.Tag);
            }

            webSite.Content = webSite.Content.Replace("&nbsp;", "").Replace("<br>", " ").Replace("</br>", " ").Replace(System.Environment.NewLine, " ");
            webSite.Content = Regex.Replace(webSite.Content, @"\r\n?|\n", "");
            webSite.Content = webSite.Content.Trim();
            return new SuccessDataResult<WebSite>(webSite);
        }

        private static string Title(string siteAllData)
        {
            int titleIndexFirst = siteAllData.IndexOf("<title>", StringComparison.Ordinal) + 7;
            int titleIndexLast = siteAllData[titleIndexFirst..].IndexOf("</title>", StringComparison.Ordinal); //8
            return siteAllData.Substring(titleIndexFirst, titleIndexLast);
        }

        private static string H1List(string siteAllData, string tag)
        {
            string text = "";
            int last = 0;
            while (true)
            {
                siteAllData = siteAllData[last..];
                int titleIndexFirst = siteAllData.IndexOf($"<{tag}", StringComparison.Ordinal);

                int titleIndexLast = titleIndexFirst != -1 ? siteAllData[titleIndexFirst..].IndexOf($"</{tag}>", StringComparison.Ordinal) : -1; //8

                if (titleIndexFirst == -1 || titleIndexLast == -1)
                {
                    break;
                }

                last = titleIndexFirst + titleIndexLast;
                text += RemoveHtml(siteAllData.Substring(titleIndexFirst, titleIndexLast)) + " ";
            }

            return text;
        }

        public static string RemoveHtml(string text)
        {
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            Regex rRemHead = new Regex(@"<head[^>]*>[\s\S]*?</head>");
            Regex rRemStyle = new Regex(@"<style[^>]*>[\s\S]*?</style>");
            Regex rRemCode = new Regex(@"<code[^>]*>[\s\S]*?</code>");
            Regex rRemImage = new Regex(@"<img[^>]* />");

            Regex rRemHtml = new Regex(@"<(.|\n)*?>");
            text = rRemScript.Replace(text, " ");
            text = rRemHead.Replace(text, " ");
            text = rRemStyle.Replace(text, " ");
            text = rRemImage.Replace(text, " ");
            text = rRemCode.Replace(text, " ");
            text = rRemHtml.Replace(text, " ");
            return text;
        }
    }
}