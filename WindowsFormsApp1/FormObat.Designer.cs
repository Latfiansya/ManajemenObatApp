namespace ManajemenObatApp
{
    partial class FormObat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormObat));
            this.btnClear = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.txtKategori = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvObat = new System.Windows.Forms.DataGridView();
            this.txtTanggal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnHapus = new System.Windows.Forms.Button();
            this.cmbSuplier = new System.Windows.Forms.ComboBox();
            this.txtJumlahStock = new System.Windows.Forms.TextBox();
            this.txtHargaSatuan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(170, 293);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(138, 30);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(170, 257);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(138, 30);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(19, 257);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(138, 30);
            this.btnTambah.TabIndex = 18;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // txtKategori
            // 
            this.txtKategori.Location = new System.Drawing.Point(133, 84);
            this.txtKategori.Name = "txtKategori";
            this.txtKategori.Size = new System.Drawing.Size(175, 22);
            this.txtKategori.TabIndex = 17;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(133, 52);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(175, 22);
            this.txtNama.TabIndex = 16;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(133, 19);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(175, 22);
            this.txtId.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Kategori Obat :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Nama Obat :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "ID Obat:";
            // 
            // dgvObat
            // 
            this.dgvObat.AllowDrop = true;
            this.dgvObat.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvObat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObat.Location = new System.Drawing.Point(329, 18);
            this.dgvObat.Name = "dgvObat";
            this.dgvObat.RowHeadersWidth = 51;
            this.dgvObat.RowTemplate.Height = 24;
            this.dgvObat.Size = new System.Drawing.Size(1136, 411);
            this.dgvObat.TabIndex = 11;
            this.dgvObat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvObat_CellContentClick);
            // 
            // txtTanggal
            // 
            this.txtTanggal.Location = new System.Drawing.Point(133, 116);
            this.txtTanggal.Name = "txtTanggal";
            this.txtTanggal.Size = new System.Drawing.Size(175, 22);
            this.txtTanggal.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Tgl Kadaluwarsa :";
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(19, 293);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(138, 30);
            this.btnHapus.TabIndex = 24;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // cmbSuplier
            // 
            this.cmbSuplier.FormattingEnabled = true;
            this.cmbSuplier.Location = new System.Drawing.Point(133, 149);
            this.cmbSuplier.Name = "cmbSuplier";
            this.cmbSuplier.Size = new System.Drawing.Size(175, 24);
            this.cmbSuplier.TabIndex = 25;
            // 
            // txtJumlahStock
            // 
            this.txtJumlahStock.Location = new System.Drawing.Point(133, 182);
            this.txtJumlahStock.Name = "txtJumlahStock";
            this.txtJumlahStock.Size = new System.Drawing.Size(175, 22);
            this.txtJumlahStock.TabIndex = 26;
            // 
            // txtHargaSatuan
            // 
            this.txtHargaSatuan.Location = new System.Drawing.Point(133, 213);
            this.txtHargaSatuan.Name = "txtHargaSatuan";
            this.txtHargaSatuan.Size = new System.Drawing.Size(175, 22);
            this.txtHargaSatuan.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "Pilih Suplier :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "Jumlah Stok";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "Harga Satuan :";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.Location = new System.Drawing.Point(12, 393);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(45, 45);
            this.btnBack.TabIndex = 31;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(19, 329);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(289, 30);
            this.btnAnalyze.TabIndex = 44;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // FormObat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 450);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHargaSatuan);
            this.Controls.Add(this.txtJumlahStock);
            this.Controls.Add(this.cmbSuplier);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.txtTanggal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtKategori);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvObat);
            this.Name = "FormObat";
            this.Text = "Halaman Obat";
            ((System.ComponentModel.ISupportInitialize)(this.dgvObat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.TextBox txtKategori;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvObat;
        private System.Windows.Forms.TextBox txtTanggal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.ComboBox cmbSuplier;
        private System.Windows.Forms.TextBox txtJumlahStock;
        private System.Windows.Forms.TextBox txtHargaSatuan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAnalyze;
    }
}