using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
using AtmInternational.Hardware.Annotations;
using AtmInternational.Hardware.Helpers;

namespace AtmInternational.Hardware.Components
{
    /// <summary>
    /// Interaction logic for SlotMachineWheel.xaml
    /// </summary>
    public partial class SlotMachineWheel : UserControl, INotifyPropertyChanged
    {


        private string pName = "test";

        public string Name
        {
            get => pName;
            set => OnPropertyChanged(pName);
        }
        
        private DispatcherTimer updateImageTimer;
        public int currentFrameIndex;
        private readonly CroppedBitmap croppedBitmap;
        private List<CroppedBitmap> frames = new List<CroppedBitmap>();
        private Random random = new Random();

        public int SpriteWidth { get; set; } = 0;
        public int SpriteHeight { get; set; } = 0;
        public int SpriteFrameCount { get; set; } = 0;
        public int Interval { get; set; } = 700;
        public Uri ImageUri { get; set; }


        public ICommand SpinCommand { get; }


        //private ICommand _clickCommand;
        //public ICommand ClickCommand
        //{
        //    get
        //    {
        //        return _clickCommand ??= new CommandHandler(Spin,()=>true);
        //    }
        //}
        
        public SlotMachineWheel()
        {
            InitializeComponent();
            this.DataContext = this;
            SpinCommand = new CommandHandler(Spin, () => true);
        }

        private void CreateFrames()
        {
            var fullBitmap = new BitmapImage(ImageUri);

            for (int i = 0; i < SpriteFrameCount; i++)
            {
                frames.Add(new CroppedBitmap(fullBitmap, new Int32Rect(SpriteWidth * i, 0, SpriteWidth, SpriteHeight)));
            }
        }

        private void updateImageTimer_Tick(object sender, EventArgs e)
        {
            ShowNextRandomFrame();
            //if (currentFrameIndex >= SpriteFrameCount)
            //{
            //    currentFrameIndex = 0;
            //}

            ////if (currentFrameIndex == 5)
            ////{
            ////    Stop();
            ////}

            //image.Source = frames[currentFrameIndex];
            //currentFrameIndex++;
        }

        public void Play()
        {
            currentFrameIndex = 0;
            updateImageTimer.Start();
        }

        public void Stop()
        {
            updateImageTimer.Stop();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            updateImageTimer = new DispatcherTimer(DispatcherPriority.Render)
            {
                Interval = TimeSpan.FromMilliseconds(Interval)
            };
            updateImageTimer.Tick += updateImageTimer_Tick;

            CreateFrames();
            ShowNextRandomFrame();
        }

        private void ShowNextRandomFrame()
        {
            Name = currentFrameIndex.ToString();
            var index = random.Next(0, SpriteFrameCount);
            if (index == currentFrameIndex)
            {
                ShowNextRandomFrame();
            }
            
            image.Source = frames[index];
            currentFrameIndex = index;
        }

        private void ShowFrame(int frameIndex)
        {
            image.Source = frames[frameIndex];

        }

        private void Spin()
        {
            updateImageTimer.Start();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
