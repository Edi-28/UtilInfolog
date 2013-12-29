using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Infolog
    {
        private InfologMessageType maxMessageType;
        public InfologMessageType MaxMessageType { get { return maxMessageType; } }

        private List<InfologLine> infologLines;
        private List<InfologPrefix> prefixes;

        private static Infolog instance;

        private Infolog()
        {
            infologLines = new List<InfologLine>();
            prefixes = new List<InfologPrefix>();
        }

        public static Infolog GetInstance()
        {
            if (instance == null)
                instance = new Infolog();

            return instance;
        }

        public static void Error(string _message)
        {
            GetInstance().addMessage(InfologMessageType.Error, _message);
        }

        public static void Warning(string _message)
        {
            GetInstance().addMessage(InfologMessageType.Warning, _message);
        }

        public static void Info(string _message)
        {
            GetInstance().addMessage(InfologMessageType.Info, _message);
        }

        public static void SetPrefix(string _prefix)
        {
            GetInstance().addPrefix(_prefix);
        }

        private void addPrefix(string _prefix)
        {
            InfologPrefix newPrefix = new InfologPrefix(_prefix);
            removeOutdatedPrefixes(newPrefix);

            prefixes.Add(newPrefix);
        }

        private void addMessage(InfologMessageType _messageType,
                                string _message)
        {
            setMaxMessageType(_messageType);

            List<string> localPrefixes = new List<string>();

            removeOutdatedPrefixes();

            foreach (var prefix in prefixes)
            {
                localPrefixes.Add(prefix.Prefix);
            }

            infologLines.Add(new InfologLine(_messageType, _message, localPrefixes));

        }

        private void removeOutdatedPrefixes(InfologPrefix _prefixToAdd = null)
        {
            List<InfologPrefix> remainingPrefixes = new List<InfologPrefix>();

            foreach (var prefix in prefixes)
            {
                if (prefix.UsePrefix())
                {
                    if (_prefixToAdd == null || ! _prefixToAdd.IsIdentically(prefix))
                    remainingPrefixes.Add(prefix);
                }
            }

            prefixes = remainingPrefixes;
        }

        private void setMaxMessageType(InfologMessageType _messageType)
        {
            if (maxMessageType < _messageType)
                maxMessageType = _messageType;
        }

        public void Clear()
        {
            maxMessageType = InfologMessageType.None;

            infologLines.Clear();
            prefixes.Clear();
        }

        public string GetInfologText()
        {
            string text = "";

            foreach (InfologLine infologLine in infologLines)
            {
                text += infologLine.prefixText() + infologLine.Message + "\n";
            }

            return text;
        }
    }
}
