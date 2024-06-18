using System;
using System.Threading;

namespace Bee.Interval
{
    public class Interval
    {
        SynchronizationContext synchronizationContext;

        private static bool staticIsStart = false;
        private static int staticCount = 0;

        private static bool isStart = false;
        private static int count = 0;

        public Interval(SynchronizationContext synchronizationContext = null) 
        {
            this.synchronizationContext = synchronizationContext;
        }

        public static void start(int seconds = 15, Action callback = null, SynchronizationContext synchronizationContext = null)
        {
            if (seconds <= 0)
                seconds = 15;
            
            staticIsStart = true;
            staticCount = seconds;

            var t = new Thread(() =>
            {
                while (staticIsStart) 
                { 
                    if (staticCount == seconds)
                    {
                        staticCount = 0;

                        if (callback != null)
                        {
                            if (synchronizationContext != null)
                            {
                                synchronizationContext.Post(_ => callback(), null);
                            }
                            else
                            {
                                callback();
                            }
                        }
                    }

                    Thread.Sleep(1000);
                    staticCount++;
                }
            });

            t.IsBackground = true;
            t.Start();
        }

        public void begin(int seconds = 15, Action callback = null)
        {
            if (seconds <= 0)
                seconds = 15;

            isStart = true;
            count = seconds;

            var t = new Thread(() =>
            {
                while (isStart)
                {
                    if (count == seconds)
                    {
                        count = 0;

                        if (callback != null)
                        {
                            if (this.synchronizationContext != null)
                            {
                                synchronizationContext.Post(_ => callback(), null);
                            }
                            else
                            {
                                callback();
                            }
                        }
                    }

                    Thread.Sleep(1000);
                    count++;
                }
            });

            t.IsBackground = true;
            t.Start();
        }

        public static void stop()
        {
            staticIsStart = false;
        }

        public void end()
        {
            isStart = false;
        }
    }
}
