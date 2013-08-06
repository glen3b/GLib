using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Glib.WinForms
{
    /// <summary>
    /// A class validating forms to ensure all <see cref="IRequiredField"/>s are complete.
    /// </summary>
    public class FormValidator
    {
        /// <summary>
        /// Gets the collection of controls to validate.
        /// </summary>
        public Control.ControlCollection ControlsToValidate
        {
            get
            {
                return _controls;
            }
        }

        private Control.ControlCollection _controls;

        /// <summary>
        /// Create a new FormValidator validating the specified form.
        /// </summary>
        /// <param name="formToValidate">The form which contains components to validate.</param>
        public FormValidator(Form formToValidate)
        {
            _controls = formToValidate.Controls;
        }

        /// <summary>
        /// Create a new FormValidator validating the specified controls.
        /// </summary>
        /// <param name="comToValidate">Components to validate.</param>
        public FormValidator(Control.ControlCollection comToValidate)
        {
            _controls = comToValidate;
        }

        /// <summary>
        /// Add the components of the specified form to be validated.
        /// </summary>
        /// <param name="formToAdd">The form which contains components to validate.</param>
        public void AddFormComponents(Form formToAdd)
        {
            foreach (Control c in formToAdd.Controls)
            {
                _controls.Add(c);
            }
        }

        /// <summary>
        /// Validate these controls for errors, marking the controls with errors as errored using the specified ErrorProvider.
        /// </summary>
        /// <param name="errors">The ErrorProvider to use for erroring out controls.</param>
        /// <returns>Whether or not the form is complete.</returns>
        public bool ValidateForm(ErrorProvider errors)
        {
            foreach (Control c in _controls)
            {
                if (c is IRequiredField)
                {
                    IRequiredField field = c.Cast<IRequiredField>();
                    if (!field.Completed)
                    {
                       errors.SetError(c, field.InvalidityError);
                    }
                    else
                    {
                        errors.SetError(c, null);
                    }
                }
            }
            return IsComplete;
        }

        /// <summary>
        /// Gets a boolean representing whether or not all required controls are completed.
        /// </summary>
        public bool IsComplete
        {
            get
            {
                foreach (Control c in _controls)
                {
                    if (c is IRequiredField)
                    {
                        IRequiredField field = c.Cast<IRequiredField>();
                        if (!field.Completed)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Validate the controls on the specified form.
        /// </summary>
        /// <param name="testForm">The form to validate.</param>
        /// <returns>Whether or not the form has all valid controls.</returns>
        public static bool ValidateForm(Form testForm)
        {
            return new FormValidator(testForm.Controls).IsComplete;
        }
    }
}
