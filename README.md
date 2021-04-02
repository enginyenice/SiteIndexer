# Web İndeksleme Uygulaması

Cemre Can Kaya
 Bilgisayar Mühendisliği
 Kocaeli Üniversitesi
 190201137

Engin Yenice
 Bilgisayar Mühendisliği
 Kocaeli Üniversitesi
 190201133

**_Özet_— Verilen bir URL&#39;deki web sayfa içeriğine göre diğer birden fazla web sayfasını benzerlik bakımından indeksleyip sıralayan web tabanlı bir uygulama geliştirmek. Böylece bu proje sayesinde web indeksleme yöntemleri hakkında bilgi edinilmesini ve web tabanlı uygulama yazma becerisinin geliştirilmesi amaçlanmaktadır.**

_ **Anahtar Kelimeler—anahtar, kelime, frekans, havuz, liste, website, link, url, semantic, benzerlik** _

1.
# Giriş

Programın arka planı (backend) **C#** , ön planı(front-end) **Angular Framework**&#39;ü ile geliştirilmiştir. Verilen bir URL&#39;deki web sayfa içeriğine göre diğer birden fazla web sayfasını benzerlik bakımından indeksleyip sıralayan web tabanlı bir uygulama geliştirilmiştir. Bu proje sayesinde web indeksleme yöntemleri hakkında bilgi edinilmiştir.

# Proje Mimarisi

Projenin arka planı (backend) kurumsal mimariye uygun bir şekilde geliştirilmiştir. Proje yapısı gereği 5 temel katmana parçalanmıştır.

Projenin ön planı (front-end) parçala yönet mantığı ile geliştirilmiş olup her bir işlem parçacığı ayrı bir bileşende (component) üzerinde yapılmaya çalışılmıştır.

  
## Arka Plan (Backend) Yapısı

Proje yapısı gereği 5 temel katmana parçalanmıştır. Bu katmanların detayları bu başlık altında açıklanmıştır.


### Core Katmanı

Bu katman proje içerisinde bulunması gereken temel bileşenleri bulundurmaktadır.

#### **Entities:**

Oluşturduğumuz nesnelerin daha somut ve yönetilebilmesi için temel arayüzleri (interface) bu klasör içerisinde tutulmaktadır.

- **IEntity:** Temel nesnelerimizin temel arayüz (interface) sınıfıdır.
- **IDto:** Uzun ismi **Veri iletim nesnesi (Data transfer object)** olarak geçmektedir. Temel nesnelerimizden Kullanıcı arayüzüne (UI (User Interface (Front-end))) göndermek istediğimiz nesne tanımlamalarının temel arayüz (interface) sınıfıdır.

#### **Utilities:**

Projenin genelinde kullanabileceğimiz araçlar bu klasör altında tutulmaktadır.

- **Results:** İş (Business) katmanında kullanacağımız metotların geriye dönüş değerlerinin daha yönetilebilir ve daha düzenli olması için oluşturduğumuz bir araç sınıfıdır. Bu sınıfı temel olarak özetlenecek olursak. İçerisinde temel olarak 2 adet değişken bulunmaktadır. Bu değişkenler mesaj ve başarı (message, success) durumu olarak isimlendirilmektedir. Veri gönderilmesi durumunda miras verdiği alt sınıfta ise data (veri) değişkeninin bulunduğu ayrı bir dönüş tipi bulunmaktadır. Sınıf içerisinde ki değişkenlerin daha kolay yönetilmesi için başarı (success) durumuna göre alt sınıflar oluşturulmuştur. Bu sınıfların çağrılması durumunda başarı durumu otomatik olarak belirlenmektedir.


### Entities Katmanı

Entity ve Dto arayüzlerinden (interface) örnek alan veri sınıfları bulunmaktadır.


#### **Concrete**

Entity arayüzünden (interface) örnek alınan veri sınıfları bulunmaktadır.

#### **Dto:**

Dto arayüzünden (interface) örnek alınan veri sınıfları bulunmaktadır.

### DataAccess Katmanı

Veri havuzunun yönetilmesinden görevli katmandır. Bu katmanımızı ileride geliştirmeye açık olması için arayüzler (interface) kullanarak geliştirdik. Bu sayede ileride bir gerektiğinde başka yapılara geçmeyi planladık. Şuanda veri havuzumuzu bellekte (In Memory) olarak tutuyoruz.


