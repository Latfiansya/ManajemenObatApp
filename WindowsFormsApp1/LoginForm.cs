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
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Ambil input dari TextBox
            string id = txtId.Text;
            string pass = txtPassword.Text;

            // Query untuk cek ID dan password dari tabel apoteker
            string query = "SELECT * FROM apoteker WHERE id_apoteker = @id AND password = @pass";
            SqlParameter[] parameters = {
        new SqlParameter("@id", id),
        new SqlParameter("@pass", pass)
    };

            // Panggil helper untuk koneksi database
            DatabaseHelper db = new DatabaseHelper("Data Source=PREDATOR579;Initial Catalog=ManajemenObat;Integrated Security=True");

            // Eksekusi query
            DataTable result = db.ExecuteQueryWithParameters(query, parameters);

            // Cek apakah login berhasil
            if (result.Rows.Count > 0)
            {
                // Sembunyikan form login dan buka form utama
                this.Hide();
                MainForm main = new MainForm(id); // Sesuaikan dengan form utama kamu
                main.Show();
            }
            else
            {
                // Tampilkan error
                lblError.Text = "ID Apoteker atau password salah";
                lblError.Visible = true;
            }
        }
    }
}
