using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIDragNDrop.Models;
using UIDragNDrop.ViewModels;
using Xamarin.Forms;

namespace UIDragNDrop
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public DragNDropViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.ViewModel = new DragNDropViewModel();
            this.BindingContext = this.ViewModel;
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            ViewModel.ItemSelected = e.SelectedItem as Icone;
            //image.Source = monkey.Image;
            switch (ViewModel.ItemSelected.Name)
            {
                case "Euro":
                    dragView.DragMode = DragMode.LongPress;
                    dragView.DragDirection = DragDirectionType.Vertical;
                    break;
                case "Dollar":
                    dragView.DragMode = DragMode.Touch;
                    dragView.DragDirection = DragDirectionType.Horizontal;
                    break;
                default:
                    dragView.DragMode = DragMode.LongPress;
                    dragView.DragDirection = DragDirectionType.All;
                    break;
            }
            dragView.RestorePositionCommand.Execute(null);

            dragLayout.IsVisible = true;

        }

        void OnCloseTapped(object sender, System.EventArgs e)
        {
            dragLayout.IsVisible = false;
            list.SelectedItem = null;
        }
    }
}
