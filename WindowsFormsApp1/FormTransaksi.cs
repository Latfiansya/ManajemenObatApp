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
    public partial class FormTransaksi: Form
    {
        DatabaseHelper db = new DatabaseHelper("Data Source=PREDATOR579;Initial Catalog=ManajemenObat;Integrated Security=True");

        public FormTransaksi()
        {
            InitializeComponent();
            LoadData();

        }

        private void LoadData()
        {
            string query = "SELECT * FROM transaksi";
            DataTable dt = db.ExecuteQueryWithParameters(query, new SqlParameter[0]);
            dgvTransaksi.DataSource = dt;
        }

        private void ClearInput()
        {
            txtIdApoteker.Clear();
            txtIdObat.Clear();
            txtJumlah.Clear();
            txtTotalHarga.Clear();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO transaksi (id_apoteker, id_obat, jumlah, total_harga, tanggal_transaksi) VALUES (@idApt, @idOb, @jml, @harga, @tgl)";
            SqlParameter[] param = {
                new SqlParameter("@idApt", txtIdApoteker.Text),
                new SqlParameter("@idOb", txtIdObat.Text),
                new SqlParameter("@jml", int.Parse(txtJumlah.Text)),
                new SqlParameter("@harga", decimal.Parse(txtTotalHarga.Text)),
                new SqlParameter("@tgl", dtTanggalTransaksi.Value)
            };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["id_transaksi"].Value);
            string query = "UPDATE transaksi SET id_apoteker = @idApt, id_obat = @idOb, jumlah = @jml, total_harga = @harga, tanggal_transaksi = @tgl WHERE id_transaksi = @id";
            SqlParameter[] param = {
                    new SqlParameter("@idApt", txtIdApoteker.Text),
                    new SqlParameter("@idOb", txtIdObat.Text),
                    new SqlParameter("@jml", int.Parse(txtJumlah.Text)),
                    new SqlParameter("@harga", decimal.Parse(txtTotalHarga.Text)),
                    new SqlParameter("@tgl", dtTanggalTransaksi.Value),
                    new SqlParameter("@id", id)
                };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvTransaksi.SelectedRows[0].Cells["id_transaksi"].Value);
            string query = "DELETE FROM transaksi WHERE id_transaksi = @id";
            SqlParameter[] param = {
                    new SqlParameter("@id", id)
                };
            db.ExecuteNonQuery(query, param);
            LoadData();
            ClearInput();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void dgvTransaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtIdApoteker.Text = dgvTransaksi.Rows[e.RowIndex].Cells["id_apoteker"].Value.ToString();
                txtIdObat.Text = dgvTransaksi.Rows[e.RowIndex].Cells["id_obat"].Value.ToString();
                txtJumlah.Text = dgvTransaksi.Rows[e.RowIndex].Cells["jumlah"].Value.ToString();
                txtTotalHarga.Text = dgvTransaksi.Rows[e.RowIndex].Cells["total_harga"].Value.ToString();
                dtTanggalTransaksi.Value = Convert.ToDateTime(dgvTransaksi.Rows[e.RowIndex].Cells["tanggal_transaksi"].Value);
            }
        }
    }
}
