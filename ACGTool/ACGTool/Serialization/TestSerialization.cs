using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ACGTool.Serialization
{
    class TestSerialization
    {
        public static void Serialize(string fileName)
        {
            // Create an Object 
            Person person = new Person("HaPN2", 27, "Ha Noi", "0914348307");

            // Create a stream to store a serialized object
            FileStream myStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            // Create a binary formatter object
            BinaryFormatter formatter = new BinaryFormatter();

            // Serialize object to stream
            formatter.Serialize(myStream, person);

            // Close stream
            myStream.Close();
        }

        public static void SerializeWithNonSerialize(string fileName)
        {
            // Create an Object 
            PersonWithNonSerialization person = new PersonWithNonSerialization("HaPN2", 27, "Ha Noi", "0914348307");

            // Create a stream to store a serialized object
            FileStream myStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            // Create a binary formatter object
            BinaryFormatter formatter = new BinaryFormatter();

            // Serialize object to stream
            formatter.Serialize(myStream, person);

            // Close stream
            myStream.Close();
        }

        public static void Deserialize(string fileName)
        {
            // Stream that store the serialized object
            
            FileStream myStream = new FileStream(fileName, FileMode.Open);

            // Create a binary formatter object
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize
            Person person = (Person)formatter.Deserialize(myStream);

            // Close stream
            myStream.Close();

            // Show object
            Console.WriteLine(person.ToString());
            Console.ReadLine();
        }

        public static void DeserializeWithNonSerialize(string fileName)
        {
            // Stream that store the serialized object

            FileStream myStream = new FileStream(fileName, FileMode.Open);

            // Create a binary formatter object
            BinaryFormatter formatter = new BinaryFormatter();

            // Deserialize
            PersonWithNonSerialization person = (PersonWithNonSerialization)formatter.Deserialize(myStream);

            // Close stream
            myStream.Close();

            // Show object
            Console.WriteLine(person.ToString());
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            string fileName = @"E:\BinarySerialization.ex";
            //Serialize(fileName);
            //Deserialize(fileName);

            SerializeWithNonSerialize(fileName);
            DeserializeWithNonSerialize(fileName);
        }
    }
}