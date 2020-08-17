using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ags
{
    class ClsIndividuo : AbsClsIndividuo 
    {

        private int bloques_random = 0;// Se uso para pruebas nada mas, se puede borrar sin problemas
        private int salvados = 0;
        private int salvadossolo = 0;
        private int salvadospares = 0;
        LinkedList<int> posiciones = new LinkedList<int>();

        public ClsIndividuo(ClsSuperIndividuo superIndv)
        {
            _SuperIndv = superIndv;
            Configuracion();
            _Horarios = new int[GSuperIndividuo._tamIndividuo, 3];
            _Llenado_Clave(_SuperIndv.ClaveGrupoMateriaDocente);
            _Llenado_Aula_Bloques(_OrdenaMaterias_Asc());
        }

        public ClsIndividuo(ClsSuperIndividuo superIndv, int[,] horarios)
        {
            _Horarios = ClonadorMatrices(horarios);
            _SuperIndv = superIndv;
            Configuracion();
            _LlenadoPropiedades();
        }

        public int[,] Horario
        {
            get => ClonadorMatrices(_Horarios);
            set => _Horarios = ClonadorMatrices(value);
        }

        public int[][,] HorariosGrupos
        {
            get => ClonadorMatrizEscalonada(_Horario_Grupos);
            set => _Horario_Grupos = ClonadorMatrizEscalonada(value);
        }

        public int[][,] HorariosDocentes
        {
            get => ClonadorMatrizEscalonada(_Horario_Docentes);
            set => _Horario_Docentes = ClonadorMatrizEscalonada(value);
        }

        public int[][,] HorariosAulas
        {
            get => ClonadorMatrizEscalonada(_Horarios_Aulas);
            set => _Horarios_Aulas = ClonadorMatrizEscalonada(value);
        }

        public int TamIndiv
        {
            get => _Horarios.GetLength(0);
        }

        public ClsSuperIndividuo SuperIndv
        {
            get => _SuperIndv;
        }

        private int[,] ClonadorMatrices(int[,] matriz)
        {
            int[,] CloneMatriz = new int[matriz.GetLength(0), matriz.GetLength(1)];

            for (int j = 0; j < matriz.GetLength(0); j++)
                for (int k = 0; k < matriz.GetLength(1); k++)
                    CloneMatriz[j, k] = matriz[j, k];

            return CloneMatriz;
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

        private void Configuracion()
        {
            HorariosAulas = _SuperIndv._Datos.Horarios_Aulas[_SuperIndv._Turno];
            HorariosDocentes = _SuperIndv._Datos.Horarios_Docentes[_SuperIndv._Turno];
            HorariosGrupos = _SuperIndv._Datos.Horarios_Grupos[_SuperIndv._Turno];
        }

        private void _LlenadoPropiedades()
        {
            int clave = -1;
            int clvDocente;
            int clvGrupo;
            int clvAula;
            int bloque;
            int posicion;

            for(int i = 0; i < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                clave = _SuperIndv.ClaveGrupoMateriaDocente[i,0];
                clvGrupo = _SuperIndv.ClaveGrupoMateriaDocente[i, 1];
                clvDocente = _SuperIndv.ClaveGrupoMateriaDocente[i, 3];

                for (int j = 0; j < _Horarios.GetLength(0); j++)
                {
                    if(clave == _Horarios[j,0])
                    {
                        clvAula = _Horarios[j, 1];
                        bloque = _Horarios[j, 2];

                        //posicion = _BuscaPosicionArray(_SuperIndv.Docentes, clvDocente);
                        //_Horario_Docentes[posicion][_BuscaPosicionBloque(_Horario_Docentes[posicion], bloque), 2] += 1;

                        //posicion = _BuscaPosicionArray(_SuperIndv.Grupos, clvGrupo);
                        //_Horario_Grupos[posicion][_BuscaPosicionBloque(_Horario_Grupos[posicion], bloque), 2] += 1;

                        //posicion = _BuscaPosicionArray(_SuperIndv.Aulas, clvAula);
                        //_Horarios_Aulas[posicion][_BuscaPosicionBloque(_Horarios_Aulas[posicion], bloque), 2] += 1;

                        posicion = _BuscaPosicionArray(_SuperIndv.Docentes, clvDocente);
                        _MarcaBloque(_Horario_Docentes, posicion, bloque);

                        posicion = _BuscaPosicionArray(_SuperIndv.Grupos, clvGrupo);
                        _MarcaBloque(_Horario_Grupos, posicion, bloque);

                        posicion = _BuscaPosicionArray(_SuperIndv.Aulas, clvAula);
                        _MarcaBloque(_Horarios_Aulas, posicion, bloque);

                    }
                }
            }
        }

        private void _Llenado_Clave(int[,] _MateriaDocente)//metodo que llena la matriz Clave, Aula Bloque
        {
            int vueltas = 0, Grupo, Materia, SuperBloque, Clave;
            for (int Registros = 0; Registros < GSuperIndividuo._GrupoMateriaSuperBloque.GetLength(0); Registros++)
            {
                Grupo = GSuperIndividuo._GrupoMateriaSuperBloque[Registros, 1];
                Materia = GSuperIndividuo._GrupoMateriaSuperBloque[Registros, 2];
                Clave = _BuscaClave(Grupo, Materia, _MateriaDocente);
                SuperBloque = GSuperIndividuo._GrupoMateriaSuperBloque[Registros, 3];

                for (int x = 0; x < SuperBloque; x++)
                {
                    _Horarios[vueltas, 0] = Clave;
                    _Horarios[vueltas, 1] = -1;
                    _Horarios[vueltas, 2] = -1;
                    vueltas++;
                }
            }
        }
        
        private int _BuscaClave(int Grupo, int Materia, int[,] _MateriaDocente)//Metodo complementario de _Llenado_Clave
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

        private void _Llenado_Aula_Bloques(int[] MateriasOrdenadas_Asc)
        {
            int[] claves_superindv;
            int posicionAula = -1;
            int clv_aula, clv_materia, clv_grupo, clv_docente;
            int[] superbloques;
            int[] bloques_comunes;
            int[,] AulasBloques = _SuperIndv.BloquesAulas;

            LinkedList<int[]> dias_bloques;
            int[] dia;


            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);
            int valor_random;

            //Recorre un arreglo de materias ordenadas de menos a mas aulas disponibles
            for (int ind_materia = 0; ind_materia < MateriasOrdenadas_Asc.Length; ind_materia++)
            {
                /*Se obtiene la clave de la materia por cada iteracion y es utilizada para obtener 
                 * las claves del super individuo en donde aparece*/
                clv_materia = MateriasOrdenadas_Asc[ind_materia];
                claves_superindv = _ObtenerClavesSuperIndividuo(clv_materia);

                /*Se obtiene un arreglo que contiene el tamaño y distribucion de los superbloques por ejemplo
                 * para una materia de 7 horas la distribucion es {2, 2, 2, 1}*/
                superbloques = _SuperIndv.MateriasSuperbloques[_BuscaPosicionArray(_SuperIndv.Materias, clv_materia)];//_ObtenerSuperbloques(clv_materia);
                /*La clave del aula en cada iteracion de materia se establece en -1*/
                clv_aula = -1;

                /* Se recorre el arreglo de claves en las que aparece esa materia en el superindividuo*/
                for (int ind_clave = 0; ind_clave < claves_superindv.Length; ind_clave++)
                {
                    /* se obtiene la clave del grupo y docente que se les asigno la materia y se crea 
                     * una lista de arreglos que cada posicion contiene los bloques correspondientes a cada dia de la semana
                     * por ejemplo el dia lunes que es la posicion 0 contiene los bloques {1, 2, 3, 4, 6, 7, 8, 9}*/
                    clv_grupo = _ObtenerGrupo(claves_superindv[ind_clave]);
                    clv_docente = _ObtenerDocente(claves_superindv[ind_clave]);
                    dias_bloques = new LinkedList<int[]>(_SuperIndv.BloquesDias);

                    /*Si la clave del aula es igual a -1 es decir que si se esta trabajando con otra materia entonces 
                     * se obtiene un aula nueva en donde se pueda impartir esa materia, aun no se asigna el aula*/
                    if(clv_aula == -1)
                    {
                        posicionAula = _Busca_Posicion_AulaRandom(AulasBloques, _BuscaPosicionArray(_SuperIndv.Materias, clv_materia));
                        clv_aula = AulasBloques[posicionAula, 0];
                    }

                    /*Ahora se itera sobre el arreglo que contiene el tamaño de los superbloques*/
                    for (int ind_sesion = 0; ind_sesion < superbloques.Length; ind_sesion++)
                    {
                        /* se elige un dia aleatoriamente y de ese dia se obtiene los bloques que pertenecen a el
                         * ese dia queda descartado meintras se siga trabajando con la materia para que ninguna sesion 
                         * de clases se de nuevamente en ese mismo dia*/
                        dia = dias_bloques.ElementAt(aleatorio.Next(0, dias_bloques.Count));
                        dias_bloques.Remove(dia);

                        /* Si el tamaño del superbloque es mayor a uno entonces se ocupara la busqueda de bloques adyacentes*/
                        if (superbloques[ind_sesion] > 1)
                        {
                            /*Se obtienen los bloques disponibles en los que coinciden el grupo, el docente y el aula 
                             * segun el dia que se establecio anteriormente, se utiliza la teoria de conjuntos*/
                            bloques_comunes = _BloquesLibres(clv_grupo, clv_docente, clv_aula, dia);
                            
                            /* De los bloques que tienen en comun se buscan las posibles combinaciones de bloques 
                             * que cumplan con el tamaño del superbloque*/
                            int[][] combinaciones = _Buscar_BloquesAdyacentes(bloques_comunes, superbloques[ind_sesion]);
                            int[] combinacion = null;

                            /*Si el arreglo combinaciones es nulo solo hay 2 posibles situaciones: no hay bloques libres 
                             * en los que coincidan o simplemente no hay combinaciones. OJO: que un no habiendo combinaciones 
                             * puede haber bloques en solitario que pueden servir en las asignaciones de una hora*/
                            if (combinaciones != null)
                            {
                                /* En caso contrario se elije de manera aleatoria la combinacion de bloques que se usara*/
                                valor_random = aleatorio.Next(0, combinaciones.Length);
                                combinacion = combinaciones[valor_random];
                            }


                            bool opc = false; //PRUEBA******************************* 


                            /*Ahora se itera sobre el tamaño del superbloque*/
                            for (int i = 0; i < superbloques[ind_sesion]; i ++)
                            {
                                /*Se recorre la matriz que contendra la clave, aula y bloque, por ahora solo contiene la clave*/
                                for (int ind_horario = 0; ind_horario < _Horarios.GetLength(0); ind_horario++)
                                {
                                    /*Ahora utilizamos el arreglo que contiene las claves de las filas del superindividuo en donde esta presente 
                                     * la clave de la materia si la clave que se tiene en la matriz de los horarios coincide con la clave que 
                                     * se esta usando y no se ha asignado ningun aula o bloque entonces....*/
                                    if (_Horarios[ind_horario, 0] == claves_superindv[ind_clave] && _Horarios[ind_horario, 1] == -1 && _Horarios[ind_horario, 2] == -1)
                                    {
                                        /*Ahora se asigna el aula seleccionada anteriormente*/
                                        //clv_aula = _AsignarAula(AulasBloques, posicionAula, ind_horario, clv_aula, clv_materia);

                                        /*Si no existen combinaciones entonces se trata de cambiar el dia en el que se imparte la sesion*/
                                        if (combinacion == null || bloques_comunes == null)
                                        {
                                            /*Si la sesion puede ser cambiada de dia entonces se asignan los bloques dentro del metodo 
                                             * _ProbarOtroDia si no se puede cambiar de dia entonces los bloques se asignan aleatoriamente
                                             * ocasionando choques, se rompe el for que itera sobre el tamaño del superbloque*/
                                            if (!_ProbarOtroDia(dias_bloques, dia, aleatorio, ind_horario, superbloques[ind_sesion], clv_grupo, clv_docente, clv_aula))
                                            {
                                                for (int z = 0; z < superbloques[ind_sesion]; z++)//PRUEBA***********************+
                                                {
                                                    clv_aula = _AsignarAula(AulasBloques, posicionAula, ind_horario, clv_aula, clv_materia);
                                                    int[] posibles = _OpcionesBloqueRandom(clv_grupo, clv_docente, clv_aula);
                                                    valor_random = aleatorio.Next(0, posibles.Length);
                                                    _AsignarBloque(ind_horario, clv_grupo, clv_docente, clv_aula, posibles[valor_random]);
                                                    ind_horario++;//PRUEBA***********************+
                                                    bloques_random++;
                                                    posiciones.AddLast(ind_horario);
                                                }
                                                opc = true;//PRUEBA***********************+
                                                break;
                                            }
                                            else
                                            {
                                                for (int z = 0; z < superbloques[ind_sesion]; z++)
                                                {
                                                    clv_aula = _AsignarAula(AulasBloques, posicionAula, ind_horario, clv_aula, clv_materia);
                                                    ind_horario++;
                                                }
                                                opc = true;//PRUEBA***********************+
                                            }
                                            break;
                                        }
                                        /* En caso de existir combinaciones se asignan dichos bloques*/
                                        else
                                        {
                                            for (int z = 0; z < superbloques[ind_sesion]; z++)//PRUEBA***********************+
                                            {
                                                clv_aula = _AsignarAula(AulasBloques, posicionAula, ind_horario, clv_aula, clv_materia);
                                                _AsignarBloque(ind_horario, clv_grupo, clv_docente, clv_aula, combinacion[z]);
                                                ind_horario++;//PRUEBA***********************+
                                            }
                                            opc = true;
                                        }
                                        
                                        break;//Se rompe el ciclo si ya se encontro la fila con la clave que se buscaba
                                    }
                                }

                                if (opc)//PRUEBA***********************+
                                    break;
                            }
                        }
                        /* Si el tamaño del superbloque no es mayor a uno entonces se usara un metodo que busque bloques que esten solos
                         * es decir que no tengan ningun otro adyacente*/
                        else
                        {
                            /*Se obtiene los bloques en comun entre grupo, docente y aula, usando la teoria de conjuntos
                             * y los bloques en solitario de la secuencia optenida*/
                            bloques_comunes = _BloquesLibres(clv_grupo, clv_docente, clv_aula, dia);
                            int[] solitos = _Buscar_BloquesSolos(bloques_comunes);

                            /*Se recorre la matriz que contendra la clave, aula y bloque, por ahora solo contiene la clave*/
                            for (int ind_horario = 0; ind_horario < _Horarios.GetLength(0); ind_horario++)
                            {
                                /*Ahora utilizamos el arreglo que contiene las claves de las filas del superindividuo en donde esta presente 
                                * la clave de la materia si la clave que se tiene en la matriz de los horarios coincide con la clave que 
                                * se esta usando y no se ha asignado ningun aula o bloque entonces....*/
                                if (_Horarios[ind_horario, 0] == claves_superindv[ind_clave] && _Horarios[ind_horario, 1] == -1 && _Horarios[ind_horario, 2] == -1)
                                {

                                    /*Se asigna el aula que se selecciono previamente*/
                                    clv_aula = _AsignarAula(AulasBloques, posicionAula, ind_horario, clv_aula, clv_materia);

                                    /* Si no se encontraron bloques en solitario entonces se recurre al cambio de dia 
                                     * como en el apartado anterior*/
                                    if (solitos == null)
                                    {
                                        if (!_ProbarOtroDia(dias_bloques, dia, aleatorio, ind_horario, superbloques[ind_sesion], clv_grupo, clv_docente, clv_aula))
                                        {
                                            int[] posibles = _OpcionesBloqueRandom(clv_grupo, clv_docente, clv_aula);
                                            valor_random = aleatorio.Next(0, posibles.Length);
                                            _AsignarBloque(ind_horario, clv_grupo, clv_docente, clv_aula, posibles[valor_random]);
                                            bloques_random++;
                                            posiciones.AddLast(ind_horario);
                                        }
                                    }
                                    else
                                    {
                                        /*Se asigan aleatoriamente cualquiera de los bloques en solitario encontrados*/
                                        valor_random = aleatorio.Next(0, solitos.Length);
                                        _AsignarBloque(ind_horario, clv_grupo, clv_docente, clv_aula, solitos[valor_random]);
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private int _AsignarAula(int[,] AulasBloques, int posicionAula, int ind_horario, int clv_aula, int clv_materia)
        {
            if (AulasBloques[posicionAula, 1] > 0)
            {
                _Horarios[ind_horario, 1] = clv_aula;
                AulasBloques[posicionAula, 1] = AulasBloques[posicionAula, 1] - 1;
            }
            else
            {
                _ActualizarAulasPosibles(clv_aula);
                posicionAula = _Busca_Posicion_AulaRandom(AulasBloques, _BuscaPosicionArray(_SuperIndv.Materias, clv_materia));
                clv_aula = AulasBloques[posicionAula, 0];
                _Horarios[ind_horario, 1] = clv_aula;
                AulasBloques[posicionAula, 1] = AulasBloques[posicionAula, 1] - 1;

                return clv_aula;
                //return _NuevaAsignacionAula(AulasBloques, ind_horario, clv_materia);
            }

            return clv_aula;
        }

        private void _AsignarBloque(int indice, int clv_grupo, int clv_docente, int clv_aula, int bloque)
        {
            _Horarios[indice, 2] = bloque;
            int pos_aula = _BuscaPosicionArray(_SuperIndv.Aulas, clv_aula);
            _MarcaBloque(_Horario_Docentes, _BuscaPosicionArray(_SuperIndv.Docentes, clv_docente), bloque);
            _MarcaBloque(_Horarios_Aulas, pos_aula, bloque);
            _MarcaBloque(_Horario_Grupos, _BuscaPosicionArray(_SuperIndv.Grupos, clv_grupo), bloque);
        }

        private int[] _OpcionesBloqueRandom(int clv_grupo, int clv_docente, int clv_aula)
        {
            LinkedList<int> posibles = new LinkedList<int>();
            LinkedList<int> bloques_grupo = _ExtraerColumna(_Horario_Grupos[_BuscaPosicionArray(_SuperIndv.Grupos, clv_grupo)], 1);
            LinkedList<int> bloques_docente = _ExtraerColumna(_Horario_Docentes[_BuscaPosicionArray(_SuperIndv.Docentes, clv_docente)], 1);
            LinkedList<int> bloques_aula = _ExtraerColumna(_Horarios_Aulas[_BuscaPosicionArray(_SuperIndv.Aulas, clv_aula)], 1);
            int valor;

            for (int i = 0; i < bloques_grupo.Count; i++)
            {
                valor = bloques_grupo.ElementAt(i);
                if(bloques_docente.Contains(valor) && bloques_aula.Contains(valor))
                {
                    posibles.AddLast(valor);
                }
            }

            return posibles.ToArray();
        }
        
        private LinkedList<int> _ExtraerColumna(int[,] matriz, int posicion_columna)
        {
            LinkedList<int> columna = new LinkedList<int>();

            for(int i = 0; i < matriz.GetLength(0); i++)
            {
                columna.AddLast(matriz[i, posicion_columna]);
            }

            return columna;
        }

        private bool _ProbarOtroDia(LinkedList<int[]> dias, int[] dia_anterior, Random aleatorio, int ind_horario, int tam_superbloque, int clv_grupo, int clv_docente, int clv_aula)
        {
            LinkedList<int[]> dias_copia = new LinkedList<int[]>();
            int[] bloques_en_comun;
            int dia = -1;
            int tam = dias.Count;

            for(int i = 0; i < dias.Count; i++)
            {
                dias_copia.AddLast(new int[dias.ElementAt(i).Length]);

                for (int j = 0; j < dias.ElementAt(i).Length; j++)
                {
                    dias_copia.ElementAt(i)[j] = dias.ElementAt(i)[j];
                }
            }

            //Este es para buscar bloques de tamaño 2 o mayor

            if (tam_superbloque > 1)
            {
                int[][] combinaciones;
                int[] combinacion;

                for (int i = 0; i < tam; i++)
                {
                    dia = aleatorio.Next(0, dias_copia.Count);
                    bloques_en_comun = _BloquesLibres(clv_grupo, clv_docente, clv_aula, dias_copia.ElementAt(dia));
                    combinaciones = _Buscar_BloquesAdyacentes(bloques_en_comun, tam_superbloque);
                    //Console.WriteLine(i);
                    if (combinaciones == null)
                    {
                        dias_copia.Remove(dias_copia.ElementAt(dia));
                        continue;
                    }

                    combinacion = combinaciones[aleatorio.Next(0, combinaciones.Length)];

                    for (int j = 0; j < combinacion.Length; j++)
                    {
                        _AsignarBloque(ind_horario, clv_grupo, clv_docente, clv_aula, combinacion[j]);
                        //Console.WriteLine("Posicion: " + ind_horario + ", Bloque: " + combinacion[j] + ", Aula: " + clv_aula);
                        ind_horario++;//    V E R I F I C A R
                        //Error se asigna un bloque en el mismo indice lo que proboca que otro asignado anteriroemnte quede marcado pero no visible en la matriz horarios
                    }

                    dias.Remove(dias.ElementAt(dia));
                    dias.AddLast(dia_anterior);
                    salvados++;
                    salvadospares++;
                    return true;
                }
            }
            else
            {
                int[] solos;

                for (int i = 0; i < tam; i++)
                {
                    dia = aleatorio.Next(0, dias_copia.Count);
                    bloques_en_comun = _BloquesLibres(clv_grupo, clv_docente, clv_aula, dias_copia.ElementAt(dia));
                    solos = _Buscar_BloquesSolos(bloques_en_comun);

                    if (solos == null)
                    {
                        dias_copia.Remove(dias_copia.ElementAt(dia));
                        continue;
                    }

                    _AsignarBloque(ind_horario, clv_grupo, clv_docente, clv_aula, solos[aleatorio.Next(0, solos.Length)]);
                    dias.Remove(dias.ElementAt(dia));
                    dias.AddLast(dia_anterior);
                    salvados++;
                    salvadossolo++;

                    return true;
                }
            }

            return false;

        }

        private void _MarcaBloque(int[][,]horario, int posicion, int bloque)
        {
            for (int indice = 0; indice < horario[posicion].GetLength(0); indice++)
            {
                if (bloque == horario[posicion][indice, 1])
                {
                    horario[posicion][indice, 2] = horario[posicion][indice, 2] + 1;
                    break;
                }
            }
        }

        private int _Busca_Posicion_AulaRandom(int[,] BloquesAulas, int posicion_materia)
        {
            int aula_posible = -1;
            LinkedList<int> lista = new LinkedList<int>(_SuperIndv.MateriasAulasPosibles[posicion_materia]);

            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);
            

            while (true)
            {
                int pos = aleatorio.Next(0, lista.Count);

                for (int indice = 0; indice < BloquesAulas.GetLength(0); indice++)
                {
                    aula_posible = lista.ElementAt(pos);
                    if (aula_posible == BloquesAulas[indice, 0])
                    {
                        if (BloquesAulas[indice, 1] > 0)
                            return indice;
                        else
                            lista.Remove(aula_posible);
                        break;
                    }
                }

                if (lista.Count == 0)
                    return -1;
           
            }
        }

        private void _ActualizarAulasPosibles(int clv_aula)//VERIFICAR CONSECUENCIAS
        {
            //Tomar en cuanta que cuando solo sea un aula posible no se actualiza
            LinkedList<int> aulas_posibles;
            int[] nuevas_aulas;

            for(int indice = 0; indice < _SuperIndv.MateriasAulasPosibles.Length; indice++)
            {
                aulas_posibles = new LinkedList<int>(_SuperIndv.MateriasAulasPosibles[indice]);
                if (aulas_posibles.Count > 1)
                {
                    aulas_posibles.Remove(clv_aula);
                    nuevas_aulas = new int[aulas_posibles.Count];
                    aulas_posibles.CopyTo(nuevas_aulas, 0);
                    _SuperIndv.MateriasAulasPosibles[indice] = nuevas_aulas;
                }
            }
        }

        private int _ObtenerGrupo(int clave)
        {
            for(int indice = 0; indice < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); indice++)
            {
                if (clave == _SuperIndv.ClaveGrupoMateriaDocente[indice, 0])
                    return _SuperIndv.ClaveGrupoMateriaDocente[indice, 1];
            }
            return -1;
        }

        private int _ObtenerDocente(int clave)
        {
            for (int indice = 0; indice < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); indice++)
            {
                if (clave == _SuperIndv.ClaveGrupoMateriaDocente[indice, 0])
                    return _SuperIndv.ClaveGrupoMateriaDocente[indice, 3];
            }
            return -1;
        }

        private int _BuscaPosicionArray(int[] array, int clave)
        {
            for (int indice = 0; indice < array.Length; indice++)
            {
                if (array[indice] == clave)
                    return indice;
            }
            return -1;
        }

        private int[] _BloquesLibres(int grupo, int docente, int aula, int[] bloques_dia)//Retorna un arreglo de los bloques vacios que tienen en comun los docentes, aulas y grupos
        {
            int posicion_grupo = _BuscaPosicionArray(_SuperIndv.Grupos, grupo);
            int posicion_docente = _BuscaPosicionArray(_SuperIndv.Docentes,docente);
            int posicion_aula = _BuscaPosicionArray(_SuperIndv.Aulas, aula);
            int bloque = -1;
            LinkedList<int> libres_grupo = _BloquesDisponibles(_Horario_Grupos[posicion_grupo]);
            LinkedList<int> libres_aula = _BloquesDisponibles(_Horarios_Aulas[posicion_aula]);
            LinkedList<int> libres_docente = _BloquesDisponibles(_Horario_Docentes[posicion_docente]);
            LinkedList<int> bloques_libres = new LinkedList<int>();

            for (int i = 0; i < bloques_dia.Length; i++)
            {
                bloque = bloques_dia[i];
                if (libres_grupo.Contains(bloque) && libres_aula.Contains(bloque) && libres_docente.Contains(bloque))
                    bloques_libres.AddLast(bloque);
            }

            #region CODIGO ANTERIOR
            //for (int ind_dia = 0; ind_dia < _SuperIndv.BloquesDias.Length; ind_dia++)
            //{
            //    for (int ind_bloque = 0; ind_bloque < _SuperIndv.BloquesDias[ind_dia].Length; ind_bloque++)
            //    {
            //        bloque = _SuperIndv.BloquesDias[ind_dia][ind_bloque];
            //        if (_Bloque_Disponible(_Horarios_Aulas[posicion_aula], bloque) && _Bloque_Disponible(_Horario_Grupos[posicion_grupo], bloque) && _Bloque_Disponible(_Horario_Docentes[posicion_docente], bloque))
            //        {
            //            bloques_libres.AddLast(_SuperIndv.BloquesDias[ind_dia][ind_bloque]);
            //        }
            //    }
            //}

            //if (bloques_libres.Count == 0)
            //    return null;

            //LinkedList<int> bloques_definitivos = new LinkedList<int>();

            //for (int indice = 0; indice < bloques_dia.Length; indice++)
            //{
            //    for (int indice2 = 0; indice2 < bloques_libres.Count; indice2++)
            //    {
            //        if (bloques_dia[indice] == bloques_libres.ElementAt(indice2))
            //        {
            //            bloques_definitivos.AddLast(bloques_libres.ElementAt(indice2));
            //            break;
            //        }
            //    }
            //}

            //if (bloques_definitivos.Count == 0)
            //    return null;
            #endregion

            if (bloques_libres.Count == 0)
                return null;

            return bloques_libres.ToArray();
        }

        private LinkedList<int> _BloquesDisponibles(int[,] _horario)
        {
            LinkedList<int> lista = new LinkedList<int>();

            for (int i = 0; i < _horario.GetLength(0); i++)
            {
                if (_horario[i, 2] == 0)
                {
                    lista.AddLast(_horario[i,1]);
                }
            }
            return lista;
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

            #region basura

            //for(int i = 1; i < bloques_disponibles.Length; i++)
            //{
            //    k = i - 1;
            //
            //    if (intentos == 0)
            //    {
            //        if (puntos == tam_superbloque - 1)
            //        {
            //            combinacion.AddLast(bloques_disponibles[k]);
            //            lista_combinaciones.AddLast(combinacion.ToArray<int>());
            //        }
            //        puntos = 0;
            //        intentos = tam_superbloque - 1;
            //        combinacion.Clear();
            //        i = puntero;
            //    }
            //
            //    if (intentos == tam_superbloque - 1)
            //    {
            //        puntero = i + 1;
            //    }
            //
            //    if (bloques_disponibles[i] - bloques_disponibles[k] == 1)
            //    {
            //        combinacion.AddLast(bloques_disponibles[k]);
            //        puntos++;
            //    }
            //
            //    intentos--;
            //}
            #endregion

            for (int i = 1; i < bloques_disponibles.Length; i++)
            {
                //Console.WriteLine("i = " + i);
                int j;
                for (j = i; j < bloques_disponibles.Length && intentos > 0; j++)
                {
                    //Console.WriteLine("j = " + j);
                    if(bloques_disponibles[j] - bloques_disponibles[j-1] == 1)
                    {
                        //l = j;
                        combinacion.AddLast(bloques_disponibles[j-1]);
                        puntos++;
                    }
                    intentos--;
                }

                if (puntos == tam_superbloque - 1)
                {
                    combinacion.AddLast(bloques_disponibles[j-1]);
                    lista_combinaciones.AddLast(combinacion.ToArray<int>());
                }
                puntos = 0;
                intentos = tam_superbloque - 1;
                combinacion.Clear();
            }

            if (lista_combinaciones.Count == 0)
                return null;

            array_combinaciones = new int[lista_combinaciones.Count][];
            lista_combinaciones.CopyTo(array_combinaciones,0);

            return array_combinaciones;
        }

        private int[] _Buscar_BloquesSolos(int[] bloques_disponibles)
        {
            if (bloques_disponibles == null)
                return null;

            LinkedList<int> lista_solos = new LinkedList<int>();
            int[] array_solos;
            int j = -1;
            for(int i = 1; i < bloques_disponibles.Length; i++)
            {
                j = i - 1;
                if (j != 0)
                {
                    if (bloques_disponibles[i] - bloques_disponibles[j] > 1 && bloques_disponibles[j] - bloques_disponibles[j - 1] > 1)
                    {
                        lista_solos.AddLast(bloques_disponibles[i - 1]);

                        if(i == bloques_disponibles.Length -1)
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

        #region Utilerias

        private int[] _OrdenaMaterias_Asc() //Ordena por Algoritmo de ordenamiento por seleccion primero van los que tienen menos salones disponibles
        {
            int tam = _SuperIndv.MateriasAulasPosibles.GetLength(0);
            int[] ordenadas = (int[])_SuperIndv.Materias.Clone();
            int[] cantidades = new int[tam];

            for (int indice = 0; indice < tam; indice++)
                cantidades[indice] = _SuperIndv.MateriasAulasPosibles[indice].Length;

            int indiceMenor, i, j;
            int n = cantidades.Length;
            for (i = 0; i < n - 1; i++)
            {
                indiceMenor = i;

                for (j = i + 1; j < n; j++)
                    if (cantidades[j] < cantidades[indiceMenor])
                        indiceMenor = j;

                if (i != indiceMenor)
                {
                    int aux = cantidades[i];
                    int aux2 = ordenadas[i];
                    cantidades[i] = cantidades[indiceMenor];
                    ordenadas[i] = ordenadas[indiceMenor];
                    cantidades[indiceMenor] = aux;
                    ordenadas[indiceMenor] = aux2;
                }
            }

            int indice_inicio;
            LinkedList<int> iguales = new LinkedList<int>();

            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));
            Random aleatorio = new Random(seed);
            int valor_random;

            for (int indice = 0; indice < cantidades.Length; indice++)
            {
                indice_inicio = -1;
                for (int indice2 = 0; indice2 < cantidades.Length; indice2++)
                {
                    if (cantidades[indice] == cantidades[indice2])
                    {
                        indice_inicio = indice2 - iguales.Count;
                        iguales.AddLast(ordenadas[indice2]);
                    }
                }

                if (iguales.Count > 1)
                {
                    int[] conjunto = new int[iguales.Count];
                    for (int indice3 = 0; indice3 < conjunto.Length; indice3++)
                    {
                        valor_random = aleatorio.Next(0, iguales.Count);
                        conjunto[indice3] = iguales.ElementAt(valor_random);
                        iguales.Remove(iguales.ElementAt(valor_random));
                    }
                    conjunto.CopyTo(ordenadas, indice_inicio);
                }
            }

            //for(int k = 0; k < cantidades.Length; k++)
            //{
            //    Console.WriteLine("Materia: " + ordenadas[k]);
            //    Console.WriteLine("Cantidad: " + cantidades[k]);
            //}

            return ordenadas;
        }

        private int[] _ObtenerClavesSuperIndividuo(int materia)
        {
            int indice_claves = 0;
            int[] claves = new int[1];
            for (int indice2 = 0; indice2 < _SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); indice2++)
            {
                if (materia == _SuperIndv.ClaveGrupoMateriaDocente[indice2, 2])
                {
                    claves[indice_claves] = _SuperIndv.ClaveGrupoMateriaDocente[indice2, 0];
                    Array.Resize(ref claves, claves.Length + 1);
                    indice_claves++;
                }
            }

            Array.Resize(ref claves, claves.Length - 1);

            return claves;
        }

        #endregion
        
        public void imprimirIndividuo()
        {
            for (int x = 0; x < GSuperIndividuo._tamIndividuo; x++)
            {
                Console.WriteLine(_Horarios[x, 0] + " " + _Horarios[x, 1] + " " + _Horarios[x, 2]);
            }
        }

    }
}

