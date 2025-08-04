using System;
using System.Data;
using System.Windows.Forms;

namespace SuppliersApp
{
    public partial class Form2 : Form
    {
        private int _supplierId;

        public Form2(int supplierId, string supplier, string representative, string contact, string email)
        {
            InitializeComponent();
            _supplierId = supplierId;

            
            lblcurrentsupplier.Text = supplier;
            lblcurrentrepresentative.Text = representative;
            lblcurrentnumber.Text = contact;
            lblcurrentemail.Text = email;

            
            txtEditRepresentative.Text = representative;
            txtEditContact.Text = contact;
            txtEditEmail.Text = email;

            
            SetEditMode(false);
            SetupHistoryGrid();
            LoadPreviousVersions(supplierId);
        }


        //Columns history Grid
        private void SetupHistoryGrid()
        {
            previous_supplier_grid.Columns.Clear();

            
            previous_supplier_grid.Columns.Add("ColName", "Supplier");
            previous_supplier_grid.Columns.Add("ColRep", "Representative");
            previous_supplier_grid.Columns.Add("ColPhone", "Contact");
            previous_supplier_grid.Columns.Add("ColEmail", "Email");
            previous_supplier_grid.Columns.Add("ColChanged", "Last Changed");

            
            previous_supplier_grid.Columns["ColChanged"].Width = 150;
        }


        //to ad previous version grom my dbhelper
        private void LoadPreviousVersions(int supplierId)
        {
            try
            {
                previous_supplier_grid.Rows.Clear();
                DataTable versions = dbhelper.GetSupplierVersions(supplierId);

                foreach (DataRow row in versions.Rows)
                {
                    previous_supplier_grid.Rows.Add(
                        row["Name"],
                        row["Supplier_Representative"],
                        row["Phone"],
                        row["Email"],
                        Convert.ToDateTime(row["DateChanged"]).ToString("yyyy-MM-dd HH:mm")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading history: {ex.Message}");
            }
        }


        private void SetEditMode(bool editMode)
        {

            lblcurrentrepresentative.Visible = !editMode;
            lblcurrentnumber.Visible = !editMode;
            lblcurrentemail.Visible = !editMode;
            emaill.Visible = !editMode;
            repre.Visible = !editMode;
            cntnumbet.Visible = !editMode;



            txtEditRepresentative.Visible = editMode;
            txtEditContact.Visible = editMode;
            txtEditEmail.Visible = editMode;

            btnEdit.Visible = !editMode;
            btnSave.Visible = editMode;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }



        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            SetEditMode(true);
            txtEditRepresentative.Focus();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dbhelper.UpdateSupplier(
                    _supplierId,
                    txtEditRepresentative.Text.Trim(),
                    txtEditContact.Text.Trim(),
                    txtEditEmail.Text.Trim()))
                {
                    // Update displayed values (supplier name stays the same)
                    lblcurrentrepresentative.Text = txtEditRepresentative.Text;
                    lblcurrentnumber.Text = txtEditContact.Text;
                    lblcurrentemail.Text = txtEditEmail.Text;

                    SetEditMode(false);
                    LoadPreviousVersions(_supplierId);
                    MessageBox.Show("Changes saved successfully!");
                }
                else
                {
                    MessageBox.Show("Save failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}");
            }
        }
    }
}
