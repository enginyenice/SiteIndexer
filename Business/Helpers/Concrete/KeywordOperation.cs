using Business.Helpers.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class KeywordOperation : IKeywordOperation
    {
        ITagAndPointDal _tagAndPointDal;
        IHtmlClearer _htmlClearer;
        IWordToExcludeDal _wordToExcludeDal;
        public KeywordOperation(ITagAndPointDal tagAndPointDal, IHtmlClearer htmlClearer, IWordToExcludeDal wordToExcludeDal)
        {
            _tagAndPointDal = tagAndPointDal;
            _htmlClearer = htmlClearer;
            _wordToExcludeDal = wordToExcludeDal;
        }

        public IDataResult<WebSite> KeywordGenerator(WebSite webSite)
        {

            foreach (var tagAndPoint in _tagAndPointDal.GetAll())
            {
                List<Frequance> Frequencies = FrequencyList(tagAndPoint.before, tagAndPoint.after, webSite.StringWebSite).Data;
                foreach (var item in Frequencies)
                {
                    var selectedFrequency = webSite.Frequances.SingleOrDefault(p => p.Keyword == item.Keyword);
                    int piece = (item.Piece * tagAndPoint.point) + selectedFrequency.Piece - item.Piece;
                    selectedFrequency.Piece = piece;

                }
            }
            foreach (var item in webSite.Frequances)
            {
                if (webSite.Keywords.Count >= 10) break;
                webSite.Keywords.Add(item.Keyword);
            }
            return new SuccessDataResult<WebSite>(webSite);
        }
        private IDataResult<List<Frequance>> FrequencyList(string before, string after, string StringWebSite)
        {
            int firstCount = 0;
            int lastCount = 0;
            string allData = "";
            while (firstCount != -1 || lastCount != -1)
            {
                try
                {
                    firstCount = StringWebSite.IndexOf(before, firstCount);
                    lastCount = StringWebSite.IndexOf(after, firstCount) + after.Length;
                    allData += _htmlClearer.RemoveHtml(StringWebSite.Substring(firstCount, (lastCount - firstCount))).Data;
                }
                catch (Exception)
                {

                    break;
                }

                firstCount += 1;
            }
            var result = CreateFrequency(allData);
            return result;
        }
        public IDataResult<List<Frequance>> RemoveWordsToExclude(List<Frequance> frequances)
        {
            List<Frequance> Tempfrequances = new List<Frequance>();
            foreach (var item in frequances)
            {
                if (_wordToExcludeDal.CheckWord(item.Keyword) == false && item.Keyword.Length >= 1)
                {
                    Tempfrequances.Add(item);
                }
            }
            return new SuccessDataResult<List<Frequance>>(Tempfrequances);
        }
        public IDataResult<string> GetTitle(string stringWebSite)
        {
            try
            {
                int titleIndexFirst = stringWebSite.IndexOf("<title>", StringComparison.Ordinal) + 7;
                int titleIndexLast = stringWebSite[titleIndexFirst..].IndexOf("</title>", StringComparison.Ordinal); //8
                return new SuccessDataResult<string>(data: _htmlClearer.ReplaceText(stringWebSite.Substring(titleIndexFirst, titleIndexLast)).Data);
            }
            catch (Exception)
            {
                return new SuccessDataResult<string>(data: "");

            }


        }
        public IDataResult<List<Frequance>> CreateFrequency(string content)
        {

            List<Frequance> frequances = new List<Frequance>();
            var keywords = content.Split(" ");
            foreach (var keyword in keywords)
            {
                if (keyword != "" && keyword != " ")
                {
                    if (frequances.SingleOrDefault(p => p.Keyword == keyword.ToLower()) != null)
                    {
                        var frequance = frequances.SingleOrDefault(p => p.Keyword == keyword.ToLower());
                        frequance.Piece += 1;
                    }
                    else
                    {
                        frequances.Add(new Frequance
                        {
                            Piece = 1,
                            Keyword = keyword.ToLower()
                        });
                    }
                }
            }
            frequances = RemoveWordsToExclude(frequances).Data;
            return new SuccessDataResult<List<Frequance>>(frequances.OrderByDescending(p => p.Piece).ToList());
        }
    }
}
