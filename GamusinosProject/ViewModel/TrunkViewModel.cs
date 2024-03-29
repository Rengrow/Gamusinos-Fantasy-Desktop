﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using GamusinosProject.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace GamusinosProject.ViewModel
{
    class TrunkViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        GamusinosFantasyEntities ctx = new GamusinosFantasyEntities();

        public TrunkViewModel()
        {
            FillItems();
            this.Player = ctx.Players.Where(x => x.id == Tools.CurrentUser.id).SingleOrDefault();
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

        #region View

        #region Private Attributes
        private Player _player;
        private List<Inventory_Items> _items;
        private List<Armor> _armors;
        private List<Weapon> _weapons;
        private Inventory_Items _selectedItem;
        #endregion

        #region Public Attributes
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
        public List<Inventory_Items> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }
        public List<Armor> Armors
        {
            get
            {
                return _armors;
            }
            set
            {
                _armors = value;
                NotifyPropertyChanged();
            }
        }
        public List<Weapon> Weapons
        {
            get
            {
                return _weapons;
            }
            set
            {
                _weapons = value;
                NotifyPropertyChanged();
            }
        }
        public Inventory_Items SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;

                if (value != null)
                    InfoCom(value);

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

        private void FillItems()
        {
            Inventory pInventory = ctx.Users.Where(u => u.id == Tools.CurrentUser.id).FirstOrDefault().Player.Inventory;
            Items = pInventory.Inventory_Items.ToList();
            Armors = pInventory.Armors.ToList();
            Weapons = pInventory.Weapons.ToList();
        }

        public void InfoCom(Inventory_Items inv)
        {
            this.Dialogs.Add(new ObjectInfoTrunkViewModel { Item = inv });
            SelectedItem = null;
            RequestClose();
        }

        public void GetItemsBackpack()
        {
            List<Backpack_Items> items = ctx.Users.Where(u => u.id == Tools.CurrentUser.id).FirstOrDefault().Player.Backpack.Backpack_Items.ToList();
            if (items.Count > 0)
            {
                List<Inventory_Items> inventory = ctx.Users.Where(u => u.id == Tools.CurrentUser.id).FirstOrDefault().Player.Inventory.Inventory_Items.ToList();

                foreach (Backpack_Items bItem in items)
                {
                    Inventory_Items item = inventory.Where(i => i.Item_id == bItem.Item_id).FirstOrDefault();
                    if (item != null)
                    {
                        item.Quantity += bItem.Quantity;
                    }
                    else
                    {
                        Inventory_Items newItem = new Inventory_Items();
                        newItem.Item = bItem.Item;
                        newItem.Quantity = bItem.Quantity;
                        ctx.Users.Where(u => u.id == Tools.CurrentUser.id).FirstOrDefault().Player.Inventory.Inventory_Items.Add(newItem);
                    }
                    ctx.Backpack_Items.Remove(bItem);
                }
                ctx.SaveChanges();

                FillItems();
            }
        }


        public ICommand GetItemsBackpackCommand
        {
            get { return new RelayCommand(GetItemsBackpack); }
        }
        #endregion

        #endregion

    }
}
