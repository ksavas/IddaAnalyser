# IddaAnalyser
C# programlama dili, entity framework, mssql server kullanılarak geliştirilmiş geçmiş idda oranlarındaki benzerlikleri analiz ederek, 
üzerine bahis oynanacak maçın sonucunu, maçın oranlarını analiz sonuçlarına göre geçmişteki oranlarla karşılaştırarak, tahmin eden bir c# form uygulaması.

Deneysel bir uygulama olup, daha çok programlama tekniklerini geliştirme, database operasyonları üzerine çalışma amaçları güdülerek geliştirilmiştir. Uygulama maç sonuçlarını %100 oranda asla tahmin edememekle beraber, kesinlikle kullanıldığında para kazandıramaz.

## Uygulama aşamaları
Uygulama işlemlerini 2 ana bölüm altında gerçekleştirmektedir:<br>
Store Tab<br>
Analyse Tab

### Store Tab 
Uygulamanın veritabanı kayıt işlemlerinin olduğu bölümde denebilir, diğer analyse tab'da herhangibir veritabanı kayıt işlemi olmaz sadece okuma işlemi yapılır.<br>
Bu bölümde 2 işlem yürütülmektedir:<br>
- Maç verilerini dışardan veri tabanına kaydetmek<br>
- Kaydedilen maçların analiz sonuçlarını kaydetmek
##### Store Tab Ekran Görüntüsü
<img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i3.png"><br>
#### Maç verilerinin dışarıdan kaydedilmesi
Uygulama geliştirme süreci boyunca, oran analizleri üzerine çalışmalar yapıldığından dolayı, maç verilerinin uygulamaya aktarımı aşaması, ihtiyaçlara yeterli miktarda elverecek düzeye getirildikten sonra daha fazla geliştirilmemiştir.<br>

Maç verileri, çeşitli bahis sitelerinde halka açık şekilde verilen iddaa oranları sitelerden kopyalanarak önceden belirlenmiş excel dosyasına yapıştırılır. Daha sonra Store Tab Ekran Görüntüsünde sol üst köşedeki Store Tuşuna tıklanıldığında, açılan diyalog kutusundan  önceden belirlenmiş excel dosyası seçilir maç verilerini alır ve veritabanında 'AnalysedMatches' tablosuna kaydeder.<br>

Eğer uygulama geliştirilmeye devam etseydi, bir api'ye bağlanılarak veya çeşitli web servislerden faydalanılarak veriler doğrudan request ile alınması planlanıyordu.<br>

#### Kaydedilen maçların analiz sonuçlarını kaydetmek
Aslında burada 'Store Tab Ekran Görüntüsü'nde sol üst köşede 'Store' butonu yanında yer alan 'Import' butonuna basıldığında yapılan işlemler anlatılıyor. 

Import butonunun çalışma prensibi, Tahilerin bulunduğu combobox'dan oynanmış ve sonuçları alınmış maçların olduğu bir tarih seçilir, o tarihteki maçlar listelenir, 'Import' tuşuna basıldığında seçilen maçları, önce analiz eder ve sonuçlarını kaydeder, ardından maçları  'AnalysedMatches' tablosundan alıp, Esas tablo'muz olan 'Matches' tablosonua uygun bir şekilde kaydeder daha sonrada seçilen maçları 'analysedMatches' tablosundan siler. Artık maçlarımız analiz aşamasına hizmet etmeye başlamıştır.

Store operasyonuyla veritabanına kaydedilmiş olan maçlardan sonuçlanmış olanlarının analiz edilerek analiz sonuçlarının veri tabanında ilgili tablolara kaydedilmesi işleminin yapıldığı yerdir.

Bir maç için çeşitli bahis platformları çeşitli oranlar verebilirler; aynı sonuç için farklı oranlar verebilirler, birinin oran verdiği bir sonuca bir başkası oran vermeyebilir, ilk taç atışını hangi takım yapar vs. şeklinde uçuk bahisler için bile oranlar verilebilir. Bu kapsamda uygulama geliştirilirken genel olarak her bahis platformunun bir maça kesin olarak verdiği 35 farklı sonuç türünün oranları üzerinden denemeler yapıldı.

Uygulama geliştirme sürecinde maçların bütün oranlarının aynı olmasının maçların aynı şekilde bitmediğini gösterdiğinden dolayı farklı yaklaşımlar sürekli denenmiştir.

Aylarca süren denemeler sonucunda birkaç farklı analizin sonuca götürmeye daha yakın olduğu kanısına varıldığı için, bu bölümde farklı farklı bir kaç operasyon yapılır ve hepsinin sonuçları farklı tablolara kaydedilir. Bu analizler:
- OddCombinations
- PartialOdds

##### OddCombinations
Veritabanında kaydedildiği tablonun adıyla anılan 'OddCombinations' "eğer bir maç belirli sonuçlar için belirli oranları almış ise kesinlikle şu şekilde biter" düşüncesiyle geliştirilmiş bir analiz biçimidir.<br>
Örnek olarak, x maçının Ms1:1.4, Ms2:1.75, Fhx:1.9, Mg+:2.00, 3.5-:1.35 oranlarını aldığını düşünelim, veritabanına kaydedilmiş maçlar arasından aynı sonuçlar için 5 tane maçın aynı oranları aldığını ve hepsinin mg+(Karşılıklı gol var) sonucuyla bittiğini görüyoruz. Bizde maçımıza mg+ sonucunu oynayıp, maçın aynı şekilde bitmesini ümit ediyoruz. 

Oddcombinations verisine ulaşmak için 60,346 maçtan elde edilen, 28,431 adet oran bütününün hepsini teker teker birbirleriyle karşılaştırıp, oranlardaki benzerliklerin hepsini kaydettik, bunun sonucunda milyarlarca data üretildi, ancak aralarında benzerlik bulunan oran bütünlerinin bağlı oldukları maçların ortak bir sonucu yoksa o oran benzerliklerini(OddCombinations) sildik. Bu sayede OddCombinations sayısını milyarlardan 1,411,349 sayısına kadar indirmeyi başardık. 

Uygulama OddCombination analizini yaparken bazen OddCombinatons tablosunda var olan OddCombination'lar bulabiliyor, bunlar'a update işlemi gerçekleştiriliyor ve update sonucunda değerleri değişen OddCombinationlar eğer ortak bir maç sonucu içermiyorsa veritabanından siliniyor.

##### PartialOdds
