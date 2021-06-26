using FirebirdSql.Data.FirebirdClient;
using System;
using System.Configuration;
using System.Text;

namespace ExcelDataReader
{
  public  class DB
    {
        public static string Caminho { get; set; }
        public static string Servidor { get; set; }
        public static string Porta { get; set; }


        private static FbConnection Conectar()
        {
            ConfigurationManager.RefreshSection("appSettings");

            Caminho = ConfigurationManager.AppSettings["Banco"].ToString();
            Servidor = ConfigurationManager.AppSettings["Servidor"].ToString();
            Porta = ConfigurationManager.AppSettings["Porta"].ToString();
            string Usuario = "SYSDBA";
            string Senha = "masterkey";

            return new FbConnection("User=" + Usuario + ";Password=" + Senha + ";Database=" + Caminho + ";DataSource=" + Servidor + ";Port=" + Porta);
        }

        public static void TestarConexao()
        {
            using (FbConnection ConexaoFB = Conectar())
            {
                try
                {
                    ConexaoFB.Open();
                }
                catch (FbException ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    ConexaoFB.Dispose();
                }
            }
        }

        public static int ExecutarSQL(string SQL)
        {
            using (FbConnection ConexaoFB = Conectar())
            {
                try
                {
                    ConexaoFB.Open();
                    FbCommand cmd = new FbCommand(SQL, ConexaoFB);
                    return cmd.ExecuteNonQuery();
                }
                catch (FbException ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    ConexaoFB.Dispose();
                }
            }
        }

       

    }
}
