using System.Windows.Controls;
using System.Windows.Media;

namespace labs08
{
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();
        private readonly Dictionary<int, (TextBlock PriceText, TextBlock ChangeText, Border Container)> _uiElements;
        private readonly double[] _prices = { 145.50, 78.25, 210.75, 89.99 };

        private const int UpdateIntervalMs = 5000;
        private const double MaxPriceChange = 1.0;

        public MainWindow()
        {
            InitializeComponent();
            _uiElements = InitializeUiElements();
            StartSimulation();
        }

        private Dictionary<int, (TextBlock, TextBlock, Border)> InitializeUiElements()
        {
            return new Dictionary<int, (TextBlock, TextBlock, Border)>
            {
                { 0, (Price1, Change1, Box1) },
                { 1, (Price2, Change2, Box2) },
                { 2, (Price3, Change3, Box3) },
                { 3, (Price4, Change4, Box4) }
            };
        }

        private void StartSimulation()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(UpdateIntervalMs);
                    await UpdateAllPrices();
                }
            });
        }

        private async Task UpdateAllPrices()
        {
            await Dispatcher.InvokeAsync(() =>
            {
                foreach (var kvp in _uiElements)
                {
                    UpdateUi(kvp.Key, kvp.Value.PriceText, kvp.Value.ChangeText, kvp.Value.Container);
                }
            });
        }

        private void UpdateUi(int id, TextBlock priceText, TextBlock changeText, Border container)
        {
            double change = CalculatePriceChange();
            _prices[id] += change;

            UpdatePriceDisplay(priceText, changeText, change);
            UpdateContainerColor(container, change);
        }

        private double CalculatePriceChange()
        {
            return _random.Next((int)(-MaxPriceChange * 100), (int)(MaxPriceChange * 100) + 1) / 100.0;
        }

        private void UpdatePriceDisplay(TextBlock priceText, TextBlock changeText, double change)
        {
            priceText.Text = _prices[priceText.Name[priceText.Name.Length - 1] - '0'].ToString("F2") + " PLN";
            changeText.Text = (change >= 0 ? "+" : "") + change.ToString("F2") + " PLN";
        }

        private void UpdateContainerColor(Border container, double change)
        {
            if (change >= 0)
            {
                container.Background = Brushes.LimeGreen;
                container.BorderBrush = Brushes.DarkGreen;
            }
            else
            {
                container.Background = Brushes.OrangeRed;
                container.BorderBrush = Brushes.DarkRed;
            }
        }
    }
}