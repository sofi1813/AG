using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ags
{
    abstract class AbsClsIndividuo
    {
        public ClsSuperIndividuo _SuperIndv;
        public int[][,] _Horario_Docentes;
        public int[][,] _Horarios_Aulas;
        public int[][,] _Horario_Grupos;
        public int[,] _Horarios;
    }
}
