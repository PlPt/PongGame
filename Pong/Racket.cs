using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pong
{
    public class Racket : IDrawable
    {

       public enum Direction { Left,Right,Top,Bot,None }

        public Point Position { get; set; }
        public Size Size { get; set; }
        public int dy = 300;




        public Racket(Point initPosition,Size size)
        {
            Position = initPosition;
            Size = size;
        }

        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Red);
            Rectangle rec = new Rectangle(Position.X, Position.Y, Size.Width, Size.Height);
            g.FillRectangle(b, rec);
        }

        public void Move(double elapsedSeconds,Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                case Direction.Top:
                    Position.Y -= (int)(dy * elapsedSeconds);
                    break;
                case Direction.Bot:
                    Position.Y += (int)(dy * elapsedSeconds);
                    break;
                default:
                    break;
            }

        }


        public void MoveTo(int Y,Size gSize)
        {
                 if(Y<0)
                 {
                     Y = 1;
                 }

                 int ddy = 1;

                 if (Position.Y > Y)
                 {
                     ddy = -1;
                 }

            var dispatcherTimer = new Timer { Interval =5 };
            dispatcherTimer.Tick += (sender, args) =>
            {
                var timer = sender as Timer;
                if (timer != null && (Position.Y==Y-Size.Height))
                {
                    timer.Stop();
                    Console.WriteLine("moved: " + Position.ToString());
                    return;
                }
                else
                {
                    Console.WriteLine("{0}=={1}", Position.Y - Size.Height, Y);
                }

             
                Position.Y += ddy;
            };
            dispatcherTimer.Start();
        }

    }
}
