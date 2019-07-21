﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using System.Windows.Input;
using System.ComponentModel;
using GamusinosProject.Model;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace GamusinosProject.ViewModel
{
    class HomeViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {
        GamusinosFantasyEntities ctx = new GamusinosFantasyEntities();

        private ObservableCollection<IDialogViewModel> _Dialog =
            new ObservableCollection<IDialogViewModel>();
        public ObservableCollection<IDialogViewModel> Dialogs { get { return _Dialog; } }

        #region autoGenerated

        public virtual bool IsModal { get { return true; } }
        public event EventHandler DialogClosing;
        #endregion

        #region closeCommand
        public void RequestClose()
        {
            this.DialogClosing(this, null);
        }

        public ICommand CloseCommand
        {
            get { return new RelayCommand(RequestClose); }
        }
        #endregion


        #region atributes

        private Player _player;
        private long _hunterlvl;
        //bonusEquip
        private long _equipmentBonusVit;
        private long _equipmentBonusFor;
        private long _equipmentBonusRes;
        private long _equipmentBonusSpe;
        private long _equipmentBonusLuc;
        // improve
        private long _improveFor;
        private long _improveRes;
        private long _improveVit;
        private long _improveSpe;
        private long _improveLuc;
        // change Armor/Weapon
        private Weapon _chWeapon;
        private Armor _chArmor;
        // EnableButtons
        private bool _impRes;
        private bool _impVit;
        private bool _impFor;
        private bool _impSpe;
        private bool _impLuc;

        public bool ImpLuc
        {
            get
            {
                return _impLuc;
            }
            set
            {
                _impLuc = value;
                NotifyPropertyChanged();
            }
        }
        public bool ImpSpe
        {
            get
            {
                return _impSpe;
            }
            set
            {
                _impSpe = value;
                NotifyPropertyChanged();
            }
        }
        public bool ImpFor
        {
            get
            {
                return _impFor;
            }
            set
            {
                _impFor = value;
                NotifyPropertyChanged();
            }
        }
        public bool ImpVit
        {
            get
            {
                return _impVit;
            }
            set
            {
                _impVit = value;
                NotifyPropertyChanged();
            }
        }
        public bool ImpRes
        {
            get
            {
                return _impRes;
            }
            set
            {
                _impRes = value;
                NotifyPropertyChanged();
            }
        }
        public Weapon ChWeapon
        {
            get
            {
                return _chWeapon;
            }
            set
            {
                _chWeapon = value;
                Player.Weapon = ChWeapon;
                FillAll();
                NotifyPropertyChanged();
            }
        }
        public Armor ChArmor

        {
            get
            {
                return _chArmor;
            }
            set
            {
                _chArmor = value;
                Player.Armor = ChArmor;
                FillAll();
                NotifyPropertyChanged();
            }
        }
        public long ImproveLuc

        {
            get
            {
                return _improveLuc;
            }
            set
            {
                _improveLuc = value;
                NotifyPropertyChanged();
            }
        }
        public long ImproveSpe

        {
            get
            {
                return _improveSpe;
            }
            set
            {
                _improveSpe = value;
                NotifyPropertyChanged();
            }
        }
        public long ImproveVit

        {
            get
            {
                return _improveVit;
            }
            set
            {
                _improveVit = value;
                NotifyPropertyChanged();
            }
        }
        public long ImproveRes
        {
            get
            {
                return _improveRes;
            }
            set
            {
                _improveRes = value;
                NotifyPropertyChanged();
            }
        }
        public long ImproveFor

        {
            get
            {
                return _improveFor;
            }
            set
            {
                _improveFor = value;
                NotifyPropertyChanged();
            }
        }
        public long EquipmentBonusLuc
        {
            get
            {
                return _equipmentBonusLuc;
            }
            set
            {
                _equipmentBonusLuc = value;
                NotifyPropertyChanged();
            }
        }
        public long EquipmentBonusSpe
        {
            get
            {
                return _equipmentBonusSpe;
            }
            set
            {
                _equipmentBonusSpe = value;
                NotifyPropertyChanged();
            }
        }
        public long EquipmentBonusRes
        {
            get
            {
                return _equipmentBonusRes;
            }
            set
            {
                _equipmentBonusRes = value;
                NotifyPropertyChanged();
            }
        }
        public long EquipmentBonusFor
        {
            get
            {
                return _equipmentBonusFor;
            }
            set
            {
                _equipmentBonusFor = value;
                NotifyPropertyChanged();
            }
        }
        public long EquipmentBonusVit
        {
            get
            {
                return _equipmentBonusVit;
            }
            set
            {
                _equipmentBonusVit = value;
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
        public long HunterLvl
        {
            get
            {
                return _hunterlvl;
            }
            set
            {
                _hunterlvl = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public HomeViewModel()
        {
            FillAll();
        }
        public void FillAll()
        {
            User currentPlayer = ctx.Users.Where(x => x.id == Tools.CurrentUser.id).SingleOrDefault();
            Player = currentPlayer.Player;
            if (ChWeapon == null)
                ChWeapon = Player.Weapon;
            if (ChArmor == null)
                ChArmor = Player.Armor;
            HunterLvl = Tools.CurrentUser.currentLvl(Player);
            EquipmentBonusVit = Player.Armor.vitalityBonus + Player.Weapon.vitalityBonus;
            EquipmentBonusFor = Player.Armor.forceBonus + Player.Weapon.forceBonus;
            EquipmentBonusLuc = Player.Armor.luckBonus + Player.Weapon.luckBonus;
            EquipmentBonusSpe = Player.Armor.speedBonus + Player.Weapon.speedBonus;
            EquipmentBonusRes = Player.Armor.resistanceBonus + Player.Weapon.resistanceBonus;

            ImproveVit = (Player.vitality - 20) / 5 + (long)Math.Floor((decimal)HunterLvl / 5) + 1;
            ImproveFor = (Player.force - 5 + (long)Math.Floor((decimal)HunterLvl / 5)) + 1;
            ImproveRes = (Player.resistance - 5 + (long)Math.Floor((decimal)HunterLvl / 5)) + 1;
            ImproveSpe = (Player.speed - 5 + (long)Math.Floor((decimal)HunterLvl / 5)) + 1;
            ImproveLuc = (Player.luck + 4 + (long)Math.Floor((decimal)HunterLvl / 5));

            if (Player.expPoints >= ImproveVit)
            {
                ImpVit = true;
            }
            else
            {
                ImpVit = false;
            }
            if (Player.expPoints >= ImproveFor)
            {
                ImpFor = true;
            }
            else
            {
                ImpFor = false;
            }
            if (Player.expPoints >= ImproveRes)
            {
                ImpRes = true;
            }
            else
            {
                ImpRes = false;
            }
            if (Player.expPoints >= ImproveSpe)
            {
                ImpSpe = true;
            }
            else
            {
                ImpSpe = false;
            }
            if (Player.expPoints >= ImproveLuc)
            {
                ImpLuc = true;
            }
            else
            {
                ImpLuc = false;
            }
        }

        #region vitalityCommand
        public void ImproveVitComm()
        {
            Player.vitality += 5;
            Player.expPoints -= ImproveVit;
            FillAll();
        }
        public ICommand ImproveVitCommand
        {
            get { return new RelayCommand(ImproveVitComm); }
        }
        #endregion
        #region forceCommand
        public void ImproveForComm()
        {
            Player.force += 1;
            Player.expPoints -= ImproveFor;
            FillAll();
        }
        public ICommand ImproveForCommand
        {
            get { return new RelayCommand(ImproveForComm); }
        }
        #endregion
        #region resistanceCommand
        public void ImproveResComm()
        {
            Player.resistance += 1;
            Player.expPoints -= ImproveRes;
            FillAll();
        }
        public ICommand ImproveResCommand
        {
            get { return new RelayCommand(ImproveResComm); }
        }
        #endregion
        #region speedCommand
        public void ImproveSpeComm()
        {
            Player.speed += 1;
            Player.expPoints -= ImproveSpe;
            FillAll();
        }
        public ICommand ImproveSpeCommand
        {
            get { return new RelayCommand(ImproveSpeComm); }
        }
        #endregion
        #region luckCommand
        public void ImproveLucComm()
        {
            Player.luck += 1;
            Player.expPoints -= ImproveLuc;
            FillAll();
        }
        public ICommand ImproveLucCommand
        {
            get { return new RelayCommand(ImproveLucComm); }
        }
        #endregion

        #region SaveChangesCommand
        public void saveChangesComm()
        {
            this.Dialogs.Add(new SaveChangesViewModel
            {
                NewFor = Player.force,
                NewVit = Player.vitality,
                NewLuc = Player.luck,
                NewRes = Player.resistance,
                NewSpe = Player.speed,
                Exp = Player.expPoints,
                NewArmor = Player.Armor,
                NewWeapon = Player.Weapon
            });
        }

        public ICommand saveChangesCommand
        {
            get { return new RelayCommand(saveChangesComm); }
        }
        #endregion

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
    }
}