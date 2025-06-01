using System.Globalization;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DocumentFormat.OpenXml.Wordprocessing;
using KompasTools.Utils;

namespace KompasTools
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Label_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label? label = sender as Label;
            DragDrop.DoDragDrop(label, label?.Content, DragDropEffects.Move);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.ScrollToEnd();
        }  

        private void TB_PreviewTextInput_OnlyNumber(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex onlyNumbers = new("[^0-9.]+");
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void WeldGOSTs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NameWeldJoints.UnselectAll();
        }

        private void NameWeldJoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeldingMethod.UnselectAll();
        }

        private void WeldingMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WeldDates.UnselectAll();
        }

        private void B_CurTurned_Click(object sender, RoutedEventArgs e)
        {
            Tb_NameCut.Text += "@63~"; //Добавляем значёк повёрнуто
        }

        private void Tb_LeftScale_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show($"{((TextBox)sender).Text}");
            //bool approvedDecimalPoint = false;
            //if (e.Text == ".")
            //{
            //    if (!((TextBox)sender).Text.Contains('.')) approvedDecimalPoint = true;
            //}

            //if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint)) e.Handled = true;
        } 
    }
}
