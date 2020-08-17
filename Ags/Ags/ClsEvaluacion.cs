using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ags
{
    class ClsEvaluacion
    {
        public static double[] value = { 0.28, 0.24, 0.22, 0.10, 0.09, 0.07 };

        public static int[] getArregloResultados(AbsClsIndividuo obj)
        {
            int[] resultados = new int[6];

            resultados[0] = ChoquesHorario(obj._Horario_Grupos);
            resultados[1] = ChoquesHorario(obj._Horarios_Aulas);
            resultados[2] = ChoquesHorario(obj._Horario_Docentes);
            resultados[3] = HuecosHorariosGrupo(obj._Horario_Grupos, obj._SuperIndv.DatosBloques, obj._SuperIndv.BloquesReceso);
            resultados[4] = HorasConsecutivas(obj._Horarios, obj._SuperIndv.ClaveGrupoMateriaDocente, obj._SuperIndv.BloquesDias, obj._SuperIndv.Grupos, obj._SuperIndv.Materias);
            resultados[5] = SalonRegular(obj._Horarios, obj._SuperIndv.ClaveGrupoMateriaDocente);

            return resultados;
        }

        public static double fitness(AbsClsIndividuo obj)
        {
            double ValorTotal = 0;

            int[] resultados = getArregloResultados(obj);

            for (int i = 0; i < resultados.GetLength(0); i++)
            {
                ValorTotal += resultados[i] * value[i];
            }

            return ValorTotal;
        }
        //public static double[,] fitnessforForm(AbsClsIndividuo obj)
        //{
        //    double[,] resultados = new double[6, 3];

        //    resultados[0, 0] = ChoquesHorario(obj._Horario_Grupos);
        //    resultados[1, 0] = ChoquesHorario(obj._Horarios_Aulas);
        //    resultados[2, 0] = ChoquesHorario(obj._Horario_Docentes);
        //    resultados[3, 0] = HuecosHorariosGrupo(obj._Horario_Grupos, obj._SuperIndv.DatosBloques, obj._SuperIndv.BloquesReceso);
        //    resultados[4, 0] = HorasConsecutivas(obj._Horarios, obj._SuperIndv.ClaveGrupoMateriaDocente, obj._SuperIndv.BloquesDias, obj._SuperIndv.Grupos, obj._SuperIndv.Materias);
        //    resultados[5, 0] = SalonRegular(obj._Horarios, obj._SuperIndv.ClaveGrupoMateriaDocente);

        //    resultados[0, 1] = 0.28;
        //    resultados[1, 1] = 0.24;
        //    resultados[2, 1] = 0.22;
        //    resultados[3, 1] = 0.10;
        //    resultados[4, 1] = 0.09;
        //    resultados[5, 1] = 0.07;

        //    resultados[0, 2] = resultados[0, 0] * resultados[0, 1];
        //    resultados[1, 2] = resultados[1, 0] * resultados[1, 1];
        //    resultados[2, 2] = resultados[2, 0] * resultados[2, 1];
        //    resultados[3, 2] = resultados[3, 0] * resultados[3, 1];
        //    resultados[4, 2] = resultados[4, 0] * resultados[4, 1];
        //    resultados[5, 2] = resultados[5, 0] * resultados[5, 1];

        //    return resultados;
        //}

        private static int ChoquesHorario(int[][,] horario)
        {
            int tamHorario = horario.GetLength(0);
            int tamHorarioGral;
            int choques = 0;
            for (int i = 0; i < tamHorario; i++)
            {
                tamHorarioGral = horario[i].GetLength(0);
                for (int j = 0; j < tamHorarioGral; j++)
                {
                    if (horario[i][j, 2] > 1)
                    {
                        choques = choques + (horario[i][j, 2] - 1);
                    }
                }
            }
            return choques;
        }

        private static int HorasConsecutivas(int[,] Horario, int[,] ClvGrpoMatDoc, int[][] BloquesDias, int[] Grupos, int[] materias)
        {
            int resultado = 0;
            int[,] horarioDia = null;
            int Clvgrupo = 0;
            int bloque = 0, materia = 0;
            int renglon = 0;
            for (int grupo = 0; grupo < Grupos.GetLength(0); grupo++) // recorre por grupos
            {
                Clvgrupo = Grupos[grupo];
                for (int dia = 0; dia < BloquesDias.GetLength(0); dia++) // recorre por días
                {
                    horarioDia = new int[BloquesDias[dia].GetLength(0), 2];
                    for (int hora = 0; hora < BloquesDias[dia].GetLength(0); hora++) // recorre por horas
                    {
                        bloque = BloquesDias[dia][hora]; // obtiene el bloque según día y hora
                        for (int i = 0; i < Horario.GetLength(0); i++) // recorre el horario
                        {
                            if (bloque == Horario[i, 2]) // encuentra el bloque del día y hora en el horario
                            {
                                renglon = Horario[i, 0];
                                for (int j = 0; j < ClvGrpoMatDoc.GetLength(0); j++) // según el renglón busca en la ClvGrupoMatDocente
                                {
                                    if (ClvGrpoMatDoc[j, 0] == renglon && Clvgrupo == ClvGrpoMatDoc[j, 1]) // compara que el renglón y el grupo sean correctos
                                    {
                                        // crea el horario semanal del grupo
                                        materia = ClvGrpoMatDoc[j, 2];
                                        horarioDia[hora, 0] = materia;
                                        horarioDia[hora, 1] = bloque;
                                    }
                                }
                            }
                        }
                    }
                    resultado += RecorreHorasConsecutivas(horarioDia, materias); // envía el horario y regresa los bloques que se dan dos veces y no seguidas
                }
            }
            return resultado;
        }
        private static int RecorreHorasConsecutivas(int[,] horarioDia, int[] materias)
        {
            int resultado = 0;
            int[] bloquesDia;
            int materia = 0;
            int vecesMateria = 0;
            for (int i = 0; i < materias.Length; i++) // recorre el horario que recibe
            {
                vecesMateria = 0;
                materia = materias[i];
                for (int j = 0; j < horarioDia.GetLength(0); j++) // recorre el horario que recibe
                {
                    if (horarioDia[j, 0] == materia) // cuenta las veces que se da una materia en el día
                    {
                        vecesMateria++;
                    }
                }
                if (vecesMateria > 1)
                {
                    int indice = 0;
                    bloquesDia = new int[vecesMateria];
                    for (int j = 0; j < horarioDia.GetLength(0); j++) // recorre el horario que recibe
                    {
                        if (horarioDia[j, 0] == materia)
                        {
                            bloquesDia[indice] = horarioDia[j, 1]; // guarda en el arreglo todos los bloques de esa materia
                            indice++;
                        }
                    }
                    Array.Sort(bloquesDia); // ordena los bloques
                    if (!bloquesDia.Zip(bloquesDia.Skip(1), (a, b) => (a + 1) == b).All(x => x))
                    {
                        resultado++;
                    }
                }
            }
            return resultado;
        }

        private static int HuecosHorariosGrupo(int[][,] horario, int[][] bloques, int[] bloquesReceso)
        {
            int[,] horarioDiario;
            int inicio = -1, final = -1;
            int huecos = 0;
            for (int i = 0; i < horario.GetLength(0); i++) //recorre por grupos
            {
                for (int m = 0; m < bloques.GetLength(0); m++) // recorre por días
                {
                    horarioDiario = new int[bloques[m].GetLength(0), 2];
                    for (int n = 0; n < bloques[m].GetLength(0); n++) // recorre por horas
                    {
                        horarioDiario[n, 0] = bloques[m][n];
                        for (int j = 0; j < horario[i].GetLength(0); j++) // busca el bloque según día y hora y guarda si está ocupado
                        {
                            if (bloques[m][n] == horario[i][j, 1])
                            {
                                horarioDiario[n, 1] = horario[i][j, 2];
                                break;
                            }
                        }
                    }
                    for (int ini = 0; ini < horarioDiario.GetLength(0); ini++) // obtiene el inicio
                    {
                        if (horarioDiario[ini, 1] > 0)
                        {
                            inicio = horarioDiario[ini, 0];
                            break;
                        }
                    }
                    for (int fin = horarioDiario.GetLength(0) - 1; fin > 0; fin--) // obtiene el final
                    {
                        if (horarioDiario[fin, 1] > 0)
                        {
                            final = horarioDiario[fin, 0];
                            break;
                        }
                    }
                    huecos += RecorreHuecosDiarios(horarioDiario, inicio, final, bloquesReceso[m]);
                }
            }
            return huecos;
        }
        private static int RecorreHuecosDiarios(int[,] horarioDiario, int inicio, int final, int bloqueReceso)
        {
            int huecos = 0;
            int bloque = 0;
            for (int j = 0; j < horarioDiario.GetLength(0); j++) // recorre el horario del día
            {
                bloque = horarioDiario[j, 0];
                if (bloque >= inicio && bloque <= final) // verifica que esté después del inicio y antes del final
                {
                    if (horarioDiario[j, 1] == 0 && bloque + 1 != bloqueReceso && bloque - 1 != bloqueReceso && bloque != bloqueReceso)
                    {
                        huecos++;
                    }
                }
            }
            return huecos;
        }

        private static int SalonRegular(int[,] horario, int[,] _ClaveGrupoMateriaDocente)
        {
            int resultado = 0;
            int renglon = 0, salon = 0;
            int tamHorario = _ClaveGrupoMateriaDocente.GetLength(0);
            for (int i = 0; i < tamHorario; i++)
            {
                renglon = _ClaveGrupoMateriaDocente[i, 0];
                salon = -1;
                for (int j = 0; j < horario.GetLength(0); j++)
                {
                    if (renglon == horario[j, 0])
                    {
                        if (salon == -1)
                        {
                            salon = horario[j, 1];
                        }
                        else
                        {
                            if (salon != horario[j, 1])
                            {
                                resultado++;
                                break;
                            }
                        }
                    }
                }
            }
            return resultado;
        }
    }
}
