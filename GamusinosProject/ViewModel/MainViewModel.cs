using GalaSoft.MvvmLight.Command;
using GamusinosProject.Model;
using MvvmDialogs.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GamusinosProject.ViewModel
{
    class MainViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<IDialogViewModel> _Dialog =
            new ObservableCollection<IDialogViewModel>();
        GamusinosFantasyEntities ctx = new GamusinosFantasyEntities();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialog; } }
        public event EventHandler DialogClosing;
        public bool IsModal { get { return false; } }

        public MainViewModel()
        {
            CorrectPassword = "Hidden";
            TimeOut = "Hidden";
        }

        #region dataChange
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region commands

        public ICommand CreateCommandG
        {
            get { return new RelayCommand(CreateCommand); }
        }
        public ICommand SigninComandG
        {
            get { return new RelayCommand(SigninCommand); }
        }
        public ICommand CloseCommand
        {
            get { return new RelayCommand(RequestClose); }
        }

        #endregion

        #region Private Attributes
        private String _userNick;
        private String _password;
        private String _correctPassword;
        private String _timeOut;
        #endregion

        #region Public Attributes
        public String UserNick
        {
            get
            {
                return _userNick;
            }
            set
            {
                _userNick = value;
            }
        }
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public String CorrectPassword
        {
            get
            {
                return _correctPassword;
            }
            set
            {
                _correctPassword = value;
                NotifyPropertyChanged();
            }
        }

        public String TimeOut
        {
            get
            {
                return _timeOut;
            }
            set
            {
                _timeOut = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region comandMethods
        public void CreateCommand()
        {
            this.Dialogs.Add(new CreateAccountViewModel());
        }
        public void SigninCommand()
        {
            CorrectPassword = "Hidden";
            TimeOut = "Hidden";
            try
            {
                User user = ctx.Users.Where(u => u.nick.Equals(UserNick)).FirstOrDefault();
                if (user != null && Password != null && Tools.SecurePasswordHasher.Verify(Password, user.password))
                {
                    GamusinosProject.ViewModel.Tools.CurrentUser.id = user.id;
                    this.Dialogs.Add(new VillageViewModel());
                }
                else
                {
                    CorrectPassword = "Visible";
                }
            }
            catch (EntityException e)
            {
                TimeOut = "Visible";
            }
        }

        public void RequestClose()
        {
            this.DialogClosing(this, null);
        }
        #endregion

        public void ResetComm()
        {
            this.Dialogs.Add(new ResetPassViewModel());
        }

        public ICommand ResetCommand
        {
            get { return new RelayCommand(ResetComm); }
        }
    }
}