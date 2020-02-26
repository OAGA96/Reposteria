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
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txbNombre.Text) && !String.IsNullOrWhiteSpace(txbCosto.Text))
            {
                BaseDeDatos bd = new BaseDeDatos();
                Boolean res = bd.RegistrarProducto(txbNombre.Text.ToString(), Convert.ToDouble(txbCosto.Text));

                if (res)
                {
                    MessageBox.Show("Producto Registrado");
                }
                else
                {
                    MessageBox.Show("No se pudo registrar producto.");
                }
            }
            else
            {
                MessageBox.Show("Llene todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            FillDGV();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dgvData.SelectedCells[0].Value.ToString();
                string nombre = dgvData.SelectedCells[1].Value.ToString();
                string costo = dgvData.SelectedCells[2].Value.ToString();
                BaseDeDatos bd = new BaseDeDatos();
                Boolean res = bd.UpdateProducto(nombre, costo, id);
                if (res)
                {
                    MessageBox.Show("Producto actualizado con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    FillDGV();
                }
                else
                {
                    MessageBox.Show("No se actualizo el producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Seleccione alguna columna", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FillDGV()
        {
            BaseDeDatos bd = new BaseDeDatos();
            DataSet ds = bd.GetProductos();
            dgvData.DataSource = ds.Tables[0];
        }
    }
}
