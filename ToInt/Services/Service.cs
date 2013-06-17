using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Timers;

namespace Glib.Services
{
    /// <summary>
    /// An abstract service class representing repeatedly executing code.
    /// </summary>
    public abstract class Service : ServiceBase
    {
        /// <summary>
        /// The delay, in milliseconds, between executions of the service code.
        /// </summary>
        protected abstract double delay { get; set; }

        /// <summary>
        /// A boolean representing whether or not this service needs administrator-level permissions on the local system.
        /// </summary>
        public abstract bool IsPrivileged { get; }

        /// <summary>
        /// The delay, in milliseconds, between executions of the service code.
        /// </summary>
        public double Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
                ServiceTimer.Interval = delay;
            }
        }

        /// <summary>
        /// Create a new service.
        /// </summary>
        public Service() : base(){
            ServiceTimer = new Timer(Delay);
            ServiceTimer.AutoReset = true;
            ServiceTimer.Elapsed += new ElapsedEventHandler(ServiceTimer_Elapsed);
        }

        private void ServiceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Main();
        }

        /// <summary>
        /// The main service code. that runs repeatedly every tick of ServiceTimer.
        /// </summary>
        protected abstract void Main();

        /// <summary>
        /// The underlying timer that is ticking the service.
        /// </summary>
        protected Timer ServiceTimer;
        /// <summary>
        /// Start the service timer.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            ServiceTimer.Enabled = true;
            base.OnStart(args);
        }

        /// <summary>
        /// Stop the service timer.
        /// </summary>
        protected override void OnStop()
        {
            ServiceTimer.Enabled = false;
            base.OnStop();
        }
    }
}
