using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Glib.WinForms.Controls
{
    /// <summary>
    /// A required text box representing a SQL parameter.
    /// </summary>
    public class RequiredSqlParameterTextBox : SqlParameterTextBox, IRequiredField
    {
        /// <summary>
        /// Create a new SqlParameterTextBox with the specified parameter name and default validation code.
        /// </summary>
        /// <param name="parameterName">The name of the SQL parameter.</param>
        public RequiredSqlParameterTextBox(string parameterName) : base(parameterName)
        {
            
        }

        /// <summary>
        /// Whether or not the entry is completed properly.
        /// Is determined by the return value of FieldValidation.
        /// </summary>
        public bool Completed
        {
            get
            {
                return FieldValidation == null ? RequiredTextBox.IsValid(Text) : FieldValidation.Invoke(Text);
            }
        }

        /// <summary>
        /// The predicate checking whether or not this field is complete.
        /// True means complete, false means incomplete.
        /// </summary>
        [Description("The predicate to use to determine if this field is complete.")]
        public Predicate<string> FieldValidation = new Predicate<string>(RequiredTextBox.IsValid);

        /// <summary>
        /// Create a new SqlParameterTextBox with the specified parameter name and validation code.
        /// </summary>
        /// <param name="parameterName">The name of the SQL parameter.</param>
        /// <param name="validator">The predicate to use to check if the field is complete.</param>
        public RequiredSqlParameterTextBox(string parameterName, Predicate<string> validator)
            : this(parameterName)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            FieldValidation = validator;
        }

        private string _invalidityError = "This field is not completed properly.";

        /// <summary>
        /// Gets or sets a string indicating the error message to display if the field is invalid.
        /// </summary>
        [DefaultValue("This field is not completed properly.")]
        public string InvalidityError
        {
            get { return _invalidityError; }
            set
            {
                _invalidityError = value;
            }
        }
    }

    /// <summary>
    /// A required text box.
    /// </summary>
    public class RequiredTextBox : TextBox, IRequiredField
    {
        /// <summary>
        /// The default field validation predicate.
        /// </summary>
        /// <remarks>
        /// Checks if the field text is empty, and returns that value inverted.
        /// </remarks>
        /// <param name="str">The text of the field.</param>
        /// <returns>Whether or not the field is valid.</returns>
        public static bool IsValid(string str)
        {
            return str != null && str.Trim() != "";
        }

        /// <summary>
        /// Create a new RequiredTextBox with default validation logic.
        /// </summary>
        public RequiredTextBox()
            : base()
        {

        }

        /// <summary>
        /// Whether or not the entry is completed properly.
        /// Is determined by the return value of FieldValidation.
        /// </summary>
        public bool Completed
        {
            get
            {
                return FieldValidation == null ? IsValid(Text) : FieldValidation.Invoke(Text);
            }
        }

        /// <summary>
        /// The predicate checking whether or not this field is complete.
        /// True means complete, false means incomplete.
        /// </summary>
        [Description("The predicate to use to determine if this field is complete.")]
        public Predicate<string> FieldValidation = new Predicate<string>(IsValid);

        /// <summary>
        /// Create a new RequiredTextBox with the specified parameter name and validation predicate.
        /// </summary>
        /// <param name="validator">The predicate to use to check if the field is complete.</param>
        public RequiredTextBox(Predicate<string> validator)
            : this()
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            FieldValidation = validator;
        }

        private string _invalidityError = "This field is not completed properly.";

        /// <summary>
        /// Gets or sets a string indicating the error message to display if the field is invalid.
        /// </summary>
        [Description("The error message to display if the field is invalid.")]
        [DefaultValue("This field is not completed properly.")]
        public string InvalidityError
        {
            get { return _invalidityError; }
            set
            {
                _invalidityError = value;
            }
        }
    }
}
