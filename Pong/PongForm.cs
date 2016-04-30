using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pong
{
    public partial class PongForm : Form
    {
        PongGame game;

     
        public PongForm()
        {
            InitializeComponent();
            game = new PongGame();
            game.gameRenderSize = gameRenderer.ClientSize;
            game.init();
            gameRenderer.Game = game;
            game.speed = (int)speedPicker.Value;

            gameRenderer.Resize += new EventHandler((s, e) => { game.gameRenderSize = gameRenderer.ClientSize; game.rightRacket.Position.X = gameRenderer.ClientSize.Width - Racket.xDistance; });
           
        }
        bool msg = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (game.Status == PongGame.GameStatus.Running)
            {
                game.Update();
                gameRenderer.Invalidate();
            }
            else
            {      
                timer.Stop();
                if(!msg)
                {

                    switch (game.Status)
                    {
                       
                      case PongGame.GameStatus.Won:
                            MessageBox.Show("You won!");
                            break;
                        case PongGame.GameStatus.Lose:
                            MessageBox.Show("You lose!");
                            break;
                        default:
                            break;
                    }

                msg = true;
                }
              
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (gameRenderer.Focused)
            {
                switch (keyData)
                {

                    case Keys.Up: game.Up = true; break;
                    case Keys.Down: game.Down = true; break;
                  //  case Keys.Tab: game.rightActive = !game.rightActive; break;
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        } 

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (gameRenderer.Focused)
            {
                switch (e.KeyCode)
                {

                    case Keys.Up: game.Up = false; break;
                    case Keys.Down: game.Down = false; break;
                }
            }
            else
            {
                base.OnKeyUp(e);
            }
        }
       /* protected override void OnResizeEnd(EventArgs e)
        {
         if(game!=null)   game.gameRenderSize = gameRenderer.ClientSize;
            base.OnResize(e);
        }

        protected override void OnResize(EventArgs e)
        {
            OnResizeEnd(e);
            base.OnResize(e);
        }   */
        

        private void PongForm_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            msg = false;
            game.Status = PongGame.GameStatus.Running;
            game.Start();
            timer.Start();
            gameRenderer.Focus();
        }

        private void speedPicker_ValueChanged(object sender, EventArgs e)
        {
            game.speed = (int)speedPicker.Value;
        }

        private void PongForm_Load(object sender, EventArgs e)
        {

        }
       
    }
}
