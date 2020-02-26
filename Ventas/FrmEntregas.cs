using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteleriaReposteria
{
    public partial class FrmEntregas : Form
    {

        private int xEstado;
        public FrmEntregas()
        {
            InitializeComponent();
        }

        private void Entregas_Load(object sender, EventArgs e)
        {
            //dgvData.Columns.Add("idEntrega", "idEntrega");
            //dgvData.Columns.Add("idCliente", "idCliente");
            //dgvData.Columns.Add("Nombre", "Nombre del Producto");
            //dgvData.Columns.Add("Correo", "Correo");
            //dgvData.Columns.Add("Detalles", "Detalles");
            //dgvData.Columns.Add("Total", "Total");
            //dgvData.Columns.Add("FechaDeEntrega", "Fecha De Entrega");
            //dgvData.Columns.Add("idPedida", "idPedido");
            //dgvData.Columns.Add("Status", "Status");
            
            LlenarDGV();
            dgvData.AutoResizeColumns();

            int rol = Convert.ToInt32(BaseDeDatos.rol);
            switch (rol)
            {
                case 3:
                    btnElaborado.Enabled = true;
                    btnEntregado.Enabled = false;
                    btnEntregado.Visible = false;
                    break;
                case 4:
                    btnEntregado.Enabled = true;
                    btnElaborado.Enabled = false;
                    btnElaborado.Visible = false;
                    break;
                default:
                    btnEntregado.Enabled = false;
                    btnElaborado.Enabled = false;
                    btnElaborado.Visible = false;
                    btnEntregado.Visible = false;
                    break;
            }

            
        }

        private void LlenarDGV()
        {
            dgvData.DataSource = null;
            BaseDeDatos bd = new BaseDeDatos();
            int rol = Convert.ToInt32(BaseDeDatos.rol);
            DataTable dt;
            int estado;
            switch (rol)
            {
                case 3:
                    estado = 0;
                    xEstado = 1;
                    dt = bd.GetEntregas(estado);
                    break;
                case 4:
                    estado = 1;
                    xEstado = 2;
                    dt = bd.GetEntregas(estado);
                    break;
                default:
                    dt = bd.GetEntregasDef();
                    break;
            }

            dgvData.DataSource = dt;
            
            //foreach (DataRow dr in dt.Rows)
            //{
            //    dgvData.Rows.Add(dr.ItemArray);
            //}
        }

        private void btnEntregado_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            Boolean res = bd.RegistrarEntrega(Convert.ToInt32(dgvData.SelectedRows[0].Cells[0].Value), xEstado);
            if (res)
            {   
                if(Convert.ToInt32(BaseDeDatos.rol) == 4)               
                {
                    SendEmail();
                    MessageBox.Show("Se marco como entregado. Encuesta Enviada");
                }
                else
                {
                    MessageBox.Show("Se marco como elaborado.");
                }
                
            }

            foreach(DataGridViewRow row in dgvData.SelectedRows)
            {
                dgvData.Rows.RemoveAt(row.Index);
            }
        }

        private void SendEmail()
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("reposteriaITH@gmail.com");
                message.To.Add(new MailAddress(dgvData.SelectedRows[0].Cells[3].Value.ToString()));
                message.Subject = "Encuesta";
                message.Body = @"Ayudanos a mejorar contestando esta corta encuesta.
https://forms.gle/6pXGjyq6Tgid1TJa7";

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("reposteriaITH@gmail.com", "Reposteria2019");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            (dgvData.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '%{0}%'", txbSearch.Text);
        }
    }
}
