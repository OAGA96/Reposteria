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
    public partial class FrmAdminView : Form
    {
        private Form formularioActivo = null;
        public FrmAdminView()
        {
            InitializeComponent();
        }
        private void AbrirFormulario(Form form)
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
        private void FrmAdminView_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnAgUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmRegistrarUsuarios());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmProductos());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPaquetes_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmPaquetes());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
