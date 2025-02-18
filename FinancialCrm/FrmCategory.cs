using FinancialCrm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }
        FinancialCrmDbEntities db = new FinancialCrmDbEntities();
        private void FrmCategory_Load(object sender, EventArgs e)
        {

            var values = db.TblCategories.ToList();
            dataGridView1.DataSource = values;

        }

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            var values = db.TblCategories.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnNewCate_Click(object sender, EventArgs e)
        {
            string Name = txtCategoryName.Text;
            TblCategories categories = new TblCategories();
            categories.CategoryName = Name;
            db.TblCategories.Add(categories);
            db.SaveChanges();
            MessageBox.Show("Kategori başarılı bir şekilde sisteme eklendi!", "Kategori Formu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var values = (from category in db.TblCategories
                          select new
                          {
                              category.CategoryId,
                              category.CategoryName
                          }).ToList();
            dataGridView1.DataSource = values;
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            var removeValue = db.TblCategories.Find(id);
            db.TblCategories.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Kategori başarılı bir şekilde sistemden silindi!", "Kategori Formu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var values = (from category in db.TblCategories
                          select new
                          {
                              category.CategoryId,
                              category.CategoryName
                          }).ToList();
            dataGridView1.DataSource = values;
        }

        private void btnUpdateCate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            string name = txtCategoryName.Text;

            var categories = db.TblCategories.Find(id);
            categories.CategoryName = name;
            db.SaveChanges();
            MessageBox.Show("Kategori başarılı bir şekilde sistemde güncellendi!", "Kategori Formu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var values = (from category in db.TblCategories
                          select new
                          {
                              category.CategoryId,
                              category.CategoryName
                          }).ToList();
            dataGridView1.DataSource = values;
        }
    }
}
