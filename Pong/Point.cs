using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    /// <summary>
    /// Represents a Point object for handling Pair coordinates
    /// Alternative to System.Drawing.Point
    /// </summary>
    public class Point
    {
        #region publicVars
        public int X { get; set; }
        public int Y { get; set; }
        #endregion


        /// <summary>
        /// Init a Point with the given values
        /// </summary>
        /// <param name="x">X-Value</param>
        /// <param name="y">Y-Value</param>
        public Point(int x, int y)
        {
            Y = y;
            X = x;
        }

        /// <summary>
        /// Converts the PointObject into a string
        /// </summary>
        /// <returns>String representation of the Point object</returns>
        public override string ToString()
        {
            return string.Format("({0}/{1})", X, Y);
        }
    }
}
