using System;
using Svetomech.Collections.Generic;
using Svetomech.Todo;

namespace Svetomech.Collections
{
    [Serializable]
    public class TodoList : IList<TodoItem>
    {
        private List<TodoItem> items = new List<TodoItem>();
        
        public void Add(TodoItem item) => items.Add(item);

        public void Remove(TodoItem item) => items.Remove(item);

        public void Clear() => items.Clear();

        public void ForEach(Action<TodoItem> action) => items.ForEach(action);
    }
}