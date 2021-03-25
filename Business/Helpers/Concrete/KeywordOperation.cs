using Business.Helpers.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Business
{
    public class KeywordOperation : IKeywordOperation
    {
        private ITagAndPointDal _tagAndPointDal;
        private IHtmlCleaner _htmlCleaner;
        private IWordToExcludeDal _wordToExcludeDal;

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
            var words = content.Replace("  ", " ").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
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
            Regex numberCheck = new Regex("([0-9])");
            foreach (var item in Words)
            {
                if ((_wordToExcludeDal.CheckWord(item.word) == false && item.word.Length >= 2)
                    ||
                    (item.word.Length == 1 && numberCheck.Match(item.word).Success))
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
            webSite.Keywords = TempKeywords.OrderByDescending(p => p.score).Take(10).ToList();

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

        public IDataResult<WebSite> SemanticKeywordGenerator(WebSite webSite, ref List<SemanticWordJsonDto> Dictionary)
        {
            List<SemanticKeyword> TempSemanticKeywords = new List<SemanticKeyword>();

            //Semantic keyword find
            foreach (var keyword in webSite.Keywords)// website keywords
            {
                List<string> tempSemantic = new List<string>();

                foreach (var part in Dictionary) // dictionary letter 'a' ,'b'....
                {
                    if (part.letter == keyword.word[0])
                    {
                        foreach (var semantic in part.data) // semantic word
                        {
                            if (keyword.word == semantic.word)
                            {
                                semantic.similarWords.ForEach(p => { tempSemantic.Add(p); });
                                break;
                            }
                        }
                        break;
                    }
                }
                if (tempSemantic.Count > 0)
                {
                    tempSemantic.ForEach(p =>
                        webSite.Words.ForEach(m =>
                        {
                            if (m.word == p)
                                TempSemanticKeywords.Add(new SemanticKeyword
                                {
                                    word = p,
                                    similar = keyword.word,
                                    frequency = m.frequency,
                                    score = m.frequency
                                });
                        }
                    ));
                }
            }

            // Keywords score calculate
            foreach (var tagAndPoint in _tagAndPointDal.GetAll())
            {
                List<Word> tagWords = ExtractWordInTag(tagAndPoint.before, tagAndPoint.after, webSite.StringHtmlPage).Data;
                foreach (var semanticKeyword in TempSemanticKeywords)
                {
                    if (tagWords.Any(p => p.word == semanticKeyword.word) == true)
                    {
                        semanticKeyword.score = tagAndPoint.score * semanticKeyword.frequency;
                    }
                }
            }
            webSite.SemanticKeywords = TempSemanticKeywords.OrderByDescending(p => p.score).ToList();

            return new SuccessDataResult<WebSite>(data: webSite);
        }

        //Website Operations
        public IDataResult<string> GetTitle(string stringWebSite)
        {
            try
            {
                Regex regexTitleAttr = new Regex("<title[^>]*>[\\s\\S]*</title>");
                Regex regexTitle = new Regex(">[^>]*[^</title>]");
                string tempWebSite = stringWebSite;

                var result = regexTitleAttr.Matches(tempWebSite);
                string tempTitle = result[0].ToString();
                result = regexTitle.Matches(tempTitle.ToString());
                tempTitle = result[0].ToString();
                int lenght = tempTitle.Length - 1;
                string title = tempTitle.Substring(1, lenght);
                return new SuccessDataResult<string>(data: title);
            }
            catch (Exception)
            {
                return new ErrorDataResult<string>(data: "");
            }
        }
    }
}