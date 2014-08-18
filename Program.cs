using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Console;

namespace overfoerselsindkomster
{
  class Program
  {
    static void Main(string[] args)
    {    
      Console.WriteLine("Eksempel/test-beregninger:");

     int alder = 30;
      int arbejdsindkomst = 0;
            int år = 2012;
      Education hf = Education.ErhFag; //højst fuldførte uddannelse

      //Lav eksempel beregninger, evt. svarende til famlietype-modellens familie-modeller...
      Console.WriteLine("Enlig på kontanthjælp, år 2012"); //test eksempler...
      
      Boolean formue = false; //har ingen formue
      int dur = 1;
      int forældreindkomst = 0;
      Boolean partnerModtagerSU = false;
      int arbejdstimer = 0;
      int andenIndkomst = 0;
      int børn = 1;
      Boolean enlig = true;
      Boolean udeboende = true;
      Boolean ægtefælleSU = false;
      int mdrbopæl = 0;
      int bundfradrag = 0;
      double aftrapningsprocent = 0;
      int ægtefælleAlder = 0;
      int ægtefælleArbejdsindkomst = 0;
      int ægtefælleArbejdstimer = 0;
      int ægtefælleAndenIndkomst = 0;
      Boolean ægtefælleKontanthjælp = false;
      Education ig = Education.IkkeUnderUddannelse;

      int kontanthjælp = Socialeydelser.Kontanthjælp(formue, alder, børn, dur, forældreindkomst, arbejdsindkomst, partnerModtagerSU, arbejdstimer, andenIndkomst, enlig, udeboende, ægtefælleSU, mdrbopæl, hf, år, bundfradrag, aftrapningsprocent, ægtefælleAlder, ægtefælleArbejdsindkomst, ægtefælleArbejdstimer, ægtefælleAndenIndkomst, ægtefælleKontanthjælp, ig);
      
      Console.WriteLine("Kontanthjælp: " + kontanthjælp + " Kr.");
      Console.WriteLine("ATP: ???");
      Console.WriteLine("Boligstøtte: ???");

      int barnetsAlder = 10;
      int forsøger1Indkomst = kontanthjælp;
      int børneydelse = Socialeydelser.børneUngeYdelse(barnetsAlder, forsøger1Indkomst, år);
      Console.WriteLine("Børneydelse: " + børneydelse + "Kr.");



      Console.WriteLine("Enlig på dagpenge, 1 barn, år 2012"); //Eksempel 2
      Boolean deltidsforsikret = false;
      Boolean akasse = true;
      int ugerBeskæftigetSomLønmodtager = 52; //har arbejdet et år førend
      int månedslønFørArbejdsløshed = 35000; //tjente 25000 om måneden før arbejdsløshed
      arbejdsindkomst = 2000;
      int dagpenge = Socialeydelser.Dagpenge(alder, deltidsforsikret, arbejdsindkomst, arbejdstimer, år, akasse, ugerBeskæftigetSomLønmodtager, månedslønFørArbejdsløshed, hf);

      Console.WriteLine("Dagpenge: " + dagpenge + " Kr.");
      Console.WriteLine("ATP: ???");
      Console.WriteLine("Boligstøtte: ???");
      Console.WriteLine("ATP: ???");

      barnetsAlder = 1;
      forsøger1Indkomst = dagpenge;
      børneydelse = Socialeydelser.børneUngeYdelse(barnetsAlder, forsøger1Indkomst, år);
      Console.WriteLine("Børneydelse: " + børneydelse + "Kr.");

      Console.Read();
    }
  }
}