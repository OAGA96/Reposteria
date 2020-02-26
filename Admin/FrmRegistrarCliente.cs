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
    public partial class FrmRegistrarCliente : Form
    {
        public FrmRegistrarCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            if(String.IsNullOrWhiteSpace(txbNombre.Text) || String.IsNullOrWhiteSpace(txbDireccion.Text) || String.IsNullOrWhiteSpace(txbTelefono.Text) || String.IsNullOrWhiteSpace(tbxCorreo.Text))
            {
                MessageBox.Show("Llene todos los campos");
            }
            else
            {
                Boolean res = bd.RegistrarCliente(txbNombre.Text.ToString(), txbDireccion.Text.ToString(), txbTelefono.Text.ToString(), tbxCorreo.Text.ToString());
                if (res)
                {
                    MessageBox.Show("Cliente Registrado con Exito.", "", MessageBoxButtons.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el cliente");
                    this.Close();
                }
            }
            
        }

        private void txbTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
