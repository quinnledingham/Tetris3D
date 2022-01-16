using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris3D
{
    class Polygon2D
    {
        #region Parameters
        List<Point2D> vertices = new List<Point2D>();

        #endregion

        #region Constructors
        public Polygon2D() { }
        public Polygon2D(List<Point2D> points)
        {
            vertices = points;
        }
        #endregion

        #region Properties
        public Face Face
        {
            get
            {
                if (vertices.Count < 3) return Face.front;
                // calculate two vectors
                Point2D v1 = vertices[1] - vertices[0];
                Point2D v2 = vertices[2] - vertices[0];
                if (v1.X * v2.Y - v1.Y * v2.X > 0)
                    return Face.front;
                else
                    return Face.back;
            }
        }
        public RectangleF BoundingRectangle
        {
            get
            {
                RectangleF boundingRectangle = new RectangleF((float)vertices[0].X, (float)vertices[0].Y,
                    (float)(vertices[1].X - vertices[0].X), (float)(vertices[2].Y - vertices[1].Y));
                return boundingRectangle;
            }
        }
        public Point2D Center
        {
            get
            {
                Point2D center = new Point2D((vertices[1].X - vertices[0].X) / 2, (vertices[2].Y - vertices[1].Y) / 2);
                return center;
            }
        }
        #endregion

        #region Methods
        public void AddVertex(Point2D pt)
        {
            vertices.Add(pt);
        }
        /// <summary>
        /// Delete a vertex from the polygon. We will delete the closest point to the one passed
        /// within one unit of separation.
        /// </summary>
        /// <param name="pt">The point to delete</param>
        /// <returns>True if point deleted, false otherwise</returns>
        public bool DeleteVertex(Point2D pt)
        {
            // find the closest point
            Point2D closestPt = null;
            double currentDistance = double.MaxValue;
            foreach (Point2D p in vertices)
                if ((p - pt).Magnitude < currentDistance)
                {
                    currentDistance = (p - pt).Magnitude;
                    closestPt = p;
                }
            if (currentDistance < 1)
            {
                vertices.Remove(closestPt);
                return true;
            }
            return false;
        }
        public void Draw(Graphics gr, Pen pen)
        {
            // convert th elist to an array of PointF
            PointF[] thePoints = new PointF[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
                thePoints[i] = vertices[i].ToPointF();
            gr.DrawPolygon(pen, thePoints);
        }
        public void Fill(Graphics gr, Brush brush, int ghost)
        {
            // convert th elist to an array of PointF
            PointF[] thePoints = new PointF[vertices.Count];
            for (int i = 0; i < vertices.Count; i++)
                thePoints[i] = vertices[i].ToPointF();
            if (ghost != 1)
                gr.FillPolygon(Brushes.Black, thePoints);
            gr.FillPolygon(brush, thePoints);
            //gr.FillPolygon(gradientBrush, thePoints);
        }
        #endregion
    }
}