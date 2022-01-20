using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonCL;


namespace ABPlayer {
    public partial class Library : Form
    {
        //Путь к библиотеки нужно считывать из текстового файла
        private DirectoryInfo coreDirectoryLibrary;

        // embedded class
        class Node
        {
            public string Name { get; private set; }
            public string Column1 { get; private set; }
            public string Column2 { get; private set; }
            public string Column3 { get; private set; }
            public string Column4 { get; private set; }
            public List<Node> Children { get; private set; }
            public Node(string name, string col1)
            {
                this.Name = name;
                this.Column1 = col1;
                this.Children = new List<Node>();
            }
        }

        // private fields
        private List<Node> data;
        private BrightIdeasSoftware.TreeListView treeListView;

        

        private List<Audiobook> audiobooks;

        public Audiobook selectedAudiobook;

        // constructor
        public Library()
        {
            audiobooks = new List<Audiobook>();
            InitializeComponent();
            AddTree();
            
            ReadDataMediaFromSetingFile();
            
            
            InitializeData();
            FillTree();

            selectedAudiobook = null;

            treeListView.DoubleClick += btnAccept_Click;
        }

        // private methods
        private void FillTree()
        {
            treeListView.Clear();

            // set the delegate that the tree uses to know if a node is expandable
            treeListView.CanExpandGetter = x => (x as Node).Children.Count > 0;
            // set the delegate that the tree uses to know the children of a node
            treeListView.ChildrenGetter = x => (x as Node).Children;

            // create the tree columns and set the delegates to print the desired object proerty
            var nameCol = new BrightIdeasSoftware.OLVColumn("Книги", "Name");
            nameCol.Width = 250;
            nameCol.AspectGetter = x => (x as Node).Name;

            var col1 = new BrightIdeasSoftware.OLVColumn("Завершено", "Column1");
            col1.Width = 75;
            col1.AspectGetter = x => (x as Node).Column1;


            // add the columns to the tree
            treeListView.Columns.Add(nameCol);
            treeListView.Columns.Add(col1);

            // set the tree roots
            treeListView.Roots = data;
        }

        private void InitializeData()
        {
            data = new List<Node>();
            
            foreach(var audiobook in audiobooks){
                var node = new Node(audiobook.nameAudiobook, audiobook.completionStatus + "%");
                data.Add(node);
            }

        }

        private void AddTree()
        {
            treeListView = new BrightIdeasSoftware.TreeListView();

            treeListView.Location = new Point(12, 27);
            treeListView.Size = new Size(335, 273);
            this.Controls.Add(treeListView);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void btnAccept_Click(object sender, EventArgs e)
        {

            int indexAudiobook = treeListView.SelectedIndex;

            if (indexAudiobook >= 0)
            {
                selectedAudiobook = audiobooks[indexAudiobook];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите аудиокнигу для воспроизведения", "Внимание");
            }
            
        }

        private void SetCoreDirectory_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    coreDirectoryLibrary = new DirectoryInfo(dialog.SelectedPath);

                    btnRefreshLibrary.PerformClick();

                }
                    

           
        }

        private void btnRefreshLibrary_Click(object sender, EventArgs e)
        {
            audiobooks = new List<Audiobook>();

            GetAudiobooks(coreDirectoryLibrary);

            if(audiobooks.Count > 0)
            {
                InitializeData();
                FillTree();

                WriteAudiobookIntoSettingFile();
            }
            else
            {
                MessageBox.Show("Аудиокниги в корневой директории не найдены","Внимание");
            }

            
        }

