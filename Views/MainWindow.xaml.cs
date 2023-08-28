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
    }
}
