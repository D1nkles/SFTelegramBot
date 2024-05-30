using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FinalTask.Utilities
{
    internal static class SumCounter
    {
        public static long CountSum(Message message) 
        {
            string text = message.Text;
            string number = "";
            long counter = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != ' ') 
                {
                    number += text[i];
                }

                else 
                {
                    counter += long.Parse(number);
                    number = "";
                }
            }
            counter += long.Parse(number);
            return counter;
        }
    }
}
