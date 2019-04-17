using System;
using System.Collections.Generic;
using UIDragNDrop.Models;
using UIDragNDrop.Views;
using Xamarin.Forms;

namespace UIDragNDrop.Pages
{
    public partial class EntityViewPage : ContentPage
    {
        public Entity TestEntity { get; set; }

        public EntityViewPage()
        {
            InitializeComponent();

            TestEntity = new Entity
            {
                Name = "Test",
                Properties = new PropertyEntity[]{
                    new PropertyEntity { Name = "Name", Type = typeof(string)},
                    new PropertyEntity { Name = "Color", Type = typeof(Color)},
                    new PropertyEntity { Name = "Count", Type = typeof(int)}
                },
                Methods = new MethodEntity[] {
                    new MethodEntity{ Name = "AddToCount", Params = new PropertyEntity[]{ new PropertyEntity { Name = "toAdd", Type = typeof(int)} }}
                }
            };

            EntityView entityView = new EntityView();
            entityView.BindingContext = TestEntity;

            DraggableView draggableView = new DraggableView() { Content = entityView, DragMode = DragMode.Touch, DragDirection = DragDirectionType.All };

            stackLayout.Children.Add(draggableView);
        }
    }
}
