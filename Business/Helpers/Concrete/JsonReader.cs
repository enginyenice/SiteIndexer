using Business.Helpers.Abstract;
using Core.Utilities.Results;
using Entities.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace Business.Helpers.Concrete
{
    public class JsonReader : IJsonReader
    {
        private string semanticJsonPath = Path.Combine(Environment.CurrentDirectory, "wwwroot\\resource", "semanticJson.json");

        public IDataResult<List<SemanticWordJsonDto>> getSemanticKeywords()
        {
            using (StreamReader reader = new StreamReader(semanticJsonPath, Encoding.UTF8, false))
            {
                string semanticJson = WebUtility.HtmlDecode(reader.ReadToEnd());
                List<SemanticWord> semanticWordList = JsonConvert.DeserializeObject<List<SemanticWord>>(semanticJson);

                List<char> alfabe = new List<char> { 'a', 'b', 'c', 'ç', 'd', 'e', 
                                                     'f', 'g', 'ğ', 'h', 'ı', 'i', 
                                                     'j', 'k', 'l', 'm', 'n', 'o', 
                                                     'ö', 'p', 'q', 'r', 's', 'ş', 
                                                     't', 'u', 'ü', 'v', 'w', 'x', 
                                                     'y', 'z' };
                List<SemanticWordJsonDto> dictionary = new List<SemanticWordJsonDto>();

                for (int i = 0; i < alfabe.Count; i++){
                    dictionary.Add(new SemanticWordJsonDto{
                        letter = alfabe[i],
                        data = new List<SemanticWord>()
                    });
                }
                foreach (var kelime in semanticWordList ){
                    int index = dictionary.FindIndex(p => p.letter == kelime.word[0]);
                    if(index != -1)
                    {
                        dictionary[index].data.Add(kelime);
                    }
                }
                return new SuccessDataResult<List<SemanticWordJsonDto>>(data: dictionary);
            }      
        }
    }
}
