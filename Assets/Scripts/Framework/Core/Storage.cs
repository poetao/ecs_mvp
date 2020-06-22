using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace MVP.Framework.Core
{
    public class Storage
    {
        public static Storage instance { get; private set; }

        public static void Setup()
        {
            instance = new Storage();
        }

        public void SaveBySerialize(string path, object graph)
        {
            var fullPath = Path.instance.Resolve(path, Resource.TYPE.Storage);
            var directory = System.IO.Path.GetDirectoryName(fullPath); 
			if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(fullPath)) File.Delete(fullPath);

            FileStream file = File.Create(fullPath);
            bf.Serialize(file, graph);
            file.Close();       
        }
    }
}
