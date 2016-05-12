using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pong
{
    /// <summary>
    /// Game Render Control for rendering the game elements
    /// </summary>
    public partial class GameRenderer : UserControl
    {
        public PongGame Game { get; set; }
        /// <summary>
        /// Init Constructor
        /// </summary>
        public GameRenderer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Paint Method, draw all Drawable Elements on the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White); // Clear with white background
            if (Game != null)
            {
                foreach (var item in Game.DrawList)  //For each drawable element
                {
                    item.Draw(e.Graphics);
                }

            }
        }

        private void GameRenderer_Load(object sender, EventArgs e)
        {

        }
    }
}
