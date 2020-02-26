using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApiBanco;

namespace PasteleriaReposteria
{
    public partial class FrmEfectivo : Form
    {
        BancoApi banco = new BancoApi(9212801696945648, 319, "05/21");
        private string monto;
        public string folio;
        public FrmEfectivo(string Monto)
        {
            InitializeComponent();
            monto = Monto;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int cambio = Convert.ToInt32(tbxDinero.Text) - Convert.ToInt32(monto);
            if (Convert.ToInt32(tbxDinero.Text) < Convert.ToInt32(monto))
            {
                MessageBox.Show("Hace falta dinero.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Los parametros a enviar son el monto
                    Comprobante comprobante = await banco.Deposito(Convert.ToDecimal(monto));
                    folio = comprobante.Id_Transaccion.ToString();
                    MessageBox.Show("Su cambio es de $" + cambio.ToString(), "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
        }

        private void tbxDinero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
