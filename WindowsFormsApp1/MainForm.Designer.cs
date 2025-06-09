namespace ManajemenObatApp
{
    partial class MainForm
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnKelolaApoteker = new System.Windows.Forms.Button();
            this.btnKelolaObat = new System.Windows.Forms.Button();
            this.btnKelolaSuplier = new System.Windows.Forms.Button();
            this.btnKelolaTransaksi = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(224, 36);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(319, 46);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Selamat Datang";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(245, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Silakan memilih menu dibawah ini  :";
            // 
            // btnKelolaApoteker
            // 
            this.btnKelolaApoteker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKelolaApoteker.Location = new System.Drawing.Point(269, 137);
            this.btnKelolaApoteker.Name = "btnKelolaApoteker";
            this.btnKelolaApoteker.Size = new System.Drawing.Size(230, 39);
            this.btnKelolaApoteker.TabIndex = 2;
            this.btnKelolaApoteker.Text = "Apoteker";
            this.btnKelolaApoteker.UseVisualStyleBackColor = true;
            this.btnKelolaApoteker.Click += new System.EventHandler(this.btnKelolaApoteker_Click);
            // 
            // btnKelolaObat
            // 
            this.btnKelolaObat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKelolaObat.Location = new System.Drawing.Point(269, 193);
            this.btnKelolaObat.Name = "btnKelolaObat";
            this.btnKelolaObat.Size = new System.Drawing.Size(230, 39);
            this.btnKelolaObat.TabIndex = 3;
            this.btnKelolaObat.Text = "Obat";
            this.btnKelolaObat.UseVisualStyleBackColor = true;
            this.btnKelolaObat.Click += new System.EventHandler(this.btnKelolaObat_Click);
            // 
            // btnKelolaSuplier
            // 
            this.btnKelolaSuplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKelolaSuplier.Location = new System.Drawing.Point(269, 251);
            this.btnKelolaSuplier.Name = "btnKelolaSuplier";
            this.btnKelolaSuplier.Size = new System.Drawing.Size(230, 39);
            this.btnKelolaSuplier.TabIndex = 5;
            this.btnKelolaSuplier.Text = "Suplier";
            this.btnKelolaSuplier.UseVisualStyleBackColor = true;
            this.btnKelolaSuplier.Click += new System.EventHandler(this.btnKelolaSuplier_Click);
            // 
            // btnKelolaTransaksi
            // 
            this.btnKelolaTransaksi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKelolaTransaksi.Location = new System.Drawing.Point(269, 308);
            this.btnKelolaTransaksi.Name = "btnKelolaTransaksi";
            this.btnKelolaTransaksi.Size = new System.Drawing.Size(230, 39);
            this.btnKelolaTransaksi.TabIndex = 6;
            this.btnKelolaTransaksi.Text = "Transaksi";
            this.btnKelolaTransaksi.UseVisualStyleBackColor = true;
            this.btnKelolaTransaksi.Click += new System.EventHandler(this.btnKelolaTransaksi_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Maroon;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLogout.Location = new System.Drawing.Point(317, 364);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(134, 30);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnKelolaTransaksi);
            this.Controls.Add(this.btnKelolaSuplier);
            this.Controls.Add(this.btnKelolaObat);
            this.Controls.Add(this.btnKelolaApoteker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblWelcome);
            this.Name = "MainForm";
            this.Text = "Halaman Utama";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnKelolaApoteker;
        private System.Windows.Forms.Button btnKelolaObat;
        private System.Windows.Forms.Button btnKelolaSuplier;
        private System.Windows.Forms.Button btnKelolaTransaksi;
        private System.Windows.Forms.Button btnLogout;
    }
}