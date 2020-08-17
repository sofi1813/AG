using System.Data;
using System.Data.SQLite;

namespace Ags
{
    class ClsPeticiones 
    {
        ClsConexion ObjetoConexion;
        SQLiteConnection conexion;
        private static SQLiteDataAdapter consultas;
        #region variables
        private DataSet Tabla;
        #endregion

        public ClsPeticiones(string cnn)
        {
            ObjetoConexion = new ClsConexion(cnn);
            conexion = ObjetoConexion.AbrirConexion();

        }

        public DataSet Turnos() {
            Tabla = new DataSet();
            string con = "select clvTurno as Turno, Sum(Horas) As TamIndividuo from clvs_tblPlaneacion group by clvTurno";
            consultas = new SQLiteDataAdapter(con,conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet Dias()
        {
            Tabla = new DataSet();
            string con = "select clvDia from dat_tblDias";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet BloquesDias()
        {
            Tabla = new DataSet();
            string con = "select clvTurno, clvDia, clvBloque from clvs_tblBloques inner join dat_tblHoras on clvs_tblBloques.clvHora=dat_tblHoras.clvHora order by clvTurno, clvBloque;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet GrupoMateriaBloque()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "SELECT clvs_tblPlaneacion.clvTurno, clvs_tblPlaneacion.clvGrupo,clvs_tblPlaneacion.clvMateria,clvs_tblBloquesDeseados.Bloques from clvs_tblPlaneacion inner join clvs_tblBloquesDeseados on clvs_tblPlaneacion.clvMateria = clvs_tblBloquesDeseados.clvMateria where (clvCarrera = 39) and clvTurno ='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "'order by clvs_tblPlaneacion.clvGrupo,clvs_tblPlaneacion.clvMateria asc";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }
            return Tabla;
        }
        public DataSet Materias()
        {         
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "select distinct clvMateria from clvs_tblPlaneacion where clvTurno ='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "'order by clvMateria asc"; 
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }
            return Tabla;
        }
        public DataSet Materias_Superbloques()
        {
            Tabla = new DataSet();
            string con = "SELECT clvs_tblBloquesDeseados.clvMateria, clvs_tblBloquesDeseados.Bloques FROM clvs_tblBloquesDeseados;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet Materia_Docente_Cantidad()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "select distinct clvs_tblPlaneacion.clvTurno, clvs_tblMateriaDocentePosible.clvMateria,clvs_tblMateriaDocentePosible.clvDocente,clvs_tblMateriaDocentePosible.cantGruposDocente FROM clvs_tblMateriaDocentePosible inner join clvs_tblPlaneacion on clvs_tblMateriaDocentePosible.clvMateria=clvs_tblPlaneacion.clvMateria where clvTurno='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "' order by clvs_tblMateriaDocentePosible.clvMateria";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }
            return Tabla;
        }
        public DataSet Grupos()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "select clvGrupo from dat_tblGrupos where clvTurno='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "' order by clvGrupo asc";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }
            return Tabla;
        }
        public DataSet _GrupoMateria()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "select  clvTurno, clvGrupo,clvMateria from clvs_tblPlaneacion  where clvTurno='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "' order by clvGrupo,clvMateria asc;";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }
            return Tabla;
        }
        public DataSet Docentes()
        {
            Tabla = new DataSet();
            string con = "select distinct(clvDocente) from clvs_tblMateriaDocentePosible order by clvDocente;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet Aulas()
        {
            Tabla = new DataSet();
            string con = "select distinct(clvAula) from clvs_tblMateriaAulaPosible order by clvAula;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet Horas_Aulas()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvs_tblPlaneacion.clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {                
                string con = "select clvs_tblHorasDisponibleAula.clvAula, clvs_tblHorasDisponibleAula.clvBloque from clvs_tblHorasDisponibleAula inner join dat_tblBloques on clvs_tblHorasDisponibleAula.clvBloque=dat_tblBloques.clvBloque inner join dat_tblHoras on dat_tblBloques.clvHora=dat_tblHoras.clvHora where clvTurno='"+ int.Parse(Turnos.Tables[0].Rows[i][0].ToString())+"' order by clvs_tblHorasDisponibleAula.clvAula,clvs_tblHorasDisponibleAula.clvBloque";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla,i+"");
            }
            
