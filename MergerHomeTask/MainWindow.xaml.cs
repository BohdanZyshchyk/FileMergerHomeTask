using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Path = System.IO.Path;

namespace MergerHomeTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string[] files = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            string path = null;

            Thread[] thread;
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                path = Path.GetDirectoryName(openFile.FileName);
                files = Directory.GetFiles(path, "*.txt");
                lbBox.DataContext = files;
            }

            thread = new Thread[files.Length];
            for (int i = 0; i < thread.Length; i++)
            {
                thread[i] = new Thread(ReadAndWrite);
                thread[i].Start(files[i]);
            }
        }

        static object obj = new object();
        private void ReadAndWrite(object path)
        {
            lock (obj)
            {
                string text = File.ReadAllText(path.ToString());
                File.AppendAllText(@"C:\Users\Zyshchuk\Desktop\Merge.txt", text);
            }
        }

        private void MergeClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
