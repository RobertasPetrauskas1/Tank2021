using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2021SharedContent;
using Tank2021SharedContent.Enums;

namespace Tank2021Client
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var firstPlayerGameWindow = new GameWindow(PlayerType.PLAYER1);
            firstPlayerGameWindow.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var secondPlayerGameWindow = new GameWindow(PlayerType.PLAYER2);
            secondPlayerGameWindow.ShowDialog();
            this.Close();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
