using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Diagnostics;
using ApiBanco;

namespace PasteleriaReposteria
{
    public partial class FrmVentas : Form
    {
        string idCotizacion;
        public FrmVentas(string idcot)
        {
            idCotizacion = idcot;
            InitializeComponent();
        }

        string idcotizacion;

        BancoApi banco = new BancoApi(9212801696945648, 319, "05/21");

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            //dgvData.Columns.Add("idProducto", "idProducto");
            //dgvData.Columns.Add("NombreProducto", "Nombre del Producto");
            //dgvData.Columns.Add("Costo", "Costo");

            BaseDeDatos bd = new BaseDeDatos();

            //Fill idCotizacion
            DataSet cot = bd.GetCotizacion();
            cmbCotizacion.DataSource = cot.Tables[0];
            cmbCotizacion.DisplayMember = "idCotizacion";
            cmbCotizacion.ValueMember = "idCotizacion";
            cmbCotizacion.SelectedIndex = -1;

            DataSet paquetes = bd.GetPaquete();
            cmbPaquetes.DataSource = paquetes.Tables[0];
            cmbPaquetes.DisplayMember = "NombrePaquete";
            cmbPaquetes.ValueMember = "idPaquete";
            cmbPaquetes.SelectedIndex = -1;

            DataSet producto = bd.GetProductos();
            cmbProductos.DataSource = producto.Tables[0];
            cmbProductos.DisplayMember = "NombreProducto";
            cmbProductos.ValueMember = "idProducto";
            cmbProductos.SelectedIndex = -1;

            cmbCotizacion.Select();
            if(idCotizacion != "")
            {
                cmbCotizacion.SelectedValue = idCotizacion;
            }
        }

        private void btnSelec_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            int a = Convert.ToInt32(cmbCotizacion.SelectedValue);
            DataSet ds = bd.GetCotizacionInfo(a);
            var nombre = ds.Tables[0].Rows[0][2];
            txbNombreCliente.Text = nombre.ToString();
            var idCliente = ds.Tables[0].Rows[0][1];
            lblidCliente.Text = idCliente.ToString();
            var notas = ds.Tables[0].Rows[0][4];
            txbNotas.Text = notas.ToString();
            var total = ds.Tables[0].Rows[0][5];
            txbTotal.Text = total.ToString();
            var info = ds.Tables[0].Rows[0][3];
            DataTable temp = ProductInfo(info.ToString());

            dgvData.DataSource = temp;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            string info = DGVtoText(dgvData, '\t');
            BaseDeDatos bd = new BaseDeDatos();
            DialogResult dialog = MessageBox.Show("¿Desea Pagar con tarjeta?", "Metodo de Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialog == DialogResult.No)
            {

                using(var efectivo = new FrmEfectivo(txbTotal.Text.ToString()))
                {
                    var result = efectivo.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Boolean res = bd.RegistrarVenta(txbNotas.Text.ToString(), Convert.ToDouble(txbTotal.Text), dtpFecha.Value.ToString("yyyyMMdd"), Convert.ToInt32(lblidCliente.Text), Convert.ToInt32(cmbCotizacion.SelectedValue), info, efectivo.folio);
                        if (res)
                        {
                            PDF(efectivo.folio);
                        }
                        else
                        {
                            MessageBox.Show("No se registro la venta en la base de datos.");
                        }
                    }
                }

            }
            else if(dialog == DialogResult.Yes)
            {
                using (var tarjeta = new FrmTarjeta(txbTotal.Text.ToString()))
                {
                    var result = tarjeta.ShowDialog();
                    if(result == DialogResult.OK)
                    {
                        Boolean res = bd.RegistrarVenta(txbNotas.Text.ToString(), Convert.ToDouble(txbTotal.Text), dtpFecha.Value.ToString("yyyyMMdd"), Convert.ToInt32(lblidCliente.Text), Convert.ToInt32(cmbCotizacion.SelectedValue), info, tarjeta.folio);
                        if (res)
                        {
                            PDF(tarjeta.folio);
                        }
                        else
                        {
                            MessageBox.Show("No se registro la venta en la base de datos.");
                        }
                    }
                }
            }

        }

        private DataTable ProductInfo(string a)
        {
            int lineas = a.Split('\n').Length;
            DataTable dt = new DataTable();
            dt.Columns.Add("idProducto");
            dt.Columns.Add("NombreProducto");
            dt.Columns.Add("Costo");
            using(StringReader reader = new StringReader(a))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    else
                    {
                        String[] arr = line.Split('\t');
                        DataRow newRow = dt.NewRow();
                        newRow.ItemArray = arr;
                        dt.Rows.Add(newRow);
                    }
                    
                }
            }
            return dt;
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

        private void button1_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            DataTable dt = bd.GetProductoInfo1(Convert.ToInt32(cmbProductos.SelectedValue));
            foreach (DataRow dr in dt.Rows)
            {
                dgvData.Rows.Add(dr.ItemArray);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            DataTable dt = bd.GetPaqueteInfo1(Convert.ToInt32(cmbPaquetes.SelectedValue));
            foreach (DataRow dr in dt.Rows)
            {
                dgvData.Rows.Add(dr.ItemArray);
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

        private void PDF(string folio)
        {
            BaseDeDatos bd = new BaseDeDatos();
            string[] datos = bd.GetLastVenta().ToArray();
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Venta";
            Console.WriteLine(datos[0]);
            Console.ReadLine();

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Size = PageSize.Statement;

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Times New Roman", 14, XFontStyle.Regular);

            XTextFormatter tf = new XTextFormatter(gfx);

            //idCotizacion
            idcotizacion = datos[0];
            string idcot = "Venta#" + datos[0] +  @"
Folio de Pago: " + folio ;
            XRect rect = new XRect(-20, 12, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString(idcot, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            //Titulo
            string titulo = "Reposteria";
            rect = new XRect(0, 60, page.Width, page.Height);
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

            //Fecha
            string fecha = "Fecha: " + datos[3];
            rect = new XRect(-20, 120, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Right;
            tf.DrawString(fecha, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            //Notas y Precio
            string notprec = "Detalles: " + datos[4] + @"
Costo: $" + datos[5];
            rect = new XRect(40, 150, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(notprec, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            string prodTI = @"Productos

";
            rect = new XRect(40, 190, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.Bold);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(prodTI, font, XBrushes.Black, rect, XStringFormats.TopLeft);


            //Productos
            string prod = datos[6];
            rect = new XRect(0, 220, page.Width, page.Height);
            font = new XFont("Times New Roman", 12, XFontStyle.BoldItalic);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString(prod, font, XBrushes.Black, rect, XStringFormats.TopLeft);


            string filename = "Venta.pdf";
            document.Save(filename);

            Process.Start(filename);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using(var form = new FrmDisponibilidad())
            {
                var result = form.ShowDialog();
            }
        }
    }
}
