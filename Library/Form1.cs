using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    public partial class Form1 : Form
    {
        private User currentUser;
        private readonly BookManager bookManager;

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
        }

        private void EnableAdminControls(bool isAdmin)
        {
            if (btnImport != null) btnImport.Enabled = isAdmin;
            if (btnRemove != null) btnRemove.Enabled = isAdmin;
            if (btnExport != null) btnExport.Enabled = isAdmin;
            if (grpConvert != null) grpConvert.Enabled = isAdmin;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bookManager.ImportBooksFromCsv(openFileDialog.FileName);
                    MessageBox.Show("Книги успешно импортированы!");
                    UpdateBooksList();
                }
            }
        }

        private void UpdateBooksList()
        {
            lstBooks.Items.Clear();
            var books = bookManager.GetAllBooks();
            foreach (var book in books)
            {
                lstBooks.Items.Add(book);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateBooksList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string author = txtAuthor.Text;
            if (int.TryParse(txtYear.Text, out int year))
            {
                if (Enum.TryParse<BookFormat>(cmbFormat.SelectedItem.ToString(), out BookFormat format))
                {
                    bookManager.AddBook(title, author, year, format);
                    MessageBox.Show("Книга добавлена!");
                    UpdateBooksList();
                }
            }
            else
            {
                MessageBox.Show("Введите корректный год издания.");
            }

            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                bool success = bookManager.RemoveBook(selectedBook.Id);
                if (success)
                {
                    MessageBox.Show("Книга удалена!");
                    UpdateBooksList();
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для удаления.");
            }
            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();
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
    }
}

