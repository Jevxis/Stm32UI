using Avalonia.Controls;
using Avalonia.Interactivity;
using Stm32UIController.Services;
using Stm32UIController.ViewModels;
using System.Data;
using System.IO.Ports;

namespace Stm32UIController.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

    }
}