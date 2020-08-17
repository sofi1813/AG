using System;
using System.Data.SQLite;


namespace Ags
{
    class ClsConexion
    {
        private static SQLiteConnection conectar;
        private static string cnn;

        public ClsConexion(string _cnn)
        {
            cnn = _cnn;
        }
        public SQLiteConnection AbrirConexion()
        {
            try
            {
                conectar = new SQLiteConnection("Data Source=" + cnn);
                conectar.Open();
                return conectar;
            }
            catch
            {
                Console.WriteLine("eroror de conexion");
                return null;
            }
        }
    }
}
