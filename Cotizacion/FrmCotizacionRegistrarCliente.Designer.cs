namespace PasteleriaReposteria
{
    partial class FrmCotizacionRegistrarCliente
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVistaRegistrar = new System.Windows.Forms.Button();
            this.btnVistaCotizacion = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.panel1.Controls.Add(this.btnVistaRegistrar);
            this.panel1.Controls.Add(this.btnVistaCotizacion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1827, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnVistaRegistrar
            // 
            this.btnVistaRegistrar.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnVistaRegistrar.FlatAppearance.BorderSize = 0;
            this.btnVistaRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVistaRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVistaRegistrar.Location = new System.Drawing.Point(808, 0);
            this.btnVistaRegistrar.Name = "btnVistaRegistrar";
            this.btnVistaRegistrar.Size = new System.Drawing.Size(488, 35);
            this.btnVistaRegistrar.TabIndex = 0;
            this.btnVistaRegistrar.Text = "Registrar Cliente";
            this.btnVistaRegistrar.UseVisualStyleBackColor = false;
            this.btnVistaRegistrar.Click += new System.EventHandler(this.btnVistaRegistrar_Click);
            // 
            // btnVistaCotizacion
            // 
            this.btnVistaCotizacion.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btnVistaCotizacion.FlatAppearance.BorderSize = 0;
            this.btnVistaCotizacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVistaCotizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVistaCotizacion.Location = new System.Drawing.Point(316, 0);
            this.btnVistaCotizacion.Name = "btnVistaCotizacion";
            this.btnVistaCotizacion.Size = new System.Drawing.Size(486, 35);
            this.btnVistaCotizacion.TabIndex = 0;
            this.btnVistaCotizacion.Text = "Cotizar";
            this.btnVistaCotizacion.UseVisualStyleBackColor = false;
            this.btnVistaCotizacion.Click += new System.EventHandler(this.btnVistaCotizacion_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 35);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1827, 581);
            this.pnlMain.TabIndex = 1;
            // 
            // FrmCotizacionRegistrarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1827, 616);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCotizacionRegistrarCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCotizacionRegistrarCliente";
            this.Load += new System.EventHandler(this.FrmCotizacionRegistrarCliente_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVistaRegistrar;
        private System.Windows.Forms.Button btnVistaCotizacion;
        private System.Windows.Forms.Panel pnlMain;
    }
}