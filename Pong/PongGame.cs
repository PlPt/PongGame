using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;


namespace Pong
{
    public class PongGame
    {
       

        public Point LeftRack { get; set; }
        public Size LeftRackSize { get; set; }
        public Point RightRack { get; set; }
        public Point Ball { get; set; }
        public Size gameRenderSize { get; set; }
        public Size BallSize { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
       public  bool lose = false;

        int dx = -150;
        int dy = 48;

        int ddy = 300;

        public int speed = 1;
       
        Stopwatch watch = new Stopwatch();


        public PongGame()
        {
            Ball = new Point(230, 30);
            BallSize = new Size(50, 50);
            LeftRack = new Point(15, 225);
            LeftRackSize = new Size(10,170);
            
        }
        public void Start()
        {
            watch.Restart();
            Update();
        }

        public void Update()
        {
            //if (!watch.IsRunning) watch.Start();
           
            Ball.X += (int)(dx * watch.Elapsed.TotalSeconds)*speed;
            Ball.Y += (int)(dy * watch.Elapsed.TotalSeconds)*speed;

            if(Up)
            {
                LeftRack.Y  -= (int)(ddy * watch.Elapsed.TotalSeconds);
            }
            if (Down)
            {
                LeftRack.Y += (int)(ddy* watch.Elapsed.TotalSeconds);
            }

            #region touchWall
            if (Ball.X < 0)   // Left
            {
                dx *= -1;
                Ball.X = 0;
                lose = true;
            }

            if((Ball.X  + BallSize.Width)> gameRenderSize.Width) //  Top
            {
               dx *= -1;
               Ball.X = gameRenderSize.Width - BallSize.Width;
               //lose = true;
            }


            if (Ball.Y < 0)    //Right
            {
                dy *= -1;
                Ball.Y = 0;
               // lose = true;
            }

            if ((Ball.Y + BallSize.Height) > gameRenderSize.Height)  //BOt
            {
                dy *= -1;
                Ball.Y = gameRenderSize.Height - BallSize.Height;
              //  lose = true;
            }
            #endregion

            int rackTop = LeftRack.Y - BallSize.Height/2;
            int rackBot = LeftRack.Y + LeftRackSize.Height + BallSize.Height / 2;



             if(Ball.X  <= (LeftRack.X ))
             {
                 if( Ball.Y >= rackTop && Ball.Y<= rackBot )
                 {
                 dx *= -1;
                 Ball.X = LeftRack.X + 3;
               //  Console.WriteLine(string.Format("Collision: Ball ({0}/{1}) Rack({2}/{3}) RackTop: {4} RackBot={5}", Ball.X, Ball.Y, LeftRack.X, LeftRack.Y, rackTop, rackBot));
                 }    
            }

             if(LeftRack.Y <=0)
            {
                LeftRack.Y = 0;
            }


             if (LeftRack.Y >= (gameRenderSize.Height- LeftRackSize.Height))
             {
                 LeftRack.Y = gameRenderSize.Height - LeftRackSize.Height;
             }

             string log = string.Format("Ball({0}/{1}); Rack({2}/{3}/{4})",Ball.X,Ball.Y,LeftRack.X,LeftRack.Y,LeftRackSize.Height);
           //  Console.WriteLine(log);


            watch.Restart();
        }

        
    }


    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x,int y)
        {
            Y = y;
            X = x;
        }
    }
}
