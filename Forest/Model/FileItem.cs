using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest.Model {
    class FileItem {
        public string Name { get; set; }
        public List<FileItem> Children { get; set; }
    }
}
