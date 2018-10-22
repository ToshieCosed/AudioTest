using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioTest
{
    public partial class QueryMulti : Form
    {
        public string returnvalue = "";
        public QueryMulti()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.returnvalue = "One";
            this.Close();
        }

        private void Four_Click(object sender, EventArgs e)
        {
            this.returnvalue = "Four";
            this.Close();
        }

        private void Two_Click(object sender, EventArgs e)
        {
            this.returnvalue = "Two";
            this.Close();
        }

        private void Three_Click(object sender, EventArgs e)
        {
            this.returnvalue = "Three";
            this.Close();
        }
    }
}
