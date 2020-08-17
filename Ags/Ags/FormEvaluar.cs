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
    public partial class FormEvaluar : Form
    {
        public FormEvaluar()
        {
            InitializeComponent();
            dgv_Valores.RowHeadersVisible = false;
        }

        public void AgregarTitulo(string Titulo)
        {
            lbl_Titulo.Text = "Individuo: " + Titulo;
        }        

        public void AgregarTabla(int[] Valores)
        {
            dgv_Valores.Rows.Insert(0, "1", "Choques Grupo", Valores[0].ToString(), ClsEvaluacion.value[0].ToString(), (Valores[0] * ClsEvaluacion.value[0]).ToString());
            dgv_Valores.Rows.Insert(1, "2", "Choques Aula", Valores[1].ToString(), ClsEvaluacion.value[1].ToString(), (Valores[1] * ClsEvaluacion.value[1]).ToString());
            dgv_Valores.Rows.Insert(2, "3", "Choques Docente", Valores[2].ToString(), ClsEvaluacion.value[2].ToString(), (Valores[2] * ClsEvaluacion.value[2]).ToString());
            dgv_Valores.Rows.Insert(3, "4", "Huecos Grupos", Valores[3].ToString(), ClsEvaluacion.value[3].ToString(), (Valores[3] * ClsEvaluacion.value[3]).ToString());
            dgv_Valores.Rows.Insert(4, "5", "Horas Consecutivas", Valores[4].ToString(), ClsEvaluacion.value[4].ToString(), (Valores[4] * ClsEvaluacion.value[4]).ToString());
            dgv_Valores.Rows.Insert(5, "6", "Salón Regular", Valores[5].ToString(), ClsEvaluacion.value[5].ToString(), (Valores[5] * ClsEvaluacion.value[5]).ToString());

            double Fitness = (Valores[0] * ClsEvaluacion.value[0]) + (Valores[1] * ClsEvaluacion.value[1]) + (Valores[2] * ClsEvaluacion.value[2]) + (Valores[3] * ClsEvaluacion.value[3]) + (Valores[4] * ClsEvaluacion.value[4]) + (Valores[5] * ClsEvaluacion.value[5]);
            lbl_Fitness.Text = "Fitness: " + Fitness;

        }

    }
}
