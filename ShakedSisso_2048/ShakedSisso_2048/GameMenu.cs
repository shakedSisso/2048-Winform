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
    public partial class GameMenu : Form
    {
        private string username;
        public GameMenu()
        {
            InitializeComponent();
            
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty)
            {
                this.username = txtName.Text;
                Form fGame = new Game2048(this.username);
                this.Hide();
                fGame.ShowDialog();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Enter a username to play");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            Form fInstractions = new HowToPlay();
            fInstractions.ShowDialog();
        }

        private void GameMenu_Load(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
