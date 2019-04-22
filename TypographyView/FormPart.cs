using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace TypographyView
{
    public partial class FormPart : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormPart()
        {
            InitializeComponent();
        }
        private void FormPart_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PartViewModel view = APIClient.GetRequest<PartViewModel>("api/Part/Get/" + id.Value); ;
                    if (view != null)
                    {
                        textBoxName.Text = view.PartName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,  MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                   APIClient.PostRequest<PartBindingModel, bool>("api/Part/UpdElement", new PartBindingModel
                    {
                        Id = id.Value,
                        PartName = textBoxName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<PartBindingModel, bool>("api/Part/AddElement", new PartBindingModel
                    {
                        PartName = textBoxName.Text
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
