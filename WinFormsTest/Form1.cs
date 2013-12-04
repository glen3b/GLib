using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Glib.WinForms.Shapes;
using Glib.WinForms.Controls;
using Glib.WinForms;

namespace WinFormsTest
{
    public partial class Form1 : Form
    {
        Graphics gfx;
        FormValidator valid;
        public Form1()
        {
            InitializeComponent();
        }

        Triangle t;

        private void Form1_Shown(object sender, EventArgs e)
        {
            valid = new FormValidator(this);
            gfx = this.CreateGraphics();
            t = new Triangle(new Point(3, 3), new Point(40, 40), new Point(3, 56), Color.Black);
            t.Draw(gfx);
        }

        private void requiredTextBox1_TextChanged(object sender, EventArgs e)
        {
            valid.ValidateForm(errorProvider1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            requiredTextBox1.FieldValidation = new Predicate<string>(delegate(string str) { return str == "Glen Husman"; });
        }

        private void requiredTextBox_Validated(object sender, EventArgs e)
        {
            t.Draw(gfx);
        }
    }
}
