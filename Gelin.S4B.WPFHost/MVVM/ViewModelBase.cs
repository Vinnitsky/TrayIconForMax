using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gelin.S4B.WPFHost.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation
        public void FireEvent([CallerMemberName]string propertyName = null)
        {
            Fire(new PropertyChangedEventArgs(propertyName));
        }
        private void Fire(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, e);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