        private void WriteAudiobookIntoSettingFile() {
            string path = Directory.GetCurrentDirectory();

            path = Path.Combine(path, "setting.ini");


            Txt txtFile = new Txt();
            bool status = txtFile.SetPathToFile(path);

            int length = coreDirectoryLibrary.FullName.Length + 1;

            if (status)
            {
                string txtBlockAudiobooks = "";

                foreach(var audiobook in audiobooks)
                {
                    txtBlockAudiobooks += "\n<audiobook>\n";

                    txtBlockAudiobooks += "<pathToAudiobook>";
                    txtBlockAudiobooks += audiobook.pathToDirectoryAudiobook.FullName.Remove(0, length);
                    txtBlockAudiobooks += "</pathToAudiobook>\n";

                    txtBlockAudiobooks += "<completionStatus>";
                    txtBlockAudiobooks += audiobook.completionStatus.ToString();
                    txtBlockAudiobooks += "</completionStatus>";

                    txtBlockAudiobooks += "\n</audiobook>\n";
                }

                bool statusRewriteLibrary = txtFile.RewriteBlock("<library>", "</library>", txtBlockAudiobooks);

                if (statusRewriteLibrary == false){
                    txtFile.text = "<library>" + txtBlockAudiobooks + "</library>";
                    txtFile.WriteBlock();
                }


                
                bool statusRewritePathToLibrary = txtFile.RewriteBlock("<pathToLibrary>", "</pathToLibrary>", coreDirectoryLibrary.FullName); ;

                if (statusRewritePathToLibrary == false)
                {
                    txtFile.text = "<pathToLibrary>" + coreDirectoryLibrary.FullName + "</pathToLibrary>";
                    txtFile.WriteBlock();
                }

            }
        }

        private void GetAudiobooks(DirectoryInfo pathDirectory, int depth=0)
        {
            //Увеличиваем глубину рекурсии
            depth++;

            //Ограничение глубины рекурсии
            if(depth > 5){
                return;
            }

            if  (IsDirectoryAudiobook(pathDirectory) && (pathDirectory != coreDirectoryLibrary) )
            {
                Audiobook audiobook = new Audiobook(pathDirectory);
                audiobooks.Add(audiobook);
            }

            DirectoryInfo[] dirs = pathDirectory.GetDirectories();

            foreach(var dir in dirs){
                GetAudiobooks(dir, depth);
            }

        }

        private void SelectAudiobookFromBlockText(List<string> blockText) {

            bool statusAudiobooksOK = true;

            Txt blockAudiobook = new Txt();

            foreach (var text in blockText)
            {
                blockAudiobook.text = text;

                string pathToAudiobook = Path.Combine( coreDirectoryLibrary.FullName, blockAudiobook.Select("<pathToAudiobook>", "</pathToAudiobook>") );

                DirectoryInfo pathToDirectoryAudiobook = new DirectoryInfo(pathToAudiobook);                

                Audiobook audiobook = new Audiobook(pathToDirectoryAudiobook);
                audiobook.completionStatus = Convert.ToInt32(blockAudiobook.Select("<completionStatus>", "</completionStatus>"));

                if ( audiobook.StatusAudiobookOK())
                {
                    audiobooks.Add(audiobook);
                }
                else
                {
                    statusAudiobooksOK &= false;
                }

                
            }

            if( statusAudiobooksOK == false)
            {
                MessageBox.Show("Одна или несколько аудиокниг не найдены. Выполните 'Настройки -> Обновить библиотеку'","Внимание");
            }

        }

        //Функция проверяет является ли папка папкой аудиокниги
        //Для этого необходимо, чтобы в папке содержался хотя бы один аудиофайл
        private bool IsDirectoryAudiobook(DirectoryInfo path) {

            FileInfo[] files = path.GetFiles();

            foreach (var file in files)
            {
                if (file.Extension == ".mp3")
                {
                    return true;
                }
            }

            return false;
        }
    
        //функция считыванию пути к библиотеке
        private void ReadDataMediaFromSetingFile() {

            string path = Directory.GetCurrentDirectory();

            path = Path.Combine(path, "setting.ini");


            Txt txtFile = new Txt();
            bool status = txtFile.SetPathToFile(path);

            if (status)
            {
                this.coreDirectoryLibrary = new DirectoryInfo(txtFile.Select("<pathToLibrary>", "</pathToLibrary>"));

                if( coreDirectoryLibrary.Exists) {
                    SelectAudiobookFromBlockText(txtFile.SelectBlock("<audiobook>", "</audiobook>"));
                }
                else {
                    MessageBox.Show("Корневая директория задана некорректно. Выполните 'Настройки -> Короневая директория'", "Внимание");
                }


            }

            
            //где-то здесь нужно считать библиотеку из файла
        }
    }
}
