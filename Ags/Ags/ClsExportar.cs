using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ags
{
    class ClsExportar
    {
        private static int IndiceHoja = 0;
        private static int TotalHoras = 0;

        private static string[] Letras = { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static void ExportarHorario(ClsIndividuo obj, FormExportar frm)
        {
            try
            {
                Console.WriteLine("Exportando Horario a Excel");

                Excel.Application AppExcel;            // Inicializa la variable para la Aplicación de Excel
                Excel.Workbook workBook;               // Inicializa la variable para los libros de Excel
                Excel.Worksheet workSheet;             // Inicializa la variable para las hojas de Excel

                object opc = Type.Missing;
                AppExcel = new Excel.Application();
                AppExcel.Visible = false;
                workBook = AppExcel.Workbooks.Add();

                int TotalHojas = obj._SuperIndv.Aulas.GetLength(0) + obj._SuperIndv.Docentes.GetLength(0) + obj._SuperIndv.Grupos.GetLength(0);

                for (int i = 1; i < TotalHojas; i++)    // Agrega Libros según las hojas necesarias
                {
                    workBook.Worksheets.Add(opc);
                }

                for (int i = 0; i < obj._SuperIndv.BloquesDias.GetLength(0); i++)
                {
                    for (int j = 0; j < obj._SuperIndv.BloquesDias[i].GetLength(0); j++)
                    {
                        TotalHoras++;
                    }
                }

                IndiceHoja = 0;
                workSheet = workBook.Worksheets[1];

                int MaxProgreso = obj._SuperIndv.Aulas.Length + obj._SuperIndv.Grupos.Length + obj._SuperIndv.Docentes.Length;
                frm.Invoke(new MethodInvoker(delegate () { frm.Components(MaxProgreso); }));

                ExportarHorarioGrupos(workBook, workSheet, obj, frm);
                ExportarHorarioDocente(workBook, workSheet, obj, frm);
                ExportarHorarioAula(workBook, workSheet, obj, frm);

                string path = "";
                frm.Invoke(new MethodInvoker(delegate () { path = frm.ProcesoTerminado(); }));              
                workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal);
                workBook.Close(true);
                AppExcel.Quit();

                Console.WriteLine("Horario Exportado con Éxito");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al exportar la informacion debido a: " + ex.ToString());
            }
        }

        private static void ExportarHorarioGrupos(Excel.Workbook workBook, Excel.Worksheet workSheet, ClsIndividuo obj, FormExportar frm)
        {
            for (int grupo = 0; grupo < obj._SuperIndv.Grupos.GetLength(0); grupo++)    // se ejecuta según la cantidad de grupos que haya
            {
                Console.WriteLine(" G R U P O : " + grupo);
                int[,] Horario;
                string[,] HorarioCompleto;
                string nombreHoja = "";
                int bloqueColocar = 0;
                string letra = "";
                int numero = 0;
                int bloqueReceso = 0;
                IndiceHoja++;

                workSheet = workBook.Worksheets[IndiceHoja];   // Se posiciona en la hoja según el índice

                char[] nombreLetras = obj._SuperIndv.DatosGrupos[grupo, 1].ToArray();
                for (int n = 0; n < obj._SuperIndv.DatosGrupos.GetLength(0); n++)
                {
                    if (Int32.Parse(obj._SuperIndv.DatosGrupos[n, 0]) == obj._SuperIndv.Grupos[grupo])
                    {
                        nombreLetras = obj._SuperIndv.DatosGrupos[n, 1].ToArray();
                        if (nombreLetras.Length >= 30)   // por si el nombre sobrepasa los 30 caracteres
                        {
                            for (int i = 0; i < 30; i++)
                            {
                                nombreHoja += nombreLetras[i];
                            }
                        }
                        break;
                    }
                }
                for (int n = 0; n < nombreLetras.Length; n++)
                {
                    nombreHoja += nombreLetras[n];
                }

                workSheet.Name = nombreHoja;   //Asigna el Nombre a las hojas
                frm.Invoke(new MethodInvoker(delegate () { frm.Actualizar(nombreHoja); }));

                ImprimeEncabezadoHoras(workSheet, obj._SuperIndv.Horas, obj._SuperIndv.Dias);

                Horario = HorarioGrupo(obj._SuperIndv.Grupos[grupo], obj, workBook, workSheet);
                HorarioCompleto = CompletarHorarioGrupo(Horario, obj._SuperIndv.DatosMaterias, obj._SuperIndv.DatosAula, obj._SuperIndv.DatosDocente);

                for (int i = 0; i < obj._SuperIndv.DatosBloques.GetLength(0); i++) // recorre por días
                {
                    letra = Letras[i + 1]; // letra en la que se va a imprimir
                    bloqueReceso = obj._SuperIndv.BloquesReceso[i]; // obtiene el bloque de receso del día
                    numero = 4; // número desde el cual se comenzará a imprimir
                    for (int j = 0; j < obj._SuperIndv.DatosBloques[i].GetLength(0); j++) // recorre por horas
                    {
                        bloqueColocar = obj._SuperIndv.DatosBloques[i][j]; // obtiene el bloque a colocar en 
                        if (bloqueColocar == bloqueReceso)
                        {
                            numero++;
                        }
                        else
                        {
                            for (int m = 0; m < HorarioCompleto.GetLength(0); m++)
                            {
                                if (bloqueColocar == Int32.Parse(HorarioCompleto[m, 0]))
                                {
                                    workSheet.Cells[numero, letra] = HorarioCompleto[m, 1] + "\r\n" + HorarioCompleto[m, 2] + "\r\n" + HorarioCompleto[m, 3];
                                }
                            }
                            workSheet.Cells[numero, letra].Font.Size = 8;   // Tamaño de la letra
                            workSheet.Cells[numero, letra].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;   //Bordes
                            workSheet.Cells[numero, letra].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Vertical
                            workSheet.Cells[numero, letra].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Horizontal
                            numero++;
                        }
                    }
                }
            }
        }
        private static void ExportarHorarioDocente(Excel.Workbook workBook, Excel.Worksheet workSheet, ClsIndividuo obj, FormExportar frm)
        {
            for (int docente = 0; docente < obj._SuperIndv.Docentes.GetLength(0); docente++)    // se ejecuta según la cantidad de docentes que haya
            {
                Console.WriteLine(" D O C E N T E : " + docente);
                int[,] Horario;
                string[,] HorarioCompleto;
                string nombreHoja = "";
                int bloqueColocar = 0;
                string letra = "";
                int numero = 0;
                int bloqueReceso = 0;
                IndiceHoja++;

                workSheet = workBook.Worksheets[IndiceHoja];   // Se posiciona en la hoja según el índice

                nombreHoja = NombreDocente(obj._SuperIndv.DatosDocente[docente, 1]);
                workSheet.Name = nombreHoja;   //Asigna el Nombre a las hojas

                frm.Invoke(new MethodInvoker(delegate () { frm.Actualizar(nombreHoja); }));

                ImprimeEncabezadoHoras(workSheet, obj._SuperIndv.Horas, obj._SuperIndv.Dias);

                Horario = HorarioDocente(obj._SuperIndv.Docentes[docente], obj, workBook, workSheet);
                HorarioCompleto = CompletarHorarioDocente(Horario, obj._SuperIndv.DatosMaterias, obj._SuperIndv.DatosGrupos, obj._SuperIndv.DatosAula);

                for (int m = 0; m < Horario.GetLength(0); m++)
                {
                    Console.WriteLine(Horario[m, 0] + " - " + Horario[m, 1] + " - " + Horario[m, 2] + " - " + Horario[m, 3]);
                }

                for (int i = 0; i < obj._SuperIndv.DatosBloques.GetLength(0); i++)
                {
                    letra = Letras[i + 1];
                    bloqueReceso = obj._SuperIndv.BloquesReceso[i];
                    numero = 4;
                    for (int j = 0; j < obj._SuperIndv.DatosBloques[i].GetLength(0); j++)
                    {
                        bloqueColocar = obj._SuperIndv.DatosBloques[i][j];
                        if (bloqueColocar == bloqueReceso)
                        {
                            numero++;
                        }
                        else
                        {
                            for (int m = 0; m < HorarioCompleto.GetLength(0); m++)
                            {
                                if (bloqueColocar == Int32.Parse(HorarioCompleto[m, 0]))
                                {
                                    workSheet.Cells[numero, letra] = HorarioCompleto[m, 0] + "\r\n" + HorarioCompleto[m, 1] + "\r\n" + HorarioCompleto[m, 2] + "\r\n" + HorarioCompleto[m, 3];
                                }
                            }
                            workSheet.Cells[numero, letra].Font.Size = 8;   // Tamaño de la letra
                            workSheet.Cells[numero, letra].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;   //Bordes
                            workSheet.Cells[numero, letra].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Vertical
                            workSheet.Cells[numero, letra].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Horizontal
                            numero++;
                        }
                    }
                }
            }
        }
        private static void ExportarHorarioAula(Excel.Workbook workBook, Excel.Worksheet workSheet, ClsIndividuo obj, FormExportar frm)
        {            
            for (int aula = 0; aula < obj._SuperIndv.Aulas.GetLength(0); aula++)    // se ejecuta según la cantidad de aulas que haya
            {
                Console.WriteLine(" A U L A : " + aula);
                int[,] Horario;
                string[,] HorarioCompleto;
                string nombreHoja = "";

                int bloqueColocar = 0;
                string letra = "";
                int numero = 0;
                int bloqueReceso = 0;
                IndiceHoja++;

                workSheet = workBook.Worksheets[IndiceHoja];   // Se posiciona en la hoja según el índice

                char[] nombreLetras = obj._SuperIndv.DatosAula[aula, 1].ToArray();
                for (int n = 0; n < obj._SuperIndv.DatosAula.GetLength(0); n++)
                {
                    if (Int32.Parse(obj._SuperIndv.DatosAula[n, 0]) == obj._SuperIndv.Aulas[aula])
                    {
                        nombreLetras = obj._SuperIndv.DatosAula[n, 1].ToArray();
                        if (nombreLetras.Length >= 30)   // por si el nombre sobrepasa los 30 caracteres
                        {
                            for (int i = 0; i < 30; i++)
                            {
                                nombreHoja += nombreLetras[i];
                            }
                        }
                        break;
                    }
                }
                for (int n = 0; n < nombreLetras.Length; n++)
                {
                    nombreHoja += nombreLetras[n];
                }

                workSheet.Name = nombreHoja;   //Asigna el Nombre a las hojas
                frm.Invoke(new MethodInvoker(delegate () { frm.Actualizar(nombreHoja); }));

                ImprimeEncabezadoHoras(workSheet, obj._SuperIndv.Horas, obj._SuperIndv.Dias);

                int aux = obj._SuperIndv.Aulas[aula] - 1;

                Horario = HorarioAula((obj._SuperIndv.Aulas[aula]), obj, workBook, workSheet);
                HorarioCompleto = CompletarHorarioAula(Horario, obj._SuperIndv.DatosDocente, obj._SuperIndv.DatosGrupos, obj._SuperIndv.DatosMaterias);
                //HorarioCompleto = CompletarHorarioAula(Horario, obj.DatosAula, obj.DatosGrupo, obj.DatosMaterias);

                for (int m = 0; m < Horario.GetLength(0); m++)
                {
                    Console.WriteLine(Horario[m, 0] + " - " + Horario[m, 1] + " - " + Horario[m, 2] + " - " + Horario[m, 3]);
                }

                for (int i = 0; i < obj._SuperIndv.DatosBloques.GetLength(0); i++)
                {
                    letra = Letras[i + 1];
                    bloqueReceso = obj._SuperIndv.BloquesReceso[i];
                    numero = 4;
                    for (int j = 0; j < obj._SuperIndv.DatosBloques[i].GetLength(0); j++)
                    {
                        bloqueColocar = obj._SuperIndv.DatosBloques[i][j];
                        if (bloqueColocar == bloqueReceso)
                        {
                            numero++;
                        }
                        else
                        {
                            for (int m = 0; m < HorarioCompleto.GetLength(0); m++)
                            {
                                if (bloqueColocar == Int32.Parse(HorarioCompleto[m, 0]))
                                {
                                    workSheet.Cells[numero, letra] = HorarioCompleto[m, 0] + "\r\n" + HorarioCompleto[m, 1] + "\r\n" + HorarioCompleto[m, 2] + "\r\n" + HorarioCompleto[m, 3];
                                }
                            }
                            workSheet.Cells[numero, letra].Font.Size = 8;   // Tamaño de la letra
                            workSheet.Cells[numero, letra].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;   //Bordes
                            workSheet.Cells[numero, letra].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Vertical
                            workSheet.Cells[numero, letra].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Horizontal
                            numero++;
                        }
                    }
                }
            }
        }

        // GRUPOS
        private static int[,] HorarioGrupo(int clvGrupo, ClsIndividuo obj, Excel.Workbook workBook, Excel.Worksheet workSheet)
        {
            int[,] Horario = new int[TotalHoras, 4];
            int indiceHorario = 0;
            int renglon = 0;
            for (int i = 0; i < obj._SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                renglon = obj._SuperIndv.ClaveGrupoMateriaDocente[i, 0];
                if (obj._SuperIndv.ClaveGrupoMateriaDocente[i, 1] == clvGrupo)
                {
                    for (int j = 0; j < obj._Horarios.GetLength(0); j++)
                    {
                        if (obj._Horarios[j, 0] == renglon)
                        {
                            Horario[indiceHorario, 0] = obj._Horarios[j, 2];                    // bloque
                            Horario[indiceHorario, 1] = obj._SuperIndv.ClaveGrupoMateriaDocente[i, 2];    // materia
                            Horario[indiceHorario, 2] = obj._Horarios[j, 1];                    // aula
                            Horario[indiceHorario, 3] = obj._SuperIndv.ClaveGrupoMateriaDocente[i, 3];    // docente
                            indiceHorario++;
                        }
                    }
                }
            }
            //Horario = OrdenarHorario(Horario);
            return Horario;
        }
        private static string[,] CompletarHorarioGrupo(int[,] Horario, string[,] DatosMateria, string[,] DatosAula, string[,] DatosDocente)
        {
            string[,] HorarioCompleto = new string[TotalHoras, 4];
            for (int i = 0; i < Horario.GetLength(0); i++)
            {
                HorarioCompleto[i, 0] = Horario[i, 0].ToString();                        // bloque
                HorarioCompleto[i, 1] = ClaveDatoMateria(Horario[i, 1], DatosMateria);   // materia
                HorarioCompleto[i, 2] = ClaveDatoAula(Horario[i, 2], DatosAula);         // salón
                HorarioCompleto[i, 3] = ClaveDatoDocente(Horario[i, 3], DatosDocente);   // docente
            }
            return HorarioCompleto;
        }

        // DOCENTES
        private static int[,] HorarioDocente(int clvDocente, ClsIndividuo obj, Excel.Workbook workBook, Excel.Worksheet workSheet)
        {
            int[,] Horario = new int[TotalHoras, 4];
            int indiceHorario = 0;
            int renglon = 0;
            for (int i = 0; i < obj._SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); i++)
            {
                renglon = obj._SuperIndv.ClaveGrupoMateriaDocente[i, 0];
                if (obj._SuperIndv.ClaveGrupoMateriaDocente[i, 3] == clvDocente)
                {
                    for (int j = 0; j < obj._Horarios.GetLength(0); j++)
                    {
                        if (obj._Horarios[j, 0] == renglon)
                        {
                            Horario[indiceHorario, 0] = obj._Horarios[j, 2];                    // bloque
                            Horario[indiceHorario, 1] = obj._SuperIndv.ClaveGrupoMateriaDocente[i, 2];    // materia
                            Horario[indiceHorario, 2] = obj._SuperIndv.ClaveGrupoMateriaDocente[i, 1];    // grupo
                            Horario[indiceHorario, 3] = obj._Horarios[j, 1];                    // aula
                            indiceHorario++;
                        }
                    }
                }
            }
            //Horario = OrdenarHorario(Horario);
            return Horario;
        }
        private static string[,] CompletarHorarioDocente(int[,] Horario, string[,] DatosMateria, string[,] DatosGrupo, string[,] DatosAula)
        {
            string[,] HorarioCompleto = new string[TotalHoras, 4];
            for (int i = 0; i < Horario.GetLength(0); i++)
            {
                HorarioCompleto[i, 0] = Horario[i, 0].ToString();
                HorarioCompleto[i, 1] = ClaveDatoMateria(Horario[i, 1], DatosMateria);
                HorarioCompleto[i, 2] = ClaveDatoGrupo(Horario[i, 2], DatosGrupo);
                HorarioCompleto[i, 3] = ClaveDatoAula(Horario[i, 3], DatosAula);
            }
            return HorarioCompleto;
        }
        private static string NombreDocente(string nombreCompleto)
        {
            string nombreDocente = "";
            string[] nombreSplit = nombreCompleto.Split();
            char[] nombreChar;
            for (int i = 1; i <= 3; i++)
            {
                nombreChar = nombreSplit[i].ToArray();
                if (i == 1)
                {
                    if (nombreChar.Length >= 5)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            nombreDocente += nombreChar[m];
                        }
                    }
                    else
                    {
                        for (int m = 0; m < nombreChar.Length; m++)
                        {
                            nombreDocente += nombreChar[m];
                        }
                    }
                }
                else if (i == 2)
                {
                    if (nombreChar.Length >= 3)
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            nombreDocente += nombreChar[m];
                        }
                    }
                    else
                    {
                        for (int m = 0; m < nombreChar.Length; m++)
                        {
                            nombreDocente += nombreChar[m];
                        }
                    }
                }
                else if (i == 3)
                {
                    nombreDocente += nombreChar[0];
                }
            }
            return nombreDocente;
        }

        // AULA 
        private static int[,] HorarioAula(int clvAula, ClsIndividuo obj, Excel.Workbook workBook, Excel.Worksheet workSheet)
        {
            int[,] Horario = new int[TotalHoras, 4];
            int indiceHorario = 0;
            int renglon = 0;
            for (int i = 0; i < obj._Horarios.GetLength(0); i++)
            {
                renglon = obj._Horarios[i, 0];
                Console.WriteLine("renglon: " + renglon);
                if (obj._Horarios[i, 1] == clvAula)
                {
                    for (int j = 0; j < obj._SuperIndv.ClaveGrupoMateriaDocente.GetLength(0); j++)
                    {
                        if (obj._SuperIndv.ClaveGrupoMateriaDocente[j, 0] == renglon)
                        {
                            Horario[indiceHorario, 0] = obj._Horarios[i, 2];                    // bloque
                            //Horario[indiceHorario, 1] = obj._Horarios[i, 1];                    // aula
                            Horario[indiceHorario, 1] = obj._SuperIndv.ClaveGrupoMateriaDocente[j, 3];    // docente
                            Horario[indiceHorario, 2] = obj._SuperIndv.ClaveGrupoMateriaDocente[j, 1];    // grupo
                            Horario[indiceHorario, 3] = obj._SuperIndv.ClaveGrupoMateriaDocente[j, 2];    // materia
                            indiceHorario++;
                        }
                    }
                }
            }
            return Horario;
        }
        private static string[,] CompletarHorarioAula(int[,] Horario, string[,] DatosDocente, string[,] DatosGrupo, string[,] DatosMateria)
        {
            string[,] HorarioCompleto = new string[TotalHoras, 4];
            for (int i = 0; i < Horario.GetLength(0); i++)
            {
                HorarioCompleto[i, 0] = Horario[i, 0].ToString();
                //HorarioCompleto[i, 1] = ClaveDatoAula(Horario[i, 1], DatosDocente);
                HorarioCompleto[i, 1] = ClaveDatoDocente(Horario[i, 1], DatosDocente);
                HorarioCompleto[i, 2] = ClaveDatoGrupo(Horario[i, 2], DatosGrupo);
                HorarioCompleto[i, 3] = ClaveDatoMateria(Horario[i, 3], DatosMateria);
            }
            return HorarioCompleto;
        }

        // GENERAL
        private static string ClaveDatoMateria(int clave, string[,] DatosMateria)
        {
            string Dato = "";
            for (int i = 0; i < DatosMateria.GetLength(0); i++)
            {
                if (Int32.Parse(DatosMateria[i, 0]) == clave)
                {
                    Dato = DatosMateria[i, 1];
                    return Dato;
                }
            }
            return Dato;
        }
        private static string ClaveDatoAula(int clave, string[,] DatosAula)
        {
            string Dato = "";
            for (int i = 0; i < DatosAula.GetLength(0); i++)
            {
                if (Int32.Parse(DatosAula[i, 0]) == clave)
                {
                    Dato = DatosAula[i, 1];
                    return Dato;
                }
            }
            return Dato;
        }
        private static string ClaveDatoDocente(int clave, string[,] DatosDocente)
        {
            string Dato = "";
            for (int i = 0; i < DatosDocente.GetLength(0); i++)
            {
                if (Int32.Parse(DatosDocente[i, 0]) == clave)
                {
                    Dato = DatosDocente[i, 1];
                    return Dato;
                }
            }
            return Dato;
        }
        private static string ClaveDatoGrupo(int clave, string[,] DatosGrupo)
        {
            string Dato = "";
            for (int i = 0; i < DatosGrupo.GetLength(0); i++)
            {
                if (Int32.Parse(DatosGrupo[i, 0]) == clave)
                {
                    Dato = DatosGrupo[i, 1];
                    return Dato;
                }
            }
            return Dato;
        }

        // FORMATO
        private static void ImprimeEncabezadoHoras(Excel.Worksheet workSheet, string[,] Horas, string[,] Dias)
        {
            int numero = 3;
            for (int i = 0; i < Dias.GetLength(0); i++)
            {
                workSheet.Cells[numero, Letras[i + 1]] = Dias[i, 1];
                workSheet.Cells[numero, Letras[i + 1]].Font.Size = 11;   // Tamaño de la letra
                workSheet.Cells[numero, Letras[i + 1]].Font.Bold = 1;   // Letra con negritas
                workSheet.Cells[numero, Letras[i + 1]].ColumnWidth = 19;   // Ancho de la columna
                workSheet.Cells[numero, Letras[i + 1]].Interior.ColorIndex = 48;   // Color de la celda
                workSheet.Cells[numero, Letras[i + 1]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Horizontal
                workSheet.Cells[numero, Letras[i + 1]].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Vertical
                workSheet.Cells[numero, Letras[i + 1]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;   // Borde
            }
            string letra = "B";
            for (int j = 0; j < Horas.GetLength(0); j++)
            {
                workSheet.Cells[j + 4, letra] = Horas[j, 1];
                workSheet.Cells[j + 4, letra].Font.Size = 11;   // Tamaño de la letra
                workSheet.Cells[j + 4, letra].Font.Bold = 1;   // Letra con Negritas
                workSheet.Cells[j + 4, letra].ColumnWidth = 19;   // Ancho de la columna
                workSheet.Cells[j + 4, letra].RowHeight = 65.5;   // Alto de la Columna
                workSheet.Cells[j + 4, letra].Interior.ColorIndex = 48;   // Color de la celda
                workSheet.Cells[j + 4, letra].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Horizonral
                workSheet.Cells[j + 4, letra].VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;   // Alineación Vertical
                workSheet.Cells[j + 4, letra].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;   //Bordes
            }
        }
    }
}
