using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Pong
{
    public class PongGame
    {

        #region varDef
        Ball ball;
    
        public Racket leftRacket;
         public Racket rightRacket;
         Stopwatch watch = new Stopwatch();
        
        public Size gameRenderSize { get; set; }

        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool lose = false;
        public bool rightActive = true;
        public int speed = 1;     
        List<IDrawable> drawList = new List<IDrawable>();
        #endregion
        public List<IDrawable> DrawList { get { return drawList; } }

        

        


        public PongGame()
        {
         
        }
       
        public void Start()
        {
            
            watch.Restart();
            Update();
        }

        public void Update()
        {
          

            ball.Move(watch.Elapsed.TotalSeconds, speed);
            

            var racket = rightActive == true ? rightRacket : leftRacket;
            if (Up)
            {
                racket.Move(watch.Elapsed.TotalSeconds, Racket.Direction.Top);
            }
            if (Down)
            {
                racket.Move(watch.Elapsed.TotalSeconds, Racket.Direction.Bot);
            }
            
            #region collisions
           //lose = 
               ball.isCollidingWall(gameRenderSize);
          
           checkColision(ball, leftRacket,Racket.Direction.Left,true);
           checkColision(ball, rightRacket,Racket.Direction.Right);
         
            #endregion

           

            string log = string.Format("Ball({0}/{1}); Rack({2}/{3}/{4})", ball.Position.X, ball.Position.Y, leftRacket.Position.X, leftRacket.Position.Y, leftRacket.Size.Height);



            watch.Restart();
        }

      
        public void checkColision(Ball b, Racket racket,Racket.Direction dir,bool calc = false)
        {
            int rackTop = racket.Position.Y - b.Size.Height / 2;
            int rackBot = racket.Position.Y + racket.Size.Height + b.Size.Height / 2;

            //Console.WriteLine("{0}/{1}", rackTop, rackBot);

            if (dir == Racket.Direction.Right)
            {
                if (b.Position.X >= (racket.Position.X - b.Size.Width) )
                {
                    if (b.Position.Y >= rackTop && b.Position.Y <= rackBot)   //evntl. toleranzzone berechnen
                    {
                        b.dx *= -1;
                        b.Position.X = b.Position.X - 3;
                        Console.WriteLine("Collided right:" + b.Position.ToString());
                        int diff = b.Position.Y - Ball._ii[Ball._ii.Count - 1];

                        Console.WriteLine("diff: " + diff);
                      
                    }
                }
            }
            else if(dir == Racket.Direction.Left)
            {
                if (b.Position.X <= (racket.Position.X))
                {
                    if (b.Position.Y >= rackTop && b.Position.Y <= rackBot)
                    {
                        b.dx *= -1;
                        b.Position.X = b.Position.X + 3;
                        if (calc)
                        {

                            var dispatcherTimer = new Timer { Interval = 500 };
                            dispatcherTimer.Tick += (sender, args) =>
                            {
                                var timer = sender as Timer;
                                if (timer != null)
                                {
                                    timer.Stop();
                                }

                                Console.WriteLine("Calc pos");
                            Ball.calc(this, ball.Clone());
                            };
                            dispatcherTimer.Start();
                            
                        }
                    }
                }
            }

            if (racket.Position.Y <= 0)
            {
                racket.Position.Y = 0;
            }


            if (racket.Position.Y >= (gameRenderSize.Height - racket.Size.Height))
            {
                racket.Position.Y = gameRenderSize.Height - racket.Size.Height;
            }
        }




      



        internal void init()
        {
            ball = new Ball(new Point(430, 30), new Size(50, 50));
            leftRacket = new Racket(new Point(15, 225), new Size(10, 170));
            rightRacket = new Racket(new Point(gameRenderSize.Width - 50, 225), new Size(10, 170));
           

            drawList.Add(ball);
            drawList.Add(leftRacket);
            drawList.Add(rightRacket);
         
        }

       
    }


    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            Y = y;
            X = x;
        }

        public override string ToString()
        {
            return string.Format("({0}/{1})",X,Y);
        }
    }
}
