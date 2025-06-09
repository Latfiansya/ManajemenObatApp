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
            try
            {
                dgvSuplier.DataSource = db.ExecuteQuery("EXEC tampilkan_data_suplier");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInput()
        {
            txtId.Clear();
            txtNama.Clear();
            txtJenisObat.Clear();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtJenisObat.Text == "")
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string query = "EXEC tambah_suplier @id_suplier, @nama, @jenis_obat";
                SqlParameter[] param = {
            new SqlParameter("@id_suplier", txtId.Text),
            new SqlParameter("@nama", txtNama.Text),
            new SqlParameter("@jenis_obat", txtJenisObat.Text)
        };

                db.ExecuteNonQuery(query, param);
                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtJenisObat.Text == "")
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin mengubah data suplier ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                string query = "EXEC update_suplier @id_suplier, @nama, @jenis_obat";
                SqlParameter[] parameters = {
            new SqlParameter("@id_suplier", txtId.Text),
            new SqlParameter("@nama", string.IsNullOrWhiteSpace(txtNama.Text) ? (object)DBNull.Value : txtNama.Text),
            new SqlParameter("@jenis_obat", string.IsNullOrWhiteSpace(txtJenisObat.Text) ? (object)DBNull.Value : txtJenisObat.Text)
        };

                db.ExecuteNonQuery(query, parameters);
                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengedit data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Pilih data suplier yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data suplier ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                string query = "EXEC hapus_suplier @id";  
                SqlParameter[] param = {
            new SqlParameter("@id", txtId.Text)
        };
                db.ExecuteNonQuery(query, param);
                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void dgvSuplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvSuplier.Rows[e.RowIndex];
                    txtId.Text = dgvSuplier.Rows[e.RowIndex].Cells["Id_Suplier"].Value.ToString();
                    txtNama.Text = dgvSuplier.Rows[e.RowIndex].Cells["Nama_Suplier"].Value.ToString();
                    txtJenisObat.Text = dgvSuplier.Rows[e.RowIndex].Cells["jenis_obat"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengambil data dari tabel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
