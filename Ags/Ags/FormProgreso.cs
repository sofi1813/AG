using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ags
{
    public partial class FormProgreso : Form
    {
        bool check = true;
        int turno = 0;

        public FormProgreso()
        {
            InitializeComponent();
            Components();
        }

        public void AgregarTitulo(int Titulo)
        {
            turno = Titulo;
            lbl_Titulo.Text = "Turno: " + turno;
        }
        private void Components()
        {
            txt_Salida.ScrollBars = ScrollBars.Vertical;
            txt_Salida.WordWrap = false;

            btn_Salir.Enabled = false;

            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.ScrollBar.Size = 10;
            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;
            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.Minimum = 1;

            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.Name = "Generaciones";
            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.Title = "Generaciones";
            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.TitleAlignment = StringAlignment.Center;
            chrt_Fitness.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Microsoft Sans Serif", 10);

            chrt_Fitness.ChartAreas["ChartArea1"].AxisY.Name = "Fitness";
            chrt_Fitness.ChartAreas["ChartArea1"].AxisY.Title = "Fitness";
            chrt_Fitness.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
            chrt_Fitness.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Microsoft Sans Serif", 10);
            chrt_Fitness.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "#.##";
            //chrt_Fitness.ChartAreas["ChartArea1"].AxisY.NumericAxis.TextFormatting

            chrt_Fitness.BackColor = Color.Transparent;
            chrt_Fitness.BackSecondaryColor = Color.White;
            chrt_Fitness.BorderlineWidth = 1;
        }
        public void Actualizar(int NumGeneracion, int MaxGeneracion, double Fitness, string Salida)
        {
            AgregarProgressBar(NumGeneracion, MaxGeneracion);
            AgregarChart(NumGeneracion, Fitness);
            AgregarTextbox(Salida);            
        }

        private void AgregarChart(int NumGeneracion, double Fitness)
        {
            chrt_Fitness.Series["Fitness"].Points.AddXY(NumGeneracion.ToString(), Math.Round((Double)Fitness, 2));
        }
        public void AgregarTextbox(string Salida)
        {
            if (check == true)
            {
                txt_Salida.Text += Salida + "\r\n";
                txt_Salida.Select(txt_Salida.Text.Length, 0);
                txt_Salida.ScrollToCaret();
            }
        }
        private void AgregarProgressBar(int NumGeneracion, int MaxGeneracion)
        {
            pgr_Progreso.Maximum = MaxGeneracion - 1;
            pgr_Progreso.Increment(1);
        }

        public string ProcesoTerminado()
        {
            string texto = "El proceso para el Turno " + turno + " ha terminado";
            MessageBox.Show(texto, "Proceso Terminado", MessageBoxButtons.OK);

            this.Invoke(new MethodInvoker(delegate () { btn_Salir.Enabled = true; }));
            this.Invoke(new MethodInvoker(delegate () { lbl_Finalizado.Text = "El proceso ha finalizado"; }));

            DateTime fechaActual = DateTime.Now;
            //string path = @"D:\TheBest" + fechaActual.Day + "-" + fechaActual.ToString("MM") + "-" + fechaActual.Year + "_" + fechaActual.ToString("HHmmss") + ".json";
            string path = fechaActual.Year + "-" + fechaActual.ToString("MM") + "-" + fechaActual.Day + "-" + fechaActual.ToString("HHmmss") + ".json";

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";
                //dialog.InitialDirectory = @"D:\";
                dialog.InitialDirectory = Environment.CurrentDirectory;
                dialog.Title = "Seleccione una ubicación para el archivo JSON";
                dialog.FileName = path;
                if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    //path = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                    path = dialog.FileName;
                }
            }
            return path;
        }

        private void chk_Mostrar_CheckedChanged(object sender, EventArgs e)
        {            
            check = !check;
            txt_Salida.Enabled = check;
        }
        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
