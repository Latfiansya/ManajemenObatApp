using ManajemenObatApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenObatApp
{
    public partial class FormRacikan: Form
    {
        DatabaseHelper db = new DatabaseHelper("Data Source=.;Initial Catalog=ManajemenObat;Integrated Security=True");

        public FormRacikan()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string query = "SELECT * FROM racikan_obat";
            DataTable dt = db.ExecuteQueryWithParameters(query, new SqlParameter[0]);
            dgvRacikan.DataSource = dt;
        }
        private void ClearInput()
        {
            txtId.Clear();
            txtIdTransaksi.Clear();
            txtResep.Clear();
            txtTanggal.Clear();
        }


        private void dgvRacikan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRacikan.Rows[e.RowIndex];
                txtId.Text = row.Cells["id_racikan"].Value.ToString();
                txtIdTransaksi.Text = row.Cells["id_transaksi"].Value.ToString();
                txtResep.Text = row.Cells["resep_dokter"].Value.ToString();
                txtTanggal.Text = Convert.ToDateTime(row.Cells["tanggal_resep"].Value).ToString("yyyy-MM-dd");
            }
        }
    }
}
