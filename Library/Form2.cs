using System;
using System.Windows.Forms;

namespace Library
{
    public partial class Form2 : Form
    {
        private UserManager userManager;

        public Form2()
        {
            InitializeComponent();
            userManager = new UserManager();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            User user = userManager.Authenticate(username, password);

            if (user != null)
            {
                MessageBox.Show("Вход выполнен!");

                // Открываем Form1 и передаем текущего пользователя
                Form1 mainForm = new Form1(user);
                mainForm.Show();

                this.Hide(); // Скрываем форму авторизации
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }
    }
}
