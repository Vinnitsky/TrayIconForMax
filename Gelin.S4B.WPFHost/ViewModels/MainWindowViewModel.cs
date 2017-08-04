using Gelin.S4B.WPFHost.MVVM;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Input;

namespace Gelin.S4B.WPFHost.ViewModels
{
    sealed class MainWindowViewModel : ViewModelBase
    {
        private string _header;
        private string _message;

        public MainWindowViewModel()
        {
            _header = "GelinSoft";
            _message = "This is a dummy text";

            DisplayErrorBalloonCommand = new RelayCommand(DisplayErrorBaloon, (x) => { return true; });
            DisplayWorningBalloonCommand = new RelayCommand(DisplayWorningBaloon, (x) => { return true; });
            DisplayInfoBalloonCommand = new RelayCommand(DisplayInfoBaloon, (x) => { return true; });
        }
        #region Properties

        public string Header { get { return _header; } set { _header = value; FireEvent(); } }
        public string Message { get { return _message; } set { _message = value; FireEvent(); } }
        #endregion

        #region commands
        public ICommand DisplayErrorBalloonCommand { get; private set; }
        private void DisplayErrorBaloon(object obj)
        {
            System.Diagnostics.Debug.WriteLine("StratListening");
            DispalyBalloon("GelinSoft", "This is a dummy text", BalloonIcon.Error);
        }
        public ICommand DisplayWorningBalloonCommand { get; private set; }
        private void DisplayWorningBaloon(object obj)
        {
            System.Diagnostics.Debug.WriteLine("StratListening");
            DispalyBalloon("GelinSoft", "This is a dummy text", BalloonIcon.Warning);
        }
        public ICommand DisplayInfoBalloonCommand { get; private set; }
        private void DisplayInfoBaloon(object obj)
        {
            System.Diagnostics.Debug.WriteLine("DisplayInfoBaloon");

            DispalyBalloon("GelinSoft", "This is a dummy text", BalloonIcon.Info);
        }

        #endregion

        #region implementation
        private void DispalyBalloon(string title, string message, BalloonIcon icon)
        {
            var tb = Application.Current.TryFindResource("NotifyIcon") as TaskbarIcon;
            tb?.ShowBalloonTip(title, message, icon);
        }
        #endregion
    }
}
