using Microsoft.Office.Interop.Excel;
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
    public partial class FormExportar : Form
    {
        private string name = "";
        private bool guardarATM = false;

        public FormExportar()
        {
            InitializeComponent();
            lbl_proceso.Text = "Exportando Individuo ..";
        }

        public void AgregarTitulo(string Titulo)
        {
            name = Titulo;
            lbl_Titulo.Text = "Exportando Individuo: " + name;
        }

        public void Components(int max)
        {
            pgr_Progreso.Maximum = max + 1;
        }

        public void Actualizar(string proceso)
        {            
            pgr_Progreso.Increment(1);

            lbl_proceso.Text = "";
            lbl_proceso.Text = "Exportando Horario: " + proceso;
        }

        public string ProcesoTerminado()
        {

            pgr_Progreso.Increment(1);            

            string path = "Horario " + name;
            if (!guardarATM)
            {
                string texto = "El proceso para el Individuo " + name + " ha terminado";
                MessageBox.Show(texto, "Proceso Terminado", MessageBoxButtons.OK);

                using (var dialog = new SaveFileDialog())
                {
                    dialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                    //dialog.InitialDirectory = @"D:\";
                    dialog.InitialDirectory = Environment.CurrentDirectory;
                    dialog.Title = "Seleccione una ubicación para guardar el archivo";
                    dialog.FileName = name;
                    if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        //path = System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                        path = dialog.FileName;
                    }
                }
                this.Close();
            }
            else
            {
                string texto = "El proceso para el Individuo " + name + " ha terminado \r\n Guardado automáticamente en: Documentos/Horario" + name ;
                MessageBox.Show(texto, "Proceso Terminado", MessageBoxButtons.OK);
            }
            return path;
        }

        private void chk_Mostrar_CheckedChanged(object sender, EventArgs e)
        {
            guardarATM = !guardarATM;
        }
    }
}
