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

Bir maç için çeşitli bahis platformları çeşitli oranlar verebilirler; aynı sonuç için farklı oranlar verebilirler, birinin oran verdiği bir sonuca bir başkası oran vermeyebilir, ilk taç atışını hangi takım yapar vs. şeklinde uçuk bahisler için bile oranlar verilebilir. Bu kapsamda uygulama geliştirilirken genel olarak her bahis platformunun bir maça kesin olarak verdiği 35 farklı sonuç türünün oranları üzerinden denemeler yapıldı.

Uygulama geliştirme sürecinde maçların bütün oranlarının aynı olmasının maçların aynı şekilde bitmediğini gösterdiğinden dolayı farklı yaklaşımlar sürekli denenmiştir.

Aylarca süren denemeler sonucunda iki analizin doğru sonuca götürmeye daha yakın olduğu kanısına varıldığından dolayı bu bölümde farklı farklı bir kaç operasyon yapılır ve hepsinin sonuçları farklı tablolara kaydedilir. Bu analizler:
- OddCombinations
- PartialOdds

#### OddCombinations
Veritabanında kaydedildiği tablonun adıyla anılan 'OddCombinations' "eğer bir maç belirli sonuçlar için belirli oranları almış ise kesinlikle şu şekilde biter" düşüncesiyle geliştirilmiş bir analiz biçimidir.<br>

Örnek olarak, x maçının Ms1:1.4, Ms2:1.75, Fhx:1.9, Mg+:2.00, 3.5-:1.35 oranlarını aldığını düşünelim, veritabanına kaydedilmiş maçlar arasından aynı sonuçlar için 5 tane maçın aynı oranları aldığını ve hepsinin mg+(Karşılıklı gol var) sonucuyla bittiğini görüyoruz. Bizde maçımıza mg+ sonucunu oynayıp, maçın aynı şekilde bitmesini ümit ediyoruz. 

Oddcombinations verisine ulaşmak için 60,346 maçtan elde edilen, 28,431 adet oran bütününün hepsini teker teker birbirleriyle karşılaştırıp, oranlardaki benzerliklerin hepsini kaydettik, bunun sonucunda milyarlarca data üretildi, ancak aralarında benzerlik bulunan oran bütünlerinin bağlı oldukları maçların ortak bir sonucu yoksa o oran benzerliklerini(OddCombinations) sildik. Bu sayede OddCombinations sayısını milyarlardan 1,411,349 sayısına kadar indirmeyi başardık. 

Uygulama OddCombination analizini yaparken bazen OddCombinatons tablosunda var olan OddCombination'lar bulabiliyor, bunlar'a update işlemi gerçekleştiriliyor ve update sonucunda değerleri değişen OddCombinationlar eğer ortak bir maç sonucu içermiyorsa veritabanından siliniyor.

#### PartialOdds
OddCombinations'a bütün oranın parçalanmasının son evresi dersek, PartialOdds'a da ilk parçalama evresi demek bir hata olmaz. 

PartialOdds'da yaklaşım; maçın bütün oranını, birbirini etkileyen oran parçalarına bölerek, bu parçaların oluşturduğu bütün permutasyonları deneyerek, aynı OddCombinations'da olduğu gibi, eğer bağlı oldukları maçlar arasında en azından bir ortak sonuç varsa o permutasyonu kaydetmektir.

Örnek olarak, Ms1,Msx,Ms2 maç sonucuyla alakalı ve birbirini etkileyen oranlardır, aynı şekilde Mg+,Mg- karşılıklı gol ile alakalı oranlardır ve bunlarda birbirlerini etkiler. Bütün bir oranı bu şekilde bölmeye çalışırsak, ALLMS:Ms1,Msx,Ms2 ve ALLMG:Mg+,Mg- şeklinde bölebiliriz. Bütün bir oranı bu şekilde parçaladığımızda 11 tane ana başlıkla (ALLMS gibi) karşılaşıyoruz. Bunlarında permutasyonunu aldığımız zaman (2<sup>11</sup>) 2048 tane permutasyona ulaşırız, sadece 11 permutasyondan hiçbirinin olmadığı "0-0-0-0-0-0-0-0-0-0-0-0" permutasyonunu çıkardığımız zaman 2047 permutasyona ulaşırız. Örnek permutasyonlar: ALLMS|ALLFH veya ALLMS|ALLMG|ALLDC veya ALLMS|ALLFH|ALLMG ... şeklindedir.

PartialOdd'da maçlar ve oranlar bu şekilde ayrıştırılır ve bunun dışında OddCombinations'la aynı yaklaşım uygulanır.

