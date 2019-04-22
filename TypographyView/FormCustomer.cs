using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace TypographyView
{
    public partial class FormCustomer : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormCustomer()
        {
            InitializeComponent();
        }
        private void FormCustomer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CustomerViewModel view = APIClient.GetRequest<CustomerViewModel>("api/Customer/Get/" + id.Value);
                    maskedTextBoxInitials.Text = view.CustomerFIO;
                    if (view != null)
                    {
                        maskedTextBoxInitials.Text = view.CustomerFIO;
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
            if (string.IsNullOrEmpty(maskedTextBoxInitials.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<CustomerBindingModel, bool>("api/Customer/UpdElement", new CustomerBindingModel
                    {
                        Id = id.Value,
                        CustomerFIO = maskedTextBoxInitials.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<CustomerBindingModel, bool>("api/Customer/AddElement", new CustomerBindingModel
                    {
                        CustomerFIO = maskedTextBoxInitials.Text
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
