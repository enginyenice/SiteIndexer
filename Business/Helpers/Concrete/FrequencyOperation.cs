//Created By Engin Yenice
//enginyenice2626@gmail.com

using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Helpers.Concrete
{
    public class FrequencyOperation
    {
        public static List<Frequance> CreateFrequency(string content)
        {
            KeywordOperation keywordOperation = new KeywordOperation();
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
            frequances = KeywordOperation.RemoveTurkishConjunctions(frequances);
            return frequances.OrderByDescending(p => p.Piece).ToList();
        }
    }
}