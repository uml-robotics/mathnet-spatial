﻿using System;
using MathNet.Numerics;

namespace MathNet.Spatial
{
    /// <summary>Quaternion Number</summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Quaternion
    /// </remarks>
    public struct Quaternion
    {
        readonly double _w; // real part
        readonly double _x, _y, _z; // imaginary part
        readonly double _abs, _norm; // norm
        readonly double _arg; // polar notation

        /// <summary>
        /// Initializes a new instance of the Quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion(double real, double imagX, double imagY, double imagZ)
        {
            _x = imagX;
            _y = imagY;
            _z = imagZ;
            _w = real;
            _norm = ToNorm(real, imagX, imagY, imagZ);
            _abs = Math.Sqrt(_norm);
            _arg = Math.Acos(real/_abs);
        }

        /// <summary>
        /// Initializes a new instance of the Quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        internal Quaternion(double real, double imagX, double imagY, double imagZ, double abs, double norm, double arg)
        {
            _x = imagX;
            _y = imagY;
            _z = imagZ;
            _w = real;
            _norm = norm;
            _abs = abs;
            _arg = arg;
        }

        [System.Diagnostics.DebuggerStepThrough]
        static double ToNorm(double real, double imagX, double imagY, double imagZ)
        {
            return (imagX*imagX) + (imagY*imagY) + (imagZ*imagZ) + (real*real);
        }

        [System.Diagnostics.DebuggerStepThrough]
        static Quaternion ToUnitQuaternion(double real, double imagX, double imagY, double imagZ)
        {
            double abs = Math.Sqrt(ToNorm(real, imagX, imagY, imagZ));
            return new Quaternion(real/abs, imagX/abs, imagY/abs, imagZ/abs, 1, 1, Math.Acos(real/abs));
        }

