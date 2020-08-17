using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ags
{
    interface ILlenadoBd
    {
        void Set_Turnos(int [,] arreglo);
        void Set_GrupoMateriaBloque(int[][,] matriz);
        void Set_Materias(int[][] arreglo); // Genera el arreglo de numeros de materias. ej. [1,3,6,7]
        void Set_Dias(int []arreglo);
        void Set_GrupoMateria(int[][,] matriz);//Genera una matriz que contiene al grupo y la materia [5,7][5,8][5,9][6,7][6,8[6,9]
        void Set_MateriasDocenteCantidad(int[][,] matriz); 
        void Set_Docentes(int[] arreglo); // Genera el arreglo de docentes ej. [1,2,3,4,5]
        void Set_MateriasAulas(int[,]matriz);
        void Set_Aulas(int[]arreglo);
        void Set_Grupos(int[][]Escalonada);
        void Set_HorasAulas(int[][,] Escalonada);
        void Set_HorasDocentes(int [][,]Escalonada);
        void Set_HorasGrupos(int[][,]Escalonada);
        void Set_GrupoMateriaBloqueCantidadAulas(int[][,]matriz);
        void Set_BloquesDias(int[,] matriz);
        void Set_MateriasSuperbloques(int[,] matriz);

        // breakdown
        void Set_DatosGrupos(string[,] matriz);
        void Set_DatosAulas(string[,] matriz);
        void Set_DatosDocente(string[,] matriz);
        void Set_DatosMaterias(string[,] matriz);
        void Set_BloquesReceso(int[] arreglo);
        void Set_Horas(string[,] matriz);
        void Set_DatosDias(string[,] matriz);
        void Set_DatosBloques(int[,] matriz);

    }
}
