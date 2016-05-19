using System;
using System.Text;

namespace ACGTool.Serialization
{
    /// <summary>
    /// Class with serialization
    /// </summary>
    [Serializable]
    public class Person
    {
        private string name = null;

        private int age = 0;
        private string address = null;
        private string mobile = null;

        public Person() { }

        public Person(string name, int age, string address, string mobile)
        {
            this.name = name;
            this.age = age;
            this.address = address;
            this.mobile = mobile;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name=" + name);
            sb.AppendLine("Age=" + age);
            sb.AppendLine("Address=" + address);
            sb.AppendLine("Mobie=" + mobile);

            return sb.ToString();
        }
    }
}
