﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using QuantConnect.Lean.Monitor.Model.Messages;
using QuantConnect.Lean.Monitor.ViewModel;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace QuantConnect.Lean.Monitor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMessenger _messenger;

        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {            
            InitializeComponent();

            // TODO: Implement dependency injection for windows
            _messenger = Messenger.Default; 
            _messenger.Register<ShowNewSessionWindowMessage>(this, message => ShowWindowDialog<NewSessionWindow>());
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Tell the viewModel we have loaded and we can process data
            ViewModel.Initialize();
        }

        private void ShowWindowDialog<T>() where T : Window
        {
            var window = Activator.CreateInstance<T>();
            window.Owner = this;
            window.ShowDialog();
        }

        private static void OpenLink(string link)
        {
            try
            {
                Process.Start(link);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ShowAboutButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowWindowDialog<AboutWindow>();
        }

        private void BrowseLeanGithubMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            OpenLink("https://github.com/QuantConnect/Lean");
        }

        private void BrowseMonitorGithubMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            OpenLink("https://github.com/mirthestam/lean-monitor");
        }

        private void BrowseChartingDocumentationMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            OpenLink("https://www.quantconnect.com/docs#Charting");
        }
    }
}
