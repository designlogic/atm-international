using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace AtmInternational.Hardware.Components
{
    /// <summary>
    /// Interaction logic for ImageSequencer.xaml
    /// </summary>
    public partial class ImageSequencer : UserControl
    {
        private DispatcherTimer updateImageTimer;
        private int currentFrameIndex;
        private readonly CroppedBitmap croppedBitmap;

        public int SpriteWidth { get; set; } = 0;
        public int SpriteHeight { get; set; } = 0;
        public int SpriteFrameCount { get; set; } = 0;
        public int Interval { get; set; } = 30;
        public Uri ImageUri { get; set; }

        private List<CroppedBitmap> frames = new List<CroppedBitmap>();


        public ImageSequencer()
        {
            InitializeComponent();


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
            if (currentFrameIndex >= SpriteFrameCount)
            {
                currentFrameIndex = 0;
            }

            image.Source = frames[currentFrameIndex];
            currentFrameIndex++;
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

            updateImageTimer.Start();
        }
    }
}