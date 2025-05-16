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
    public partial class FormStock: Form
    {
        DatabaseHelper db;
        public FormStock()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["ManajemenObatDB"].ConnectionString;
            db = new DatabaseHelper(connectionString);
            LoadData();

        }
        private void LoadData()
        {
            string query = @"SELECT so.id_stock, o.nama AS NamaObat, s.nama AS NamaSuplier, so.jumlah, so.tanggal_terakhir_diperbarui
                             FROM stock_obat so
                             JOIN obat o ON so.id_obat = o.id_obat
                             JOIN suplier s ON so.id_suplier = s.id_suplier";

            dgvStock.DataSource = db.ExecuteQueryWithParameters(query, new SqlParameter[] { });
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStock.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data stok terlebih dahulu.");
                return;
            }

            int id_stock = Convert.ToInt32(dgvStock.SelectedRows[0].Cells["id_stock"].Value);
            int jumlahBaru;

            if (!int.TryParse(txtJumlah.Text, out jumlahBaru) || jumlahBaru < 0)
            {
                MessageBox.Show("Jumlah harus angka positif.");
                return;
            }

            string query = "UPDATE stock_obat SET jumlah = @jumlah, tanggal_terakhir_diperbarui = GETDATE() WHERE id_stock = @id";
            SqlParameter[] parameters = {
                new SqlParameter("@jumlah", jumlahBaru),
                new SqlParameter("@id", id_stock)
            };

            int result = db.ExecuteNonQuery(query, parameters);
            if (result > 0)
            {
                MessageBox.Show("Stok berhasil diperbarui.");
                LoadData();
                txtJumlah.Clear();
            }
        }
    }
}
