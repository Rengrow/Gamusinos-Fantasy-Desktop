﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GamusinosProject.ViewModel
{
    class RecoverCodeViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private String _recoverCode;
        public String RecoverCode
        {
            get
            {
                return _recoverCode;
            }
            set
            {
                _recoverCode = value;
                NotifyPropertyChanged();
            }
        }


        #region Dialogs

        #region Vars
        private ObservableCollection<IDialogViewModel> _Dialog =
            new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialog; } }
        #endregion

        #region Methods AutoGenerated
        public virtual bool IsModal { get { return true; } }
        public event EventHandler DialogClosing;
        #endregion

        #region Close Methods
        public void RequestClose()
        {
            this.DialogClosing(this, null);
        }

        public ICommand CloseCommand
        {
            get { return new RelayCommand(RequestClose); }
        }
        #endregion

        #endregion
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
