﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;

namespace StudentDiary
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var metroWindow = Current.MainWindow as MetroWindow;
            metroWindow.ShowMessageAsync("Nieoczekiwany wyjątek", "Wystąpił nieoczekiwany wyjątek." + Environment.NewLine + e.Exception.Message);
            e.Handled = true;
        }
    }
}
