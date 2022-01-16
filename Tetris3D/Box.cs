using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tetris3D
{
    class Box : IComparable<Box>
    {
        #region Parameters
        enum sideTypes { top = 0, bottom = 1, left = 2, right = 3, front = 4, back = 5 }
        Polygon3D[] sides = new Polygon3D[6];
        int ghost = 0;
        Point3D center = new Point3D();
        #endregion

        #region Constructors
        public Box() { }
        public Box(Point3D center, int pieceType, int cellSize, int ghost)
        {
            this.center = center;
            this.ghost = ghost;

            #region Vertices Creation
            // create a collection of lines for a 2x2x2 cube centered at the origin
            List<Point3D> vertices = new List<Point3D>();
            //left
            vertices.Add(new Point3D(-1, -1, 1));
            vertices.Add(new Point3D(-1, -1, -1));
            vertices.Add(new Point3D(-1, 1, -1));
            vertices.Add(new Point3D(-1, 1, 1));
            Left = new Polygon3D(vertices, pieceType, ghost);

            //right
            vertices = new List<Point3D>();
            vertices.Add(new Point3D(1, -1, -1));
            vertices.Add(new Point3D(1, -1, 1));
            vertices.Add(new Point3D(1, 1, 1));
            vertices.Add(new Point3D(1, 1, -1));
            Right = new Polygon3D(vertices, pieceType, ghost);

            //top
            vertices = new List<Point3D>();
            vertices.Add(new Point3D(-1, -1, 1));
            vertices.Add(new Point3D(1, -1, 1));
            vertices.Add(new Point3D(1, -1, -1));
            vertices.Add(new Point3D(-1, -1, -1));
            Top = new Polygon3D(vertices, pieceType, ghost);

            //bottom
            vertices = new List<Point3D>();
            vertices.Add(new Point3D(-1, 1, -1));
            vertices.Add(new Point3D(1, 1, -1));
            vertices.Add(new Point3D(1, 1, 1));
            vertices.Add(new Point3D(-1, 1, 1));
            Bottom = new Polygon3D(vertices, pieceType, ghost);

            //back
            vertices = new List<Point3D>();
            vertices.Add(new Point3D(-1, -1, -1));
            vertices.Add(new Point3D(1, -1, -1));
            vertices.Add(new Point3D(1, 1, -1));
            vertices.Add(new Point3D(-1, 1, -1));
            Back = new Polygon3D(vertices, pieceType, ghost);

            //front
            vertices = new List<Point3D>();
            vertices.Add(new Point3D(1, -1, 1));
            vertices.Add(new Point3D(-1, -1, 1));
            vertices.Add(new Point3D(-1, 1, 1));
            vertices.Add(new Point3D(1, 1, 1));
            Front = new Polygon3D(vertices, pieceType, ghost);

            Scale(cellSize / 2);
            Shift(center);
            #endregion
        }
        #endregion

        #region Properties
        public Polygon3D Top
        {
            get { return sides[(int)sideTypes.top]; }
            set { sides[(int)sideTypes.top] = value; }
        }
        public Polygon3D Bottom
        {
            get { return sides[(int)sideTypes.bottom]; }
            set { sides[(int)sideTypes.bottom] = value; }
        }
        public Polygon3D Left
        {
            get { return sides[(int)sideTypes.left]; }
            set { sides[(int)sideTypes.left] = value; }
        }
        public Polygon3D Right
        {
            get { return sides[(int)sideTypes.right]; }
            set { sides[(int)sideTypes.right] = value; }
        }
        public Polygon3D Front
        {
            get { return sides[(int)sideTypes.front]; }
            set { sides[(int)sideTypes.front] = value; }
        }
        public Polygon3D Back
        {
            get { return sides[(int)sideTypes.back]; }
            set { sides[(int)sideTypes.back] = value; }
        }
        //public Point3D Height
        //{
        //    get { return height; }
        //    set { height = value; }
        //}
        public Point3D Center
        {
            get { return center; }
            set { center = value; }
        }
        public Polygon3D[] Sides
        {
            get { return sides; }          
        }
        #endregion

        #region Methods
        public void Scale(double amount)
        {
            foreach (Polygon3D poly in sides)
                if (poly != null)
                    poly.Scale(amount);
        }
        public void Shift(Point3D amount)
        {
            foreach (Polygon3D poly in sides)
                if (poly != null)
                    poly.Shift(amount);
            center += amount;
        }
        /// <summary>
        /// Rotate the box by the 3D angle specified (degrees)
        /// </summary>
        /// <param name="angle">The 3D angle in degrees</param>
        public void RotateD(Point3D angle)
        {
            foreach (Polygon3D side in sides)
                if (side != null)
                    side.RotateD(angle);
            center.RotateD(angle);
        }
        /// <summary>
        /// UnRotate the box by the 3D angle specified (degrees)
        /// </summary>
        /// <param name="angle">The 3D angle in degrees</param>
        public void UnRotateD(Point3D angle)
        {
            foreach (Polygon3D side in sides)
                if (side != null)
                    side.UnRotateD(angle);
            center.UnRotateD(angle);
        }
        public void Draw(Graphics gr, Pen pen, double distance)
        {
            foreach (Polygon3D poly in sides)
                if (poly != null)
                    poly.Draw(gr, pen, distance);
        }
        public void Fill(Graphics gr, double distance, Point3D lightSrc, Face face, int pieceType)
        {
            foreach (Polygon3D poly in sides)
                if (poly != null)
                    poly.Fill(gr, distance, face, lightSrc);
        }
        #endregion

        #region Comparator
        public int CompareTo(Box otherBox)
        {
            return (int)(this.center.Z - otherBox.center.Z);
        }
        #endregion
    }
}