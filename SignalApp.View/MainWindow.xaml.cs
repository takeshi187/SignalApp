using LiveCharts;
using LiveCharts.Wpf;
using SignalApp.ApplicationServices.Interfaces;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SignalApp.View
{
    public partial class MainWindow : Window
    {
        private readonly ISignalService _signalService;
        private readonly ISignalRepository _repository;
        private readonly IFileStorageService _fileStorage;

        private List<SignalPoint> _lastPoints;
        public MainWindow(
            ISignalService signalService,
            ISignalRepository signalRepository,
            IFileStorageService fileStorage)
        {
            InitializeComponent();
            _signalService = signalService;
            _repository = signalRepository;
            _fileStorage = fileStorage;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SignalTypeComboBox.ItemsSource = Enum.GetNames(typeof(SignalTypeEnum));
            SignalTypeComboBox.SelectedIndex = 0;
        }

        private bool TryGetParameters( // Проверка входных данных
            out SignalTypeEnum type,
            out double amplitude,
            out double frequency,
            out int pointsCount)
        {
            try
            {
                type = Enum.Parse<SignalTypeEnum>(SignalTypeComboBox.SelectedItem.ToString());
                amplitude = double.Parse(AmplitudeTextBox.Text);
                frequency = double.Parse(FrequencyTextBox.Text);
                pointsCount = int.Parse(PointsCountTextBox.Text);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неверный ввод: {ex.Message}");
                type = default;
                amplitude = frequency = pointsCount = 0;
                return false;
            }
        }

        private void DrawPlot(List<SignalPoint> points)
        {
            var values = new ChartValues<double>(points.Select(p => p.Value));

            SignalChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = values,
                    PointGeometry = null,     // без точек
                    StrokeThickness = 1
                }
            };

            // Оси
            SignalChart.AxisX.Clear();
            SignalChart.AxisY.Clear();

            SignalChart.AxisX.Add(new Axis
            {
                Title = "Index",
                LabelsRotation = 0
            });

            SignalChart.AxisY.Add(new Axis
            {
                Title = "Value"
            });
        }

        private void ShowAnalysis(List<SignalPoint> points)
        {
            MaxValueText.Text = $"Max: {_signalService.GetMax(points)}";
            MinValueText.Text = $"Min: {_signalService.GetMin(points)}";
            AverageValueText.Text = $"Average: {_signalService.GetAverage(points)}";
            ZeroCrossText.Text = $"Zero Crossings: {_signalService.ZeroCrossingsCount(points)}";
        }

        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            if (!TryGetParameters(out var type, out var amp, out var freq, out var count))
                return;

            _lastPoints = _signalService.Generate(type, amp, freq, count);

            DrawPlot(_lastPoints);
            ShowAnalysis(_lastPoints);

            MessageBox.Show("Signal generated.");
        }

        private async void OnGenerateSaveDbClick(object sender, RoutedEventArgs e)
        {
            if (!TryGetParameters(out var type, out var amp, out var freq, out var count))
                return;

            var signal = await _signalService.GenerateAndSaveToDbAsync(type, amp, freq, count);

            _lastPoints = signal.Points.ToList();

            DrawPlot(_lastPoints);
            ShowAnalysis(_lastPoints);

            MessageBox.Show($"Saved to DB. Signal ID = {signal.SignalId}");
        }

        private void OnGenerateSaveFileClick(object sender, RoutedEventArgs e)
        {
            if (!TryGetParameters(out var type, out var amp, out var freq, out var count))
                return;

            _lastPoints = _signalService.Generate(type, amp, freq, count);

            string path = _fileStorage.SaveToTxt(
                "Signals",
                type, amp, freq, count,
                _lastPoints);

            DrawPlot(_lastPoints);
            ShowAnalysis(_lastPoints);

            MessageBox.Show($"Saved to file:\n{path}");
        }
    }
}