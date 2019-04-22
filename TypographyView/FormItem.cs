using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TypographyView
{
    public partial class FormItem : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        private List<ItemPartViewModel> productComponents;
        public FormItem()
        {
            InitializeComponent();
        }
        private void FormItem_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ItemViewModel view = APIClient.GetRequest<ItemViewModel>("api/Item/Get/" + id.Value);
                    if (view != null)
                    {
                        maskedTextBoxName.Text = view.ItemName;
                        maskedTextBoxCost.Text = view.Cost.ToString();
                        productComponents = view.ItemPart;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                productComponents = new List<ItemPartViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productComponents;
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
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormItemPart();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.ItemId= id.Value;
                    }
                    productComponents.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormItemPart();
                form.Model = productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productComponents.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(maskedTextBoxCost.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<ItemPartBindingModel> productComponentBM = new List<ItemPartBindingModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new ItemPartBindingModel
                    {
                        Id = productComponents[i].Id,
                        ItemId = productComponents[i].ItemId,
                        PartId = productComponents[i].PartId,
                        Cnt = productComponents[i].Cnt
                    });
                }
                if (id.HasValue)
                {
                    APIClient.PostRequest<ItemBindingModel, bool>("api/Item/UpdElement", new ItemBindingModel
                    {
                        Id = id.Value,
                        ItemName = maskedTextBoxName.Text,
                        Cost = Convert.ToInt32(maskedTextBoxCost.Text),
                        ItemPart = productComponentBM
                    });
                }
                else
                {
                    APIClient.PostRequest<ItemBindingModel, bool>("api/Item/AddElement", new ItemBindingModel
                    {
                        ItemName = maskedTextBoxName.Text,
                        Cost = Convert.ToInt32(maskedTextBoxCost.Text),
                        ItemPart = productComponentBM
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
