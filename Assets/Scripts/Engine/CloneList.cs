using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BOYAREngine.Engine
{
    [System.Serializable]
    public class CloneList : MonoBehaviour
    {
        public static List<T> DeepCopy<T>(List<T> oldList)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, oldList);
            stream.Position = 0;
            return (List<T>)formatter.Deserialize(stream);
        }
    }
}

