using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FinalTask.Utilities
{
    internal static class CharCounter
    {
        public static int CountChars(Message message) 
        {
            return message.Text.Length;
        }
    }
}
