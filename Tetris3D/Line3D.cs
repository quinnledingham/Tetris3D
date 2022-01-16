using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris3D
{
    class Line3D
    {
        #region Parameters
        Point3D p1 = new Point3D(), p2 = new Point3D();
        #endregion
        #region Constructors
        public Line3D() { }
        public Line3D(Point3D p1, Point3D p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        #endregion
        #region Properties
        public Point3D P1
        {
            get { return p1; }
            set { p1 = value; }
        }
        public Point3D P2
        {
            get { return p2; }
            set { p2 = value; }
        }
        public double Length
        {
            get { return (p1 - p2).Magnitude; }

        }
        #endregion
        #region Methods
        public void RotateD(Point3D angle)
        {
            p1.RotateD(angle);
            p2.RotateD(angle);
        }
        public void UnRotateD(Point3D angle)
        {
            p1.UnRotateD(angle);
            p2.UnRotateD(angle);
        }
        /// <summary>
        /// draw the 3d line to the user's display
        /// </summary>
        /// <param name="gr">The graphics display</param>
        /// <param name="pen">The pen for drawing</param>
        /// <param name="distance">The distance from the user to the display</param>
        public void Draw(Graphics gr, Pen pen, double distance)
        {
            // first project the 3D line onto the 2D surface
            Line2D line = new Line2D(p1.Projection(distance), p2.Projection(distance));
            // set the line's pen         
            line.Draw(gr, pen);
        }
        /// <summary>
        /// Scale the line by a multiplication factor
        /// </summary>
        /// <param name="scale">The scaling amount</param>
        public void Scale(double scale)
        {
            p1 *= scale;
            p2 *= scale;
        }
        #endregion
    }
}