### Analyse Tab 
Uygulamanın, Yukarıda nasıl analiz edildiği anlatılan analiz sonuçlarının kullanıcı tarafından değerlendirme yaptığı bölümdür. Kullanıcı bu bölümü 2 amaç için kullanabilir:
- Oynanmış maçları test ederek kendince analiz yapmak.
- Oynanmamış maçlar üzerinde, analiz sonuçlarını değerlendirip, uygun gördüğü maçlara ve sonuçlarına bahis oynamak.

Burada önemli nokta şudur, kullanıcı zaten veritabanında kayıtlı olan bir maçı test etmek isterse gelen sonuçlar arasında hiç yanlış cevap olmayacaktır. Buda kullancıyı hataya sürükleyebilir. Yani kullanıcı oynanmış maçlar üzerinde test yapmak istiyorsa o maçların veritabanına 'import edilmediğinden' emin olması gerekir.

#### Analyse Tab Ekran Görüntüsü
<img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i4.png"><br>

Analyse Tab Ekran Görüntüsünde görüldüğü üzere, üst kısımda seçilmiş olan maç ve sonucu, OddCombination ve PartialOdd Limitleri, Limitleri resetleme kaydetme operasyonları yer alıyor.

Alt tarafta soldan sağa doğru: OddCombination sonuçları, PartialOddSonuçları, OddCombination ve PartialOdd'dan gelen sonuçların kesişimi (intersection), Picked Matches ve analiz edilecek maçların listesi bulunuyor.

#### OddCombination ve PartialOdd Limitleri
<img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i5.png"><br>

OddCombination ve PartialOdd'ların nasıl çalıştığından yukarıda kısaca bahsetmiştik, burada yine kısa bir şekilde, limitlerin anlaşılması açısından, tekrar bahsedeceğiz.

OddCombination değerleri veritabanı tasarımında 'FullOdd' adını verdiğimiz bütün oran değerlerinin bulunduğu tablo değerlerine bağlıdır: FullOdd tablosu içindeki her satırda 35 adet oranın tamamı bulunur. ve bu 35 orandan çıkan ve spesifik sonuca giden oran kombinasyonları(OddCombinations)'da dolayısıyla bu 35 oranı tutan ve aynı kombinasyonları tutan diğer FullOddlara bağlıdır. Aynı şekilde FullOdd'larda birebir aynı oranları alan maçlara bağlıdır.

Kullanıcı, analiz ve değerlendirme yaparken sadece 1 maç oynanmış veya sadece 1 FullOdd'dan gelen sonuçların çok belirleyici olmayacağını düşünerek OddCombinations'da FullOdd ve Match için Min olarak 2 değerini seçer ve analiz sonuçlarını ona göre değerlendirebilir.

Buradaki (Result) değerleri ise OddCombination veya PartialOdd'lardan gelen sonuçlar ayrıca kaydedilir ve toplam Match ve FullOdd sayıları toplanır, onların değerlerine sınırlama getirmek için kullanılır. Aslında aşağıda bulunan, sonuçların listelendiği bölümü, filtreleme amacı için kullanıldığını söylemek daha doğru olacaktır.

**Analyse Again** :Bir maç için çeşitli limitleri kullanıp tekrardan analiz etmek için kullanılır.
**Store Limits**  :Seçilen limitleri daimi olarak belirlemek için kullanılır.
**Reset Limits**  :Limitleri varsayılan değerlerine getirmek için (Min:0, Max:int.Max) kullanılır.
**Clear Limits**  :Limitleri varsayılan değerlerine getirir ama veritabanına o şekilde kaydetmez.

#### Alt Bölüm <br><img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i6.png"><br>
Burada OddCombination ve PartialOdd'dan gelen sonuçlar listelenir. Liste çok'tan aza doğru sıralıdır. Bulunan değeler:
**Given Result**:Gelen sonuç.<br>
**MCount**      :Maç sayısı.<br>
**FCount**      :FullOdd sayısı.<br>

**Picked Matches**, kullanıcı bahis oynayacağı zaman ilgilendiği bir maçı daha sonra tekrar bakmak üzere seçmek istediği zaman **Pick** butonuna tıklayarak PickedMatches grid'i içine atar daha sonra doğrudan ordan tıklayarak maça ulaşabilir. Kullanıcı isterse daha sonra seçtiği maçı **UnPick** ederek grid'den kaldırabilir.

**Analiz maçları listesi** 'AnalysedMatches' tablosunda bulunan maçlar burada listelenir, Tarihlerine göre sıralıdırlar.<br>
**UnPlayed**&nbsp;&nbsp; Sadece oynanmamış maçları gösterir.<br>
**Played**&nbsp;&nbsp;   Sadece oynanmış maçları gösterir.<br>
Kullanıcı isterse yukarıdaki textbox'a arama kriterlerini yazarak maçları filtreleyebilir.
