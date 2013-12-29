using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    internal class InfologLine
    {
        private List<string> prefixes;
        private string message;
        private InfologMessageType messageType;

        public InfologLine(InfologMessageType _messageType,
                           string _message,
                           List<string> _prefixes)
        {
            messageType = _messageType;
            message = _message;
            prefixes = _prefixes;
        }

        public string prefixText()
        {
            string text = "";
            foreach (string prefix in prefixes)
            {
                text += prefix + "\t";
            }

            return text;
        }

        public string Message { get { return message; } }
    }
}
