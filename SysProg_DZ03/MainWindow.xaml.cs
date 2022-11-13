using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace SysProg_DZ03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    struct HorseResult
    {
        public string Horsename { set; get; }
        public TimeSpan Time { set; get; }
    }

    public partial class MainWindow : Window
    {
        public static object locker = new();
        static Random random = new Random(DateTime.Now.Millisecond);
        public MainWindow()
        {
            InitializeComponent();
            horseResults = new();
            LV_Results.ItemsSource = horseResults;
        }

        ObservableCollection<HorseResult> horseResults;
        void RunHorsy(ProgressBar pb, string horsename)
        {
            double minvalue = 0, maxvalue = 0;
            Dispatcher.InvokeAsync(delegate () { pb.Value = 0; minvalue = pb.Minimum; maxvalue = pb.Maximum; });
            DateTime startTime = DateTime.Now;
            int increment = random.Next(1, 10);
            for (var i = minvalue; i <= maxvalue; i += increment)
            {
                Dispatcher.InvokeAsync(delegate () { pb.Value += i; });
                Thread.Sleep(300);
            }
            DateTime endTime = DateTime.Now;
            Dispatcher.InvokeAsync(delegate () { horseResults.Add(new HorseResult { Horsename = horsename, Time = endTime.Subtract(startTime) }); });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread1 = new(() => RunHorsy(PB_Horsy_1, "Horse A"));
            thread1.IsBackground = true;
            Thread thread2 = new(() => RunHorsy(PB_Horsy_2, "Horse B"));
            thread2.IsBackground = true;
            Thread thread3 = new(() => RunHorsy(PB_Horsy_3, "Horse C"));
            thread3.IsBackground = true;
            Thread thread4 = new(() => RunHorsy(PB_Horsy_4, "Horse D"));
            thread4.IsBackground = true;
            Thread thread5 = new(() => RunHorsy(PB_Horsy_5, "Horse E"));
            thread5.IsBackground = true;

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();


        }

    }
}
