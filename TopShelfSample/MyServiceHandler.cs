using System.Timers;

namespace TopShelfSample
{
    public class MyServiceHandler
    {
        private System.Timers.Timer aTimer;

        public void Start()
        {
            // startup code here
            // run thread, start timer, or connect to external service (like a queue)
            SetTimer();
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }

        public void Stop()
        {
            // cleanup
            aTimer.Stop();
        }
    }
}
