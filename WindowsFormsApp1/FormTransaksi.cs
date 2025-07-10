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
using System.Configuration;
using System.Runtime.Caching;


namespace ManajemenObatApp
{
    public partial class FormTransaksi: Form
    {
        private int selectedIdTransaksi = -1; //variabel internal untuk menampung nilai Id
        DatabaseHelper db;
        private MemoryCache cache = MemoryCache.Default;
        private string cacheKey = "DataTransaksiCache";
        private CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
        public FormTransaksi()
        {
            InitializeComponent();
            var kon = new ManajemenObatApp.Helpers.Koneksi();
            string connectionString = kon.connectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show(
                    "Gagal membangun connection string. Pastikan jaringan dan konfigurasi benar.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            db = new DatabaseHelper(connectionString);
            LoadData();
            LoadApoteker();
            LoadObat();
            SetupHandlers();

        }

        private void LoadData()
        {
            try
            {
                DataTable dt = cache.Get(cacheKey) as DataTable;
                if (dt == null)
                {
                    dt = db.ExecuteQuery("SET STATISTICS IO, TIME ON; EXEC tampilkan_data_transaksi");
                    cache.Set(cacheKey, dt, policy);
                }
                dgvTransaksi.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
            
        }

        private void LoadApoteker()
        {
            try
            {
                string query = "EXEC data_apoteker";
                DataTable dt = db.ExecuteQuery(query);
                cmbApoteker.DataSource = dt;
                cmbApoteker.DisplayMember = "nama";
                cmbApoteker.ValueMember = "id_apoteker";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat apoteker: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadObat()
        {
            try
            {
                var dt = db.ExecuteQuery("EXEC data_obat");
                cmbObat.DataSource = dt;
                cmbObat.DisplayMember = "nama";
                cmbObat.ValueMember = "id_obat";
                cmbObat.Tag = dt; // simpan DataTable untuk harga

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat obat: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshData()
        {
            cache.Remove(cacheKey);
            LoadData();
            ClearInput();
        }
        private void AnalyzeQuery(string sqlQuery)
        {
            string connectionString = db.ConnectionString;
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

        private void SetupHandlers()
        {
            txtJumlah.TextChanged += RecalculateTotal;
            cmbObat.SelectedIndexChanged += RecalculateTotal;
        }

        private void RecalculateTotal(object sender, EventArgs e)
        {
            if (cmbObat.SelectedValue == null) return;

            if (!int.TryParse(txtJumlah.Text, out int jumlah))
            {
                txtTotalHarga.Text = "";
                return;
            }

            var dt = cmbObat.Tag as DataTable;
            if (dt == null) return;

            var selectedId = cmbObat.SelectedValue.ToString();
            var foundRow = dt.AsEnumerable()
                .FirstOrDefault(r => r.Field<string>("id_obat") == selectedId);
            if (foundRow == null) return;

            decimal harga = foundRow.Field<decimal>("harga_satuan");
            txtTotalHarga.Text = (harga * jumlah).ToString("N2");
        }


        private void ClearInput()
        {
            cmbApoteker.SelectedIndex = -1;
            txtJumlah.Clear();
            txtTotalHarga.Clear();
            cmbObat.SelectedIndex = -1; 
            txtResep.Clear();

            selectedIdTransaksi = -1;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (cmbApoteker.SelectedIndex == -1 || cmbObat.SelectedIndex == -1 || txtJumlah.Text == "" || txtResep.Text == "")
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int jumlah = int.Parse(txtJumlah.Text);
                if (jumlah <= 0)
                {
                    MessageBox.Show("Jumlah harus lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "EXEC tambah_transaksi @id_apoteker, @id_obat, @jumlah, @resep_dokter";
                SqlParameter[] parameters = {
                    new SqlParameter("@id_apoteker", cmbApoteker.SelectedValue.ToString()),
                    new SqlParameter("@id_obat", cmbObat.SelectedValue.ToString()),
                    new SqlParameter("@jumlah", jumlah),
                    new SqlParameter("@resep_dokter", txtResep.Text)
                };

                db.ExecuteNonQuery(query, parameters);
                RefreshData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Jumlah harus berupa angka.", "Kesalahan Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambahkan transaksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedIdTransaksi == -1)
            {
                MessageBox.Show("Pilih data transaksi yang ingin diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbApoteker.SelectedIndex == -1 || cmbObat.SelectedIndex == -1 || txtJumlah.Text == "" || txtResep.Text == "")
            {
                MessageBox.Show("Isi semua data dengan benar!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int jumlah = int.Parse(txtJumlah.Text);
                if (jumlah <= 0)
                {
                    MessageBox.Show("Jumlah harus lebih dari 0!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "EXEC update_transaksi @id_transaksi, @id_apoteker, @id_obat, @jumlah, @resep_dokter";
                SqlParameter[] parameters = {
            new SqlParameter("@id_transaksi", selectedIdTransaksi),
            new SqlParameter("@id_apoteker", cmbApoteker.SelectedValue.ToString()),
            new SqlParameter("@id_obat", cmbObat.SelectedValue.ToString()),
            new SqlParameter("@jumlah", jumlah),
            new SqlParameter("@resep_dokter", txtResep.Text)
        };

                db.ExecuteNonQuery(query, parameters);
                RefreshData();
            }
            catch (FormatException)
            {
                MessageBox.Show("Jumlah harus berupa angka.", "Kesalahan Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengubah transaksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedIdTransaksi == -1)
            {
                MessageBox.Show("Pilih transaksi yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus transaksi ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                string query = "EXEC hapus_transaksi @id_transaksi";
                SqlParameter[] parameters = {
            new SqlParameter("@id_transaksi", selectedIdTransaksi )
        };
                db.ExecuteNonQuery(query, parameters);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus transaksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void dgvTransaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvTransaksi.Rows[e.RowIndex];
                    selectedIdTransaksi = Convert.ToInt32(row.Cells["id_transaksi"].Value);
                    cmbApoteker.Text = dgvTransaksi.Rows[e.RowIndex].Cells["Nama_Apoteker"].Value.ToString();
                    cmbObat.Text = dgvTransaksi.Rows[e.RowIndex].Cells["Nama_Obat"].Value.ToString();
                    txtJumlah.Text = dgvTransaksi.Rows[e.RowIndex].Cells["jumlah"].Value.ToString();
                    txtTotalHarga.Text = dgvTransaksi.Rows[e.RowIndex].Cells["total_harga"].Value.ToString();
                    txtResep.Text = dgvTransaksi.Rows[e.RowIndex].Cells["Resep_Dokter"].Value.ToString();
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            FormExport f = new FormExport();
            f.ShowDialog();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            string query = "EXEC tampilkan_data_transaksi";
            AnalyzeQuery(query);
        }
    }
}
