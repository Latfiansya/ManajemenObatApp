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
    public partial class FormRacikan: Form
    {
        DatabaseHelper db;
        public FormRacikan()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["ManajemenObatDB"].ConnectionString;
            db = new DatabaseHelper(connectionString);
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

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtIdTransaksi.Text == "" || txtResep.Text == "" || txtTanggal.Text == "")
            {
                MessageBox.Show("Isi data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO racikan_obat (id_racikan, id_transaksi, resep_dokter, tanggal_resep) VALUES (@id, @trans, @resep, @tgl)";
            SqlParameter[] parameters = {
                new SqlParameter("@id", txtId.Text),
                new SqlParameter("@trans", txtIdTransaksi.Text),
                new SqlParameter("@resep", txtResep.Text),
                new SqlParameter("@tgl", txtTanggal.Text)
            };

            try
            {
                db.ExecuteNonQuery(query, parameters);
                LoadData();
                ClearInput();
                MessageBox.Show("Data berhasil ditambahkan.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah data: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtIdTransaksi.Text == "" || txtResep.Text == "" || txtTanggal.Text == "")
            {
                MessageBox.Show("Isi data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE racikan_obat SET id_transaksi=@trans, resep_dokter=@resep, tanggal_resep=@tgl WHERE id_racikan=@id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", txtId.Text),
                new SqlParameter("@trans", txtIdTransaksi.Text),
                new SqlParameter("@resep", txtResep.Text),
                new SqlParameter("@tgl", txtTanggal.Text)
            };

            try
            {
                db.ExecuteNonQuery(query, parameters);
                LoadData();
                ClearInput();
                MessageBox.Show("Data berhasil diubah.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengubah data: " + ex.Message);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Isi data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "DELETE FROM racikan_obat WHERE id_racikan=@id";
            SqlParameter[] parameters = {
                new SqlParameter("@id", txtId.Text)
            };

            try
            {
                db.ExecuteNonQuery(query, parameters);
                LoadData();
                ClearInput();
                MessageBox.Show("Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data: " + ex.Message);
            }
        }
    }
}
