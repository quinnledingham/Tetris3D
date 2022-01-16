using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris3D
{
    class Point3D
    {
        #region Parameters
        /// <summary>
        /// the three main variables for a 2D point are x, y, z coordinate values
        /// </summary>
        double x = 0, y = 0, z = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a class object with default initial values
        /// </summary>
        public Point3D() { }
        /// <summary>
        /// Construct a 3D point with the values passed
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="z">z-coordinate</param>
        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set the x-value of this point.
        /// </summary>
        public double X { get { return x; } set { x = value; } }
        /// <summary>
        /// Get/Set the y-value of this point.
        /// </summary>
        public double Y { get { return y; } set { y = value; } }
        /// <summary>
        /// Get/Set the z-value of this point.
        /// </summary>
        public double Z { get { return z; } set { z = value; } }
        /// <summary>
        /// Calculate the magnitude of the vector or point (distance from the origin)
        /// </summary>
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(x * x + y * y + z * z);
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Define the point addition operator
        /// </summary>
        /// <param name="ptA"></param>
        /// <param name="ptB"></param>
        /// <returns>The coordinate sum of the two points</returns>
        public static Point3D operator +(Point3D ptA, Point3D ptB)
        {
            return new Point3D(ptA.x + ptB.x, ptA.y + ptB.y, ptA.z + ptB.z);
        }

        /// <summary>
        /// Define the point subtraction operator
        /// </summary>
        /// <param name="ptA"></param>
        /// <param name="ptB"></param>
        /// <returns>The coordinate difference of the two points</returns>
        public static Point3D operator -(Point3D ptA, Point3D ptB)
        {
            return new Point3D(ptA.x - ptB.x, ptA.y - ptB.y, ptA.z - ptB.z);
        }
        /// <summary>
        /// Define the point multiplication operator for a dot-product
        /// </summary>
        /// <param name="ptA"></param>
        /// <param name="ptB"></param>
        /// <returns>The dot-product calculation</returns>
        public static double operator *(Point3D ptA, Point3D ptB)
        {
            return (ptA.x * ptB.x) + (ptA.y * ptB.y) + (ptA.z * ptB.z);
        }
        /// <summary>
        /// Determine the cross-product of two points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>The resulting cross product is returned</returns>
        public static Point3D operator ^(Point3D p1, Point3D p2)
        {
            return new Point3D(
                p1.y * p2.z - p1.z * p2.y,
                p1.z * p2.x - p1.x * p2.z,
                p1.x * p2.y - p1.y * p2.x
                );
        }
        /// <summary>
        /// Define the multiplication operator
        /// </summary>
        /// <param name="ptA"></param>
        /// <param name="ptB"></param>
        /// <returns>The coodinate scaled by the multiplier</returns>
        public static Point3D operator *(Point3D ptA, double n)
        {
            return new Point3D(ptA.x * n, ptA.y * n, ptA.z * n);
        }
        /// <summary>
        /// Define the multiplication operator
        /// </summary>
        /// <param name="ptA"></param>
        /// <param name="ptB"></param>
        /// <returns>The coodinate scaled by the multiplier</returns>

        public static Point3D operator *(double n, Point3D ptA)
        {
            return new Point3D(n * ptA.x, n * ptA.y, n * ptA.z);
        }
        /// <summary>
        /// Define the point division operator
        /// </summary>
        /// <param name="ptA"></param>
        /// <param name="n"></param>
        /// <returns>The coordinate scaled by the divisor</returns>
        public static Point3D operator /(Point3D ptA, double n)
        {
            return new Point3D(ptA.x / n, ptA.y / n, ptA.z / n);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Rotate the point by the angle specified (degrees)
        /// </summary>
        /// <param name="angle"></param>
        public void RotateD(Point3D angle)
        {
            Point3D radians = angle * Math.PI / 180;
            double x2 = x, y2 = y, z2 = z;

            // rotate in the z-axis (change the x, y values)
            x2 = x * Math.Cos(radians.z) - y * Math.Sin(radians.z);
            y2 = x * Math.Sin(radians.z) + y * Math.Cos(radians.z);
            x = x2;
            y = y2;
            // rotate in the y-axis (change the x, z values)
            x2 = x * Math.Cos(radians.y) - z * Math.Sin(radians.y);
            z2 = x * Math.Sin(radians.y) + z * Math.Cos(radians.y);
            x = x2;
            z = z2;
            // rotate in the x-axis (change the z, y values)
            y2 = y * Math.Cos(radians.x) - z * Math.Sin(radians.x);
            z2 = y * Math.Sin(radians.x) + z * Math.Cos(radians.x);
            y = y2;
            z = z2;
        }
        /// <summary>
        /// UnRotate the point by the angle specified (degrees)
        /// </summary>
        /// <param name="angle"></param>
        public void UnRotateD(Point3D angle)
        {
            Point3D radians = angle * Math.PI / 180;
            double x2 = x, y2 = y, z2 = z;

            // rotate in the x-axis (change they y, z values)
            y2 = y * Math.Cos(radians.x) - z * Math.Sin(radians.x);
            z2 = y * Math.Sin(radians.x) + z * Math.Cos(radians.x);
            y = y2;
            z = z2;
            // rotate in the y-axis (change the x, z values)
            x2 = x * Math.Cos(radians.y) - z * Math.Sin(radians.y);
            z2 = x * Math.Sin(radians.y) + z * Math.Cos(radians.y);
            x = x2;
            z = z2;
            // rotate in the z-axis (change the x, y values)
            x2 = x * Math.Cos(radians.z) - y * Math.Sin(radians.z);
            y2 = x * Math.Sin(radians.z) + y * Math.Cos(radians.z);
            x = x2;
            y = y2;
        }
        /// <summary>
        /// Normalize the point/vector to have a length of 1
        /// </summary>
        public void Normalize()
        {
            double magnitude = Magnitude;
            x /= magnitude;
            y /= magnitude;
            z /= magnitude;
        }
        /// <summary>
        /// Project the 3D point onto the 2D surface.
        /// </summary>
        /// <param name="distance">The distnace from the observer to the projection surface.</param>
        /// <returns></returns>
        public Point2D Projection(double distance)
        {
            return new Point2D(
                x * (distance) / (distance - z),
                y * (distance) / (distance - z)
                );
        }
        public Point3D Move(Point3D center)
        {
            Point3D newCenter = new Point3D();
            x += center.X;
            y += center.Y;
            z += center.Z;
            return newCenter;
        }
        public void Draw(Graphics gr, Color color, double distance)
        {
            Projection(distance).Draw(gr, color);
        }
        #endregion
    }
}