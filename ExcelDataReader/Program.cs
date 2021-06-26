using System;
using System.Windows.Forms;

namespace ExcelDataReader
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            try
            {
                DB.TestarConexao();
            }
            catch
            {
                frmConfigDados config = new frmConfigDados();
                config.ShowDialog();
                Application.Exit();
            }

            Application.Run(new Form1());

        }
    }
}
