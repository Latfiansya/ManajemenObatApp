using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenObatApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoginForm()); 

            // Untuk menonaktifkan login sementara, panggil MainForm langsung
            // Ganti "A001" dengan ID Apoteker dummy yang Anda inginkan
            //Application.Run(new MainForm("A001"));
            //Application.Run(new FormSuplier());
        }
    }
}
