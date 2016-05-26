using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PassphraseGen;
using Windows.Storage;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using SplitViewMenu;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App27
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            InitializeComponent();
            var mainViewModel = new ViewModel();
            
            mainViewModel.MenuItems.Add(new SimpleNavMenuItem
            {
                Label = "Generate",
                DestinationPage = typeof(MainPage1),
                Symbol = Symbol.Calculator
            });
            mainViewModel.MenuItems.Add(new SimpleNavMenuItem
            {
                Label = "Translate",
                DestinationPage = typeof(Translate),
                Symbol = Symbol.Calculator
            });
            DataContext = mainViewModel;
        }

    }
}

