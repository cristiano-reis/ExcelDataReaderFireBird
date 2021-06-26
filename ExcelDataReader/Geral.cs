using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelDataReader
{
    class Geral
    {
        public static bool ValidaCampos(Control Grupo, ErrorProvider erro)
        {
            erro.Clear();

            foreach (Control item in Grupo.Controls)
            {
                if (item.Tag != null && item.Tag.ToString() != "" && item.Text.Trim() == "")
                {
                    MessageBox.Show("O campo " + item.Tag.ToString() + " é obrigatório!");
                    item.Focus();
                    erro.SetError(item, "Obrigatório!");
                    return true;
                }
            }

            return false;
        }
    }
}
