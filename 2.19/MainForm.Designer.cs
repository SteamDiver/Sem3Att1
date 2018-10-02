namespace Task2GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.InputDGV = new System.Windows.Forms.DataGridView();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.InputDGV_Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputDGV_ProgsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputDGV_LangCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputDGV_SuccessProgs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputDGV_Q = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.InputDGV)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputDGV
            // 
            this.InputDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InputDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.InputDGV_Surname,
            this.InputDGV_ProgsCount,
            this.InputDGV_LangCount,
            this.InputDGV_SuccessProgs,
            this.InputDGV_Q});
            this.InputDGV.Location = new System.Drawing.Point(31, 48);
            this.InputDGV.Margin = new System.Windows.Forms.Padding(4);
            this.InputDGV.Name = "InputDGV";
            this.InputDGV.Size = new System.Drawing.Size(992, 348);
            this.InputDGV.TabIndex = 1;
            this.InputDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.InputCompositionsDGV_CellEndEdit);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1089, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFileOpen,
            this.MainMenuFileSave});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // MainMenuFileOpen
            // 
            this.MainMenuFileOpen.Name = "MainMenuFileOpen";
            this.MainMenuFileOpen.Size = new System.Drawing.Size(158, 26);
            this.MainMenuFileOpen.Text = "Открыть";
            this.MainMenuFileOpen.Click += new System.EventHandler(this.MainMenuFileOpen_Click_1);
            // 
            // MainMenuFileSave
            // 
            this.MainMenuFileSave.Name = "MainMenuFileSave";
            this.MainMenuFileSave.Size = new System.Drawing.Size(158, 26);
            this.MainMenuFileSave.Text = "Сохранить";
            this.MainMenuFileSave.Click += new System.EventHandler(this.MainMenuFileSave_Click);

            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Items.AddRange(new object[] {
            "Programmer",
            "CustomProgrammer"});
            this.Type.Name = "Type";
            // 
            // InputDGV_Surname
            // 
            this.InputDGV_Surname.HeaderText = "Surname";
            this.InputDGV_Surname.Name = "InputDGV_Surname";
            this.InputDGV_Surname.Width = 200;
            // 
            // InputDGV_ProgsCount
            // 
            this.InputDGV_ProgsCount.HeaderText = "ProgsCount";
            this.InputDGV_ProgsCount.Name = "InputDGV_ProgsCount";
            // 
            // InputDGV_LangCount
            // 
            this.InputDGV_LangCount.HeaderText = "LangCount";
            this.InputDGV_LangCount.Name = "InputDGV_LangCount";
            // 
            // InputDGV_SuccessProgs
            // 
            this.InputDGV_SuccessProgs.HeaderText = "SuccessProgs";
            this.InputDGV_SuccessProgs.Name = "InputDGV_SuccessProgs";
            // 
            // InputDGV_Q
            // 
            this.InputDGV_Q.HeaderText = "Q";
            this.InputDGV_Q.Name = "InputDGV_Q";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 479);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.InputDGV);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "2.19";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InputDGV)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView InputDGV;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileOpen;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileSave;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDGV_Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDGV_ProgsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDGV_LangCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDGV_SuccessProgs;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputDGV_Q;
    }
}

