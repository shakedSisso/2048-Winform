using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShakedSisso_2048
{
    public partial class HowToPlay : Form
    {
        public HowToPlay()
        {
            InitializeComponent();
        }

        private void HowToPlay_Load(object sender, EventArgs e)
        {
            string str = "Use your arrow keys to move the tiles.\nTiles with the same number merge into\none when they touch.\nAdd them up to reach\n";
            lblGameInstructions.Text = str;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
