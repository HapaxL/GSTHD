using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTHD
{
    public static class Math
    {
        public static class Exception
        {
            [Serializable]
            public class ModuloByZeroException : System.Exception
            {
                private static string msg = "Modulo by zero";
                public ModuloByZeroException() : base(msg) { }
                public ModuloByZeroException(System.Exception inner) : base(msg, inner) { }
            }

            [Serializable]
            public class DivisionByZeroException : System.Exception
            {
                private static string msg = "Division by zero";
                public DivisionByZeroException() : base(msg) { }
                public DivisionByZeroException(System.Exception inner) : base(msg, inner) { }
            }
        }

        public static int FlooredDiv(int a, int b)
        {
            if (b == 0) throw new Exception.DivisionByZeroException();
            return (a / b - Convert.ToInt32(((a < 0) ^ (b < 0)) && (a % b != 0)));
        }

        /// <summary>
        /// Floored modulo (the sign of the result is the same as the divisor's).
        /// </summary>
        public static int FMod(int a, int b)
        {
            if (b == 0) throw new Exception.ModuloByZeroException();
            return a - b * FlooredDiv(a, b);
        }

        /// <summary>
        /// Euclidean modulo (the result is always positive).
        /// </summary>
        public static int EMod(int a, int b)
        {
            if (b == 0) throw new Exception.ModuloByZeroException();
            int babs = System.Math.Abs(b);
            return a - babs * FlooredDiv(a, babs);
        }
    }
}
