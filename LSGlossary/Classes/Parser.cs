using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngleSharp.Parser.Html;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using LSGlossary.Models;

namespace LSGlossary.Classes
{
    public class Parser
    {
        public string GetHtmlPageText(string url)
        {
            string txt = String.Empty;
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            using (Stream stream = resp.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    txt = sr.ReadToEnd();
                }
            }
            return txt;
        }

        public Word OxfordDictionarieParser(string wordName)
        {
            string url = @"https://en.oxforddictionaries.com/definition/" + wordName;
            string htmlStr = GetHtmlPageText(url);
            var parser = new HtmlParser();
            var document = parser.Parse(htmlStr);

            var name = document.QuerySelector("h2.hwg");
            string namePattern = @"([a-z]+)";
            Regex regex = new Regex(namePattern, RegexOptions.IgnoreCase);

            Word word = new Word();
            try
            {
                word.Name = regex.Match(name.TextContent).Value;
                word.Pronunciation = document.QuerySelectorAll("span.phoneticspelling").Last().TextContent;
                word.Definition = document.QuerySelector("span.ind").TextContent;
                word.Example = document.QuerySelector("div.ex").TextContent;
            }
            catch
            {
                return null;
            }
            return word;
        }
    }
}