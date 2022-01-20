using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using CommonCL;

namespace ABPlayer {
    public class Bookmarks
    {
        //закладки должны быть отсортированны по filePosition (временному интервалу)
        string path; //путь к файлу содержащему закладки аудиокниги

        public List<Bookmark> bookmarks;

        public Bookmarks(string path)
        {
            this.path = path;
            //при создании объекта необходимо считать из файла закладки
            
            Read();
        }

        //функция чтения закладок
        public List<Bookmark> Read()
        {
            bookmarks = new List<Bookmark>();

            if (File.Exists(path)){            
                using (StreamReader sr = new StreamReader(path))
                {                                
                    XDocument text = LoadFromMemoryStream(sr);
                    var elements = text.Root.Elements();
                    foreach (var element in elements)
                    {
                        Bookmark bookmark = new Bookmark(element);
                        
                        bookmarks.Add(bookmark);
                    }
                }
            }

            return bookmarks;
        }

        //функция записи закладок
        public void Write()
        {
            Txt txt = new Txt();

            //создаем файл, если он несуществует
            txt.CreateFile(path);

            XDocument document = new XDocument();
            
            XElement root = new XElement("root");

            foreach (var bookmark in bookmarks)
            {
                XElement element = TransformBookmarkToXElement(bookmark);
                root.Add(element);
            }

            document.Add(root);


            if (path != null)
            {
                try
                {
                    //Перезаписываем файл
                    using (StreamWriter fs = new StreamWriter(path, false))
                    {
                        document.Save(fs);
                    }

                }
                catch
                {
                    
                }
            }

            /*using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(text);
            }*/
        }

        private XElement TransformBookmarkToXElement(Bookmark bookmark) {

            XElement element = new XElement("bookmark");

            XElement title = new XElement("title", bookmark.title);
            element.Add(title);

            XElement description = new XElement("description", bookmark.description);
            element.Add(description);

            XElement fileName = new XElement("fileName", bookmark.fileName);
            element.Add(fileName);

            XElement filePosition = new XElement("filePosition", bookmark.filePosition.ToString());
            element.Add(filePosition);


            return element;
        }

        //Парсирование HML-файла
        private XDocument LoadFromMemoryStream(StreamReader ms)
        {
            try
            {
                var s = ms.ReadToEnd();
                return XDocument.Parse(s);
            }
            catch
            {
                return null;
            }
        }

        public void Sort() {
            //bookmarks.Sort();

            var sortedList = bookmarks.OrderBy(x => x.fileName).ThenBy(x => x.filePosition).ToList();

            bookmarks = sortedList;
        }

        public void AddBookmark(Bookmark bookmark) {

            int index = bookmarks.FindIndex((x) => ((x.fileName == bookmark.fileName) && (x.filePosition < bookmark.filePosition)));

            index++;

            bookmarks.Insert(index, bookmark);

            Write();
        }

    }

    public class Bookmark
    {
        public string fileName;
        public int filePosition;
        public string title;
        public string description;

        public Bookmark() { }

        public Bookmark(XElement element) {
            this.title = element.Element("title").Value;
            this.description = element.Element("description").Value;
            this.fileName = element.Element("fileName").Value;
            this.filePosition = Convert.ToInt32( element.Element("filePosition").Value );
            
        }
    }
}
