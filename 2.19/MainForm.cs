using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using libTask2;
using Utils;

namespace Task2GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DataGridViewUtils.InitGridForArr(InputDGV, 40, false, true, true, true, false);
        }

        private void MainMenuFileOpen_Click_1(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = OpenFileDialog.FileName;

                    List<Programmer> prList = FilesUtils.ReadFromFile(path);
                    DgvConvert.ListToDgv(InputDGV, prList);

                    MessagesUtils.ShowMessage("Данные загружены из файла");
                }
                catch (Exception ex)
                {
                    MessagesUtils.ShowError("Ошибка чтения из файла");
                }
            }
        }

        private void MainMenuFileSave_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = SaveFileDialog.FileName;

                    List<Programmer> list = DgvConvert.DgvToList(InputDGV);
                    FilesUtils.SaveToFile(path, list);

                    MessagesUtils.ShowMessage("Данные сохранены в файл");
                }
                catch (Exception ex)
                {
                    MessagesUtils.ShowError("Ошибка сохранения в файл");
                }
            }
        }

        private void InputCompositionsDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = InputDGV.Rows[e.RowIndex];
            var type = row.Cells["Type"].Value?.ToString();
            var surname = row.Cells["inputDGV_Surname"].Value?.ToString();
            var progsCount = Convert.ToInt32(row.Cells["inputDGV_ProgsCount"].Value);
            var langCount = Convert.ToInt32(row.Cells["inputDGV_LangCount"].Value);

            if (progsCount >=0 && langCount >= 0)
            {
                if (type == "Programmer")
                {
                    var tv = new Programmer(surname, progsCount, langCount);
                    row.Cells["InputDGV_SuccessProgs"].Value = "";
                    row.Cells["inputDGV_Q"].Value = tv.Q();
                }
                else
                {
                    var successProgs = Convert.ToInt32(row.Cells["inputDGV_SuccessProgs"].Value);
                    if (successProgs >=0)
                    {
                        var tv = new CustomProgrammer(surname, progsCount, langCount, successProgs);
                        row.Cells["inputDGV_Q"].Value = tv.Q();
                    }
                }
            }
        }
    }
}
