using Business.Helpers.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Text;
using System.Text.RegularExpressions;

namespace Business
{
    public class HtmlCleaner : IHtmlCleaner
    {
        public IDataResult<string> RemoveHtmlTags(string StringHtmlPage)
        {
            string text = StringHtmlPage.ToLower();
            Regex regexDocType = new Regex(@"<!DOCTYPE[^>]*>");
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
            Regex regexUnicodeCleaner = new Regex(@"&#?[a-z-A-Z-0-9]+;");
            //Regex regexMark = new Regex(@"[>£#$½{@€₺¨~`´ßæ}\|“”‘’!'^+%&/()=?_#½{[\]}\\|\-.,~:;><•*+]*");
            Regex regexMark = new Regex(@"[^a-zA-Z0-9ığĞüÜşŞİöÖçÇ ]");

            #region Regex Replace

            text = regexDocType.Replace(text, " ");
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
            text = regexUnicodeCleaner.Replace(text, " ");
            text = regexAdditional.Replace(text, " ");
            text = regexMark.Replace(text, " ");
            

            #endregion Regex Replace

            return new SuccessDataResult<string>(data: text);
        }

    }
}