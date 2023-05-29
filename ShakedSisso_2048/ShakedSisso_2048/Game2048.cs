using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ShakedSisso_2048
{
    public partial class Game2048 : Form
    {
        private string username;
        private Board board;
        private Score score;
        private SQLiteConnection connection;

        public Game2048(string username)
        {
            InitializeComponent();

            this.connection = new SQLiteConnection(@"Data Source= D:\high\Computer science\year10\WindowsApp\ShakedSisso_2048\ShakedSisso_2048\users.db");
            this.connection.Open();

            int bestScore = 0;
            this.username = username;
            ChangeHelloMessage();

            if (DoesUserExist())
            {
                bestScore = GetBestScore();
            }

            this.board = new Board();
            this.score = new Score(bestScore);
            StartGame();
        }

        private int GetBestScore()
        {
            string query = @"SELECT BESTSCORE FROM USERS WHERE USERNAME = '" + this.username + "'";
            SQLiteCommand command = new SQLiteCommand(query, this.connection);
            command.ExecuteNonQuery();
            SQLiteDataReader reader = command.ExecuteReader();
            int bestScore = 0;
            if (reader.Read())
            {
                bestScore = reader.GetInt32(0);
            }
            return bestScore;
        }

        private bool DoesUserExist()
        {
            string query = @"SELECT * FROM USERS WHERE USERNAME = '" + this.username + "'";
            SQLiteCommand command = new SQLiteCommand(query, this.connection);
            command.ExecuteNonQuery();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read()) { return true; }
            return false;
        }

        private void ChangeHelloMessage()
        {
            lblHello.Text = "Hello " + username + "!";
            lblHello.Left = (this.Width - lblHello.Width - 20) / 2;
        }
        private void StartGame()
        {
            this.board.ResetBoard();
            this.score.ResetScore();
            ChangeColors();
            ChangeScores();
        }

        private void ChangeScores()
        {
            lblScore.Text = this.score.GetScore().ToString();
            lblBestScore.Text = this.score.GetBestScore().ToString();
        }
        private void ChangeColors()
        {
            int tileValue = 0;
            for (int i = 0; i < tlpnlBoard.RowCount; i++)
            {
                for (int j = 0; j < tlpnlBoard.ColumnCount; j++)
                {
                    tileValue = this.board.gameBoard[i, j].GetValue();
                    Control c = tlpnlBoard.GetControlFromPosition(i, j);
                    c.BackgroundImage = Image.FromFile(Application.StartupPath + @"/../../Resources/picture" + tileValue + ".png");
                }
            }
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fInstractions = new HowToPlay();
            fInstractions.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Dispose();
        }

        private void SaveData()
        {
            string query;
            this.score.ResetScore();
            if (DoesUserExist())
            {
                query = @"UPDATE USERS SET BESTSCORE = " + this.score.GetBestScore() + " WHERE USERNAME='" + this.username + "'";
            }
            else
            {
                query = @"INSERT INTO USERS(USERNAME, BESTSCORE) VALUES('" + this.username + "', " + this.score.GetBestScore() + ")";
            }
            SQLiteCommand command = new SQLiteCommand(query, this.connection);
            command.ExecuteNonQuery();
        }

        private void GameOver()
        {
            string winOrLose;
            if (this.board.FindBiggestTile() == 2048)
            {
                winOrLose = "You won!!";
            }
            else
            {
                winOrLose = "You lost";
            }
            DialogResult result = MessageBox.Show("Would you like to play again?", winOrLose, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                StartGame();
            }
            else if (result == DialogResult.No)
            {
                SaveData();
                this.Dispose();
            }
        }

        private void resetGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void Game2048_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (this.board.MoveLeft())
                {
                    if (!this.board.IsGameOver())
                    {
                        this.board.NewTile();
                        ChangeColors();
                    }
                }
                else if (this.board.IsGameOver())
                    GameOver();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (this.board.MoveRight())
                {
                    if (!this.board.IsGameOver())
                    {
                        this.board.NewTile();
                        ChangeColors();
                    }
                }
                else if (this.board.IsGameOver())
                    GameOver();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (this.board.MoveUp())
                {
                    if (!this.board.IsGameOver())
                    {
                        this.board.NewTile();
                        ChangeColors();
                    }
                }
                else if (this.board.IsGameOver())
                    GameOver();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (this.board.MoveDown())
                {
                    if (!this.board.IsGameOver())
                    {
                        this.board.NewTile();
                        ChangeColors();
                    }
                }
                else if (this.board.IsGameOver())
                    GameOver();
            }
            if (this.board.FindBiggestTile() == 2048)
            {
                GameOver();
            }
            this.score.UpdateScore(this.board.GetScoreValue());
            ChangeScores();
        }
    }
}
