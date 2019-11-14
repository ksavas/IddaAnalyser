# IddaAnalyser
C# programlama dili, entity framework, mssql server kullanılarak geliştirilmiş geçmiş idda oranlarındaki benzerlikleri analiz ederek, 
üzerine bahis oynanacak maçın sonucunu, maçın oranlarını analiz sonuçlarına göre geçmişteki oranlarla karşılaştırarak, tahmin eden bir c# form uygulaması.

Deneysel bir uygulama olup, daha çok programlama tekniklerini geliştirme, database operasyonları üzerine çalışma amaçları güdülerek geliştirilmiştir. Uygulama maç sonuçlarını 
%100 oranda asla tahmin edememekle beraber, kesinlikle kullanıldığında para kazandıramaz.

## Uygulama aşamaları
Uygulama işlemlerini 2 ana bölüm altında gerçekleştirmektedir:<br>
Store Tab<br>
Analyse Tab

### Store Tab <br>
Uygulamanın veritabanı kayıt işlemlerinin olduğu bölümde denebilir, diğer analyse tab'da herhangibir veritabanı kayıt işlemi olmaz sadece okuma işlemi yapılır.<br>
Bu bölümde 2 işlem yürütülmektedir:<br>
- Maç verilerini dışardan veri tabanına kaydetmek<br>
- Kaydedilen maçların analiz sonuçlarını kaydetmek

#### Uygulama aşamaları
