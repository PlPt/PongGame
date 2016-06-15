using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Pong
{
    /// <summary>
    /// Main game logic
    /// Calulates all game elements
    /// </summary>
    public class PongGame
    {
        #region StatusEnum
        /// <summary>
        /// Enum representing the current staus of the game
        /// </summary>
        public enum GameStatus { Running, Stopped, Won, Lose }
        #endregion

        #region varDef

        //privateVars
        Ball ball;
        Stopwatch watch = new Stopwatch();
        List<IDrawable> drawList = new List<IDrawable>();
        bool calc = false;

        //public vars
        public Racket leftRacket;
        public Racket rightRacket;
        public int speed = 1;

        //public Properies
        public Size gameRenderSize { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool rightActive = false;

        public List<IDrawable> DrawList { get { return drawList; } }
        public GameStatus Status { get; set; }

        #endregion






        /// <summary>
        /// Contructor which initializes the game
        /// </summary>
        public PongGame(Size gameRenderSize)
        {
            Status = GameStatus.Stopped;
            this.gameRenderSize = gameRenderSize;
            init();
           // test();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void Start()
        {

            watch.Restart();
            Update();
        }

        /// <summary>
        ///   Update/refresh the  game lements like move ball and rackets
        /// </summary>
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
            Status = ball.isCollidingWall(gameRenderSize);

            checkColision(ball, leftRacket, Racket.Direction.Left);
            checkColision(ball, rightRacket, Racket.Direction.Right);

            if( gameRenderSize.Width - ball.Position.X < 450 && ball.dx > 0 &&  !calc)
            {
                Ball.calc(this, ball.Clone(), rightRacket, Racket.Direction.Right);
                calc = true;
            }

            #endregion



            string log = string.Format("Ball({0}/{1}); Rack({2}/{3}/{4})", ball.Position.X, ball.Position.Y, leftRacket.Position.X, leftRacket.Position.Y, leftRacket.Size.Height);



            watch.Restart();
        }


        /// <summary>
        /// Checks collisions with Racket
        /// </summary>
        /// <param name="b">used ball</param>
        /// <param name="racket">Racket to check</param>
        /// <param name="dir">RacketDirection</param>
        /// <param name="calc">Auto play second racket</param>
        public void checkColision(Ball b, Racket racket, Racket.Direction dir, bool calc = false)
        {
            int rackTop = racket.Position.Y - b.Size.Height / 2;
            int rackBot = racket.Position.Y + racket.Size.Height + b.Size.Height / 2;

            //Console.WriteLine("{0}/{1}", rackTop, rackBot);

            if (dir == Racket.Direction.Right)
            {

                if (b.Position.X >= (racket.Position.X - b.Size.Width))
                {
                  //  Console.WriteLine("LOOG  {0}>={1}", b.Position.X, racket.Position.X - (b.Size.Width / 2) - racket.Size.Width);
                    if (b.Position.Y >= rackTop && b.Position.Y <= rackBot)   //evntl. toleranzzone berechnen
                    {
                        b.dx *= -1;
                        b.Position.X = b.Position.X - 5;
                        Console.WriteLine("Collided right:" + b.Position.ToString());
                      

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
                                Ball.calc(this, ball.Clone(), leftRacket, Racket.Direction.Left);
                            };
                            dispatcherTimer.Start();

                        }


                    }
                }
            }
            else if (dir == Racket.Direction.Left)
            {

                if (b.Position.X <= (racket.Position.X + b.Size.Width / 12))
                {

                    if (b.Position.Y >= rackTop && b.Position.Y <= rackBot)
                    {
                        b.dx *= -1;
                        b.Position.X = b.Position.X + 5;
                        Console.WriteLine("Collided left:" + b.Position.ToString());
                        this.calc = false;
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
                                Ball.calc(this, ball.Clone(), rightRacket, Racket.Direction.Right);
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







        /// <summary>
        /// Initialises the game elements
        /// </summary>
        internal void init()
        {
            ball = new Ball(new Point(430, 130), new Size(50, 50));
            leftRacket = new Racket(new Point(Racket.xDistance, 20), new Size(10, 170));
            rightRacket = new Racket(new Point(gameRenderSize.Width - Racket.xDistance, 225), new Size(10, 170));


            drawList.Add(ball);
            drawList.Add(leftRacket);
            drawList.Add(rightRacket);

        }

        public void test()
        {
           
            List<Point> collisionList = new List<Point>();
            int dx = ball.dx;
            int dy = ball.dy;
            Point ballCoordiantes = new Point(ball.Position.X,ball.Position.Y);
            Size renSize = new Size(ball.Size.Width,ball.Size.Height);

            for (int i = 0; i < 250; i++)
            {


                bool end = false;
                while (!end)
                {
                    ballCoordiantes.X += dx/100;
                    ballCoordiantes.Y += dy;

                    if (ballCoordiantes.X < 0)   // Left
                    {
                        
                        dx *= -1;
                        ballCoordiantes.X = 0;

                        collisionList.Add(new Point(ballCoordiantes.X, ballCoordiantes.Y));
                        end = true;

                    }

                    if ((ballCoordiantes.X + renSize.Width) >= gameRenderSize.Width) //  right
                    {
                        dx *= -1;
                        ballCoordiantes.X = gameRenderSize.Width - renSize.Width;
                        Console.WriteLine("Colision right " + ballCoordiantes.Y);
                        collisionList.Add(new Point(ballCoordiantes.X, ballCoordiantes.Y));

                        end = true;
                    }


                    if (ballCoordiantes.Y < 0)    //top
                    {
                        dy *= -1;
                        ballCoordiantes.Y = 0;



                    }

                    if ((ballCoordiantes.Y + renSize.Height) > gameRenderSize.Height)  //BOt
                    {
                        dy *= -1;
                        ballCoordiantes.Y = gameRenderSize.Height - renSize.Height;


                    }



                }
            }

            foreach (var item in collisionList)
            {
                Console.WriteLine(item.ToString());
            }

        }


    }



}
