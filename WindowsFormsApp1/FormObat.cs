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
using System.Runtime.Caching;

namespace ManajemenObatApp
{
    public partial class FormObat : Form
    {
        DatabaseHelper db;
        private MemoryCache cache = MemoryCache.Default;
        private string cacheKey = "DataObatCache";
        private CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };

        public FormObat()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["ManajemenObatDB"].ConnectionString;
            db = new DatabaseHelper(connectionString);
            EnsureIndexes();
            LoadData();
            LoadSuplier();
        }

        private void EnsureIndexes()
        {
            // Dijalankan sekali per startup
            string sql = @"
    IF NOT EXISTS (SELECT 1 FROM sys.indexes i JOIN sys.objects o ON i.object_id=o.object_id
                   WHERE o.name='stock_obat' AND i.name='idx_stock_obat_id_obat')
      CREATE NONCLUSTERED INDEX idx_stock_obat_id_obat ON stock_obat(id_obat);

    IF NOT EXISTS (SELECT 1 FROM sys.indexes i JOIN sys.objects o ON i.object_id=o.object_id
                   WHERE o.name='stock_obat' AND i.name='idx_stock_obat_id_suplier')
      CREATE NONCLUSTERED INDEX idx_stock_obat_id_suplier ON stock_obat(id_suplier);

    IF NOT EXISTS (SELECT 1 FROM sys.indexes i JOIN sys.objects o ON i.object_id=o.object_id
                   WHERE o.name='transaksi' AND i.name='idx_transaksi_id_apoteker')
      CREATE NONCLUSTERED INDEX idx_transaksi_id_apoteker ON transaksi(id_apoteker);

    IF NOT EXISTS (SELECT 1 FROM sys.indexes i JOIN sys.objects o ON i.object_id=o.object_id
                   WHERE o.name='transaksi' AND i.name='idx_transaksi_id_obat')
      CREATE NONCLUSTERED INDEX idx_transaksi_id_obat ON transaksi(id_obat);

    IF NOT EXISTS (SELECT 1 FROM sys.indexes i JOIN sys.objects o ON i.object_id=o.object_id
                   WHERE o.name='racikan_obat' AND i.name='idx_racikan_id_transaksi')
      CREATE NONCLUSTERED INDEX idx_racikan_id_transaksi ON racikan_obat(id_transaksi);
    ";

            db.ExecuteNonQuery(sql, new SqlParameter[0]);
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = cache.Get(cacheKey) as DataTable;
                if (dt == null)
                {
                    dt = db.ExecuteQuery("SET STATISTICS IO, TIME ON; EXEC tampilkan_data_obat");
                    cache.Set(cacheKey, dt, policy);
                }
                dgvObat.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuplier()
        {
            try
            {
                string query = "EXEC data_suplier";
                DataTable dt = db.ExecuteQuery(query);
                cmbSuplier.DataSource = dt;
                cmbSuplier.DisplayMember = "nama";
                cmbSuplier.ValueMember = "id_suplier";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat suplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInput()
        {
            txtId.Clear();
            txtNama.Clear();
            txtKategori.Clear();
            dtpTanggal.Value = DateTime.Today;
            txtHargaSatuan.Clear();
            txtJumlahStock.Clear();
            cmbSuplier.SelectedIndex = -1; // Reset combobox
        }

        private void RefreshData()
        {
            cache.Remove(cacheKey);
            LoadData();
            ClearInput();
        }
        private void AnalyzeQuery(string sqlQuery)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ManajemenObatDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO");

                try
                {
                    conn.Open();
                    string wrappedQuery = @"
                SET STATISTICS IO ON;
                SET STATISTICS TIME ON;
                " + sqlQuery + @";
                SET STATISTICS IO OFF;
                SET STATISTICS TIME OFF;
            ";

                    using (SqlCommand cmd = new SqlCommand(wrappedQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menjalankan analisis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtKategori.Text == "" ||  txtHargaSatuan.Text == "" || txtJumlahStock.Text == "" || cmbSuplier.SelectedIndex == -1)
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime tglKadaluarsa = dtpTanggal.Value;
            if (tglKadaluarsa < DateTime.Today)

            {
                MessageBox.Show("Tanggal kadaluarsa tidak boleh kurang dari hari ini!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "EXEC tambah_obat @id, @nama, @kategori, @tgl, @harga, @id_suplier, @jumlah";
                SqlParameter[] parameters = {
                    new SqlParameter("@id", txtId.Text),
                    new SqlParameter("@nama", txtNama.Text),
                    new SqlParameter("@kategori", txtKategori.Text),
                    new SqlParameter("@tgl", dtpTanggal.Value),
                    new SqlParameter("@harga", decimal.Parse(txtHargaSatuan.Text)),
                    new SqlParameter("@id_suplier", cmbSuplier.SelectedValue.ToString()),
                    new SqlParameter("@jumlah", int.Parse(txtJumlahStock.Text))
                };
                db.ExecuteNonQuery(query, parameters);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambahkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "" || txtNama.Text == "" || txtKategori.Text == "" ||
        txtJumlahStock.Text == "" || txtHargaSatuan.Text == "" || cmbSuplier.SelectedValue == null)
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin mengubah data obat ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            DateTime tglKadaluarsa = dtpTanggal.Value;
            if (tglKadaluarsa < DateTime.Today)
            {
                MessageBox.Show("Tanggal kadaluarsa tidak boleh kurang dari hari ini!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "EXEC update_obat @id, @nama, @kategori, @tgl, @harga, @suplier, @jumlah";
                SqlParameter[] parameters = {
            new SqlParameter("@id", txtId.Text),
            new SqlParameter("@nama", txtNama.Text),
            new SqlParameter("@kategori", txtKategori.Text),
            new SqlParameter("@tgl", dtpTanggal.Value),
            new SqlParameter("@harga", decimal.Parse(txtHargaSatuan.Text)),
            new SqlParameter("@suplier", cmbSuplier.SelectedValue.ToString()),
            new SqlParameter("@jumlah", int.Parse(txtJumlahStock.Text))
        };
                db.ExecuteNonQuery(query, parameters);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengedit data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Pilih data obat yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data obat ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                string query = "EXEC hapus_obat @id";
                SqlParameter[] parameters = {
            new SqlParameter("@id", txtId.Text)
        };
                db.ExecuteNonQuery(query, parameters);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvObat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvObat.Rows[e.RowIndex];
                    txtId.Text = row.Cells["id_obat"].Value.ToString();
                    txtNama.Text = row.Cells["nama_obat"].Value.ToString();
                    txtKategori.Text = row.Cells["kategori"].Value.ToString();
                    dtpTanggal.Value = Convert.ToDateTime(row.Cells["tgl_kadaluwarsa"].Value);
                    txtHargaSatuan.Text = row.Cells["harga_satuan"].Value.ToString();
                    txtJumlahStock.Text = row.Cells["jumlah_stock"].Value.ToString();
                    cmbSuplier.Text = dgvObat.Rows[e.RowIndex].Cells["suplier"].Value.ToString();
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

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string query = "EXEC tampilkan_data_obat";
            AnalyzeQuery(query);
        }

        private void btnIndexReport_Click(object sender, EventArgs e)
        {
            try
            {
                // ambil data index
                DataTable dt = db.ExecuteQuery("EXEC rpt_index_list");

                // bikin form kustom untuk report
                Form reportForm = new Form();
                reportForm.Text = "Index Report";
                reportForm.StartPosition = FormStartPosition.CenterParent;
                reportForm.Size = new Size(600, 400);   // sesuaikan lebar & tinggi

                // DataGridView untuk menampilkan index
                DataGridView dgv = new DataGridView
                {
                    DataSource = dt,
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };
                reportForm.Controls.Add(dgv);

                // Tombol Close di bawah
                Button btnClose = new Button
                {
                    Text = "Close",
                    Dock = DockStyle.Bottom,
                    Height = 30
                };
                btnClose.Click += (s, ea) => reportForm.Close();
                reportForm.Controls.Add(btnClose);

                // Tampilkan sebagai dialog modal agar bisa kembali ke FormObat setelah ditutup
                reportForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal generate Index Report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}