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

            checkColision(ball, leftRacket, Racket.Direction.Left, true);
            checkColision(ball, rightRacket, Racket.Direction.Right);

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
                    Console.WriteLine("LOOG  {0}>={1}", b.Position.X, racket.Position.X - (b.Size.Width / 2) - racket.Size.Width);
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
            ball = new Ball(new Point(430, 30), new Size(50, 50));
            leftRacket = new Racket(new Point(Racket.xDistance, 20), new Size(10, 170));
            rightRacket = new Racket(new Point(gameRenderSize.Width - Racket.xDistance, 225), new Size(10, 170));


            drawList.Add(ball);
            drawList.Add(leftRacket);
            drawList.Add(rightRacket);

        }


    }



}
