using System;
using System.Windows.Forms;

namespace Library
{
    public partial class FeedbackForm : Form
    {
        private readonly User currentUser;
        private readonly DatabaseManager dbManager;
        private readonly bool isAdmin;

        public FeedbackForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            dbManager = new DatabaseManager();
            isAdmin = user.IsAdmin;

            listViewFeedback.View = View.Details;
            listViewFeedback.Columns.Add("Дата", 100);
            listViewFeedback.Columns.Add("Сообщение", 200);
            listViewFeedback.Columns.Add("Статус", 100);
            listViewFeedback.Columns.Add("Ответ", 200);

            button1.Text = "Ответить";
            button2.Text = "Отправить";

            txtResponse.Visible = isAdmin;
            button1.Visible = isAdmin;

            if (isAdmin)
            {
                LoadAllFeedback();
            }
            else
            {
                LoadUserFeedback();
            }
        }

        private void LoadUserFeedback()
        {
            var feedbacks = dbManager.GetUserFeedback(currentUser.Id.ToString());
            listViewFeedback.Items.Clear();
            foreach (var feedback in feedbacks)
            {
                var item = new ListViewItem(new[]
                {
                    feedback.CreatedAt.ToString("dd.MM.yyyy"),
                    feedback.Message,
                    feedback.Status,
                    feedback.AdminResponse ?? ""
                });
                item.Tag = feedback;
                listViewFeedback.Items.Add(item);
            }
        }

        private void LoadAllFeedback()
        {
            var feedbacks = dbManager.GetAllFeedback();
            listViewFeedback.Items.Clear();
            foreach (var feedback in feedbacks)
            {
                var item = new ListViewItem(new[]
                {
                    feedback.CreatedAt.ToString("dd.MM.yyyy"),
                    feedback.Message,
                    feedback.Status,
                    feedback.AdminResponse ?? ""
                });
                item.Tag = feedback;
                listViewFeedback.Items.Add(item);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Пожалуйста, введите сообщение");
                return;
            }

            var feedback = new Feedback(currentUser.Id, txtMessage.Text);
            dbManager.AddFeedback(feedback);
            txtMessage.Clear();

            if (isAdmin)
                LoadAllFeedback();
            else
                LoadUserFeedback();
        }

        private void btnRespond_Click(object sender, EventArgs e)
        {
            if (!isAdmin || listViewFeedback.SelectedItems.Count == 0)
                return;

            var feedback = (Feedback)listViewFeedback.SelectedItems[0].Tag;
            var response = txtResponse.Text.Trim();

            if (string.IsNullOrEmpty(response))
            {
                MessageBox.Show("Пожалуйста, введите ответ");
                return;
            }

            dbManager.UpdateFeedbackStatus(feedback.Id.ToString(), "Завершено", response);
            LoadAllFeedback();
            txtResponse.Clear();
        }
    }
}