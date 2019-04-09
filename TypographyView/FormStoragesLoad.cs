using TypographyServiceDAL.BindingModels;
using TypographyServiceDAL.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace TypographyView
{
    public partial class FormStoragesLoad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IEditionService service;
        public FormStoragesLoad(IEditionService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormStoragesLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = service.GetStoragesLoad();
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridView.Rows.Add(new object[] { elem.StorageName, "", "" });
                        foreach (var listElem in elem.Parts)
                        {
                            dataGridView.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalCnt});
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    service.SaveStoragesLoad(new EditionBindingModel
                    {
                        FilePath = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
    }
}
