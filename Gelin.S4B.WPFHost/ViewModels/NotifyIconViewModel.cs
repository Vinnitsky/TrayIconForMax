using Gelin.S4B.WPFHost.MVVM;
using System.Windows;
using System.Windows.Input;

namespace Gelin.S4B.WPFHost.ViewModels
{
    public class NotifyIconViewModel : ViewModelBase
    {
        private bool _listening;
        public bool Listening { get { return _listening; } set { _listening = value; FireEvent(); } }

        public NotifyIconViewModel()
        {
            StratListeningCommand = new RelayCommand(StratListening, CanStratListening);
            StopListeningCommand = new RelayCommand(StopListening, CanStopListening);
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
            HideWindowCommand = new RelayCommand(HideWindow, CanHideWindow);
            ExitApplicationCommand = new RelayCommand(ExitApplication, CanExitApplication);
        }

        #region commands
        public ICommand StratListeningCommand { get; private set; }
        private void StratListening(object obj)
        {
            System.Diagnostics.Debug.WriteLine("StratListening");
            Listening = true;
            (StopListeningCommand as RelayCommand).OnCanExecuteChanged();
            (StratListeningCommand as RelayCommand).OnCanExecuteChanged();
        }
        private bool CanStratListening(object obj)
        {
            return Listening == false;
        }

        public ICommand StopListeningCommand { get; private set; }
        private void StopListening(object obj)
        {
            System.Diagnostics.Debug.WriteLine("StopListening");
            Listening = false;
            (StopListeningCommand as RelayCommand).OnCanExecuteChanged();
            (StratListeningCommand as RelayCommand).OnCanExecuteChanged();
        }
        private bool CanStopListening(object obj)
        {
            return Listening == true;
        }

        public ICommand ShowWindowCommand { get; private set; }
        private void ShowWindow(object obj)
        {
            System.Diagnostics.Debug.WriteLine("ShowWindow");
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();

        }
        private bool CanShowWindow(object obj)
        {
            return Application.Current.MainWindow == null;
        }

        public ICommand HideWindowCommand { get; private set; }
        private void HideWindow(object obj)
        {
            System.Diagnostics.Debug.WriteLine("HideWindow");
            Application.Current.MainWindow.Close();
        }
        private bool CanHideWindow(object obj)
        {
            return Application.Current.MainWindow != null;
        }

        public ICommand ExitApplicationCommand { get; private set; }
        private void ExitApplication(object obj)
        {
            System.Diagnostics.Debug.WriteLine("ExitApplication");
            Application.Current.Shutdown();
        }
        private bool CanExitApplication(object obj)
        {
            return true;
        }
        #endregion
    }
}
