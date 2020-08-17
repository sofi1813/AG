namespace Ags
{
    partial class FormProgreso
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chrt_Fitness = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pgr_Progreso = new System.Windows.Forms.ProgressBar();
            this.chk_Mostrar = new System.Windows.Forms.CheckBox();
            this.txt_Salida = new System.Windows.Forms.TextBox();
            this.lbl_Progreso = new System.Windows.Forms.Label();
            this.lbl_Titulo = new System.Windows.Forms.Label();
            this.lbl_Finalizado = new System.Windows.Forms.Label();
            this.btn_Salir = new System.Windows.Forms.Button();
            this.lbl_Descripcion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chrt_Fitness)).BeginInit();
            this.SuspendLayout();
            // 
            // chrt_Fitness
            // 
            chartArea1.Name = "ChartArea1";
            this.chrt_Fitness.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chrt_Fitness.Legends.Add(legend1);
            this.chrt_Fitness.Location = new System.Drawing.Point(403, 12);
            this.chrt_Fitness.Name = "chrt_Fitness";
            this.chrt_Fitness.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Fitness";
            this.chrt_Fitness.Series.Add(series1);
            this.chrt_Fitness.Size = new System.Drawing.Size(679, 580);
            this.chrt_Fitness.TabIndex = 0;
            // 
            // pgr_Progreso
            // 
            this.pgr_Progreso.Location = new System.Drawing.Point(13, 445);
            this.pgr_Progreso.Name = "pgr_Progreso";
            this.pgr_Progreso.Size = new System.Drawing.Size(364, 32);
            this.pgr_Progreso.TabIndex = 1;
            // 
            // chk_Mostrar
            // 
            this.chk_Mostrar.AutoSize = true;
            this.chk_Mostrar.Checked = true;
            this.chk_Mostrar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Mostrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Mostrar.Location = new System.Drawing.Point(12, 62);
            this.chk_Mostrar.Name = "chk_Mostrar";
            this.chk_Mostrar.Size = new System.Drawing.Size(139, 21);
            this.chk_Mostrar.TabIndex = 3;
            this.chk_Mostrar.Text = "Mostrar Mensajes";
            this.chk_Mostrar.UseVisualStyleBackColor = true;
            this.chk_Mostrar.CheckedChanged += new System.EventHandler(this.chk_Mostrar_CheckedChanged);
            // 
            // txt_Salida
            // 
            this.txt_Salida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Salida.Location = new System.Drawing.Point(12, 89);
            this.txt_Salida.Multiline = true;
            this.txt_Salida.Name = "txt_Salida";
            this.txt_Salida.Size = new System.Drawing.Size(365, 293);
            this.txt_Salida.TabIndex = 4;
            // 
            // lbl_Progreso
            // 
            this.lbl_Progreso.AutoSize = true;
            this.lbl_Progreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Progreso.Location = new System.Drawing.Point(13, 425);
            this.lbl_Progreso.Name = "lbl_Progreso";
            this.lbl_Progreso.Size = new System.Drawing.Size(66, 17);
            this.lbl_Progreso.TabIndex = 5;
            this.lbl_Progreso.Text = "Progreso";
            // 
            // lbl_Titulo
            // 
            this.lbl_Titulo.AutoSize = true;
            this.lbl_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Titulo.Location = new System.Drawing.Point(12, 12);
            this.lbl_Titulo.Name = "lbl_Titulo";
            this.lbl_Titulo.Size = new System.Drawing.Size(55, 20);
            this.lbl_Titulo.TabIndex = 6;
            this.lbl_Titulo.Text = "Turno";
            // 
            // lbl_Finalizado
            // 
            this.lbl_Finalizado.AutoSize = true;
            this.lbl_Finalizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Finalizado.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbl_Finalizado.Location = new System.Drawing.Point(123, 15);
            this.lbl_Finalizado.Name = "lbl_Finalizado";
            this.lbl_Finalizado.Size = new System.Drawing.Size(0, 17);
            this.lbl_Finalizado.TabIndex = 7;
            this.lbl_Finalizado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Salir
            // 
            this.btn_Salir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Salir.Location = new System.Drawing.Point(289, 11);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(88, 25);
            this.btn_Salir.TabIndex = 8;
            this.btn_Salir.Text = "Salir";
            this.btn_Salir.UseVisualStyleBackColor = true;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // lbl_Descripcion
            // 
            this.lbl_Descripcion.AutoSize = true;
            this.lbl_Descripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Descripcion.Location = new System.Drawing.Point(12, 385);
            this.lbl_Descripcion.Name = "lbl_Descripcion";
            this.lbl_Descripcion.Size = new System.Drawing.Size(316, 17);
            this.lbl_Descripcion.TabIndex = 9;
            this.lbl_Descripcion.Text = "Fitness / Desviación Estándar / Fitness Promedio";
            // 
            // FormProgreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 606);
            this.Controls.Add(this.lbl_Descripcion);
            this.Controls.Add(this.btn_Salir);
            this.Controls.Add(this.lbl_Finalizado);
            this.Controls.Add(this.lbl_Titulo);
            this.Controls.Add(this.lbl_Progreso);
            this.Controls.Add(this.txt_Salida);
            this.Controls.Add(this.chk_Mostrar);
            this.Controls.Add(this.pgr_Progreso);
            this.Controls.Add(this.chrt_Fitness);
            this.Name = "FormProgreso";
            this.Text = "Generando Individuo";
            ((System.ComponentModel.ISupportInitialize)(this.chrt_Fitness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar pgr_Progreso;
        private System.Windows.Forms.CheckBox chk_Mostrar;
        private System.Windows.Forms.TextBox txt_Salida;
        private System.Windows.Forms.Label lbl_Progreso;
        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrt_Fitness;
        private System.Windows.Forms.Label lbl_Finalizado;
        private System.Windows.Forms.Button btn_Salir;
        private System.Windows.Forms.Label lbl_Descripcion;
    }
}