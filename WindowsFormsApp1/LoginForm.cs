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
using System.Configuration;

namespace ManajemenObatApp
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtId.Text.Trim();
                string pass = txtPassword.Text.Trim();

                string query = "SELECT * FROM apoteker WHERE id_apoteker = @id AND password = @pass";
                SqlParameter[] parameters = {
            new SqlParameter("@id", id),
            new SqlParameter("@pass", pass)
        };

                // Ambil connection string dari App.config
                string connectionString = ConfigurationManager.ConnectionStrings["ManajemenObatDB"].ConnectionString;

                // Gunakan helper
                DatabaseHelper db = new DatabaseHelper(connectionString);
                DataTable result = db.ExecuteQueryWithParameters(query, parameters);

                if (result.Rows.Count > 0)
                {
                    this.Hide();
                    MainForm main = new MainForm(id);
                    main.Show();
                }
                else
                {
                    lblError.Text = "ID Apoteker atau password salah";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat login:\n" + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
