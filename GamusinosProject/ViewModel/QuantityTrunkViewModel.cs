using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs.ViewModels;
using GamusinosProject.Model;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GamusinosProject.ViewModel
{
    class QuantityTrunkViewModel : IUserDialogViewModel, INotifyPropertyChanged
    {

        GamusinosFantasyEntities ctx = new GamusinosFantasyEntities();

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

        public void RequestClose()
        {
            this.DialogClosing(this, null);
        }

        public ICommand CloseCommand
        {
            get { return new RelayCommand(RequestClose); }
        }

        public QuantityTrunkViewModel()
        {

        }

        private List<int> _lQ;
        public List<int> lQ {
            get { return _lQ; }
            set { _lQ = value; NotifyPropertyChanged(); }
        }

        private long _qnt;
        public long Qnt
        {
            get { return _qnt; }
            set { _qnt = value; NotifyPropertyChanged(); }
        }

        private Inventory_Items _item;
        public Inventory_Items Item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (lQ == null)
                    lQ = new List<int>();

                for (int i = 1; i <= value.Quantity; i++)
                {
                    lQ.Add(i);
                }
                NotifyPropertyChanged();
            }
        }

        private int _selectedQnt;
        public int SelectedQnt {
            get { return _selectedQnt; }
            set { _selectedQnt = value; NotifyPropertyChanged(); }
        }


        public void acceptCom() {
            Player currPlayer = ctx.Players.Where(x => x.Users.FirstOrDefault().id == Tools.CurrentUser.id).SingleOrDefault();
            (currPlayer.Inventory.Inventory_Items.Where(x => x.Item.id.Equals(Item.Item_id)).FirstOrDefault()).Quantity -= SelectedQnt;
            ctx.SaveChanges();
            RequestClose();
        }
        public ICommand AcceptCommand {
            get { return new RelayCommand(acceptCom); }
        }


    }
}
