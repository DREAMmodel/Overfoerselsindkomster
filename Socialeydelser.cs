using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overfoerselsindkomster
{
  /// <summary>
  /// Uddannelsesniveau inkl. ukendt og ikke under uddannelse
  /// </summary>
  public enum Education { Ukendt, Grundskole, TiendeKlasse, AlmGym, ErhGym, Unused, ErhFag, KV, ProfBach, MV, UniBach, UdeltKand, DeltKand, Phd, Master, IkkeUnderUddannelse };
  public enum CivilstandPensionist { Samlevende, Enlig, ReeltEnlig, Gift, Samgift };

  class Socialeydelser
  {

    #region satser
    //Førtidspension satser
    static int[] _indtægtsgrænserÆgtefællePensionist = { 271100, 279000, 284600, 290300, 297300, 305900, 315400, 327100, 333300, 343000, 350200 }; //indtægtsgrænser, hvis ægtefælle er pensionist, talrække starter i 2003
    static int[] _indtægtsgrænserÆgtefælleIkkePensionist = { 179400, 184600, 188300, 192100, 196700, 202400, 208700, 216400, 220500, 226900, 231700 }; //indtægtsgrænser, hvis ægtefælle ikke er pensionist, talrække starter i 2003
    static int[] _førtidspensionSatserEnlige = { 162036, 166740, 170076, 173472, 177636, 182784, 188448, 195420, 199128, 204900, 209208 }; //Førtidspension, enlige, talrække starter i 2003
    static int[] _førtidspensionSatserPar = { 137724, 141720, 144552, 147444, 150984, 155364, 160176, 166104, 169260, 174168, 177828 }; //Førtidspension, par, talrække starter i 2003
    static int[] _fpFradragHosÆgtefælle = _førtidspensionSatserPar; //Fradrag i indtægt hos ægtefælle/samlever (ikke-pensionist)

    static int[] _fradragEnlig = { 55100, 56700, 57800, 59000, 60400, 62200, 64100, 66500, 67800, 69800, 71300 };
    static int[] _fradragPar = { 87500, 90000, 91800, 93600, 95800, 98600, 101700, 105500, 107500, 110600, 112900 };
    static double[] _nedsættelse = { 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3 }; //Nedsættelse i pct. af indtægtsgrundlag
    static double[] _nedsættelseÆgtefællePensionist = { 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15, 0.15 }; //Nedsættelse i pct. af indtægtsgrundlag, ægtefælles/samlever har ret til pension			

    //Skattesatser
    static double[] _skatArbejdsmarkedsbidragssats = { 0.5, 0.6, 0.7, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8, 0.8 }; //Arbejdsmarkedsbidragssats i pct, talrække starter i  1994

    //Kontanthjælp satser
    static double[] _khTimesats = { 133.4900929, 134.0240532, 137.2406305, 137.6523524, 140.9560089, 144.6208651, 149.2486949, 154.0239743, 158.1827291, 163.2452483, 167.9794322, 171.3390101, 174.7657871, 178.96, 185.94, 195.05, 200.51, 205.52, 209.56, }; //Timesats, hvis ikke timelønnet, talrække starter i 1994
    const int _khMaksTimer = 160;
    #endregion satser

    /// <summary>
    /// Beregner månedlig udbetaling af førtidspension
    /// </summary>
    /// <param name="personligIndkomst">Årlig personlig indkomst</param>
    /// <param name="par">Er personen i et parforhold</param>
    /// <param name="PersonligIndkomst_Ægtefælle">Eventuel ægtefælles personlige indkomst</param>
    /// <param name="Pensionist_Ægtefælle">Er eventuel ægtefælle pensionist</param>
    /// <param name="år">År der simuleres</param>
    /// <returns></returns>
    public int Førtidspension(int personligIndkomst, int år, int alderVedFørsteFørtidspension, int årIDksiden15, Boolean par = false, int personligIndkomstÆgtefælle = 0, Boolean pensionistÆgtefælle = false)
    {
      //Brøkpension
      double brøkpension = Math.Max(0, Math.Min(1, årIDksiden15 / ((alderVedFørsteFørtidspension - 15) * 4 / 5d)));

      if (!par)
        return Convert.ToInt32(FørtidspensionEnlig(personligIndkomst, år) * brøkpension); //beregner førtidspension for enlige

      int ægtefællesIndkomst;
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
        ægtefællesIndkomst = Math.Max(0, Math.Min(_indtægtsgrænserÆgtefælleIkkePensionist[år - 2003], personligIndkomstÆgtefælle - _fpFradragHosÆgtefælle[år - 2003])); //Ægtefælles indkomst
        //Din førtidspension bliver sat ned med [30] kr., for hver 100 kr. jeres samlede indtægt er højere end [114.400] kr. om året.
        nedsættelsePct = _nedsættelse[år - 2003];
      }

      int indtægtsgrundlag = personligIndkomst + ægtefællesIndkomst;
      //Herefter kan I sammenlagt have supplerende indtægter på op til 114.400 kr. om året, før det påvirker din pension. 
      int nedsættelse = Convert.ToInt32(nedsættelsePct * Math.Max(0, indtægtsgrundlag - _fradragPar[år - 2003])); //nedsættelse i kr

      return Convert.ToInt32(Math.Max(0, _førtidspensionSatserPar[år - 2003] - nedsættelse) / 12 * brøkpension); //beregnet førtidspension
    }

    public int FørtidspensionEnlig(int personligIndkomst, int år)
    {
      //Din førtidspension bliver sat [30] kr. ned, for hver 100 kr. din samlede indtægt er højere end [72.200] kr. om året.
      //Hvis du er enlig, kan du have andre indtægter op til [72.200] kr. om året, uden at det påvirker din førtidspensions størrelse.
      int nedsættelse = Convert.ToInt32(_nedsættelse[år - 2003] * Math.Max(0, personligIndkomst - _fradragEnlig[år - 2003])); //nedsættelse i kr   

      return Math.Max(0, _førtidspensionSatserEnlige[år - 2003] - nedsættelse) / 12; //beregnet førtidspension
    }

    /// <summary>
    /// Beregner arbejdsløshedsdagpenge
    /// For at få udregningen pr måned sættes ugerArbejdsløs = 4, for et helt år sættes ugerArbejdsløs = 52 og hvis personen har været arbejdsløs i et halvt år beregnes det ved at sætte ugerArbejdsløs = 52/2.
    /// </summary>
    /// <param name="år">År der simuleres</param>
    /// <param name="akasse">Akasse medlemskab</param>
    /// <param name="dagpengeMåneder">Antal måneder personen allerede har modtaget dagpenge i</param>
    /// <param name="ugerBeskæftigetSomLønmodtager">Antal uger beskæftiget som lønmodtager inden for de sidste tre år</param>
    /// <param name="hf">Højst fuldførte uddannelse (DREAMs kategorier)</param>
    /// <param name="månederArbejdsløs">Antal uger personen skal have dagpenge i/er arbejdsløs i</param>
    /// <param name="alder">Personens alder</param>
    /// <param name="arbejdstimer">Arbejdstimer arbejdet, som dagpengesatsen skal nedsættes ift</param>
    /// <param name="deltidsforsikret">Sand hvis personen kun er deltidsforsikret</param>
    /// <param name="arbejdsindkomst">Indkomst, som dagpengesatsen skal nedsættes ift. Omregnes til arbejdstimer og anvendes kun hvis arbejdstimer ikke er angivet</param>
    /// <param name="månedslønFørArbejdsløshed">Seneste månedsløn</param>
    /// <returns>Arbejdsløshedsdagpenge pr år</returns>
    public static int Dagpenge(int alder, Boolean deltidsforsikret, int arbejdsindkomst, double arbejdstimer, int år, Boolean akasse, int ugerBeskæftigetSomLønmodtager, int månedslønFørArbejdsløshed, Education hf, int dagpengeMåneder = 0, int månederArbejdsløs = 1)
    {
      //Dagpenge til nyuddannede og værnepligtige rammes automatisk idet månedslønFørArbejdsløshed er meget lille

      //Krav: - Medlem af en a-kasse i mindst ét år. - Beskæftiget som lønmodtager i mindst 52 uger inden for de sidste tre år, hvis du er fuldtidsforsikret. - Beskæftiget som lønmodtager i mindst 34 uger inden for de sidste tre år, hvis du er deltidsforsikret. 
      //Du har ret til arbejdsløshedsdagpenge i to år (104 uger) inden for en periode på tre år, hvis du opfylder betingelserne.
      if (!akasse || dagpengeMåneder > (104/4) || ugerBeskæftigetSomLønmodtager < 34 || (!deltidsforsikret && ugerBeskæftigetSomLønmodtager < 52))
        return 0; //opfylder ikke krav for dagpenge

      int dagpenge;
      if (alder < 25 && hf < Education.ErhFag && ugerBeskæftigetSomLønmodtager < (52 * 2) && dagpengeMåneder >= 26/4) //særlige regler for unge uden uddannelse efter 26. uge...
      {
        //hvis du er under 25 år, og du ikke har en dimittenduddannelse (en uddannelse over gymnasieniveau af mindst 1½ års varighed), og du ikke har arbejdet mindst 3.848 timer (to år på fuld tid) inden for de seneste tre år, så får du kun udbetalt 50 % af den maksimale dagpengesats (408 kroner per dag i 2014 tal) fra din 26. ledighedsuge til din 104. ledighedsuge.
        dagpenge = Convert.ToInt32(Satser.DpMaksimalSats[år - 1994] * 0.5); //fra 26. uge!!
      }
      else
      {
        //Sats (mindstesats er 82% af maksimalsats).
        dagpenge = Convert.ToInt32(Math.Max(0.82 * Satser.DpMaksimalSats[år - 1994], Math.Min(Satser.DpMaksimalSats[år - 1994], månedslønFørArbejdsløshed * 12 * 0.9))); //Du kan dog højst få 90 pct. af den løn, du tjente, før du blev arbejdsløs. 
      }

      if (deltidsforsikret)
        dagpenge *= 2 / 3; //check type-cast!

      //reduktion ved supplerende dagpenge
      if (arbejdstimer == 0)
        arbejdstimer = arbejdsindkomst / Satser.Timesats[år - 1994]; //omregn fra indkomst til timer
      arbejdstimer *= 12 / 52d; //omregnes fra timer pr måned til timer pr uge

      double reduktion = arbejdstimer > 30 ? 0 : 1 - arbejdstimer / 37d;//Aftrapning af ydelse ved supplerende dagpenge

      int månederMedDagpenge = Math.Min(26 - dagpengeMåneder, månederArbejdsløs);
      double andelAfÅrligtBeløb = månederMedDagpenge / 12d; //der skal kun udbetales dagpenge i de uger, personen skal have dagpenge i....

      return Convert.ToInt32(dagpenge * reduktion * andelAfÅrligtBeløb);

      //Ikke implementeret:
      //Særligt genoptjeningskrav (kravet samme for alle her)
      //Seniorjob?
    }

    public static int Sygedagpenge(Boolean akasse, Boolean tilknyttetArbejdsmarkedet, Boolean nyuddannet, Boolean elev, Boolean ledig, int lønFørSygdom, int år, int alder, Education hf, int sygedagpengeMåneder = 0, double uarbejdsdygtighed = 1)
    {
      if (!akasse && !tilknyttetArbejdsmarkedet && !nyuddannet && !elev)
        return 0; //Din kommune udbetaler sygedagpenge til dig, hvis: - Du har været tilknyttet arbejdsmarkedet de sidste 26 uger før sygefraværet og i denne periode har arbejdet mindst 240 timer, eller: - Du er dagpengeberettiget medlem af en a-kasse, eller: - Du har afsluttet en uddannelse på mindst 18 måneder inden for den sidste måned, eller: - Du er elev i lønnet praktik

      if (sygedagpengeMåneder >= 12)
        return 0; //man kan (normalt) maks få sygedagpenge i 52 uger

      double ydelse = 0;

      if (akasse && ledig)
        ydelse = Dagpenge(alder, false, 0, 0, år, akasse, 999, lønFørSygdom, hf); //Sygedagpenge til et ledigt medlem af en anerkendt arbejdsløshedskasse udgør samme beløb, som personen kunne have modtaget i arbejdsløshedsdagpenge
      else //lønmodtager
        ydelse = Math.Min(lønFørSygdom, Convert.ToDouble(Satser.SygedagpengeMaks(år)));

      return Convert.ToInt32(ydelse * uarbejdsdygtighed);
    }

    /*    int Uddannelseshjælp(Education ig)
        {
          if (ig == Education.IkkeUnderUddannelse)
            return Kontanthjælp();
          else
            return SU();
        }*/

    /// <summary>
    /// Beregner folkepensionsudbetaling
    /// </summary>
    /// <param name="år"></param>
    /// <param name="alder"></param>
    /// <param name="årIDK"></param>
    /// <param name="arbejdsindtægt">Arbejdsindtægt ved personligt arbejde. Det vil sige, at pensionsindtægter, renteindtægter og en eventuel ægtefælles indtægter ikke medgår i beregningen. Det gør folkepensionen og ATP heller ikke. Honorarer og mødediæter anses for indtægt ved personligt arbejde.</param>
    /// <param name="danskIndfødsret"></param>
    /// <returns></returns>
    public static int Folkepension(int år, int alder, int årIDK, CivilstandPensionist civilstand, int arbejdsindtægt, int samletIndtægt, int samleverIndkomst = 0, Boolean samleverPensionist = false, Boolean danskIndfødsret = true)
    {
      int født = år - alder;
        
      int folkepensionsalder;
      if (født < 1954)
        folkepensionsalder = 65;
      else if (født < 1955)
        folkepensionsalder = 66;
      else
        folkepensionsalder = 67;

      //krav:
      if (alder < folkepensionsalder || !danskIndfødsret) //dansk bopæl tages for givet
        return 0;

      int folkepension;

      int grundbeløb = Satser.fpGrundbeløb[år - 2012];
      int pensionstillæg;
      if (civilstand == CivilstandPensionist.Enlig || civilstand == CivilstandPensionist.ReeltEnlig)
      {
        int fradragsbeløb = Satser.fpFradragvTillægEnlig[år - 2012];

        pensionstillæg = Satser.fpPensionstillægEnlige[år - 2012]; //før evt nedsættelse
        int fradrag = Math.Max(0, Math.Min(arbejdsindtægt, Satser.fpFradragvArbejde[år-2012])) + fradragsbeløb;

        double reduktionsgrad = civilstand == CivilstandPensionist.Enlig ? 0.32 : 0.309;

        int nedsættelse = Convert.ToInt32(Math.Max(0, (samletIndtægt - fradrag) * reduktionsgrad)); //Der foretages fratræk med 30,9% af din indtægt der overstiger [fradrag] kroner pr. år. 
        pensionstillæg = Math.Max(0, pensionstillæg - nedsættelse);
      }
      else
      { //samlevende og gifte
        int fradragsbeløb = Satser.fpFradragvTillægPar[år - 2012];

        pensionstillæg = Satser.fpPensionstillægPar[år - 2012];

        int samleverIndkomstIndgåriBeregning;
        if (samleverPensionist)
          samleverIndkomstIndgåriBeregning = samleverIndkomst;
        else
        { //hvis ægtefælle ikke er pensionist gives et fradrag
          if (samleverIndkomst > Satser.fpFradragSamlever[år - 2012]) //For en pensionist, der er gift/samlevende med en person, der ikke modtager social pension, ...
          {
            samleverIndkomstIndgåriBeregning = Satser.fpFradragSamlever[år - 2012] / 2; //...fradrages halvdelen af ægtefællens/samleverens indtægter op til 201.100 kr.
            samleverIndkomstIndgåriBeregning += (samleverIndkomst - Satser.fpFradragSamlever[år - 2012]); // Indtægt derover indgår fuldt ud i beregningen.
          }
          else
            samleverIndkomstIndgåriBeregning = samleverIndkomst / 2;
        }

        int fradrag = Math.Max(0, Math.Min(arbejdsindtægt, Satser.fpFradragvArbejde[år - 2012])) + fradragsbeløb;
        int beregningsgrundlag = Math.Max(0, (samletIndtægt + samleverIndkomstIndgåriBeregning - fradrag));
        double reduktionsgrad = samleverPensionist ? 0.16 : 0.32;
        int nedsættelse = Convert.ToInt32(beregningsgrundlag * reduktionsgrad);
        pensionstillæg = Math.Max(0, pensionstillæg - nedsættelse);
      }

      int fpFradragvGrundbeløb = Satser.fpFradragvGrundbeløb[år - 2012];
      if (arbejdsindtægt > fpFradragvGrundbeløb)
      {
        int nedsættelse = Convert.ToInt32(0.3 * Convert.ToDouble(arbejdsindtægt - fpFradragvGrundbeløb)); //...af grundbeløb, hvis arbejdsindtægt > [291.200]
        grundbeløb -= nedsættelse;
      }

      folkepension = grundbeløb + pensionstillæg;
      
      double brøkpension = Math.Max(1, årIDK / 40); //evt. brøkpension
      return Convert.ToInt32(folkepension * brøkpension) / 12;

      //Ikke implementeret:
      //Opsat pension
      //Helbredstillæg
      //Supplerende pensionsydelse
      //Personlige tillæg
    }

    public static int SU(Education ig, Boolean hjemmeboende, int alder, int dur, int år, int forældreindkomst, int børn, Boolean partnerModtagerSU, Boolean enlig)
    {
      if (alder < 18 || ig == Education.IkkeUnderUddannelse) //skal være over 18 år og igang med en uddannelse for at modtage SU
        return 0;

      Boolean startetEfter2013;
      if (dur >= år - 2013) //startet på uddannelse efter 2013
        startetEfter2013 = true;
      else
        startetEfter2013 = false;

      int su;
      if (ig <= Education.ErhFag && alder < 20) //Under 20 år og igang med ungdomsuddannelse
      {
        int grundsats;
        if (startetEfter2013) //startet på uddannelse efter 2013
          grundsats = Satser.SUunder20StartetEfter2013(år); //hvis du starter på en ny uddannelse  den 1. juli 2014 eller senere...
        else
          grundsats = Satser.SUunder20StartetFør2013(år);

        int tillæg;
        //forældreindkomst fratræk evt. søskendefradrag....
        if (forældreindkomst < Satser.SUforældreindkomstMin(år)) //antager at personen er startet uddannelsen i 2014 el. senere
          tillæg = Satser.SUmaximaltTillæg(år);
        else if (forældreindkomst > Satser.SUforældreindkomstMax(år))
          tillæg = 0;
        else
          tillæg = Satser.SUmaximaltTillæg(år) * (Satser.SUforældreindkomstMax(år) - forældreindkomst) / (Satser.SUforældreindkomstMax(år) - Satser.SUforældreindkomstMin(år)); //tillæg beregnes efter forældre indkomst

        su = grundsats + tillæg;
      }
      else //20 år eller over og/eller igang med videregående uddannelse
      {
        if (!hjemmeboende)
          su = Satser.SUudeboende(år); //udeboende, uanset start på udannelsen
        else if (startetEfter2013)
          su = Satser.SUhjemmeboendeEfter2013(år); //hjemmeboende, hvis du starter på en ny udannelse den 1. juli 2014 eller senere...
        else
          su = Satser.SUhjemmeboendeFør2013(år); //hjemmeboende startet på udd. før 2014

        if (børn > 0 && enlig)
          su += 5839; //tillægsstipendium
        else if (børn > 0 && partnerModtagerSU)
          su += 2332; //tillægsstipendium
      }

      return su;

      //Ikke implementeret:
      //Sammenhæng til indtægt - fribeløb mv.
      //Forældre under 20 år på ungdomsuddannelse
      //Handicaptillæg
      //Satser (før skat) til udeboende med særlig godkendelse - fordi at "Når du er under 20 år og går på en ungdomsuddannelse, kan du normalt kun få SU med satsen for hjemmeboende - også selv om du er flyttet hjemmefra"
      //SU-lån
    }

    public static int Kontanthjælp(Boolean formue, int alder, int børn, int dur, int forældreindkomst, int arbejdsindkomst, Boolean partnerModtagerSU, int arbejdstimer, int andenIndkomst, Boolean enlig, Boolean udeboende, Boolean ægtefælleSU, int mdrbopæl, Education uddannelse, int år, int bundfradrag, double aftrapningsprocent, int ægtefælleAlder, int ægtefælleArbejdsindkomst, int ægtefælleArbejdstimer, int ægtefælleAndenIndkomst, Boolean ægtefælleKontanthjælp, Education ig = Education.IkkeUnderUddannelse, Boolean aktivitetstillæg = false)
    {
      if (formue) //hvis formue, friværdi eller høj løn hos partner, så ingen kontanthjælp
        return 0;

      int aldersgrænse = år >= 2014 ? 25 : 30;

      //Unge under 30 uden uddannelse får fra 2014 ikke kontanthjælp, men uddannelseshjælp, det svarer cirka til SU og kræver at personen påbegynder et studie, er personen ikke uddannelsesparat får personen et beløb svarende til kontanthjælp. Derfor kan disses satser beregnes som hhv. SU og kontanthjælp.
      if (alder < aldersgrænse && uddannelse < Education.ErhFag && år >= 2014)
      {
        //Uddannelseshjælp
        if (ig != Education.IkkeUnderUddannelse) //ikke uddannelsesparate får ydelse svarende til kontanthjælp
          return SU(ig, !udeboende, alder, dur, år, forældreindkomst, børn, partnerModtagerSU, enlig); //...øvrige får uddannelseshjælp svarende til SU
      }
      else if (alder < aldersgrænse && uddannelse >= Education.ErhFag && år >= 2014)
      {
        //Unge under 30 år med uddannelse modtager fortsat kontanthjælp, men på niveau med SU. 
        Boolean aktivitetspålæg = false;
        if (!aktivitetspålæg) //I de perioder, hvor de unge med uddannelse deltager i aktive tilbud, får de et aktivitetspålæg, så de fastholder en samlet ydelse, der svarer til den nuværende kontanthjælpssats.
          return SU(ig, !udeboende, alder, dur, år, forældreindkomst, børn, partnerModtagerSU, enlig);
      }

      //if (år == 2014)
      //overgangsordning ikke implementeret

      //Satser fra: http://bm.dk/da/Tal%20og%20tendenser/Satser%20for%202014/Kontanthjaelp.aspx
      int sats;
      if (alder >= aldersgrænse)
        if (børn > 0) //Fyldt 30, forsørger børn
          sats = Satser.khOverAldermBørn[år - 2012];
        else //Fyldt 30, andre
          sats = Satser.khOverAlderuBørn[år - 2012];
      else if (børn > 0) //forsøger under 30 år      
        if (enlig) //Enlige forsørgere, under 30 år
          sats = Satser.khUnderAldermBørnEnlig[år - 2012];
        else if (ægtefælleKontanthjælp || ægtefælleSU)
          sats = 9498; //Forsørgere under 30 år, gift/bor med person på SU, uddhj. eller kontanthj.
        else
          sats = Satser.khUnderAldermBørnParPåSU[år - 2012]; //Forsørgere under 30 år, gift/bor med person, der ikke er på SU, uddhj. eller kontanthj.
      else
        sats = Satser.khUnderAlderuBørnUdeboende[år - 0212]; //Under 30 år, udeboende, ikke forsørger

      //Ikke implementerede satser:
      //Under 30 år, hjemmeboende, ikke forsørger - sats = 3324;
      //Under 30 år, gravid (passeret 12. svangerskabsuge)
      //Under 30 år, psykisk syg, forsøgerpligt
      //Under 30 år, psykisk syg, udeboende
      //Under 30 år, bidragspligt, maksimum hjælp inkl. tillæg

      //Aktivitetstillæg
      int aktivitetstillægBeløb = 0;
      if (aktivitetstillæg && år >= 2014)
      {
        if (børn > 0 && alder < aldersgrænse)
        {
          if (enlig)
            aktivitetstillægBeløb = 628; //§ 25, stk 8, nr. 1
          else if (partnerModtagerSU)
            aktivitetstillægBeløb = 4705; //§ 25, stk 8, nr. 2
          else
            aktivitetstillægBeløb = 7314; //§ 25, stk 8, nr. 3
        }
        else if (alder > 25)
          if (udeboende)
            aktivitetstillægBeløb = 3800; //§ 25, stk 8, nr. 4
          else
            aktivitetstillægBeløb = 7365; //§ 25, stk. 8, nr. 5
      }

      //Ikke implementeret:      
      //Periodesanktioner
      //Punktsanktioner
      //Engangshjælp

      int kontanthjælp = sats - andenIndkomst - ægtefælleAndenIndkomst; //Ikke-arbejdsrelaterede indtægter fratrækkes

      if (arbejdstimer == 0)
        arbejdstimer = Math.Min(Convert.ToInt32(Convert.ToDouble(arbejdsindkomst) / _khTimesats[år - 1994] * (1 - _skatArbejdsmarkedsbidragssats[år - 1994])), _khMaksTimer); //beregning af arbejdstimer udfra arbejdsindtægt
      else
        arbejdstimer = Math.Min(arbejdstimer, _khMaksTimer);

      if (!enlig)
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

    public static int børneUngeYdelse(int barnetsAlder, int forsøger1Indkomst, int år, int forsøger2Indkomst = 0)
    {
      if (barnetsAlder >= 18)
        return 0; //kun børn kan modtage ydelsen

      int ydelse;
      if (barnetsAlder > 15)
        ydelse = Satser.Buydelse15_17[år - 2011];
      else if (barnetsAlder > 7)
        ydelse = Satser.Buydelse7_14[år - 2011];
      else if (barnetsAlder > 3)
        ydelse = Satser.Buydelse3_6[år - 2011];
      else
        ydelse = Satser.Buydelse0_2[år - 2011];

      if (år < 2014)
        return ydelse; //ingen aftrapning

      int aftrapning = Convert.ToInt32(Math.Max(0, 0.02 * (forsøger1Indkomst - 712600))) + Convert.ToInt32(Math.Max(0, 0.02 * (forsøger2Indkomst - 712600))); //For par, der er gift, opgøres aftrapningsgrundlaget som summen af den del af hver ægtefælles topskattegrundlag, der overstiger 712.600 kr.
      return ydelse - aftrapning;

      //Ikke implementeret
      //Aftrapning af ydelse ved flere børn...
      //Aftrapning for ikke-gifte par, det antages at par er gift
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arbejdstimer">Arbejdstimer pr måned</param>
    /// <returns></returns>
    public int ATP_indbetaling(int arbejdstimer)
    {
      //Satser fra http://www.virk.dk/files/live/sites/virk/files/PDF-Filer_og%20Word-dokumenter/ATP_LP/ATP-satser%20offentlige.pdf
      //Antager at personen er månedslønnet
      int atp;
      if (arbejdstimer >= 117)
        atp = 90;
      else if (arbejdstimer >= 78)
        atp = 60;
      else if (arbejdstimer >= 39)
        atp = 30;
      else
        atp = 0;

      return atp;

      //Ikke implementeret:
      //Personer på overførselsindkomst...
      //Beregner kun lønmodtagers andel
      //ikke-månedslønnende lønmodtagere
      //Antager at alle hører under A-bidrag
    }

    public int Boligstøtte(int børn, int kvardratmeter, int husleje)
    {
      return 0;
    }

    //Ikke implementerede ydelser:
    //Specielle ydelser og satser for indvandrere/udvandrere
    //Arbejdsskade
    //Sygedagpenge
    //Revalidering
    //Fleksjob
    //Førtidspension

    //Boligstøtte
    //Børnebidrag
    //Barselsdagpenge
    //Børnebidrag
    //Daginstitutionstilskud

    //Efterløn
    //Fleksydelse
    //Folkepension
    //ATP
    //Særlige ydelser til pensionister m.fl.

  }
}
