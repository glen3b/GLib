using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Mail;

namespace Glib.Networking
{
    /// <summary>
    /// An email message that can be sent.
    /// </summary>
    public class Email
    {
        /// <summary>
        /// The mail message to be sent.
        /// </summary>
        public MailMessage Message;

        /// <summary>
        /// The SmtpClient used in sending the message.
        /// </summary>
        public SmtpClient Sending;

        /// <summary>
        /// Send the mail message.
        /// </summary>
        public void Send()
        {
            Sending.Send(Message);
        }

        /// <summary>
        /// Create a new email message.
        /// </summary>
        /// <param name="from">The email address to send from.</param>
        /// <param name="send">The client to use to send the message.</param>
        public Email(MailAddress from, SmtpClient send)
        {
            Sender = from;
            From = from;
            Sending = send;
        }

        /// <summary>
        /// Occurs when an async send is completed.
        /// </summary>
        public event SendCompletedEventHandler AsyncSendCompleted
        {
            add
            {
                Sending.SendCompleted += value;
            }
            remove
            {
                Sending.SendCompleted -= value;
            }
        }

        /// <summary>
        /// Send the mail message asynchronously.
        /// </summary>
        public void SendAsync()
        {
            Sending.SendAsync(Message, null);
        }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        public string Subject
        {
            get
            {
                return Message.Subject;
            }
            set
            {
                Message.Subject = value;
            }
        }

        /// <summary>
        /// Gets or sets the body of the email.
        /// </summary>
        public string Body
        {
            get
            {
                return Message.Body;
            }
            set
            {
                Message.Body = value;
            }
        }

        /// <summary>
        /// Gets or sets a boolean representing whether or not the message body is HTML.
        /// </summary>
        public bool IsHtml
        {
            get
            {
                return Message.IsBodyHtml;
            }
            set
            {
                Message.IsBodyHtml = value;
            }
        }

        /// <summary>
        /// Gets the attachments to be sent along with this mail message.
        /// </summary>
        public AttachmentCollection Attachments
        {
            get
            {
                return Message.Attachments;
            }
        }

        /// <summary>
        /// Gets the collection of recipient mail addresses.
        /// </summary>
        public MailAddressCollection Recipients
        {
            get
            {
                return Message.To;
            }
        }

        /// <summary>
        /// Gets or sets the mail message sender.
        /// </summary>
        public MailAddress Sender
        {
            get
            {
                return Message.Sender;
            }
            set
            {
                Message.Sender = value;
            }
        }

        /// <summary>
        /// Gets or sets the mail message from address.
        /// </summary>
        public MailAddress From
        {
            get
            {
                return Message.From;
            }
            set
            {
                Message.From = value;
            }
        }
    }

    /// <summary>
    /// A class for pinging IPs.
    /// </summary>
    public class IpPing
    {
        private Ping _ping;

        private int _timeOut;

        /// <summary>
        /// Get or set the ping timeout in milliseconds.
        /// </summary>
        public int TimeOut
        {
            get
            {
                return _timeOut;
            }
            set
            {
                _timeOut = value;
            }
        }

        /// <summary>
        /// Create a new IpPing.
        /// </summary>
        public IpPing()
        {
            this._ping = new Ping();
            this._timeOut = 1500;
        }

        /// <summary>
        /// Create a new IpPing with the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout of the ping in milliseconds.</param>
        public IpPing(int timeout)
            : this()
        {
            this._timeOut = timeout;
        }

        private bool Ping(IPAddress address, int timeout)
        {
            bool status = this._ping.Send(address, timeout).Status == IPStatus.Success;
            return status;
        }

        /// <summary>
        /// Ping the first IP address associated with the specified domain.
        /// </summary>
        /// <param name="domainName">The domain to ping.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>Whether or not the ping succeeded.</returns>
        public bool PingDomain(string domainName, int timeout)
        {
            return Ping(Dns.GetHostAddresses(domainName)[0], timeout);
        }

        /// <summary>
        /// Ping the specified IP address associated with the specified domain.
        /// </summary>
        /// <param name="domainName">The domain to ping.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="whichDomainIp">The zero-based index of an IPAddress in System.Net.Dns.GetHostAddresses(domainName).</param>
        /// <returns>Whether or not the ping succeeded.</returns>
        public bool PingDomain(string domainName, int timeout, int whichDomainIp)
        {
            return Ping(Dns.GetHostAddresses(domainName)[whichDomainIp], timeout);
        }

        /// <summary>
        /// Ping the first IP address associated with the specified domain.
        /// </summary>
        /// <param name="domainName">The domain to ping.</param>
        /// <returns>Whether or not the ping succeeded.</returns>
        public bool PingDomain(string domainName)
        {
            return Ping(Dns.GetHostAddresses(domainName)[0], _timeOut);
        }

        /// <summary>
        /// Ping the specified IP address.
        /// </summary>
        /// <param name="ip">The IP to ping.</param>
        /// <returns>Whether or not the ping succeeded.</returns>
        public bool PingIP(string ip)
        {
            return this.Ping(IPAddress.Parse(ip), this._timeOut);
        }

        /// <summary>
        /// Ping the specified IP.
        /// </summary>
        /// <param name="ip">The IP address to ping.</param>
        /// <returns>Whether or not the ping succeeded.</returns>
        public bool PingIP(IPAddress ip)
        {
            bool flag = this.Ping(ip, this._timeOut);
            return flag;
        }

        /// <summary>
        /// Ping the specified IP with the specified timeout.
        /// </summary>
        /// <param name="ip">The IP address to ping.</param>
        /// <param name="timeout">The timeout, in milliseconds, of the ping.</param>
        /// <returns>Whether or not the ping succeeded.</returns>
        public bool PingIP(string ip, int timeout)
        {
            bool flag = this.Ping(IPAddress.Parse(ip), timeout);
            return flag;
        }

        /// <summary>
        /// Ping the specified IP with the specified timeout.
        /// </summary>
        /// <param name="ip">The IP address to ping.</param>
        /// <param name="timeout">The timeout, in milliseconds, of the ping.</param>
        /// <returns>Whether or not the ping succeeded.</returns>
        public bool PingIP(IPAddress ip, int timeout)
        {
            bool flag = this.Ping(ip, timeout);
            return flag;
        }
    }
}
