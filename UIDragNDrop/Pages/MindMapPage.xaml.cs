using System;
using System.Collections.Generic;
using UIDragNDrop.Models;
using UIDragNDrop.ViewModels;
using UIDragNDrop.Views;
using Xamarin.Forms;

namespace UIDragNDrop.Pages
{
    public partial class MindMapPage : ContentPage
    {
        public MindMapViewModel ViewModel { get; set; }

        public MindMapPage()
        {
            this.ViewModel = new MindMapViewModel();
            this.BindingContext = this.ViewModel;
            InitializeComponent();
            dndLayout.DoubleTapped += (object sender, EventArgs e) =>
            {
                TappedEventArgs evnt = e as TappedEventArgs;
                Point tappedLocation = (Point)evnt.Parameter;
                MindMapEntity mapEntity = new MindMapEntity
                {
                    Title = $"MindMap{tappedLocation.X}{tappedLocation.Y}"
                };
                MindMapView mindMapView = new MindMapView(mapEntity);
                DraggableView mindMapDrag = new DraggableView { Content = mindMapView };
                TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (sender1, e1) =>
                {
                    this.ViewModel.CurrentMindMapEntity = (sender1 as MindMapView).MapEntity;
                };
                mindMapView.GestureRecognizers.Add(tapGesture);
                this.dndLayout.Children.Add(
                    mindMapDrag,
                    tappedLocation
                );
            };
        }

        private bool hasLoadedBackground = false;
        private void InitCrosses()
        {
            if (!hasLoadedBackground)
            {
                int maxI = (int)Math.Round(dndLayout.Width / 50);
                int maxY = (int)Math.Round(dndLayout.Height / 50);
                for (int i = 0; i < maxI; i++)
                {
                    for (int j = 0; j < maxY; j++)
                    {
                        Image img = new Image { Source = "thin_cross" };
                        dndLayout.Children.Add(img, new Point(i * 50 - 4, j * 50 - 4));
                    }
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.InitCrosses();
        }

        void Handle_Clicked_Clear(object sender, System.EventArgs e)
        {
            this.ViewModel.ClearMindMaps();
        }

        void Handle_Clicked_Add(object sender, System.EventArgs e)
        {
            //this.ViewModel.AddBaseMindMap()
        }

        void Handle_Tapped_CreateNew(object sender, System.EventArgs e)
        {

        }
    }
}