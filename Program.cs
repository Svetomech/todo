using System;
using Svetomech.Utilities.Extensions;
using static Svetomech.Utilities.BinarySerializationUtils;

namespace Svetomech.Todo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var todoItems = new TodoList();
            string todoItemsPath = args.Length > 0 ? args[0] : 
                $"{nameof(todoItems)}.bin";

            try { todoItems = Deserialize<TodoList>(todoItemsPath); }
            catch { /* File is either empty or does not exist*/ }

            string input;
            while (true)
            {
                todoItems.ForEach(item => Console.WriteLine(item));

                DisplayGreetingMessage();
                input = Console.ReadLine()?.TrimStart();
                if (!IsInputValid()) continue;

                if (input.EqualsOrdinal("--"))
                {
                    todoItems.Clear();
                }
                else if (input[0] == '-')
                {
                    var item = new TodoItem { PlainText = input.Substring(2) };

                    todoItems.Remove(item);
                }
                else if (input[0] == '+')
                {
                    var item = new TodoItem { PlainText = input.Substring(2) };

                    todoItems.Add(item);
                }

                Serialize<TodoList>(todoItems, todoItemsPath);
            }
            
            void DisplayGreetingMessage() =>
                Console.Write("Enter command (+ item, - item, or -- to clear)): ");
            
            bool IsInputValid() => String.IsNullOrWhiteSpace(input) || 
                (input.Length < 3 && !input.EqualsOrdinal("--"));
        }
    }
}