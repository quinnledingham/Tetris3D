using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris3D
{
    class Polygon3D : IComparable<Polygon3D>
    {
        #region Parameters
        List<Point3D> vertices = new List<Point3D>();
        Color[] colors = new Color[8] { Color.Yellow, Color.Blue, Color.Orange, Color.DarkRed, Color.Purple, Color.DarkBlue, Color.Green, Color.Black };
        bool visible = true;
        int pieceType = 0;
        int ghost = 0;
        #endregion

        #region Constructors
        public Polygon3D() { }
        public Polygon3D(List<Point3D> points, int pieceType)
        {
            vertices = points;
            this.pieceType = pieceType;
        }
        public Polygon3D(List<Point3D> points, int pieceType, int ghost)
        {
            vertices = points;
            this.pieceType = pieceType;
            this.ghost = ghost;
        }
        #endregion

        #region Properties
        public Color ColorFront
        {
            get { return colors[(int)Face.front]; }
            set { colors[(int)Face.front] = value; }
        }
        public Color ColorBack
        {
            get { return colors[(int)Face.back]; }
            set { colors[(int)Face.back] = value; }
        }
        public Point3D Center
        {
            get
            {
                Point3D center = new Point3D(0, 0, 0);
                foreach (Point3D vertex in vertices)
                    center += vertex;
                center /= vertices.Count;
                return center;
            }
        }
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        public int Ghost
        {
            get { return ghost; }
        }
        #endregion

        #region Methods
        public void AddVertex(Point3D pt)
        {
            vertices.Add(pt);
        }
        /// <summary>
        /// Delete a vertex from the polygon. We will delete the closest point to the one passed
        /// within one unit of separation.
        /// </summary>
        /// <param name="pt">The point to delete</param>
        /// <returns>True if point deleted, false otherwise</returns>
        public bool DeleteVertex(Point3D pt)
        {
            // find the closest point
            Point3D closestPt = null;
            double currentDistance = double.MaxValue;
            foreach (Point3D p in vertices)
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
        public void Scale(double amount)
        {
            for (int i = 0; i < vertices.Count; i++)
                vertices[i] *= amount;
        }
        public void Shift(Point3D amount)
        {
            for (int i = 0; i < vertices.Count; i++)
                vertices[i] += amount;
        }
        /// <summary>
        /// Rotate the box by the 3D angle specified (degrees)
        /// </summary>
        /// <param name="angle">The 3D angle in degrees</param>
        public void RotateD(Point3D angle)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].RotateD(angle);
            }
        }
        /// <summary>
        /// UnRotate the box by the 3D angle specified (degrees)
        /// </summary>
        /// <param name="angle">The 3D angle in degrees</param>
        public void UnRotateD(Point3D angle)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].UnRotateD(angle);
            }
        }
        /// <summary>
        /// Calculate the vector normal to this polygon
        /// </summary>
        /// <returns>The normal (perpendicular) vector as a unit vector</returns>
        private Point3D Normal()
        {
            Point3D vector1 = vertices[1] - vertices[0];
            Point3D vector2 = vertices[2] - vertices[0];
            Point3D normal = vector1 ^ vector2;
            normal.Normalize();
            return normal;
        }
        /// <summary>
        /// Calculate the reflectivity of the light off the polygon.
        /// </summary>
        /// <param name="lightSrc">The location of the light source</param>
        /// <returns>a value between 0 and 255</returns>
        private int Reflectivity(Point3D lightSrc)
        {
            Point3D normal = Normal(); // get the normal to this polygon
            // calculate the cos of the angle between the normal and the light source
            // this is simply the dot product
            double dotProductToLight = normal * lightSrc / lightSrc.Magnitude;

            // calculate the dotProduct between the normal and the viewer
            double dotProductToViewer = dotProductToLight * normal * new Point3D(0, 0, 1);

            // calculate the alpha value
            int alpha = (int)((dotProductToViewer + 1) / 2 * 255);
            return alpha;
        }
        public void Draw(Graphics gr, Pen pen, double distance)
        {
            Polygon2D poly = new Polygon2D();

            pen = new Pen(Color.FromArgb(255, colors[pieceType]));
            if (ghost == 1)
                pen = new Pen(Color.FromArgb(160, colors[pieceType]));

            foreach (Point3D v in vertices)
                poly.AddVertex(v.Projection(distance));
            poly.Draw(gr, pen);
        }
        /// <summary>
        /// Fill the polygon on the display
        /// </summary>
        /// <param name="gr">The graphics device</param>
        /// <param name="distance">Distance from device to viewer</param>
        /// <param name="faceToFill">The face being drawn (front or back)</param>
        /// /// <param name="lightSrc">The location of the light source</param>
        public void Fill(Graphics gr, double distance, Face faceToFill, Point3D lightSrc)
        {
            if (visible == false)
                return;

            Polygon2D poly = new Polygon2D();
            foreach (Point3D v in vertices)
                poly.AddVertex(v.Projection(distance));
            // determine which face is showing
            Face whichFace = poly.Face;
            if (whichFace != faceToFill) return; // nothing to do

            int alpha = Reflectivity(lightSrc);
            //if (whichFace == Face.back)
            //    alpha = 255 - alpha;
           
            Brush brush = new SolidBrush(Color.FromArgb(alpha, colors[pieceType]));
            if (ghost == 1)
                brush = new SolidBrush(Color.FromArgb(70, colors[pieceType]));

            //LinearGradientBrush lgBrush = new LinearGradientBrush(poly.BoundingRectangle, Color.FromArgb(alpha, colors[(int)whichFace]), Color.FromArgb(100, Color.Black),
            //    (float)(Math.Atan2(poly.Center.Y - lightSrc.Y, poly.Center.X - lightSrc.X) * 180 / Math.PI)); // Angle in degrees
            // to fix for the transparency issue created by the reflectivity (alpha) value,
            // we first color in solid background color (black)

            poly.Fill(gr, brush, ghost);
        }
        #endregion

        #region Comparator
        public int CompareTo(Polygon3D otherPolygon3D)
        {
            return (int)(this.Center.Z - otherPolygon3D.Center.Z);
        }
        #endregion
    }
}