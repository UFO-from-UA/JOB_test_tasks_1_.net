using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class File
    {
        private string fileName;
        private string fileData;

        public string FileName { get => fileName; set => fileName = value; }
        public string FileData { get => fileData; set => fileData = value; }
    }
}
