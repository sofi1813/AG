using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Ags
{
    class BdVirtual : ILlenadoBd
    {
        private int[] _Aulas;
        private int[] _Dias;
        private int[] _Docentes;
        private int[][] _Materias;       
        private int[][] _Grupos;
        private int[][] _Bloques_Horario_Aulas;
        private int[,] _Turnos;
        private int[,] _MateriasAulas;
        private int[][,] _GrupoMateriaBloque;       
        private int[][,] _GrupoMateria;
        private int[][,] _MateriaDocenteCantidad;
        private int[][][] _Multi_MateriasAulasPosibles;       
        private int[][,] _GrupoMateriaBloqueCantidadAulas;
        private int[][][,] _Horarios_Aulas;
        private int[][][,] _Horarios_Grupos;
        private int[][][,] _Horarios_Docentes;
        private int[,] _Bloques_Dias;
        private int[][][] _Multi_BloquesDias;
        private int[,] _MateriasSuperbloques;
        private int[][][] _Multi_MateriasSuperbloques;

        // breakdown
        private string[,] _DatosGrupos;
        private string[,] _DatosAulas;
        private string[,] _DatosDocente;
        private string[,] _DatosMaterias;
        private int[] _BloquesReceso;
        private string[,] _Horas;
        private string[,] _DatosDias;
        private int[,] _DatosBloques;
        private int[][][] _Multi_DatosBloques;

        #region Metodos Get
        public int[,] Turnos { get => ClonadorMatrices(_Turnos); }
        public int[][,] GrupoMateriaBloque { get => ClonadorMatrizEscalonada(_GrupoMateriaBloque); }
        public int[][] Materias { get => ClonadorArregloEscalonada(_Materias); }
        public int[][] Grupos { get => ClonadorArregloEscalonada(_Grupos); }
        public int[][,] GrupoMateria { get => ClonadorMatrizEscalonada(_GrupoMateria); }
        public int[][,] MateriaDocenteCantidad { get => ClonadorMatrizEscalonada(_MateriaDocenteCantidad); }
        public int[] Docentes { get => ClonadorArreglo(_Docentes); }
        public int[,] MateriasAulas { get => ClonadorMatrices(_MateriasAulas); }
        public int[] Aulas { get => ClonadorArreglo(_Aulas); }
        public int[][,] GrupoMateriaBloquesCantidadAulas {get => ClonadorMatrizEscalonada(_GrupoMateriaBloqueCantidadAulas) ; }
        public int[][][] ArregloMateriasAulasPosibles { get => ClonadorArregloMultidimension(_Multi_MateriasAulasPosibles); }
        public int[][][,] Horarios_Aulas { get => ClonadorMatrizMultidimensional(_Horarios_Aulas); }
        public int[][][,] Horarios_Docentes { get => ClonadorMatrizMultidimensional(_Horarios_Docentes); }
        public int[][][,] Horarios_Grupos { get => ClonadorMatrizMultidimensional(_Horarios_Grupos); }
        public int[][] TotalesBloquesAulas{ get => ClonadorArregloEscalonada(_Bloques_Horario_Aulas); }
        public int[] Dias { get => ClonadorArreglo(_Dias); }
        public int[,] Bloques_Dias { get => ClonadorMatrices(_Bloques_Dias); }
        public int[][][] MultiBloquesDias{ get => ClonadorArregloMultidimension(_Multi_BloquesDias); }
        public int[,] MateriasSuperbloques { get => ClonadorMatrices(_MateriasSuperbloques); }
        public int[][][] MultiMateriasSuperbloques { get => ClonadorArregloMultidimension(_Multi_MateriasSuperbloques); }

        // breakdown
        public string[,] DatosGrupos { get => ClonadorMatricesString(_DatosGrupos); }
        public string[,] DatosAulas { get => ClonadorMatricesString(_DatosAulas); }
        public string[,] DatosDocentes { get => ClonadorMatricesString(_DatosDocente); }
        public string[,] DatosMaterias { get => ClonadorMatricesString(_DatosMaterias); }
        public int[] BloquesReceso { get => ClonadorArreglo(_BloquesReceso); }
        public string[,] Horas { get => ClonadorMatricesString(_Horas); }
        public string[,] DatosDias { get => ClonadorMatricesString(_DatosDias); }
        public int[,] DatosBloques { get => ClonadorMatrices(_DatosBloques); }
        public int[][][] MultiDatosBloques { get => ClonadorArregloMultidimension(_Multi_DatosBloques); }
        #endregion

        #region Metodos Interfaz
        void ILlenadoBd.Set_Turnos(int[,] Matriz)
        {
            _Turnos = Matriz;
        }
        void ILlenadoBd.Set_GrupoMateriaBloque(int[][,] Escalonada)
        {
            _GrupoMateriaBloque = Escalonada;
        }
        void ILlenadoBd.Set_Materias(int[][] arreglo)
        {
            _Materias = arreglo;
        }
        void ILlenadoBd.Set_Dias(int[] arreglo) {
            _Dias = arreglo;
        }
        void ILlenadoBd.Set_Grupos(int[][] Escalonada) {
            _Grupos = Escalonada;
        }
        void ILlenadoBd.Set_GrupoMateria(int[][,] matriz)
        {
            _GrupoMateria = matriz;
        }
        void ILlenadoBd.Set_MateriasDocenteCantidad(int[][,] Escalonada)
        {
            _MateriaDocenteCantidad = Escalonada;

        }
        void ILlenadoBd.Set_Docentes(int[] arreglo)
        {
            _Docentes = arreglo;
        }
        void ILlenadoBd.Set_HorasDocentes(int[][,] matriz)
        {
            _Horarios_Docentes = HorariosTurno(matriz,_Docentes);
        }
        void ILlenadoBd.Set_MateriasAulas(int[,] matriz)
        {
            _MateriasAulas = matriz;
            Set_MateriasAulasPosibles();
        }
        void ILlenadoBd.Set_Aulas(int[] arreglo)
        {
            _Aulas = arreglo;
        }
        void ILlenadoBd.Set_HorasAulas(int[][,] Escalonada)
        {
            _Horarios_Aulas = HorariosTurno(Escalonada,_Aulas);
            _Bloques_Horario_Aulas = bloquesHorarios(_Horarios_Aulas);/*________________________________________*/
        }
        void ILlenadoBd.Set_GrupoMateriaBloqueCantidadAulas(int[][,] matriz)
        {
            _GrupoMateriaBloqueCantidadAulas = matriz;
        }
        void ILlenadoBd.Set_HorasGrupos(int[][,] Escalonada) {
            _Horarios_Grupos = Carga_Horario_Grupo(Escalonada);
        }
        void ILlenadoBd.Set_BloquesDias(int[,] matriz)
        {
            _Bloques_Dias = matriz;
            Set_MultiBloquesDias();
        }
        void ILlenadoBd.Set_MateriasSuperbloques(int[,] matriz)
        {
            _MateriasSuperbloques = matriz;
            Set_MultiMateriasSuperbloques();
        }

        // breakdown
        void ILlenadoBd.Set_DatosGrupos(string[,] matriz)
        {
            _DatosGrupos = matriz;
        }
        void ILlenadoBd.Set_DatosAulas(string[,] matriz)
        {
            _DatosAulas = matriz;
        }
        void ILlenadoBd.Set_DatosDocente(string[,] matriz)
        {
            _DatosDocente = matriz;
        }
        void ILlenadoBd.Set_DatosMaterias(string[,] matriz)
        {
            _DatosMaterias = matriz;
        }
        void ILlenadoBd.Set_BloquesReceso(int[] arreglo)
        {
            _BloquesReceso = arreglo;
        }
        void ILlenadoBd.Set_Horas(string[,] matriz)
        {
            _Horas = matriz;
        }
        void ILlenadoBd.Set_DatosDias(string[,] matriz)
        {
            _DatosDias = matriz;
        }
        void ILlenadoBd.Set_DatosBloques(int[,] matriz)
        {
            _DatosBloques = matriz;
            Set_MultiDatosBloques();
        }

        #endregion

        #region CLonadores
        private int[] ClonadorArreglo(int[] arreglo)
        {
            int[] CloneArreglo = new int[arreglo.Length];

            for (int i = 0; i < arreglo.Length; i++)
                CloneArreglo[i] = arreglo[i];

            return CloneArreglo;
        }
        private int[,] ClonadorMatrices(int[,] matriz)
        {
            int[,] CloneMatriz = new int[matriz.GetLength(0), matriz.GetLength(1)];

            for (int j = 0; j < matriz.GetLength(0); j++)
                for (int k = 0; k < matriz.GetLength(1); k++)
                    CloneMatriz[j, k] = matriz[j, k];

            return CloneMatriz;
        }
        private int[][] ClonadorArregloEscalonada(int[][] mEscalonada)
        {
            int[][] CloneEscalonada = new int[mEscalonada.Length][];

            for (int i = 0; i < mEscalonada.Length; i++)
            {
                CloneEscalonada[i] = new int[mEscalonada[i].Length];

                for (int j = 0; j < mEscalonada[i].Length; j++)
                    CloneEscalonada[i][j] = mEscalonada[i][j];
            }

            return CloneEscalonada;
        }
        private int[][,] ClonadorMatrizEscalonada(int[][,] mEscalonada)
        {
            int[][,] CloneEscalonada = new int[mEscalonada.Length][,];

            for (int i = 0; i < mEscalonada.Length; i++)
            {
                CloneEscalonada[i] = new int[mEscalonada[i].GetLength(0), mEscalonada[i].GetLength(1)];

                for (int j = 0; j < mEscalonada[i].GetLength(0); j++)
                    for (int k = 0; k < mEscalonada[i].GetLength(1); k++)
                        CloneEscalonada[i][j, k] = mEscalonada[i][j, k];
            }

            return CloneEscalonada;
        }
        private int[][][,] ClonadorMatrizMultidimensional(int[][][,] mEscalonada)
        {
            int[][][,] CloneMultidimensional = new int[mEscalonada.Length][][,];

            for (int h = 0; h < mEscalonada.Length; h++)
            {
                CloneMultidimensional[h] = new int[mEscalonada[h].Length][,];

                for (int i = 0; i < mEscalonada[h].Length; i++)
                {
                    CloneMultidimensional[h][i] = new int[mEscalonada[h][i].GetLength(0), mEscalonada[h][i].GetLength(1)];

                    for (int j = 0; j < mEscalonada[h][i].GetLength(0); j++)
                        for (int k = 0; k < mEscalonada[h][i].GetLength(1); k++)
                            CloneMultidimensional[h][i][j, k] = mEscalonada[h][i][j, k];
                }
            }

            return CloneMultidimensional;
        }        
        private int[][][] ClonadorArregloMultidimension(int[][][] Multidimensional)
        {
            int[][][] CloneMultidimensional = new int[Multidimensional.Length][][];

            for (int i = 0; i < Multidimensional.Length; i++)
            {
                CloneMultidimensional[i] = new int[Multidimensional[i].Length][];

                for (int j = 0; j < Multidimensional[i].Length; j++)
                {
                    CloneMultidimensional[i][j] = new int[Multidimensional[i][j].Length];

                    for (int k = 0; k < Multidimensional[i][j].Length; k++)
                        CloneMultidimensional[i][j][k] = Multidimensional[i][j][k];
                }
            }

            return CloneMultidimensional;
        }

        // breakdown
        private string[,] ClonadorMatricesString(string[,] matriz)
        {
            string[,] CloneMatriz = new string[matriz.GetLength(0), matriz.GetLength(1)];

            for (int j = 0; j < matriz.GetLength(0); j++)
                for (int k = 0; k < matriz.GetLength(1); k++)
                    CloneMatriz[j, k] = matriz[j, k];

            return CloneMatriz;
        }

        #endregion

        #region Utilerias

        private void Set_MultiMateriasSuperbloques()
        {
            int dimensiones = _Turnos.GetLength(0);
            _Multi_MateriasSuperbloques = new int[dimensiones][][];
            for(int indice = 0; indice < dimensiones; indice++)
            {
                int[] materias = _Materias[indice];
                _Multi_MateriasSuperbloques[indice] = new int[materias.Length][];
                for(int indice2 = 0; indice2 < materias.Length; indice2++)
                {
                    _Multi_MateriasSuperbloques[indice][indice2] = SuperbloquesMateria(materias[indice2]);
                }
            }
        }
        private int[] SuperbloquesMateria(int materia)
        {
            LinkedList<int> superbloques = new LinkedList<int>();

            for(int i = 0; i < _MateriasSuperbloques.GetLength(0); i++)
            {
                if (_MateriasSuperbloques[i, 0] == materia)
                    superbloques.AddLast(_MateriasSuperbloques[i, 1]);
            }

            int[] resultado = new int[superbloques.Count];

            superbloques.CopyTo(resultado, 0);

            return resultado;
        }
        private void Set_MateriasAulasPosibles()
        {
            int dimensiones = _Turnos.GetLength(0);
            int _tam = _Materias.Length;
            _Multi_MateriasAulasPosibles = new int[dimensiones][][];
            for (int indice = 0; indice < dimensiones; indice++)
            {
                int[] Materias = _Materias[indice];
                _Multi_MateriasAulasPosibles[indice] = new int[Materias.Length][];
                for (int filas = 0; filas < Materias.Length; filas++)
                {
                    _Multi_MateriasAulasPosibles[indice][filas] = busca_AulasMateria(_MateriasAulas,Materias[filas]);
                }
            }
       }
        private int[] busca_AulasMateria(int[,] _MatrizAulas, int clave)
        {
            int tam = _MatrizAulas.GetLength(0);
            int[] Matriz_Aulas;
            LinkedList<int> _Aula = new LinkedList<int>();
            for (int indice = 0; indice < tam; indice++)
            {
                if (_MatrizAulas[indice, 0] == clave)
                {
                    _Aula.AddLast(_MatrizAulas[indice, 1]);
                }
            }
            int dimension = _Aula.Count;
            Matriz_Aulas = new int[dimension];
            for (int indice = 0; indice < dimension; indice++)
            {
                Matriz_Aulas[indice] = _Aula.ElementAt(indice);
            }
            return Matriz_Aulas;
        }
        private void Set_MultiBloquesDias()
        {
            int dimensiones = _Turnos.GetLength(0);
            int tam = _Dias.Length;
            _Multi_BloquesDias = new int[dimensiones][][];
            for(int indice = 0; indice < dimensiones; indice++)
            {
                _Multi_BloquesDias[indice] = new int[tam][];
                for(int filas = 0; filas < tam; filas++)
                {
                    _Multi_BloquesDias[indice][filas] = busca_DiasBloques(_Bloques_Dias, _Turnos[indice,0], _Dias[filas]);
                }
            }
        }
        public int[] busca_DiasBloques(int[,] MatrizBloques, int turno, int dia)
        {
            int[] bloques;
            int tam = MatrizBloques.GetLength(0);
            LinkedList<int> ListaBloques = new LinkedList<int>();
            for(int indice = 0; indice < tam; indice++)
            {
                if(MatrizBloques[indice,0] == turno && MatrizBloques[indice,1] == dia)
                {
                    ListaBloques.AddLast(MatrizBloques[indice,2]);
                }
            }
            int dimension = ListaBloques.Count;
            bloques = new int[dimension];
            for(int indice = 0; indice < dimension; indice++)
            {
                bloques[indice] = ListaBloques.ElementAt(indice);
            }
            return bloques;
        }
        private int[][][,] HorariosTurno(int[][,] Escalonada,int []Arreglo)
        {
            int dim = _Turnos.GetLength(0);
            int[][][,] Multidimensional = new int[dim][][,];
            for (int i = 0; i < dim; i++)
            {
                Multidimensional[i] = new int[Arreglo.GetLength(0)][,];
                int[][,] horarios = Separa(Escalonada[i],Arreglo);
                for (int p = 0; p < Arreglo.GetLength(0); p++)
                {
                    Multidimensional[i][p] = horarios[p];
                }
            }
            return Multidimensional;
        }
        private int[][,] Separa(int[,] Matriz, int[] Arreglo)
        {
            int[][,] separados = new int[Arreglo.GetLength(0)][,];
            int filas = Matriz.GetLength(0);
            for (int aulas = 0; aulas < Arreglo.GetLength(0); aulas++)
            {
                LinkedList<int> Parametro1 = new LinkedList<int>();
                LinkedList<int> Parametro2 = new LinkedList<int>();
                for (int indice = 0; indice < filas; indice++)
                {
                    if (Matriz[indice, 0] == Arreglo[aulas])
                    {
                        Parametro1.AddLast(Matriz[indice, 0]);
                        Parametro2.AddLast(Matriz[indice, 1]);
                    }
                }
                int tam = Parametro1.Count;
                int[,] matriz = new int[tam, 3];
                for (int indice = 0; indice < tam; indice++)
                {
                    matriz[indice, 0] = Parametro1.ElementAt(indice);
                    matriz[indice, 1] = Parametro2.ElementAt(indice);
                    matriz[indice, 2] = 0;
                }
                separados[aulas] = matriz;
            }
            return separados;
        }
        private int[][][,] Carga_Horario_Grupo(int [][,]Escalonada) {
            int dim = _Turnos.GetLength(0);
            int[][][,] Multidimension = new int[dim][][,];
            for (int indice = 0; indice < _Turnos.GetLength(0); indice++)
            {
                int[] Grupos = _Grupos[indice];
                Multidimension[indice] = new int[Grupos.Length][,];
                for (int filas = 0; filas < Grupos.Length; filas++)
                {
                    Multidimension[indice][filas] = Escalonada[indice];
                }
            }
            return Multidimension;
        }

        // breakdown
        private void Set_MultiDatosBloques()
        {
            int dimensiones = _Turnos.GetLength(0);
            int tam = _Dias.Length;
            _Multi_DatosBloques = new int[dimensiones][][];
            for (int indice = 0; indice < dimensiones; indice++)
            {
                _Multi_DatosBloques[indice] = new int[tam][];
                for (int filas = 0; filas < tam; filas++)
                {
                    _Multi_DatosBloques[indice][filas] = busca_DiasBloques(_DatosBloques, _Turnos[indice, 0], _Dias[filas]);
                }
            }
        }

        #endregion

        private int[][] bloquesHorarios(int[][][,]Escalonada) 
        {
            int tam = Escalonada.GetLength(0);
            int [][]prueba = new int[tam][];
            for (int indice = 0; indice < tam; indice++)
            {
                int dim = Escalonada[indice].GetLength(0);
                prueba[indice] = new int[dim];
                int[] arreglo = new int[1];
                for (int filas = 0; filas < dim; filas++)
                {
                    
                    int total = Escalonada[indice][filas].GetLength(0);
                    arreglo[filas]= total;
                    Array.Resize(ref arreglo, arreglo.Length + 1);
                }
                Array.Resize(ref arreglo, arreglo.Length -1);
                prueba[indice] = arreglo;
            }
            return prueba;
        }

    }

    

}

