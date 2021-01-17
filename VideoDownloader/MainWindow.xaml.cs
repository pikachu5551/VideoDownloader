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
using MSAPI = Microsoft.WindowsAPICodePack;

namespace VideoDownloader
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /* youtube-dlのファイルパスを変数に代入
           @はヒアドキュメントという物の為に付けている(エスケープシーケンスが不要になる) */
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
            argComboBox.Items.Add("mp4");
            argComboBox.Items.Add("mp3");
            argComboBox.Items.Add("option");

        }

        /// <summary>
        /// Downloadボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // ダウンロードするURLにURLを貼ってない場合にクリップボードから文字列を取得
            if (downloadURL.Text == "")
            {
                downloadURL.Text = Clipboard.GetText();
            }

            Download();
        }
        /// <summary>
        /// Folder Selectを押した時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new MSAPI::Dialogs.CommonOpenFileDialog();

            // trueだとフォルダをfalseだとファイル選択になる
            dialog.IsFolderPicker = true;
            // ウィンドウのタイトル
            dialog.Title = "ダウンロード先のフォルダを選択して下さい";
            // 初期ディレクトリ
            dialog.InitialDirectory = Environment.CurrentDirectory;
            // フォルダの選択をしたとき、ダウンロード先のフォルダに表示
            if(dialog.ShowDialog() == MSAPI::Dialogs.CommonFileDialogResult.Ok)
            {
                downloadFolder.Text = dialog.FileName;
            }
            if(!(downloadURL.Text == ""))
            {
                Download();
            }
        }

        private void Download()
        {
            string saveFolder = "";

            if (downloadFolder.Text != "")
            {
                saveFolder = @" -o " + "\"" + downloadFolder.Text + "\\%(title)s.mp4" + "\"";
            }

            string add = "";


            switch (argComboBox.Text)
            {
                case "mp4":
                    add = " -f \"bestvideo[ext=mp4]+bestaudio[ext=m4a]/best[ext=mp4]/best\"";
                    break;
                case "mp3":
                    add = " --extract-audio";
                    break;
                case "option":
                    add = " " + addArg.Text;
                    break;
                default:
                    break;
            }


            string DlCommand = downloadURL.Text + add + saveFolder;

            outputLog.Text = DlCommand;
            // 第一引数のプロセスを起動、第二引数はyoutube-dlのコマンドラインに入力する文字列
            Process.Start(YoutubeDlFilePath, DlCommand);
            downloadURL.Text = "";
        }
    }
}
