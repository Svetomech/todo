using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Svetomech.Utilities
{
    public static class BinarySerializationUtils
    {
        public static T Deserialize<T>(string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void Serialize<T>(T value, string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath,
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, value);
            }
        }
    }
}