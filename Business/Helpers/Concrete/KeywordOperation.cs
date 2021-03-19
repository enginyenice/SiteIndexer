using Business.Helpers.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Business
{
    public class KeywordOperation : IKeywordOperation
    {
        ITagAndPointDal _tagAndPointDal;
        IHtmlCleaner _htmlCleaner;
        IWordToExcludeDal _wordToExcludeDal;

        public KeywordOperation(ITagAndPointDal tagAndPointDal, IHtmlCleaner htmlClearer, IWordToExcludeDal wordToExcludeDal)
        {
            _tagAndPointDal = tagAndPointDal;
            _htmlCleaner = htmlClearer;
            _wordToExcludeDal = wordToExcludeDal;
        }

        //Frequency Generater
        public IDataResult<List<Word>> FrequencyGenerater(string content)
        {
            List<Word> tempWordsList = new List<Word>();
            var words = content.Split(" ");
            foreach (var word in words)
            {
                if (word != "" && word != " ")
                {
                    if (tempWordsList.SingleOrDefault(p => p.word == word.ToLower()) != null)
                    {
                        var frequance = tempWordsList.SingleOrDefault(p => p.word == word.ToLower());
                        frequance.frequency += 1;
                    }
                    else
                    {
                        tempWordsList.Add(new Word
                        {
                            word = word.ToLower(),
                            frequency = 1
                        });
                    }
                }
            }
            tempWordsList = RemoveWordsToExclude(tempWordsList).Data;
            return new SuccessDataResult<List<Word>>(tempWordsList.OrderByDescending(p => p.frequency).ToList());
        }
        public IDataResult<List<Word>> RemoveWordsToExclude(List<Word> Words)
        {
            List<Word> tempWordsList = new List<Word>();
            foreach (var item in Words)
            {
                if (_wordToExcludeDal.CheckWord(item.word) == false && item.word.Length >= 1)
                {
                    tempWordsList.Add(item);
                }
            }
            return new SuccessDataResult<List<Word>>(tempWordsList.ToList());
        }
        //Keyword Generator
        public IDataResult<WebSite> KeywordGenerator(WebSite webSite)
        {
            List<Keyword> TempKeywords = new List<Keyword>();

            foreach (var word in webSite.Words)
            {
                //Default score = 1
                TempKeywords.Add(new Keyword { word = word.word, frequency = word.frequency, score = (1 * word.frequency) });
            }

            // Keywords score calculate
            foreach (var tagAndPoint in _tagAndPointDal.GetAll())
            {
                List<Word> tagWords = ExtractWordInTag(tagAndPoint.before, tagAndPoint.after, webSite.StringHtmlPage).Data;
                foreach (var keyword in TempKeywords)
                {
                    if (tagWords.Any(p => p.word == keyword.word) == true)
                    {
                        keyword.score = tagAndPoint.score * keyword.frequency;
                    }
                }
            }
            webSite.Keywords = TempKeywords.OrderByDescending(p => p.score).Take(20).ToList();

            return new SuccessDataResult<WebSite>(webSite);
        }
        private IDataResult<List<Word>> ExtractWordInTag(string before, string after, string StringHtmlPage)
        {
            int firstIndex = 0;
            int lastIndex = 0;
            string stringTagWords = "";
            while (firstIndex != -1 || lastIndex != -1)
            {
                try
                {
                    firstIndex = StringHtmlPage.IndexOf(before, firstIndex);
                    lastIndex = StringHtmlPage.IndexOf(after, firstIndex) + after.Length;
                    stringTagWords += _htmlCleaner.RemoveHtmlTags(StringHtmlPage.Substring(firstIndex, (lastIndex - firstIndex))).Data;
                }
                catch (Exception)
                {
                    break;
                }
                firstIndex += 1;
            }
            var words = stringTagWords.Split(" ");
            List<Word> tagWords = new List<Word>();
            foreach (var word in words)
            {
                tagWords.Add(new Word { word = word });
            }
            return new SuccessDataResult<List<Word>>(tagWords);
        }
        //Website Operations
        public IDataResult<string> GetTitle(string stringWebSite)
        {
            try
            {
                int titleIndexFirst = stringWebSite.IndexOf("<title>", StringComparison.Ordinal) + 7;
                int titleIndexLast = stringWebSite[titleIndexFirst..].IndexOf("</title>", StringComparison.Ordinal); //8
                return new SuccessDataResult<string>(data: stringWebSite.Substring(titleIndexFirst, titleIndexLast));
            }
            catch (Exception)
            {
                return new ErrorDataResult<string>(data: "");
            }
        }
        public IDataResult<WebSite> GetSubWebSite(WebSite webSite)
        {
            webSite.SubUrl = new WebSite
            {
                Url = "https://stackoverflow.com/questions/2248411/get-all-links-on-html-page"
            };

            return new SuccessDataResult<WebSite>(data: webSite);
        }

    }
}
