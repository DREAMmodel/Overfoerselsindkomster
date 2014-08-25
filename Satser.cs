using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overfoerselsindkomster
{
  static class Satser
  {

    //Dagpengesatser
    //static public int[] DpMindsteSats = { 111504, 111930, 114702, 117686, 121524, 125362, 128773, 132824, 136661, 139433, 142204, 145616, 149880, 154570, 160326, 163311, 168002, 171626 }; //talrække starter i 1996
    //static public int[] DpUngeUnder25 = { 71760, 74100, 76440, 78520, 81120, 83460, 85020, 86840, 88920, 91520, 94380, 97760, 99580, 102440, 104780 }; //talrække starter i 1999
    static public int[] DpMaksimalSats = { 132340, 132860, 135980, 136500, 139880, 143520, 148200, 152880, 157040, 161980, 166660, 170040, 173420, 177580, 182780, 188500, 195520, 199160, 204880, 208260, 211900 }; //talrække løber 1994-2014
    static public double[] Timesats = { 133.4900929, 134.0240532, 137.2406305, 137.6523524, 140.9560089, 144.6208651, 149.2486949, 154.0239743, 158.1827291, 163.2452483, 167.9794322, 171.3390101, 174.7657871, 178.96, 185.94, 195.05, 200.51, 205.52, 209.56, 213.19, 217.03 }; //Timesats, hvis ikke timelønnet. Talrække løber 1994-2014

    //Børne- ungeydelse
    static public int[] Buydelse0_2 = { 1416, 1422, 1433, 1468 };
    static public int[] Buydelse3_6 = { 1121, 1125, 1134, 1162 };
    static public int[] Buydelse7_14 = { 882, 886, 893, 915 };
    static public int[] Buydelse15_17 = { 882, 886, 893, 915 };

    static public int[] sdMaksPrUge = { 3940, 4005, 4075 };

    static public int[] fpGrundbeløb = { 68556, 69996, 70896 };
    static public int[] fpPensionstillægEnlige = { 71196, 72696, 73644 };
    static public int[] fpPensionstillægPar = { 34416,	35136, 35592 };
    static public int[] fpFradragvGrundbeløb = { 291200, 297300, 301200 };
    static public int[] fpFradragvTillægEnlig = {64300,	65700, 66500 };
    static public int[] fpFradragvTillægPar = {128900,	131600, 133400 };
    static public int[] fpFradragvArbejde = { 30000, 30000, 60000 };
    static public int[] fpFradragSamlever = { 201100, 204300, 208000 };

    //Kontanthjælp
    static public int[] khOverAldermBørn = { 13732, 13952, 14203 }; //§ 25, stk 1, nr. 1
    static public int[] khOverAlderuBørn = { 10335, 10500, 10689 }; //§ 25, stk 1, nr. 2
    static public int[] khUnderAldermBørnEnlig = { 13732, 13952, 13575 }; //kan det passe at denne falder i 2014?
    static public int[] khUnderAldermBørnPar = { 13732, 13952, 6889 };
    static public int[] khUnderAldermBørnParPåSU = { 13732, 13952, 9498 }; //Par på SU, uddhj. eller kontanthj. § 25, stk. 3, nr. 2
    static public int[] khUnderAlderuBørnUdeboende = { 6660, 6767, 6889 }; //§ 25, stk 1, nr. 3

    //Boligstøtte
    static public int[] bsMaks0Børn = { 74900, 77000, 78800 };
    static public int[] bsMaks1Børn = { 78645, 80900, 80900 };
    static public int[] bsMaks2Børn = { 82390, 84700, 84700 };
    static public int[] bsMaks3Børn = { 86135, 88600, 88600 };
    static public int[] bsMaks4Børn = { 89880, 92400, 92400 };
    static public int[] bsPensionistMaks0Børn = { 81000, 82200, 83700 };
    static public int[] bsPensionistMaks1Børn = { 85050, 86300, 86300 };
    static public int[] bsPensionistMaks2Børn = { 89100, 90400, 90400 };
    static public int[] bsPensionistMaks3Børn = { 93150, 94500, 94500 };
    static public int[] bsPensionistMaks4Børn = { 97200, 98600, 98600 };
    static public int[] bsMindstebeløb = { 22500, 23100, 23700 };
    static public int[] bsPensionistMindstebeløb = { 15300, 15500, 15800 };
    static public int[] bsPensionistTillæg = { 6100, 6200, 6300 };
    static public int[] bsPensionistIndkomstgrænse = { 144300, 146600, 149300 };
    static public int[] bsIndkomstgrænse = { 133500, 137200, 140500 };
    static public int[] bsIndkomstgrænseForhøjelse = { 35200, 36100, 37000 };
    static public int[] bsPensionistIndkomstgrænseForhøjelse = { 38000, 38600, 39300 };
    static public int[] bsMaxYdelse = { 39516, 40620, 41592};
    static public int[] bsPensionistMaxYdelse = { 42720, 43404, 44184 };

    //Efterløn
    static public int[] elBundfradrag = { 14200, 14400, 14700 };

    public static int SygedagpengeMaks(int år)
    {      
      return sdMaksPrUge[år - 2012] * 52 / 12; //omregnes fra uge til år til måned
    }

    public static int SUunder20StartetEfter2013(int år)
    {
      return 906;
    }

    public static int SUunder20StartetFør2013(int år)
    {
      return 1293;
    }

    public static int SUforældreindkomstMin(int år)
    {
      return 320000;
    }

    public static int SUforældreindkomstMax(int år)
    {
      return 680000;
    }

    public static int SUmaximaltTillæg(int år)
    {
      return 1610;
    }

    public static int SUhjemmeboendeEfter2013(int år)
    {
      return 2516;
    }

    public static int SUhjemmeboendeFør2013(int år)
    {
      return 2903;
    }

    public static int SUudeboende(int år)
    {
      return 5839;
    }




  }
}
