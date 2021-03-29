using Entities.Concrete;
using Entities.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public static class InMemoryGlobalSemanticWordDal
    {
        public static List<SemanticWordJsonDto> GlobalSemanticWordList { get; set; }

        public static List<SemanticWordJsonDto> GetGlobalSemanticWordList()
        {
            if (GlobalSemanticWordList != null) { }
            else
            {
                string semanticJsonPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "resource", "semanticJson.json");
                GlobalSemanticWordList = new List<SemanticWordJsonDto>();
                using (StreamReader reader = new StreamReader(semanticJsonPath, Encoding.UTF8, false))
                {
                    List<SemanticWord> semanticWordList = JsonConvert.DeserializeObject<List<SemanticWord>>(reader.ReadToEnd().ToString());
                    List<char> alfabe = new List<char> { 'a', 'b', 'c', 'ç', 'd', 'e',
                                                     'f', 'g', 'ğ', 'h', 'ı', 'i',
                                                     'j', 'k', 'l', 'm', 'n', 'o',
                                                     'ö', 'p', 'q', 'r', 's', 'ş',
                                                     't', 'u', 'ü', 'v', 'w', 'x',
                                                     'y', 'z' };

                    for (int i = 0; i < alfabe.Count; i++)
                    {
                        GlobalSemanticWordList.Add(new SemanticWordJsonDto
                        {
                            letter = alfabe[i],
                            data = new List<SemanticWord>()
                        });
                    }
                    foreach (var kelime in semanticWordList)
                    {
                        int index = GlobalSemanticWordList.FindIndex(p => p.letter == kelime.word[0]);
                        if (index != -1)
                        {
                            GlobalSemanticWordList[index].data.Add(kelime);
                        }
                    }
                }
            }
            return GlobalSemanticWordList;
        }
    }
}