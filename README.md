# IddaAnalyser

[English](https://github.com/ksavas/IddaAnalyser/edit/master/README.en.md)

C# programlama dili, entity framework, mssql server kullanılarak geliştirilmiş geçmiş idda oranlarındaki benzerlikleri analiz ederek, 
üzerine bahis oynanacak maçın sonucunu, maçın oranlarını analiz sonuçlarına göre geçmişteki oranlarla karşılaştırarak, tahmin eden bir c# form uygulaması.

Deneysel bir uygulama olup, daha çok programlama tekniklerini geliştirme, database operasyonları üzerine çalışma amaçları güdülerek geliştirilmiştir. Uygulama maç sonuçlarını %100 oranda asla tahmin edememekle beraber, kesinlikle kullanıldığında para kazandıramaz.

## Application stages
The application performs actions in 2 parts:<br>
Store Tab<br>
Analyse Tab

### Store Tab 
All database store operations of the application are run in this tab. There is no any database store operation in the other analyse tab, the analyse tab only performs read operations.<br>
2 operations are applied in this tab:<br>
- Storing Match data into database from the external source.<br>
- Storing the analyse results of the stored matches.
##### Screenshot of the Store Tab
<img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i3.png"><br>
#### Storing Match data into database from the external source
the process of the transfering the match data to appliaction has not been further developed after it has been brought to a sufficient level to meet the needs because of the focused operation is the odd analysing.<br>

The match data is copied from public odd values of various iddaa bet web sites and pasted to predetermined excel file. After that pushing "Store" button on the top-left of the store tab of the application opens a dialog box to decide which excel file do you wan to use. The user selects the predetermined and then the application fetch the match values from the excel file to store the "AnalysedMatches" table of the database.<br>

The future work of this application, I mean if the application had continued to be developed, the data is gotten by connecting some api or requesting some web services.<br>

#### Storing the analyse results of the stored matches.
Actually the title of this part explains the process of what happens when user pushes the import button of the store tab next to store button. 

The working principle of the import button is the user selects a date the from combobox that holds the dates of AnalysedMatches in the database, after selecting the date the matches of this date are listed in the gridview as you see in the figure at above. When matches are listed user clicks the import button.<br>
When user clicks the import button the application analyses the selected AnalysedMatches, stores the analyse results of this matches then stores the selected AnalysedMatches to the Matches table that is our main table for matches and removes the selected AnalysedMatches from the table AnalysedMatches respectively. After that operation, now the selected analysed matches serves to the analyse part of the application.

The various bet companies have various bet values for same result of the specific soccer match. One bet company opens a bet for some situations but another bet company doesn't open a bet for the same situation. The bet companies open a bet for extreme predictions such as Which team will be the first to take a throw-in etc. In this scope I focused to the 35 different result type of match that is opened by almost every bet companies and the experiments were done for these 35 different result type.

I faced with whether the matches have same odds for the 35 different result type but doesn't have same scores, so i focused different approaches to gain same scores for matches. 2 analyses have approached to the purpose. These analyses are:

- OddCombinations
- PartialOdds

#### OddCombinations
The name of the OddCombinations has come from the table name of the saved values. The main motivation of the OddCombinations is the idea of "If a match has some specific odds for the specific result types, the match definitely ends with this score".<br>

For example, lets suppose the 'x' match has Ms1:1.4, Ms2:1.75, Fhx:1.9, Mg+:2.00, 3.5-:1.35 values, we see 5 matches in the Matches table that holds the past matches has same odds for same result types and these all 5 matches end with mutual goal result type. In this situation we bet for mutual goal exist result and hope for both teams to score.

I compared similarities of 28,431 FullOdd(means 35 odd as one piece) thats is obtained from 60,346 matches with each other one by one and stored them to the database. After this operation we had billions of OddCombination but when we filtered OddCombinations that don't have intersected results (For example an OddCombination has 45 match but all af them have different result types) I achieved the decrease the OddCombination count from billions to 1,411,349.

While the application analysing the OddCombinations sometimes finds existing OddCombinations in the database. When it finds existing OddCombinations it updates the OddCombinations matches, results etc. and if updated OddCombination has no intersected result the OddCombination is removed from the database.

#### PartialOdds
If the OddCombination is the final stage of the division of FullOdd then the PartialOdd is the first stage of the division.

The approach of the PartialOdd is, dividing the FullOdd to the odd groups that effects each other. Trying all permutations of this group. If the matches thats relational for the one of the permutation of the groups has at least one intersected result type storing that permutation.

For Example; Ms1,Msx,Ms2 is a effected odds about match score, and Mg+,Mg- is also effected odds about mutual goal. If we want to divide the FullOdd as this approach we can say ALLMS:Ms1,Msx,Ms2 and ALLMG: Mg+,Mg- etc. If we divide FullOdd like this we can have 11 Main header (ALLMS, ALLMG, etc.) and when we the permutations of this ordered headers we will hav (2^11) 2048 permutation. i.e. ALLMS|ALLFH or ALLMS|ALLMG|ALLDC or ALLMS|ALLFH|ALLMG.

I've divided FullOdd in this way for PartialOdd except this approach all process is same with OddCombination.

### Analyse Tab 
We've tried to explain how is analysing done and how to store matches shortly. The analyse tab is the evaluating the analyse result by user part of the application. The user uses this part for 2 purposes:
- Exercising on played matches.
- Evaluating the analyse results on unplayed matches and putting a bet for appropriate matches.

One of the important points of this tab is if the user wants to evaluate a match thats already stored in Matches table, all analyse results about a match will be true and the user never finds a wrong score prediction about the match. This situation increases the confidens about this application and cause for mistakes. So if the user want to evaluate a match he/she have to be sure that the 
match hasn't been stored to the database Matches table.

#### A screenshot from Analyse Tab
<img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i7.png"><br>

As you see in the Analyse tab screenshot the upside part of the UI has these elements: Selected match and its scores, OddCombination and PartialOdd limits, Reseting/saving limits etc.

The underpart of the UI has these elements from left to right: OddCombination results, PartialOdd results, The intersection of OddCombination and PartialOdd results, picked matches, and the list of the analysed matches.

#### Limits of OddCombination PartialOdd
<img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i5.png"><br>

We have explanied the working mechanism of the OddCombination and PartialOdd as you see in above. We will explain it again to understand the limits.

The OddCombination and FullOdd table have many-to-many relation. FullOdd values have all of the 35 odds as one piece. The OddCombinations that comes up from FullOdd specific odd values are related to the FullOdd and also FullOdds are related to the OddCombinations and  FullOdds also related to the matches that has related FullOdd.

When user analysing a match he/she may think the OddCombinations which have only 1 match or 1 FullOdd are not decisive for predicting the result. So the user specifies the min value of both match and FullOdd count as 2 and analyses according to the this analyse results.

The (Result)s are about filtering the analyse result of the under part. The output results are also grouped by Match and FullOdd count. For example, an OddCombination gives Ms1 and Mg+ as an output and another OddCombination gives Ms2 and Mg+ as an output. The application groups the results in different collection such as: [{Ms1:OddCombination1},{Ms2:OddCombination2},{Mg+:OddCombination1,OddCombination2}]
and reaches the FullOdd and matches via OddCombinations. So you might want to see Results between 10-50 Match or FullOddCount in the output results. So thats are used for filtering.

**Analyse Again**&nbsp;Used when you change the limits and analyse again.<br>
**Store Limits**&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Used when you change the limits and store it permanently.<br>
**Reset Limits**&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Used for Changing the limits to the default values (Min:0,Max:int.Max).<br>
**Clear Limits**&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Changes the limits to the default values but doesn't store.<br>

#### Under Part <br><img src="https://raw.githubusercontent.com/ksavas/IddaAnalyser/master/SS/i8.png"><br>
Here, the output results that are coming from OddCombination and PartialOdds are listed. The list in descending order. The Headers:<br>
**Given Result**&nbsp;&nbsp;&nbsp;The output result.<br>
**MCount**&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Match count.<br>
**FCount**&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullOdd count.<br>

**Picked Matches**, when user wishes to select a match he/she is interested in and want to look it again later, he/she will click on the **Pick** button and puts the match into PickedMatches grid. Then when she/he wants to look again to the match the user clicks the match from the Pciked matches and reaches to the match directly. If the user wants to the remove the picked match from picked matches he/she clicks the **UnPick** button to remove it from grid.

**AnalysedMatches list** The list of the matches that are stored in the AnalysedMatches table. Ther are ordered by their dates.<br>
**UnPlayed**&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Lists unplayed matches.<br>
**Played**&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Lists played matches.<br>
If the user wants to search a specific team name, match code etc. the user can use the text box on the analysed matches list.
