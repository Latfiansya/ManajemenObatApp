using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenObatApp
{
    public partial class MainForm: Form
    {
        private string idApoteker;
        public MainForm(string idApoteker)
        {
            InitializeComponent();
            this.idApoteker = idApoteker;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Selamat datang, " + idApoteker;
        }

        private void btnKelolaApoteker_Click(object sender, EventArgs e)
        {
            FormApoteker f = new FormApoteker();
            f.ShowDialog();
        }

        private void btnKelolaObat_Click(object sender, EventArgs e)
        {
            FormObat f = new FormObat();
            f.ShowDialog();
        }

        private void btnKelolaRacikan_Click(object sender, EventArgs e)
        {
            FormRacikan f = new FormRacikan();
            f.ShowDialog();
        }

        private void btnKelolaSuplier_Click(object sender, EventArgs e)
        {
            FormSuplier f = new FormSuplier();
            f.ShowDialog();
        }

        private void btnKelolaTransaksi_Click(object sender, EventArgs e)
        {
            FormTransaksi f = new FormTransaksi();
            f.ShowDialog();
        }

        private void btnKelolaStock_Click(object sender, EventArgs e)
        {
            FormStock f = new FormStock();
            f.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }
    }
}
