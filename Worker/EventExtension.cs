using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Worker
{
    public static class EventExtensions
    {
        /// <summary>Raises the event (on the UI thread if available).</summary>
        /// <param name="multicastDelegate">The event to raise.</param>
        /// <param name="args">The arguments of the event.</param>
        /// <returns>The return value of the event invocation or null if none.</returns>
        public static object Raise(this MulticastDelegate multicastDelegate, object[] args)
        {
            object retVal = null;

            MulticastDelegate threadSafeMulticastDelegate = multicastDelegate;
            if (threadSafeMulticastDelegate != null) {
                foreach (Delegate d in threadSafeMulticastDelegate.GetInvocationList()) {
                    var synchronizeInvoke = d.Target as ISynchronizeInvoke;
                    if ((synchronizeInvoke != null) && synchronizeInvoke.InvokeRequired) {
                        retVal = synchronizeInvoke.EndInvoke(synchronizeInvoke.BeginInvoke(d, args));
                    }
                    else {
                        retVal = d.DynamicInvoke(args);
                    }
                }
            }

            return retVal;
        }
    }
}
