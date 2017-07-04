using Microsoft.Azure.Mobile.Analytics;
using RSSReader.Logs.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RSSReader.Logs
{
    public class MobileCenter
    {
        // max item in a Dictionary you can send in one TrackEvent to MobileCenter
        const int groupSize = 5;

        /// <summary>
        /// Sends the event.
        /// </summary>
        /// <param name="Action">Action.</param>
        /// <param name="Message">Message.</param>
        public void SendEvent(string Action, string Message)
        {
            Analytics.TrackEvent(Action, new Dictionary<string, string>() { { "Message", Message } });
        }

        /// <summary>
        /// Sends the message to azure.
        /// </summary>
        /// <param name="Action">The action.</param>
        /// <param name="Message">The message.</param>
        public void SendEvent(string Action, Dictionary<string, string> Message)
        {
            Analytics.TrackEvent(Action, new Dictionary<string, string>(Message));
        }

        /// <summary>
        /// Track an event to MobileCenter.
        /// </summary>
        /// <param name="EventName">Event name.</param>
        /// <param name="PageName">Page name.</param>
        /// <param name="BaseClass">Base class.</param>
        /// <param name="MoreInfo">More info.</param>
        /// <param name="DictionaryInfo">Dictionary info.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="logLevel">Log level.</param>
        public void SendEvent(string EventName, string PageName, string BaseClass,
                              string MoreInfo = "", Dictionary<string, string> DictionaryInfo = null,
                              Exception exception = null, LogLevel logLevel = LogLevel.Info)
        {
            try
            {
                Dictionary<string, string> info = new Dictionary<string, string>() {
                    { "LogLevel", logLevel.ToString() }
                };

                if (!string.IsNullOrEmpty(BaseClass))
                    info.Add("BaseClass", BaseClass);

                if (!string.IsNullOrEmpty(PageName))
                    info.Add("PageName", PageName);

                if (!string.IsNullOrEmpty(MoreInfo))
                {
                    info.Add("MoreInfo", MoreInfo.Trim());
                }

                if (DictionaryInfo != null)
                {
                    info.Union(DictionaryInfo);
                }

                if (exception != null)
                {
                    info.Add("Message", exception.Message);
                    info.Add("Source", exception.Source);
                    info.Add("StackTrace", exception.StackTrace);
                    info.Add("InnerException", exception.InnerException.ToString());
                }

                int counter = 0;
                IEnumerable<Dictionary<string, string>> result = info
                    .GroupBy(x => counter++ / groupSize)
                    .Select(g => g.ToDictionary(h => h.Key, h => h.Value));

                foreach (Dictionary<string, string> rsl in result)
                    SendEvent(EventName, rsl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Mobile Center Analytics Error: " + ex.Message);
                Debug.WriteLine("                   StackTrace: " + ex.StackTrace);
                Debug.WriteLine("               InnerException: " + ex.InnerException);
                Debug.WriteLine("                       Source: " + ex.Source.ToString());
            }
        }
    }
}