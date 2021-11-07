
namespace Tank2021Client
{
    partial class GameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
        //    this.player1Tank = new System.Windows.Forms.PictureBox();
        //    this.timer = new System.Windows.Forms.Timer(this.components);
        //    ((System.ComponentModel.ISupportInitialize)(this.player1Tank)).BeginInit();
        //    this.SuspendLayout();
        //    // 
        //    // player1Tank
        //    // 
        //    this.player1Tank.Image = ((System.Drawing.Image)(resources.GetObject("player1Tank.Image")));
        //    this.player1Tank.Location = new System.Drawing.Point(30, 26);
        //    this.player1Tank.Name = "player1Tank";
        //    this.player1Tank.Size = new System.Drawing.Size(65, 88);
        //    this.player1Tank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        //    this.player1Tank.TabIndex = 0;
        //    this.player1Tank.TabStop = false;
        //    // 
        //    // timer
        //    // 
        //    this.timer.Enabled = true;
        //    this.timer.Interval = 50;
        //    this.timer.Tick += new System.EventHandler(this.timer_Tick);
        //    // 
        //    // GameWindow
        //    // 
        //    this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(982, 453);
        //    this.Controls.Add(this.player1Tank);
        //    this.Name = "GameWindow";
        //    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        //    this.Text = "GameWindow";
        //    this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
        //    ((System.ComponentModel.ISupportInitialize)(this.player1Tank)).EndInit();
        //    this.ResumeLayout(false);
        //    this.PerformLayout();

        //}

        #endregion

        private System.Windows.Forms.Label gameStartLabel;
        private System.Windows.Forms.Label gameEndLabel;
        private System.Windows.Forms.Label ChooseTankLabel;
        private System.Windows.Forms.Button LightTankButton;
        private System.Windows.Forms.Button MediumTankButton;
        private System.Windows.Forms.Button HeavyTankButton;
        private System.Windows.Forms.Button HeavyForcefieldTankButton;
        private System.Windows.Forms.Label player1Score;
        private System.Windows.Forms.Label player2Score;
    }
}