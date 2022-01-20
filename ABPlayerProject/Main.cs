using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Threading;
using System.IO;
using TagLib;
using System.Reflection;
using CommonCL;
using TagLib.Mpeg;
//using System.Threading;

namespace ABPlayer {
    public partial class Main : Form
    {
        /// <summary>
        /// значение для перемотки (в секундах)
        /// </summary>
        private const int valueRewind = 10;

        private MediaPlayer mediaPlayer;

        private Audiobook currentAudiobook = null;

        Dictionary<int, Bookmark> dictionaryBookmark;

        private readonly double[] arraySpeed = { 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0 };


        public delegate void AudiofileEndedEventHandler();
        public event AudiofileEndedEventHandler WasAudiofileEnded;


        public Main()
        {
            InitializeComponent();

            InitializeCmbBxSelectorSpeedPlayback(arraySpeed[2].ToString());
            cmbBxSelectorSpeedPlayback.SelectedIndexChanged += cmbBxSelectorSpeedPlayback_SelectedIndexChanged;

            mediaPlayer = new MediaPlayer();



            sliderAudiofile.WasChangeThumbPossition += sliderAudiofile_WasDamaged;

            sliderAudiobook.WasChangeThumbPossition += sliderAudiobook_WasDamaged;

            BtnReverse.WasClickButton += BtnRewindReverce_Click;
            BtnPlay.WasChangeStateButton += BtnPlay_Click;
            BtnStop.WasClickButton += BtnStop_Click;
            BtnForward.WasClickButton += BtnRewindForward_Click;


            //mediaPlayer.MediaEnded += AudiofileEnded;
            WasAudiofileEnded += ChangeAudiofile;

            ReadCurrentAudiobook();

        }

        private void sliderAudiofile_WasDamaged(TimeSpan currentValue){
            mediaPlayer.Position = currentValue;
        }

        private void sliderAudiobook_WasDamaged(TimeSpan currentValue) {
            //mediaPlayer.Position = currentValue;

            mainTimer.Stop();

            List<AudioFile> audioFiles = currentAudiobook.audiofiles;


            int duration = 0;

            foreach (var audioFile in audioFiles)
            {

                int tempDuration = (int)audioFile.Properties.Duration.TotalSeconds;

                duration += tempDuration;

                if(duration >= currentValue.TotalSeconds)
                {                    

                    currentAudiobook.currentAudiofile = audioFile;

                    currentAudiobook.currentPosition = (int)( currentValue.TotalSeconds - (duration - tempDuration) );

                    InitilizeAudiofile();

                    BtnPlay_Click();

                    break;
                }

            }


        }

        private bool InitilizeMediaPlayer(){

            if(currentAudiobook.nameAudiobook.Length > 33)
            {
                nameAudiobook.Text = currentAudiobook.nameAudiobook.Substring(0, 33) + "...";
            }
            else
            {
                nameAudiobook.Text = currentAudiobook.nameAudiobook;
            }


            InitilizeSelecterBookmarks();

            SetTimeToSliderAudiobook();

            return true;

        }

        /// <summary>
        /// Функция инициализация закладок в выпадающее меню 
        /// </summary>
        private void InitilizeSelecterBookmarks() {
                       
            dictionaryBookmark = new Dictionary<int, Bookmark>();

            Bookmarks bookmarks = currentAudiobook.bookmarks;

            //Очищаем список перед записью
            cmbBxBookmark.Items.Clear();

            foreach (var bookmark in bookmarks.bookmarks)
            {
                int index = cmbBxBookmark.Items.Add(bookmark.title);

                dictionaryBookmark.Add(index, bookmark);
            }
        }

        private void SetTimeToSliderAudiobook() {

            sliderAudiobook.maxValue = currentAudiobook.durationAudiobook;
            

            List<AudioFile> audioFiles = currentAudiobook.audiofiles;


            int duration = 0;

            foreach(var audioFile in audioFiles)
            {
                if(audioFile.Name == currentAudiobook.currentAudiofile.Name)
                {
                    duration += (int)mediaPlayer.Position.TotalSeconds;
                    break;
                }

                duration += (int)audioFile.Properties.Duration.TotalSeconds;


            }

            sliderAudiobook.SetCurrentValue(duration);


            TimeSpan currentTime = TimeSpan.FromSeconds(duration);
            TimeSpan time = TimeSpan.FromSeconds(currentAudiobook.durationAudiobook);

            timeValueCurrentAudiobook.Text = ConvertTimeValueToDoubleDigit(time.Hours) + ':'
                                            + ConvertTimeValueToDoubleDigit(time.Minutes) + ':'
                                            + ConvertTimeValueToDoubleDigit(time.Seconds) + '/'
                                            + ConvertTimeValueToDoubleDigit(currentTime.Hours) + ':'
                                            + ConvertTimeValueToDoubleDigit(currentTime.Minutes) + ':'
                                            + ConvertTimeValueToDoubleDigit(currentTime.Seconds);

        }

