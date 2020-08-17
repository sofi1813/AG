using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ags
{
    static class GSuperIndividuo
    {
        public static BdVirtual _Datos;
        /*variable estatica del tipo BdVirtual que contiene copia de de la informacion de la 
         * Base de Datos Virtual*/        
        public static int[,] _MateriaDocenteCantidad;
        /*Variable que contiene una coleccion de datos de una materia que docente la imparte y a cuantos grupos puede impartirla 
         * el docente en relacion a un turno elegido*/
        public static int[,] _GrupoMateria;
        /*Variable que contiene una coleccion de datos de Grupos y las materias que se imparten en cada uno de ellos
         * en relacion a un turno elegido*/
        public static int[,] _GrupoMateriaSuperBloque;
        /*Variable que contiene una coleccion de datos con la cantidad de bloques que tiene una materia para los grupos de 
         * de un turno elegido*/
        public static int _tamIndividuo;
        /* Variable que permite conocer el tamaño que debe crearse del individuo se carga en relacion
         * a la eleccion del turno*/
        public static int _Turno;

        static GSuperIndividuo() {
            _tamIndividuo = -1;
            _Turno = -1;
        }


        public static void SetDb(BdVirtual bd,int turno) {
            if (_tamIndividuo == -1)
            {
                _Datos = bd;
                _Turno = turno;
                configuracion();
            }
            else if (_Turno != turno) {
                _Turno = turno;
                configuracion();
            }


        }

        private static void configuracion() // Este metodo permite elegir con que turno se desea trabajar
        {
            _MateriaDocenteCantidad = _Datos.MateriaDocenteCantidad[_Turno];
            _tamIndividuo = _Datos.Turnos[_Turno, 1];
            _GrupoMateria = _Datos.GrupoMateria[_Turno];
            _GrupoMateriaSuperBloque = _Datos.GrupoMateriaBloque[_Turno];
        }
    }
}
