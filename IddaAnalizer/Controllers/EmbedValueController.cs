using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IddaAnalyser
{


    public sealed class ConstValues
    {


    }

    public sealed class EmbedValueController
    {
        public static readonly string connectionString = "Data Source=DESKTOP-0ITDCMB\\SQLEXPRESS;Initial Catalog=Matches1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //"Data Source=DESKTOP-0ITDCMB\\SQLEXPRESS;Initial Catalog=Matches1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //"Data Source=|DataDirectory|\\Matches1.db;Version=3;";
        public static readonly string strOddItemSeperator = "|";
        public static readonly char charOddItemSeperator = '|';

        public static readonly string strOddValueSeperator = ":";
        public static readonly char charOddValueSeperator = ':';

        public static readonly int indexOddItem = 0;
        public static readonly int indexoddValue = 1;

        public static readonly int indexLeagueSuccessCountLeagueName = 0;
        public static readonly int indexLeagueSuccessCountTrueCount = 1;
        public static readonly int indexLeagueSuccessCountFalseCount = 2;

        public static readonly string strLeagueSuccessCountSuccessCountSeperator = ":";
        public static readonly char charLeagueSuccessCountSuccessCountSeperator = ':';

        public static readonly string strLeagueSuccessCountLeagueNameSeperator = "|";
        public static readonly char charleagueSuccessCountLeagueNameSeperator = '|';

        public static readonly char charCombinedSimTypeSimIdSeperator = '|';
        public static readonly string strCombinedSimTypeSımIdSeperator = "|";

        public static readonly int indexSimLeagueResultLeagueName = 0;
        public static readonly int indexSimLeagueResultGeneralResultIds = 1;

        public static readonly string strSimLeagueResultSeperator = "|";
        public static readonly char charSimLeagueResultSeperator = '|';

        public static readonly string strSimLeagueResultLeagueNameSeperator = ":";
        public static readonly char charSimLeagueResultLeagueNameSeperator = ':';

        public static readonly string strSimLeagueResultGeneralResultSeperator = ",";
        public static readonly char charSimLeagueResultGeneralResultSeperator = ',';

        public static readonly int intersectedSimLeagueResultIndex = 0;
        public static readonly int currentSimLeagueResultIndex = 1;


        public static readonly IList<OddItem> OddItems = new ReadOnlyCollection<OddItem>(Enum.GetValues(typeof(OddItem)).Cast<OddItem>().ToList());

        public static readonly IList<SimLeagueResultType> simleagueResultTypes = new ReadOnlyCollection<SimLeagueResultType>(Enum.GetValues(typeof(SimLeagueResultType)).Cast<SimLeagueResultType>().ToList());

        public static readonly IList<CombinedSimType> combinedSimTypes = new ReadOnlyCollection<CombinedSimType>(Enum.GetValues(typeof(CombinedSimType)).Cast<CombinedSimType>().ToList());

        public static readonly IList<SimType> allSimTypes = new ReadOnlyCollection<SimType>(Enum.GetValues(typeof(SimType)).Cast<SimType>().ToList());
        public static readonly IList<ParentSimType> parentSimTypes = new ReadOnlyCollection<ParentSimType>(Enum.GetValues(typeof(ParentSimType)).Cast<ParentSimType>().ToList());
        public static readonly IList<SimCombinedSim> parentChildSimTypes = new ReadOnlyCollection<SimCombinedSim>(Enum.GetValues(typeof(SimCombinedSim)).Cast<SimCombinedSim>().ToList());

        public static readonly IList<Result> allResults = new ReadOnlyCollection<Result>(Enum.GetValues(typeof(Result)).Cast<Result>().ToList());

        public static readonly IList<SpecialSim> specialSims = Enum.GetValues(typeof(SpecialSim)).Cast<SpecialSim>().ToList();

        SortedDictionary<int, Odd> SortedOddItems;
        SortedDictionary<int, string> currentLeagueSuccessCounts;
        SortedDictionary<int, string> currentSimLeagueResults;
        private static EmbedValueController _EmbedValueController { get; set; }
        private EmbedValueController()
        {

        }
        public static EmbedValueController GetEmbedValueController
        {
            get
            {
                if (_EmbedValueController == null)
                    _EmbedValueController = new EmbedValueController();

                return _EmbedValueController;
            }
        }

        private SortedDictionary<int, Odd> GetSortedOddItems()
        {
            SortedOddItems = new SortedDictionary<int, Odd>();
            foreach (var oddItem in OddItems)
                SortedOddItems.Add((int)oddItem, new Odd(oddItem));

            return SortedOddItems;
        }
        public SortedDictionary<int, Odd> GetNewOddItems
        {
            get
            {
                return GetSortedOddItems();
            }
        }

        private string GetStrFullOdds(IEnumerable<string> fullOddStrValues)
        {
            return string.Join(strOddItemSeperator, fullOddStrValues);
        }
        public string GetStrFullOdds(SortedDictionary<int, Odd> oddItemValues)
        {
            return GetStrFullOdds(oddItemValues.Values.Select(x => x.StrValue));
        }

        public string GetStrOddItems(SortedDictionary<int, Odd> oddItemValues)
        {
            return string.Join(strOddItemSeperator, oddItemValues.Keys.Cast<OddItem>());
        }

        public SortedDictionary<int, Odd> GetDictionaryFullOdds(string fullOddStrValues)
        {
            List<string> seperatedOddItems = fullOddStrValues.Split(charOddItemSeperator).ToList();
            Dictionary<string, string> dictSeperatedOddItems = new Dictionary<string, string>();
            foreach (var seperatedOddItem in seperatedOddItems)
            {
                string[] aSeperated = seperatedOddItem.Split(charOddValueSeperator);
                dictSeperatedOddItems.Add(aSeperated[indexOddItem],aSeperated[indexoddValue]);
            }
            SortedOddItems = new SortedDictionary<int, Odd>();
            double tempOddValue;
            foreach (var oddItem in OddItems.Where(x => dictSeperatedOddItems.Keys.Contains(x.ToString())))
            {
                tempOddValue = ConvertOddValueToDouble(dictSeperatedOddItems[oddItem.ToString()]);
                SortedOddItems.Add((int)oddItem, new Odd(oddItem,tempOddValue));
            }
            return SortedOddItems;
        }
        public double ConvertOddValueToDouble(string seperatedOddValue)
        {
            seperatedOddValue = seperatedOddValue.Trim();
            seperatedOddValue = seperatedOddValue.Replace(" ", "");
            seperatedOddValue = seperatedOddValue.Replace(".", ",");
            return double.Parse(seperatedOddValue);
        }

        public void SetRandomSimHolder()
        {
            SortedOddItems = new SortedDictionary<int, Odd>();
        }
        public SortedDictionary<int, Odd> GetRandomSims()
        {
            return SortedOddItems;
        }

        public void PutRandomSim(Odd odd)
        {
            SortedOddItems.Add(odd.IntName,odd);
        }

        public string GetStrRandomSims()
        {
            if (SortedOddItems.Count == 0)
                return "";

            return GetStrFullOdds(SortedOddItems);
        }

        public string GetRandomSimOddItems()
        {
            if (SortedOddItems.Count == 0)
                return "";

            return GetStrOddItems(SortedOddItems);
        }

        public string GetCombinedSimsFromDict(SortedSet<int> selectedSims)
        {
            return string.Join(strCombinedSimTypeSımIdSeperator, selectedSims);
        }

        
        public Dictionary<string, HashSet<int>> GetDictSimLeagueResultIds(string leagueResults)
        {
            Dictionary<string, HashSet<int>> seperatedSimLeagueResults = new Dictionary<string, HashSet<int>>();
            string[] aSimLeagueResult;
            string[] aGeneralResultIds;
            if(leagueResults != null)
            {
                List<string> allSimLeagueResults = leagueResults.Split(charSimLeagueResultSeperator).ToList();
                foreach (var allSimLeagueResult in allSimLeagueResults)
                {
                    aSimLeagueResult = allSimLeagueResult.Split(charSimLeagueResultLeagueNameSeperator);
                    seperatedSimLeagueResults.Add(aSimLeagueResult[indexSimLeagueResultLeagueName], new HashSet<int>());

                    aGeneralResultIds = aSimLeagueResult[indexSimLeagueResultGeneralResultIds].Split(charSimLeagueResultGeneralResultSeperator);
                    foreach (var aGeneralResultId in aGeneralResultIds)
                        seperatedSimLeagueResults[aSimLeagueResult[indexSimLeagueResultLeagueName]].Add(int.Parse(aGeneralResultId));
                }
            }
            return seperatedSimLeagueResults;

        }
        public string GetStrSimLeagueResultIds(Dictionary<string, HashSet<int>> dictleagueResults)
        {
            List<string> seperatedLeagueResults = new List<string>();
            string strGeneralResultIds;
            foreach (var dictleagueResult in dictleagueResults)
            {
                strGeneralResultIds = string.Join(strSimLeagueResultGeneralResultSeperator,dictleagueResult.Value);
                currentSimLeagueResults = new SortedDictionary<int, string>()
                {
                    {indexSimLeagueResultLeagueName,dictleagueResult.Key },
                    {indexSimLeagueResultGeneralResultIds,strGeneralResultIds }
                };
                seperatedLeagueResults.Add(string.Join(strSimLeagueResultLeagueNameSeperator,currentSimLeagueResults.Values));
            }
            return string.Join(strSimLeagueResultSeperator, seperatedLeagueResults);
        }

    }
}
