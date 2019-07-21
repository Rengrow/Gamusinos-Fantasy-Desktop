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
using GamusinosProject.Model;

namespace GamusinosProject.ViewModel
{
    class CreateArmorViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {
        GamusinosFantasyEntities ctx = new GamusinosFantasyEntities();

        public CreateArmorViewModel() {
            User currentPlayer = ctx.Users.Where(x => x.id == Tools.CurrentUser.id).SingleOrDefault();
            Player = currentPlayer.Player;
            FillArmors();
        }

        #region cosas de los dialogos
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

        private Player _player;
        private List<Armor> _armors;
        private Armor _selectedArmor;
        private List<Armor_Craft> _itemsCraft;
        private bool _canCreate;
        private List<Inventory_Items> _itemsHave;


        public List<Inventory_Items> ItemsHave
        {
            get
            {
                return _itemsHave;
            }
            set
            {
                _itemsHave = value;
                NotifyPropertyChanged();
            }
        }
        public Player Player {
            get
            {
                return _player;
            }
            set {
                _player = value;
                NotifyPropertyChanged();
            }
        }
        public List<Armor> Armors {
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
        public Armor SelectedArmor
        {
            get
            {
                return _selectedArmor;
            }
            set
            {
                _selectedArmor = value;
                if (value != null)
                {
                    List<Inventory_Items> cosa = new List<Inventory_Items>();
                    if (ItemsHave == null)
                        ItemsHave = new List<Inventory_Items>();

                    ItemsCraft = value.Armor_Craft.ToList();
                    List<Inventory_Items> allItems = Player.Inventory.Inventory_Items.ToList();
                    int cont = 0;
                    List<Armor_Craft> itemsNeed = ItemsCraft.ToList();

                    foreach (Armor_Craft item in itemsNeed)
                    {
                        foreach (Inventory_Items Iitem in allItems)
                        {
                            if (item.Item.Equals(Iitem.Item))
                            {
                                cosa.Add(Iitem);
                            }
                            if (item.Item.Equals(Iitem.Item) && item.quantity <= Iitem.Quantity)
                            {
                                cont++;
                            }

                        }
                    }

                    ItemsHave = cosa;

                    if (cont == ItemsCraft.Count)
                    {
                        CanCreate = true;
                    }
                    else
                    {
                        CanCreate = false;
                    }
                }
                NotifyPropertyChanged();
            }
        }
        public List<Armor_Craft> ItemsCraft
        {
            get
            {
                return _itemsCraft;
            }
            set
            {
                _itemsCraft = value;
                NotifyPropertyChanged();
            }
        }
        public bool CanCreate
        {
            get
            {
                return _canCreate;
            }
            set
            {
                _canCreate = value;
                NotifyPropertyChanged();
            }
        }

        public void FillArmors() {

            List<Armor> allArmors = ctx.Armors.ToList();
            List<Armor> armorsCreated = Player.Inventory.Armors.ToList();
            Armors = allArmors.Except(armorsCreated).ToList();
        }

        public void craftArmorComm() {

            List<Inventory_Items> allItems = Player.Inventory.Inventory_Items.ToList();
            List<Armor_Craft> itemsNeed = ItemsCraft.ToList();

            foreach (Armor_Craft item in itemsNeed)
            {
                foreach (Inventory_Items Iitem in allItems)
                {
                    if (item.Item.Equals(Iitem.Item))
                    {
                        Iitem.Quantity -= (long)item.quantity;
                    }

                }
            }

            Player.Inventory.Armors.Add(SelectedArmor);
            ctx.SaveChanges();
            FillArmors();
        }

        public ICommand craftArmorCommand {
            get { return new RelayCommand(craftArmorComm); }
        }

    }
}
