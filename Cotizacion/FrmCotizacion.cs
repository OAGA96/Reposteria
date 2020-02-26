using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.IO;
using System.Diagnostics;

namespace PasteleriaReposteria
{
    public partial class FrmCotizacion : Form
    {
        private DataTable dt;
        string idcotizacion;
        //private bool isChecked;
        public FrmCotizacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            dt = bd.GetProductoInfo1(Convert.ToInt32(cmbProductos.SelectedValue));
            foreach(DataRow dr in dt.Rows)
            {
                dgvData.Rows.Add(dr.ItemArray);
            }
            dgvData.AutoResizeColumns();
        }

        private void FrmCotizacion_Load(object sender, EventArgs e)
        {
            //dt.Clear();
            dgvData.Columns.Add("idProducto", "idProducto");
            dgvData.Columns.Add("NombreProducto", "Nombre del Producto");
            dgvData.Columns.Add("Costo", "Costo");
            GetInfo();

        }

        private void GetInfo()
        {
            BaseDeDatos bd = new BaseDeDatos();

            DataSet ds = bd.GetCliente();
            cmbCliente.DataSource = ds.Tables[0];
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.ValueMember = "idCliente";
            cmbCliente.SelectedIndex = -1;

            DataSet paquetes = bd.GetPaquete();
            cmbPaquete.DataSource = paquetes.Tables[0];
            cmbPaquete.DisplayMember = "NombrePaquete";
            cmbPaquete.ValueMember = "idPaquete";
            cmbPaquete.SelectedIndex = -1;

            DataSet producto = bd.GetProductos();
            cmbProductos.DataSource = producto.Tables[0];
            cmbProductos.DisplayMember = "NombreProducto";
            cmbProductos.ValueMember = "idProducto";
            cmbProductos.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                dt = bd.GetPaqueteInfo1(Convert.ToInt32(cmbPaquete.SelectedValue));
                foreach (DataRow dr in dt.Rows)
                {
                    dgvData.Rows.Add(dr.ItemArray);
                }
                dgvData.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Suma();
        }

        private void dgvData_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Suma();
        }
        private void Suma()
        {
            int sum = 0;
            if (dgvData.Rows.Count == 1)
            {
                return;
            }
            else
            {
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgvData.Rows[i].Cells[2].Value);
                }
            }

            txbTotal.Text = sum.ToString();
        }
        private void Cootizar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txbTotal.Text) || String.IsNullOrWhiteSpace(cmbCliente.Text))
            {
                MessageBox.Show("Agregue productos y seleccione el cliente.");
            }
            else
            {
                string info = DGVtoText(dgvData, '\t');
                BaseDeDatos bd = new BaseDeDatos();
                Boolean res = bd.RegistrarCotizacion(txbDetalles.Text.ToString(), Convert.ToDouble(txbTotal.Text), Convert.ToInt32(cmbCliente.SelectedValue), info);
                if (res)
                {
                    MessageBox.Show("Se Registro Cotizacion");
                    PDF();
                    try
                    {
                        this.Close();
                        FrmMenuPrincipal _formularioabierto = Application.OpenForms.OfType<FrmMenuPrincipal>().Where(pre => pre.Name == "FrmMenuPrincipal").SingleOrDefault();
                        if (_formularioabierto != null)
                        {
                            _formularioabierto.AbrirFormulario(new FrmVentas(idcotizacion));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No se registro la cotizacion");
                }
            }
            
        }
        private static string DGVtoText(DataGridView dgv, char delimiter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    sb.Append(cell.Value);
                    sb.Append(delimiter);
                }
                sb.Remove(sb.Length - 1, 1); // Removes the last delimiter 
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void PDF()
        {
            BaseDeDatos bd = new BaseDeDatos();
            string[] datos = bd.GetLastCotizacion().ToArray();
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Cotizacion";

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Size = PageSize.Statement;


            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Times New Roman", 14, XFontStyle.Regular);
            
            XTextFormatter tf = new XTextFormatter(gfx);

            //idCotizacion
            idcotizacion = datos[0];
            string idcot = "Cotizacion#" + datos[0];
            XRect rect = new XRect(-20, 12, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString(idcot, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            //Titulo
            string titulo = "Reposteria";
            rect = new XRect(0,60, page.Width, page.Height);
            font = new XFont("Times New Roman", 24, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString(titulo, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            //Datos Cliente
            string cliente = "idCliente: " + datos[1] + @"
Nombre del Cliente: " + datos[2];
            rect = new XRect(40, 90, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(cliente, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            //Notas y Precio
            string notprec = "Detalles: " + datos[4] + @"
Costo: $" + datos[5];
            rect = new XRect(40, 130, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(notprec, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            string prodTI = @"Productos

";
            rect = new XRect(40, 160, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(prodTI, font, XBrushes.Black, rect, XStringFormats.TopLeft);


            //Productos
            string prod = datos[3];
            rect = new XRect(0, 190, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.BoldItalic);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString(prod, font, XBrushes.Black, rect, XStringFormats.TopLeft);


            string filename = "Cotizacion.pdf";
            document.Save(filename);

            Process.Start(filename);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PDF();
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            using(var form = new FrmRegistrarCliente())
            {
                var result = form.ShowDialog();
                GetInfo();
            }
        }
    }
}
