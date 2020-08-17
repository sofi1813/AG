namespace Ags
{
    partial class FormEvaluar
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
            this.dgv_Valores = new System.Windows.Forms.DataGridView();
            this.clm_Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Aspecto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_Titulo = new System.Windows.Forms.Label();
            this.lbl_Fitness = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Valores)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Valores
            // 
            this.dgv_Valores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Valores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clm_Numero,
            this.col_Aspecto,
            this.col_Cantidad,
            this.col_Peso,
            this.col_Subtotal});
            this.dgv_Valores.Location = new System.Drawing.Point(12, 33);
            this.dgv_Valores.Name = "dgv_Valores";
            this.dgv_Valores.ReadOnly = true;
            this.dgv_Valores.Size = new System.Drawing.Size(375, 202);
            this.dgv_Valores.TabIndex = 0;
            // 
            // clm_Numero
            // 
            this.clm_Numero.HeaderText = "No.";
            this.clm_Numero.Name = "clm_Numero";
            this.clm_Numero.ReadOnly = true;
            this.clm_Numero.Width = 50;
            // 
            // col_Aspecto
            // 
            this.col_Aspecto.HeaderText = "Aspecto";
            this.col_Aspecto.Name = "col_Aspecto";
            this.col_Aspecto.ReadOnly = true;
            this.col_Aspecto.Width = 150;
            // 
            // col_Cantidad
            // 
            this.col_Cantidad.HeaderText = "Cant.";
            this.col_Cantidad.Name = "col_Cantidad";
            this.col_Cantidad.ReadOnly = true;
            this.col_Cantidad.Width = 50;
            // 
            // col_Peso
            // 
            this.col_Peso.HeaderText = "Peso";
            this.col_Peso.Name = "col_Peso";
            this.col_Peso.ReadOnly = true;
            this.col_Peso.Width = 50;
            // 
            // col_Subtotal
            // 
            this.col_Subtotal.HeaderText = "Subtotal";
            this.col_Subtotal.Name = "col_Subtotal";
            this.col_Subtotal.ReadOnly = true;
            this.col_Subtotal.Width = 50;
            // 
            // lbl_Titulo
            // 
            this.lbl_Titulo.AutoSize = true;
            this.lbl_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Titulo.Location = new System.Drawing.Point(12, 9);
            this.lbl_Titulo.Name = "lbl_Titulo";
            this.lbl_Titulo.Size = new System.Drawing.Size(64, 17);
            this.lbl_Titulo.TabIndex = 1;
            this.lbl_Titulo.Text = "Individuo";
            // 
            // lbl_Fitness
            // 
            this.lbl_Fitness.AutoSize = true;
            this.lbl_Fitness.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Fitness.Location = new System.Drawing.Point(285, 9);
            this.lbl_Fitness.Name = "lbl_Fitness";
            this.lbl_Fitness.Size = new System.Drawing.Size(57, 17);
            this.lbl_Fitness.TabIndex = 3;
            this.lbl_Fitness.Text = "Fitness:";
            // 
            // FormEvaluar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 247);
            this.Controls.Add(this.lbl_Fitness);
            this.Controls.Add(this.lbl_Titulo);
            this.Controls.Add(this.dgv_Valores);
            this.Name = "FormEvaluar";
            this.Text = "Evaluar Individuo";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Valores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Valores;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm_Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Aspecto;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Peso;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Subtotal;
        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.Label lbl_Fitness;
    }
}