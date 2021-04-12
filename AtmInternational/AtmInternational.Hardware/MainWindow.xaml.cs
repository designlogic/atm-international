using System;
using System.Windows;
using AtmInternational.Hardware.Helpers;
using AtmInternational.Hardware.ViewModels;

namespace AtmInternational.Hardware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RandomSlotMachineSymbolGenerator randomSlotMachineSymbolGenerator;

        public MainWindow()
        {
            InitializeComponent();
            randomSlotMachineSymbolGenerator = new RandomSlotMachineSymbolGenerator();
        }

        private void Spin(object sender, RoutedEventArgs e)
        {
            Wheel1.Spin(randomSlotMachineSymbolGenerator.WheelOne);
            Wheel2.Spin(randomSlotMachineSymbolGenerator.WheelTwo);
            Wheel3.Spin(randomSlotMachineSymbolGenerator.WheelThree);
        }
    }

    public class RandomSlotMachineSymbolGenerator
    {
        private readonly Random random = new Random();

        public SlotWheelSymbol WheelOne => GetRandomSlotWheelSymbol();
        public SlotWheelSymbol WheelTwo => GetRandomSlotWheelSymbol();
        public SlotWheelSymbol WheelThree => GetRandomSlotWheelSymbol();

        private SlotWheelSymbol GetRandomSlotWheelSymbol()
        {
            return (SlotWheelSymbol) random.Next(0, 6);
        }
    }
}
