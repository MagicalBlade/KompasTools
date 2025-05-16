using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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
    }
}
