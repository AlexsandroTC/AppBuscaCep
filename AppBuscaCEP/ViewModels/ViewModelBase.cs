using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBuscaCEP.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Propriedade para notificar as views que ocorreu alteração nas propriedades que são utilizadas com Bidding.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null) return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Propriedade usada para fazer load. 
        /// </summary>
        private bool _isBusy = false;
        public bool IsBusy 
        { 
            get => _isBusy; 
            set 
            { 
                _isBusy = value; 
                OnPropertyChanged(); 
                OnPropertyChanged(nameof(IsNotBusy)); 
            } 
        }

        public bool IsNotBusy { get => !_isBusy; }
    }
}
