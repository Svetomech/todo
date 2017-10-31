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

            while (true)
            {
                todoItems.ForEach(item => Console.WriteLine(item));

                DisplayGreetingMessage();
                string input = Console.ReadLine()?.TrimStart();

                if (String.IsNullOrWhiteSpace(input) ||
                    (input.Length < 3 && !input.EqualsOrdinal("--")))
                {
                    DisplayErrorMessage();
                    continue;
                }

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
                else
                {
                    DisplayErrorMessage();
                    continue;
                }

                Serialize<TodoList>(todoItems, todoItemsPath);
            }

            void DisplayGreetingMessage() =>
                Console.Write("Enter command (+ item, - item, or -- to clear)): ");

            void DisplayErrorMessage() =>
                Console.WriteLine("Please, enter something meaningful.");
        }
    }
}