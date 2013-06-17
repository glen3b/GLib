using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.WinForms
{
    /// <summary>
    /// An interface representing a required winforms field.
    /// </summary>
    public interface IRequiredField
    {
        /// <summary>
        /// Whether or not the entry is completed properly.
        /// </summary>
        bool Completed { get; }


        /// <summary>
        /// Gets a string indicating the error to display if the field is invalid.
        /// </summary>
        string InvalidityError { get; }
    }
}
