using System;

namespace Common.ConsoleUI
{
    public struct MenuItem
    {
        public string CommandName;
        public Action Operation;
        public Func<bool> Displayed;
        public bool Stopping;
        public object Tag;

        public MenuItem(string commandName, Action operation, Func<bool> displayed = null, bool stopping = false, object tag = null)
        {
            this.CommandName = commandName;
            this.Operation = operation;
            this.Displayed = displayed;
            this.Stopping = stopping;
            this.Tag = tag;
        }
    }
}
