using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Util
{
    internal class InfologPrefix
    {
        private string prefix;
        private List<StackFrame> stackFrames;
        
        public InfologPrefix(string _prefix)
        {
            prefix = _prefix;
            StackTrace stackTrace = new StackTrace();
            stackFrames = stackTrace.GetFrames().ToList();
            stackFrames.Reverse();
            stackFrames.RemoveRange(stackFrames.Count - 3, 3);
        }

        public string Prefix
        {
            get { return prefix; }
        }

        public Boolean UsePrefix()
        {
            StackTrace stackTrace = new StackTrace();
            List<StackFrame> stackFramesNow = stackTrace.GetFrames().ToList();

            stackFramesNow.Reverse();
            
            Boolean foundAll = true;

            foreach (var stackFrame in stackFrames)
            {
                Boolean foundNow = false;

                foreach (var frameNow in stackFramesNow)
                {
                    string stackCompare = stackFrame.GetMethod().Module + "#" + stackFrame.GetMethod().Name;
                    string nowCompare = frameNow.GetMethod().Module + "#" + frameNow.GetMethod().Name;

                    if (stackCompare == nowCompare)
                    {
                        foundNow = true;
                        break;
                    }
                }
                
                foundAll = foundAll & foundNow;
            }

            return foundAll;
        }

        public Boolean IsIdentically(InfologPrefix _prefix)
        {
            if (_prefix.prefix != this.prefix)
                return false;

            if (_prefix.stackFrames.Last().GetMethod().Module != this.stackFrames.Last().GetMethod().Module ||
                _prefix.stackFrames.Last().GetMethod().Name != this.stackFrames.Last().GetMethod().Name)
                return false;

            return true;
        }
    }
}
