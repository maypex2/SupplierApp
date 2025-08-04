using System;
using System.Data;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace SuppliersApp
{
    public partial class Form1 : Form
    {
        private forprint printHelper;

        public Form1()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadCategories();
            LoadPhilgepsOptions();

            printHelper = new forprint(suppliers_record);

            btnPrint.Click += new EventHandler(BtnPrint_Click);
            btnPrintPreview.Click += new EventHandler(BtnPrintPreview_Click);



            // for burgerbtn and printercontainer
            printercontainer.Visible = false;
            btnBurger.Click += btnBurger_Click;
        }

        private void btnBurger_Click(object sender, EventArgs e)
        {
            
            printercontainer.Visible = !printercontainer.Visible;

            
            if (printercontainer.Visible)
            {
                btnBurger.Text = " Hide Printer";
                btnBurger.BackColor = Color.LightBlue;
            }
            else
            {
                btnBurger.Text = " Show Printer";
                btnBurger.BackColor = SystemColors.Control;
            }
        }
        

        //to select philhgeps membership
        private void LoadPhilgepsOptions()

        {
            comboPhilgeps.Items.Clear();
            comboPhilgeps.Items.Add("Platinum Membership");
            comboPhilgeps.Items.Add("Red Membership");
            comboPhilgeps.SelectedIndex = 0;
        }

        //suppliers datagrid settings
        private void SetupDataGridView()
        {
            suppliers_record.AutoGenerateColumns = false;
            suppliers_record.Columns.Clear();

            suppliers_record.Columns.Add("Column1", "ID");
            suppliers_record.Columns.Add("Column2", "Suppliers");
            suppliers_record.Columns.Add("Column3", "Category");
            suppliers_record.Columns.Add("Column4", "Supplier Representative");
            suppliers_record.Columns.Add("Column5", "Contact Number");
            suppliers_record.Columns.Add("Column6", "Address");
            suppliers_record.Columns.Add("Column7", "Email");
            suppliers_record.Columns.Add("Column8", "Philgeps");



            suppliers_record.Columns["Column1"].Width = 55;
            suppliers_record.Columns["Column4"].Width = 280;
            suppliers_record.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            suppliers_record.Columns["Column1"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            suppliers_record.Columns["Column4"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            suppliers_record.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            foreach (DataGridViewColumn column in suppliers_record.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }



            suppliers_record.EnableHeadersVisualStyles = false;
            suppliers_record.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            suppliers_record.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            suppliers_record.AllowUserToOrderColumns = false; // to this able the user to in order the number
            suppliers_record.AllowUserToResizeColumns = true;



            suppliers_record.CellClick += suppliers_record_CellClick;

        }


        private void LoadCategories()
        {
            try
            {
                selectcategory.Items.Clear();
                DataTable categories = dbhelper.GetAllCategories();

                selectcategory.Items.Add("All Categories");

                foreach (DataRow row in categories.Rows)
                {
                    selectcategory.Items.Add(row["category_type"].ToString());
                }

                selectcategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }

        private void btnsupplieradd_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Suppliername.Text))
            {
                MessageBox.Show("Supplier name is required!");
                return;
            }

            if (string.IsNullOrWhiteSpace(categorytype.Text))
            {
                MessageBox.Show("Category type is required!");
                return;
            }

            int categoryId = dbhelper.AddCategory(categorytype.Text);
            if (categoryId <= 0)
            {
                MessageBox.Show("Failed to add category");
                return;
            }

            string philgeps = comboPhilgeps.SelectedItem?.ToString() ?? "";

            if (dbhelper.AddSupplier(
                Suppliername.Text,
                categoryId,
                Supplier_representative.Text,
                CnctNumber.Text,
                address.Text,
                email.Text,
                philgeps))

            {
                Suppliername.Text = "";
                categorytype.Text = "";
                Supplier_representative.Text = "";
                CnctNumber.Text = "";
                address.Text = "";
                email.Text = "";


                //philgeps
                comboPhilgeps.SelectedIndex = 0;
                PerformSearch();


                LoadCategories();
                //LoadSuppliers();
                MessageBox.Show("Supplier added successfully!");
            }
            else
            {
                MessageBox.Show("Failed to add supplier");
            }
        }

        private void btnsrch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(btnsrch.Text))
            {


                PerformSearch();
            }
            else
            {
                PerformSearch();
            }
        }

        private void selectcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch();
            //if (selectcategory.selectedindex < 0) return;

            //if (selectcategory.selectedindex == 0) // "all categories"
            //{
            //    loadallsuppliers();
            //}s
            //else
            //{
            //    string selectedcategory = selectcategory.selecteditem?.tostring();
            //    if (!string.isnullorempty(selectedcategory))
            //    {
            //        showsuppliersforcategory(selectedcategory);
            //    }
            //}
        }

        // inperform search method
        private void PerformSearch()
        {
            try
            {
                suppliers_record.Rows.Clear();
                var suppliers = dbhelper.SearchSuppliers1(selectcategory.Text, btnsrch.Text);

                foreach (DataRow row in suppliers.Rows)
                {
                    suppliers_record.Rows.Add(
                        row["Id"],
                        row["Name"],
                        dbhelper.GetCategoryNameById(Convert.ToInt32(row["CategoryId"])),
                        row["Supplier_Representative"],
                        row["Phone"],
                        row["Address"],
                        row["Email"],
                        row["Philgeps"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching suppliers: {ex.Message}");
            }
        }


        //for supplier click to open form2 and display in data 
        private void suppliers_record_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = suppliers_record.Rows[e.RowIndex];

                Form2 form2 = new Form2(
                    Convert.ToInt32(row.Cells["Column1"].Value),
                    row.Cells["Column2"].Value?.ToString() ?? "",
                    row.Cells["Column4"].Value?.ToString() ?? "",
                    row.Cells["Column5"].Value?.ToString() ?? "",
                    row.Cells["Column7"].Value?.ToString() ?? ""
                );

                form2.Show();
                this.Hide();
            }
        }


        private void BtnPrint_Click(object sender, EventArgs e)
        {
            printHelper.Print();
        }


        private void BtnPrintPreview_Click(object sender, EventArgs e)
        {
            printHelper.PrintPreview();
        }
    }


}