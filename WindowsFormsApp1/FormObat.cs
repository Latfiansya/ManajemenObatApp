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
    public partial class FormObat: Form
    {
        DatabaseHelper db;
        public FormObat()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["ManajemenObatDB"].ConnectionString;
            db = new DatabaseHelper(connectionString);
            LoadData();

        }


        private void FormObat_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            string query = "SELECT * FROM obat";
            dgvObat.DataSource = db.ExecuteQueryWithParameters(query, new SqlParameter[] { });
        }

        private void ClearInput()
        {
            txtId.Clear();
            txtNama.Clear();
            txtKategori.Clear();
            txtTanggal.Clear();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtKategori.Text == "" || txtTanggal.Text == "")
            {
                MessageBox.Show("Isi data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO obat (id_obat, nama, kategori, tgl_kadaluwarsa) VALUES (@id, @nama, @kategori, @tgl)";
            SqlParameter[] parameters = {
                new SqlParameter("@id", txtId.Text),
                new SqlParameter("@nama", txtNama.Text),
                new SqlParameter("@kategori", txtKategori.Text),
                new SqlParameter("@tgl", DateTime.Parse(txtTanggal.Text))
            };
            db.ExecuteNonQuery(query, parameters);
            LoadData();
            ClearInput();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtKategori.Text == "" || txtTanggal.Text == "")
            {
                MessageBox.Show("Isi data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE obat SET nama = @nama, kategori = @kategori, tgl_kadaluwarsa = @tgl WHERE id_obat = @id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", txtId.Text),
                new SqlParameter("@nama", txtNama.Text),
                new SqlParameter("@kategori", txtKategori.Text),
                new SqlParameter("@tgl", DateTime.Parse(txtTanggal.Text))
            };
            db.ExecuteNonQuery(query, parameters);
            LoadData();
            ClearInput();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Ketik ID yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "DELETE FROM obat WHERE id_obat = @id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", txtId.Text)
            };
            db.ExecuteNonQuery(query, parameters);
            LoadData();
            ClearInput();
        }

        private void dgvObat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvObat.Rows[e.RowIndex];
            txtId.Text = row.Cells["id_obat"].Value.ToString();
            txtNama.Text = row.Cells["nama"].Value.ToString();
            txtKategori.Text = row.Cells["kategori"].Value.ToString();
            txtTanggal.Text = Convert.ToDateTime(row.Cells["tgl_kadaluwarsa"].Value).ToString("yyyy-MM-dd");
        }
    }
}
