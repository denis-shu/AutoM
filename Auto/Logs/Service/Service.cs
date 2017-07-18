using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Logs.Service
{
    public class Service
    {
        public static Exception Deserialize(byte [] byteArr)
        {
            if (byteArr == null)
                return null;

            IFormatter formatter = new BinaryFormatter();

            using(MemoryStream mStream = new MemoryStream(byteArr))
            {
                var exp = formatter.Deserialize(mStream) as Exception;
                return exp;
            }
               
        }
    }
}
