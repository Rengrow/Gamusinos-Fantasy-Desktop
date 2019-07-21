using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace GamusinosProject.ViewModel
{
    class VillageViewModel : IUserDialogViewModel
    {
        private ObservableCollection<IDialogViewModel> _Dialog =
            new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialog; } }
        public bool IsModal { get { return true; } }

        public event EventHandler DialogClosing;

        #region missionPanel
        public void MissionPanelCom()
        {
            this.Dialogs.Add(new MissionPanelViewModel());
        }
        public ICommand MissionCommand
        {
            get { return new RelayCommand(MissionPanelCom); }
        }
        #endregion

        #region close
        public ICommand CloseCommand
        {
            get { return new RelayCommand(RequestClose); }
        }

        public void RequestClose()
        {
            this.DialogClosing(this, null);
        }
        #endregion
        #region logout
        public void logout() {
            Tools.CurrentUser.id = null;
            this.DialogClosing(this, null);
        }
        public ICommand logoutCommand {
            get { return new RelayCommand(logout); }
        }
        #endregion

        #region home
        public void HomeCom()
        {
            this.Dialogs.Add(new HomeViewModel());
        }
        public ICommand HomeCommand
        {
            get { return new RelayCommand(HomeCom); }
        }
        #endregion
        #region trunk
        public void TrunkCom()
        {
            this.Dialogs.Add(new TrunkViewModel());
        }
        public ICommand TrunkCommand
        {
            get { return new RelayCommand(TrunkCom); }
        }
        #endregion
        #region tips
        public void TipsCom()
        {
            this.Dialogs.Add(new UsefulTipsViewModel());
        }
        public ICommand TipsCommand
        {
            get { return new RelayCommand(TipsCom); }
        }
        #endregion
        #region IronWork
        public void IronComm()
        {
            this.Dialogs.Add(new IronWorksViewModel());
        }
        public ICommand IronCommand
        {
            get { return new RelayCommand(IronComm); }
        }
        #endregion
    }
}
