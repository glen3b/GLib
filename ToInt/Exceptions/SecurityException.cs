using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.Exceptions
{
    /// <summary>
    /// An enum representing a type of security risk that caused a <see cref="SecurityException"/>.
    /// </summary>
    public enum SecurityRiskType
    {
        /// <summary>
        /// Represesnts a detected XSS attempt.
        /// </summary>
        XSS,

        /// <summary>
        /// Represents a detected SQL injection attempt.
        /// </summary>
        SQLInject
    }

    /// <summary>
    /// An exception representing a possible security risk.
    /// </summary>
    public class SecurityException : Exception
    {
        /// <summary>
        /// The type of risk represented by the exception. Null means no specific risk type.
        /// </summary>
        public SecurityRiskType? RiskType = null;

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                return String.Format("A potential security risk{0}was detected.", RiskType.HasValue ? " in the form of a(n) "+RiskType.Value+" attack " : " ");
            }
        }

        /// <summary>
        /// Create a new security exception with no specific risk type.
        /// </summary>
        public SecurityException() : base()
        {

        }

        /// <summary>
        /// Create a new security exception with the specified risk type.
        /// </summary>
        /// <param name="risk">The type of security risk.</param>
        public SecurityException(SecurityRiskType risk)
            : base()
        {
            this.RiskType = risk;
        }
    }
}
