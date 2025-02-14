using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Library
{
    public partial class Form1 : Form
    {
        private User currentUser;
        private readonly BookManager bookManager;
        private PictureBox pictureBoxQR;

        public Form1(User user = null)
        {
            InitializeComponent();
            bookManager = new BookManager();

            // Заполняем комбобоксы форматами
            cmbFormat.Items.AddRange(Enum.GetNames(typeof(BookFormat)));
            cmbSourceFormat.Items.AddRange(Enum.GetNames(typeof(BookFormat)));
            cmbTargetFormat.Items.AddRange(Enum.GetNames(typeof(BookFormat)));
            
            if (cmbFormat.Items.Count > 0) cmbFormat.SelectedIndex = 0;

            if (user != null)
            {
                currentUser = user;
                EnableAdminControls(user.IsAdmin);
            }

            btnImport.Click += btnImport_Click;
            btnExport.Click += btnExport_Click;

            InitializeAdditionalControls();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Настраиваем Chart
            chartRating.Series.Clear();
            var series = new Series("Рейтинг");
            series.ChartType = SeriesChartType.Column;
            chartRating.Series.Add(series);
            
            // Добавляем обработчик выбора книги
            lstBooks.SelectedIndexChanged += lstBooks_SelectedIndexChanged;
        }

        private void EnableAdminControls(bool isAdmin)
        {
            if (btnImport != null) btnImport.Enabled = isAdmin;
            if (btnRemove != null) btnRemove.Enabled = isAdmin;
            if (btnExport != null) btnExport.Enabled = isAdmin;
            if (grpConvert != null) grpConvert.Enabled = isAdmin;
        }

        private void InitializeAdditionalControls()
        {
            // QR Code
            pictureBoxQR = new PictureBox();
            pictureBoxQR.Location = new System.Drawing.Point(450, 500);
            pictureBoxQR.Size = new System.Drawing.Size(200, 150);
            pictureBoxQR.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxQR.Image = bookManager.GenerateQRCode();
            Controls.Add(pictureBoxQR);
        }

        private void UpdateBooksList()
        {
            try
            {
                lstBooks.Items.Clear();
                var books = bookManager.GetAllBooks();
                foreach (var book in books)
                {
                    lstBooks.Items.Add(book);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении списка книг: {ex.Message}");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        bookManager.ImportBooksFromCsv(openFileDialog.FileName);
                        MessageBox.Show("Книги успешно импортированы! Дубликаты были пропущены.");
                        UpdateBooksList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при импорте: {ex.Message}");
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author))
            {
                MessageBox.Show("Пожалуйста, заполните название и автора книги.");
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("Пожалуйста, введите корректный год издания.");
                return;
            }

            if (cmbFormat.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите формат книги.");
                return;
            }

            if (Enum.TryParse<BookFormat>(cmbFormat.SelectedItem.ToString(), out BookFormat format))
            {
                try
                {
                    if (bookManager.AddBook(title, author, year, format))
                    {
                        MessageBox.Show("Книга успешно добавлена!");
                        ClearInputFields();
                        UpdateBooksList();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении книги.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                try
                {
                    if (bookManager.RemoveBook(selectedBook.Id))
                    {
                        MessageBox.Show("Книга успешно удалена!");
                        UpdateBooksList();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении книги.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу для удаления.");
            }
        }

        private void btnSearchByTitle_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Введите название книги для поиска.");
                return;
            }

            var results = bookManager.FindBookByName(title);
            DisplayResults(results);
        }

        private void btnSearchByAuthor_Click(object sender, EventArgs e)
        {
            string author = txtAuthor.Text.Trim();
            if (string.IsNullOrEmpty(author))
            {
                MessageBox.Show("Введите имя автора для поиска.");
                return;
            }

            var results = bookManager.FindBookByAuthor(author);
            DisplayResults(results);
        }

        private void DisplayResults(List<Book> results)
        {
            lstBooks.Items.Clear();
            foreach (var book in results)
            {
                lstBooks.Items.Add(book);
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            UpdateBooksList();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы вышли из системы.");
            Application.Exit();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                        switch (extension)
                        {
                            case ".csv":
                                bookManager.ExportBooksToCsv(saveFileDialog.FileName);
                                break;
                            case ".txt":
                                bookManager.ExportBooksToTxt(saveFileDialog.FileName);
                                break;
                            default:
                                MessageBox.Show("Неподдерживаемый формат файла.");
                                return;
                        }
                        MessageBox.Show("Книги успешно экспортированы!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}");
                    }
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                if (Enum.TryParse<BookFormat>(cmbSourceFormat.SelectedItem.ToString(), out BookFormat sourceFormat) &&
                    Enum.TryParse<BookFormat>(cmbTargetFormat.SelectedItem.ToString(), out BookFormat targetFormat))
                {
                    if (!selectedBook.AvailableFormats.Contains(sourceFormat))
                    {
                        MessageBox.Show("Выбранная книга недоступна в исходном формате!");
                        return;
                    }

                    selectedBook.AvailableFormats.Add(targetFormat);
                    MessageBox.Show($"Книга сконвертирована из {sourceFormat} в {targetFormat}");
                    UpdateBooksList();
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для конвертации.");
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bookManager.ExportToPdf(saveFileDialog.FileName);
                    MessageBox.Show("PDF exported successfully!");
                }
            }
        }

        private void btnExportWord_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Word files (*.docx)|*.docx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bookManager.ExportToWord(saveFileDialog.FileName);
                    MessageBox.Show("Word document exported successfully!");
                }
            }
        }

        private void ClearInputFields()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();
            cmbFormat.SelectedIndex = -1;
        }

        private void lstBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                // Получаем статистику для выбранной книги
                var stats = bookManager.GetBookStatistics(selectedBook.Id.ToString());
                UpdateRatingChart(stats);
                
                // Проверяем, прочитана ли книга текущим пользователем
                bool isRead = bookManager.IsBookReadByUser(currentUser.Id.ToString(), selectedBook.Id.ToString());
                numericRating.Enabled = isRead;
                btnRate.Enabled = isRead;

                // Получаем прогресс чтения
                var currentPage = bookManager.GetBookReadingProgress(currentUser.Id.ToString(), selectedBook.Id.ToString());
                if (currentPage.HasValue)
                {
                    numericCurrentPage.Value = currentPage.Value;
                    numericCurrentPage.Enabled = true;
                }
                else
                {
                    numericCurrentPage.Value = numericCurrentPage.Minimum;
                    numericCurrentPage.Enabled = false;
                }
            }
        }

        private void UpdateRatingChart(Dictionary<string, double> stats)
        {
            chartRating.Series.Clear();
            var series = new Series("Рейтинг");
            series.ChartType = SeriesChartType.Column;

            // Добавляем данные в диаграмму
            series.Points.AddXY("Средняя оценка", stats["average_rating"]);
            series.Points.AddXY("Прочитали", stats["completed_count"]);
            series.Points.AddXY("Читают", stats["reading_count"]);
            series.Points.AddXY("Хотят прочитать", stats["want_to_read_count"]);

            chartRating.Series.Add(series);
            chartRating.Visible = true;
        }

        private void btnMarkAsRead_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                try
                {
                    bookManager.UpdateBookStatus(
                        currentUser.Id.ToString(),
                        selectedBook.Id.ToString(),
                        "completed"
                    );
                    MessageBox.Show("Книга отмечена как прочитанная!");
                    numericRating.Enabled = true;
                    btnRate.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении статуса книги: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу.");
            }
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                try
                {
                    int rating = (int)numericRating.Value;
                    bookManager.UpdateBookStatus(
                        currentUser.Id.ToString(),
                        selectedBook.Id.ToString(),
                        "completed",
                        rating
                    );
                    MessageBox.Show("Оценка сохранена!");
                    
                    // Обновляем статистику
                    var stats = bookManager.GetBookStatistics(selectedBook.Id.ToString());
                    UpdateRatingChart(stats);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении оценки: {ex.Message}");
                }
            }
        }

        private void btnFeedback_Click(object sender, EventArgs e)
        {
            var feedbackForm = new FeedbackForm(currentUser);
            feedbackForm.ShowDialog();
        }

        private void btnMarkAsReading_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                try
                {
                    bookManager.UpdateBookStatus(
                        currentUser.Id.ToString(),
                        selectedBook.Id.ToString(),
                        "reading",
                        null,
                        (int)numericCurrentPage.Value
                    );
                    MessageBox.Show("Статус книги обновлен!");
                    numericCurrentPage.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении статуса книги: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу.");
            }
        }
    }
}

