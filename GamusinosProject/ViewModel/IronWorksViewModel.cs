using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using System.Collections.ObjectModel;

namespace GamusinosProject.ViewModel
{
    class IronWorksViewModel : IUserDialogViewModel
    {
        private ObservableCollection<IDialogViewModel> _Dialog =
            new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialog; } }

        public virtual bool IsModal { get { return true; } }
        public event EventHandler DialogClosing;

        public void RequestClose()
        {
            this.DialogClosing(this, null);
        }

        public ICommand CloseCommand
        {
            get { return new RelayCommand(RequestClose); }
        }

        public void ArmorComm() {
            this.Dialogs.Add(new CreateArmorViewModel());
        }

        public ICommand ArmorCommand {
            get { return new RelayCommand(ArmorComm); }
        }
        public void WeaponComm()
        {
            this.Dialogs.Add(new CreateWeaponViewModel());
        }

        public ICommand WeaponCommand
        {
            get { return new RelayCommand(WeaponComm); }
        }
    }
}
