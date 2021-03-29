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

        public IDataResult<WebSite> SemanticKeywordGeneratorForTarget(WebSite webSite, ref List<SemanticWordJsonDto> Dictionary)
        {
            List<SemanticWord> TempSemanticKeywords = new List<SemanticWord>();
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
                                if (semantic.similarWords.Count > 0)
                                {
                                    TempSemanticKeywords.Add(new SemanticWord
                                    {
                                        word = keyword.word,
                                        similarWords = semantic.similarWords
                                    });
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            webSite.SemanticKeywordsList = TempSemanticKeywords;
            return new SuccessDataResult<WebSite>(data: webSite);
        }

        public IDataResult<WebSite> SemanticKeywordGeneratorForPool(WebSite webSite, WebSite webSitePool)
        {
            webSitePool.SemanticKeywords = new List<SemanticKeyword>();
            webSite.SemanticKeywordsList.ForEach(keyword =>
            {
                keyword.similarWords.ForEach(similar =>
                {
                    webSitePool.Words.ForEach(p =>
                    {
                        if (p.word == similar)
                        {
                            int score = GetWordScore(webSitePool, similar).Data;
                            if (webSitePool.SemanticKeywords.Any(a => a.word == keyword.word))
                            {
                                webSitePool.SemanticKeywords.Single(a => a.word == keyword.word).similarWords.Add(new Keyword
                                {
                                    word = similar,
                                    frequency = p.frequency,
                                    score = score
                                });
                            }
                            else
                            {
                                var tempWord = new Keyword { word = similar, frequency = p.frequency, score = score };
                                webSitePool.SemanticKeywords.Add(new SemanticKeyword
                                {
                                    word = keyword.word,
                                    similarWords = new List<Keyword> { tempWord }
                                });
                            }
                        }
                    });
                });
            });

            return new SuccessDataResult<WebSite>(data: webSitePool);
        }

        public IDataResult<int> GetWordScore(WebSite webSite, string similar)
        {
            int score = 1;
            foreach (var tagAndPoint in _tagAndPointDal.GetAll())
            {
                List<Word> tagWords = ExtractWordInTag(tagAndPoint.before, tagAndPoint.after, webSite.StringHtmlPage).Data;
                if (tagWords.Any(p => p.word == similar))
                {
                    score = tagAndPoint.score;
                }
            }
            return new SuccessDataResult<int>(data: score);
        }

        //Website Operations
        public IDataResult<string> GetTitle(string stringWebSite)
        {
            try
            {
                Regex regexTitleAttr = new Regex("<title[^>]*>[\\s\\S]*</title>");
                Regex regexTitle = new Regex(">[^>]*[^/title>]");
                string tempWebSite = stringWebSite;

                var result = regexTitleAttr.Match(tempWebSite);
                string tempTitle = result.Value.ToString();
                result = regexTitle.Match(tempTitle.ToString());
                tempTitle = result.Value.ToString();
                int lenght = tempTitle.Length - 1;
                string title = tempTitle.Substring(1, lenght - 1);
                return new SuccessDataResult<string>(data: title);
            }
            catch (Exception)
            {
                return new ErrorDataResult<string>(data: "");
            }
        }

        //Similarity Operations
        public IDataResult<InputDto> SimilarityCalculate(WebSite webSite, List<WebSite> webSitePool, bool subUrlCheck = false, bool semanticCheck = false)
        {
            //Similarity calculating
            foreach (var item in webSitePool)
            {
                //MaxValue = 3.40282347E+38F
                float ratelvl1 = 0;
                float lvl1machedKeyword = 0;
                float lvl1allKeyword = 0;

                foreach (var keyword in item.Keywords) // 85% for stage four,five
                {
                    lvl1allKeyword += keyword.score;

                    if (webSite.Keywords.Any(p => p.word == keyword.word))
                        lvl1machedKeyword += keyword.score;
                }
                //if semantic keyword calculate
                if (semanticCheck)
                {
                    var temp = SemanticKeywordGeneratorForPool(webSite, item).Data;
                    item.SemanticKeywords = temp.SemanticKeywords;
                    foreach (var semantic in item.SemanticKeywords)
                    {
                        semantic.similarWords.ForEach(p =>
                        {
                            lvl1allKeyword += p.score * p.frequency;
                            lvl1machedKeyword += p.score * p.frequency;
                        });
                    }
                }
                // if have SubUrl
                if (subUrlCheck) //2.Seviye %20
                {
                    float ratelvl2 = 0;
                    float lvl2MachedKeyword = 0;
                    float lvl2UrlAllKeyword = 0;
                    float ratelvl3 = 0;
                    float lvl3MachedKeyword = 0;
                    float lvl3UrlAllKeyword = 0;

                    //lvl 2
                    foreach (var subUrl in item.SubUrls)
                    {
                        foreach (var keyword in subUrl.Keywords)
                        {
                            lvl2UrlAllKeyword += keyword.score;

                            if (webSite.Keywords.Any(p => p.word == keyword.word))
                            {
                                lvl2MachedKeyword += keyword.score;
                            }
                        }
                        //if semantic keyword calculate
                        if (semanticCheck)
                        {
                            var temp = SemanticKeywordGeneratorForPool(webSite, subUrl).Data;
                            subUrl.SemanticKeywords = temp.SemanticKeywords;
                            foreach (var semantic in subUrl.SemanticKeywords)
                            {
                                semantic.similarWords.ForEach(p =>
                                {
                                    lvl2UrlAllKeyword += p.score * p.frequency;
                                    lvl2MachedKeyword += p.score * p.frequency;
                                });
                            }
                        }
                        if (subUrl.SubUrls.Count > 0) //3.Seviye %10
                        {
                            //lvl 3
                            foreach (var subUrl2 in subUrl.SubUrls)
                            {
                                foreach (var keyword in subUrl2.Keywords)
                                {
                                    lvl3UrlAllKeyword += keyword.score;

                                    if (webSite.Keywords.Any(p => p.word == keyword.word))
                                    {
                                        lvl3MachedKeyword += keyword.score;
                                    }
                                }
                                //if semantic keyword calculate
                                if (semanticCheck)
                                {
                                    var temp = SemanticKeywordGeneratorForPool(webSite, subUrl2).Data;
                                    subUrl2.SemanticKeywords = temp.SemanticKeywords;
                                    foreach (var semantic in subUrl2.SemanticKeywords)
                                    {
                                        semantic.similarWords.ForEach(p =>
                                        {
                                            lvl3UrlAllKeyword += p.score * p.frequency;
                                            lvl3MachedKeyword += p.score * p.frequency;
                                        });
                                    }
                                }
                            }
                            
                        }
                    }
                    if (lvl3UrlAllKeyword == 0) lvl3UrlAllKeyword = 1;
                    if (lvl2UrlAllKeyword == 0) lvl2UrlAllKeyword = 1;
                    if (lvl1allKeyword == 0) lvl1allKeyword = 1;

                    ratelvl3 = (lvl3MachedKeyword * 5)  / (lvl3UrlAllKeyword * 100);
                    ratelvl2 = (lvl2MachedKeyword * 10) / (lvl2UrlAllKeyword * 100);
                    ratelvl1 = (lvl1machedKeyword * 85) / (lvl1allKeyword * 100);
                    item.SimilarityScore = (ratelvl1 + ratelvl2 + ratelvl3) * 100;
                }
                else
                {
                    item.SimilarityScore = (lvl1machedKeyword / lvl1allKeyword) * 100;
                }
                if (float.IsNaN(item.SimilarityScore) || float.IsNegative(item.SimilarityScore))
                {
                    item.SimilarityScore = 0;
                }
            }
            webSitePool = webSitePool.OrderByDescending(p => p.SimilarityScore).ToList();
            return new SuccessDataResult<InputDto>(data: new InputDto { webSite = webSite, webSitePool = webSitePool });
        }
    }
}