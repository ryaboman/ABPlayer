using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ABPlayer {
    public partial class AddBookmarks : Form {

        public delegate void ChangePossitionEventHandler(int value);
        public event ChangePossitionEventHandler WasRewind;


        public delegate void ChangeStatePlayingEventHandler();
        public event ChangeStatePlayingEventHandler WasChangeStatePlaying;


        public Bookmark bookmark = null;

        public Audiobook currentAudiobook = null;

        private int currentPosition = 0;

        public AddBookmarks(Audiobook currentAudiobook)
        {
            InitializeComponent();

            BtnReverse.WasClickButton += RewindReverce_Click;
            BtnPlay.WasChangeStateButton += BtnPlay_Click;
            BtnForward.WasClickButton += RewindForward_Click;

            this.currentAudiobook = currentAudiobook;
            this.currentAudiobook.WasRewind += UpdateForm;

            if (currentAudiobook.audiobookPlaying) {
                BtnPlay.SetBtnStateInPlay();
            }
            else {
                BtnPlay.SetBtnStateInPause();
            }

            UpdateForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e) {

            //если у закладки есть имя, то продолжаем, иначе выводим сообщение
            if (textBoxNameBookmark.Text != String.Empty)
            {
                CreatingBookmark();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Закладка должна иметь наименование", "Внимание!");
            }
            
        }

        private void CreatingBookmark() {
            
            bookmark = new Bookmark();

            bookmark.title = textBoxNameBookmark.Text;
            bookmark.description = textBoxDescriptionBookmark.Text;
            
            FileInfo nameAudiofile = new FileInfo(currentAudiobook.currentAudiofile.Name);
            bookmark.fileName = nameAudiofile.Name;


            bookmark.filePosition = currentPosition;
            

        }

        private void RewindForward_Click() {

            //перематываем на секунду вперед
            currentPosition += 1;

            WasRewind?.Invoke(currentPosition);

        }

        private void RewindReverce_Click() {
            
            //перематываем на секунду назад
            currentPosition -= 1;

            WasRewind?.Invoke(currentPosition);   
        }

        private void BtnPlay_Click() {            
            
            if (currentAudiobook.audiobookPlaying == false)
            {
                BtnPlay.SetBtnStateInPlay();
            }
            else
            {
                BtnPlay.SetBtnStateInPause();
            }

            WasChangeStatePlaying?.Invoke();
        }

        public void UpdateForm() {
            currentPosition = currentAudiobook.currentPosition;

            SetIndicatorTime();

            SetIndicatorNameAudiofile();
        }

        private void SetIndicatorNameAudiofile() {       

            FileInfo nameAudiofile = new FileInfo( currentAudiobook.currentAudiofile.Name );

            textBxNameAudiofile.Text = nameAudiofile.Name;

            textBxNameAudiofile.TextAlign = HorizontalAlignment.Center;

        }

        private void SetIndicatorTime() {

            TimeSpan time = TimeSpan.FromSeconds(currentPosition);

            indicatorTime.Text = ConvertTimeValueToDoubleDigit(time.Hours) + ':'
                               + ConvertTimeValueToDoubleDigit(time.Minutes) + ':'
                               + ConvertTimeValueToDoubleDigit(time.Seconds);

        }

        private string ConvertTimeValueToDoubleDigit(int time) {

            if (time < 10)
            {
                return "0" + time.ToString();
            }

            return time.ToString();
        }
    }
}
