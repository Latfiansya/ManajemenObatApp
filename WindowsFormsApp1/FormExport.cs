using ManajemenObatApp.Helpers;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenObatApp
{
    public partial class FormExport: Form
    {
        DatabaseHelper db;
        public FormExport()
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
        }

        private void FormExport_Load(object sender, EventArgs e)
        {
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            try
            {
                // Eksekusi query seperti pada LoadData FormTransaksi
                string query = "EXEC tampilkan_data_transaksi";
                DataTable dt = db.ExecuteQuery(query);

                // Buat ReportDataSource, pastikan nama dataset sesuai dengan yang diatur di file .rdlc
                ReportDataSource rds = new ReportDataSource("MO_DataSet", dt);

                // Bersihkan sumber data sebelumnya dan setel ulang
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Path ke file RDLC (pastikan path ini benar)
                reportViewer1.LocalReport.ReportPath = @"D:\MATERI\SEMESTER IV\PABD\ManajemenObatApp\ManajemenObatApp\WindowsFormsApp1\ManajemenReportReport.rdlc";

                // Refresh report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
