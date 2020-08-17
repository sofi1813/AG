using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Ags
{
    class ClsSuperIndividuo
    {
        #region Variables Dinamicas

        public BdVirtual _Datos;
        public int _Turno;

        private int[,] _Bloques_x_Aula;//--------------------
        private int[][] _Bloques_x_Dia;//--------------------
        private int[][] _MateriaAulasPosibles;//--------------------
        private int[] _Materias;//--------------------
        private int[] _Docentes;//--------------------
        private int[] _Grupos;//--------------------
        private int[] _Aulas;//--------------------
        private int[][] _MateriasSuperbloques;//------------------

        private int[,] _ClaveGrupoMateriaDocente;//cada objeto tiene su propia copia 
        /*Dentro de esta variable se encuentra la estructura un superindividuo, es decir 
         cuenta con una clave autoincrementable para cada registro y en dicho registro se encuentra el Grupo la materia y un Docente
         ejemplo: [1,5,7,1] el primer valor es la clave autoincrementable, el segundo valor es el grupo, el tercer valor es la materia
         y el cuarto valor es el docente*/
        private int[,] _ClaveAulaBloqueVacio;
        /*Dentro de esta variable se encuentra la estructura basica de un superindividuo, es decir 
         * utilizando la clave autoincrementable de la variable _ClaveGrupoMateriaDocente se añade las columnas  para 
         * Aula y Bloque inicializadas como vacias ejemplo [1,-1,-1] el primero valor es la clave extraida de la matriz _ClaveGrupoMateriaDocente
         * el segundo valor es el de aula incializada en -1 y el tercer valor es el bloque incializado en -1(este parametro reprsenta a un vacio)*/

        // breakdown
        private string[,] _DatosGrupo;
        private string[,] _DatosAula;
        private string[,] _DatosDocente;
        private string[,] _DatosMaterias;
        private string[,] _Horas;
        private string[,] _Dias;
        private int[][] _DatosBloques;
        private int[] _BloquesReceso;

        #endregion
        //public int[,] ClavAulaBloqueVacio { get => _ClaveAulaBloqueVacio; }


        #region getters
        public int[,] ClaveGrupoMateriaDocente
        {
            get => ClonadorMatrices(_ClaveGrupoMateriaDocente);
            set => _ClaveGrupoMateriaDocente = ClonadorMatrices(value);
        }
        public int[,] BloquesAulas { get => ClonadorMatrices(_Bloques_x_Aula); }
        public int[] Materias { get => ClonadorArreglo(_Materias); }
        public int[] Docentes { get => ClonadorArreglo(_Docentes); }
        public int[] Aulas { get => ClonadorArreglo(_Aulas); }
        public int[] Grupos { get => ClonadorArreglo(_Grupos); }
        public int[][] MateriasAulasPosibles { get => ClonadorArregloEscalonada(_MateriaAulasPosibles); }
        public int[][] BloquesDias { get => ClonadorArregloEscalonada(_Bloques_x_Dia); }
        public int[][] MateriasSuperbloques { get => ClonadorArregloEscalonada(_MateriasSuperbloques); }

        // breakdown
        public string[,] DatosGrupos { get => _DatosGrupo; }
        public string[,] DatosAula { get => _DatosAula; }
        public string[,] DatosDocente { get => _DatosDocente; }
        public string[,] DatosMaterias { get => _DatosMaterias; }
        public string[,] Horas { get => _Horas; }
        public string[,] Dias { get => _Dias; }
        public int[][] DatosBloques { get => _DatosBloques; }
        public int[] BloquesReceso { get => _BloquesReceso; }
        #endregion

        #region clonadores
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
        #endregion


        #region Constructor 
        public ClsSuperIndividuo()
        {
            /*_ClaveAulaBloqueVacio = llenado_Clave_Aula_Bloque(llenado_Clave_Grupo_Materia_Docente());*/
            /* Dentro de este apartado a la matriz _ClaveAulabloque  se le cargan datos mediante un metodo 
              que se encarga de llenar con claves y valores -1 para las columnas aula y bloque, cabe mencionar
              que la matriz que contiene las claves se crea apartir del metodo llenado_Clave_Grupo_Materia_Docente*/
            configurar();
            llenado_Clave_Grupo_Materia_Docente();
            imprimirSuperIndividuo();//Metod que imprime la matriz _ClaveGrupoMateriaDocente
            //imprimirSuperIndividuo1();//Metod que imprime la matriz _ClaveAulaBloque
        }
        public ClsSuperIndividuo(int[,] ClaveGrupoMateriaDocente, int turno)
        {
            /*_ClaveAulaBloqueVacio = llenado_Clave_Aula_Bloque(llenado_Clave_Grupo_Materia_Docente());*/
            /* Dentro de este apartado a la matriz _ClaveAulabloque  se le cargan datos mediante un metodo 
              que se encarga de llenar con claves y valores -1 para las columnas aula y bloque, cabe mencionar
              que la matriz que contiene las claves se crea apartir del metodo llenado_Clave_Grupo_Materia_Docente*/            
            this.ClaveGrupoMateriaDocente = ClaveGrupoMateriaDocente;
            //_Turno = turno;
            configurar();
            imprimirSuperIndividuo();//Metod que imprime la matriz _ClaveGrupoMateriaDocente
            //imprimirSuperIndividuo1();//Metod que imprime la matriz _ClaveAulaBloque                         
        }
        #endregion

        private void configurar()
        {
            _Turno = GSuperIndividuo._Turno;
            _Datos = GSuperIndividuo._Datos;
            _MateriasSuperbloques = _Datos.MultiMateriasSuperbloques[_Turno];
            _Aulas = _Datos.Aulas;
            _Docentes = _Datos.Docentes;
            _Grupos = _Datos.Grupos[_Turno];
            _Materias = _Datos.Materias[_Turno];
            _MateriaAulasPosibles = _Datos.ArregloMateriasAulasPosibles[_Turno];
            _Bloques_x_Aula = _ObtenerAula_Bloques(_Aulas, _Datos.TotalesBloquesAulas[_Turno]);
            _Bloques_x_Dia = _Datos.MultiBloquesDias[_Turno];

            // breakdown
            _DatosGrupo = GSuperIndividuo._Datos.DatosGrupos;
            _DatosAula = GSuperIndividuo._Datos.DatosAulas;
            _DatosDocente = GSuperIndividuo._Datos.DatosDocentes;
            _DatosMaterias = GSuperIndividuo._Datos.DatosMaterias;
            _Horas = _AcomodaHoras(GSuperIndividuo._Datos.Horas);
            _Dias = GSuperIndividuo._Datos.DatosDias;
            _DatosBloques = GSuperIndividuo._Datos.MultiDatosBloques[GSuperIndividuo._Turno];
            _BloquesReceso = GSuperIndividuo._Datos.BloquesReceso;
        }

        private void configurar2()
        {
            _Datos = GSuperIndividuo._Datos;
            _MateriasSuperbloques = _Datos.MultiMateriasSuperbloques[_Turno];
            _Aulas = _Datos.Aulas;
            _Docentes = _Datos.Docentes;
            _Grupos = _Datos.Grupos[_Turno];
            _Materias = _Datos.Materias[_Turno];
            _MateriaAulasPosibles = _Datos.ArregloMateriasAulasPosibles[_Turno];
            _Bloques_x_Aula = _ObtenerAula_Bloques(_Aulas, _Datos.TotalesBloquesAulas[_Turno]);
            _Bloques_x_Dia = _Datos.MultiBloquesDias[_Turno];

            // breakdown
            _DatosGrupo = GSuperIndividuo._Datos.DatosGrupos;
            _DatosAula = GSuperIndividuo._Datos.DatosAulas;
            _DatosDocente = GSuperIndividuo._Datos.DatosDocentes;
            _DatosMaterias = GSuperIndividuo._Datos.DatosMaterias;
            _Horas = _AcomodaHoras(GSuperIndividuo._Datos.Horas);
            _Dias = GSuperIndividuo._Datos.DatosDias;
            _DatosBloques = GSuperIndividuo._Datos.MultiDatosBloques[GSuperIndividuo._Turno];
            _BloquesReceso = GSuperIndividuo._Datos.BloquesReceso;
        }

        private int[,] _ObtenerAula_Bloques(int[] Aulas, int[] Bloques) //Ingresa en una matriz la relacion Aula/Numero de bloques
        {
            int tam = Aulas.Length;
            int[,] Aula_Bloques = new int[tam, 2];
            for (int indice = 0; indice < tam; indice++)
            {
                Aula_Bloques[indice, 0] = Aulas[indice];
                Aula_Bloques[indice, 1] = Bloques[indice];
            }
            return Aula_Bloques;
        }
        private int[,] llenado_Clave_Grupo_Materia_Docente() //Metodo que crea la matriz Clave,Grupo,Materia,Docente
        {
            int filas = GSuperIndividuo._GrupoMateria.GetLength(0), Grupo, Materia;
            _ClaveGrupoMateriaDocente = new int[filas, 4];
            for (int indice = 0; indice < filas; indice++)
            {
                Grupo = GSuperIndividuo._GrupoMateria[indice, 1];
                Materia = GSuperIndividuo._GrupoMateria[indice, 2];
                _ClaveGrupoMateriaDocente[indice, 0] = indice + 1;
                _ClaveGrupoMateriaDocente[indice, 1] = Grupo;
                _ClaveGrupoMateriaDocente[indice, 2] = Materia;
                _ClaveGrupoMateriaDocente[indice, 3] = GenDocente(Materia);
            }
            return _ClaveGrupoMateriaDocente;
        }
        private int GenDocente(int materia) //metodo que permite asignar de manera aleatoria un docente en relacion a una materia en especifico
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);
            int[] posibles;
            int[] posiciones;
            LinkedList<int> _Docentes = new LinkedList<int>();
            LinkedList<int> _Posiciones = new LinkedList<int>();
            for (int indice = 0; indice < GSuperIndividuo._MateriaDocenteCantidad.GetLength(0); indice++)
            {
                if (GSuperIndividuo._MateriaDocenteCantidad[indice, 1] == materia && GSuperIndividuo._MateriaDocenteCantidad[indice, 2] != 0)
                {
                    _Docentes.AddLast(GSuperIndividuo._MateriaDocenteCantidad[indice, 2]);
                    _Posiciones.AddLast(indice);
                }
            }
            posibles = new int[_Docentes.Count];
            posiciones = new int[_Posiciones.Count];
            _Docentes.CopyTo(posibles, 0);
            _Posiciones.CopyTo(posiciones, 0);
            int pos = aleatorio.Next(0, posibles.Length);
            int ClvDocente = posibles[pos];
            int _posicion = posiciones[pos];
            GSuperIndividuo._MateriaDocenteCantidad[_posicion, 3] = GSuperIndividuo._MateriaDocenteCantidad[_posicion, 3] - 1;
            return ClvDocente;
        }
        private int[,] llenado_Clave_Aula_Bloque(int[,] _MateriaDocente)//metodo que llena la matriz Clave, Aula Bloque
        {
            int[,] _IndividuoTemporal = new int[GSuperIndividuo._tamIndividuo, 3];
            int vueltas = 0, Grupo, Materia, SuperBloque, Docente;
            for (int Registros = 0; Registros < GSuperIndividuo._GrupoMateriaSuperBloque.GetLength(0); Registros++)
            {
                Grupo = GSuperIndividuo._GrupoMateriaSuperBloque[Registros, 1];
                Materia = GSuperIndividuo._GrupoMateriaSuperBloque[Registros, 2];
                Docente = _BuscaDocente(Grupo, Materia, _MateriaDocente);
                SuperBloque = GSuperIndividuo._GrupoMateriaSuperBloque[Registros, 3];

                for (int x = 0; x < SuperBloque; x++)
                {
                    _IndividuoTemporal[vueltas, 0] = Docente;
                    _IndividuoTemporal[vueltas, 1] = -1;
                    _IndividuoTemporal[vueltas, 2] = -1;
                    vueltas++;
                }
            }

            return _IndividuoTemporal;
        }
        private int _BuscaDocente(int Grupo, int Materia, int[,] _MateriaDocente)//Metodo complementario de llenado_Clave_Aula_Bloque
        {
            int identificador = 0;
            for (int indice = 0; indice < _MateriaDocente.GetLength(0); indice++)
            {
                if (_MateriaDocente[indice, 1] == Grupo && _MateriaDocente[indice, 2] == Materia)
                {
                    identificador = _MateriaDocente[indice, 0];
                    break;
                }
            }
            return identificador;
        }
        private string[,] _AcomodaHoras(string[,] matriz)
        {
            int aux = 0;             
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (Int32.Parse(matriz[i, 2]) == _Turno + 1)
                    aux++;
            }
            string[,] horas = new string[aux, 2];
            int indice = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (Int32.Parse(matriz[i, 2]) == _Turno + 1)
                {
                    horas[indice, 0] = matriz[i, 0];
                    horas[indice, 1] = matriz[i, 1];
                    indice++;
                }
            }
            return horas;
        }
        public void imprimirSuperIndividuo()
        {
            int tam = _ClaveGrupoMateriaDocente.GetLength(0);
            for (int x = 0; x < tam; x++)
            {
                Console.WriteLine(_ClaveGrupoMateriaDocente[x, 0] + " " + _ClaveGrupoMateriaDocente[x, 1] + " " + _ClaveGrupoMateriaDocente[x, 2] + " " + _ClaveGrupoMateriaDocente[x, 3]);
            }
        }
        //public void imprimirSuperIndividuo1()
        //{
        //    int[,] jaja = new int[1,2];
        //    for (int x = 0; x < GSuperIndividuo._tamIndividuo; x++)
        //    {
        //        Console.WriteLine(_ClaveAulaBloqueVacio[x, 0] + " " + _ClaveAulaBloqueVacio[x, 1] + " " + _ClaveAulaBloqueVacio[x, 2]);
        //    }
        //}
    }
}
