using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libTask2;

namespace Task2GUI
{
    public class DgvConvert
    {
        public static List<Programmer> DgvToList(DataGridView dgv)
        {
            List<Programmer> prList = new List<Programmer>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                var surname = row.Cells["InputDGV_Surname"].Value.ToString();
                var progsCount = Convert.ToInt32(row.Cells["InputDGV_ProgsCount"].Value);
                var langCount = Convert.ToInt32(row.Cells["InputDGV_LangCount"].Value);
                var successProgs = Convert.ToInt32(row.Cells["InputDGV_SuccessProgs"].Value);
                Programmer pr;
                pr = successProgs ==-1 ? new Programmer(surname, progsCount, langCount) : new CustomProgrammer(surname, progsCount, langCount, successProgs);
                prList.Add(pr);
            }
            return prList;
        }

        public static void ListToDgv(DataGridView dgv, List<Programmer> programmers)
        {
            dgv.Rows.Clear();

            foreach (Programmer pr in programmers)
            {
                dgv.Rows.Add();
                DataGridViewRow lastRow = dgv.Rows[dgv.RowCount - 1];
                lastRow.Cells["Type"].Value = pr.GetType().Name;
                lastRow.Cells["InputDGV_ProgsCount"].Value = pr.ProgramCount;
                lastRow.Cells["InputDGV_LangCount"].Value = pr.LangCount;
                lastRow.Cells["InputDGV_Surname"].Value = pr.Surname;
                lastRow.Cells["InputDGV_Q"].Value = pr.Q();
                if (pr.GetType() == typeof(CustomProgrammer))
                    lastRow.Cells["InputDGV_SuccessProgs"].Value = ((CustomProgrammer) pr).P;
                else
                {
                    //lastRow.Cells["InputDGV_SuccessProgs"].Value = -1;
                }
            }
        }
    }
}
