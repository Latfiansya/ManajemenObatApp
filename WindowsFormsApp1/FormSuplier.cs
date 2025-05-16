using ManajemenObatApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenObatApp
{
    public partial class FormSuplier: Form
    {
        DatabaseHelper db;
        public FormSuplier()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["ManajemenObatDB"].ConnectionString;
            db = new DatabaseHelper(connectionString);
            LoadData();

        }

        private void LoadData()
        {
            string query = "SELECT * FROM suplier";
            DataTable dt = db.ExecuteQueryWithParameters(query, new SqlParameter[0]);
            dgvSuplier.DataSource = dt;
        }
        private void ClearInput()
        {
            txtId.Clear();
            txtNama.Clear();
            txtJenisObat.Clear();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO suplier VALUES (@id, @nama, @jenis)";
            SqlParameter[] param = {
                new SqlParameter("@id", txtId.Text),
                new SqlParameter("@nama", txtNama.Text),
                new SqlParameter("@jenis", txtJenisObat.Text)
            };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string query = "UPDATE suplier SET nama=@nama, jenis_obat=@jenis WHERE id_suplier=@id";
            SqlParameter[] param = {
                new SqlParameter("@nama", txtNama.Text),
                new SqlParameter("@jenis", txtJenisObat.Text),
                new SqlParameter("@id", txtId.Text)
            };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM suplier WHERE id_suplier=@id";
            SqlParameter[] param = {
                new SqlParameter("@id", txtId.Text)
            };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void dgvSuplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvSuplier.Rows[e.RowIndex].Cells["id_suplier"].Value.ToString();
                txtNama.Text = dgvSuplier.Rows[e.RowIndex].Cells["nama"].Value.ToString();
                txtJenisObat.Text = dgvSuplier.Rows[e.RowIndex].Cells["jenis_obat"].Value.ToString();
            }
        }
    }
}
