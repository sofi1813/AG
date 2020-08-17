using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ags
{
    static class ClsTrataJSON
    {
        public static void Individuo_JSON(int[,] ClvGrpMatDoc, int[,] horarios, int turno, string path)
        {
            var Indv = new ClsEstructura
            {
                SuperIndv = ClvGrpMatDoc,
                Horarios = horarios,
                Turno = turno
            };

            String json = JsonConvert.SerializeObject(Indv, Formatting.Indented);
            System.IO.File.WriteAllText(path, json);

            /*String json = JsonConvert.SerializeObject(Indv, Formatting.Indented);
            DateTime fechaActual = DateTime.Now;
            String path = @"D:\TheBest" + fechaActual.Day + "-" + fechaActual.ToString("MM") + "-" + fechaActual.Year + "_" + fechaActual.ToString("HHmmss") + ".json";
            System.IO.File.WriteAllText(path, json);*/
        }

        public static ClsIndividuo JSON_Individuo(String file_location, BdVirtual bd)
        {
            if (file_location == "")
            {
                string dir_archivo = "D:/";
                var directory = new DirectoryInfo(dir_archivo);
                var ultimo_archivo = (from f in directory.GetFiles()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                //Console.WriteLine(ultimo_archivo.FullName);
                file_location = ultimo_archivo.FullName;
            }
            Console.WriteLine(file_location);
            string path = @"" + file_location;
            ClsEstructura esquema;
            using (StreamReader jsonStream = File.OpenText(path))
            {
                var json = jsonStream.ReadToEnd();
                esquema = JsonConvert.DeserializeObject<ClsEstructura>(json);
            }

            GSuperIndividuo.SetDb(bd, esquema.Turno);///PRUEBA

            ClsSuperIndividuo super = new ClsSuperIndividuo(esquema.SuperIndv, esquema.Turno);

            ClsIndividuo indv = new ClsIndividuo(super, esquema.Horarios);

            return indv;
        }
    }
}
