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
    public partial class FrmClientes : Form
    {
        private Form formularioActivo = null;
        public FrmClientes()
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

        private void btnAgregarClientes_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormulario(new FrmRegistrarCliente());
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
