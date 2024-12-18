using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleUI
{
    public enum CtrlType
    {
        CTRL_C_EVENT = 0,
        CTRL_BREAK_EVENT = 1,
        CTRL_CLOSE_EVENT = 2,
        CTRL_LOGOFF_EVENT = 5,
        CTRL_SHUTDOWN_EVENT = 6,
    }

    public delegate bool SetConsoleCtrlEventHandler(CtrlType signal);
    public class CtrlEventHandling
    {
        [DllImport("Kernel32")]

        public static extern bool SetConsoleCtrlHandler(SetConsoleCtrlEventHandler handler, bool add);
    }
}
