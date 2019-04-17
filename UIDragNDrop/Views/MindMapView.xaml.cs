using System;
using System.Collections.Generic;
using UIDragNDrop.Models;
using Xamarin.Forms;

namespace UIDragNDrop.Views
{
    public partial class MindMapView : ContentView
    {
        public MindMapEntity MapEntity { get; set; }

        public MindMapView(MindMapEntity entity = null)
        {
            this.MapEntity = entity;
            this.BindingContext = this.MapEntity;
            InitializeComponent();
        }
    }
}
