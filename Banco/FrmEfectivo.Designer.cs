﻿namespace PasteleriaReposteria
{
    partial class FrmEfectivo
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
            this.Recibo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbxDinero = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Recibo
            // 
            this.Recibo.AutoSize = true;
            this.Recibo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Recibo.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.Recibo.Location = new System.Drawing.Point(72, 30);
            this.Recibo.Name = "Recibo";
            this.Recibo.Size = new System.Drawing.Size(154, 24);
            this.Recibo.TabIndex = 0;
            this.Recibo.Text = "Pago En Efectivo";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.button1.Location = new System.Drawing.Point(99, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Confirmar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbxDinero
            // 
            this.tbxDinero.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxDinero.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDinero.Location = new System.Drawing.Point(66, 57);
            this.tbxDinero.Name = "tbxDinero";
            this.tbxDinero.Size = new System.Drawing.Size(167, 22);
            this.tbxDinero.TabIndex = 2;
            this.tbxDinero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxDinero_KeyPress);
            // 
            // FrmEfectivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(310, 141);
            this.Controls.Add(this.tbxDinero);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Recibo);
            this.Name = "FrmEfectivo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmEfectivo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Recibo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbxDinero;
    }
}