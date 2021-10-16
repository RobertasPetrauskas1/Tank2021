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
            var firstPlayerGameWindow = new GameWindow(PlayerType.PLAYER1);
            firstPlayerGameWindow.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var secondPlayerGameWindow = new GameWindow(PlayerType.PLAYER2);
            secondPlayerGameWindow.Show();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
