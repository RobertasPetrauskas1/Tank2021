using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            var gameWindowPlayer1 = new GameWindow();
            gameWindowPlayer1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var gameWindowPlayer2 = new GameWindow();
            gameWindowPlayer2.Show();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
