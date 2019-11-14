namespace IddaAnalyser
{

    public enum AnalyseResultType
    {
        OddCombination,
        PartialOdd
    }

    public enum PartialOddType
    {
        ALLMS = 0,
        ALLFH = 1,
        ALLDC = 2,
        ALLHND = 3,
        ALLTG = 4,
        ALLMG = 5,
        FHUPDOWN = 6,
        UPDOWN15 = 7,
        UPDOWN25 = 8,
        UPDOWN35  = 9,
        ALLFHMS = 10
    }

    public enum ParentSimType
    {
        SpecialSim = SimType.SpecialSim,
        RandomSim = SimType.RandomSim,
        RandomSimAndValue = SimType.RandomSimAndValue
    }
    public enum SimCombinedSim
    {
        Sim,
        CombinedSim
    }
    public enum SimLeagueResultType
    {
        RandomSim = SimType.RandomSim,
        RandomSimAndValue = SimType.RandomSimAndValue
    }
    public enum CombinedSimType
    {
        CombinedRandomSim = SimType.CombinedRandomSim,
        CombinedRandomSimAndValue = SimType.CombinedRandomSimAndValue,
        CombinedSpecialSim = SimType.CombinedSpecialSim
    }


    public enum SimType
    {
        RandomSim = 0,
        RandomSimAndValue = 1,
        SpecialSim = 2,
        CombinedSpecialSim = 3,
        CombinedRandomSim = 4,
        CombinedRandomSimAndValue = 5
    }
    public enum ExcelColumnType
    {
        MatchTime = 58,
        League = 59,
        MatchCode = 60,
        HomeTeamName = 61,
        AwayTeamName = 62,
        FhScore = 63,
        MsScore = 64,
        FhMsMatchCodeCol = 65,
        MS1 = SpecialSim.MS1,
        MSX = SpecialSim.MSX,
        MS2 = SpecialSim.MS2,
        FH1 = SpecialSim.FH1,
        FHX = SpecialSim.FHX,
        FH2 = SpecialSim.FH2,
        H1 = SpecialSim.H1,
        HX = SpecialSim.HX,
        H2 = SpecialSim.H2,
        TG01 = SpecialSim.TG01,
        TG23 = SpecialSim.TG23,
        TG46 = SpecialSim.TG46,
        TG7 = SpecialSim.TG7,
        DC1X = SpecialSim.DC1X,
        DC12 = SpecialSim.DC12,
        DCX2 = SpecialSim.DCX2,
        DOWNFH15 = SpecialSim.DOWNFH15,
        UPFH15 = SpecialSim.UPFH15,
        DOWN15 = SpecialSim.DOWN15,
        UP15 = SpecialSim.UP15,
        DOWN25 = SpecialSim.DOWN25,
        UP25 = SpecialSim.UP25,
        DOWN35 = SpecialSim.DOWN35,
        UP35 = SpecialSim.UP35,
        MGEXIST = SpecialSim.MGEXIST,
        MGNOTEXIST = SpecialSim.MGNOTEXIST,
        FH1MS1 = SpecialSim.FH1MS1,
        FHXMS1 = SpecialSim.FHXMS1,
        FH2MS1 = SpecialSim.FH2MS1,
        FH1MSX = SpecialSim.FH1MSX,
        FHXMSX = SpecialSim.FHXMSX,
        FH2MSX = SpecialSim.FH2MSX,
        FH1MS2 = SpecialSim.FH1MS2,
        FHXMS2 = SpecialSim.FHXMS2,
        FH2MS2 = SpecialSim.FH2MS2,
    }

    public enum OddItem
    {
        MS1 = SpecialSim.MS1,
        MSX = SpecialSim.MSX,
        MS2 = SpecialSim.MS2,
        FH1 = SpecialSim.FH1,
        FHX = SpecialSim.FHX,
        FH2 = SpecialSim.FH2,
        H1 = SpecialSim.H1,
        HX = SpecialSim.HX,
        H2 = SpecialSim.H2,
        TG01 = SpecialSim.TG01,
        TG23 = SpecialSim.TG23,
        TG46 = SpecialSim.TG46,
        TG7 = SpecialSim.TG7,
        DC1X = SpecialSim.DC1X,
        DC12 = SpecialSim.DC12,
        DCX2 = SpecialSim.DCX2,
        DOWNFH15 = SpecialSim.DOWNFH15,
        UPFH15 = SpecialSim.UPFH15,
        DOWN15 = SpecialSim.DOWN15,
        UP15 = SpecialSim.UP15,
        DOWN25 = SpecialSim.DOWN25,
        UP25 = SpecialSim.UP25,
        DOWN35 = SpecialSim.DOWN35,
        UP35 = SpecialSim.UP35,
        MGEXIST = SpecialSim.MGEXIST,
        MGNOTEXIST = SpecialSim.MGNOTEXIST,
        FH1MS1 = SpecialSim.FH1MS1,
        FHXMS1 = SpecialSim.FHXMS1,
        FH2MS1 = SpecialSim.FH2MS1,
        FH1MSX = SpecialSim.FH1MSX,
        FHXMSX = SpecialSim.FHXMSX,
        FH2MSX = SpecialSim.FH2MSX,
        FH1MS2 = SpecialSim.FH1MS2,
        FHXMS2 = SpecialSim.FHXMS2,
        FH2MS2 = SpecialSim.FH2MS2,
    }

    public enum SpecialSim
    {
        ALL = 0,
        ALLMS = 1,
        ALLFH = 2,
        ALLHANDICAP = 3,
        ALLDC = 4,
        ALLTG = 5,
        ALLUPDOWN = 6,
        ALLMUTUALGOAL = 7,
        ALLFHMS = 8,
        FHUPDOWN = 10,
        UPDOWN15 = 11,
        UPDOWN25 = 12,
        UPDOWN35 = 13,
        FHMS1 = 14,
        FHMSX = 15,
        FHMS2 = 16,
        FH1MS = 17,
        FHXMS = 18,
        FH2MS = 19,
        MS1 = 20,
        MSX = 21,
        MS2 = 22,
        FH1 = 23,
        FHX = 24,
        FH2 = 25,
        H1 = 26,
        HX = 27,
        H2 = 28,
        TG01 = 29,
        TG23 = 30,
        TG46 = 31,
        TG7 = 32,
        DC1X = 33,
        DC12 = 34,
        DCX2 = 35,
        DOWNFH15 = 36,
        UPFH15 = 37,
        DOWN15 = 38,
        UP15 = 39,
        DOWN25 = 40,
        UP25 = 41,
        DOWN35 = 42,
        UP35 = 43,
        MGEXIST = 44,
        MGNOTEXIST = 45,
        FH1MS1 = 46,
        FHXMS1 = 47,
        FH2MS1 = 48,
        FH1MSX = 49,
        FHXMSX = 50,
        FH2MSX = 51,
        FH1MS2 = 52,
        FHXMS2 = 53,
        FH2MS2 = 54
    }

    public enum Result:byte
    {
        MS1 = SpecialSim.MS1,
        MSX = SpecialSim.MSX,
        MS2 = SpecialSim.MS2,
        FH1 = SpecialSim.FH1,
        FHX = SpecialSim.FHX,
        FH2 = SpecialSim.FH2,
        H1 = SpecialSim.H1,
        HX = SpecialSim.HX,
        H2 = SpecialSim.H2,
        TG01 = SpecialSim.TG01,
        TG23 = SpecialSim.TG23,
        TG46 = SpecialSim.TG46,
        TG7 = SpecialSim.TG7,
        DC1X = SpecialSim.DC1X,
        DC12 = SpecialSim.DC12,
        DCX2 = SpecialSim.DCX2,
        DOWNFH15 = SpecialSim.DOWNFH15,
        UPFH15 = SpecialSim.UPFH15,
        DOWN15 = SpecialSim.DOWN15,
        UP15 = SpecialSim.UP15,
        DOWN25 = SpecialSim.DOWN25,
        UP25 = SpecialSim.UP25,
        DOWN35 = SpecialSim.DOWN35,
        UP35 = SpecialSim.UP35,
        MGEXIST = SpecialSim.MGEXIST,
        MGNOTEXIST = SpecialSim.MGNOTEXIST,
        FH1MS1 = SpecialSim.FH1MS1,
        FHXMS1 = SpecialSim.FHXMS1,
        FH2MS1 = SpecialSim.FH2MS1,
        FH1MSX = SpecialSim.FH1MSX,
        FHXMSX = SpecialSim.FHXMSX,
        FH2MSX = SpecialSim.FH2MSX,
        FH1MS2 = SpecialSim.FH1MS2,
        FHXMS2 = SpecialSim.FHXMS2,
        FH2MS2 = SpecialSim.FH2MS2,
        FHDC1X = 55,
        FHDC12 = 56,
        FHDCX2 = 57
    }

}