#### Abstract

Veri sınıflarımızın arayüzlerinin tutulduğu klasör.


#### Concrete

Veri sınıflarımızın tutulduğu klasör.

- **WordToExcludeDal:** Anahtar kelime olmasını istemediğimiz kelimelerin listesinin tutulduğu bir listedir.
- **TagAndPointDal:** Site içerisinde belirli etiketlere ait özel puanlama yapılmaktadır. Bu puanlama listesinin puanlarının belirlendiği liste bu sınıfta tutulmaktadır.

| **HTML Etiketler** | **Puan (Her bir kelime için)** |
| --- | --- |
| **Title** | 10 |
| **H1** | **9** |
| **H2** | **8** |
| **H3** | **7** |
| **H4** | **6** |
| **H5** | **5** |
| **H6** | **4** |
| **B** | **3** |
| **Strong** | **3** |
| **U** | **2** |
| **P** | **2** |

- **MemoryGlobalSemanticWord:** Semantik kelimelerin bulunduğu bir listeyi bellekte tutmaktadır. Program ilk açıldığında bu liste hafızaya alınmaktadır.


### Business Katmanı

Projenin iş kodlarının yazıldığı katmandır. Bu gerekli işlemlerin yönetildiği katmandır. Temel olarak 4 klasöre bölünmüştür.


#### Abstract

Business sınıflarımızın arayüzlerinin tutulduğu klasör.

#### Concrete

Business sınıflarımızın tutulduğu klasör.

- **IndexerManager:** WebAPI tarafından gönderilen isteklerin gerekli helper sınıflarına yönlendirilmesini sağlayan ve dönen sonuçların geri WebAPI tarafına yönlendiren sınıftır.

#### DependencyResolvers

Bağımlılıkların çözülmesi ve isim havuzuna aktarılması için kullanılmaktadır. (Paket olarak Autofac kullanılmıştır.)


#### Helpers

Projenin akışını sürdürecek helper sınıfları yazılmıştır. Helper sınıflarımızın arayüzlerinin tutulduğu abstract klasörü ve örnek alındığı concrete klasörü bulunmaktadır.

- **WebSiteOperation:** Gönderilen web site adresi ile iletişim kurmakla görevlidir. Gönderilen web sitelerinin bilgilerini tespit etmekle görevlidir.
- **HtmlCleaner:** Gönderilen web site nesnesi içerisinde bulunan html kodlarının temizlenmesi görevini yapmaktadır.
- **KeywordOperation:** Gönderilen website nesnesi üzerinde kelime, anahtar kelime, frekans, semantic kelime bulma görevlerini yapmaktadır.


### WebAPI katmanı

Ön Plan (Front-end) kısmından gelen istekleri karşılayıp gerekli dönüşleri yapmakla görevli olan katmandır. Ön Plan (Front-end) tarafından 5 farklı isteğe karşılık verebilecek 5 adet Controller bulunmaktadır.


#### StageOneController

Sayfada geçen kelimelerin frekanslarını hesaplayan metotlarını çağırmakla görevlidir. (1. Madde)

#### StageTwoController

Anahtar kelime çıkarma metotlarını çağırmakla görevlidir (2. Madde) [Otomatik olarak 1. Madde ile bağlantılıdır.]


#### StageThreeController

URL ve URL havuzu arasındaki benzerlik skorlaması metotlarını çağırmakla görevlidir (3. Madde) [Otomatik olarak 2. Madde ile bağlantılıdır.]


#### StageFourController

Site indeksleme ve sıralama metotlarını çağırmakla görevlidir. (4. Madde) [Otomatik olarak 3. Madde ile bağlantılıdır.]

#### StageFiveController

Semantik analiz metotlarını çağırmakla görevlidir (5. Madde) [Otomatik olarak 4. Madde ile bağlantılıdır.]


## Ön Plan (Frontend) Yapısı

Projenin kullanıcı ara yüzü (User Interface) ekranıdır. Arka plan (Backend) tarafında yazılan kodların kullanıcıya görüntüsel olarak aktarıldığı taraftır. Angular Framework ile geliştirilmiştir. NG-ZORRO bileşen (component) paketi kullanılmıştır.


### Ana Bileşen (Component) (app.component)

