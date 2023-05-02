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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<double> data = new List<double>();
        public MainWindow()
        {
            InitializeComponent();
            
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Изделия",
                    Values = new ChartValues<double> (data)
                }
            };

            DataContext = this;

            Calculate();
            UpdateChart();
        }

        public void UpdateChart() {
            var columnSeries = (ColumnSeries)SeriesCollection[0];
            columnSeries.Values = new ChartValues<double>(data);
        }

        public void Calculate() {
            const int T = 100; // время наблюдения (часы)
            const double lambda = 8.0 / 24; // интенсивность потока (ед/час)

            data = PoissonProcess.Generate(lambda,T);
        }
    }
}

public class PoissonProcess {
    public static List<double> Generate(double rate, double T)
    {
        /* Генерация Пуассоновского потока событий со скоростью rate в течение времени T. */

        List<double> arrivals = new List<double>();
        double n = 0;
        double t = 0;
        while (t < T)
        {
            double U = new Random().NextDouble();
            t -= Math.Log(U) / rate;
            if (t < T)
            {
                arrivals.Add(t);
                n++;
            }
        }
        return arrivals;
    }
}
