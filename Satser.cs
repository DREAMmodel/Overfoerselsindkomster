using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overfoerselsindkomster
{
  static class Satser
  {
    private static int year = 2012;

    //Dagpengesatser
    //static public int[] DpMindsteSats = { 111504, 111930, 114702, 117686, 121524, 125362, 128773, 132824, 136661, 139433, 142204, 145616, 149880, 154570, 160326, 163311, 168002, 171626 }; //talrække starter i 1996
    //static public int[] DpUngeUnder25 = { 71760, 74100, 76440, 78520, 81120, 83460, 85020, 86840, 88920, 91520, 94380, 97760, 99580, 102440, 104780 }; //talrække starter i 1999
    static int[] _DpMaksimalSats = { 132340, 132860, 135980, 136500, 139880, 143520, 148200, 152880, 157040, 161980, 166660, 170040, 173420, 177580, 182780, 188500, 195520, 199160, 204880, 208260, 211900 }; //talrække løber 1994-2014
    public static int DpMaksimalSats()
    {
      return _DpMaksimalSats[year - 1994];
    }

    static double[] _Timesats = { 133.4900929, 134.0240532, 137.2406305, 137.6523524, 140.9560089, 144.6208651, 149.2486949, 154.0239743, 158.1827291, 163.2452483, 167.9794322, 171.3390101, 174.7657871, 178.96, 185.94, 195.05, 200.51, 205.52, 209.56, 213.19, 217.03 }; //Timesats, hvis ikke timelønnet. Talrække løber 1994-2014
    public static double Timesats()
    {
      return _Timesats[year - 1994];
    }

    //Børne- ungeydelse
    static int[] _Buydelse0_2 = { 1416, 1422, 1433, 1468 };
    public static int Buydelse0_2()
    {
      return _Buydelse0_2[year - 2011];
    }
    static int[] _Buydelse3_6 = { 1121, 1125, 1134, 1162 };
    public static int Buydelse3_6()
    {
      return _Buydelse3_6[year - 2011];
    }
    static int[] _Buydelse7_14 = { 882, 886, 893, 915 };
    public static int Buydelse7_14()
    {
      return _Buydelse7_14[year - 2011];
    }
    static int[] _Buydelse15_17 = { 882, 886, 893, 915 };
    public static int Buydelse15_17()
    {
      return _Buydelse15_17[year - 2011];
    }

    static int[] _sdMaksPrUge = { 3940, 4005, 4075 };
    public static int sdMaksPrUge()
    {
      return _sdMaksPrUge[year - 2012];
    }


    static int[] _fpGrundbeløb = { 68556, 69996, 70896 };
    public static int fpGrundbeløb()
    {
      return _fpGrundbeløb[year - 2012];
    }

    static int[] _fpPensionstillægEnlige = { 71196, 72696, 73644 };
    public static int fpPensionstillægEnlige()
    {
      return _fpPensionstillægEnlige[year - 2012];
    }

    static int[] _fpPensionstillægPar = { 34416, 35136, 35592 };
    public static int fpPensionstillægPar()
    {
      return _fpPensionstillægPar[year - 2012];
    }

    static int[] _fpFradragvGrundbeløb = { 291200, 297300, 301200 };
    public static int fpFradragvGrundbeløb()
    {
      return _fpFradragvGrundbeløb[year - 2012];
    }

    static int[] _fpFradragvTillægEnlig = { 64300, 65700, 66500 };
    public static int fpFradragvTillægEnlig()
    {
      return _fpFradragvTillægEnlig[year - 2012];
    }

    static int[] _fpFradragvTillægPar = { 128900, 131600, 133400 };
    public static int fpFradragvTillægPar()
    {
      return _fpFradragvTillægPar[year - 2012];
    }

    static int[] _fpFradragvArbejde = { 30000, 30000, 60000 };
    public static int fpFradragvArbejde()
    {
      return _fpFradragvArbejde[year - 2012];
    }

    static int[] _fpFradragSamlever = { 201100, 204300, 208000 };
    public static int fpFradragSamlever()
    {
      return _fpFradragSamlever[year - 2012];
    }


    //Kontanthjælp
    static int[] _khOverAldermBørn = { 13732, 13952, 14203 }; //§ 25, stk 1, nr. 1
    static public int khOverAldermBørn()
    {
      return _khOverAldermBørn[year - 2012];
    }
    static int[] _khOverAlderuBørn = { 10335, 10500, 10689 }; //§ 25, stk 1, nr. 2
    static public int khOverAlderuBørn()
    {
      return _khOverAlderuBørn[year - 2012];
    }
    static public int[] _khUnderAldermBørnEnlig = { 13732, 13952, 13575 }; //kan det passe at denne falder i 2014?
    static public int khUnderAldermBørnEnlig()
    {
      return _khUnderAldermBørnEnlig[year - 2012];
    }
    static public int[] _khUnderAldermBørnPar = { 13732, 13952, 6889 };
    static public int khUnderAldermBørnPar()
    {
      return _khUnderAldermBørnPar[year - 2012];
    }
    static public int[] _khUnderAldermBørnParPåSU = { 13732, 13952, 9498 }; //Par på SU, uddhj. eller kontanthj. § 25, stk. 3, nr. 2
    static public int khUnderAldermBørnParPåSU()
    {
      return _khUnderAldermBørnParPåSU[year - 2012];
    }
    static public int[] _khUnderAlderuBørnUdeboende = { 6660, 6767, 6889 }; //§ 25, stk 1, nr. 3
    static public int khUnderAlderuBørnUdeboende()
    {
      return _khUnderAlderuBørnUdeboende[year - 2012];
    }

    //Boligstøtte
    static public int[] _bsMaks0Børn = { 74900, 77000, 78800 };
    static public int bsMaks0Børn()
    {
      return _bsMaks0Børn[year - 2012];
    }
    static public int[] _bsMaks1Børn = { 78645, 80900, 80900 };
    static public int bsMaks1Børn()
    {
      return _bsMaks1Børn[year - 2012];
    }
    static public int[] _bsMaks2Børn = { 82390, 84700, 84700 };
    static public int bsMaks2Børn()
    {
      return _bsMaks2Børn[year - 2012];
    }
    static public int[] _bsMaks3Børn = { 86135, 88600, 88600 };
    static public int bsMaks3Børn()
    {
      return _bsMaks3Børn[year - 2012];
    }
    static public int[] _bsMaks4Børn = { 89880, 92400, 92400 };
    static public int bsMaks4Børn()
    {
      return _bsMaks4Børn[year - 2012];
    }
    static public int[] _bsPensionistMaks0Børn = { 81000, 82200, 83700 };
    static public int bsPensionistMaks0Børn()
    {
      return _bsPensionistMaks0Børn[year - 2012];
    }
    static public int[] _bsPensionistMaks1Børn = { 85050, 86300, 86300 };
    static public int bsPensionistMaks1Børn()
    {
      return _bsPensionistMaks1Børn[year - 2012];
    }
    static public int[] _bsPensionistMaks2Børn = { 89100, 90400, 90400 };
    static public int bsPensionistMaks2Børn()
    {
      return _bsPensionistMaks2Børn[year - 2012];
    }
    static public int[] _bsPensionistMaks3Børn = { 93150, 94500, 94500 };
    static public int bsPensionistMaks3Børn()
    {
      return _bsPensionistMaks3Børn[year - 2012];
    }
    static int[] _bsPensionistMaks4Børn = { 97200, 98600, 98600 };
    static public int bsPensionistMaks4Børn()
    {
      return _bsPensionistMaks4Børn[year - 2012];
    }
    static int[] _bsMindstebeløb = { 22500, 23100, 23700 };
    static public int bsMindstebeløb()
    {
      return _bsMindstebeløb[year - 2012];
    }
    static public int[] _bsPensionistMindstebeløb = { 15300, 15500, 15800 };
    static public int bsPensionistMindstebeløb()
    {
      return _bsPensionistMindstebeløb[year - 2012];
    }
    static public int[] _bsPensionistTillæg = { 6100, 6200, 6300 };
    static public int bsPensionistTillæg()
    {
      return _bsPensionistTillæg[year - 2012];
    }
    static int[] _bsPensionistIndkomstgrænse = { 144300, 146600, 149300 };
    static public int bsPensionistIndkomstgrænse()
    {
      return _bsPensionistIndkomstgrænse[year - 2012];
    }
    static int[] _bsIndkomstgrænse = { 133500, 137200, 140500 };
    static public int bsIndkomstgrænse()
    {
      return _bsIndkomstgrænse[year - 2012];
    }
    static int[] _bsIndkomstgrænseForhøjelse = { 35200, 36100, 37000 };
    static public int bsIndkomstgrænseForhøjelse()
    {
      return _bsIndkomstgrænseForhøjelse[year - 2012];
    }
    static int[] _bsPensionistIndkomstgrænseForhøjelse = { 38000, 38600, 39300 };
    static public int bsPensionistIndkomstgrænseForhøjelse()
    {
      return _bsPensionistIndkomstgrænseForhøjelse[year - 2012];
    }
    static int[] _bsMaxYdelse = { 39516, 40620, 41592 };
    static public int bsMaxYdelse()
    {
      return _bsMaxYdelse[year - 2012];
    }
    static int[] _bsPensionistMaxYdelse = { 42720, 43404, 44184 };
    static public int bsPensionistMaxYdelse()
    {
      return _bsPensionistMaxYdelse[year - 2012];
    }

    //Efterløn
    static int[] _elBundfradrag = { 14200, 14400, 14700 };
    static public int elBundfradrag()
    {
      return _elBundfradrag[year - 2012];
    }

    public static int SygedagpengeMaks(int år)
    {
      return _sdMaksPrUge[år - 2012] * 52 / 12; //omregnes fra uge til år til måned
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

    //Førtidspension satser
    static int[] _indtægtsgrænserÆgtefællePensionist = { 271100, 279000, 284600, 290300, 297300, 305900, 315400, 327100, 333300, 343000, 350200 }; //indtægtsgrænser, hvis ægtefælle er pensionist, talrække starter i 2003
    public static int IndtægtsgrænserÆgtefællePensionist()
    {
      return _indtægtsgrænserÆgtefællePensionist[year - 2003];
    }
    static int[] _indtægtsgrænserÆgtefælleIkkePensionist = { 179400, 184600, 188300, 192100, 196700, 202400, 208700, 216400, 220500, 226900, 231700 }; //indtægtsgrænser, hvis ægtefælle ikke er pensionist, talrække starter i 2003
    public static int indtægtsgrænserÆgtefælleIkkePensionist()
    {
      return _indtægtsgrænserÆgtefælleIkkePensionist[year - 2003];
    }
    static int[] _førtidspensionSatserEnlige = { 162036, 166740, 170076, 173472, 177636, 182784, 188448, 195420, 199128, 204900, 209208 }; //Førtidspension, enlige, talrække starter i 2003
    public static int førtidspensionSatserEnlige()
    {
      return _førtidspensionSatserEnlige[year - 2003];
    }
    static int[] _førtidspensionSatserPar = { 137724, 141720, 144552, 147444, 150984, 155364, 160176, 166104, 169260, 174168, 177828 }; //Førtidspension, par, talrække starter i 2003
    public static int førtidspensionSatserPar()
    {
      return _førtidspensionSatserPar[year - 2003];
    }
    static int[] _fpFradragHosÆgtefælle = _førtidspensionSatserPar; //Fradrag i indtægt hos ægtefælle/samlever (ikke-pensionist)
    public static int fpFradragHosÆgtefælle()
    {
      return _fpFradragHosÆgtefælle[year - 2003];
    }

    static int[] _fradragEnlig = { 55100, 56700, 57800, 59000, 60400, 62200, 64100, 66500, 67800, 69800, 71300 }; //talrække starter i 2003
    public static int fradragEnlig()
    {
      return _fradragEnlig[year - 2003];
    }
    static int[] _fradragPar = { 87500, 90000, 91800, 93600, 95800, 98600, 101700, 105500, 107500, 110600, 112900 }; //talrække starter i 2003
    public static int fradragPar()
    {
      return _fradragPar[year - 2003];
    }
    const double _nedsættelse = 0.3; //Nedsættelse i pct. af indtægtsgrundlag
    public static double nedsættelse()
    {
      return _nedsættelse;
    }
    const double _nedsættelseÆgtefællePensionist = 0.15; //Nedsættelse i pct. af indtægtsgrundlag, ægtefælles/samlever har ret til pension			
    public static double nedsættelseÆgtefællePensionist()
    {
      return _nedsættelseÆgtefællePensionist;
    }

    //Skattesatser
    static double[] _skatArbejdsmarkedsbidragssats = { 0.5, 0.6, 0.7, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8 }; //Arbejdsmarkedsbidragssats i pct, talrække starter i  1994
    public static double skatArbejdsmarkedsbidragssats()
    {
      return _skatArbejdsmarkedsbidragssats[year - 1994];
    }

    //Kontanthjælp satser
    static double[] _khTimesats = { 133.4900929, 134.0240532, 137.2406305, 137.6523524, 140.9560089, 144.6208651, 149.2486949, 154.0239743, 158.1827291, 163.2452483, 167.9794322, 171.3390101, 174.7657871, 178.96, 185.94, 195.05, 200.51, 205.52, 209.56, }; //Timesats, hvis ikke timelønnet, talrække starter i 1994
    public static double khTimesats()
    {
      return _khTimesats[year - 1994];
    }

    public const int khMaksTimer = 160;

  }
}
