namespace Ags
{
    partial class FormMain
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
            this.lbl_Titulo = new System.Windows.Forms.Label();
            this.lbl_PorcCruza = new System.Windows.Forms.Label();
            this.txt_Individuos = new System.Windows.Forms.TextBox();
            this.lbl_Generaciones = new System.Windows.Forms.Label();
            this.txt_Generaciones = new System.Windows.Forms.TextBox();
            this.cmb_Turnos = new System.Windows.Forms.ComboBox();
            this.txt_PorcCruza = new System.Windows.Forms.TextBox();
            this.lbl_Individuos = new System.Windows.Forms.Label();
            this.btn_Generar = new System.Windows.Forms.Button();
            this.lbl_Turno = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Exportar = new System.Windows.Forms.Button();
            this.btn_Evaluar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.generarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseDeDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seleccionarBaseDeDatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_PorcReparacion = new System.Windows.Forms.TextBox();
            this.txt_PorcTurbulencia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Titulo
            // 
            this.lbl_Titulo.AutoSize = true;
            this.lbl_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Titulo.Location = new System.Drawing.Point(8, 36);
            this.lbl_Titulo.Name = "lbl_Titulo";
            this.lbl_Titulo.Size = new System.Drawing.Size(135, 20);
            this.lbl_Titulo.TabIndex = 34;
            this.lbl_Titulo.Text = "Generar Individuo";
            // 
            // lbl_PorcCruza
            // 
            this.lbl_PorcCruza.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_PorcCruza.AutoSize = true;
            this.lbl_PorcCruza.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PorcCruza.Location = new System.Drawing.Point(60, 117);
            this.lbl_PorcCruza.Name = "lbl_PorcCruza";
            this.lbl_PorcCruza.Size = new System.Drawing.Size(81, 17);
            this.lbl_PorcCruza.TabIndex = 25;
            this.lbl_PorcCruza.Text = "% Mutación";
            // 
            // txt_Individuos
            // 
            this.txt_Individuos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Individuos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Individuos.Location = new System.Drawing.Point(147, 43);
            this.txt_Individuos.Name = "txt_Individuos";
            this.txt_Individuos.Size = new System.Drawing.Size(156, 22);
            this.txt_Individuos.TabIndex = 26;
            this.txt_Individuos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Individuos_KeyPress);
            // 
            // lbl_Generaciones
            // 
            this.lbl_Generaciones.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Generaciones.AutoSize = true;
            this.lbl_Generaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Generaciones.Location = new System.Drawing.Point(44, 81);
            this.lbl_Generaciones.Name = "lbl_Generaciones";
            this.lbl_Generaciones.Size = new System.Drawing.Size(97, 17);
            this.lbl_Generaciones.TabIndex = 24;
            this.lbl_Generaciones.Text = "Generaciones";
            // 
            // txt_Generaciones
            // 
            this.txt_Generaciones.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Generaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Generaciones.Location = new System.Drawing.Point(147, 79);
            this.txt_Generaciones.Name = "txt_Generaciones";
            this.txt_Generaciones.Size = new System.Drawing.Size(156, 22);
            this.txt_Generaciones.TabIndex = 27;
            this.txt_Generaciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Generaciones_KeyPress);
            // 
            // cmb_Turnos
            // 
            this.cmb_Turnos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmb_Turnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Turnos.FormattingEnabled = true;
            this.cmb_Turnos.Location = new System.Drawing.Point(147, 6);
            this.cmb_Turnos.Name = "cmb_Turnos";
            this.cmb_Turnos.Size = new System.Drawing.Size(156, 24);
            this.cmb_Turnos.TabIndex = 30;
            this.cmb_Turnos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_Turnos_KeyPress);
            // 
            // txt_PorcCruza
            // 
            this.txt_PorcCruza.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_PorcCruza.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PorcCruza.Location = new System.Drawing.Point(147, 115);
            this.txt_PorcCruza.Name = "txt_PorcCruza";
            this.txt_PorcCruza.Size = new System.Drawing.Size(156, 22);
            this.txt_PorcCruza.TabIndex = 28;
            this.txt_PorcCruza.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_PorcCruza_KeyPress);
            // 
            // lbl_Individuos
            // 
            this.lbl_Individuos.AccessibleDescription = "";
            this.lbl_Individuos.AccessibleName = "";
            this.lbl_Individuos.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Individuos.AutoSize = true;
            this.lbl_Individuos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Individuos.Location = new System.Drawing.Point(70, 45);
            this.lbl_Individuos.Name = "lbl_Individuos";
            this.lbl_Individuos.Size = new System.Drawing.Size(71, 17);
            this.lbl_Individuos.TabIndex = 23;
            this.lbl_Individuos.Text = "Individuos";
            // 
            // btn_Generar
            // 
            this.btn_Generar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Generar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Generar.Location = new System.Drawing.Point(233, 220);
            this.btn_Generar.Name = "btn_Generar";
            this.btn_Generar.Size = new System.Drawing.Size(117, 27);
            this.btn_Generar.TabIndex = 31;
            this.btn_Generar.Text = "Generar";
            this.btn_Generar.UseVisualStyleBackColor = true;
            this.btn_Generar.Click += new System.EventHandler(this.btn_Aceptar_Click);
            // 
            // lbl_Turno
            // 
            this.lbl_Turno.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_Turno.AutoSize = true;
            this.lbl_Turno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Turno.Location = new System.Drawing.Point(95, 9);
            this.lbl_Turno.Name = "lbl_Turno";
            this.lbl_Turno.Size = new System.Drawing.Size(46, 17);
            this.lbl_Turno.TabIndex = 29;
            this.lbl_Turno.Text = "Turno";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.87982F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.12018F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_Turno, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Individuos, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_PorcCruza, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Turnos, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_Generaciones, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Generaciones, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_Individuos, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_PorcCruza, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_Evaluar, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.btn_Exportar, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.btn_Generar, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txt_PorcReparacion, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txt_PorcTurbulencia, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 59);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.75862F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.06897F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(440, 290);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // btn_Exportar
            // 
            this.btn_Exportar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Exportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exportar.Location = new System.Drawing.Point(231, 257);
            this.btn_Exportar.Name = "btn_Exportar";
            this.btn_Exportar.Size = new System.Drawing.Size(121, 27);
            this.btn_Exportar.TabIndex = 32;
            this.btn_Exportar.Text = "Exportar a Excel";
            this.btn_Exportar.UseVisualStyleBackColor = true;
            this.btn_Exportar.Click += new System.EventHandler(this.btn_Exportar_Click);
            // 
            // btn_Evaluar
            // 
            this.btn_Evaluar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Evaluar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Evaluar.Location = new System.Drawing.Point(15, 257);
            this.btn_Evaluar.Name = "btn_Evaluar";
            this.btn_Evaluar.Size = new System.Drawing.Size(113, 27);
            this.btn_Evaluar.TabIndex = 33;
            this.btn_Evaluar.Text = "Evaluar";
            this.btn_Evaluar.UseVisualStyleBackColor = true;
            this.btn_Evaluar.Click += new System.EventHandler(this.btn_evaluar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarToolStripMenuItem,
            this.baseDeDatosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(464, 24);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // generarToolStripMenuItem
            // 
            this.generarToolStripMenuItem.Name = "generarToolStripMenuItem";
            this.generarToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.generarToolStripMenuItem.Text = "Generar";
            // 
            // baseDeDatosToolStripMenuItem
            // 
            this.baseDeDatosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seleccionarBaseDeDatosToolStripMenuItem});
            this.baseDeDatosToolStripMenuItem.Name = "baseDeDatosToolStripMenuItem";
            this.baseDeDatosToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.baseDeDatosToolStripMenuItem.Text = "Base de Datos";
            // 
            // seleccionarBaseDeDatosToolStripMenuItem
            // 
            this.seleccionarBaseDeDatosToolStripMenuItem.Name = "seleccionarBaseDeDatosToolStripMenuItem";
            this.seleccionarBaseDeDatosToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.seleccionarBaseDeDatosToolStripMenuItem.Text = "Seleccionar Base de Datos";
            this.seleccionarBaseDeDatosToolStripMenuItem.Click += new System.EventHandler(this.seleccionarBaseDeDatosToolStripMenuItem_Click);
            // 
            // txt_PorcReparacion
            // 
            this.txt_PorcReparacion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_PorcReparacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PorcReparacion.Location = new System.Drawing.Point(147, 151);
            this.txt_PorcReparacion.Name = "txt_PorcReparacion";
            this.txt_PorcReparacion.Size = new System.Drawing.Size(156, 23);
            this.txt_PorcReparacion.TabIndex = 34;
            // 
            // txt_PorcTurbulencia
            // 
            this.txt_PorcTurbulencia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_PorcTurbulencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PorcTurbulencia.Location = new System.Drawing.Point(147, 187);
            this.txt_PorcTurbulencia.Name = "txt_PorcTurbulencia";
            this.txt_PorcTurbulencia.Size = new System.Drawing.Size(156, 23);
            this.txt_PorcTurbulencia.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 36;
            this.label1.Text = "% Reparación";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 37;
            this.label2.Text = "% Turbulencia";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 361);
            this.Controls.Add(this.lbl_Titulo);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(780, 620);
            this.MinimumSize = new System.Drawing.Size(480, 320);
            this.Name = "FormMain";
            this.Text = "Generar Horario";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.Label lbl_PorcCruza;
        private System.Windows.Forms.TextBox txt_Individuos;
        private System.Windows.Forms.Label lbl_Generaciones;
        private System.Windows.Forms.TextBox txt_Generaciones;
        private System.Windows.Forms.ComboBox cmb_Turnos;
        private System.Windows.Forms.TextBox txt_PorcCruza;
        private System.Windows.Forms.Label lbl_Individuos;
        private System.Windows.Forms.Button btn_Generar;
        private System.Windows.Forms.Label lbl_Turno;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Exportar;
        private System.Windows.Forms.Button btn_Evaluar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem generarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baseDeDatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seleccionarBaseDeDatosToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_PorcReparacion;
        private System.Windows.Forms.TextBox txt_PorcTurbulencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}