        private void InitilizeAudiofile() {

            currentAudiobook.audiobookPlaying = false;
            mainTimer.Stop();

            string pathToAudiobook = currentAudiobook.pathToDirectoryAudiobook.FullName;

            string currentAudiofile = currentAudiobook.currentAudiofile.Name;

            string pathMediaFile = Path.Combine(pathToAudiobook, currentAudiofile);

            //задаем максимальное время индикатора
            sliderAudiofile.maxValue = currentAudiobook.GetDurationCurrentAudiofile();

            mediaPlayer.Open(new Uri(pathMediaFile, UriKind.Relative));

            //Без задержки слетает позиция времени при переключении на предыдущий аудиофайл
            Thread.Sleep(50);

            mediaPlayer.Position = TimeSpan.FromSeconds(currentAudiobook.currentPosition);

            mainTimer_Tick(null, null);            

        }

        //Установка нового положения слайдера по таймеру
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            int currentValue = (int)mediaPlayer.Position.TotalSeconds;

            if ( currentValue >= currentAudiobook.GetDurationCurrentAudiofile() ){
                
                AudiofileEnded();

                return;
            }            
            

            currentAudiobook.currentPosition = currentValue;

            SetValueTimeIndicator();

            SetTimeToSliderAudiobook();

            currentAudiobook.WriteStateAudiobookIntoFile();

            


        }

        /// <summary>
        /// Функция устанавливает значение на индикаторе времени
        /// </summary>
        private void SetValueTimeIndicator() {

            TimeSpan time = TimeSpan.FromSeconds(currentAudiobook.GetDurationCurrentAudiofile());            

            TimeSpan currentTime = mediaPlayer.Position;

            sliderAudiofile.SetCurrentValue(currentTime.TotalSeconds);

            timeValueCurrentAudiofile.Text = ConvertTimeValueToDoubleDigit(time.Hours) + ':' 
                                            + ConvertTimeValueToDoubleDigit(time.Minutes) + ':' 
                                            + ConvertTimeValueToDoubleDigit(time.Seconds) + '/' 
                                            + ConvertTimeValueToDoubleDigit(currentTime.Hours) + ':' 
                                            + ConvertTimeValueToDoubleDigit(currentTime.Minutes) + ':' 
                                            + ConvertTimeValueToDoubleDigit(currentTime.Seconds);

        }

        private string ConvertTimeValueToDoubleDigit(int time) {

            if(time < 10){
                return "0" + time.ToString();
            }

            return time.ToString();
        }

        private void BtnPlay_Click()
        {

            if (mediaPlayer.Source == null)
            {
                MessageBox.Show("URI не задан!");

                return;
            }

            if (currentAudiobook.audiobookPlaying){
                mainTimer.Stop();
                mediaPlayer.Pause();
                BtnPlay.SetBtnStateInPause();
                currentAudiobook.audiobookPlaying = false;
            }
            else{
                mediaPlayer.Play();
                BtnPlay.SetBtnStateInPlay();
                mainTimer.Start();
                currentAudiobook.audiobookPlaying = true;
            }

            //timeline = mediaPlayer.Clock.Timeline;
        }

        private void BtnRewindReverce_Click()
        {
            int valueTime = (int)mediaPlayer.Position.TotalSeconds - valueRewind;

            MakeRewind(valueTime);
        }


        private void BtnRewindForward_Click()
        {
            int valueTime = (int)mediaPlayer.Position.TotalSeconds + valueRewind;

            MakeRewind(valueTime);
        }

