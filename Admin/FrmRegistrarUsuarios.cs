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
    public partial class FrmRegistrarUsuarios : Form
    {
        public FrmRegistrarUsuarios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrWhiteSpace(txbNombre.Text) && !String.IsNullOrWhiteSpace(txbApellido.Text) && !String.IsNullOrWhiteSpace(txbDireccion.Text) && !String.IsNullOrWhiteSpace(txbCorreo.Text) && !String.IsNullOrWhiteSpace(txbTelefono.Text) && !String.IsNullOrWhiteSpace(txbUsuario.Text) && !String.IsNullOrWhiteSpace(txbPsswd.Text))
            {
                BaseDeDatos bd = new BaseDeDatos();

                Boolean res = bd.RegistrarUsuario(txbNombre.Text.ToString() + " " + txbApellido.Text.ToString(), txbDireccion.Text.ToString(), txbCorreo.Text.ToString(), txbTelefono.Text.ToString(), txbUsuario.Text.ToString(), txbPsswd.Text.ToString());

                if (res)
                {
                    MessageBox.Show("Se registro Usuario");
                }
                else
                {
                    MessageBox.Show("No se pudo registrar");
                }
            }
            else
            {
                MessageBox.Show("Llene todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
