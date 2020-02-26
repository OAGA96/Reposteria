using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApiBanco;

namespace PasteleriaReposteria
{
    public partial class FrmTarjeta : Form
    {
        private string monto;
        public string folio;

        
        public FrmTarjeta(string Monto)
        {
            InitializeComponent();
            monto = Monto;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            

            if (!String.IsNullOrWhiteSpace(tbxTarjeta.Text) && !String.IsNullOrWhiteSpace(tbxVerificador.Text) && !String.IsNullOrWhiteSpace(tbxCaducidad.Text))
            {
                Regex regex = new Regex("^[0-9][0-9]+(/[0-9][0-9]+)+$");
                Match match = regex.Match(tbxCaducidad.Text.ToString());
                if (!match.Success)
                {
                    MessageBox.Show("Error: Ingrese la fecha de vencimiento en el formato '00/00'", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        BancoApi banco = new BancoApi(Convert.ToInt64(tbxTarjeta.Text), Convert.ToInt32(tbxVerificador.Text), tbxCaducidad.Text.ToString());
                        Comprobante comprobante = await banco.Transferencia(9212801696945648, Convert.ToInt32(monto));
                        folio = comprobante.Id_Transaccion.ToString();
                        if(Convert.ToInt32(folio) != 0)
                        {
                            MessageBox.Show("Tranferencia Exitosa.");
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se logro la transaccion: " + comprobante.Mensaje.ToString(), "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error );
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Llene todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void tbxTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbxVerificador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
