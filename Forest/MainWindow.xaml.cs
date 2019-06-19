using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Forest {
    using Model;
    public class NestedFile {
        public string Path { get; set; }
        public string Name { get; set; }
        public List<NestedFile> Children { get; set; }
    }
   
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e) {
            string path = "";
#if DEBUG
            path = @"C:\work\QMK\qmk_firmware\keyboards\dz60\keymaps";
#else
            path = this.PathTextBox.Text.ToString();
#endif
            List<NestedFile> files = GenNestedFileList(path);

            this.FileTreeView.ItemsSource = files;
        }

        private List<NestedFile> GenNestedFileList(string rootDirPath) {
            List<NestedFile> ret = new List<NestedFile>();
            DirectoryInfo di = new DirectoryInfo(rootDirPath);
            if (di==null) {
                return ret;
            }

            foreach (FileInfo f in di.GetFiles("*", SearchOption.TopDirectoryOnly)) {
                ret.Add(new NestedFile {
                    Path = f.FullName,
                    Name = f.Name
                });
            }

            foreach (DirectoryInfo d in di.GetDirectories("*", SearchOption.TopDirectoryOnly)){
                FileInfo f = new FileInfo(d.FullName);
                List<NestedFile> children = GenNestedFileList(f.FullName);
                ret.Add(new NestedFile {
                    Path = f.FullName,
                    Name = f.Name,
                    Children = children
                }) ;
            }


            return ret;
        }

    }
}
