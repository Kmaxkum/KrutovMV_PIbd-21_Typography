using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TypographyView
{
    public partial class FormPutOnStorage : Form
    {
        public FormPutOnStorage()
        {
            InitializeComponent();
        }

        private void FormPutOnStorage_Load(object sender, EventArgs e)
        {
            try
            {
                List<PartViewModel> listC = APIClient.GetRequest<List<PartViewModel>>("api/Part/GetList");
                if (listC != null)
                {
                    comboBoxPart.DisplayMember = "PartName";
                    comboBoxPart.ValueMember = "Id";
                    comboBoxPart.DataSource = listC;
                    comboBoxPart.SelectedItem = null;
                }
                List<StorageViewModel> listS = APIClient.GetRequest<List<StorageViewModel>>("api/Storage/GetList");
                if (listS != null)
                {
                    comboBoxStorages.DisplayMember = "StorageName";
                    comboBoxStorages.ValueMember = "Id";
                    comboBoxStorages.DataSource = listS;
                    comboBoxStorages.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPart.SelectedValue == null)
            {
                MessageBox.Show("Выберите канцелярию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorages.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<StoragePartBindingModel, bool>("api/Main/PutComponentOnStock", new StoragePartBindingModel
                {
                    PartId = Convert.ToInt32(comboBoxPart.SelectedValue),
                    StorageId = Convert.ToInt32(comboBoxStorages.SelectedValue),
                    Cnt = Convert.ToInt32(textBoxCount.Text)
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
