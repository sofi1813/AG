using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ags
{
    class ClsAlgoritmo
    {
        private ClsSuperIndividuo _SuperIndv;
        private ClsPoblacion _PoblacionPadres;
        private ClsPoblacion _PoblacionHijos;
        private ClsPoblacion _NuevaGeneracion;
        private ClsIndividuo ElMejor;
        private double porcentaje_reparacion = 0.15;
        private double porcentaje_turbulencia = 0.10;
        private int posicion;
        private static double porcentaje_mutacion = 0.03;
        private static int max_iteraciones;


        public ClsAlgoritmo(ClsSuperIndividuo superIndv, int popSize, int iteraciones, double mutacion, double reparacion, double turbulencia)
        {
            _SuperIndv = superIndv;
            max_iteraciones = iteraciones;
            porcentaje_reparacion = reparacion;
            porcentaje_turbulencia = turbulencia;
            porcentaje_mutacion = mutacion;
            _PoblacionPadres = new ClsPoblacion(popSize);
            _PoblacionPadres.generar_poblacion(_SuperIndv);
            _PoblacionPadres._Evalua_Poblacion();
            Console.WriteLine(_PoblacionPadres.toString());
            _PoblacionHijos = new ClsPoblacion(popSize);
            _NuevaGeneracion = new ClsPoblacion(popSize);
            
        }

        #region

        public ClsPoblacion NuevaGeneracion
        {
            get => _NuevaGeneracion;
        }

        public ClsPoblacion Poblacion
        {
            get => _PoblacionPadres;
            set => _PoblacionPadres = value;
        }

        #endregion

        public void Run(FormProgreso frm)
        {
            try
            {
                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                var seed = int.Parse(justNumbers.Substring(0, 4));
                Random aleatorio = new Random(seed);


                double fitness_inicio = _PoblacionPadres.TheBestFitness;
                int repetido = 1;
                int limite_repeticion = 20;
                int estado = 0;
                for (int i = 0; i < max_iteraciones; i++)
                {
                    #region opcion 1
                    //if (repetido % limite_repeticion == 0)
                    //{
                    //    //volado
                    //    estado = aleatorio.Next(0,4);
                    //    if (estado == 0)
                    //    {
                    //        _ManipularPorcentajes(0.07, 0.9, 0.05);//Reparacion
                    //       //Console.WriteLine(" CAMBIO  -   REPARACION");//
                    //        repetido = 1;
                    //        estado = -1;
                    //    }
                    //    else if (estado == 1)
                    //    {
                    //        _ManipularPorcentajes(0.95, 0.05, 0.05);//Mutacion
                    //        //Console.WriteLine(" CAMBIO  -  MUTACION");
                    //        repetido = 1;
                    //        estado = -1;
                    //    }
                    //    else if (estado == 2)
                    //    {
                    //        _ManipularPorcentajes(0.03, 0.05, 0.95);//Turbulencia
                    //        //Console.WriteLine(" CAMBIO  -  TURBULENCIA");
                    //        repetido = 1;
                    //        estado = -1;
                    //    }
                    //    else if (estado == 3)
                    //    {
                    //        _ManipularPorcentajes(0.03, 0.05, 0.03);//ESTADO NORMAL
                    //         //Console.WriteLine("CAMBIO  -  NORMAL ");
                    //        repetido = 1;
                    //        estado = -1;
                    //    }
                    //    else
                    //    {
                    //        _ManipularPorcentajes(0.03, 0.05, 0.03);//ESTADO NORMAL
                    //    }
                    //}
                    #endregion

                    #region opcion 2
                    if (repetido % limite_repeticion == 0 && estado == 0)
                    {
                        _ManipularPorcentajes(0.07, 0.9, 0.05);//Reparacion
                                                               //Console.WriteLine(" CAMBIO  -   REPARACION");//
                        repetido = 1;
                        estado = 1;
                    }
                    else if (repetido % limite_repeticion == 0 && estado == 1)
                    {
                        _ManipularPorcentajes(0.95, 0.05, 0.05);//Mutacion
                                                                //Console.WriteLine(" CAMBIO  -  MUTACION");
                        repetido = 1;
                        estado = 2;
                    }
                    else if (repetido % limite_repeticion == 0 && estado == 2)
                    {
                        _ManipularPorcentajes(0.03, 0.05, 0.95);//Turbulencia
                                                                //Console.WriteLine(" CAMBIO  -  TURBULENCIA");
                        repetido = 1;
                        estado = 3;
                    }
                    else if (repetido % limite_repeticion == 0 && estado == 3)
                    {
                        _ManipularPorcentajes(0.03, 0.05, 0.03);//ESTADO NORMAL
                                                                //Console.WriteLine("CAMBIO  -  NORMAL ");
                        repetido = 1;
                        estado = 0;
                    }
                    else
                    {
                        _ManipularPorcentajes(0.03, 0.05, 0.03);//ESTADO NORMAL
                                                                //Console.WriteLine(" NORMAL ");
                    }
                    #endregion

                    _ObtenerNuevaGeneracion();
                    if (fitness_inicio == _PoblacionPadres.TheBestFitness)
                    {
                        repetido++;
                    }
                    else
                    {
                        fitness_inicio = _PoblacionPadres.TheBestFitness;
                        //Console.WriteLine("Tamaño meseta: " + repetido);
                        repetido = 1;
                    }
                    try
                    {
                        frm.Invoke(new MethodInvoker(delegate () { frm.Actualizar(i, max_iteraciones, _PoblacionPadres.TheBestFitness, i + ".- " + _PoblacionPadres.toString()); }));
                    }
                    catch (Exception err) { }
                    Console.WriteLine(i + " " + _PoblacionPadres.toString());

                }
                ClsTrataJSON.Individuo_JSON(_SuperIndv.ClaveGrupoMateriaDocente, _PoblacionPadres.Poblacion[_PoblacionPadres.TheBest]._Horarios, GSuperIndividuo._Turno, frm.ProcesoTerminado());
            }
            catch (Exception err) { }
        }

        private void _ManipularPorcentajes(double mutacion, double reparacion, double turbulencia)
        {
            porcentaje_mutacion = mutacion;
            porcentaje_reparacion = reparacion;
            porcentaje_turbulencia = turbulencia;
        }

        public void _ObtenerNuevaGeneracion()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            int indice = 0;
            ClsIndividuo[] _Descendientes = new ClsIndividuo[_PoblacionPadres.getTam];
            _PoblacionHijos = new ClsPoblacion(_PoblacionPadres.getTam);
            for (int i = 0; i < _PoblacionPadres.getTam / 2; i++)
            {
                int padreA = _Seleccion_por_Torneo();
                int padreB = _Seleccion_por_Torneo();
                posicion = 0;
                ClsIndividuo[] hijos = _Cruzar2(_PoblacionPadres.Poblacion[padreA], _PoblacionPadres.Poblacion[padreB]);

                for (int x = 0; x < hijos.Length; x++, indice++)
                {
                    _Descendientes[indice] = hijos[x];
                }
            }
            _PoblacionHijos.Poblacion = _Descendientes;

            if(aleatorio.NextDouble() < porcentaje_mutacion)
                _MutacionAula();
            else
                _MutacionBloque();

            _PoblacionHijos._Evalua_Poblacion();

            _NewGeneration();
        }

        private void _MutacionAula()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            int vueltas = (int)Math.Round(_PoblacionHijos.getTam * porcentaje_mutacion);
            int posicion_poblacion = -1;
            int posicion_horario = -1;
            int posicion_aula_posible = -1;
            int clave = -1;
            int materia = -1;
            int bloque = -1;
            int aula_actual = -1;
            int aula_nueva = -1;
            int[,] horario;
            int[][,] horarioA;
            double[] fitness = _PoblacionHijos.Fitness;

            for (int i = 0; i < vueltas; i++)
            {
                posicion_poblacion = aleatorio.Next(0, _PoblacionHijos.Poblacion.Length);
                posicion_horario = aleatorio.Next(0, _PoblacionHijos.Poblacion[posicion_poblacion].TamIndiv);

                horario = _PoblacionHijos.Poblacion[posicion_poblacion].Horario;
                horarioA = _PoblacionHijos.Poblacion[posicion_poblacion].HorariosAulas;

                clave = _PoblacionHijos.Poblacion[posicion_poblacion].Horario[posicion_horario, 0];
                materia = _SuperIndv.ClaveGrupoMateriaDocente[clave - 1, 2];

                posicion_aula_posible = aleatorio.Next(0, _SuperIndv.MateriasAulasPosibles[_BuscarEnArray(_SuperIndv.Materias, materia)].Length);

                aula_actual = _PoblacionHijos.Poblacion[posicion_poblacion].Horario[posicion_horario, 1];
                aula_nueva = _SuperIndv.MateriasAulasPosibles[_BuscarEnArray(_SuperIndv.Materias, materia)][posicion_aula_posible];
                bloque = _PoblacionHijos.Poblacion[posicion_poblacion].Horario[posicion_horario, 2];

                horario[posicion_horario, 1] = aula_nueva;

                _MarcarBloqueHorario(horarioA[_BuscarEnArray(_SuperIndv.Aulas, aula_nueva)], bloque);
                _DesmarcarBloqueHorario(horarioA[_BuscarEnArray(_SuperIndv.Aulas, aula_actual)], bloque);

                _PoblacionHijos.Poblacion[posicion_poblacion].Horario = horario;
                _PoblacionHijos.Poblacion[posicion_poblacion].HorariosAulas = horarioA;

                //fitness[posicion_poblacion] = ClsEvaluacion.fitness(_PoblacionHijos.Poblacion[posicion_poblacion]);
            }

            //_PoblacionHijos.Fitness = fitness;
        }

        private void _MutacionBloque()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            int[] dia;
            int[] conjunto;
            int[] permitidos;
            int[] randoms;
            int[,] horarios;
            int[][,] horariosG;
            int[][,] horariosA;
            int[][,] horariosD;
            double[] fitness = _PoblacionHijos.Fitness;
            int vueltas = (int)Math.Round(_PoblacionHijos.getTam * porcentaje_mutacion);
            int posicion_poblacion = -1;
            int posicion_horario = -1;
            int clave = -1;
            int grupo = -1;
            int docente = -1;
            int aula = -1;
            int bloque = -1;
            int bloque_random = -1;

            for (int i = 0; i < vueltas; i++)
            {
                posicion_poblacion = aleatorio.Next(0, _PoblacionHijos.getTam);
                posicion_horario = aleatorio.Next(0, _PoblacionHijos.Poblacion[posicion_poblacion].TamIndiv);

                horarios = _PoblacionHijos.Poblacion[posicion_poblacion].Horario;
                horariosA = _PoblacionHijos.Poblacion[posicion_poblacion].HorariosAulas;
                horariosG = _PoblacionHijos.Poblacion[posicion_poblacion].HorariosGrupos;
                horariosD = _PoblacionHijos.Poblacion[posicion_poblacion].HorariosDocentes;

                clave = horarios[posicion_horario, 0];
                grupo = _SuperIndv.ClaveGrupoMateriaDocente[clave - 1, 1];
                docente = _SuperIndv.ClaveGrupoMateriaDocente[clave - 1, 3];
                aula = horarios[posicion_horario, 1];
                bloque = horarios[posicion_horario, 2];

                dia = _SuperIndv.BloquesDias[aleatorio.Next(0, _SuperIndv.BloquesDias.Length)];

                int pos_aula = _BuscaPosicionArray(_SuperIndv.Aulas, aula);
                int pos_grupo = _BuscaPosicionArray(_SuperIndv.Grupos, grupo);
                int pos_docente = _BuscaPosicionArray(_SuperIndv.Docentes, docente);
                conjunto = _TeoriaConjuntos(_ObtenerBloquesDisponibles(horariosA[pos_aula]), _ObtenerBloquesDisponibles(horariosG[pos_grupo]), _ObtenerBloquesDisponibles(horariosD[pos_docente]));

                _DesmarcarBloqueHorario(horariosA[pos_aula], bloque);
                _DesmarcarBloqueHorario(horariosG[pos_grupo], bloque);
                _DesmarcarBloqueHorario(horariosD[pos_docente], bloque);

                if (conjunto != null)
                {
                    permitidos = _BloquesPermitidos(_DiasOcupadosMateria(horarios, clave), conjunto);

                    if (permitidos != null)
                    {
                        bloque_random = aleatorio.Next(0, permitidos.Length);
                        horarios[posicion_horario, 2] = permitidos[bloque_random];
                        _MarcarBloqueHorario(horariosA[pos_aula], permitidos[bloque_random]);
                        _MarcarBloqueHorario(horariosG[pos_grupo], permitidos[bloque_random]);
                        _MarcarBloqueHorario(horariosD[pos_docente], permitidos[bloque_random]);
                    }
                    else
                    {
                        randoms = _OpcionesBloqueRandom(horariosA[pos_aula], horariosG[pos_grupo], horariosD[pos_docente]);
                        bloque_random = aleatorio.Next(0, randoms.Length);
                        horarios[posicion_horario, 2] = randoms[bloque_random];
                        _MarcarBloqueHorario(horariosA[pos_aula], randoms[bloque_random]);
                        _MarcarBloqueHorario(horariosG[pos_grupo], randoms[bloque_random]);
                        _MarcarBloqueHorario(horariosD[pos_docente], randoms[bloque_random]);
                    }
                }
                else
                {
                    randoms = _OpcionesBloqueRandom(horariosA[pos_aula], horariosG[pos_grupo], horariosD[pos_docente]);
                    bloque_random = aleatorio.Next(0, randoms.Length);
                    horarios[posicion_horario, 2] = randoms[bloque_random];
                    _MarcarBloqueHorario(horariosA[pos_aula], randoms[bloque_random]);
                    _MarcarBloqueHorario(horariosG[pos_grupo], randoms[bloque_random]);
                    _MarcarBloqueHorario(horariosD[pos_docente], randoms[bloque_random]);
                }

                _PoblacionHijos.Poblacion[posicion_poblacion].Horario = horarios;
                _PoblacionHijos.Poblacion[posicion_poblacion].HorariosAulas = horariosA;
                _PoblacionHijos.Poblacion[posicion_poblacion].HorariosGrupos = horariosG;
                _PoblacionHijos.Poblacion[posicion_poblacion].HorariosDocentes = horariosD;

                //fitness[posicion_poblacion] = ClsEvaluacion.fitness(_PoblacionHijos.Poblacion[posicion_poblacion]);
            }

            //_PoblacionHijos.Fitness = fitness;
        }

        private int[] _OpcionesBloqueRandom(int[,] horarioA, int[,] horarioG, int[,] horarioD)//AÑADIR
        {
            LinkedList<int> posibles = new LinkedList<int>();
            LinkedList<int> bloques_grupo = _ExtraerColumna(horarioG, 1);
            LinkedList<int> bloques_docente = _ExtraerColumna(horarioD, 1);
            LinkedList<int> bloques_aula = _ExtraerColumna(horarioA, 1);
            int valor;

            for (int i = 0; i < bloques_grupo.Count; i++)
            {
                valor = bloques_grupo.ElementAt(i);
                if (bloques_docente.Contains(valor) && bloques_aula.Contains(valor))
                {
                    posibles.AddLast(valor);
                }
            }

            return posibles.ToArray();
        }

        private LinkedList<int> _ExtraerColumna(int[,] matriz, int posicion_columna)//AÑADIR
        {
            LinkedList<int> columna = new LinkedList<int>();

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                columna.AddLast(matriz[i, posicion_columna]);
            }

            return columna;
        }

        private int _Seleccion_por_Torneo()
        {
            int tam_muestra = 7;
            int posible;
            int[] posiciones_posibles = new int[tam_muestra];
            double[] fitness = new double[tam_muestra];

            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            for (int i = 0; i < posiciones_posibles.Length; i++)
            {
                posible = aleatorio.Next(0, _PoblacionPadres.getTam);
                //competidores[i] = _PoblacionPadres.Poblacion[posible];
                fitness[i] = _PoblacionPadres.Fitness[posible];
                posiciones_posibles[i] = posible;
            }

            int mejor = _TheBest(fitness);

            int Ganador = posiciones_posibles[mejor];

            return Ganador;
        }

        private int _TheBest(double[] Numeros)
        {
            int p = 0;

            for (int i = 1; i < Numeros.Length; i++)
                if (Numeros[i] < Numeros[p])
                    p = i;

            return p;
        }

        private ClsIndividuo[] _Cruzar(ClsIndividuo indvPadre, ClsIndividuo indvMadre)
        {
            int clave;
            int tam = indvPadre.TamIndiv;
            int[,] hijoA = new int[tam, 3];
            int[,] hijoB = new int[tam, 3];
            ClsIndividuo[] hijos = new ClsIndividuo[2];
            int[,] GenesPadre;
            int[,] GenesMadre;
            int[] claves_seleccionadas = _ObtenerClavesCruza();

            for (int i = 0; i < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                clave = _SuperIndv.ClaveGrupoMateriaDocente[i, 0];
                GenesPadre = _ExtraerGenes(indvPadre.Horario, clave);
                GenesMadre = _ExtraerGenes(indvMadre.Horario, clave);

                if (_BuscarEnArray(claves_seleccionadas, clave) != -1)
                {
                    _InsertarDirecto(GenesPadre, GenesMadre, hijoA, hijoB);
                }
                else
                {
                    _InsertarCruzado(GenesPadre, GenesMadre, hijoA, hijoB);
                }
            }

            ClsIndividuo _HijoA = new ClsIndividuo(_SuperIndv, hijoA);
            ClsIndividuo _HijoB = new ClsIndividuo(_SuperIndv, hijoB);

            hijos[0] = _HijoA;
            hijos[1] = _HijoB;

            return hijos;
        }

        private ClsIndividuo[] _Cruzar2(ClsIndividuo indvPadre, ClsIndividuo indvMadre)
        {
            int clave;
            int tam = indvPadre.TamIndiv;
            int[,] hijoA = new int[tam, 3];
            int[,] hijoB = new int[tam, 3];
            ClsIndividuo[] hijos = new ClsIndividuo[2];
            int[,] GenesPadre;
            int[,] GenesMadre;
            int[] claves_seleccionadas = _ObtenerClavesCruza2();

            for (int i = 0; i < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                clave = _SuperIndv.ClaveGrupoMateriaDocente[i, 0];
                GenesPadre = _ExtraerGenes(indvPadre.Horario, clave);
                GenesMadre = _ExtraerGenes(indvMadre.Horario, clave);

                if (_BuscarEnArray(claves_seleccionadas, clave) != -1)
                {
                    _InsertarCruzado(GenesPadre, GenesMadre, hijoA, hijoB);
                }
                else
                {
                    _InsertarDirecto(GenesPadre, GenesMadre, hijoA, hijoB);
                }
            }

            ClsIndividuo _HijoA = new ClsIndividuo(_SuperIndv, hijoA);
            ClsIndividuo _HijoB = new ClsIndividuo(_SuperIndv, hijoB);

            hijos[0] = _HijoA;
            hijos[1] = _HijoB;

            return hijos;
        }

        private ClsIndividuo[] _Cruzar3(ClsIndividuo indvPadre, ClsIndividuo indvMadre)
        {
            int clave;
            int tam = indvPadre.TamIndiv;
            int[,] hijoA = new int[tam, 3];
            int[,] hijoB = new int[tam, 3];
            ClsIndividuo[] hijos = new ClsIndividuo[2];
            int[,] GenesPadre;
            int[,] GenesMadre;
            int[] claves_seleccionadas = _ObtenerClavesCruza3();

            for (int i = 0; i < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                clave = _SuperIndv.ClaveGrupoMateriaDocente[i, 0];
                GenesPadre = _ExtraerGenes(indvPadre.Horario, clave);
                GenesMadre = _ExtraerGenes(indvMadre.Horario, clave);

                if (_BuscarEnArray(claves_seleccionadas, clave) != -1)
                {
                    _InsertarCruzado(GenesPadre, GenesMadre, hijoA, hijoB);
                }
                else
                {
                    _InsertarDirecto(GenesPadre, GenesMadre, hijoA, hijoB);
                }
            }

            ClsIndividuo _HijoA = new ClsIndividuo(_SuperIndv, hijoA);
            ClsIndividuo _HijoB = new ClsIndividuo(_SuperIndv, hijoB);

            hijos[0] = _HijoA;
            hijos[1] = _HijoB;

            return hijos;
        }
        
        #region Reparar Choques

        public ClsIndividuo _ReparaChoqueGrupoAula(ClsIndividuo indv)
        {
            ClsIndividuo indv_reparado;
            //Hacer sorteos de numeros
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            //Utilidades de apoyo/modificacion
            int[,] superindv = indv.SuperIndv.ClaveGrupoMateriaDocente;
            int[,] horarios = indv.Horario;
            int[][,] horarios_grupos = indv.HorariosGrupos;
            int[][,] horarios_aulas = indv.HorariosAulas;
            int[][,] horarios_docentes = indv.HorariosDocentes;

            LinkedList<int> bloques_chocando;
            LinkedList<int> claves_grupo_superindv;
            LinkedList<int> posiciones_horarios = new LinkedList<int>();
            LinkedList<int> aulas_posibles;
            int grupo = -1;

            //Itera sobre los horarios de los grupos
            for (int i = 0; i < horarios_grupos.Length; i++)
            {
                //Obtiene la clave del grupo
                grupo = indv.SuperIndv.Grupos[i];

                //Obtiene claves de bloques con choque
                bloques_chocando = _ObtenerBloquesChocando(horarios_grupos[i]);

                //Si no hay bloques con choque entonces procede con la siguiente iteracion
                if (bloques_chocando.Count == 0)
                    continue;

                //Obtiene las claves del super individuo donde aparece el grupo
                //Ejemplo el primer grupo de la clave 1 a la clave 20
                //claves_grupo_superindv = _Obtener_DatosSuperIndv(1, 0, grupo);
                claves_grupo_superindv = _Obtener_DatosMatriz(1, 0, grupo, _SuperIndv.ClaveGrupoMateriaDocente);

                //Itera sobre los bloques con choque
                for (int j = 0; j < bloques_chocando.Count; j++)
                {
                    //Recorre el horario para saber las posiciones en donde ese bloque aparece
                    //Solo se buscan en las filas con las claves pertenecientes a un cierto grupo, ejemplo anterior
                    for (int k = 0; k < horarios.GetLength(0); k++)
                    {
                        if (claves_grupo_superindv.Contains(horarios[k, 0]) && horarios[k, 2] == bloques_chocando.ElementAt(j))
                        {
                            posiciones_horarios.AddLast(k);
                        }
                    }

                    //Se remueve el primer elemento por que es el que no se asigno de manera aleatoria
                    //Si la cuenta es cero probablemente se corrigio en un superbloque de tamaño 2
                    if (posiciones_horarios.Count != 0)
                        posiciones_horarios.RemoveFirst();
                    else
                        continue;

                    //Se itera sobre las posiciones que se obtuvieron 
                    //Ejemplo posiciones 0, 5, 23, 34
                    for (int k = 0; k < posiciones_horarios.Count; k++)
                    {
                        //Obtiene los bloques disponibles(bloques en 0) del horario
                        int[] bloques_temporales = _ObtenerBloquesDisponibles(horarios_grupos[i]);
                        if (bloques_temporales == null)
                        {
                            //************ V E R I F I C A R *****************
                            //cambio de dia
                            //NO TIENE CASO SI EL HORARIO DEL GRUPO ESTA LLENO
                            //p_dias.AddLast(posiciones_horarios.ElementAt(k));
                            continue;
                        }
                        //Se crea una lista con los bloques disponibles del grupo
                        LinkedList<int> bloques_disponibles_grupo = new LinkedList<int>(bloques_temporales);

                        //Posicion del individuo que se indica
                        int posicion_horario = posiciones_horarios.ElementAt(k);

                        //clave de materia que indica dicha clave
                        int materia = superindv[horarios[posicion_horario, 0] - 1, 2];

                        //Aula que se encuentra asignada
                        int aula_actual = horarios[posicion_horario, 1];

                        //Bloque que se encuentra asignado
                        int bloque_actual = horarios[posicion_horario, 2];

                        //Lista de aulas posibles seguna la clave de la materia
                        aulas_posibles = new LinkedList<int>(indv.SuperIndv.MateriasAulasPosibles[_BuscaPosicionArray(indv.SuperIndv.Materias, materia)]);

                        //Todas las demas son posibles menos la que queremos cambiar 
                        aulas_posibles.Remove(aula_actual);

                        //Si no hay mas opciones para esa materia se trata de cambiar el dia en el que se imparte
                        if (aulas_posibles.Count == 0)
                        {
                            //**************** CAMBIO DE DIA *************************
                            //Busqueda de posiciones para identificar estructuras en arreglos/matrices escalonad@s
                            int posicion_aula = _BuscaPosicionArray(indv.SuperIndv.Aulas, aula_actual);
                            int posicion_docente = _BuscaPosicionArray(indv.SuperIndv.Docentes, superindv[horarios[posicion_horario, 0] - 1, 3]);
                            //Llama metodo que cambia de dia
                            _ReparaChoqueGrupoDia(horarios_grupos[i], horarios_aulas[posicion_aula], horarios_docentes[posicion_docente], horarios, posicion_horario);
                            //p_dias.AddLast(posicion_horario);
                            continue;
                        }
                        else//En caso de haber mas aulas, se realiza el cambio de esta
                        {

                            int pos_docente = _BuscaPosicionArray(indv.SuperIndv.Docentes, superindv[horarios[posicion_horario, 0] - 1, 3]);
                            //Bloques disponibles de aula y de docente
                            int[] disponibles_aula;
                            int[] disponibles_docente = _ObtenerBloquesDisponibles(horarios_docentes[pos_docente]);
                            LinkedList<int[]> conjuntos = new LinkedList<int[]>();
                            int[] posible;
                            int[] permitidos;

                            LinkedList<int[]> bloques_comunes_aula = new LinkedList<int[]>();//CHECAR SI AYUDA
                            LinkedList<int> aulas_finales = new LinkedList<int>();
                            //int[] bloques_comunes;
                            for (int l = 0; l < aulas_posibles.Count; l++)
                            {
                                disponibles_aula = _ObtenerBloquesDisponibles(horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, aulas_posibles.ElementAt(l))]);
                                posible = _TeoriaConjuntos(bloques_disponibles_grupo.ToArray() , disponibles_aula, disponibles_docente);

                                //bloques_comunes = _BloquesDisponiblesAula(bloques_disponibles_grupo, horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, aulas_posibles.ElementAt(l))]);
                                if (posible != null)
                                {
                                    permitidos = _BloquesPermitidos(_DiasOcupadosMateria(horarios, horarios[posicion_horario, 0]), posible);
                                    if (permitidos != null)
                                    {
                                        conjuntos.AddLast(permitidos);
                                        ///bloques_comunes_aula.AddLast(bloques_comunes);
                                        aulas_finales.AddLast(aulas_posibles.ElementAt(l));
                                    }
                                }
                            }

                            if (conjuntos.Count == 0)
                            {
                                //**************** CAMBIO DE DIA *************************
                                //int posicion_horario = posiciones_horarios.ElementAt(k);
                                int posicion_aula = _BuscaPosicionArray(indv.SuperIndv.Aulas, horarios[posicion_horario, 1]);
                                int posicion_docente = _BuscaPosicionArray(indv.SuperIndv.Docentes, superindv[horarios[posicion_horario, 0] - 1, 3]);
                                _ReparaChoqueGrupoDia(horarios_grupos[i], horarios_aulas[posicion_aula], horarios_docentes[posicion_docente], horarios, posicion_horario);
                                //p_dias.AddLast(posicion_horario);
                                continue;
                            }
                            else
                            {
                                //p_aulas.AddLast(posicion_horario);
                                int clv_materia = _Obtener_DatosMatriz(0, 2, horarios[posicion_horario, 0], _SuperIndv.ClaveGrupoMateriaDocente).First();
                                int[] patron_superbloque = _SuperIndv.MateriasSuperbloques[_BuscaPosicionArray(_SuperIndv.Materias, materia)];
                                LinkedList<int> bloques = _Obtener_DatosMatriz(0, 2, horarios[posicion_horario, 0], horarios);
                                LinkedList<int> posiciones_bloques = new LinkedList<int>();
                                int[][] bloques_superbloque = new int[patron_superbloque.Length][];
                                int[][] bloques_posiciones = new int[patron_superbloque.Length][];
                                int pos_superbloque = -1;

                                for (int l = 0; l < horarios.GetLength(0); l++)
                                {
                                    if (horarios[posicion_horario, 0] == horarios[l, 0])
                                        posiciones_bloques.AddLast(l);
                                }

                                for (int l = 0; l < bloques_superbloque.Length; l++)
                                {
                                    bloques_superbloque[l] = new int[patron_superbloque[l]];
                                    bloques_posiciones[l] = new int[patron_superbloque[l]];

                                    for (int m = 0; m < bloques_superbloque[l].Length; m++)
                                    {
                                        bloques_superbloque[l][m] = bloques.ElementAt(0);
                                        bloques.RemoveFirst();
                                        bloques_posiciones[l][m] = posiciones_bloques.ElementAt(0);
                                        posiciones_bloques.RemoveFirst();
                                    }
                                }

                                for (int l = 0; l < bloques_superbloque.Length; l++)
                                {
                                    if (_BuscaPosicionArray(bloques_superbloque[l], bloque_actual) != -1)
                                    {
                                        pos_superbloque = l;
                                    }
                                }

                                int[] posiciones_finales = bloques_posiciones[pos_superbloque];

                                if (bloques_superbloque[pos_superbloque].Length > 1)
                                {
                                    int valor_random = aleatorio.Next(0, conjuntos.Count);
                                    int[][] adyacentes = _Buscar_BloquesAdyacentes(conjuntos.ElementAt(valor_random), bloques_superbloque[pos_superbloque].Length);
                                    int salon = aulas_finales.ElementAt(valor_random);
                                    if(adyacentes != null)
                                    {
                                        int[] combinacion = adyacentes[aleatorio.Next(0, adyacentes.Length)];
                                        for(int l = 0; l < combinacion.Length; l++)
                                        {
                                            int posicion_docente = _BuscaPosicionArray(indv.SuperIndv.Docentes, superindv[horarios[bloques_posiciones[pos_superbloque][l], 0] - 1, 3]);
                                            int pos_horario = posiciones_finales[l];

                                            _MarcarBloqueHorario(horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, salon)], combinacion[l]);
                                            _MarcarBloqueHorario(horarios_grupos[_BuscaPosicionArray(indv.SuperIndv.Grupos, grupo)], combinacion[l]);
                                            _MarcarBloqueHorario(horarios_docentes[posicion_docente], combinacion[l]);
                                            _DesmarcarBloqueHorario(horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, horarios[posiciones_finales[l], 1])], horarios[pos_horario, 2]);
                                            _DesmarcarBloqueHorario(horarios_grupos[_BuscaPosicionArray(indv.SuperIndv.Grupos, grupo)], horarios[pos_horario, 2]);
                                            _DesmarcarBloqueHorario(horarios_docentes[posicion_docente], horarios[pos_horario, 2]);

                                            horarios[pos_horario, 1] = salon;
                                            horarios[pos_horario, 2] = combinacion[l];
                                            //p_aulas.AddLast(pos_horario);
                                        }
                                    }
                                }
                                else
                                {
                                    int valor_random = aleatorio.Next(0, conjuntos.Count);
                                    int[] solos = _Buscar_BloquesSolos(conjuntos.ElementAt(valor_random));
                                    int salon = aulas_finales.ElementAt(valor_random);
                                    if (solos != null)
                                    {
                                        int bloque_nuevo = solos[aleatorio.Next(0, solos.Length)];
                                        int posicion_docente = _BuscaPosicionArray(indv.SuperIndv.Docentes, superindv[horarios[bloques_posiciones[pos_superbloque][0], 0] - 1, 3]);

                                        _MarcarBloqueHorario(horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, salon)], bloque_nuevo);
                                        _MarcarBloqueHorario(horarios_grupos[_BuscaPosicionArray(indv.SuperIndv.Grupos, grupo)], bloque_nuevo);
                                        _MarcarBloqueHorario(horarios_docentes[posicion_docente], bloque_nuevo);

                                        _DesmarcarBloqueHorario(horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, horarios[posiciones_finales[0], 1])], horarios[posiciones_finales[0], 2]);
                                        _DesmarcarBloqueHorario(horarios_grupos[_BuscaPosicionArray(indv.SuperIndv.Grupos, grupo)], horarios[posiciones_finales[0], 2]);
                                        _DesmarcarBloqueHorario(horarios_docentes[posicion_docente], horarios[posiciones_finales[0], 2]);

                                        horarios[posiciones_finales[0], 1] = salon;
                                        horarios[posiciones_finales[0], 2] = bloque_nuevo;
                                        //p_aulas.AddLast(posiciones_finales[0]);
                                    }
                                }

                                //int posicion_horario = posiciones_horarios.ElementAt(k);
                                //int numero = aleatorio.Next(0, bloques_comunes_aula.Count);
                                ////Quitar los que salen nulos
                                //int[] bloques_disponibles = bloques_comunes_aula.ElementAt(numero);
                                //int clv_aula = aulas_finales.ElementAt(numero);
                                //int bloque = bloques_disponibles[aleatorio.Next(0, bloques_disponibles.Length)];

                                //_MarcarBloqueHorario(horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, clv_aula)], bloque);
                                //_DesmarcarBloqueHorario(horarios_aulas[_BuscaPosicionArray(indv.SuperIndv.Aulas, aula_actual)], bloque_actual);
                                //_MarcarBloqueHorario(horarios_grupos[_BuscaPosicionArray(indv.SuperIndv.Grupos, grupo)], bloque);
                                //_DesmarcarBloqueHorario(horarios_grupos[_BuscaPosicionArray(indv.SuperIndv.Grupos, grupo)], bloque_actual);

                                //horarios[posicion_horario, 1] = clv_aula;
                                //horarios[posicion_horario, 2] = bloque;
                            }
                        }
                    }
                    posiciones_horarios.Clear();
                }
                bloques_chocando.Clear();
                claves_grupo_superindv.Clear();
            }

            indv_reparado = new ClsIndividuo(indv.SuperIndv, horarios);
            //indv_reparado.HorariosAulas = horarios_aulas;
            //indv_reparado.HorariosDocentes = horarios_docentes;
            //indv_reparado.HorariosGrupos = horarios_grupos;

            return indv_reparado;
        }

        public void _ReparaChoqueGrupoDia(int[,] horario_grupo, int[,] horario_aula, int[,] horario_docente, int[,] horarios, int posicion_horario)
        {

            //Bloques disponibles de cada "recurso"
            int[] disponibles_grupo = _ObtenerBloquesDisponibles(horario_grupo);
            int[] disponibles_aula = _ObtenerBloquesDisponibles(horario_aula);
            int[] disponibles_docente = _ObtenerBloquesDisponibles(horario_docente);

            //Si alguno no posee bloques disponibles entonces no se puede aplicar los cambios
            if (disponibles_grupo != null && disponibles_aula != null && disponibles_docente != null)
            {
                //Sorteo de numeros
                var guid = Guid.NewGuid();
                var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                var seed = int.Parse(justNumbers.Substring(0, 4));
                Random aleatorio = new Random(seed);
                int clave_horario = horarios[posicion_horario, 0];

                //Se obtiene la clave de la materia
                int materia = _Obtener_DatosMatriz(0, 2, clave_horario, _SuperIndv.ClaveGrupoMateriaDocente).First();

                //Se busca el patron del superbloque de esa materia
                //Ejemplo una materia de 7 horas [ 2, 2, 2, 1 ]
                int[] patron_superbloque = _SuperIndv.MateriasSuperbloques[_BuscaPosicionArray(_SuperIndv.Materias, materia)];

                //Extraer los bloques de una clave
                //Ejemplo clave 1 representa a una materia de 7 horas con los bloques 1, 2, 15, 16, 20, 21 y 30
                LinkedList<int> bloques = _Obtener_DatosMatriz(0, 2, clave_horario, horarios);

                //Almacenara la posicion de donde se encuentran los bloques en la matriz horario
                //Ejemplo posiciones 0, 1, 2, 3, 4, 5, 6
                LinkedList<int> posiciones_bloques = new LinkedList<int>();

                //Almacenara los bloques en orden segun su patron
                //Ejemplo 
                //Patron: 2, 2, 2, 1      Bloques: 1, 2, 15, 16, 20, 21 y 30
                //Resultado: { [1,2]    [15,16]     [20,21]    [30] }
                int[][] bloques_superbloque = new int[patron_superbloque.Length][];

                //De la misma forma pero con las posiciones
                //{ [0,1]    [2,3]     [4,5]    [6] }
                int[][] bloques_posiciones = new int[patron_superbloque.Length][];

                //Almacenara combinaciones de bloques que se encuentren juntos(en este caso maximos de 2)
                int[][] opc_adyacentes;
                //Almacenara bloques que se encuentren solos sin, con sus bloques vecinos ya ocupados
                int[] opc_solos;

                //Bloque que se desea corregir
                int bloque_choque = horarios[posicion_horario, 2];

                //Haciendo uso de la teroria de conjuntos se obtienen los bloques disponibles en los que coinciden los tres horarios
                int[] libres_comun = _TeoriaConjuntos(disponibles_grupo, disponibles_aula, disponibles_docente);

                //Si no coniciden en ninguno no es posible hacer cambios
                if (libres_comun != null)
                {
                    //De los bloques que coniciden en los tres horarios se retorna aquellos que no se encuentren en un dia ya ocupado por la materia
                    int[] permitidos = _BloquesPermitidos(_DiasOcupadosMateria(horarios, horarios[posicion_horario, 0]), libres_comun);

                    //Si solo estaban diponibles los de un dia ya ocupado entonces no se puede proceder
                    if (permitidos != null)
                    {
                        //*************
                        int pos_superbloque = -1;

                        //Ver descripcion de la estructura " posiciones_bloques "
                        for (int i = 0; i < horarios.GetLength(0); i++)
                        {
                            if (horarios[posicion_horario, 0] == horarios[i, 0])
                                posiciones_bloques.AddLast(i);
                        }

                        //Se realiza la distribucion de superbloques como se indico en la descripcion de las estructuras:
                        // bloques y posiciones_bloques
                        for (int i = 0; i < bloques_superbloque.Length; i++)
                        {
                            bloques_superbloque[i] = new int[patron_superbloque[i]];
                            bloques_posiciones[i] = new int[patron_superbloque[i]];
                            for (int j = 0; j < bloques_superbloque[i].Length; j++)
                            {
                                bloques_superbloque[i][j] = bloques.First();
                                bloques.RemoveFirst();
                                bloques_posiciones[i][j] = posiciones_bloques.First();
                                posiciones_bloques.RemoveFirst();
                            }
                        }

                        //Se busca la posicion del superbloque donde se encuentra el bloque que deseamos cambiar
                        for (int i = 0; i < bloques_superbloque.Length; i++)
                        {
                            if (_BuscaPosicionArray(bloques_superbloque[i], bloque_choque) != -1)
                            {
                                pos_superbloque = i;
                                break;
                            }
                        }

                        //Arreglo con las posiciones del superbloque dentro de la matriz horarios
                        int[] posiciones_finales = bloques_posiciones[pos_superbloque];

                        //Si el superbloque es de un tamaño mayor a uno entonces...
                        if (bloques_superbloque[pos_superbloque].Length > 1)
                        {
                            //Se buscan bloques adyacentes
                            opc_adyacentes = _Buscar_BloquesAdyacentes(permitidos, bloques_superbloque[pos_superbloque].Length);
                            
                            //Si no existen combinaciones de cierto tamaño no se pueden asignar y no se realizan cambios
                            if(opc_adyacentes != null)
                            {
                                //En caso contrario se elije una de las combinaciones aleatoriamente y se asigna
                                int[] combinacion = opc_adyacentes[aleatorio.Next(0, opc_adyacentes.Length)];

                                for(int i = 0; i < combinacion.Length; i++)
                                {
                                    _DesmarcarBloqueHorario(horario_docente, horarios[posiciones_finales[i], 2]);
                                    _DesmarcarBloqueHorario(horario_aula, horarios[posiciones_finales[i], 2]);
                                    _DesmarcarBloqueHorario(horario_grupo, horarios[posiciones_finales[i], 2]);
                                    horarios[posiciones_finales[i], 2] = combinacion[i];
                                    _MarcarBloqueHorario(horario_docente, combinacion[i]);
                                    _MarcarBloqueHorario(horario_aula, combinacion[i]);
                                    _MarcarBloqueHorario(horario_grupo, combinacion[i]);
                                    //p_dias.AddLast(posiciones_finales[i]);
                                }
                            }
                        }
                        else
                        {
                            opc_solos = _Buscar_BloquesSolos(permitidos);

                            if (opc_solos != null)
                            {
                                _DesmarcarBloqueHorario(horario_docente, bloque_choque);
                                _DesmarcarBloqueHorario(horario_aula, bloque_choque);
                                _DesmarcarBloqueHorario(horario_grupo, bloque_choque);
                                horarios[posicion_horario, 2] = opc_solos[aleatorio.Next(0, opc_solos.Length)];
                                _MarcarBloqueHorario(horario_docente, horarios[posicion_horario, 2]);
                                _MarcarBloqueHorario(horario_aula, horarios[posicion_horario, 2]);
                                _MarcarBloqueHorario(horario_grupo, horarios[posicion_horario, 2]);
                                //p_dias.AddLast(posicion_horario);
                            }
                        }

                        //_DesmarcarBloqueAula(horario_docente, bloque_choque);
                        //_DesmarcarBloqueAula(horario_aula, bloque_choque);
                        //_DesmarcarBloqueAula(horario_grupo, bloque_choque);
                        //horarios[posicion_horario, 2] = permitidos[aleatorio.Next(0, permitidos.Length)];
                        //_MarcarBloqueAula(horario_docente, horarios[posicion_horario, 2]);
                        //_MarcarBloqueAula(horario_aula, horarios[posicion_horario, 2]);
                        //_MarcarBloqueAula(horario_grupo, horarios[posicion_horario, 2]);
                    }
                }
            }
        }

        public ClsIndividuo _ReparaChoqueDocente(ClsIndividuo indv)
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            int[,] horarios = indv.Horario;
            int[][,] horariosDocente = indv.HorariosDocentes;
            int[][,] horariosGrupo = indv.HorariosGrupos;
            int[][,] horariosAulas = indv.HorariosAulas;

            LinkedList<int> bloques_chocando = new LinkedList<int>();
            LinkedList<int> claves_aparicion_docente = new LinkedList<int>();
            LinkedList<int> posiciones_choque = new LinkedList<int>();
            LinkedList<int> claves_choque = new LinkedList<int>();
            LinkedList<int> grupos_afectados = new LinkedList<int>();
            LinkedList<LinkedList<int>> bloques_claves = new LinkedList<LinkedList<int>>();
            LinkedList<LinkedList<int>> bloques_posiciones = new LinkedList<LinkedList<int>>();
            LinkedList<int> posiciones_clave = new LinkedList<int>();
            int[][] opc_adyacentes;
            int[] opc_solos;

            int clave = -1;
            bool resuelto = false;

            for (int i = 0; i < horariosDocente.Length; i++)
            {
                bloques_chocando = _ObtenerBloquesChocando(horariosDocente[i]);
                if (bloques_chocando.Count == 0)
                    continue;

                int clv_docente = _SuperIndv.Docentes[i];

                claves_aparicion_docente = _Obtener_DatosMatriz(3, 0, clv_docente, _SuperIndv.ClaveGrupoMateriaDocente);


                for (int j = 0; j < bloques_chocando.Count; j++)
                {
                    bloques_claves.AddLast(new LinkedList<int>());
                    bloques_posiciones.AddLast(new LinkedList<int>());

                    for (int k = 0; k < horarios.GetLength(0); k++)
                    {
                        clave = horarios[k, 0];
                        if (claves_aparicion_docente.Contains(clave) && horarios[k, 2] == bloques_chocando.ElementAt(j))
                        {
                            bloques_claves.ElementAt(j).AddLast(clave);
                            bloques_posiciones.ElementAt(j).AddLast(k);
                        }
                    }

                    if(bloques_claves.ElementAt(j).Count == 0)
                    {
                        bloques_claves.RemoveLast();
                        bloques_posiciones.RemoveLast();
                    }
                }

                for (int j = 0; j < bloques_claves.Count; j++)
                {
                    int bloque = bloques_chocando.ElementAt(j);

                    for (int k = 0; k < bloques_claves.ElementAt(j).Count; k++)
                    {
                        int posicion = bloques_posiciones.ElementAt(j).ElementAt(k);
                        int clv = bloques_claves.ElementAt(j).ElementAt(k);
                        int materia = _SuperIndv.ClaveGrupoMateriaDocente[clv - 1, 2];
                        int pos_superbloque = -1;
                        int pos_objeto = -1;

                        int pos_grupo = _BuscaPosicionArray(_SuperIndv.Grupos, _SuperIndv.ClaveGrupoMateriaDocente[clv - 1, 1]);
                        int[] disponibles_grupo = _ObtenerBloquesDisponibles(horariosGrupo[pos_grupo]);
                        int pos_aula = _BuscaPosicionArray(_SuperIndv.Aulas, horarios[posicion, 1]);
                        int[] disponibles_aula = _ObtenerBloquesDisponibles(horariosAulas[pos_aula]);
                        int pos_docente = _BuscaPosicionArray(_SuperIndv.Docentes, _SuperIndv.ClaveGrupoMateriaDocente[clv - 1, 3]);
                        int[] disponibles_docente = _ObtenerBloquesDisponibles(horariosDocente[pos_docente]);

                        int[] libres_comun = _TeoriaConjuntos(disponibles_grupo, disponibles_aula, disponibles_docente);

                        if (libres_comun != null)
                        {
                            int[] permitidos = _BloquesPermitidos(_DiasOcupadosMateria(horarios, horarios[posicion, 0]), libres_comun);

                            if (permitidos != null)
                            {

                                int[] patron_superbloque = _SuperIndv.MateriasSuperbloques[_BuscaPosicionArray(_SuperIndv.Materias, materia)];
                                int[][] bloques_superbloque = new int[patron_superbloque.Length][];
                                int[][] superbloque_posiciones = new int[patron_superbloque.Length][];
                                LinkedList<int> bloques = _Obtener_DatosMatriz(0, 2, clv, horarios);

                                for (int l = 0; l < horarios.GetLength(0); l++)
                                {
                                    if (clv == horarios[l, 0])
                                        posiciones_clave.AddLast(l);
                                }

                                for (int l = 0; l < bloques_superbloque.Length; l++)
                                {
                                    bloques_superbloque[l] = new int[patron_superbloque[l]];
                                    superbloque_posiciones[l] = new int[patron_superbloque[l]];
                                    for (int m = 0; m < bloques_superbloque[l].Length; m++)
                                    {
                                        bloques_superbloque[l][m] = bloques.First();
                                        bloques.RemoveFirst();
                                        superbloque_posiciones[l][m] = posiciones_clave.First();
                                        posiciones_clave.RemoveFirst();
                                    }
                                }

                                for (int l = 0; l < bloques_superbloque.Length; l++)
                                {
                                    if (_BuscaPosicionArray(bloques_superbloque[l], bloque) != -1)
                                    {
                                        pos_superbloque = l;
                                        break;
                                    }
                                }

                                if (pos_superbloque == -1)
                                    continue;

                               int[] posiciones_finales = superbloque_posiciones[pos_superbloque];


                                if (bloques_superbloque[pos_superbloque].Length > 1)
                                {
                                    opc_adyacentes = _Buscar_BloquesAdyacentes(permitidos, bloques_superbloque[pos_superbloque].Length);

                                    if (opc_adyacentes != null)
                                    {
                                        int[] combinacion = opc_adyacentes[aleatorio.Next(0, opc_adyacentes.Length)];

                                        for (int l = 0; l < combinacion.Length; l++)
                                        {
                                            _DesmarcarBloqueHorario(horariosDocente[pos_docente], horarios[posiciones_finales[l], 2]);
                                            _DesmarcarBloqueHorario(horariosAulas[pos_aula], horarios[posiciones_finales[l], 2]);
                                            _DesmarcarBloqueHorario(horariosGrupo[pos_grupo], horarios[posiciones_finales[l], 2]);
                                            horarios[posiciones_finales[l], 2] = combinacion[l];
                                            _MarcarBloqueHorario(horariosDocente[pos_docente], combinacion[l]);
                                            _MarcarBloqueHorario(horariosAulas[pos_aula], combinacion[l]);
                                            _MarcarBloqueHorario(horariosGrupo[pos_grupo], combinacion[l]);
                                            resuelto = true;
                                            //p_dias.AddLast(posiciones_finales[i]);
                                        }
                                    }
                                }
                                else
                                {
                                    opc_solos = _Buscar_BloquesSolos(permitidos);

                                    if (opc_solos != null)
                                    {
                                        _DesmarcarBloqueHorario(horariosDocente[pos_docente], bloque);
                                        _DesmarcarBloqueHorario(horariosAulas[pos_aula], bloque);
                                        _DesmarcarBloqueHorario(horariosGrupo[pos_grupo], bloque);
                                        horarios[posicion, 2] = opc_solos[aleatorio.Next(0, opc_solos.Length)];
                                        _MarcarBloqueHorario(horariosDocente[pos_docente], horarios[posicion, 2]);
                                        _MarcarBloqueHorario(horariosAulas[pos_aula], horarios[posicion, 2]);
                                        _MarcarBloqueHorario(horariosGrupo[pos_grupo], horarios[posicion, 2]);
                                        resuelto = true;
                                        //p_dias.AddLast(posicion_horario);
                                    }
                                }
                            }
                        }

                        if(resuelto == true)
                        {
                            if(bloques_claves.ElementAt(j).Count == 2)
                            {
                                //se borra toda la lista de la posicion J o solo se rompe el ciclo interno
                                //Por lo tanto se rompe el ciclo interno 
                                resuelto = false;
                                break;
                            }
                            else
                            {
                                //No pasa nada
                            }
                        }


                    }
                }
                bloques_posiciones.Clear();
                bloques_claves.Clear();
                bloques_chocando.Clear();
                claves_aparicion_docente.Clear();
                posiciones_choque.Clear();
                claves_choque.Clear();
                grupos_afectados.Clear();
            }

            ClsIndividuo indvNuevo = new ClsIndividuo(_SuperIndv, horarios);
            return indvNuevo;
        }

        private int[] _TeoriaConjuntos(int[] BloquesGrupo, int[] BloquesAula, int[] BloquesDocente)
        {
            if (BloquesAula == null || BloquesDocente == null || BloquesGrupo == null)
                return null;
            LinkedList<int> horarioAula = new LinkedList<int>(BloquesAula);
            LinkedList<int> horarioDocente = new LinkedList<int>(BloquesDocente);
            LinkedList<int> comunes = new LinkedList<int>();
            int bloque = -1;
            for(int i = 0; i < BloquesGrupo.Length; i++)
            {
                bloque = BloquesGrupo[i];
                if(horarioAula.Contains(bloque) && horarioDocente.Contains(bloque))
                {
                    comunes.AddLast(bloque);
                }
            }
            if (comunes.Count == 0)
                return null;
            return comunes.ToArray();
        }

        private int[] _ObtenerBloquesDisponibles(int[,] horario)
        {
            LinkedList<int> disponibles = new LinkedList<int>();

            for (int i = 0; i < horario.GetLength(0); i++)
            {
                if (horario[i, 2] == 0)
                {
                    disponibles.AddLast(horario[i, 1]);
                }
            }

            if (disponibles.Count == 0)
                return null;

            return disponibles.ToArray();
        }

        private void _MarcarBloqueHorario(int[,] horario, int bloque)
        {
            for(int i = 0; i < horario.GetLength(0); i++)
            {
                if (horario[i, 1] == bloque)
                {
                    horario[i, 2] = horario[i, 2] + 1;
                    break;
                }
            }
        }
        
        private void _DesmarcarBloqueHorario(int[,] horario, int bloque)
        {
            for (int i = 0; i < horario.GetLength(0); i++)
            {
                if (horario[i, 1] == bloque)
                {
                    horario[i, 2] = horario[i, 2] - 1;
                    break;
                }
            }
        }

        private LinkedList<int> _ObtenerBloquesChocando(int[,] horario)
        {
            LinkedList<int> bloques = new LinkedList<int>();
            for (int j = 0; j < horario.GetLength(0); j++)
            {
                if (horario[j, 2] > 1)
                    bloques.AddLast(horario[j, 1]);
            }

            return bloques;
        }

        private LinkedList<int> _DiasOcupadosMateria(int[,] horario, int clv_horario)
        {
            //Extrae los bloques que se estan utilizando para esa materia
            LinkedList<int> bloques = _Obtener_DatosMatriz(0, 2, clv_horario, horario);
            LinkedList<int> dias_ocupados = new LinkedList<int>();

            //Recorre los bloques que tiene disponible cada dia
            //Ejemplo el lunes tiene los bloques 1,2,3,4,6,7,8,9
            //Recorre los dias
            for (int j = 0; j < _SuperIndv.BloquesDias.Length; j++)
            {
                //Recorre los bloques de cada dia
                for (int k = 0; k < _SuperIndv.BloquesDias[j].Length; k++)
                {
                    //Si uno de los bloques se encuentra en la lista de los que estamos ocupando
                    //Significa que ese dia ya se esta ocupando por lo que no se puede impartir la materia de nuevo ese mismo dia
                    if (bloques.Contains(_SuperIndv.BloquesDias[j][k]))
                    {
                        //Se añade el dia que ya se ocupo y se rompe el ciclo 
                        //Para pasar con los bloques de otro dia
                        dias_ocupados.AddLast(j);
                        break;
                    }
                }
            }

            //Retorna las posiciones de los dias ocupados (0 , 1, 2, 3, 4)Solo de lunes a viernes
            return dias_ocupados;
        }

        private int[] _BloquesPermitidos(LinkedList<int> ocupados, int[] libres_comunes)
        {
            //Bloques disponibles en los tres horarios
            LinkedList<int> libres = new LinkedList<int>(libres_comunes);
            LinkedList<int> permitidos = new LinkedList<int>();
            int bloque;

            //Recorre los dias y sus bloques disponibles
            for (int i = 0; i < _SuperIndv.BloquesDias.Length; i++)
            {
                //Si ese dia ya se esta ocupando sus bloques no estan disponibles
                if (ocupados.Contains(i))
                    continue;
                for(int j = 0; j < _SuperIndv.BloquesDias[i].Length; j++)
                {
                    bloque = _SuperIndv.BloquesDias[i][j];
                    if (libres.Contains(bloque))
                        permitidos.AddLast(bloque);
                }
            }

            if (permitidos.Count == 0)
                return null;
            return permitidos.ToArray();
            //De los bloques que se tienen en comun solo se retornaran aquellos que no esten en un dia ya ocupado por la materia
        }

        private int[][] _Buscar_BloquesAdyacentes(int[] bloques_disponibles, int tam_superbloque)
        {
            if (bloques_disponibles == null)
                return null;

            int[][] array_combinaciones;
            LinkedList<int[]> lista_combinaciones = new LinkedList<int[]>();
            LinkedList<int> combinacion = new LinkedList<int>();

            int puntos = 0;
            int intentos = tam_superbloque - 1;

            for (int i = 1; i < bloques_disponibles.Length; i++)
            {
                int j;
                for (j = i; j < bloques_disponibles.Length && intentos > 0; j++)
                {
                    if (bloques_disponibles[j] - bloques_disponibles[j - 1] == 1)
                    {
                        combinacion.AddLast(bloques_disponibles[j - 1]);
                        puntos++;
                    }
                    intentos--;
                }

                if (puntos == tam_superbloque - 1)
                {
                    combinacion.AddLast(bloques_disponibles[j - 1]);
                    lista_combinaciones.AddLast(combinacion.ToArray<int>());
                }
                puntos = 0;
                intentos = tam_superbloque - 1;
                combinacion.Clear();
            }

            if (lista_combinaciones.Count == 0)
                return null;

            array_combinaciones = new int[lista_combinaciones.Count][];
            lista_combinaciones.CopyTo(array_combinaciones, 0);

            return array_combinaciones;
        }

        private int[] _Buscar_BloquesSolos(int[] bloques_disponibles)
        {
            if (bloques_disponibles == null)
                return null;

            LinkedList<int> lista_solos = new LinkedList<int>();
            int[] array_solos;
            int j = -1;
            for (int i = 1; i < bloques_disponibles.Length; i++)
            {
                j = i - 1;
                if (j != 0)
                {
                    if (bloques_disponibles[i] - bloques_disponibles[j] > 1 && bloques_disponibles[j] - bloques_disponibles[j - 1] > 1)
                    {
                        lista_solos.AddLast(bloques_disponibles[i - 1]);

                        if (i == bloques_disponibles.Length - 1)
                            lista_solos.AddLast(bloques_disponibles[i]);
                    }
                }
                else
                {
                    if (bloques_disponibles[i] - bloques_disponibles[j] > 1)
                        lista_solos.AddLast(bloques_disponibles[i - 1]);
                }
            }

            if (lista_solos.Count == 0)
                return null;

            array_solos = new int[lista_solos.Count];
            lista_solos.CopyTo(array_solos, 0);

            return array_solos;
        }

        #endregion

        private int _BuscaPosicionArray(int[] array, int clave)
        {
            for (int indice = 0; indice < array.Length; indice++)
            {
                if (array[indice] == clave)
                    return indice;
            }
            return -1;
        }

        private int[,] _ExtraerGenes(int[,] padre, int clv_usada)
        {
            LinkedList<int> claves_indv = new LinkedList<int>();
            LinkedList<int> aulas_indv = new LinkedList<int>();
            LinkedList<int> bloques_indv = new LinkedList<int>();

            for (int i = 0; i < padre.GetLength(0); i++)
            {
                if (padre[i, 0] == clv_usada)
                {
                    claves_indv.AddLast(padre[i, 0]);
                    aulas_indv.AddLast(padre[i, 1]);
                    bloques_indv.AddLast(padre[i, 2]);
                }
            }

            int[,] genes = new int[aulas_indv.Count, 3];

            for (int i = 0; i < genes.GetLength(0); i++)
            {
                genes[i, 0] = claves_indv.ElementAt(i);
                genes[i, 1] = aulas_indv.ElementAt(i);
                genes[i, 2] = bloques_indv.ElementAt(i);
            }

            return genes;
        }

        private int[] _ObtenerClvs_SuperIndv(int pos_columna, int valor)
        {
            LinkedList<int> lista = new LinkedList<int>();
            for (int i = 0; i < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                if(_SuperIndv.ClaveGrupoMateriaDocente[i, pos_columna] ==  valor)
                    lista.AddLast(_SuperIndv.ClaveGrupoMateriaDocente[i, 0]);
            }

            if (lista.Count == 0)
                return null;

            int[] claves = new int[lista.Count];
            lista.CopyTo(claves, 0);
            return claves;
        }

        private LinkedList<int> _Obtener_DatosSuperIndv(int columa_compara, int columna_extraccion, int valor)
        {
            LinkedList<int> lista = new LinkedList<int>();
            for (int i = 0; i < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                if (_SuperIndv.ClaveGrupoMateriaDocente[i, columa_compara] == valor)
                    lista.AddLast(_SuperIndv.ClaveGrupoMateriaDocente[i, columna_extraccion]);
            }

            return lista;
        }

        private LinkedList<int> _Obtener_DatosMatriz(int columa_compara, int columna_extraccion, int valor, int[,] matriz)
        {
            LinkedList<int> lista = new LinkedList<int>();
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                if (matriz[i, columa_compara] == valor)
                    lista.AddLast(matriz[i, columna_extraccion]);
            }

            return lista;
        }

        private int[] _ObtenerClavesCruza()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            LinkedList<int> materias = new LinkedList<int>(_SuperIndv._Datos.Materias[_SuperIndv._Turno]);
            LinkedList<int> lista_clvsTemporales = new LinkedList<int>();
            int[] arreglo_clvsTemporales;

            int tam = materias.Count / 2;
            int posicion = -1;

            for (int i = 0; i < tam; i++)
            {
                posicion = aleatorio.Next(0, materias.Count);
                arreglo_clvsTemporales = _ObtenerClvs_SuperIndv(2, materias.ElementAt(posicion));

                for (int j = 0; j < arreglo_clvsTemporales.Length; j++)
                {
                    lista_clvsTemporales.AddLast(arreglo_clvsTemporales[j]);
                }

                materias.Remove(materias.ElementAt(posicion));
            }

            int[] claves = new int[lista_clvsTemporales.Count];
            lista_clvsTemporales.CopyTo(claves, 0);

            return claves;

        }//Al azar la mitad de las materias

        private int[] _ObtenerClavesCruza2()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            LinkedList<int> materias = new LinkedList<int>(_SuperIndv._Datos.Materias[_SuperIndv._Turno]);
            LinkedList<int> lista_clvsTemporales = new LinkedList<int>();
            int[] arreglo_clvsTemporales;

            int tam = aleatorio.Next(1, materias.Count);
            int posicion = -1;

            for (int i = 0; i < tam; i++)
            {
                posicion = aleatorio.Next(0, materias.Count);
                arreglo_clvsTemporales = _ObtenerClvs_SuperIndv(2, materias.ElementAt(posicion));

                for (int j = 0; j < arreglo_clvsTemporales.Length; j++)
                {
                    lista_clvsTemporales.AddLast(arreglo_clvsTemporales[j]);
                }

                materias.Remove(materias.ElementAt(posicion));
            }

            int[] claves = new int[lista_clvsTemporales.Count];
            lista_clvsTemporales.CopyTo(claves, 0);

            return claves;

        }//Al azar la cantidad de materias y las materias

        private int[] _ObtenerClavesCruza3()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);

            int cantidad = aleatorio.Next(1, _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0)/2 + 1);
            int[] claves = new int[cantidad];
            LinkedList<int> lista_claves = new LinkedList<int>();
            int dato;

            for(int i = 0; i < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                lista_claves.AddLast(_SuperIndv.ClaveGrupoMateriaDocente[i, 0]);
            }

            for(int i = 0; i < claves.Length; i++)
            {
                dato = aleatorio.Next(0, lista_claves.Count);
                claves[i] = dato;
                lista_claves.Remove(dato);
            }

            return claves;

        }

        private int _BuscarEnArray(int[] arreglo, int clave)
        {
            for (int i = 0; i < arreglo.Length; i++)
            {
                if (arreglo[i] == clave)
                    return i;
            }
            return -1;
        }

        private void _InsertarDirecto(int[,] GenesA, int[,] GenesB, int[,] HijoA, int[,] HijoB)
        {
            for (int i = 0; i < GenesA.GetLength(0) && posicion < HijoA.GetLength(0); i++, posicion++)
            {
                HijoA[posicion, 0] = GenesA[i, 0];
                HijoA[posicion, 1] = GenesA[i, 1];
                HijoA[posicion, 2] = GenesA[i, 2];

                HijoB[posicion, 0] = GenesB[i, 0];
                HijoB[posicion, 1] = GenesB[i, 1];
                HijoB[posicion, 2] = GenesB[i, 2];
            }
        }

        private void _InsertarCruzado(int[,] GenesA, int[,] GenesB, int[,] HijoA, int[,] HijoB)
        {
            for (int i = 0; i < GenesA.GetLength(0) && posicion < HijoA.GetLength(0); i++, posicion++)
            {
                HijoA[posicion, 0] = GenesB[i, 0];
                HijoA[posicion, 1] = GenesB[i, 1];
                HijoA[posicion, 2] = GenesB[i, 2];

                HijoB[posicion, 0] = GenesA[i, 0];
                HijoB[posicion, 1] = GenesA[i, 1];
                HijoB[posicion, 2] = GenesA[i, 2];
            }
        }

        private void _NewGeneration(/*ClsIndividuo[] _PoblacionPadres.Poblacion, ClsIndividuo[] Descendencia*/)
        {
            int tam_muestra = 7;
            ClsIndividuo[] NewPopulation = new ClsIndividuo[_PoblacionPadres.Poblacion.Length];
            ClsIndividuo[] Competidores = new ClsIndividuo[tam_muestra];//variable local tam = 7;

            LinkedList<ClsIndividuo> PoblacionPadres = new LinkedList<ClsIndividuo>(_PoblacionPadres.Poblacion);
            LinkedList<double> FitnessPadres = new LinkedList<double>(_PoblacionPadres.Fitness);

            LinkedList<ClsIndividuo> PoblacionHijos = new LinkedList<ClsIndividuo>(_PoblacionHijos.Poblacion);
            LinkedList<double> FitnessHijos = new LinkedList<double>(_PoblacionHijos.Fitness);

            double[] fitness = new double[tam_muestra];
            double[] fitness_newpopulation = new double[_PoblacionPadres.Poblacion.Length];
            int[] origen = new int[tam_muestra];
            int[] posiciones = new int[tam_muestra];
            int[,] origen_posicion = new int[tam_muestra, 2];

            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);
            int value;
            int limite = _PoblacionPadres.Poblacion.Length + _PoblacionHijos.Poblacion.Length;
            double fitness_thebest;

            ElMejor = _PoblacionHijos.Poblacion[_PoblacionHijos.TheBest];
            fitness_thebest = _PoblacionHijos.TheBestFitness;

            if (_PoblacionPadres.Fitness[_PoblacionPadres.TheBest] < _PoblacionHijos.Fitness[_PoblacionHijos.TheBest])
            { 
                ElMejor = _PoblacionPadres.Poblacion[_PoblacionPadres.TheBest];
                PoblacionPadres.Remove(PoblacionPadres.ElementAt(_PoblacionPadres.TheBest));
                fitness_thebest = _PoblacionPadres.TheBestFitness;
            }
            else
            {
                PoblacionHijos.Remove(PoblacionHijos.ElementAt(_PoblacionHijos.TheBest));
            }

            ClsIndividuo reparado = _ReparaChoqueGrupoAula(ElMejor);
            reparado = _ReparaChoqueDocente(reparado);

            NewPopulation[0] = ElMejor;
            fitness_newpopulation[0] = fitness_thebest;
            NewPopulation[1] = reparado;
            fitness_newpopulation[1] = ClsEvaluacion.fitness(reparado);

            for (int i = 2; i < NuevaGeneracion.getTam; i++)
            {
                for (int j = 0; j < Competidores.Length; j++)
                {
                    value = aleatorio.Next(0, 200);// 0 - 199 aqui iba la variable limite

                    if (value < 100)
                    {
                        value = aleatorio.Next(0, PoblacionPadres.Count);
                        origen_posicion[j, 0] = 0;
                        origen_posicion[j, 1] = value;
                        Competidores[j] = PoblacionPadres.ElementAt(value);
                        fitness[j] = FitnessPadres.ElementAt(value);
                    }
                    else
                    {
                        value = aleatorio.Next(0, PoblacionHijos.Count);
                        origen_posicion[j, 0] = 1;
                        origen_posicion[j, 1] = value;
                        Competidores[j] = PoblacionHijos.ElementAt(value);
                        fitness[j] = FitnessHijos.ElementAt(value);
                    }
                }
                value = 0;

                //Incluir operador de turbulencia
                if (porcentaje_turbulencia < aleatorio.NextDouble())
                {
                    int thebest = _TheBest(fitness);

                    if (aleatorio.NextDouble() < porcentaje_reparacion)
                    {
                        ClsIndividuo reparado2 = _ReparaChoqueGrupoAula(Competidores[thebest]);
                        reparado2 = _ReparaChoqueDocente(reparado2);
                        NewPopulation[i] = reparado2;
                        fitness_newpopulation[i] = ClsEvaluacion.fitness(reparado2);
                    }
                    else
                    {
                        NewPopulation[i] = Competidores[thebest];
                        fitness_newpopulation[i] = fitness[thebest];
                    }


                    //fitness_newpopulation[i] = fitness[thebest];


                    if (origen_posicion[thebest, 0] == 0)
                        PoblacionPadres.Remove(PoblacionPadres.ElementAt(origen_posicion[thebest, 1]));
                    else
                        PoblacionHijos.Remove(PoblacionHijos.ElementAt(origen_posicion[thebest, 1]));
                }
                else
                {
                    //APLICA TURBULENCIA
                    ClsIndividuo nuevo = new ClsIndividuo(_SuperIndv);
                    NewPopulation[i] = nuevo;
                    fitness_newpopulation[i] = ClsEvaluacion.fitness(nuevo);
                }
            }

            NuevaGeneracion.Poblacion = NewPopulation;
            //Console.WriteLine("************************");
            NuevaGeneracion.Fitness = fitness_newpopulation;
            //NuevaGeneracion._Evalua_Poblacion();
            _PoblacionPadres = NuevaGeneracion;
        }
    }
}