        /// <summary>
        /// Функция перематывает плеер на заданное время воспроизведения
        /// </summary>
        /// <param name="valueTime">Время воспроизведения, на которое нужно перемотать плеер</param>
        private void MakeRewind(int valueTime) {
            
            int durationCurrentAudiofile = currentAudiobook.GetDurationCurrentAudiofile();

            if ( (valueTime >= 0) && (valueTime <= durationCurrentAudiofile) ){
                
                mediaPlayer.Position = TimeSpan.FromSeconds(valueTime);
                currentAudiobook.currentPosition = valueTime;

                SetValueTimeIndicator();
                SetTimeToSliderAudiobook();

            }
            else if(valueTime < 0){
                int deltaRewind = Math.Abs(valueTime);
                PreviousAudiofile( deltaRewind );
            }
            else if(valueTime > durationCurrentAudiofile){
                int deltaRewind = valueTime - durationCurrentAudiofile;
                NextAudiofile( deltaRewind );
            }

            //Сообщаем всем, что была выполнена перемотка
            currentAudiobook.WasRewindAudifile();
        }

        private void BtnStop_Click()
        {
            //MessageBox.Show(timeline.Source.ToString());
            mediaPlayer.Stop();
            BtnPlay.SetBtnStateInPause();

            currentAudiobook.audiobookPlaying = false;
            mainTimer.Stop();            

            mainTimer_Tick(null, null);
        }


        private void addBookmarks_Click(object sender, EventArgs e)
        {
            AddBookmarks addBookmarks = new AddBookmarks(currentAudiobook);            

            //Подписываемся на события
            addBookmarks.WasRewind += MakeRewind;
            addBookmarks.WasChangeStatePlaying += BtnPlay_Click;

            //Отписываемся от событий автоматического переключения аудиофайла
            WasAudiofileEnded -= ChangeAudiofile;

            addBookmarks.ShowDialog();

            //Отписываемся от событий
            addBookmarks.WasRewind -= MakeRewind;
            addBookmarks.WasChangeStatePlaying -= BtnPlay_Click;

            //Подписываемся на события автоматического переключения аудиофайла
            WasAudiofileEnded += ChangeAudiofile;

            if (addBookmarks.DialogResult == DialogResult.OK)
            {
                Bookmark bookmark = addBookmarks.bookmark;

                currentAudiobook.bookmarks.AddBookmark(bookmark);

                InitilizeSelecterBookmarks();
            }
        }

        private void RewindFromFormAddBookmarks() {

        }

        private void btnLibrary_Click(object sender, EventArgs e) {
            
            Library library = new Library();
            library.Owner = this;
            library.ShowDialog();
            
            if(library.DialogResult == DialogResult.OK)
            {                

                currentAudiobook = library.selectedAudiobook;

                //Здесь нужно инициализировать плеер с новой книгой
                InitilizeMediaPlayer();

                InitilizeAudiofile();

                WriteStatePlayerIntoFile();
            }

            


        }

        //Переключение на следующий аудиофайл аудиокниги
        private void ChangeAudiofile() {

            
            NextAudiofile(0);

        }

        private void AudiofileEnded() {
            mainTimer.Stop();
            BtnPlay_Click();

            WasAudiofileEnded?.Invoke();
        }

        /// <summary>
        /// Функция переключения плеера на предыдущий аудиофайл
        /// </summary>
        /// <param name="deltaRewind">Дельта на которую нужно перемотать время воспроизведения аудиофайла с его конца</param>
        private void PreviousAudiofile(int deltaRewind) {

            Predicate<AudioFile> predicate = FindPoints;

            int index = currentAudiobook.audiofiles.FindIndex(predicate);

            index--;

            if (index >= 0)
            {
                mainTimer.Stop();
                mediaPlayer.Stop();

                currentAudiobook.currentAudiofile = currentAudiobook.audiofiles[index];

                //максимальная позиция минус deltaRewind
                currentAudiobook.currentPosition = currentAudiobook.GetDurationCurrentAudiofile() - deltaRewind;

                InitilizeAudiofile();

                BtnPlay_Click();
            }            

        }


        /// <summary>
        /// Функция переключения плеера на следующий аудиофайл
        /// </summary>
        /// <param name="deltaRewind">Дельта на которую нужно перемотать время воспроизведения аудиофайла с его начала</param>
        private void NextAudiofile(int deltaRewind) {

            Predicate<AudioFile> predicate = FindPoints;

            int index = currentAudiobook.audiofiles.FindIndex(predicate);

            index++;

            if (currentAudiobook.audiofiles.Count > index)
            {
                mainTimer.Stop();
                mediaPlayer.Stop();

                currentAudiobook.currentAudiofile = currentAudiobook.audiofiles[index];


                currentAudiobook.currentPosition = deltaRewind;

                InitilizeAudiofile();

                //Воcпроизвести аудиофайл
                BtnPlay_Click();
            }
            
        }

