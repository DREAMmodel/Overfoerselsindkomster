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

      //Lav eksempel beregninger, evt. svarende til famlietype-modellens familie-modeller...
      Console.WriteLine("Enlig på kontanthjælp, år 2012"); //test eksempler...
      
      Boolean formue = false; //har ingen formue
      int alder = 30;
      int dur = 1;
      int forældreindkomst = 0;
      int arbejdsindkomst = 0;
      Boolean partnerModtagerSU = false;
      int arbejdstimer = 0;
      int andenIndkomst = 0;
      int børn = 1;
      Boolean enlig = true;
      Boolean udeboende = true;
      Boolean ægtefælleSU = false;
      int mdrbopæl = 0;
      Education uddannelse = Education.ErhFag;
      int år = 2012;
      int bundfradrag = 0;
      double aftrapningsprocent = 0;
      int ægtefælleAlder = 0;
      int ægtefælleArbejdsindkomst = 0;
      int ægtefælleArbejdstimer = 0;
      int ægtefælleAndenIndkomst = 0;
      Boolean ægtefælleKontanthjælp = false;
      Education ig = Education.IkkeUnderUddannelse;

      int kontanthjælp = Socialeydelser.Kontanthjælp(formue, alder, børn, dur, forældreindkomst, arbejdsindkomst, partnerModtagerSU, arbejdstimer, andenIndkomst, enlig, udeboende, ægtefælleSU, mdrbopæl, uddannelse, år, bundfradrag, aftrapningsprocent, ægtefælleAlder, ægtefælleArbejdsindkomst, ægtefælleArbejdstimer, ægtefælleAndenIndkomst, ægtefælleKontanthjælp, ig);

      Console.WriteLine("Kontanthjælp: " + kontanthjælp + " Kr.");

      Console.Read();
    }
  }
}