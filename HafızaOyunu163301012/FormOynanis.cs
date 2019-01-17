using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HafızaOyunu163301012
{
    public partial class FormOynanis : Form
    {
        private FormAna _ana = null;
        private static FormOynanis _formOynanis = null;
        private FormOynanis(FormAna _Ana)
        {
            InitializeComponent();
            _ana = _Ana;
        }
        public static FormOynanis Olustur(FormAna _Ana)
        {
            if (_formOynanis == null)
            {
                _formOynanis = new FormOynanis(_Ana);
            }
            return _formOynanis;
        }

        private void FormOynanis_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ana._oynanis = null;
            _formOynanis = null;
        }
    }
}
