using DocumentFormat.OpenXml.Wordprocessing;
using KompasTools.Utils;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KompasTools
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Привязка события изменения списка сварных швов
            if (WeldDates.Items is INotifyCollectionChanged WeldDatesNotifyCollectionChanged)
            {
                WeldDatesNotifyCollectionChanged.CollectionChanged += WeldDates_CollectionChanged;
            }
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

        private void B_CurTurned_Click(object sender, RoutedEventArgs e)
        {
            Tb_NameCut.Text += "@63~"; //Добавляем значёк повёрнуто
        }

        private void WeldDates_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            WeldDates.SelectedIndex = 0;            
        }

        private void cb_transitionTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tb_thickness2Str == null) return;
            if (cb_transitionTypes.SelectedIndex == 0)
            {
                tb_TransitionDimLorK.IsEnabled = false;
                cb_IstransitionDimL.IsEnabled = false;
                tb_thickness2Str.IsEnabled = false;
            }
            else
            {
                tb_TransitionDimLorK.IsEnabled = true;
                cb_IstransitionDimL.IsEnabled = true;
                tb_thickness2Str.IsEnabled = true;
            }
        }
    }
}
