using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ags
{
    class ClsCargaDatos
    {
        ClsPeticiones peticiones;
        private DataSet consulta;

        public ClsCargaDatos(string _cnn) {
            peticiones = new ClsPeticiones(_cnn);
        }

        private int[,] Set_Turnos() {
            int[,] _Matriz;
            consulta = peticiones.Turnos();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new int[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
                _Matriz[indice, 1] = int.Parse(consulta.Tables[0].Rows[indice][1].ToString());
            }
            return _Matriz;
        }
        private int[] Set_Dias()
        {
            int[] Arreglo;
            consulta = peticiones.Dias();
            int tam = consulta.Tables[0].Rows.Count;
            Arreglo = new int[tam];
            for (int indice = 0; indice < tam; indice++)
            {
                Arreglo[indice] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
            }
            return Arreglo;
        }
        private int[][] Set_Materias()
        {
            int[][] _Tablas;
            consulta = peticiones.Materias();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[] _Arreglo;
                int tam = consulta.Tables[indice].Rows.Count;
                _Arreglo = new int[tam];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Arreglo[fila] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                }
                _Tablas[indice] = _Arreglo;
            }
            return _Tablas;
        }
        private int[,] Set_MateriasSuperbloques()
        {
            int[,] _Matriz;
            consulta = peticiones.Materias_Superbloques();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new int[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
                _Matriz[indice, 1] = int.Parse(consulta.Tables[0].Rows[indice][1].ToString());
            }
            return _Matriz;
        }
        private int[][] Set_Grupos()
        {
            int[][] _Tablas;
            consulta = peticiones.Grupos();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[] _Arreglo;
                int tam = consulta.Tables[indice].Rows.Count;
                _Arreglo = new int[tam];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Arreglo[fila] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                }
                _Tablas[indice] = _Arreglo;
            }
            return _Tablas;
        }
        private int[][,] Set_GrupoMateriaBloque()
        {
            int[][,] _Tablas;
            consulta = peticiones.GrupoMateriaBloque();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][,];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[,] _Matriz;
                int tam = consulta.Tables[indice].Rows.Count;
                _Matriz = new int[tam, 4];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Matriz[fila, 0] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                    _Matriz[fila, 1] = int.Parse(consulta.Tables[indice].Rows[fila][1].ToString());
                    _Matriz[fila, 2] = int.Parse(consulta.Tables[indice].Rows[fila][2].ToString());
                    _Matriz[fila, 3] = int.Parse(consulta.Tables[indice].Rows[fila][3].ToString());
                }
                _Tablas[indice] = _Matriz;
            }
            return _Tablas;
        }
        private int[][,] Set_GrupoMateria()
        {
            int[][,] _Tablas;
            consulta = peticiones._GrupoMateria();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][,];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[,] _Matriz;
                int tam = consulta.Tables[indice].Rows.Count;
                _Matriz = new int[tam,3];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Matriz[fila,0] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                    _Matriz[fila, 1] = int.Parse(consulta.Tables[indice].Rows[fila][1].ToString());
                    _Matriz[fila, 2] = int.Parse(consulta.Tables[indice].Rows[fila][2].ToString());
                }
                _Tablas[indice] = _Matriz;
            }
            return _Tablas;
        }
        private int[][,] Set_MateriasDocenteCantidad()
        {            
            int[][,] _Tablas;
            consulta = peticiones.Materia_Docente_Cantidad();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][,];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[,] _Matriz;
                int tam = consulta.Tables[indice].Rows.Count;
                _Matriz = new int[tam, 4];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Matriz[fila, 0] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                    _Matriz[fila, 1] = int.Parse(consulta.Tables[indice].Rows[fila][1].ToString());
                    _Matriz[fila, 2] = int.Parse(consulta.Tables[indice].Rows[fila][2].ToString());
                    _Matriz[indice, 3] = int.Parse(consulta.Tables[0].Rows[indice][3].ToString());
                }
                _Tablas[indice] = _Matriz;
            }
            return _Tablas;
        }
        private int [] Set_Docentes()
        {
            int[] _Arreglo;
            consulta = peticiones.Docentes();
            int tam = consulta.Tables[0].Rows.Count;
            _Arreglo = new int[tam];
            for (int indice = 0; indice < tam; indice++)
            {
                _Arreglo[indice] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
            }
            return _Arreglo;
        }
        private int[,] Set_MateriasAulas()
        {
            int[,] _Matriz;
            consulta = peticiones.MateriasAulas();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new int[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
                _Matriz[indice, 1] = int.Parse(consulta.Tables[0].Rows[indice][1].ToString());
            }
            return _Matriz;
        }
        private int[,] Set_BloquesDias()
        {
            int[,] _Matriz;
            consulta = peticiones.BloquesDias();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new int[tam, 3];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
                _Matriz[indice, 1] = int.Parse(consulta.Tables[0].Rows[indice][1].ToString());
                _Matriz[indice, 2] = int.Parse(consulta.Tables[0].Rows[indice][2].ToString());
            }
            return _Matriz;
        }
        private int[] Set_Aulas()
        {
            int []_Arreglo;
            consulta = peticiones.Aulas();
            int tam = consulta.Tables[0].Rows.Count;
            _Arreglo = new int[tam];
            for (int indice = 0; indice < tam; indice++)
            {
                _Arreglo[indice] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
            }
            return _Arreglo;
        }
        private int[][,] Set_HorasAulas()
        {
            int[][,] _Tablas;
            consulta = peticiones.Horas_Aulas();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][,];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[,] _Matriz;
                int tam = consulta.Tables[indice].Rows.Count;
                _Matriz = new int[tam, 2];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Matriz[fila, 0] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                    _Matriz[fila, 1] = int.Parse(consulta.Tables[indice].Rows[fila][1].ToString());
                }
                _Tablas[indice] = _Matriz;
            }
            return _Tablas;
        }
        private int[][,] Set_HorasGrupo()
        {
            int[][,] _Tablas;
            consulta = peticiones.Horas_Grupo();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][,];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[,] _Matriz;
                int tam = consulta.Tables[indice].Rows.Count;
                _Matriz = new int[tam,3];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Matriz[fila,0] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                    _Matriz[fila,1] = int.Parse(consulta.Tables[indice].Rows[fila][1].ToString());
                    _Matriz[fila, 2] = 0;
                }
                _Tablas[indice] = _Matriz;
            }
            return _Tablas;
        }
        private int[][,] Set_HorasDocente()
        {
            int[][,] _Tablas;
            consulta = peticiones.Horas_Docente();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][,];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[,] _Matriz;
                int tam = consulta.Tables[indice].Rows.Count;
                _Matriz = new int[tam, 2];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Matriz[fila, 0] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                    _Matriz[fila, 1] = int.Parse(consulta.Tables[indice].Rows[fila][1].ToString());
                }
                _Tablas[indice] = _Matriz;
            }
            return _Tablas;
        }
        private int[][,] Set_GrupoMateriaBLoqueCantidadAulas()
        {
            int[][,] _Tablas;
            consulta = peticiones.GrupoMateriaBloquesCantAulas();
            int NoTablas = consulta.Tables.Count;
            _Tablas = new int[NoTablas][,];
            for (int indice = 0; indice < NoTablas; indice++)
            {
                int[,] _Matriz;
                int tam = consulta.Tables[indice].Rows.Count;
                _Matriz = new int[tam, 4];
                for (int fila = 0; fila < tam; fila++)
                {
                    _Matriz[fila, 0] = int.Parse(consulta.Tables[indice].Rows[fila][0].ToString());
                    _Matriz[fila, 1] = int.Parse(consulta.Tables[indice].Rows[fila][1].ToString());
                    _Matriz[fila, 2] = int.Parse(consulta.Tables[indice].Rows[fila][2].ToString());
                    _Matriz[fila, 3] = int.Parse(consulta.Tables[indice].Rows[fila][3].ToString());
                }
                _Tablas[indice] = _Matriz;
            }
            return _Tablas;
        }

        // breakdown
        private string[,] Set_DatosGrupos()
        {
            string[,] _Matriz;
            consulta = peticiones.DatosGrupos();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new string[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = consulta.Tables[0].Rows[indice][0].ToString();
                _Matriz[indice, 1] = consulta.Tables[0].Rows[indice][1].ToString();
            }
            return _Matriz;
        }
        private string[,] Set_DatosAulas()
        {
            string[,] _Matriz;
            consulta = peticiones.DatosAulas();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new string[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = consulta.Tables[0].Rows[indice][0].ToString();
                _Matriz[indice, 1] = consulta.Tables[0].Rows[indice][1].ToString();
            }
            return _Matriz;
        }
        private string[,] Set_DatosDocentes()
        {
            string[,] _Matriz;
            consulta = peticiones.DatosDocentes();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new string[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = consulta.Tables[0].Rows[indice][0].ToString();
                _Matriz[indice, 1] = consulta.Tables[0].Rows[indice][1].ToString();
            }
            return _Matriz;
        }
        private string[,] Set_DatosMaterias()
        {
            string[,] _Matriz;
            consulta = peticiones.DatosMaterias();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new string[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = consulta.Tables[0].Rows[indice][0].ToString();
                _Matriz[indice, 1] = consulta.Tables[0].Rows[indice][1].ToString();
            }
            return _Matriz;
        }
        private int[] Set_BloquesReceso()
        {
            int[] _Matriz;
            consulta = peticiones.BloquesReceso();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new int[tam];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
            }
            return _Matriz;
        }
        private string[,] Set_Horas()
        {
            string[,] _Matriz;
            consulta = peticiones.Horas();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new string[tam, 3];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = consulta.Tables[0].Rows[indice][0].ToString();
                _Matriz[indice, 1] = consulta.Tables[0].Rows[indice][1].ToString();
                _Matriz[indice, 2] = consulta.Tables[0].Rows[indice][2].ToString();
            }
            return _Matriz;
        }
        private string[,] Set_DatosDias()
        {
            string[,] _Matriz;
            consulta = peticiones.DatosDias();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new string[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = consulta.Tables[0].Rows[indice][0].ToString();
                _Matriz[indice, 1] = consulta.Tables[0].Rows[indice][1].ToString();
            }
            return _Matriz;
        }
        private int[,] Set_DatosBloques()
        {
            int[,] _Matriz;
            consulta = peticiones.DatosBloques();
            int tam = consulta.Tables[0].Rows.Count;
            _Matriz = new int[tam, 3];
            for (int indice = 0; indice < tam; indice++)
            {
                _Matriz[indice, 0] = int.Parse(consulta.Tables[0].Rows[indice][0].ToString());
                _Matriz[indice, 1] = int.Parse(consulta.Tables[0].Rows[indice][1].ToString());
                _Matriz[indice, 2] = int.Parse(consulta.Tables[0].Rows[indice][2].ToString());
            }
            return _Matriz;
        }

        public void fill(ILlenadoBd bd)
        {
            bd.Set_Turnos(Set_Turnos());
            bd.Set_GrupoMateriaBloque(Set_GrupoMateriaBloque());
            bd.Set_Dias(Set_Dias());
            bd.Set_BloquesDias(Set_BloquesDias());
            bd.Set_Materias(Set_Materias());
            bd.Set_Grupos(Set_Grupos());
            bd.Set_GrupoMateria(Set_GrupoMateria());
            bd.Set_MateriasDocenteCantidad(Set_MateriasDocenteCantidad());
            bd.Set_Docentes(Set_Docentes());
            bd.Set_HorasDocentes(Set_HorasDocente());
            bd.Set_MateriasAulas(Set_MateriasAulas());
            bd.Set_Aulas(Set_Aulas());
            bd.Set_HorasGrupos(Set_HorasGrupo());
            bd.Set_HorasAulas(Set_HorasAulas());
            bd.Set_GrupoMateriaBloqueCantidadAulas(Set_GrupoMateriaBLoqueCantidadAulas());
            bd.Set_MateriasSuperbloques(Set_MateriasSuperbloques());

            // sofía
            bd.Set_DatosGrupos(Set_DatosGrupos());
            bd.Set_DatosAulas(Set_DatosAulas());
            bd.Set_DatosDocente(Set_DatosDocentes());
            bd.Set_DatosMaterias(Set_DatosMaterias());
            bd.Set_BloquesReceso(Set_BloquesReceso());
            bd.Set_Horas(Set_Horas());
            bd.Set_DatosDias(Set_DatosDias());
            bd.Set_DatosBloques(Set_DatosBloques());
        }
    }
}
