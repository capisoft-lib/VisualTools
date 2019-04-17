using System;
using System.Collections.Generic;
using UIDragNDrop.Models;

namespace UIDragNDrop.ViewModels
{
    public class DragNDropViewModel : BaseViewModel
    {
        private List<Icone> items;
        public List<Icone> Items { get => items; set { items = value; NotifyPropertyChanged(); } }

        private Icone selectedItem;
        public Icone ItemSelected
        {
            get => selectedItem; set
            {
                selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        public DragNDropViewModel()
        {
            Items = new List<Icone>()
            {
                new Icone{ Name = "Euro", Image = "euro"},
                new Icone{ Name = "Dollar", Image = "dollar"},
                new Icone{ Name = "Bitcoin", Image = "bitcoin"},
            };
        }
    }
}
