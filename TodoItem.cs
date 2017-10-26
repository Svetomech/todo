using System;

namespace Svetomech.Todo
{
    [Serializable]
    public struct TodoItem
    {
        public string PlainText { get; set; }

        public override string ToString() => PlainText;
    }
}