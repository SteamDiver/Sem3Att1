using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils
{
    public class MessagesUtils
    {
        public static void ShowMessage(string text) {
            MessageBox.Show(text);
        }

        public static void ShowError(string text) {
            MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
