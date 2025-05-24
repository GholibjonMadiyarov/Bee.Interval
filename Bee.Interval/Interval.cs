using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace Bee.Interval
{
    public class Interval
    {
        private bool active;
        private int seconds;

        private Thread t;

        public Interval()
        {
            this.active = false;

            //30 seconds
            this.seconds = 30;
        }

        public void version(string path = null)
        {
            if (path != null)
            {
                Log.info(path, "Version:" + Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString());
            }
        }

        public void start(int seconds = 30, Action callback = null)
        {
            int i = seconds;

            this.seconds = seconds;

            this.active = true;

            t = new Thread(() =>
            {
                while (active) 
                {
                    while (i > 0)
                    { 
                        Thread.Sleep(1000);
                        i--;
                    }

                    i = seconds;

                    if (active == false)
                        return;

                    if (callback != null)
                        callback();
                }
            });

            t.IsBackground = true;
            t.Start();
        }

        public void stop()
        {
            this.active = false;
            this.seconds = 0;
        }
    }
}
