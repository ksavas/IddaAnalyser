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
Uygulama geliştirme süreci boyunca, oran analizleri üzerine çalışmalar yapıldığından dolayı, maç verilerinin uygulamaya aktarımı aşaması, ihtiyaçlara yeterli miktarda elverecek düzeye getirildikten sonra daha fazla geliştirilmemiştir.
Maç verileri, çeşitli bahis sitelerinde halka açık şekilde verilen iddaa oranları sitelerden kopyalanarak belirli excel dosyasına yapıştırılır.