![](RackMultipart20210402-4-d6e3c1_html_260a45aaffa24763.png)
 Projenin çalıştığı ana bileşendir. Bu bileşen içerisinde sabit ve dinamik bileşenler çağrılmaktadır.

Sabit bileşenler direk ana bileşen içerisinden çağrılmaktadır. Dinamik bileşenler ise rotalama (routing) yapısı ile ekrana getirilmektedir.


# Yontem


## Frekans Hesaplaması

Frekans hesaplama algoritması web sitesi içerisinde içerik olarak girilen her metni baz alır. Örneğin başlık etiketi içeriği, paragraf etiketi içeriği, buton isimleri gibi. Web sitesi kaynak kodları içerisinde bulunan tüm html etiketleri ve back-end tarafından gelen tüm kodlardan arındırılır. Regex yani Regular Expression (Düzenli ifade) kullanılarak html sayfası içerisindeki tüm etiketler temizlenir. Geriye kalan veri, web sitesinin tarayıcıda kullanıcıya gösterdiği website içeriğidir.


## Anahtar Kelime Hesaplaması

Anahtar kelime hesaplama algoritması web sitesi içerisinde bulunan kelimelerin, sayfa içerisinde bulunduğu etiketlerin önem sırasına göre hesaplanır. Bazı html etiketleri özel olarak puanlandırılır. Bu etiketler dışında kalan kelimeler ise varsayılan olarak 1 puan değerinde hesaplanır.

| **HTML Etiketler** | **Puan (Her bir kelime için)** |
| --- | --- |
| **Title** | 10 |
| **H1** | **9** |
| **H2** | **8** |
| **H3** | **7** |
| **H4** | **6** |
| **H5** | **5** |
| **H6** | **4** |
| **B** | **3** |
| **Strong** | **3** |
| **U** | **2** |
| **P** | **2** |
| **Varsayılan** | **1** |

Web site içerisinde bulunan tüm kelimelerin puanı, frekansları ile çarpılarak kelimenin önem skoru elde edilir. Tüm kelimeler büyükten küçüğe sıralanır ve ilk 10 kelime web sitesinin anahtar kelimesi olarak seçilir.


## Benzerlik Sıralaması

Benzerlik sıralamasında girilen iki web sitesi için aşama 2 tekrarlanır ve web sitelerin anahtar kelimeleri oluşturulur. Benzerlik testi iki web sitesi içerisindeki anahtar kelimelerden kaçının ortak olması durumunu inceler. Karşılaştırılan web sitenin içerisinde eşleşen anahtar kelime skoru bölü tüm anahtar kelimelerin skoru, web sitenin benzerlik skorunu belirler.

 
## Alt Url&#39; ler ile Benzerlik Sıralaması

Aşama 4&#39; te Aşama 3&#39; te yapılan işleme ilaveten bir web sitesinin birden çok web site ile benzerlik sıralaması hesaplanır ve benzerlik skoru puanına havuzda bulunan web sitelere ek olarak site içerisinde linklenmiş alt URL&#39;leri de dahil edilir.

Bir web sitesinden linklenmiş maksimum 5 alt url incelenir. Bu alt URL&#39;ler 2. katman URL&#39;lerdir. 2. katman URL&#39;lerin de her birinden maksimum 5 alt URL incelenir. Bu alt URL&#39;ler 3.katman URL&#39;leridir.

Alt URL&#39;leri skor hesaplamasına dahil edildiğinde skor formülü şu şekilde güncellenir;

Havuzda bulunan web sitesinin içeriğinin yüzdelik benzerliğinin yüzde 85 i ile tüm 2.katman alt URL&#39;lerin toplam benzerliğinin yüzde 10 u ile tüm 3.katman alt URL&#39;lerin toplam benzerliğinin yüzde 5&#39; inin toplamı havuzda bulunan web sitesinin hedef web siteye olan benzerliğini belirler.

![](RackMultipart20210402-4-d6e3c1_html_f72261fc00b1b554.png)

= 1. Katmandaki =

= 2. Katmandaki =

= 3. Katmandaki =

## Semantik Kelimeler ile Benzerlik Sıralaması

