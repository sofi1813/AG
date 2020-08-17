using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ags
{
    class Program
    {
        [STAThread()]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            /*string pathConnection = "C:/Users/sofil/source/repos/Ag/Ags/Ags/bin/Debug/BDHorariosUTHH.db";
            BdVirtual bd = new BdVirtual();
            ClsCargaDatos llenado = new ClsCargaDatos(pathConnection);
            llenado.fill(bd);
            llenado = null;
            int[,] MatrizTurnos = bd.Turnos;
            int Turno = 0;
            Console.WriteLine("Elije Turno");
            for (int indice = 0; indice < MatrizTurnos.GetLength(0); indice++)
            {
                Console.WriteLine("[" + indice + "]" + "Turno " + MatrizTurnos[indice, 0]);
            }

            //Turno = int.Parse(Console.ReadLine());

            Turno = 0;
            GSuperIndividuo.SetDb(bd, Turno);
            ClsSuperIndividuo Super1 = new ClsSuperIndividuo();

            Console.WriteLine("Indica el tamaño de la poblacion");
            ClsAlgoritmo algr = new ClsAlgoritmo(Super1, 100, 100);

            Console.WriteLine("Agradecido con el d arriba");
            algr.Run();

            //ClsIndividuo indv = new ClsIndividuo(Super1);
            //indv.imprimirIndividuo();

            Console.ReadKey();*/

        }
    }
}
