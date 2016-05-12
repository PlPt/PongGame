using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pong
{
    /// <summary>
    ///  Racket  model Object for hit back the ball 
    /// </summary>
    public class Racket : IDrawable
    {

        #region DrectionEnum
        /// <summary>
           ///   Driection/Positions enum, whitch Defines varous directions and Positions eg. for the left and reight racket
           /// </summary>
       public enum Direction { Left,Right,Top,Bot,None }
        #endregion 


        #region varDef
       public Point Position { get; set; }
        public Size Size { get; set; }
       public const int  xDistance =20;
        public int dy = 300;
    #endregion



         /// <summary>
         /// Constructior to init a Racket
         /// </summary>
         /// <param name="initPosition">Initial position</param>
         /// <param name="size">Racket size</param>
        public Racket(Point initPosition,Size size)
        {
            Position = initPosition;
            Size = size;
        }

        /// <summary>
        /// Public draw Method for drawing the racket on the contol (inherited from IDrawable)
        /// </summary>
        /// <param name="g">Graphic object for painting</param>
        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Red);
            Rectangle rec = new Rectangle(Position.X, Position.Y, Size.Width, Size.Height);
            g.FillRectangle(b, rec);
        }


        /// <summary>
        /// Moves the racket up or down
        /// </summary>
        /// <param name="elapsedSeconds">mainTimer elapsedTime seconds</param>
        /// <param name="direction">Moving direction</param>
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
        /// <summary>
        /// Moves the racket to a specific Y-Coordinate
        /// </summary>
        /// <param name="Y">Y-Coorinate to move</param>
        /// <param name="gSize">GameRender size</param>
        /// <param name="speed">Game speed</param>
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
                  //  Console.WriteLine("moved: " + Position.ToString());
                    return;
                }
                else
                {
                   // Console.WriteLine("{0}=={1}", Position.Y - Size.Height, Y);
                }

             
                Position.Y += ddy*speed;
            };
            dispatcherTimer.Start();
        }

    }
}
