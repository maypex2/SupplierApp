// Form3.cs
using System;
using System.Windows.Forms;

namespace SuppliersApp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            dbhelper.InitializeDatabase();

            txtPassword.PasswordChar = '*';
            radioShowPassword.CheckedChanged += RadioShowPassword_CheckedChanged;
            radioShowPassword.Text = "Show Password";
            btnLogin.Click += BtnLogin_Click;
            this.AcceptButton = btnLogin;

            logincontainer.Visible = true;
            changepassandusercontainer.Visible = false;

            ChangePassanduser.Click += ChangePassanduser_Click;
            confirmchange.Click += Confirmchange_Click;
            showpasswordcheck.CheckedChanged += Showpasswordcheck_CheckedChanged;

            // Add back button event handler
            btnBackToLogin.Click += (s, e) => {
                changepassandusercontainer.Visible = false;
                logincontainer.Visible = true;
            };
        }

        private void RadioShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = radioShowPassword.Checked ? '\0' : '*';
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (dbhelper.ValidateUser(username, password))
            {
                Form1 mainForm = new Form1();
                mainForm.FormClosed += (s, args) => this.Close();
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void ChangePassanduser_Click(object sender, EventArgs e)
        {
            logincontainer.Visible = false;
            changepassandusercontainer.Visible = true;
            changeuser.Text = txtUsername.Text;
            changepass.Clear();
        }

        private void Showpasswordcheck_CheckedChanged(object sender, EventArgs e)
        {
            changepass.PasswordChar = showpasswordcheck.Checked ? '\0' : '*';
        }

        private void Confirmchange_Click(object sender, EventArgs e)
        {
            string newUsername = changeuser.Text.Trim();
            string newPassword = changepass.Text.Trim();

            if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Username and password cannot be empty");
                return;
            }

            // Simply replace all credentials in the database
            dbhelper.ReplaceAllCredentials(newUsername, newPassword);

            MessageBox.Show("Credentials updated successfully!");
            txtUsername.Text = newUsername;
            txtPassword.Text = newPassword;
            changepassandusercontainer.Visible = false;
            logincontainer.Visible = true;
        }
    }
}