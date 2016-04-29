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
                Brush b = new SolidBrush(Color.Red);
                e.Graphics.FillEllipse(b, new Rectangle(Game.Ball.X, Game.Ball.Y, Game.BallSize.Width, Game.BallSize.Height));

                Rectangle rec = new Rectangle(Game.LeftRack.X,Game.LeftRack.Y,Game.LeftRackSize.Width,Game.LeftRackSize.Height);
                e.Graphics.FillRectangle(b, rec);
            }
        }
    }
}
