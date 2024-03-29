using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialization_Lab
{
   
    [Serializable]
    public class Event
    {
        public int EventNumber { get; set; }
        public string Location { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "event.txt";
            Event myEvent = new Event
            {
                EventNumber = 1,
                Location = "Calgary"
            };

            SerializeToFile(filePath, myEvent);
            Event deserializedEvent = DeserializeFromFile<Event>(filePath);

            Console.WriteLine( $"{deserializedEvent.EventNumber}");
            Console.WriteLine( $"{deserializedEvent.Location}");
            Console.WriteLine($"Tech Competition ");

            WriteAndSeek(filePath,"Hackathon");
            ReadAndSeek(filePath);

            Console.ReadLine();
        }

        static void SerializeToFile<T>(string filePath, T obj)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
        }

        static T DeserializeFromFile<T>(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fs);
            }
        }

        static void WriteAndSeek(string filePath, string content)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Seek(5, SeekOrigin.Begin);
                byte[] data = Encoding.UTF8.GetBytes(content);
                fs.Write(data, 0, data.Length);
            }
        }

        static void ReadAndSeek(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(5, SeekOrigin.Begin);

                byte[] buffer = new byte[10];
                int bytesRead = fs.Read(buffer, 0, buffer.Length);

                Console.WriteLine("In Word: " + Encoding.UTF8.GetString(buffer, 0, bytesRead));
                Console.WriteLine($"The First Character is: \"{(char)buffer[0]}\"");
                Console.WriteLine($"The Middle Character is: \"{(char)buffer[(int)(buffer.Length / 2.5)]}\"");
                Console.WriteLine($"The Last Character is: \"{(char)buffer[buffer.Length -2]}\"");
            }
        }
    }
}
   


