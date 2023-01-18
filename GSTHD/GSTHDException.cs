using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    public class GSTHDException : Exception
    {
        public string Title;

        public GSTHDException(string title, string message)
            : base(message)
        {
            Title = title;
        }

        public GSTHDException(string title, string message, Exception inner)
            : base($"{message}\n\nDetails: {inner.Message}", inner)
            //: base($"{message}\n\nDetails: {inner.Message}\n{inner.StackTrace}", inner)
        {
            Title = title;
        }
    }
}
