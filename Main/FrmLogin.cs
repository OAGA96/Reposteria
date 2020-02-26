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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logon();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void txbUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Logon();
            }
        }

        private void txbPsswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Logon();
            }
        }

        private void Logon()
        {
            if (!String.IsNullOrEmpty(txbUsuario.Text) && !String.IsNullOrEmpty(txbPsswd.Text))
            {
                try
                {
                    BaseDeDatos bd = new BaseDeDatos();

                    Boolean res = bd.InicioSesion(txbUsuario.Text.ToString(), txbPsswd.Text.ToString());

                    if (res)
                    {
                        FrmMenuPrincipal mp = new FrmMenuPrincipal();
                        mp.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Datos Incorrectos");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