        /// <summary>
        /// Gets the real part of the quaternion.
        /// </summary>
        public double Real
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _w; }
        }

        /// <summary>
        /// Gets the imaginary X part (coefficient of complex I) of the quaternion.
        /// </summary>
        public double ImagX
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _x; }
        }

        /// <summary>
        /// Gets the imaginary Y part (coefficient of complex J) of the quaternion.
        /// </summary>
        public double ImagY
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _y; }
        }

        /// <summary>
        /// Gets the imaginary Z part (coefficient of complex K) of the quaternion.
        /// </summary>
        public double ImagZ
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _z; }
        }

        /// <summary>
        /// Gets the standard euclidean length |q| = sqrt(||q||) of the quaternion q: the
        /// square root of the sum of the squares of the four components. Q may then be
        /// represented as q = r*(cos(phi) + u * sin(phi)) = r*exp(phi*u) where u is the
        /// unit vector and phi the argument of q.
        /// </summary>
        public double Abs
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _abs; }
        }

        /// <summary>
        /// Gets the norm ||q|| = |q|^2 of the quaternion q: the sum of the squares of the four components.
        /// </summary>
        public double Norm
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _norm; }
        }

        /// <summary>
        /// Gets the argument phi = arg(q) of the quaternion q, such that q = r*(cos(phi) +
        /// u * sin(phi)) = r*exp(phi*u) where r is the absolute and u the unit vector of
        /// q.
        /// </summary>
        public double Arg
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _arg; }
        }

        /// <summary>
        /// True if the quaternion q is of length |q| = 1.
        /// </summary>
        /// <remarks>
        /// To normalize a quaternion to a length of 1, use the <see cref="Sign"/> method.
        /// All unit quaternions form a 3-sphere.
        /// </remarks>
        public bool IsUnitQuaternion
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return _abs.AlmostEqual(1); }
        }

        /// <summary>
        /// Returns a new Quaternion q with the Scalar part only.
        /// If you need a Double, use the Real-Field instead.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Scalar()
        {
            return new Quaternion(_w, 0, 0, 0);
        }

        /// <summary>
        /// Returns a new Quaternion q with the Vector part only.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Vector()
        {
            return new Quaternion(0, _x, _y, _z);
        }

        /// <summary>
        /// Returns a new normalized Quaternion u with the Vector part only, such that ||u|| = 1.
        /// Q may then be represented as q = r*(cos(phi) + u * sin(phi)) = r*exp(phi*u) where r is the absolute and phi the argument of q.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion UnitVector()
        {
            return ToUnitQuaternion(0, _x, _y, _z);
        }

        /// <summary>
        /// Returns a new normalized Quaternion q with the direction of this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Sign()
        {
            return ToUnitQuaternion(_w, _x, _y, _z);
        }

        /////// <summary>
        /////// Returns a new Quaternion q with the Sign of the components.
        /////// </summary>
        /////// <returns>
        /////// <list type="bullet">
        /////// <item>1 if Positive</item>
        /////// <item>0 if Neutral</item>
        /////// <item>-1 if Negative</item>
        /////// </list>
        /////// </returns>
        ////public Quaternion ComponentSigns()
        ////{
        ////    return new Quaternion(
        ////        Math.Sign(_x),
        ////        Math.Sign(_y),
        ////        Math.Sign(_z),
        ////        Math.Sign(_w));
        ////}

        /// <summary>
        /// (nop)
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator +(Quaternion q)
        {
            return q;
        }

        /// <summary>
        /// Negate a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator -(Quaternion q)
        {
            return q.Negate();
        }

        /// <summary>
        /// Add a quaternion to a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return q1.Add(q2);
        }

        /// <summary>
        /// Add a floating point number to a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator +(Quaternion q1, double d)
        {
            return q1.Add(d);
        }

        /// <summary>
        /// Subtract a quaternion from a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return q1.Subtract(q2);
        }

        /// <summary>
        /// Subtract a floating point number from a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator -(Quaternion q1, double d)
        {
            return q1.Subtract(d);
        }

        /// <summary>
        /// Multiply a quaternion with a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            return q1.Multiply(q2);
        }

        /// <summary>
        /// Multiply a floating point number with a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator *(Quaternion q1, double d)
        {
            return q1.Multiply(d);
        }

        /// <summary>
        /// Divide a quaternion by a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator /(Quaternion q1, Quaternion q2)
        {
            return q1.Divide(q2);
        }

        /// <summary>
        /// Divide a quaternion by a floating point number.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator /(Quaternion q1, double d)
        {
            return q1.Divide(d);
        }

        /// <summary>
        /// Raise a quaternion to a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator ^(Quaternion q1, Quaternion q2)
        {
            return q1.Pow(q2);
        }

        /// <summary>
        /// Raise a quaternion to a floating point number.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static Quaternion operator ^(Quaternion q1, double d)
        {
            return q1.Pow(d);
        }

        /// <summary>
        /// Convert a floating point number to a quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static implicit operator Quaternion(double d)
        {
            return new Quaternion(d, 0, 0, 0);
        }

        /// <summary>
        /// Add a quaternion to this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Add(Quaternion q)
        {
            return new Quaternion(_w + q._w, _x + q._x, _y + q._y, _z + q._z);
        }

        /// <summary>
        /// Add a floating point number to this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Add(double r)
        {
            return new Quaternion(_w + r, _x, _y, _z);
        }

        /// <summary>
        /// Subtract a quaternion from this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Subtract(Quaternion q)
        {
            return new Quaternion(_w - q._w, _x - q._x, _y - q._y, _z - q._z);
        }

        /// <summary>
        /// Subtract a floating point number from this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Subtract(double r)
        {
            return new Quaternion(_w - r, _x, _y, _z);
        }

        /// <summary>
        /// Negate this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Negate()
        {
            return new Quaternion(-_w, -_x, -_y, -_z, _abs, _norm, Math.PI - _arg);
        }

        /// <summary>
        /// Multiply a quaternion with this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Multiply(Quaternion q)
        {
            double ci = (+_x*q._w) + (_y*q._z) - (_z*q._y) + (_w*q._x);
            double cj = (-_x*q._z) + (_y*q._w) + (_z*q._x) + (_w*q._y);
            double ck = (+_x*q._y) - (_y*q._x) + (_z*q._w) + (_w*q._z);
            double cr = (-_x*q._x) - (_y*q._y) - (_z*q._z) + (_w*q._w);
            return new Quaternion(cr, ci, cj, ck);
        }

        /// <summary>
        /// Multiply a floating point number to this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Multiply(double d)
        {
            return new Quaternion(d*_w, d*_x, d*_y, d*_z);
        }

        /// <summary>
        /// Multiplies a Quaternion with the inverse of another
        /// Quaternion (q*q<sup>-1</sup>). Note that for Quaternions
        /// q*q<sup>-1</sup> is not the same then q<sup>-1</sup>*q,
        /// because this will lead to a rotation in the other direction.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Divide(Quaternion q)
        {
            return Multiply(q.Inverse());
        }

        /// <summary>
        /// Multiplies a Quaternion with the inverse of a real number.
        /// </summary>
        /// <remarks>
        /// Its also possible to cast a double to a Quaternion and make the division
        /// afterward, but that would be more expensive.
        /// </remarks>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Divide(double d)
        {
            return new Quaternion(_w/d, _x/d, _y/d, _z/d);
        }

        /// <summary>
        /// Inverts this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Inverse()
        {
            if (_abs.AlmostEqual(1))
            {
                return new Quaternion(_w, -_x, -_y, -_z);
            }

            return new Quaternion(_w/_norm, -_x/_norm, -_y/_norm, -_z/_norm);
        }

        /// <summary>
        /// Returns the distance |a-b| of two quaternions, forming a metric space.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static double Distance(Quaternion a, Quaternion b)
        {
            return a.Subtract(b).Abs;
        }

        /// <summary>
        /// Conjugate this quaternion.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Conjugate()
        {
            return new Quaternion(_w, -_x, -_y, -_z, _abs, _norm, _arg);
        }

        /// <summary>
        /// Logarithm to a given base.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Log(double lbase)
        {
            return Ln().Divide(Math.Log(lbase));
        }

        /// <summary>
        /// Natural Logarithm to base E.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Ln()
        {
            return UnitVector().Multiply(_arg).Add(Math.Log(_abs));
        }

        /// <summary>
        /// Common Logarithm to base 10.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Lg()
        {
            return Ln().Divide(Math.Log(10));
        }

        /// <summary>
        /// Exponential Function.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Exp()
        {
            double vabs = Math.Sqrt(ToNorm(0, _x, _y, _z));
            return UnitVector().Multiply(Math.Sin(vabs)).Add(Math.Cos(vabs)).Multiply(Math.Exp(_w));
        }

        /// <summary>
        /// Raise the quaternion to a given power.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Pow(double power)
        {
            double arg = power*_arg;
            return UnitVector().Multiply(Math.Sin(arg)).Add(Math.Cos(arg)).Multiply(Math.Pow(_w, power));
        }

        /// <summary>
        /// Raise the quaternion to a given power.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Pow(Quaternion power)
        {
            return power.Multiply(Ln()).Exp();
        }

        /// <summary>
        /// Square of the Quaternion q: q^2.
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Sqr()
        {
            double arg = _arg*2;
            return UnitVector().Multiply(Math.Sin(arg)).Add(Math.Cos(arg)).Multiply(_w*_w);
        }

        /// <summary>
        /// Square root of the Quaternion: q^(1/2).
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public Quaternion Sqrt()
        {
            double arg = _arg*0.5;
            return UnitVector().Multiply(Math.Sin(arg)).Add(Math.Cos(arg)).Multiply(Math.Sqrt(_w));
        }
    }
}
