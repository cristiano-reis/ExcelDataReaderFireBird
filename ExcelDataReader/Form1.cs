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
            try
            {

                var stream = File.Open(path, FileMode.Open, FileAccess.Read);
                var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet();
                var tables = result.Tables.Cast<DataTable>();
                foreach (DataTable table in tables)
                {
                    dataGridView1.DataSource = table;
                }

                lbRegistros.Text = "Linhas: " + dataGridView1.Rows.Count.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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
            
            int cont = 0;
            double custo = 0;
            string ean = "";
            string sql = "";
            string sql2 = "";
            if (dataGridView1.RowCount ==0)
            {
                MessageBox.Show("Escolha um arquivo em excel para  atualizar!");
                return;
            }
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Application.DoEvents();
                    cont++;


                    lbContador.Text = cont.ToString();
                    ean = row.Cells[0].Value.ToString();
                    custo = Convert.ToDouble(row.Cells[1].Value.ToString());

                    sql = "SELECT B.FK_PRODUTO FROM PRODUTO_CODIGO_BARRA B  WHERE B.CODIGO_BARRA  = '" + ean + "'";
                    string id = DB.Buscar(sql).Rows[0][0].ToString();

                     sql2 = " UPDATE PRODUTO P  SET P.CUSTO = '" + custo.ToString("N5").Replace(",", ".") + "' WHERE P.ID = '" + id +"'";
                    DB.ExecutarSQL(sql2);

                }
                if (cont == dataGridView1.Rows.Count)
                    MessageBox.Show("Concluido");
            }
            catch (Exception ex)
            {
                FrmError error = new FrmError();
                error.txtError.Text = "Linha no arquivo excel:\n\r"+ cont + "\n\rcomando 1:\n\r" +sql +"\n\rcomando 2:" +sql2+"\n\r\n\r"+ex.Message;
                error.ShowDialog();
              
            }   
           
       
       
        }
     

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
