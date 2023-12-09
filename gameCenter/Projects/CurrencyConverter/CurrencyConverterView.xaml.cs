using gameCenter.Projects.CurrencyConverter.Services;
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
using System.Windows.Shapes;

namespace gameCenter.Projects.CurrencyConverter
{
    /// <summary>
    /// Interaction logic for CurrencyConverterView.xaml
    /// </summary>
    public partial class CurrencyConverterView : Window

    {
        private CurrencyService _currencyService;
        private Dictionary<string, double> _exchangeRates;

        public CurrencyConverterView()
        {
            InitializeComponent();
            _currencyService = new CurrencyService();
            LoadCurrencies();

        }

        private async void LoadCurrencies()
        {
            try
            {
                _exchangeRates = await _currencyService.GetExchangeRatesAsync();
                string[] currencyNames = _exchangeRates.Keys.ToArray();
                FromCurrencyComboBox.ItemsSource = currencyNames;
                ToCurrencyComboBox.ItemsSource = currencyNames;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading currencies: {e.Message}");
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {

            string fromCurrency = FromCurrencyComboBox.SelectedItem?.ToString();
            string toCurrency = ToCurrencyComboBox.SelectedItem?.ToString();
            double amount;

            if (!string.IsNullOrWhiteSpace(AmountTextBox.Text))
            {
                amount = double.Parse(AmountTextBox.Text);
            }
            else
            {
                MessageBox.Show("Please enter a valid exchange rate!");
                return;
            }

            if (fromCurrency == null || toCurrency == null || amount <= 0)
            {
                MessageBox.Show("Please make sure that you've entered a positive number for \nthe convertion rate and currencies are selected!");
                return;
            }

            double baseToFromRate = _exchangeRates[fromCurrency];
            double baseToToRate = _exchangeRates[toCurrency];

            double convertedAmount = (baseToToRate / baseToFromRate) * amount;

            txtResult.Text = $"Converted amount: \n {amount} {fromCurrency} \n to \n {convertedAmount.ToString()} {toCurrency} ";
        }

    }
}
