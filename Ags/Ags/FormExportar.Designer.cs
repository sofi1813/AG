namespace Ags
{
    partial class FormExportar
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
            this.lbl_proceso = new System.Windows.Forms.Label();
            this.pgr_Progreso = new System.Windows.Forms.ProgressBar();
            this.lbl_Titulo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chk_Mostrar = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_proceso
            // 
            this.lbl_proceso.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbl_proceso.AutoSize = true;
            this.lbl_proceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_proceso.Location = new System.Drawing.Point(134, 86);
            this.lbl_proceso.Name = "lbl_proceso";
            this.lbl_proceso.Size = new System.Drawing.Size(140, 17);
            this.lbl_proceso.TabIndex = 0;
            this.lbl_proceso.Text = "Exportando Individuo";
            // 
            // pgr_Progreso
            // 
            this.pgr_Progreso.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pgr_Progreso.Location = new System.Drawing.Point(81, 106);
            this.pgr_Progreso.Name = "pgr_Progreso";
            this.pgr_Progreso.Size = new System.Drawing.Size(246, 31);
            this.pgr_Progreso.TabIndex = 2;
            // 
            // lbl_Titulo
            // 
            this.lbl_Titulo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Titulo.AutoSize = true;
            this.lbl_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Titulo.Location = new System.Drawing.Point(3, 11);
            this.lbl_Titulo.Name = "lbl_Titulo";
            this.lbl_Titulo.Size = new System.Drawing.Size(161, 20);
            this.lbl_Titulo.TabIndex = 35;
            this.lbl_Titulo.Text = "Exportando Individuio";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 409F));
            this.tableLayoutPanel1.Controls.Add(this.chk_Mostrar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Titulo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_proceso, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pgr_Progreso, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.84211F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.84211F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 192);
            this.tableLayoutPanel1.TabIndex = 36;
            // 
            // chk_Mostrar
            // 
            this.chk_Mostrar.AutoSize = true;
            this.chk_Mostrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Mostrar.Location = new System.Drawing.Point(3, 166);
            this.chk_Mostrar.Name = "chk_Mostrar";
            this.chk_Mostrar.Size = new System.Drawing.Size(193, 21);
            this.chk_Mostrar.TabIndex = 36;
            this.chk_Mostrar.Text = "Guardar Automáticamente";
            this.chk_Mostrar.UseVisualStyleBackColor = true;
            this.chk_Mostrar.CheckedChanged += new System.EventHandler(this.chk_Mostrar_CheckedChanged);
            // 
            // FormExportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 217);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "FormExportar";
            this.Text = "Exportar Archivo";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_proceso;
        private System.Windows.Forms.ProgressBar pgr_Progreso;
        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chk_Mostrar;
    }
}