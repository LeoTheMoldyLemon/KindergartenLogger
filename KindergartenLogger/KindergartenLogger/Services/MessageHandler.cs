using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace KindergartenLogger.Services
{
    public class MessageHandler
    {
        static Timer timer = new Timer();
        static Timer resetTimer = new Timer();
        public MessageHandler() 
        {
            CheckAlarm();
            timer.AutoReset = false;
            timer.Elapsed += SendEntryMessages;
            resetTimer.Interval = (DateTime.Today + new TimeSpan(1, 0, 0, 10) - DateTime.Now).TotalMilliseconds;
            resetTimer.Enabled = true;
            resetTimer.AutoReset = false;
            resetTimer.Elapsed += ResetAlarm;
        }

        private static void ResetAlarm(Object source, System.Timers.ElapsedEventArgs e)
        {
            resetTimer.Interval = (DateTime.Today + new TimeSpan(1, 0, 0, 10) - DateTime.Now).TotalMilliseconds;
            resetTimer.Enabled = true;
            CheckAlarm();
        }

        public static void CheckAlarm() 
        {
            if (DateTime.Now > (DateTime.Today + App.Data.options.EntryTime))
            {
                timer.Interval = (DateTime.Today + App.Data.options.EntryTime - DateTime.Now).TotalMilliseconds;
                timer.Enabled = true;
            }
        }

        private static void SendEntryMessages(Object source, System.Timers.ElapsedEventArgs e) 
        {
            
        }


    }
}