            return Tabla;
        }
        public DataSet Horas_Grupo()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvs_tblPlaneacion.clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "select clvTurno, clvBloque from clvs_tblBloques inner join dat_tblHoras on clvs_tblBloques.clvHora=dat_tblHoras.clvHora where clvTurno='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "' order by clvBloque;";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }
            return Tabla;
        }
        public DataSet Horas_Docente()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvs_tblPlaneacion.clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "select clvs_tblHorasDispDocente.clvDocente, clvs_tblHorasDispDocente.clvBloque from clvs_tblHorasDispDocente inner join dat_tblBloques on clvs_tblHorasDispDocente.clvBloque=dat_tblBloques.clvBloque inner join dat_tblHoras on dat_tblBloques.clvHora=dat_tblHoras.clvHora where clvTurno='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "'order by clvs_tblHorasDispDocente.clvDocente,clvs_tblHorasDispDocente.clvBloque";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }

            return Tabla;
        }
        public DataSet MateriasAulas()
        {
            Tabla = new DataSet();
            string con = "select clvs_tblMateriaAulaPosible.clvMateria,clvs_tblMateriaAulaPosible.clvAula from clvs_tblMateriaAulaPosible order by clvMateria, clvAula asc";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet GrupoMateriaBloquesCantAulas()
        {
            DataSet Turnos = new DataSet();
            string Consulta_turnos = "select distinct clvTurno from clvs_tblPlaneacion";
            consultas = new SQLiteDataAdapter(Consulta_turnos, conexion);
            consultas.Fill(Turnos);
            int tam = Turnos.Tables[0].Rows.Count;
            Tabla = new DataSet();
            for (int i = 0; i < tam; i++)
            {
                string con = "select clvs_tblPlaneacion.clvGrupo, clvs_tblPlaneacion.clvMateria, clvs_tblBloquesDeseados.Bloques, dat_tblMaterias.intCantAulas from clvs_tblPlaneacion inner join clvs_tblBloquesDeseados on clvs_tblPlaneacion.clvMateria = clvs_tblBloquesDeseados.clvMateria inner join dat_tblMaterias on clvs_tblPlaneacion.clvMateria = dat_tblMaterias.clvMateria where clvCarrera = 39 and clvTurno='" + int.Parse(Turnos.Tables[0].Rows[i][0].ToString()) + "' order by dat_tblMaterias.intCantAulas, clvs_tblPlaneacion.clvGrupo,clvs_tblPlaneacion.clvMateria asc";
                consultas = new SQLiteDataAdapter(con, conexion);
                consultas.Fill(Tabla, i + "");
            }
            return Tabla;
        }

        // breakdown
        public DataSet DatosGrupos()
        {
            Tabla = new DataSet();
            string con = "select clvGrupo, vchGrupo from dat_tblGrupos;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet DatosAulas()
        {
            Tabla = new DataSet();
            string con = "select clvAula, vchAula from dat_tblAulas;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet DatosDocentes()
        {
            Tabla = new DataSet();
            string con = "select clvDocente, vchDocente from dat_tblDocentes;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet DatosMaterias()
        {
            Tabla = new DataSet();
            string con = "select clvMateria, vchMateria from dat_tblMaterias;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet BloquesReceso()
        {
            Tabla = new DataSet();
            string con = "Select clvBloque from dat_tblBloques where vchTipo = 'receso';";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet Horas()
        {
            Tabla = new DataSet();
            string con = "Select distinct dat_tblBloques.clvHora, dat_tblHoras.vchRango, dat_tblHoras.clvTurno from dat_tblBloques, dat_tblHoras where dat_tblBloques.clvHora = dat_tblHoras.clvHora and dat_tblBloques.vchTipo != 'noLaboral';";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }        
        public DataSet DatosDias()
        {
            Tabla = new DataSet();
            string con = "Select clvDia, vchDia from dat_tblDias;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }
        public DataSet DatosBloques() 
        {
            Tabla = new DataSet();
            string con = "select dat_tblHoras.clvTurno, dat_tblBloques.clvDia, dat_tblBloques.clvBloque from dat_tblHoras, dat_tblBloques where dat_tblBloques.clvHora = dat_tblHoras.clvHora and dat_tblBloques.vchTipo != 'noLaboral' order by clvTurno, clvBloque;";
            consultas = new SQLiteDataAdapter(con, conexion);
            consultas.Fill(Tabla);
            return Tabla;
        }

    }
}