Aşama 5 te yine aşama 4 te yapılanlar tekrarlanır. Aşama 5 te ekstra olarak skor hesaplamasına, hedef olarak girilen web sitenin anahtar kelimelerinin eş anlamlı kelimeleri ile havuzda bulunan web sitelerin eşleşenleri anlamlı kelimeleri web sitenin skor hesaplamasına eklenir.

 ![](RackMultipart20210402-4-d6e3c1_html_ef7973b53c70cc64.png)
 Örneğin: Havuzda bulunan ilk web sitesinin eşleşen anahtar kelimeleri = 0.7 yani %70 olarak benzerlik bulunmuş olsun. Semantik kelimeler bulundukça (50 semantik bir kelime skoru, rastgele bir sayı) şeklinde hesaplamaya eklenir yeni değer = 0.714 yani %71 olarak benzerlik skoru güncellenir.

![](RackMultipart20210402-4-d6e3c1_html_fa5804c2586702b3.png)

= 1. Katmandaki =

i = 1. Katmandaki semantik kelimeler skoru

= 2. Katmandaki =

t = 1. Katmandaki semantik kelimeler skoru

= 3. Katmandaki =

p = 1. Katmandaki semantik kelimeler skoru

# Nasıl Kullanılır

## Temel Birleşenler

### Yardım

 ![](RackMultipart20210402-4-d6e3c1_html_b2f3d7af23ffbbcb.png)
Kullanıcının bulunduğu sayfanın temel işleyişiyle ilgili bilgiler vermektedir.

## Anasayfa

Tüm sayfalar hakkında temel bilgiler vermektedir.

## Frekans (Ödev Madde 1)

Sayfanın ortasında bulunan veri girişi nesnesi (input) içerisine kontrol edilmesi istenen link girilmektedir. Ardından gönder butonuna tıklayarak site hakkında sonuçlara ulaşabilirsiniz.

Gelen sonuç içerisinde temel site bilgileri ve frekans değerleri gösterilmektedir.

Frekans tablosu:

Kelime ve site içerisinde geçen tekrar sayısı gösterilmektedir.

![](RackMultipart20210402-4-d6e3c1_html_9fe2ca4e6f4d1851.png)
 ![](RackMultipart20210402-4-d6e3c1_html_754308eaec88b5be.png)

## Anahtar Kelimeler (Ödev Madde 2)

Sayfanın ortasında bulunan veri girişi nesnesi (input) içerisine kontrol edilmesi istenen link girilmektedir. Ardından gönder butonuna tıklayarak site hakkında sonuçlara ulaşabilirsiniz.

Gelen sonuç içerisinde temel site bilgileri ve belirlenen anahtar kelimeler gösterilmektedir.

 ![](RackMultipart20210402-4-d6e3c1_html_7ab8382c71e67bf5.png)
 Anahtar Kelime Tablosu: Anahtar kelime, site içerisinde geçen tekrar sayısı ve hesaplama sonucu aldığı skor değeri gösterilmektedir.

![](RackMultipart20210402-4-d6e3c1_html_819b451125329ce7.png)

## URL Havuzu (Ödev Madde 3)

Sayfanın ortasında bulunan veri girişi nesnesi (input) içerisine kontrol edilmesi istenen link girilmektedir.

Ardından havuza eklenmesini istediğiniz linkleri havuza ekle butonunu kullanarak ekleyebilirsiniz.

Bir hata ile karşılaşılması durumunda ekranın sağ üst köşesinden çıkan bir açılır pencere (pop-up) ile bilgilendirme sağlanmaktadır.

Havuza eklemiş olduğunuz bir linki sağ tarafında bulunan çöp kutusu görünümündeki butona tıklayarak havuzdan kaldırabilirsiniz.

Ardından başlat butonuna tıklayarak gerekli sonuçları elde edebilirsiniz.

Oluşturulan sonuç ekranında üst tarafta hedeflediğiniz ana site ile ilgiler verilmektedir. Alt tarafta ise havuzunuzun içerisinde bulunan tüm sitelerin bilgileri ve benzerlik skorlaması bulunmaktadır.

![](RackMultipart20210402-4-d6e3c1_html_5d073f5a5e53668d.png)

![](RackMultipart20210402-4-d6e3c1_html_3c6d53ceb7874bc6.png)

![](RackMultipart20210402-4-d6e3c1_html_c7979c5a907e2f0f.png)
 ![](RackMultipart20210402-4-d6e3c1_html_3c6d53ceb7874bc6.png)
 ![](RackMultipart20210402-4-d6e3c1_html_f93051ab029f51e8.png)