        private bool FindPoints(AudioFile file) {
            return ( file.Name == currentAudiobook.currentAudiofile.Name );
        }

        //Выбор закладок
        private void cmbBxBookmark_SelectedIndexChanged(object sender, EventArgs e) {

            int slctIndx = cmbBxBookmark.SelectedIndex;

            Bookmark bookmark = dictionaryBookmark[slctIndx];

            textBoxForBookmark.Text = bookmark.description;

            currentAudiobook.currentAudiofile = currentAudiobook.audiofiles.Find( (x) => Path.GetFileName(x.Name) == bookmark.fileName );

            currentAudiobook.currentPosition = bookmark.filePosition;

            

            InitilizeAudiofile();

            BtnPlay_Click();
        }

        //Запись состояния плеера в файл настроек
        private void WriteStatePlayerIntoFile() {

            string path = Directory.GetCurrentDirectory();

            path = Path.Combine(path, "setting.ini");


            Txt txtFile = new Txt();
            bool status = txtFile.SetPathToFile(path);

            if (status)
            {
                string pathToDirectoryLibrary = txtFile.Select("<pathToLibrary>", "</pathToLibrary>");

                int length = pathToDirectoryLibrary.Length + 1;

                string relativePathToCurrentAudiobook = currentAudiobook.pathToDirectoryAudiobook.FullName.Remove(0, length);

                txtFile.RewriteBlock("<currentAudiobook>", "</currentAudiobook>", relativePathToCurrentAudiobook);

                int indexSpeed = cmbBxSelectorSpeedPlayback.SelectedIndex;
                txtFile.RewriteBlock( "<playbackSpeed>", "</playbackSpeed>", arraySpeed[indexSpeed].ToString() );


            }


        }

        //считывает текущую аудиокнигу из файла настроек
        private void ReadCurrentAudiobook() {

            string path = Directory.GetCurrentDirectory();

            path = Path.Combine(path, "setting.ini");


            Txt txtFile = new Txt();
            bool status = txtFile.SetPathToFile(path);

            if (status)
            {

                string pathToDirectoryLibrary = txtFile.Select("<pathToLibrary>", "</pathToLibrary>");
                string relativePathToCurrentAudiobook = txtFile.Select("<currentAudiobook>", "</currentAudiobook>");

                string valueSpeed = txtFile.Select("<playbackSpeed>", "</playbackSpeed>");
                


                string pathToCurrentAudiobook = Path.Combine(pathToDirectoryLibrary, relativePathToCurrentAudiobook);

                DirectoryInfo pathToAudiobook = new DirectoryInfo(pathToCurrentAudiobook);

                currentAudiobook = new Audiobook(pathToAudiobook);
                currentAudiobook.audiobookPlaying = false;
                
                if( currentAudiobook.StatusAudiobookOK() )
                {
                    InitilizeMediaPlayer();

                    InitilizeAudiofile();

                    InitializeCmbBxSelectorSpeedPlayback(valueSpeed.Replace(',', '.'));
                }
                else
                {
                    MessageBox.Show("Текущая аудиокнига не может быть воспроизведена. Возможно она была переименована или перемещена. Выполните обновление библиотеки", "Внимание");
                }

                
            }

            

        }


        private void InitializeCmbBxSelectorSpeedPlayback(string valueSpeed) {

            int index = cmbBxSelectorSpeedPlayback.FindString(valueSpeed);

            cmbBxSelectorSpeedPlayback.SelectedIndex = index;
        }


        /// <summary>
        /// Настройка скорости воспроизведения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBxSelectorSpeedPlayback_SelectedIndexChanged(object sender, EventArgs e) {
            
            int indexSpeed = cmbBxSelectorSpeedPlayback.SelectedIndex;
            
            mediaPlayer.SpeedRatio = arraySpeed[indexSpeed];

            if (currentAudiobook.audiobookPlaying == false)
            {
                mediaPlayer.Pause();
            }


            //перемотка назад на 5 секунды. Если не перемотать, то пропустим фрагмент аудиокниги
            int valueTime = (int)mediaPlayer.Position.TotalSeconds - 5;
            MakeRewind(valueTime);

            WriteStatePlayerIntoFile();
        }

        private void nameAudiobook_MouseLeave(object sender, EventArgs e) {
            toolTip.Hide(nameAudiobook);
        }

        private void nameAudiobook_MouseEnter(object sender, EventArgs e) {
            toolTip.Show(currentAudiobook.nameAudiobook, nameAudiobook);
        }
    }
}
