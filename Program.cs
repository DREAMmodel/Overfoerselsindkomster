using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overfoerselsindkomster
{
  class Program
  {
    static void Main(string[] args)
    {
    }

     /// <summary>
  /// Uddannelsesniveau inkl. ukendt og ikke under uddannelse
  /// </summary>
     public enum Education { Ukendt, Grundskole, TiendeKlasse, AlmGym, ErhGym, Unused, ErhFag, KV, ProfBach, MV, UniBach, UdeltKand, DeltKand, Phd, Master, IkkeUnderUddannelse };

    //Førtidspension satser
    const int[] _indtægtsgrænserÆgtefællePensionist = { 271100, 279000, 284600, 290300, 297300, 305900, 315400, 327100, 333300, 343000, 350200 }; //indtægtsgrænser, hvis ægtefælle er pensionist, talrække starter i 2003
    const int[] _indtægtsgrænserÆgtefælleIkkePensionist = { 179400, 184600, 188300, 192100, 196700, 202400, 208700, 216400, 220500, 226900, 231700 }; //indtægtsgrænser, hvis ægtefælle ikke er pensionist, talrække starter i 2003
    const int[] _førtidspensionSatserEnlige = { 162036, 166740, 170076, 173472, 177636, 182784, 188448, 195420, 199128, 204900, 209208 }; //Førtidspension, enlige, talrække starter i 2003
    const int[] _førtidspensionSatserPar = { 137724, 141720, 144552, 147444, 150984, 155364, 160176, 166104, 169260, 174168, 177828 }; //Førtidspension, par, talrække starter i 2003
    const int[] _fpFradragHosÆgtefælle = _førtidspensionSatserPar; //Fradrag i indtægt hos ægtefælle/samlever (ikke-pensionist)

    const int[] _fradragEnlig = { 55100, 56700, 57800, 59000, 60400, 62200, 64100, 66500, 67800, 69800, 71300 };
    const int[] _fradragPar = { 87500, 90000, 91800, 93600, 95800, 98600, 101700, 105500, 107500, 110600, 112900 };
    const double[] _nedsættelse = { 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3 }; //Nedsættelse i pct. af indtægtsgrundlag
    const double[] _nedsættelseÆgtefællePensionist = { 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15 }; //Nedsættelse i pct. af indtægtsgrundlag, ægtefælles/samlever har ret til pension			

    //Dagpengesatser
    const int[] _dpMindsteSats = { 111504, 111930, 114702, 117686, 121524, 125362, 128773, 132824, 136661, 139433, 142204, 145616, 149880, 154570, 160326, 163311, 168002, 171626 }; //talrække starter i 1996
    const int[] _dpUngeUnder25 = { 71760, 74100, 76440, 78520, 81120, 83460, 85020, 86840, 88920, 91520, 94380, 97760, 99580, 102440, 104780 }; //talrække starter i 1999
    const double[] _dpKompensationsgrad = { 0.9, 0.9, 0.9, 0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9,0.9}; //Kompensationsgrad, talrække starter i 1994
    const int[] _dpMaksimalSats = {	132340,	132860,	135980,	136500,	139880,	143520,	148200,	152880,	157040,	161980,	166660,	170040,	173420,	177580,	182780,	188500,	195520,	199160,	204880,	209300 }; //talrække starter i 1994

    const double[] _timesats = { 133.4900929, 134.0240532, 137.2406305, 137.6523524, 140.9560089, 144.6208651, 149.2486949, 154.0239743, 158.1827291, 163.2452483, 167.9794322, 171.3390101, 174.7657871, 178.96, 185.94, 195.05, 200.51, 205.52, 209.56 }; //Timesats, hvis ikke timelønnet, talrække starter i 1994 og slutter i 2012!


    /// <summary>
    /// Beregner månedlig udbetaling af førtidspension
    /// </summary>
    /// <param name="personligIndkomst">Årlig personlig indkomst</param>
    /// <param name="par">Er personen i et parforhold</param>
    /// <param name="PersonligIndkomst_Ægtefælle">Eventuel ægtefælles personlige indkomst</param>
    /// <param name="Pensionist_Ægtefælle">Er eventuel ægtefælle pensionist</param>
    /// <param name="år">År der simuleres</param>
    /// <returns></returns>
    int Førtidspension(int personligIndkomst, int år, Boolean par = false, int personligIndkomstÆgtefælle = 0, Boolean pensionistÆgtefælle = false)
    {
      if (!par)
        return FørtidspensionEnlig(personligIndkomst, år); //beregner førtidspension for enlige

      int indtægtsgrænse, ægtefællesIndkomst;
      double nedsættelsePct;
      if (pensionistÆgtefælle)
      {
        //Din partners indtægter kan højst indgå med [354.800] kr. i beregningen af din førtidspension.
        ægtefællesIndkomst = Math.Max(0, Math.Min(_indtægtsgrænserÆgtefællePensionist[år - 2003], personligIndkomstÆgtefælle - _førtidspensionSatserPar[år - 2003])); //Ægtefælles indkomst før førtidspension       
        //Din førtidspension bliver sat [15] kr. ned, for hver 100 kr. jeres samlede indtægt er højere end [114.400] kr. 
        nedsættelsePct = _nedsættelseÆgtefællePensionist[år - 2003];
      }
      else
      {   
        //Din partners indtægter kan højst indgå med [234.600] kr. i beregningen af din førtidspension.  
        //Er din partner ikke pensionist, skal Udbetaling Danmark se bort fra de første [180.137] kr., din partner tjener.
        ægtefællesIndkomst = Math.Max(0, Math.Min(_indtægtsgrænserÆgtefælleIkkePensionist[år - 2003], personligIndkomstÆgtefælle -  _fpFradragHosÆgtefælle[år-2003])); //Ægtefælles indkomst
        //Din førtidspension bliver sat ned med [30] kr., for hver 100 kr. jeres samlede indtægt er højere end [114.400] kr. om året.
        nedsættelsePct = _nedsættelse[år - 2003];
      }

      int indtægtsgrundlag = personligIndkomst + ægtefællesIndkomst;
      //Herefter kan I sammenlagt have supplerende indtægter på op til 114.400 kr. om året, før det påvirker din pension. 
      int nedsættelse = Convert.ToInt32(nedsættelsePct * Math.Max(0, indtægtsgrundlag - _fradragPar[år - 2003])); //nedsættelse i kr

      return Math.Max(0, _førtidspensionSatserPar[år - 2003] - nedsættelse) / 12; //beregnet førtidspension
    }

    int FørtidspensionEnlig(int personligIndkomst, int år)
    {
      //Din førtidspension bliver sat [30] kr. ned, for hver 100 kr. din samlede indtægt er højere end [72.200] kr. om året.
      //Hvis du er enlig, kan du have andre indtægter op til [72.200] kr. om året, uden at det påvirker din førtidspensions størrelse.
      int nedsættelse = Convert.ToInt32(_nedsættelse[år - 2003] * Math.Max(0, personligIndkomst - _fradragEnlig[år - 2003])); //nedsættelse i kr   

      return Math.Max(0, _førtidspensionSatserEnlige[år - 2003] - nedsættelse) / 12; //beregnet førtidspension
    }

    /// <summary>
    /// Beregner arbejdsløshedsdagpenge
    /// </summary>
    /// <param name="arbejdsindkomstGrundlag"></param>
    /// <param name="alder"></param>
    /// <param name="nyuddannet"></param>
    /// <param name="deltidsforsikret"></param>
    /// <param name="indkomstAftrapning"></param>
    /// <param name="arbejdstimerAftrapning"></param>
    /// <param name="år">År der simuleres</param>
    /// <param name="akasse">Akasse medlemskab</param>
    /// <param name="arbejdstimerBerettigelse"></param>
    /// <param name="dagpengeUger">Antal uger personen allerede har modtaget dagpenge i</param>
    /// <param name="ugerBeskæftigetSomLønmodtager">Antal uger beskæftiget som lønmodtager inden for de sidste tre år</param>
    /// <param name="hf">Højst fuldførte uddannelse (DREAMs kategorier)</param>
    /// <param name="ugerArbejdsløs">Antal uger personen skal have dagpenge i/er arbejdsløs i</param>
    /// <returns>Arbejdsløshedsdagpenge pr år</returns>
    int Dagpenge(int alder, Boolean nyuddannet, Boolean deltidsforsikret, double indkomstAftrapning, double arbejdstimerAftrapning, int år, Boolean akasse, double arbejdstimerBerettigelse, int dagpengeUger, int ugerBeskæftigetSomLønmodtager, int månedslønFørArbejdsløshed, Education hf, int ugerArbejdsløs)
    {
      //Dagpenge til nyuddannede og værnepligtige rammes automatisk idet månedslønFørArbejdsløshed er meget lille

      //Krav: - Medlem af en a-kasse i mindst ét år. - Beskæftiget som lønmodtager i mindst 52 uger inden for de sidste tre år, hvis du er fuldtidsforsikret. - Beskæftiget som lønmodtager i mindst 34 uger inden for de sidste tre år, hvis du er deltidsforsikret. 
      //Du har ret til arbejdsløshedsdagpenge i to år (104 uger) inden for en periode på tre år, hvis du opfylder betingelserne.
      if (!akasse || dagpengeUger > 104 || ugerBeskæftigetSomLønmodtager < 34 || (!deltidsforsikret && ugerBeskæftigetSomLønmodtager < 52))
        return 0; //opfylder ikke krav for dagpenge

      int dagpenge;
      if (alder < 25 && hf < Education.ErhFag && ugerBeskæftigetSomLønmodtager < (52*2)) //særlige regler for unge uden uddannelse...
      {
        //hvis du er under 25 år, og du ikke har en dimittenduddannelse (en uddannelse over gymnasieniveau af mindst 1½ års varighed), og du ikke har arbejdet mindst 3.848 timer (to år på fuld tid) inden for de seneste tre år, så får du kun udbetalt 50 % af den maksimale dagpengesats (408 kroner per dag i 2014 tal) fra din 26. ledighedsuge til din 104. ledighedsuge.
        dagpenge = Convert.ToInt32(_dpMaksimalSats[år-1994] * 0.5); //fra 26. uge!!
      }
      else
      {
        //Sats (mindstesats er 82% af maksimalsats).
        dagpenge = Convert.ToInt32(Math.Max(0.82*_dpMaksimalSats[år-1994], Math.Min(_dpMaksimalSats[år-1994], månedslønFørArbejdsløshed * 12 * 0.9))); //Du kan dog højst få 90 pct. af den løn, du tjente, før du blev arbejdsløs. 
      }

      if (deltidsforsikret)
        dagpenge *= 2/3; //check type-cast!

      //reduktion ved supplerende dagpenge
      if (arbejdstimerAftrapning == 0)
        arbejdstimerAftrapning = (indkomstAftrapning / _timesats[år-1994]); //omregn fra indkomst til timer
      arbejdstimerAftrapning *= 12 / 52; //omregnes fra timer pr måned til timer pr uge

      double reduktion = arbejdstimerAftrapning > 30 ? 0 : 1 - arbejdstimerAftrapning / 37;//Aftrapning af ydelse ved supplerende dagpenge
      
      int ugerMedDagpenge =  Math.Min(104-dagpengeUger, ugerArbejdsløs);
      double andelAfÅrligtBeløb = ugerMedDagpenge / 52; //der skal kun udbetales dagpenge i de uger, personen skal have dagpenge i....

      return Convert.ToInt32(dagpenge * reduktion * andelAfÅrligtBeløb);
    }

    //Skattesatser
    const double[] _skatArbejdsmarkedsbidragssats = { 0.5, 0.6, 0.7, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8 }; //Arbejdsmarkedsbidragssats i pct, talrække starter i  1994

    //Kontanthjælp satser
    const double[] _khTimesats = { 133.4900929, 134.0240532, 137.2406305, 137.6523524, 140.9560089, 144.6208651, 149.2486949, 154.0239743, 158.1827291, 163.2452483, 167.9794322, 171.3390101, 174.7657871, 178.96, 185.94, 195.05, 200.51, 205.52, 209.56, }; //Timesats, hvis ikke timelønnet, talrække starter i 1994
    const int _khMaksTimer = 160;

/*    int Uddannelseshjælp(Education ig)
    {
      if (ig == Education.IkkeUnderUddannelse)
        return Kontanthjælp();
      else
        return SU();
    }*/

    int SU(Education ig, Boolean hjemmeboende, int alder, int dur, int år, int forældreindkomst, int børn, Boolean partnerModtagerSU, Boolean enlig)
    {
      if (alder < 18 || ig == Education.IkkeUnderUddannelse) //skal være over 18 år og igang med en uddannelse for at modtage SU
        return 0;

      Boolean startetEfter2013;
      if (dur >= år - 2013) //startet på uddannelse efter 2013
        startetEfter2013 = true;
      else
        startetEfter2013 = false;


      int su;
      if (ig > Education.TiendeKlasse && ig <= Education.ErhFag) //Ungdomsuddannelse
      {
        int grundsats;
        if (alder < 20)
        {
          if (startetEfter2013) //startet på uddannelse efter 2013
            grundsats = 906; //hvis du starter på en ny uddannelse  den 1. juli 2014 eller senere...
          else
            grundsats = 1293;

          int tillæg;
          //forældreindkomst fratræk evt. søskendefradrag....
          if (forældreindkomst < 320000) //antager at personen er startet uddannelsen i 2014 el. senere
              tillæg = 1610;
            else if (forældreindkomst > 680000)
              tillæg = 0;
            else
            {
              tillæg = 1610 * (680000 - forældreindkomst) / (680000 - 320000); //tillæg beregnes efter forældre indkomst
            }

            su = grundsats + tillæg;
        }
        else //20 år eller over
        {
          if (hjemmeboende)
            if (startetEfter2013)
              su = 2516; //hvis du starter på en ny udannelse den 1. juli 2014 eller senere...
            else
              su = 2903;
          else //udeboende
              su = 5839; //uanset start på udannelsen

          if (børn > 0 && enlig)
            su += 5839; //tillægsstipendium
          else if (børn > 0 && partnerModtagerSU)
            su += 2332; //tillægsstipendium
        }
      }

      return su;

      //Ikke implementeret:
      //Sammenhæng til indtægt - fribeløb mv.
      //Forælder under 20 år på ungdomsuddannelse
      //Handicatillæg
      //Satser (før skat) til udeboende med særlig godkendelse - fordi at "Når du er under 20 år og går på en ungdomsuddannelse, kan du normalt kun få SU med satsen for hjemmeboende - også selv om du er flyttet hjemmefra"
      //SU-lån
    }

    int Kontanthjælp(Boolean formue, int alder, int børn, int arbejdsindkomst, int arbejdstimer, int andenIndkomst, Boolean gift, Boolean udeboende, Boolean formue, Boolean ægtefælleSU, int mdrbopæl, Education uddannelse, int år, int bundfradrag, double aftrapningsprocent, int ægtefælleAlder, int ægtefælleArbejdsindkomst, int ægtefælleArbejdstimer, int ægtefælleAndenIndkomst, Boolean ægtefælleKontanthjælp, Education ig = Education.IkkeUnderUddannelse)
    {
      if (formue) //hvis formue, friværdi eller høj løn hos partner, så ingen kontanthjælp
        return 0;

      //Unge under 30 uden uddannelse får fra 2014 ikke kontanthjælp, men uddannelseshjælp, det svarer cirka til SU og kræver at personen påbegynder et studie, er personen ikke uddannelsesparat får personen et beløb svarende til kontanthjælp. Derfor kan disses satser beregnes som hhv. SU og kontanthjælp.
      if (alder < 30 && uddannelse < Education.ErhFag && år >= 2014)
      {
        //Uddannelseshjælp
        if (ig != Education.IkkeUnderUddannelse) //ikke uddannelsesparate får ydelse svarende til kontanthjælp
          return SU(ig); //...øvrige får uddannelseshjælp svarende til SU
      }
      else if (alder < 30 && uddannelse >= Education.ErhFag && år >= 2014)
      {
        //Unge under 30 år med uddannelse modtager fortsat kontanthjælp, men på niveau med SU. 
        Boolean aktivitetspålæg = false;
        if (!aktivitetspålæg) //I de perioder, hvor de unge med uddannelse deltager i aktive tilbud, får de et aktivitetspålæg, så de fastholder en samlet ydelse, der svarer til den nuværende kontanthjælpssats.
          return SU(); 
      }
     
      //if (år == 2014)
      //overgangsordning ikke implementeret

      //Satser fra: http://bm.dk/da/Tal%20og%20tendenser/Satser%20for%202014/Kontanthjaelp.aspx
      int sats;
      if (alder > 30)
      {
        if (børn > 0) //Fyldt 30, forsørger børn
          sats = 14203; //2014 sats!
        if (alder > 30) //Fyldt 30, andre
          sats = 10689;
      }
      else if (alder < 30) //Under 30 år
      {
        if (børn > 0) //forsøger under 30 år
        {
          if (!gift) //Enlige forsørgere, under 30 år
            sats = 13575;
          else if (ægtefælleKontanthjælp || ægtefælleSU)
            sats = 9498; //Forsørgere under 30 år, gift/bor med person på SU, uddhj. eller kontanthj.
          else
            sats = 6889; //Forsørgere under 30 år, gift/bor med person, der ikke er på SU, uddhj. eller kontanthj.
        }
        else
          sats = 6889; //Under 30 år, udeboende, ikke forsørger
      }

      //Ikke implementerede satser:
      //Under 30 år, hjemmeboende, ikke forsørger - sats = 3324;
      //Under 30 år, gravid (passeret 12. svangerskabsuge)
      //Under 30 år, psykisk syg, forsøgerpligt
      //Under 30 år, psykisk syg, udeboende
      //Under 30 år, bidragspligt, maksimum hjælp inkl. tillæg

      //Ikke implementeret:
      //Aktivitetstillæg
      //Periodesanktioner
      //Punktsanktioner
      //Engangshjælp

      int kontanthjælp = sats - andenIndkomst - ægtefælleAndenIndkomst; //Ikke-arbejdsrelaterede indtægter fratrækkes

      if (arbejdstimer == 0)
        arbejdstimer = Math.Min(Convert.ToInt32(Convert.ToDouble(arbejdsindkomst) / _khTimesats[år - 1994] * (1 - _skatArbejdsmarkedsbidragssats[år - 1994])), _khMaksTimer); //beregning af arbejdstimer udfra arbejdsindtægt
      else
        arbejdstimer = Math.Min(arbejdstimer, _khMaksTimer);

      if (gift)
      {
        if (ægtefælleArbejdstimer == 0)
          ægtefælleArbejdstimer = Math.Min(Convert.ToInt32(Convert.ToDouble(ægtefælleArbejdsindkomst) / _khTimesats[år - 1994] * (1 - _skatArbejdsmarkedsbidragssats[år - 1994])), _khMaksTimer); //beregning af arbejdstimer udfra arbejdsindtægt
        else
          ægtefælleArbejdstimer = Math.Min(ægtefælleArbejdstimer, _khMaksTimer);
      }

      //Fratrækning af arbejdsindtægter....
      kontanthjælp -= arbejdsindkomst + ægtefælleArbejdsindkomst; //Dine og din ægtefælles eller samlevers indtægter trækkes fra i hjælpen
      kontanthjælp += arbejdstimer + ægtefælleArbejdstimer * 25; //du bliver tilgodeset med kr. 25,- pr. udført arbejdstime.

      return kontanthjælp;
    }



    /*
Function børnetilskud( _
                        AntalBørn As Integer, _
                        Optional Par As Boolean = False, _
                        Optional Indkomst As Double = 0, _
                        Optional Børnebidrag = False, _
                        Optional Pension1 = False, _
                        Optional Pension2 = False, _
                        Optional Uddannelse1 = False, _
                        Optional Uddannelse2 = False, _
                        Optional Mdrbosat As Integer = 25, _
                        Optional År As Integer = 2012 _
                        )

'Application.Volatile

'dagpenge ved fødsel
'børnetilskud
'børnebidrag

'intro
står = Range("StartÅr").Value       'Startår
slÅr = Range("Slutår").Value        'Slutår
t = År - står + 1                   'standardisering til periode
sats = Range("BTsatser").Value      'satsdatabase


'status for bopæl i DK
Select Case Mdrbosat
Case 1 To 5
mdrfaktor = 0
Case 5 To 11
mdrfaktor = 0.25
Case 12 To 17
mdrfaktor = 0.5
Case 18 To 23
mdrfaktor = 0.75
Case Is > 23
mdrfaktor = 1
End Select

'Beregning af tilskud
'Ordinært børnetilskud
If Par = False Or (Pension1 = True And Pension2 = True) _
Or (Uddannelse1 = True Or Uddannelse2 = True) Then
    Ordinær = sats(1, t) * AntalBørn
End If

'Ekstra børnetilskud
If AntalBørn <> 0 And Par = False Then
    Ekstra = sats(2, t)
End If

'Særligt børnetilskud
If (Par = False And Børnebidrag = False) Or (Pension1 = True _
Or Pension2 = True) Then
Særlig = sats(3, t) * AntalBørn
End If

'Tillæg til særligt børnetilskud
If Par = False Then
Tillæg = sats(4, t) * AntalBørn
End If

børnetilskud = (Ordinær + Særlig + Ekstra + Tillæg) * mdrfaktor

'tilskud til udd søgende
'overførselsindkomst
End Function*/


    /*
Function børnefamilieydelse(Alder1 As Integer, _
                            Optional Alder2 As Integer = -1, _
                            Optional Alder3 As Integer = -1, _
                            Optional Alder4 As Integer = -1, _
                            Optional Alder5 As Integer = -1, _
                            Optional År As Integer = 2012, _
                            Optional Indkomst1 As Double = 0, _
                            Optional Indkomst2 As Double = 0)
'Application.Volatile

'satser
står = Range("StartÅr").Value       'Startår
slÅr = Range("Slutår").Value        'Slutår
t = År - står + 1                   'standardisering til periode
Ydelse = Range("BFsatser").Value    'satsdatabase
Klasse1 = 0
Klasse2 = 0
Klasse3 = 0
Klasse4 = 0

'aldersklasser
Select Case Alder1
Case 0 To 2
Klasse1 = Klasse1 + 1
Case 3 To 6
Klasse2 = Klasse2 + 1
Case 7 To 15
Klasse3 = Klasse3 + 1
Case 15 To 17
Klasse4 = Klasse4 + 1
End Select
 
Select Case Alder2
Case 0 To 2
Klasse1 = Klasse1 + 1
Case 3 To 6
Klasse2 = Klasse2 + 1
Case 7 To 15
Klasse3 = Klasse3 + 1
Case 15 To 17
Klasse4 = Klasse4 + 1
End Select

Select Case Alder3
Case 0 To 2
Klasse1 = Klasse1 + 1
Case 3 To 6
Klasse2 = Klasse2 + 1
Case 7 To 15
Klasse3 = Klasse3 + 1
Case 15 To 17
Klasse4 = Klasse4 + 1
End Select

Select Case Alder4
Case 0 To 2
Klasse1 = Klasse1 + 1
Case 3 To 6
Klasse2 = Klasse2 + 1
Case 7 To 15
Klasse3 = Klasse3 + 1
Case 15 To 17
Klasse4 = Klasse4 + 1
End Select

Select Case Alder5
Case 0 To 2
Klasse1 = Klasse1 + 1
Case 3 To 6
Klasse2 = Klasse2 + 1
Case 7 To 15
Klasse3 = Klasse3 + 1
Case 15 To 17
Klasse4 = Klasse4 + 1
End Select

reduktion = 0.02 * (max(0, Indkomst1 * 12 - 700000) + max(0, Indkomst2 * 12 - 700000)) / 12

børnefamilieydelse = max(0, Ydelse(1, t) * Klasse1 + Ydelse(2, t) * Klasse2 + Ydelse(3, t) * Klasse3 _
+ Ydelse(4, t) * Klasse4 - reduktion)


End Function*/

    /*
Function samletbørneydelse(Alder1 As Integer, _
                           Institution1 As String, _
                           Optional Alder2 As Integer = -1, _
                           Optional institution2 As String, _
                           Optional Alder3 As Integer = -1, _
                           Optional Institution3 As String, _
                           Optional Alder4 As Integer = -1, _
                           Optional Institution4 As String, _
                           Optional Alder5 As Integer = -1, _
                           Optional Institution5 As String, _
                           Optional Par As Boolean = False, _
                           Optional Indkomst As Double = 0, _
                           Optional År As Integer = 2012)


'bfydelseAlder1 As Integer, _
                            Optional Alder2 As Integer = -1, _
                            Optional Alder3 As Integer = -1, _
                            Optional Alder4 As Integer = -1, _
                            Optional Alder5 As Integer = -1, _
                            Optional År As Integer = 2012)
 'instttilskud_
 '                       Dagpleje As Integer, _
                        Vuggestue As Integer, _
                        Børnehave As Integer, _
                        SFO As Integer, _
                        Optional Øvrigebørn As Integer = 0, _
                        Optional Indkomst As Double = 0, _
                        Optional par As Boolean = False, _
                        Optional År As Integer = 2012)
Dim AntalBørnehave As Integer
Dim AntalVuggestue As Integer
Dim AntalDagpleje As Integer
Dim AntalSFO As Integer
Dim AntalØvrigebørn As Integer

AntalDagpleje = 0
AntalVuggestue = 0
AntalBørnehave = 0
AntalSFO = 0
AntalØvrigebørn = 0

Select Case Institution1
Case "Vuggestue"
AntalVuggestue = AntalVuggestue + 1
Case "Dagpleje"
AntalDagpleje = AntalDagpleje + 1
Case "Børnehave"
AntalBørnehave = AntalBørnehave + 1
Case "SFO"
AntalSFO = AntalSFO + 1
Case Else
If Alder1 < 18 And Alder1 >= 0 Then AntalØvrigebørn = AntalØvrigebørn + 1
End Select

Select Case institution2
Case "Vuggestue"
AntalVuggestue = AntalVuggestue + 1
Case "Dagpleje"
AntalDagpleje = AntalDagpleje + 1
Case "Børnehave"
AntalBørnehave = AntalBørnehave + 1
Case "SFO"
AntalSFO = AntalSFO + 1
Case Else
If Alder2 < 18 And Alder2 >= 0 Then AntalØvrigebørn = AntalØvrigebørn + 1
End Select

Select Case Institution3
Case "Vuggestue"
AntalVuggestue = AntalVuggestue + 1
Case "Dagpleje"
AntalDagpleje = AntalDagpleje + 1
Case "Børnehave"
AntalBørnehave = AntalBørnehave + 1
Case "SFO"
AntalSFO = AntalSFO + 1
Case Else
If Alder3 < 18 And Alder3 >= 0 Then AntalØvrigebørn = AntalØvrigebørn + 1
End Select

Select Case Institution4
Case "Vuggestue"
AntalVuggestue = AntalVuggestue + 1
Case "Dagpleje"
AntalDagpleje = AntalDagpleje + 1
Case "Børnehave"
AntalBørnehave = AntalBørnehave + 1
Case "SFO"
AntalSFO = AntalSFO + 1
Case Else
If Alder4 < 18 And Alder4 >= 0 Then AntalØvrigebørn = AntalØvrigebørn + 1
End Select

Select Case Institution5
Case "Vuggestue"
AntalVuggestue = AntalVuggestue + 1
Case "Dagpleje"
AntalDagpleje = AntalDagpleje + 1
Case "Børnehave"
AntalBørnehave = AntalBørnehave + 1
Case "SFO"
AntalSFO = AntalSFO + 1
Case Else
If Alder5 < 18 And Alder5 >= 0 Then AntalØvrigebørn = AntalØvrigebørn + 1
End Select

samletbørneydelse = insttilskud(AntalDagpleje, AntalVuggestue, AntalBørnehave, AntalSFO, AntalØvrigebørn, Indkomst, Par, År) + _
børnefamilieydelse(Alder1, Alder2, Alder3, Alder4, Alder5, År)

End Function
*/


    /*
Function boligstøtte(husleje As Double, _
                     Husstandsindkomst As Double, _
                     kvm As Double, _
                     nbørn As Integer, _
                     Optional År As Integer = 2012, _
                     Optional Formue As Double = 0, _
                     Optional Par As Boolean = False, _
                     Optional pensionist As Boolean = False, _
                     Optional hustype As String = "lejer")
'Application.Volatile

'Satser
står = Range("StartÅr").Value                     'Startår
slÅr = Range("Slutår").Value                      'Slutår
t = År - står + 1                                 'standardisering til periode
sikring = Range("BSikringSatser").Value           'satsdatabase, sikring
Ydelse = Range("BYdelseSatser").Value             'satsdatabase, ydelse
husleje = husleje * 12
Husstandsindkomst = Husstandsindkomst * 12

'opdeles på boligsikring og boligydelse efter pensionsstatus
If pensionist = False Then
'boligsikring

'beregning af boligudgift (husleje uden varme vand og el), korrigeret for boligudgiftloft og maks kvm
makskvm = sikring(23, t) + sikring(24, t) * (-Par + nbørn)
kvmkvotient = min(makskvm / kvm, 1)
maksboligudgift = sikring(18, t) * (1 + 0.05 * min(nbørn, 4))
boligudgift = min(maksboligudgift, husleje * kvmkvotient)

'beregning af indkomst, der skal fratrækkes i støtten
indkomstgrænse = sikring(4, t) + sikring(7, t) * min(max(nbørn - 1, 0), 3) + sikring(9, t) * nbørn
indkomstfratræk1 = sikring(6, t) * max(0, Husstandsindkomst - indkomstgrænse)
indkomstfratræk2 = sikring(5, t) * min(Husstandsindkomst, indkomstgrænse)
indkomstfratræk = indkomstfratræk1 + indkomstfratræk2

'beregning af boligsikringsloft
If nbørn = 0 Then
    maksstøtte = min(sikring(25, t), 0.15 * boligudgift)
ElseIf nbørn < 4 Then
    maksstøtte = sikring(25, t)
Else
    maksstøtte = sikring(25, t) * 1.25
   'maksstøtte = sikring(27, t)
End If

boligstøtte = min(maksstøtte, _
max(boligudgift * sikring(3, t) - indkomstfratræk, 0) _
) / 12


Else
'boligydelse
makskvm = sikring(23, t) + sikring(24, t) * (parstatus * -1 + nbørn + npers)
maksleje = sikring(18 + min(nbørn, 4), t)
minstøtte = sikring(28, t)
maksstøtte = sikring(25, t)
'kvm
boligstøtte = "ingen beregning for pensionister endnu"
End If

'implementer formue og boligydelse

End Function*/


  }
}
