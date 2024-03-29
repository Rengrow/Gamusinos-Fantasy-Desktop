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
using System.Windows;
using System.Windows.Controls;
using GamusinosProject.Model;

namespace GamusinosProject.ViewModel
{
    class CreateAccountViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        GamusinosFantasyEntities ctx = new GamusinosFantasyEntities();

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

        public CreateAccountViewModel()
        {
            EqualsPasswordAndConfirmation = "Hidden";
            RepeatedUsername = "Hidden";
        }

        #region View

        #region Private Attributes
        private String _userNick;
        private String _password;
        private String _confirmationPassword;
        private String _equalsPasswordAndConfirmation;
        private String _repeatedUsername;
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
        public String ConfirmationPassword
        {
            get
            {
                return _confirmationPassword;
            }
            set
            {
                _confirmationPassword = value;
            }
        }
        public String EqualsPasswordAndConfirmation
        {
            get
            {
                return _equalsPasswordAndConfirmation;
            }
            set
            {
                _equalsPasswordAndConfirmation = value;
                NotifyPropertyChanged();
            }
        }

        public String RepeatedUsername
        {
            get
            {
                return _repeatedUsername;
            }
            set
            {
                _repeatedUsername = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pBox = sender as PasswordBox;
            string blank = pBox.Password;

        }

        private void CreateUser()
        {
            if (Password.Equals(ConfirmationPassword))
            {
                bool repeatedUser = ctx.Users.Where(u => u.nick.Equals(UserNick)).FirstOrDefault() != null;
                if (!repeatedUser)
                {
                    User newUser = new User();
                    newUser.nick = UserNick;
                    newUser.password = Tools.SecurePasswordHasher.Hash(Password);
                    newUser.recoverCode = GenerateRecuperationCode(10);

                    newUser.Player = new Player();
                    newUser.Player.vitality = 20;
                    newUser.Player.force = 5;
                    newUser.Player.resistance = 5;
                    newUser.Player.speed = 5;
                    newUser.Player.luck = 1;

                    newUser.Player.Inventory = new Inventory();
                    newUser.Player.Backpack = new Backpack();
                    Armor defaultArmor = ctx.Armors.Where(w => w.id == 1).FirstOrDefault();
                    Weapon defaultWeapon = ctx.Weapons.Where(w => w.id == 1).FirstOrDefault();
                    newUser.Player.Weapon = defaultWeapon;
                    newUser.Player.Armor = defaultArmor;
                    newUser.Player.Inventory.Armors.Add(defaultArmor);
                    newUser.Player.Inventory.Weapons.Add(defaultWeapon);

                    ctx.Users.Add(newUser);
                    ctx.SaveChanges();

                    this.Dialogs.Add(new RecoverCodeViewModel
                    {
                        RecoverCode = newUser.recoverCode
                    });

                    RequestClose();
                }
            }
            else
            {
                EqualsPasswordAndConfirmation = "Visible";

            }

        }

        private string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static string GenerateRecuperationCode(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public ICommand CreateUserCommand
        {
            get { return new RelayCommand(CreateUser); }
        }
        #endregion

        #endregion
    }
}
