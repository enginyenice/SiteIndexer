using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryWordToExcludeDal : IWordToExcludeDal
    {
        List<Word> words;
        public InMemoryWordToExcludeDal()
        {
            words = new List<Word>
            {
            #region Türkçe
                // A
                new Word { word = "a'nî"},
                new Word { word = "ama"},
                new Word { word = "amma"},
                new Word { word = "ancak"},
                new Word { word = "altı"},
                new Word { word = "altmış"},
                new Word { word = "az"},
                new Word { word = "açılmak"},
                new Word { word = "açmak"},
                new Word { word = "ağlamak"},
                new Word { word = "akmak"},
                new Word { word = "almak"},
                new Word { word = "anlamak"},
                new Word { word = "anlatmak"},
                new Word { word = "aramak"},
                new Word { word = "artmak"},
                new Word { word = "aşmak"},
                new Word { word = "atılmak"},
                new Word { word = "atmak"},
                new Word { word = "ayırmak"},
                new Word { word = "ayrılmak"},
                new Word { word = "azalmak"},

               
                // B
                new Word { word = "belki"},
                new Word { word = "bile"},
                new Word { word = "bu"},
                new Word { word = "ben"},
                new Word { word = "biz"},
                new Word { word = "benim"},
                new Word { word = "bunlar"},
                new Word { word = "bir"},
                new Word { word = "beş"},
                new Word { word = "bin"},
                new Word { word = "bağırmak"},
                new Word { word = "bağlamak"},
                new Word { word = "bakmak"},
                new Word { word = "basmak"},
                new Word { word = "başlamak"},
                new Word { word = "beklemek"},
                new Word { word = "belirlemek"},
                new Word { word = "belirlenmek"},
                new Word { word = "belirtmek"},
                new Word { word = "benzemek"},
                new Word { word = "bırakmak"},
                new Word { word = "bilinmek"},
                new Word { word = "bilmek"},
                new Word { word = "binmek"},
                new Word { word = "bitirmek"},
                new Word { word = "bitmek"},
                new Word { word = "bozulmak"},
                new Word { word = "bulmak"},
                new Word { word = "bulunmak"},
                new Word { word = "büyümek"},
                // C
                new Word { word = "cevapla"},

                // Ç
                new Word { word = "çünkü"},
                new Word { word = "çok"},
                new Word { word = "çalışmak"},
                new Word { word = "çalmak"},
                new Word { word = "çekilmek"},
                new Word { word = "çekmek"},
                new Word { word = "çevirmek"},
                new Word { word = "çıkarılmak"},
                new Word { word = "çıkarmak"},
                new Word { word = "çıkmak"},
                new Word { word = "çizmek"},

                
                // D
                new Word { word = "da"},
                new Word { word = "de"},
                new Word { word = "dahi"},
                new Word { word = "daha"},
                new Word { word = "demek"},
                new Word { word = "dışında"},
                new Word { word = "dört"},
                new Word { word = "dokuz"},
                new Word { word = "doksan"},
                new Word { word = "diye"},
                new Word { word = "dayanmak"},
                new Word { word = "değerlendirmek"},
                new Word { word = "değişmek"},
                new Word { word = "değiştirmek"},
                new Word { word = "demek"},
                new Word { word = "dilemek"},
                new Word { word = "dinlemek"},
                new Word { word = "doğmak"},
                new Word { word = "dolaşmak"},
                new Word { word = "doldurmak"},
                new Word { word = "dönmek"},
                new Word { word = "dönüşmek"},
                new Word { word = "durmak"},
                new Word { word = "duymak"},
                new Word { word = "düşmek"},
                new Word { word = "düşünmek"},
                new Word { word = "düzenlemek"},
                new Word { word = "dedi"},

                // E
                new Word { word = "eğer"},
                new Word { word = "encami"},
                new Word { word = "elli"},
                new Word { word = "eklemek"},
                new Word { word = "etkilemek"},
                new Word { word = "etmek"},
                new Word { word = "evlenmek"},



                // F
                new Word { word = "fakat"},
                // G
                new Word { word = "gâh"},
                new Word { word = "gelgelelim"},
                new Word { word = "gibi"},
                new Word { word = "geçirmek"},
                new Word { word = "geçmek"},
                new Word { word = "gelişmek"},
                new Word { word = "geliştirmek"},
                new Word { word = "gelmek"},
                new Word { word = "gerçekleşmek"},
                new Word { word = "gerekmek"},
                new Word { word = "getirmek"},
                new Word { word = "girmek"},
                new Word { word = "gitmek"},
                new Word { word = "giymek"},
                new Word { word = "göndermek"},
                new Word { word = "görmek"},
                new Word { word = "görünmek"},
                new Word { word = "görüşmek"},
                new Word { word = "göstermek"},
                new Word { word = "götürmek"},
                new Word { word = "gülmek"},
                
                // H
                new Word { word = "ha"},
                new Word { word = "hâlbuki"},
                new Word { word = "hatta"},
                new Word { word = "hangisi"},
                new Word { word = "hatırlamak"},
                new Word { word = "hazırlamak"},
                new Word { word = "hazırlanmak"},
                new Word { word = "hissetmek"},

                // I
                // İ
                new Word { word = "ile"},
                new Word { word = "ille"},
                new Word { word = "imdi"},
                new Word { word = "iki"},
                new Word { word = "iyi"},
                new Word { word = "için"},
                new Word { word = "içmek"},
                new Word { word = "ilerlemek"},
                new Word { word = "ilgilenmek"},
                new Word { word = "inanmak"},
                new Word { word = "inmek"},
                new Word { word = "istemek"},
                new Word { word = "istenmek"},
                new Word { word = "izlemek"},
                // J

                // K
                new Word { word = "kâh"},
                new Word { word = "karşın"},
                new Word { word = "kırk"},
                new Word { word = "kötü"},
                new Word { word = "kim"},
                new Word { word = "ki"},
                new Word { word = "kimi"},
                new Word { word = "kimin"},
                new Word { word = "kaçmak"},
                new Word { word = "kaldırmak"},
                new Word { word = "kalkmak"},
                new Word { word = "kalmak"},
                new Word { word = "kapanmak"},
                new Word { word = "kapatmak"},
                new Word { word = "karışmak"},
                new Word { word = "karıştırmak"},
                new Word { word = "karşılamak"},
                new Word { word = "katılmak"},
                new Word { word = "kaybetmek"},
                new Word { word = "kazanmak"},
                new Word { word = "kesilmek"},
                new Word { word = "kesmek"},
                new Word { word = "kılmak"},
                new Word { word = "konuşmak"},
                new Word { word = "korkmak"},
                new Word { word = "korumak"},
                new Word { word = "koşmak"},
                new Word { word = "koymak"},
                new Word { word = "kullanılmak"},
                new Word { word = "kurmak"},
                new Word { word = "kurtarmak"},
                new Word { word = "kurtulmak"},
                new Word { word = "kurulmak"},

                // L
                new Word { word = "lakin"},
                // M
                new Word { word = "madem"},
                new Word { word = "mı"},
                new Word { word = "mi"},
                new Word { word = "mademki"},
                new Word { word = "maydamı"},
                new Word { word = "meğerki"},
                new Word { word = "meğerse"},
                // N
                new Word { word = "neyse"},
                new Word { word = "ne"},
                new Word { word = "nerede"},
                new Word { word = "nereye"},
                new Word { word = "niçin"},
                new Word { word = "neden"},
                new Word { word = "nasıl"},
                // O
                new Word { word = "oysa"},
                new Word { word = "oysaki"},
                new Word { word = "o"},
                new Word { word = "onlar"},
                new Word { word = "onun"},
                new Word { word = "onların"},
                new Word { word = "on"},
                new Word { word = "otuz"},
                new Word { word = "olan"},
                new Word { word = "olarak"},
                new Word { word = "okumak"},
                new Word { word = "olmak"},
                new Word { word = "oluşmak"},
                new Word { word = "oturmak"},
                new Word { word = "oynamak"},


                // Ö
                new Word { word = "ödemek"},
                new Word { word = "öğrenmek"},
                new Word { word = "öldürmek"},
                new Word { word = "ölmek"},

                // P
                new Word { word = "paylaşmak"},

                // R

                // S
                new Word { word = "seksle"},
                new Word { word = "sen"},
                new Word { word = "siz"},
                new Word { word = "senin"},
                new Word { word = "sizin"},
                new Word { word = "şunlar"},
                new Word { word = "sekiz"},
                new Word { word = "seksen"},
                new Word { word = "sağlamak"},
                new Word { word = "sağlanmak"},
                new Word { word = "saymak"},
                new Word { word = "seçmek"},
                new Word { word = "sevmek"},
                new Word { word = "seyretmek"},
                new Word { word = "sokmak"},
                new Word { word = "sormak"},
                new Word { word = "söylemek"},
                new Word { word = "söylenmek"},
                new Word { word = "sunmak"},
                new Word { word = "sunulmak"},
                new Word { word = "sürdürmek"},
                new Word { word = "sürmek"},
                // Ş

                // T
                new Word { word = "tanımak"},
                new Word { word = "taşımak"},
                new Word { word = "toplamak"},
                new Word { word = "toplanmak"},
                new Word { word = "tutmak"},
                new Word { word = "tutulmak"},
                // U 
                new Word { word = "uğraşmak"},
                new Word { word = "ulaşmak"},
                new Word { word = "unutmak"},
                new Word { word = "uygulamak"},
                new Word { word = "uygulanmak"},
                new Word { word = "uymak"},
                new Word { word = "uzanmak"},

                // Ü
                new Word { word = "üç"},
                new Word { word = "üretmek"},

                // V
                new Word { word = "ve"},
                new Word { word = "velakin"},
                new Word { word = "velev"},
                new Word { word = "velhâsıl"},
                new Word { word = "velhâsılıkelâm"},
                new Word { word = "veya"},
                new Word { word = "veyahut"},
                new Word { word = "varmak"},
                new Word { word = "verilmek"},
                new Word { word = "vermek"},
                new Word { word = "vurmak"},
                
                // Y
                new Word { word = "yedi"},
                new Word { word = "yirmi"},
                new Word { word = "yetmiş"},
                new Word { word = "yüz"},
                new Word { word = "yakalamak"},
                new Word { word = "yaklaşmak"},
                new Word { word = "yakmak"},
                new Word { word = "yanmak"},
                new Word { word = "yapılmak"},
                new Word { word = "yapmak"},
                new Word { word = "yaptırmak"},
                new Word { word = "yararlanmak"},
                new Word { word = "yaratmak"},
                new Word { word = "yaşamak"},
                new Word { word = "yatmak"},
                new Word { word = "yayılmak"},
                new Word { word = "yayımlanmak"},
                new Word { word = "yazılmak"},
                new Word { word = "yazmak"},
                new Word { word = "yemek"},
                new Word { word = "yetmek"},
                new Word { word = "yükselmek"},
                new Word { word = "yürümek"},

                // Z
                new Word { word = "zira"},
                #endregion Türkçe
            #region İngilizce
                // A
                new Word { word = "and"},
                new Word { word = "am"},
                new Word { word = "are"},
                // B
                new Word { word = "by"},
                // C
                // D
                // E
                // F
                new Word { word = "follow"},
                // G
                // H
                new Word { word = "he"},
                new Word { word = "her"},
                new Word { word = "has"},
                // I
                new Word { word = "is"},
                new Word { word = "in"},
                new Word { word = "it"},
                // J
                // K
                // L
                // M
                // N
                // O
                new Word { word = "on"},
                // P
                // R
                // S
                new Word { word = "she"},
                // T
                new Word { word = "to"},
                new Word { word = "they"},
                new Word { word = "there"},
                new Word { word = "this"},
                new Word { word = "the"},
                // U
                // V
                // Y
                new Word { word = "your"},
                new Word { word = "you"},
                // Z
            #endregion
            };
        }

        public bool CheckWord(string word)
        {
            if (words.Any(p => p.word == word))
            {
                return true;
            }
            return false;
        }
    }
}
