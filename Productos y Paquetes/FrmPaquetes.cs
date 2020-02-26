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
    public partial class FrmPaquetes : Form
    {
        public FrmPaquetes()
        {
            InitializeComponent();
        }


        private DataTable dt = new DataTable();

        private void FrmPaquetes_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn chkColumn = new DataGridViewCheckBoxColumn();
            chkColumn.Name = "Seleccionar";
            dgvProductos.Columns.Add(chkColumn);
            dgvProductos.Columns.Add("idProducto", "idProducto");
            dgvProductos.Columns.Add("NombreProducto", "Nombre del Producto");
            dgvProductos.Columns.Add("Costo", "Costo");
            BaseDeDatos bd = new BaseDeDatos();
            DataSet ds = bd.GetProductos();
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                dgvProductos.Rows.Add(false, row[0], row[1], row[2]);
            }
            dgvProductos.AutoResizeColumns();
            //dgvPaquete.Columns.Add("idProducto", "idProducto");
            //dgvPaquete.Columns.Add("NombreProducto", "Nombre del Producto");
            //dgvPaquete.Columns.Add("Costo", "Costo");

            dt.Columns.Add("IdProducto");
            dt.Columns.Add("NombreProducto");
            dt.Columns.Add("Costo");
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                bool selected = Convert.ToBoolean(row.Cells["Seleccionar"].Value);
                if (selected)
                {
                    dt.Rows.Add(row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value);
                }
            }
            dgvPaquete.DataSource = dt;
            dgvPaquete.AutoResizeColumns();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string elementos = Elementos();
            BaseDeDatos bd = new BaseDeDatos();
            if (String.IsNullOrWhiteSpace(txbNombre.Text))
            {
                MessageBox.Show("Ingrese un nombre.");
            }
            Boolean res = bd.RegistrarPaquete(txbNombre.Text.ToString(), elementos, Convert.ToDouble(tbxCosto.Text));

            if (res)
            {
                MessageBox.Show("Se registro el paquete.");
            }
            else
            {
                MessageBox.Show("No se registro el paquete.");
            }


        }

        private string Elementos()
        {
            string[] arr = dt.AsEnumerable().Select(r => r.Field<string>("idProducto")).ToArray();
            string elementos = "";
            for (int i = 0; i < arr.Length; i++)
            {
                elementos += arr[i];
                if (i < arr.Length - 1)
                {
                    elementos += ",";
                }
            }

            return elementos;
        }

        private void dgvPaquete_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Suma();
        }
        private void Suma()
        {
            int sum = 0;
            if (dgvPaquete.Rows.Count == 1)
            {
                return;
            }
            else
            {
                for (int i = 0; i < dgvPaquete.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgvPaquete.Rows[i].Cells[2].Value);
                }
            }

            tbxCosto.Text = sum.ToString();
        }

        private void dgvPaquete_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Suma();
        }
    }
}
