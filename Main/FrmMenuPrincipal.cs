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
    public partial class FrmMenuPrincipal : Form
    {

        private Form formularioActivo = null;

        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        public void AbrirFormulario(Form form)
        {
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }
            formularioActivo = form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(form);
            pnlMain.Tag = form;
            form.BringToFront();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmCotizacion());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmVentas(""));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            if (FrmCerrar.x == true)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmEntregas());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmAdminView());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            lblRol.Text = BaseDeDatos.rol;
            int i = Convert.ToInt32(BaseDeDatos.rol);

            switch (i)
            {
                case 1:
                    btnCot.Enabled = true;
                    btnVenta.Enabled = true;
                    btnEntregas.Enabled = true;
                    btnAdmin.Enabled = true;
                    btnVentas.Enabled = true;
                    break;
                case 2:
                    btnCot.Enabled = true;
                    btnVenta.Enabled = true;
                    btnEntregas.Enabled = true;
                    btnAdmin.Enabled = false;
                    btnVentas.Enabled = false;
                    break;
                case 3:
                    btnCot.Enabled = false;
                    btnVenta.Enabled = false;
                    btnEntregas.Enabled = true;
                    btnAdmin.Enabled = false;
                    btnVentas.Enabled = false;
                    break;
                case 4:
                    btnCot.Enabled = false;
                    btnVenta.Enabled = false;
                    btnEntregas.Enabled = true;
                    btnAdmin.Enabled = false;
                    btnVentas.Enabled = false;
                    break;
                default:
                    break;
            }

        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmGetVentas());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {

        }

        private void FrmMenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            using(var cerrar = new FrmCerrar())
            {
                var result = cerrar.ShowDialog();
                switch (result)
                {
                    case DialogResult.Yes:
                        //Application.Exit();
                        e.Cancel = false;
                        break;
                    case DialogResult.No:
                        FrmLogin log = new FrmLogin();
                        log.Show();
                        e.Cancel = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            //e.Cancel = false;
            //DialogResult salir = MessageBox.Show("¿Desea salir del programa?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if (salir == DialogResult.Yes)
            //{
            //    Application.Exit();
            //}
            //else
            //{
            //    e.Cancel = true;
            //    DialogResult cerrar = MessageBox.Show("¿Desea cerrar sesion?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (cerrar == DialogResult.Yes)
            //    {
            //        FrmLogin log = new FrmLogin();
            //        log.Show();
            //        this.Close();
            //    }
            //}
        }
    }
}
