using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    public class WindowsService
    {
        List<IShareString> subscribers = new List<IShareString>();

        public void SendMessageToSubscribers(string message)
        {
            foreach(var sub in subscribers)
            {
                sub.OnMessageSend(message);
            }
        }

        public void AddSubscriber(IShareString wind)
        {
            if (wind != null && subscribers.Contains(wind) == false)
            {
                subscribers.Add(wind);
            }
        }
    }
}
