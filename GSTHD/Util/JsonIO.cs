using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTHD.Util
{
    public static class JsonIO
    {
        public static T Read<T>(string filePath)
            where T : new()
        {
            if (File.Exists(filePath))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
            else
            {
                var obj = new T();
                Write(obj, filePath);
                return obj;
            }
        }

        //public static string ReadAsString<T>(string filePath)
        //    where T : new()
        //{
        //    if (!File.Exists(filePath))
        //    {
        //        var obj = new T();
        //        Write(obj, filePath);
        //    }
        //    return File.ReadAllText(filePath);
        //}
        
        //public static T StringToObject<T>(string text)
        //    where T : new()
        //{
        //    return JsonConvert.DeserializeObject<T>(text);
        //}

        public static void Write<T>(T obj, string filePath)
        {
            var str = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText(filePath, str);
        }
    }
}
