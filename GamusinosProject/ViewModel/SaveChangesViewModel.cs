using System;
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
    class SaveChangesViewModel : IUserDialogViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DialogClosing;

        GamusinosFantasyEntities ctx = new GamusinosFantasyEntities();

        public SaveChangesViewModel() {
            User currentPlayer = ctx.Users.Where(x => x.id == Tools.CurrentUser.id).SingleOrDefault();
            Player = currentPlayer.Player;
        }

        #region Vars
        private ObservableCollection<IDialogViewModel> _Dialog =
            new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialog; } }
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

        public ICommand SaveChangesCommand {
            get { return new RelayCommand(saveChangesComm); }
        }
        public void saveChangesComm() {
            Player.vitality = NewVit;
            Player.force = NewFor;
            Player.resistance = NewRes;
            Player.speed = NewSpe;
            Player.luck = NewLuc;
            Player.expPoints = Exp;
            Player.Armor = ctx.Armors.Where(w => w.id == NewArmor.id).FirstOrDefault();
            Player.Weapon = ctx.Weapons.Where(w => w.id == NewWeapon.id).FirstOrDefault(); 
            ctx.SaveChanges();

            RequestClose();
        }

        public bool IsModal
        {
            get
            {
                return true;
            }
        }

        private Player _player;
        private long _newVit;
        private long _newFor;
        private long _newRes;
        private long _newSpe;
        private long _newLuc;
        private long _exp;
        private Armor _newArmor;
        private Weapon _newWeapon;
   
        public Weapon NewWeapon
        {
            get
            {
                return _newWeapon;
            }
            set
            {
                _newWeapon = value;
                NotifyPropertyChanged();
            }
        }
        public Armor NewArmor
        {
            get
            {
                return _newArmor;
            }
            set
            {
                _newArmor = value;
                NotifyPropertyChanged();
            }
        }
        public long Exp
        {
            get
            {
                return _exp;
            }
            set
            {
                _exp = value;
                NotifyPropertyChanged();
            }
        }
        public Player Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
                NotifyPropertyChanged();
            }
        }
        public long NewFor

        {
            get
            {
                return _newFor;
            }
            set
            {
                _newFor = value;
                NotifyPropertyChanged();
            }
        }
        public long NewVit

        {
            get
            {
                return _newVit;
            }
            set
            {
                _newVit = value;
                NotifyPropertyChanged();
            }
        }
        public long NewRes

        {
            get
            {
                return _newRes;
            }
            set
            {
                _newRes = value;
                NotifyPropertyChanged();
            }
        }
        public long NewSpe

        {
            get
            {
                return _newSpe;
            }
            set
            {
                _newSpe = value;
                NotifyPropertyChanged();
            }
        }
        public long NewLuc

        {
            get
            {
                return _newLuc;
            }
            set
            {
                _newLuc = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

