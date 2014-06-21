using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace RomanNumbers
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string InvalidRomanNumberErrorMessage = 
            "Tem que introduzir um número romano válido";

        private const string OverTheLimitsNumberErrorMessage = 
            "Apenas são admitidos valores entre 1 e 3999";

        private const string InvalidArabicNumberErrorMEssage =
            "O que introduziu não é um número válido. Introduza um valor entre 1 e 3999";

        private static readonly string[] ConversionTypes = 
        { 
            "Romana para Árabe", 
            "Árabe para Romana"
        };

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.LP_ConversionType.ItemsSource = ConversionTypes;
        }

        private void BtConvertClick(object sender, RoutedEventArgs e)
        {
            if(this.LP_ConversionType.SelectedIndex == 0)
            {
                if (this.Value_Number.Text.Length > 15)
                {
                    MessageBox.Show(InvalidRomanNumberErrorMessage);
                    return;
                }
                int res = RomanConverter.RomanToArabic(this.Value_Number.Text);
                if(res < 0)
                {
                    MessageBox.Show(InvalidRomanNumberErrorMessage);
                    return;
                }
                this.Value_Result.Text = res.ToString();
            }
            else
            {
                try
                {
                    string arabicToRoman = 
                        RomanConverter.ArabicToRoman(Convert.ToInt32(this.Value_Number.Text));
                    if(arabicToRoman == null)
                    {
                        MessageBox.Show(OverTheLimitsNumberErrorMessage);
                        return;
                    }
                    this.Value_Result.Text =
                        arabicToRoman;
                }
                catch (OverflowException)
                {
                    MessageBox.Show(OverTheLimitsNumberErrorMessage);
                }
                catch (FormatException)
                {
                    MessageBox.Show(InvalidArabicNumberErrorMEssage);
                }   
            }
        }

        private void LpConversionTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Value_Number.InputScope.Names.Clear();
            this.Value_Number.InputScope.Names.Add(
                this.LP_ConversionType.SelectedIndex == 0
                    ? new InputScopeName {NameValue = InputScopeNameValue.PersonalNameSuffix}
                    : new InputScopeName {NameValue = InputScopeNameValue.Number});

            this.Value_Number.Text = String.Empty;
            this.Value_Result.Text = String.Empty;
        }

        private void ValueNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.LP_ConversionType.SelectedIndex == 0)
            {
                int selectionStart = this.Value_Number.SelectionStart;
                int selectionLength = this.Value_Number.SelectionLength;
                this.Value_Number.Text = this.Value_Number.Text.ToUpper();
                this.Value_Number.SelectionStart = selectionStart;
                this.Value_Number.SelectionLength = selectionLength;
            }
        }
    }
}