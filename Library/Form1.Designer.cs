namespace Library
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstBooks = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.btnSearchByAuthor = new System.Windows.Forms.Button();
            this.btnSearchByTitle = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.cmbSourceFormat = new System.Windows.Forms.ComboBox();
            this.cmbTargetFormat = new System.Windows.Forms.ComboBox();
            this.grpConvert = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnMarkAsRead = new System.Windows.Forms.Button();
            this.numericRating = new System.Windows.Forms.NumericUpDown();
            this.lblRating = new System.Windows.Forms.Label();
            this.btnRate = new System.Windows.Forms.Button();
            this.chartRating = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.numericCurrentPage = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.grpConvert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrentPage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(86, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(131, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Автор";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(157, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Год";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(638, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "Все книги";
            // 
            // lstBooks
            // 
            this.lstBooks.BackColor = System.Drawing.Color.Silver;
            this.lstBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lstBooks.ForeColor = System.Drawing.Color.Black;
            this.lstBooks.FormattingEnabled = true;
            this.lstBooks.HorizontalScrollbar = true;
            this.lstBooks.ItemHeight = 29;
            this.lstBooks.Location = new System.Drawing.Point(498, 103);
            this.lstBooks.Name = "lstBooks";
            this.lstBooks.Size = new System.Drawing.Size(411, 149);
            this.lstBooks.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Gray;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(243, 266);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(185, 41);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Gray;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnRemove.ForeColor = System.Drawing.Color.Black;
            this.btnRemove.Location = new System.Drawing.Point(243, 308);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(187, 41);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Удалить";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.BackColor = System.Drawing.Color.Gray;
            this.btnPrintAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnPrintAll.ForeColor = System.Drawing.Color.Black;
            this.btnPrintAll.Location = new System.Drawing.Point(498, 355);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(151, 41);
            this.btnPrintAll.TabIndex = 7;
            this.btnPrintAll.Text = "Все книги";
            this.btnPrintAll.UseVisualStyleBackColor = false;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnSearchByAuthor
            // 
            this.btnSearchByAuthor.BackColor = System.Drawing.Color.Gray;
            this.btnSearchByAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnSearchByAuthor.ForeColor = System.Drawing.Color.Black;
            this.btnSearchByAuthor.Location = new System.Drawing.Point(498, 449);
            this.btnSearchByAuthor.Name = "btnSearchByAuthor";
            this.btnSearchByAuthor.Size = new System.Drawing.Size(215, 41);
            this.btnSearchByAuthor.TabIndex = 8;
            this.btnSearchByAuthor.Text = "Поиск по автору";
            this.btnSearchByAuthor.UseVisualStyleBackColor = false;
            this.btnSearchByAuthor.Click += new System.EventHandler(this.btnSearchByAuthor_Click);
            // 
            // btnSearchByTitle
            // 
            this.btnSearchByTitle.BackColor = System.Drawing.Color.Gray;
            this.btnSearchByTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnSearchByTitle.ForeColor = System.Drawing.Color.Black;
            this.btnSearchByTitle.Location = new System.Drawing.Point(498, 402);
            this.btnSearchByTitle.Name = "btnSearchByTitle";
            this.btnSearchByTitle.Size = new System.Drawing.Size(250, 41);
            this.btnSearchByTitle.TabIndex = 9;
            this.btnSearchByTitle.Text = "Поиск по названию";
            this.btnSearchByTitle.UseVisualStyleBackColor = false;
            this.btnSearchByTitle.Click += new System.EventHandler(this.btnSearchByTitle_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Gray;
            this.txtTitle.Location = new System.Drawing.Point(219, 123);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(157, 20);
            this.txtTitle.TabIndex = 10;
            // 
            // txtAuthor
            // 
            this.txtAuthor.BackColor = System.Drawing.Color.Gray;
            this.txtAuthor.Location = new System.Drawing.Point(219, 81);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(157, 20);
            this.txtAuthor.TabIndex = 11;
            // 
            // txtYear
            // 
            this.txtYear.BackColor = System.Drawing.Color.Gray;
            this.txtYear.Location = new System.Drawing.Point(218, 161);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(157, 20);
            this.txtYear.TabIndex = 12;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.Gray;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnImport.ForeColor = System.Drawing.Color.Black;
            this.btnImport.Location = new System.Drawing.Point(27, 581);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(228, 41);
            this.btnImport.TabIndex = 13;
            this.btnImport.Text = "Импорт из файла";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Gray;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnLogout.ForeColor = System.Drawing.Color.Black;
            this.btnLogout.Location = new System.Drawing.Point(1244, 609);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(125, 41);
            this.btnLogout.TabIndex = 14;
            this.btnLogout.Text = "Выйти";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Gray;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(27, 440);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(209, 41);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "Экспорт в файл";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.Gray;
            this.btnConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnConvert.ForeColor = System.Drawing.Color.Black;
            this.btnConvert.Location = new System.Drawing.Point(6, 93);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(127, 41);
            this.btnConvert.TabIndex = 16;
            this.btnConvert.Text = "Конверт";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // cmbFormat
            // 
            this.cmbFormat.BackColor = System.Drawing.Color.Gray;
            this.cmbFormat.FormattingEnabled = true;
            this.cmbFormat.Location = new System.Drawing.Point(219, 232);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(121, 21);
            this.cmbFormat.TabIndex = 17;
            // 
            // cmbSourceFormat
            // 
            this.cmbSourceFormat.BackColor = System.Drawing.Color.Gray;
            this.cmbSourceFormat.FormattingEnabled = true;
            this.cmbSourceFormat.Location = new System.Drawing.Point(6, 25);
            this.cmbSourceFormat.Name = "cmbSourceFormat";
            this.cmbSourceFormat.Size = new System.Drawing.Size(121, 28);
            this.cmbSourceFormat.TabIndex = 18;
            // 
            // cmbTargetFormat
            // 
            this.cmbTargetFormat.BackColor = System.Drawing.Color.Gray;
            this.cmbTargetFormat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmbTargetFormat.FormattingEnabled = true;
            this.cmbTargetFormat.Location = new System.Drawing.Point(6, 59);
            this.cmbTargetFormat.Name = "cmbTargetFormat";
            this.cmbTargetFormat.Size = new System.Drawing.Size(121, 28);
            this.cmbTargetFormat.TabIndex = 19;
            // 
            // grpConvert
            // 
            this.grpConvert.Controls.Add(this.cmbSourceFormat);
            this.grpConvert.Controls.Add(this.cmbTargetFormat);
            this.grpConvert.Controls.Add(this.btnConvert);
            this.grpConvert.Cursor = System.Windows.Forms.Cursors.Cross;
            this.grpConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.grpConvert.ForeColor = System.Drawing.Color.Black;
            this.grpConvert.Location = new System.Drawing.Point(27, 266);
            this.grpConvert.Name = "grpConvert";
            this.grpConvert.Size = new System.Drawing.Size(210, 135);
            this.grpConvert.TabIndex = 20;
            this.grpConvert.TabStop = false;
            this.grpConvert.Text = "Выбор формата";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(108, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 29);
            this.label5.TabIndex = 21;
            this.label5.Text = "Формат";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(28, 487);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 41);
            this.button1.TabIndex = 22;
            this.button1.Text = "Экспорт в Word";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnExportWord_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gray;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(28, 534);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(194, 41);
            this.button2.TabIndex = 23;
            this.button2.Text = "Экспорт в PDF";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnMarkAsRead
            // 
            this.btnMarkAsRead.BackColor = System.Drawing.Color.Gray;
            this.btnMarkAsRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnMarkAsRead.ForeColor = System.Drawing.Color.Black;
            this.btnMarkAsRead.Location = new System.Drawing.Point(498, 258);
            this.btnMarkAsRead.Name = "btnMarkAsRead";
            this.btnMarkAsRead.Size = new System.Drawing.Size(151, 46);
            this.btnMarkAsRead.TabIndex = 24;
            this.btnMarkAsRead.Text = "Прочитана";
            this.btnMarkAsRead.UseVisualStyleBackColor = false;
            this.btnMarkAsRead.Click += new System.EventHandler(this.btnMarkAsRead_Click);
            // 
            // numericRating
            // 
            this.numericRating.Enabled = false;
            this.numericRating.Location = new System.Drawing.Point(824, 355);
            this.numericRating.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericRating.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRating.Name = "numericRating";
            this.numericRating.Size = new System.Drawing.Size(50, 20);
            this.numericRating.TabIndex = 25;
            this.numericRating.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.BackColor = System.Drawing.Color.Silver;
            this.lblRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblRating.Location = new System.Drawing.Point(798, 323);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(101, 29);
            this.lblRating.TabIndex = 26;
            this.lblRating.Text = "Оценка";
            // 
            // btnRate
            // 
            this.btnRate.BackColor = System.Drawing.Color.Gray;
            this.btnRate.Enabled = false;
            this.btnRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.btnRate.Location = new System.Drawing.Point(783, 276);
            this.btnRate.Name = "btnRate";
            this.btnRate.Size = new System.Drawing.Size(132, 43);
            this.btnRate.TabIndex = 27;
            this.btnRate.Text = "Оценить";
            this.btnRate.UseVisualStyleBackColor = false;
            this.btnRate.Click += new System.EventHandler(this.btnRate_Click);
            // 
            // chartRating
            // 
            this.chartRating.Location = new System.Drawing.Point(949, 103);
            this.chartRating.Name = "chartRating";
            this.chartRating.Size = new System.Drawing.Size(349, 149);
            this.chartRating.TabIndex = 28;
            this.chartRating.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Silver;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(1054, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 29);
            this.label6.TabIndex = 29;
            this.label6.Text = "Статистика";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gray;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(1154, 562);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(215, 41);
            this.button3.TabIndex = 30;
            this.button3.Text = "Обратная связь";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnFeedback_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Gray;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(498, 301);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 38);
            this.button4.TabIndex = 31;
            this.button4.Text = "Читаю";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnMarkAsReading_Click);
            // 
            // numericCurrentPage
            // 
            this.numericCurrentPage.Enabled = false;
            this.numericCurrentPage.Location = new System.Drawing.Point(655, 333);
            this.numericCurrentPage.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericCurrentPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCurrentPage.Name = "numericCurrentPage";
            this.numericCurrentPage.Size = new System.Drawing.Size(50, 20);
            this.numericCurrentPage.TabIndex = 32;
            this.numericCurrentPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Silver;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label7.Location = new System.Drawing.Point(650, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 29);
            this.label7.TabIndex = 33;
            this.label7.Text = "Страница";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1381, 682);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericCurrentPage);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grpConvert);
            this.Controls.Add(this.cmbFormat);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnSearchByTitle);
            this.Controls.Add(this.btnSearchByAuthor);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstBooks);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMarkAsRead);
            this.Controls.Add(this.numericRating);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.btnRate);
            this.Controls.Add(this.chartRating);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpConvert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrentPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstBooks;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.Button btnSearchByAuthor;
        private System.Windows.Forms.Button btnSearchByTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.ComboBox cmbSourceFormat;
        private System.Windows.Forms.ComboBox cmbTargetFormat;
        private System.Windows.Forms.GroupBox grpConvert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnMarkAsRead;
        private System.Windows.Forms.NumericUpDown numericRating;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Button btnRate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRating;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.NumericUpDown numericCurrentPage;
        private System.Windows.Forms.Label label7;
    }
}

