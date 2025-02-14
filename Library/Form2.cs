using System;
using System.Windows.Forms;

namespace Library
{
    public partial class Form2 : Form
    {
        private readonly UserManager userManager;

        public Form2()
        {
            InitializeComponent();
            userManager = new UserManager();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.");
                return;
            }

            try
            {
                var user = userManager.Authenticate(username, password);
                if (user != null)
                {
                    var mainForm = new Form1(user);
                    this.Hide();
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при авторизации: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
