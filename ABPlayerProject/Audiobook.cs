using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib.Mpeg;
using CommonCL;
using System.Windows;

namespace ABPlayer {
    
    public class Audiobook {

        public string nameAudiobook { get; set; }

        public DirectoryInfo pathToDirectoryAudiobook;

        public List<AudioFile> audiofiles = null;

        public string pathToBookmarks { get; set; }

        public string pathToFileInfoAudiobook { get; set; }

        public Bookmarks bookmarks;

        public int durationAudiobook { get; }

        public int completionStatus;

        public bool audiobookPlaying;

        public delegate void RewindAudiofileEndedEventHandler();
        public event RewindAudiofileEndedEventHandler WasRewind;

        /// <summary>
        /// Текущее время воспроизведения проигрываемого аудиофайла
        /// </summary>
        public int currentPosition { get; set; }

        public AudioFile currentAudiofile = null;

        public Audiobook (DirectoryInfo path){
            pathToDirectoryAudiobook = path;
            nameAudiobook = pathToDirectoryAudiobook.Name;
            pathToBookmarks = Path.Combine(pathToDirectoryAudiobook.FullName, "bookmarks.sabp.xml");
            bookmarks = new Bookmarks(pathToBookmarks);
            
            GetAudiofiles();
            durationAudiobook = GetDurationAudiobook();
            pathToFileInfoAudiobook = Path.Combine(pathToDirectoryAudiobook.FullName, "infoAudiobook.txt");
            currentPosition = GetInfoAboutAudiobook();
            
            audiobookPlaying = false;

        }

        /// <summary>
        /// Функция сообщает состояние аудиокниги. Т.е. может ли она быть воспроизведена. Если да, то true, иначе - false
        /// </summary>
        /// <returns></returns>
        public bool StatusAudiobookOK() {
            bool status = true;

            if( pathToDirectoryAudiobook.Exists )
            {
                status &= true;
            }
            else
            {
                status &= false;
            }

            if (audiofiles != null)
            {
                if ((audiofiles.Count > 0))
                {
                    status &= true;
                }
                else
                {
                    status &= false;
                }
            }
            else
            {
                status &= false;
            }

            return status;
        }

        public void WasRewindAudifile() {
            WasRewind?.Invoke();
        }

        /// <summary>
        /// Эта функция возвращает продолжительность аудиофайла
        /// </summary>
        /// <returns></returns>
        public int GetDurationCurrentAudiofile() {

            var file = currentAudiofile;
            int duraction = (int)currentAudiofile.Properties.Duration.TotalSeconds;

            return duraction;
        }

        private void GetAudiofiles() {
            try
            {
                FileInfo[] files = pathToDirectoryAudiobook.GetFiles();

                audiofiles = new List<AudioFile>();

                foreach (var file in files)
                {
                    if (file.Extension == ".mp3")
                    {
                        audiofiles.Add(new AudioFile(file.FullName));
                    }
                }
            }
            catch
            {
                audiofiles = null;
            }            

        }


        private int GetDurationAudiobook() {

            int duraction = 0;

            try
            {
                if(audiofiles != null)
                {
                    foreach (var audioFile in audiofiles)
                    {

                        duraction += (int)audioFile.Properties.Duration.TotalSeconds;

                    }
                }
                
            }
            catch
            {

            }

            return duraction;
        }

        private int GetInfoAboutAudiobook() {
            int currentPosition = 0;

            if( StatusAudiobookOK() ){

                Txt txtFile = new Txt();
                bool status = txtFile.SetPathToFile(pathToFileInfoAudiobook);

                if (status)
                {
                    string position = txtFile.Select("<currentPosition>", "</currentPosition>");
                    currentPosition = Convert.ToInt32(position);



                    string pathToAudiobook = this.pathToDirectoryAudiobook.FullName;
                    string currentAudiofile = txtFile.Select("<currentAudiofile>", "</currentAudiofile>");

                    FileInfo pathMediaFile = new FileInfo( Path.Combine(pathToAudiobook, currentAudiofile) );

                    if ( pathMediaFile.Exists )
                    {
                        this.currentAudiofile = new AudioFile( pathMediaFile.FullName );
                    }
                    else
                    {
                        this.currentAudiofile = audiofiles[0];
                    }

                }
                else
                {
                    this.currentAudiofile = audiofiles[0];
                }

                //Процент завершения аудиокниги
                float temp = (float)currentPosition / (float)durationAudiobook;
                completionStatus = (int)(temp * 100);

            }

            return currentPosition;
        }

        public void WriteStateAudiobookIntoFile() {
            
            Txt txtFile = new Txt();

            //если файл существует, он не будет создан
            txtFile.CreateFile(pathToFileInfoAudiobook);

            txtFile.text = "<currentAudiofile>";
            string nameAudiofile = new FileInfo( currentAudiofile.Name ).Name;
            txtFile.text += nameAudiofile;
            txtFile.text += "</currentAudiofile>";
            
            txtFile.text += "\n";

            txtFile.text += "<currentPosition>";
            txtFile.text += currentPosition.ToString();
            txtFile.text += "</currentPosition>";

            txtFile.RewriteFile();
        }
    }
    /*
    public class Audiofile {
        
        public FileInfo pathToFile;
        public string fileName;
        public int durationAudiofile;

        public Audiofile(FileInfo pathToFile) {
            this.pathToFile = pathToFile;

            GetDurationAudiofile();

            GetNameAudiofile();
        }

        public Audiofile(string pathToFile) {
            this.pathToFile = new FileInfo(pathToFile);

            GetDurationAudiofile();

            GetNameAudiofile();
        }

        private void GetDurationAudiofile() {
        
            var file = TagLib.File.Create(pathToFile.FullName);
            durationAudiofile = file.Properties.Duration.Seconds + file.Properties.Duration.Minutes * 60 + file.Properties.Duration.Hours * 3600;

        }

        private void GetNameAudiofile() {

            string NameWithoutExtension = Path.GetFileNameWithoutExtension(pathToFile.Name);

            fileName = NameWithoutExtension;

        }
    }
    */

}
