using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pong
{
    /// <summary>
    /// Interface for all Drawable Graphic elements in the game
    /// </summary>
   public interface IDrawable
    {
       /// <summary>
       /// Generic draw method, have to be overritten by all classes which inherit this interface
       /// </summary>
       /// <param name="g">Graphics element to draw</param>
       void Draw(Graphics g);
    }
}
