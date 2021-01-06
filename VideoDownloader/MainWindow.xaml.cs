using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace VideoDownloader
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /* youtube-dlのファイルパスを変数に代入
           @はヒアドキュメントという物の為に付けている
           エスケープシーケンスが不要になる */
        public string YoutubeDlFilePath = Environment.CurrentDirectory + @"\youtube-dl.exe";
        public MainWindow()
        {
            InitializeComponent();

            // ディレクトリにyoutube-dlが存在しないと知らせアプリを終了
            if (!File.Exists(YoutubeDlFilePath))
            {
                MessageBox.Show("次のフォルダにyoutube-dl.exeが存在しません\n" + Environment.CurrentDirectory);
                Environment.Exit(1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            outputLog.Text = YoutubeDlFilePath;
            /*if (!(downloadURL.Text == null))
            {
                // 第一引数のプロセスを起動
                Process.Start(, downloadURL.Text);
            }*/
        }
    }
}
