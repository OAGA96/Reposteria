using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteleriaReposteria
{
    public partial class FrmDisponibilidad : Form
    {
        public FrmDisponibilidad()
        {
            InitializeComponent();
        }

        private void btnDisponibilidad_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            DataSet res = bd.CheckDisponibilidad(dtpFecha.Value.ToString("yyyyMMdd"));

            if (res.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Fecha disponible.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Fecha no disponible.");
                this.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
