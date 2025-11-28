using OxyPlot;
using OxyPlot.Series;
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

        private List<SignalPoint> _lastPoints;

        public MainWindow(
            ISignalService signalService,
            ISignalRepository signalRepository)
        {
            InitializeComponent();
            _signalService = signalService;
            _repository = signalRepository;
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
                MessageBox.Show($"Ошибка ввод: {ex.Message}");
                type = default;
                amplitude = frequency = pointsCount = 0;
                return false;
            }
        }

        private void DrawPlot(List<SignalPoint> points)
        {
            var model = new PlotModel
            {
                Title = "Сигнал",
            };

            var line = new LineSeries
            {
                Color = OxyColors.DeepSkyBlue,
                StrokeThickness = 1
            };

            foreach (var p in points)
                line.Points.Add(new DataPoint(p.Time, p.Value));

            model.Series.Add(line);

            SignalPlot.Model = model;
        }

        private void ShowAnalysis(List<SignalPoint> points)
        {
            MaxValueText.Text = $"Макс.: {_signalService.GetMax(points)}";
            MinValueText.Text = $"Мин.: {_signalService.GetMin(points)}";
            AverageValueText.Text = $"Среднее: {_signalService.GetAverage(points)}";
            ZeroCrossText.Text = $"Пер. нуля: {_signalService.ZeroCrossingsCount(points)}";
        }

        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            if (!TryGetParameters(out var type, out var amp, out var freq, out var count))
                return;

            _lastPoints = _signalService.Generate(type, amp, freq, count);

            DrawPlot(_lastPoints);
            ShowAnalysis(_lastPoints);

            MessageBox.Show("Сигнал успешно сгенерирован.");
        }

        private async void OnGenerateSaveDbClick(object sender, RoutedEventArgs e)
        {
            if (!TryGetParameters(out var type, out var amp, out var freq, out var count))
                return;

            var signal = await _signalService.GenerateAndSaveToDbAsync(type, amp, freq, count);

            _lastPoints = signal.Points.ToList();

            DrawPlot(_lastPoints);
            ShowAnalysis(_lastPoints);

            MessageBox.Show($"Сигнал сохранен в БД. SignalId = {signal.SignalId}");
        }

        private void OnGenerateSaveFileClick(object sender, RoutedEventArgs e)
        {
            if (!TryGetParameters(out var type, out var amp, out var freq, out var count))
                return;

            string filePath = _signalService.GenerateAndSaveToFile(
                type, amp, freq, count, "Signals");

            _lastPoints = _signalService.Generate(type, amp, freq, count);

            DrawPlot(_lastPoints);
            ShowAnalysis(_lastPoints);

            MessageBox.Show($"Сигнал сохранен в файл:\n{filePath}");
        }
    }
}