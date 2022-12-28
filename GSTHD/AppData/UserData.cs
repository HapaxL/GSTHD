//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace GSTHD.AppData
//{
//    public class UserData
//    {
//        public static string FilePath = $@"{AppData.DirectoryPath}\userdata.json";

//        public static UserData Read()
//        {
//            if (File.Exists(FilePath))
//                return JsonConvert.DeserializeObject<UserData>(File.ReadAllText(FilePath));
//            else
//            {
//                var userdata = new UserData();
//                userdata.Write();
//                return userdata;
//            }
//        }

//        public void Write()
//        {
//            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
//            File.WriteAllText(FilePath, str);
//        }
//    }
//}
