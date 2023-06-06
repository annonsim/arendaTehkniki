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
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;

namespace ArendaDiplom
{
    /// <summary>
    /// Логика взаимодействия для adminPage.xaml
    /// </summary>
    public partial class adminPage : Window
    {
        public adminPage()
        {
            InitializeComponent();
            manager.MainFrame = MainFrame;
            manager.MainFrame.Navigate(new wellcome());
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void audio_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new audioAdmin());
        }

        private void video_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new videoAdmin());
        }

        private void light_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new lightAdmin());
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new usersPage());
        }

        private void otchets_Click(object sender, RoutedEventArgs e)
        {
            var audio = arendaDipEntities.GetContext().audioDevice.ToList();
            var video = arendaDipEntities.GetContext().videosDevice.ToList();
            var light = arendaDipEntities.GetContext().lightDevice.ToList();

            var application = new Word.Application();

            Word.Document document = application.Documents.Add();

            Word.Paragraph audioPa = document.Paragraphs.Add();
            Word.Range audioRan = audioPa.Range;
            audioRan.Text = "Аудио устройства";
            audioPa.set_Style("Выделенная цитата");
            audioRan.InsertParagraphAfter();

            foreach (var audi in audio)
            {
                Word.Paragraph audioPar = document.Paragraphs.Add();
                Word.Range audioRange = audioPar.Range;
                audioRange.Text = audi.name + audi.model;
                audioPar.set_Style("Заголовок 1");
                audioRange.InsertParagraphAfter();


                Word.Paragraph tableParag = document.Paragraphs.Add();
                Word.Range tableRange = tableParag.Range;
                Word.Table payMents = document.Tables.Add(tableRange, 1, 2); //!!
                payMents.Borders.InsideLineStyle = payMents.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                payMents.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRAnge;

                cellRAnge = payMents.Cell(1, 1).Range;
                cellRAnge.Text = ("Прибыль: ");

                for (int i = 0; i < audio.Count(); i++)
                {
                    var currentAudio = audio[i];
                    decimal prices = audi.chetOrder.Value;
                    var sll = currentAudio.price * prices;
                    cellRAnge = payMents.Cell(2, 2).Range;
                    cellRAnge.Text = sll.ToString();

                }

            }


            Word.Paragraph videoPa = document.Paragraphs.Add();
            Word.Range videoRan = videoPa.Range;
            videoRan.Text = "Видео устройства";
            videoPa.set_Style("Выделенная цитата");
            videoRan.InsertParagraphAfter();

            foreach (var vid in video)
            {
                Word.Paragraph videoPar = document.Paragraphs.Add();
                Word.Range videoRange = videoPar.Range;
                videoRange.Text = vid.name + vid.model;
                videoPar.set_Style("Заголовок 1");
                videoRange.InsertParagraphAfter();


                Word.Paragraph tableParag = document.Paragraphs.Add();
                Word.Range tableRange = tableParag.Range;
                Word.Table payMents = document.Tables.Add(tableRange, 1, 2); //!!
                payMents.Borders.InsideLineStyle = payMents.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                payMents.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRAnge;

                cellRAnge = payMents.Cell(1, 1).Range;
                cellRAnge.Text = ("Прибыль: ");

                for (int i = 0; i < video.Count(); i++)
                {
                    var currentVideo = video[i];
                    decimal prices = vid.chetOrder.Value;
                    var sll = currentVideo.price * prices;
                    cellRAnge = payMents.Cell(2, 2).Range;
                    cellRAnge.Text = sll.ToString();

                }

            }


            Word.Paragraph lighPa = document.Paragraphs.Add();
            Word.Range lighRan = lighPa.Range;
            lighRan.Text = "Световые устройства";
            lighPa.set_Style("Выделенная цитата");
            lighRan.InsertParagraphAfter();

            foreach (var lig in light)
            {
                Word.Paragraph lighPar = document.Paragraphs.Add();
                Word.Range lighRange = lighPar.Range;
                lighRange.Text = lig.name + lig.model;
                lighPar.set_Style("Заголовок 1");
                lighRange.InsertParagraphAfter();


                Word.Paragraph tableParag = document.Paragraphs.Add();
                Word.Range tableRange = tableParag.Range;
                Word.Table payMents = document.Tables.Add(tableRange, 1, 2); //!!
                payMents.Borders.InsideLineStyle = payMents.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                payMents.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRAnge;

                cellRAnge = payMents.Cell(1, 1).Range;
                cellRAnge.Text = ("Прибыль: ");

                for (int i = 0; i < light.Count(); i++)
                {
                    var currentLigh = light[i];
                    decimal prices = lig.chetOrder.Value;
                    var sll = currentLigh.price * prices;
                    cellRAnge = payMents.Cell(2, 2).Range;
                    cellRAnge.Text = sll.ToString();

                }

            }

            document.SaveAs2(@"C:\Users\home\Desktop\otchetd.docx");
            MessageBox.Show("Отчет добавлен на рабочий стол!");
        }
    }
}      