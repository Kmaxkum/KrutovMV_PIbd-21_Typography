using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TypographyView
{
    public partial class FormBooking : Form
    {
        public FormBooking()
        {
            InitializeComponent();
        }
        private void FormBooking_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = APIClient.GetRequest<List<CustomerViewModel>>("api/Customer/GetList");
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomerFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<ItemViewModel> listP = APIClient.GetRequest<List<ItemViewModel>>("api/Item/GetList");
                if (listP != null)
                {
                    comboBoxItem.DisplayMember = "ItemName";
                    comboBoxItem.ValueMember = "Id";
                    comboBoxItem.DataSource = listP;
                    comboBoxItem.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxItem.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxItem.SelectedValue);
                    ItemViewModel item = APIClient.GetRequest<ItemViewModel>("api/Item/Get/" + id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxTotal.Text = (count * item.Cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void comboBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxItem.SelectedValue == null)
            {
                MessageBox.Show("Выберите печатное изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<BookingBindingModel, bool>("api/Main/CreateOrder", new BookingBindingModel
                {
                    CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    ItemId = Convert.ToInt32(comboBoxItem.SelectedValue),
                    Cnt = Convert.ToInt32(textBoxCount.Text),
                    TotalSum = Convert.ToDecimal(textBoxTotal.Text)
                });
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
