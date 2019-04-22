using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace TypographyView
{
    public partial class FormWorker : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormWorker()
        {
            InitializeComponent();
        }
        private void FormWorker_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WorkerViewModel view = APIClient.GetRequest<WorkerViewModel>("api/Worker/Get/" + id.Value);
                    maskedTextBoxInitials.Text = view.WorkerFIO;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBoxInitials.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<WorkerBindingModel, bool>("api/Worker/UpdElement", new WorkerBindingModel
                    {
                        Id = id.Value,
                        WorkerFIO = maskedTextBoxInitials.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<WorkerBindingModel, bool>("api/Worker/AddElement", new WorkerBindingModel
                    {
                        WorkerFIO = maskedTextBoxInitials.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
