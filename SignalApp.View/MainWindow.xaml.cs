using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OxyPlot;
using OxyPlot.Series;
using SignalApp.ApplicationServices.Interfaces;
using SignalApp.Domain.Enums;
using SignalApp.Domain.Interfaces;
using SignalApp.Domain.Models;
using SignalApp.Infrastructure.Database;
using System.Windows;

namespace SignalApp.View
{
    public partial class MainWindow : Window
    {
        private readonly ISignalService _signalService;
        private readonly ISignalRepository _repository;

        private List<SignalPoint> _lastPoints;

        public MainWindow(
            ISignalService signalService,
            ISignalRepository signalRepository
            )
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

        private bool TryGetParameters(
    out SignalTypeEnum type,
    out double amplitude,
    out double frequency,
    out int pointsCount)
        {
            type = default;
            amplitude = 0;
            frequency = 0;
            pointsCount = 0;

            if (SignalTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип сигнала.");
                return false;
            }

            type = Enum.Parse<SignalTypeEnum>(SignalTypeComboBox.SelectedItem.ToString());

            if (!double.TryParse(AmplitudeTextBox.Text, out amplitude))
            {
                MessageBox.Show("Амплитуда должна быть числом.");
                return false;
            }
            if (amplitude <= 0)
            {
                MessageBox.Show("Амплитуда должна быть больше нуля.");
                return false;
            }

            if (!double.TryParse(FrequencyTextBox.Text, out frequency))
            {
                MessageBox.Show("Частота должна быть числом.");
                return false;
            }
            if (frequency <= 0)
            {
                MessageBox.Show("Частота должна быть больше нуля.");
                return false;
            }

            if (!int.TryParse(PointsCountTextBox.Text, out pointsCount))
            {
                MessageBox.Show("Количество точек должно быть целым числом.");
                return false;
            }
            if (pointsCount < 100 || pointsCount > 10000)
            {
                MessageBox.Show("Количество точек должно быть в пределах от 100 до 10000.");
                return false;
            }

            return true;
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