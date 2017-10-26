using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Svetomech.Collections;
using Svetomech.Todo;

namespace todo
{
    class Program
    {
        private const StringComparison comparison = StringComparison.OrdinalIgnoreCase;
        private static readonly string path = $"{nameof(todo)}-{nameof(items)}.bin";
        private static TodoList items = new TodoList();

        public static void Main()
        {
            Deserialize();

            while (true)
            {
                items.ForEach(item => Console.WriteLine(item));

                DisplayGreetingMessage();
                string input = Console.ReadLine()?.TrimStart();

                if (String.IsNullOrWhiteSpace(input) ||
                    (input.Length < 3 && !input.Equals("--", comparison)))
                {
                    DisplayErrorMessage();
                    continue;
                }

                if (input.Equals("--", comparison))
                {
                    items.Clear();
                }
                else if (input[0] == '-')       // .StartsWith("-", comparison) is slower
                {
                    var item = new TodoItem { PlainText = input.Substring(2) };

                    items.Remove(item);
                }
                else if (input[0] == '+')       // .StartsWith("+", comparison) is slower
                {
                    var item = new TodoItem { PlainText = input.Substring(2) };

                    items.Add(item);
                }
                else
                {
                    DisplayErrorMessage();
                    continue;
                }

                Serialize();
            }
        }

        private static void Deserialize()
        {
            if (!File.Exists(path)) return;

            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(path,
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (stream.Length == 0) return;

                items = (TodoList)formatter.Deserialize(stream);
            }
        }

        private static void Serialize()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(path,
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, items);
            }
        }

        private static void DisplayGreetingMessage() =>
            Console.Write("Enter command (+ item, - item, or -- to clear)): ");

        private static void DisplayErrorMessage() =>
            Console.WriteLine("Please, enter something meaningful.");
    }
}