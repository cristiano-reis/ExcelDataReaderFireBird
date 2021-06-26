using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ExcelDataReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            
        }

        public void ExcelFileReader(string path)
        {
            

            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            var result = reader.AsDataSet();
            var tables = result.Tables.Cast<DataTable>();
            foreach (DataTable table in tables)
            {
                dataGridView1.DataSource = table;
            }
        }

        private void btnArquivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Arquivo Excel";
            openFileDialog1.Filter = "Excel xls|*.xls ;*.xlsx";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtArquivo.Text = openFileDialog1.FileName.ToString();
           
                  ExcelFileReader(txtArquivo.Text);

            }



          
        }

        private void btnServidor_Click(object sender, EventArgs e)
        {
            frmConfigDados config = new frmConfigDados();
            config.ShowDialog();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
