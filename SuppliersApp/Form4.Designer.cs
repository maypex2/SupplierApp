namespace SuppliersApp
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel1 = new Panel();
            guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            panel2 = new Panel();
            printercontainer = new Guna.UI2.WinForms.Guna2ContainerControl();
            guna2HtmlLabel10 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel9 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnPrintPreview = new Guna.UI2.WinForms.Guna2ImageButton();
            btnPrint = new Guna.UI2.WinForms.Guna2ImageButton();
            btnBurger = new Guna.UI2.WinForms.Guna2ImageButton();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            printercontainer.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(guna2TextBox1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(985, 143);
            panel1.TabIndex = 0;
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            guna2TextBox1.BorderColor = Color.Transparent;
            guna2TextBox1.BorderThickness = 0;
            guna2TextBox1.CustomizableEdges = customizableEdges1;
            guna2TextBox1.DefaultText = "ghhhg";
            guna2TextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.Dock = DockStyle.Fill;
            guna2TextBox1.Enabled = false;
            guna2TextBox1.FillColor = Color.WhiteSmoke;
            guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Font = new Font("Segoe UI", 9F);
            guna2TextBox1.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox1.Location = new Point(0, 0);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PlaceholderText = "";
            guna2TextBox1.ReadOnly = true;
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2TextBox1.Size = new Size(665, 143);
            guna2TextBox1.TabIndex = 1;
            guna2TextBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.Controls.Add(printercontainer);
            panel2.Controls.Add(btnBurger);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(665, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(320, 143);
            panel2.TabIndex = 0;
            // 
            // printercontainer
            // 
            printercontainer.BackColor = Color.Transparent;
            printercontainer.BorderColor = Color.Transparent;
            printercontainer.BorderRadius = 2;
            printercontainer.BorderThickness = 1;
            printercontainer.Controls.Add(guna2HtmlLabel10);
            printercontainer.Controls.Add(guna2HtmlLabel9);
            printercontainer.Controls.Add(btnPrintPreview);
            printercontainer.Controls.Add(btnPrint);
            printercontainer.CustomizableEdges = customizableEdges5;
            printercontainer.Location = new Point(70, 23);
            printercontainer.Name = "printercontainer";
            printercontainer.ShadowDecoration.CustomizableEdges = customizableEdges6;
            printercontainer.Size = new Size(185, 84);
            printercontainer.TabIndex = 4;
            printercontainer.Text = "guna2ContainerControl5";
            // 
            // guna2HtmlLabel10
            // 
            guna2HtmlLabel10.BackColor = Color.White;
            guna2HtmlLabel10.Font = new Font("Segoe UI", 11.25F);
            guna2HtmlLabel10.ForeColor = SystemColors.ActiveCaptionText;
            guna2HtmlLabel10.Location = new Point(13, 4);
            guna2HtmlLabel10.Name = "guna2HtmlLabel10";
            guna2HtmlLabel10.Size = new Size(88, 22);
            guna2HtmlLabel10.TabIndex = 11;
            guna2HtmlLabel10.Text = "Print Preview";
            // 
            // guna2HtmlLabel9
            // 
            guna2HtmlLabel9.BackColor = Color.Transparent;
            guna2HtmlLabel9.Font = new Font("Segoe UI", 11.25F);
            guna2HtmlLabel9.Location = new Point(130, 3);
            guna2HtmlLabel9.Name = "guna2HtmlLabel9";
            guna2HtmlLabel9.Size = new Size(33, 22);
            guna2HtmlLabel9.TabIndex = 10;
            guna2HtmlLabel9.Text = "Print";
            // 
            // btnPrintPreview
            // 
            btnPrintPreview.CheckedState.ImageSize = new Size(64, 64);
            btnPrintPreview.Cursor = Cursors.Hand;
            btnPrintPreview.HoverState.ImageSize = new Size(46, 46);
            btnPrintPreview.Image = (Image)resources.GetObject("btnPrintPreview.Image");
            btnPrintPreview.ImageOffset = new Point(0, 0);
            btnPrintPreview.ImageRotate = 0F;
            btnPrintPreview.ImageSize = new Size(47, 47);
            btnPrintPreview.Location = new Point(28, 28);
            btnPrintPreview.Name = "btnPrintPreview";
            btnPrintPreview.PressedState.ImageSize = new Size(45, 45);
            btnPrintPreview.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnPrintPreview.Size = new Size(45, 45);
            btnPrintPreview.TabIndex = 10;
            // 
            // btnPrint
            // 
            btnPrint.CheckedState.ImageSize = new Size(64, 64);
            btnPrint.Cursor = Cursors.Hand;
            btnPrint.HoverState.ImageSize = new Size(46, 46);
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.ImageOffset = new Point(0, 0);
            btnPrint.ImageRotate = 0F;
            btnPrint.ImageSize = new Size(45, 45);
            btnPrint.Location = new Point(123, 29);
            btnPrint.Name = "btnPrint";
            btnPrint.PressedState.ImageSize = new Size(45, 45);
            btnPrint.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnPrint.Size = new Size(45, 45);
            btnPrint.TabIndex = 10;
            // 
            // btnBurger
            // 
            btnBurger.BackColor = Color.White;
            btnBurger.CheckedState.ImageSize = new Size(64, 64);
            btnBurger.HoverState.ImageSize = new Size(49, 49);
            btnBurger.Image = (Image)resources.GetObject("btnBurger.Image");
            btnBurger.ImageOffset = new Point(0, 0);
            btnBurger.ImageRotate = 0F;
            btnBurger.ImageSize = new Size(48, 48);
            btnBurger.Location = new Point(256, 24);
            btnBurger.Name = "btnBurger";
            btnBurger.PressedState.ImageSize = new Size(50, 50);
            btnBurger.ShadowDecoration.CustomizableEdges = customizableEdges7;
            btnBurger.Size = new Size(52, 46);
            btnBurger.TabIndex = 3;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 620);
            Controls.Add(panel1);
            Name = "Form4";
            Text = "Form4";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            printercontainer.ResumeLayout(false);
            printercontainer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Guna.UI2.WinForms.Guna2ContainerControl printercontainer;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel10;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel9;
        private Guna.UI2.WinForms.Guna2ImageButton btnPrintPreview;
        private Guna.UI2.WinForms.Guna2ImageButton btnPrint;
        private Guna.UI2.WinForms.Guna2ImageButton btnBurger;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox1;
    }
}