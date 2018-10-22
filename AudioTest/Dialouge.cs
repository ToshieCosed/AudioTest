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
    public partial class Dialouge : Form
    {
        public string ReturnValue{ get; set; }
        public Dialouge()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "Yes";
            this.Close();
        }

        private void No_Click(object sender, EventArgs e)
        {
            this.ReturnValue = "No";
            this.Close();
        }
    }
}
