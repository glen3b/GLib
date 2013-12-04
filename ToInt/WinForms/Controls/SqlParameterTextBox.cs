using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.SQL;
using System.Windows.Forms;

namespace Glib.WinForms.Controls
{
    /// <summary>
    /// A SQL parameter provider in the form of a text box.
    /// </summary>
    public class SqlParameterTextBox : TextBox, ISQLParameterProvider
    {
        /// <summary>
        /// Create a new SqlParameterTextBox with the specified parameter name.
        /// </summary>
        /// <param name="parameterName">The name of the SQL parameter.</param>
        public SqlParameterTextBox(string parameterName)
        {
            _paramName = parameterName;
        }

        private string _paramName;

        /// <summary>
        /// Gets the name of the represented SQL parameter.
        /// </summary>
        public string ParameterName
        {
            get
            {
                return _paramName;
            }
        }

        /// <summary>
        /// Gets or sets the string value of the text box, which is the value of the SQL parameter.
        /// </summary>
        public object ParameterValue
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value.ToString();
            }
        }
    }
}
