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
    /// <summary>
    /// Form for the game
    /// </summary>
    public partial class PongForm : Form
    {
        #region varDef
        PongGame game;
        bool msg = false;
        #endregion

        /// <summary>
        /// Form constructor, init the form and the game
        /// </summary>
        public PongForm()
        {
            InitializeComponent();
            game = new PongGame(gameRenderer.ClientSize);
           gameRenderer.Game = game; // Set game object to renderer
            game.speed = (int)speedPicker.Value;

            //Resize Event, changes game Sizes
            gameRenderer.Resize += new EventHandler((s, e) => { game.gameRenderSize = gameRenderer.ClientSize; game.rightRacket.Position.X = gameRenderer.ClientSize.Width - Racket.xDistance; });
           
        }
      
        /// <summary>
        /// Main Game timer for  "real time"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Key Events for control the game   
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Key Events for control the game   
        /// </summary>
        /// <param name="e"></param>
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
               

        /// <summary>
        /// Start Button to start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            msg = false;
            game.Status = PongGame.GameStatus.Running;
            game.Start();
            timer.Start();
            gameRenderer.Focus();
        }

        /// <summary>
        /// Speed change listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedPicker_ValueChanged(object sender, EventArgs e)
        {
            game.speed = (int)speedPicker.Value;
        }

       
       
    }
}
