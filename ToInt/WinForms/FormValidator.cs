using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Glib.WinForms
{
    /// <summary>
    /// The handler for a control validation event.
    /// </summary>
    /// <param name="sender">The object performing validation.</param>
    /// <param name="eventArgs">The <see cref="ControlValidatedEventArgs"/> for this event.</param>
    public delegate void ControlValidatedEventHandler(object sender, ControlValidatedEventArgs eventArgs);

    /// <summary>
    /// Represents event arguments for the <see cref="ControlValidatedEventHandler"/> event.
    /// </summary>
    public class ControlValidatedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the control that was validated.
        /// </summary>
        public Control ValidatedControl { get; protected internal set; }

        /// <summary>
        /// Gets the result of the validation.
        /// </summary>
        public bool ValidationResult { get; protected internal set; }
    }

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
        /// An event fired when a control is validated.
        /// </summary>
        public event ControlValidatedEventHandler ControlValidated;

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
            bool allValid = true;

            foreach (Control c in _controls)
            {
                if (c == null)
                {
                    continue;
                }
                if (c is IRequiredField)
                {
                    IRequiredField field = c.Cast<IRequiredField>();
                    bool fieldFinished = field.Completed;
                    
                    if (!fieldFinished)
                    {
                        allValid = false;
                        errors.SetError(c, field.InvalidityError);
                    }
                    else
                    {
                        errors.SetError(c, null);
                    }

                    if (ControlValidated != null)
                    {
                        ControlValidated(this, new ControlValidatedEventArgs() { ValidatedControl = c, ValidationResult = fieldFinished });
                    }
                }
            }
            return allValid;
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
                        bool fieldComplete = field.Completed;

                        if (ControlValidated != null)
                        {
                            ControlValidated(this, new ControlValidatedEventArgs() { ValidatedControl = c, ValidationResult = fieldComplete });
                        }

                        if (!fieldComplete)
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
