using Business.Helpers.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Text.RegularExpressions;

namespace Business
{
    public class HtmlClearer : IHtmlClearer
    {
        public IDataResult<string> RemoveHtml(string content)
        {
            string text = content.ToLower();
            Regex regexScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            Regex regexHead = new Regex(@"<head[^>]*>[\s\S]*?</head>");
            Regex regexStyle = new Regex(@"<style[^>]*>[\s\S]*?</style>");
            Regex regexCode = new Regex(@"<code[^>]*>[\s\S]*?</code>");
            Regex regexImage = new Regex(@"<img[^>]* />");
            Regex regexHtml = new Regex(@"<(.|\n)*?>");
            Regex regexTab = new Regex(@"\t");
            Regex regexWhiteSpace = new Regex(@"&nbsp;");
            Regex regexNewLine = new Regex(@"<br>");
            Regex regexNewLine2 = new Regex(@"</br>");
            Regex regexNewLine3 = new Regex(@"\n");
            Regex regexRN = new Regex(@"\r\n?|\n");
            Regex regexAdditional = new Regex(@"’[a-z]+");
            Regex regexMark = new Regex(@"\\[“”!'^+%&/()=?_#½{[\]}\\|\-.,,~:;><•*+]");


            #region Regex Replace

            text = regexScript.Replace(text, " ");
            text = regexHead.Replace(text, " ");
            text = regexStyle.Replace(text, " ");
            text = regexCode.Replace(text, " ");
            text = regexImage.Replace(text, " ");
            text = regexTab.Replace(text, " ");
            text = regexWhiteSpace.Replace(text, " ");
            text = regexNewLine.Replace(text, " ");
            text = regexNewLine2.Replace(text, " ");
            text = regexNewLine3.Replace(text, " ");
            text = regexRN.Replace(text, " ");
            text = regexHtml.Replace(text, " ");
            text = ReplaceText(text).Data;

            text = regexAdditional.Replace(text, " ");
            text = regexMark.Replace(text, " ");

            #endregion Regex Replace

           
            return new SuccessDataResult<string>(data:text);
        }
        public IDataResult<string> ReplaceText(string text)
        {
            text = text.Replace("&bull;", " "); // •
            
            text = text.Replace(",", " "); // ,
            text = text.Replace(":", " "); // ,
            text = text.Replace(".", " "); // ,
            text = text.Replace("&#8217;", "'");
            text = text.Replace('"', '\'');
            text = text.Replace("&#39;", "'");
            text = text.Replace("&#x27;", "'");
            text = text.Replace("&#8217;", "\"");
            text = text.Replace("&#8211;", " ");
            text = text.Replace("&#8221;", "\"");
            text = text.Replace("&#8220;", "\"");
            text = text.Replace("&#231;", "ç");
            text = text.Replace("&#199;", "Ç");
            text = text.Replace("&#287;", "ğ");
            text = text.Replace("&#286;", "Ğ");
            text = text.Replace("&#351;", "ş");
            text = text.Replace("&#350;", "Ş");
            text = text.Replace("&#305;", "ı");
            text = text.Replace("&#304;", "İ");
            text = text.Replace("&#252;", "ü");
            text = text.Replace("&#220;", "ü");
            text = text.Replace("&#214;", "Ö");
            text = text.Replace("&#246;", "ö");
            text = text.Replace("&copy;", "©");
            text = text.Replace('\'', ' '); // •
            return new SuccessDataResult<string>(data: text);
        }
    }
}