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
    public partial class FormApoteker: Form
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
            dgvApoteker.DataSource = db.ExecuteQuery("EXEC select_apoteker");
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
                MessageBox.Show("Isi data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Klik ID yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "EXEC hapus_apoteker @id";
            SqlParameter[] param = {
                new SqlParameter("@id", txtId.Text)
            };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Isi data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
        private void dgvApoteker_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvApoteker.Rows[e.RowIndex].Cells["id_apoteker"].Value.ToString();
                txtNama.Text = dgvApoteker.Rows[e.RowIndex].Cells["nama"].Value.ToString();
                txtPassword.Text = dgvApoteker.Rows[e.RowIndex].Cells["password"].Value.ToString();
            }
        }
    }
}
