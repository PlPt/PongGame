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
    public partial class GameRenderer : UserControl
    {
        public PongGame Game { get; set; }
        public GameRenderer()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            if (Game != null)
            {
                foreach (var item in Game.DrawList)
                {
                    item.Draw(e.Graphics);
                }

            }
        }
    }
}
