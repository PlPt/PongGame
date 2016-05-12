using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pong
{
    /// <summary>
    /// Repesents a Ball
    /// </summary>
    public class Ball   : IDrawable
    {
        #region varDef
        public static List<int> _ii = new List<int>();
        public Point Position { get; set; }
        public Size Size { get; set; }

       public  int dx = -150;
       public  int dy = 48;
        #endregion


          /// <summary>
          /// Construcor to create a Ball with initial position and fixed size
          /// </summary>
          /// <param name="initPos"></param>
          /// <param name="size"></param>
       public Ball(Point initPos,Size size)
        {
            Position = initPos;
            this.Size = size;

        }

        /// <summary>
        /// IDrawable Draw Method to draw the Object with Graphics
        /// </summary>
        /// <param name="g">Graphics use to draw</param>
        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Blue);
           g.FillEllipse(b, new Rectangle(Position.X, Position.Y, Size.Width, Size.Height));
        }

        /// <summary>
        /// Moves a Ball around
        /// </summary>
        /// <param name="elapsedSeconds">Timer elapsed seconds, for calculating position</param>
        /// <param name="speed">Ball speed</param>
        public void Move(double elapsedSeconds,int speed)
        {
            Position.X += (int)(dx * elapsedSeconds) * speed;
            Position.Y += (int)((dy * elapsedSeconds) * speed);
        }

        /// <summary>
        /// Checks whether the ball is colliding the walls
        /// </summary>
        /// <param name="gameRenderSize">Game redner size</param>
        /// <returns>Colliding Status</returns>
        public PongGame.GameStatus isCollidingWall(Size gameRenderSize)
        {
             if (Position.X < 0)   // Left
            {
                dx *= -1;
                Position.X = 0;
                Console.WriteLine("Colision left " + Position.Y);
                return PongGame.GameStatus.Lose;
            }

            if ((Position.X + Size.Width) > gameRenderSize.Width) //  right
            {
                dx *= -1;
                Position.X = gameRenderSize.Width - Size.Width;
                Console.WriteLine("Colision right " + Position.Y);

              

                return PongGame.GameStatus.Won;


            }


            if (Position.Y < 0)    //top
            {
                dy *= -1;
                Position.Y = 0;
              
               
              
            }

            if ((Position.Y + Size.Height) > gameRenderSize.Height)  //BOt
            {
                dy *= -1;
                Position.Y = gameRenderSize.Height - Size.Height;
                
                
            }
            return PongGame.GameStatus.Running;
        }

        /// <summary>
        /// Checks whether ball is Colliding Rackets, for KI
        /// </summary>
        /// <param name="gameRenderSize">Game size</param>
        /// <returns>RacketDirection</returns>
        public Racket.Direction isCollidingWallEX(Size gameRenderSize)
        {
            if (Position.X < 0)   // Left
            {
                dx *= -1;
                Position.X = 0;

                return Racket.Direction.Left;
            }

            if ((Position.X + Size.Width) > gameRenderSize.Width) //  right
            {
                dx *= -1;
                Position.X = gameRenderSize.Width - Size.Width;
                return Racket.Direction.Right;


            }


            if (Position.Y < 0)    //top
            {
                dy *= -1;
                Position.Y = 0;
              


            }

            if ((Position.Y + Size.Height) > gameRenderSize.Height)  //BOt
            {
                dy *= -1;
                Position.Y = gameRenderSize.Height - Size.Height;


            }
            return Racket.Direction.None;
        }

             /// <summary>
             /// Calc the next Racket colliding position and move the Racket to this pos
             /// </summary>
             /// <param name="g">GameObject</param>
             /// <param name="b">Ball to check</param>
             /// <param name="r">Racket to check</param>
             /// <param name="dir">Racket direction</param>
        public static void calc(PongGame g,Ball b,Racket r,Racket.Direction dir)
        {
            Ball imag = new Ball(new Point(b.Position.X, b.Position.Y), new Size(50, 50));
            imag.dx = int.Parse(b.dx.ToString());
            imag.dy = int.Parse(b.dy.ToString());
           
            
            while (imag.isCollidingWallEX(g.gameRenderSize)!=dir)
            {
                imag.Move(0.2d, 1);
 
            } 

           // Console.WriteLine("while ended colliding: " + imag.Position.ToString());
            _ii.Add(imag.Position.Y);
            int i = imag.Position.Y - (int)(imag.Size.Height);  ///0.5f
           // Console.WriteLine("moving to: " + i);
            r.MoveTo(i,g.gameRenderSize,g.speed);
           
        }

        /// <summary>
        /// Clones a Ball Object
        /// </summary>
        /// <returns>Cloned ball object</returns>
        public Ball Clone()
        {
            return (Ball)this.MemberwiseClone();
        }



    }
}
