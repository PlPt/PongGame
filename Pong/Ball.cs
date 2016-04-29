using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pong
{
    public class Ball   : IDrawable
    {

        public static List<int> _ii = new List<int>();
        public Point Position { get; set; }
        public Size Size { get; set; }

       public  int dx = -150;
       public  int dy = 48;

       //float G = 1.81f;

        public Ball(Point initPos,Size size)
        {
            Position = initPos;
            this.Size = size;

        }

        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Red);
           g.FillEllipse(b, new Rectangle(Position.X, Position.Y, Size.Width, Size.Height));
        }

        public void Move(double elapsedSeconds,int speed)
        {
            Position.X += (int)(dx * elapsedSeconds) * speed;
            Position.Y += (int)((dy * elapsedSeconds) * speed);
        }

        public bool isCollidingWall(Size gameRenderSize)
        {
             if (Position.X < 0)   // Left
            {
                dx *= -1;
                Position.X = 0;
                Console.WriteLine("Colision left " + Position.Y);
                return true;
            }

            if ((Position.X + Size.Width) > gameRenderSize.Width) //  right
            {
                dx *= -1;
                Position.X = gameRenderSize.Width - Size.Width;
                Console.WriteLine("Colision right " + Position.Y);

                int diff = Position.Y - Ball._ii[Ball._ii.Count - 1];

                Console.WriteLine("diff: " + diff);
                      
                return true;


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
            return false ;
        }

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


        public static void calc(PongGame g,Ball b)
        {
            Ball imag = new Ball(new Point(b.Position.X, b.Position.Y), new Size(50, 50));
            imag.dx = int.Parse(b.dx.ToString());
            imag.dy = int.Parse(b.dy.ToString());
           
            
            while (imag.isCollidingWallEX(g.gameRenderSize)!=Racket.Direction.Right)
            {
                imag.Move(0.2d, 1);

                               

            } 

            Console.WriteLine("while ended colliding: " + imag.Position.ToString());
            _ii.Add(imag.Position.Y);
            int i = imag.Position.Y - imag.Size.Height/2 ;
            Console.WriteLine("moving to: " + i);
            g.rightRacket.Position.Y = i;
           
        }

        public Ball Clone()
        {
            return (Ball)this.MemberwiseClone();
        }



    }
}
