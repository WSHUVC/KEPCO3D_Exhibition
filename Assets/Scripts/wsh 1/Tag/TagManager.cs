using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WSH.Util
{
    public class TagManager : MonoBehaviour
    {
        public string tagGroupTextFilePath;

        public void SetGroupFile()
        {
            using (StreamReader fs = new StreamReader(tagGroupTextFilePath))
            {
                var data = fs.ReadToEnd();
                var dataSplit = data.Split('\n');
                for(int i = 0; i < dataSplit.Length; ++i) 
                { 
                }
            }
        }

        public void SaveGroupSetting()
        {
        }
    }
}
