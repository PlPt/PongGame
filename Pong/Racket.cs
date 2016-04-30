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
       public const int  xDistance =20;
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

        Timer dispatcherTimer;
        public void MoveTo(int Y,Size gSize,int speed)  //......
        {
            if(dispatcherTimer!=null)
            {
                if (dispatcherTimer.Enabled) dispatcherTimer.Stop();
            }
                 if(Y<=0)
                 {
                     Y = 1;
                 }

                  if(gSize.Height<=Y)
                  {

                      Y = gSize.Height-10;
                  }
                      
                 int ddy = 1;

                 if (Position.Y > Y)
                 {
                     ddy = -1;
                 }

          

             dispatcherTimer = new Timer { Interval =10 };
            dispatcherTimer.Tick += (sender, args) =>
            {
                var timer = sender as Timer;
                if (timer != null && (Position.Y >= gSize.Height || Position.Y == Y))
                {
                    timer.Stop();
                    Console.WriteLine("moved: " + Position.ToString());
                    return;
                }
                else
                {
                    Console.WriteLine("{0}=={1}", Position.Y - Size.Height, Y);
                }

             
                Position.Y += ddy*speed;
            };
            dispatcherTimer.Start();
        }

    }
}
