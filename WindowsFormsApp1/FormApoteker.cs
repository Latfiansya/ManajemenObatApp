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
    public partial class FormApoteker : Form
    {
        DatabaseHelper db;


        public FormApoteker()
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
                dgvApoteker.DataSource = db.ExecuteQuery("EXEC tampilkan_data_apoteker");
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
            txtPassword.Clear();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "EXEC tambah_apoteker @id, @nama, @pass";
                SqlParameter[] param = {
                    new SqlParameter("@id", txtId.Text),
                    new SqlParameter("@nama", txtNama.Text),
                    new SqlParameter("@pass", txtPassword.Text)
                };
                db.ExecuteNonQuery(query, param);
                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambahkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Pilih data apoteker yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                string query = "EXEC hapus_apoteker @id";
                SqlParameter[] param = {
                    new SqlParameter("@id", txtId.Text)
                };
                db.ExecuteNonQuery(query, param);
                LoadData();
                ClearInput();
                MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin mengubah data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                string query = "EXEC update_apoteker @id, @nama, @pass";
                SqlParameter[] param = {
                    new SqlParameter("@id", txtId.Text),
                    new SqlParameter("@nama", txtNama.Text),
                    new SqlParameter("@pass", txtPassword.Text)
                };
                db.ExecuteNonQuery(query, param);
                LoadData();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengedit data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvApoteker_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    txtId.Text = dgvApoteker.Rows[e.RowIndex].Cells["id_apoteker"].Value.ToString();
                    txtNama.Text = dgvApoteker.Rows[e.RowIndex].Cells["nama"].Value.ToString();
                    txtPassword.Text = dgvApoteker.Rows[e.RowIndex].Cells["password"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data ke form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
