using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace TypographyView
{
    public partial class FormStorage : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormStorage()
        {
            InitializeComponent();
        }
        private void FormStorage_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StorageViewModel view = APIClient.GetRequest<StorageViewModel>("api/Storage/Get/" + id.Value);
                    if (view != null)
                    {
                        nameTextBox.Text = view.StorageName;
                        dataGridView.DataSource = view.StoragePart;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<StorageBindingModel, bool>("api/Storage/UpdElement", new StorageBindingModel
                    {
                        Id = id.Value,
                        StorageName = nameTextBox.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<StorageBindingModel, bool>("api/Storage/AddElement", new StorageBindingModel
                    {
                        StorageName = nameTextBox.Text
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
