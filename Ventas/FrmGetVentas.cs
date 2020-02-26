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
    public partial class FrmGetVentas : Form
    {
        public FrmGetVentas()
        {
            InitializeComponent();
        }

        private void FrmGetVentas_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            DataTable dt = bd.GetVentas();

            dgvData.DataSource = dt;

            dgvData.AutoResizeColumns();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            (dgvData.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '%{0}%'", txbSearch.Text);
            //private void searchTextBox_TextChanged(object sender, EventArgs e)
            //{
            //    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", searchTextBox.Text);
            //}
        }
    }
}
