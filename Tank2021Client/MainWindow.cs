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
            var gameWindow = new GameWindow();
            gameWindow.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
