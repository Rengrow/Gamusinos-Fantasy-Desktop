using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using System.Collections.ObjectModel;
using GamusinosProject.Model;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace GamusinosProject.ViewModel
{
    class ObjectInfoTrunkViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual bool IsModal { get { return true; } }
        public event EventHandler DialogClosing;

        private ObservableCollection<IDialogViewModel> _Dialog =
            new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialog; } }

        public void RequestClose()
        {
            this.DialogClosing(this, null);
        }

        public ICommand CloseCommand
        {
            get { return new RelayCommand(RequestClose); }
        }


        public void QuantityComm() {
            this.Dialogs.Add(new QuantityTrunkViewModel { Item = Item });
        }     

        public ICommand QuantityCommand {
            get { return new RelayCommand(QuantityComm); }
        }

        private Inventory_Items _item;
        public Inventory_Items Item {
            get { return _item; }
            set { _item = value; NotifyPropertyChanged(); }
        }


    }
}