## Url – Alt Url Havuzu (Ödev Madde 4)

Sayfanın ortasında bulunan veri girişi nesnesi (input) içerisine kontrol edilmesi istenen link girilmektedir.

Ardından havuza eklenmesini istediğiniz linkleri havuza ekle butonunu kullanarak ekleyebilirsiniz.

 ![](RackMultipart20210402-4-d6e3c1_html_4acd5a264175ee1.png)
 Bir hata ile karşılaşılması durumunda ekranın sağ üst köşesinden çıkan bir açılır pencere (pop-up) ile bilgilendirme sağlanmaktadır.

Havuza eklemiş olduğunuz bir linki sağ tarafında bulunan çöp kutusu görünümündeki butona tıklayarak havuzdan kaldırabilirsiniz.

Ardından başlat butonuna tıklayarak gerekli sonuçları elde edebilirsiniz.

Oluşturulan sonuç ekranında üst tarafta hedeflediğiniz ana site ile ilgiler verilmektedir. Alt tarafta ise havuzunuzun içerisinde bulunan tüm sitelerin bilgileri ve benzerlik skorlaması bulunmaktadır.

![](RackMultipart20210402-4-d6e3c1_html_94fcbb4b296d6f91.png)

Oluşturulan ağacın solunda bulunan + ve – butonlarına tıklayarak tespit edilen alt URL&#39;ler gösterilmektedir. (--- şeklinde gösterilen başlıklar ise sayfada başlık olmadığını belirtmektedir.)

Tespit edilen tüm sayfalar benzerlik skorlamasına göre büyükten küçüğe doğru sıralanmaktadır.

## Semantik (Ödev Madde 5)

Sayfanın ortasında bulunan veri girişi nesnesi (input) içerisine kontrol edilmesi istenen link girilmektedir.

Ardından havuza eklenmesini istediğiniz linkleri havuza ekle butonunu kullanarak ekleyebilirsiniz.

Bir hata ile karşılaşılması durumunda ekranın sağ üst köşesinden çıkan bir açılır pencere (pop-up) ile bilgilendirme sağlanmaktadır.

Havuza eklemiş olduğunuz bir linki sağ tarafında bulunan çöp kutusu görünümündeki butona tıklayarak havuzdan kaldırabilirsiniz.

Ardından başlat butonuna tıklayarak gerekli sonuçları elde edebilirsiniz.

Oluşturulan sonuç ekranında üst tarafta hedeflediğiniz ana site ile ilgiler verilmektedir. Alt tarafta ise havuzunuzun içerisinde bulunan tüm sitelerin bilgileri ve benzerlik skorlaması bulunmaktadır.

Oluşturulan ağacın solunda bulunan + ve – butonlarına tıklayarak tespit edilen alt URL&#39;ler gösterilmektedir. (--- şeklinde gösterilen başlıklar ise sayfada başlık olmadığını belirtmektedir.)

Tespit edilen tüm sayfalar benzerlik skorlamasına göre büyükten küçüğe doğru sıralanmaktadır.

Alt sitelerde bulunan semantik kelimeler ise tespit edildiği sitenin alt bilgileri arasında yer almaktadır.

![](RackMultipart20210402-4-d6e3c1_html_dd1ce47b79e52c24.png)

![](RackMultipart20210402-4-d6e3c1_html_1dedc6f4b2dd1ef1.png)
 ![](RackMultipart20210402-4-d6e3c1_html_7e3d1b7278cf9118.png)

![](RackMultipart20210402-4-d6e3c1_html_520472caad42f69a.png)

# Çevrimiçi Linkler

## Backend (Arka Plan)

### [http://yazlab21.somee.com/](http://yazlab21.somee.com/)

## Frontend (Kullanıcı Arayüzü (UI))

### [https://siteindexer-729f6.web.app/home](https://siteindexer-729f6.web.app/home)

# Kaynakça

### [https://ng.ant.design/docs/introduce/en](https://ng.ant.design/docs/introduce/en)

### [http://angular.io/](http://angular.io/)

### [https://enginyenice.com/](https://enginyenice.com/)

### [https://enginyenice.com/seo-ve-google-analytics/](https://enginyenice.com/seo-ve-google-analytics/)
