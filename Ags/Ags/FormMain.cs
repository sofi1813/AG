using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace Ags
{    
    public partial class FormMain : Form
    {
        int Turno = 0;
        int Poblacion = 80;
        int Generaciones = 1000;
        double Mutacion = 0.03, Turbulencia = 0.10, Reparacion = 0.15;

        BdVirtual bd;      

        public FormMain()
        {
            InitializeComponent();
            ControlEnable(false);
        }

        private void AddTurnosCmb(int[,] MatrizTurnos)
        {
            cmb_Turnos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_Turnos.Text = "-";
            for (int indice = 0; indice < MatrizTurnos.GetLength(0); indice++)
            {
                cmb_Turnos.Items.Add(MatrizTurnos[indice, 0]);
            }
        }

        private void SeleccionarBD()
        {
            try
            {
                string path = "";
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Database Files|*.db";
                    //dialog.InitialDirectory = @"D:\";
                    dialog.InitialDirectory = Environment.CurrentDirectory;
                    dialog.Title = "Seleccione una Base de Datos";
                    if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        path = dialog.FileName;
                        try
                        {
                            //string pathConnection = "C:/Users/sofil/source/repos/Ag/Ags/Ags/bin/Debug/BDHorariosUTHH.db";
                            bd = new BdVirtual();
                            ClsCargaDatos llenado = new ClsCargaDatos(path);
                            llenado.fill(bd);
                            llenado = null;
                            int[,] MatrizTurnos = bd.Turnos;
                            AddTurnosCmb(MatrizTurnos);
                            ControlEnable(true);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString(), "Proceso Terminado", MessageBoxButtons.OK);
                        }

                    }
                }
            }
            catch (Exception err) { }
        }
        private void ControlEnable(bool status)
        {
            cmb_Turnos.Enabled = status;
            txt_Individuos.Enabled = status;
            txt_Generaciones.Enabled = status;
            txt_PorcCruza.Enabled = status;
            txt_PorcReparacion.Enabled = status;
            txt_PorcTurbulencia.Enabled = status;
            btn_Generar.Enabled = status;
            btn_Exportar.Enabled = status;
            btn_Evaluar.Enabled = status;

            if (status)
            {
                txt_Individuos.Text = Poblacion.ToString();
                txt_Generaciones.Text = Generaciones.ToString();
                txt_PorcCruza.Text = Mutacion.ToString();
                txt_PorcReparacion.Text = Reparacion.ToString();
                txt_PorcTurbulencia.Text = Turbulencia.ToString();
            }

        }
        private void Acciones()
        {
            try
            {
                FormProgreso grafica = new FormProgreso();
                grafica.AgregarTitulo(Turno + 1);
                grafica.Show();

                GSuperIndividuo.SetDb(bd, Turno);
                ClsSuperIndividuo Super1 = new ClsSuperIndividuo();
                ClsAlgoritmo algr = new ClsAlgoritmo(Super1, Poblacion, Generaciones, Mutacion, Reparacion, Turbulencia);
                // pop size, iteraciones

                Thread hilo = new Thread(delegate ()
                {
                    algr.Run(grafica);
                });
                hilo.SetApartmentState(ApartmentState.STA);
                hilo.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        private void Exportar()
        {
            try
            {
                string path = "", name = "";
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                    //dialog.InitialDirectory = @"D:\";
                    dialog.InitialDirectory = Environment.CurrentDirectory;
                    dialog.Title = "Seleccione un archivo JSON";
                    if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        path = dialog.FileName;
                        name = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);

                        //GSuperIndividuo.SetDb(bd, Turno);
                        ClsIndividuo indiv = ClsTrataJSON.JSON_Individuo(path, bd);

                        FormExportar exportar = new FormExportar();
                        exportar.AgregarTitulo(name);
                        exportar.Show();

                        Thread hilo = new Thread(delegate ()
                        {
                            ClsExportar.ExportarHorario(indiv, exportar);
                        });
                        hilo.SetApartmentState(ApartmentState.STA);
                        hilo.Start();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
        private void Evaluar()
        {
            try
            {
                string path = "", name = "";
                int[] result;
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                    //dialog.InitialDirectory = @"D:\";
                    dialog.InitialDirectory = Environment.CurrentDirectory;
                    dialog.Title = "Seleccione un archivo JSON";
                    if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        path = dialog.FileName;
                        name = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);

                        //GSuperIndividuo.SetDb(bd, Turno);
                        ClsIndividuo indiv = ClsTrataJSON.JSON_Individuo(path, bd);
                        result = ClsEvaluacion.getArregloResultados(indiv);

                        FormEvaluar evaluar = new FormEvaluar();
                        evaluar.AgregarTitulo(name);
                        evaluar.AgregarTabla(result);
                        evaluar.Show();
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void cmb_Turnos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txt_Individuos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txt_Generaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txt_PorcCruza_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Turno = Int32.Parse(cmb_Turnos.Text) - 1;
                Poblacion = Int32.Parse(txt_Individuos.Text);
                Generaciones = Int32.Parse(txt_Generaciones.Text);
                Mutacion = Double.Parse(txt_PorcCruza.Text);
                Reparacion = Double.Parse(txt_PorcReparacion.Text);
                Turbulencia = Double.Parse(txt_PorcTurbulencia.Text);

                Acciones();
            } 
            catch (Exception err)
            {
                //MessageBox.Show(err.ToString());
                MessageBox.Show("Verificar los Datos");
            }
            
        }
        private void btn_Exportar_Click(object sender, EventArgs e)
        {
            Exportar();
        }
        private void btn_evaluar_Click(object sender, EventArgs e)
        {
            Evaluar();
        }
        private void seleccionarBaseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeleccionarBD();
        }
    }
}
