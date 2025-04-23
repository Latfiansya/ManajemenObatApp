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
    public partial class FormApoteker: Form
    {
        DatabaseHelper db = new DatabaseHelper("Data Source=PREDATOR579;Initial Catalog=ManajemenObat;Integrated Security=True");
        public FormApoteker()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgvApoteker.DataSource = db.ExecuteQuery("SELECT * FROM apoteker");
        }

        private void ClearInput()
        {
            txtId.Clear();
            txtNama.Clear();
            txtPassword.Clear();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO apoteker VALUES (@id, @nama, @pass)";
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
            string query = "DELETE FROM apoteker WHERE id_apoteker=@id";
            SqlParameter[] param = {
                new SqlParameter("@id", txtId.Text)
            };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string query = "UPDATE apoteker SET nama=@nama, password=@pass WHERE id_apoteker=@id";
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
