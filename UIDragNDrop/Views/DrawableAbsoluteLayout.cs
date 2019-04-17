using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIDragNDrop.Views
{
    public class DrawableAbsoluteLayout : AbsoluteLayout
    {
        public event EventHandler DragStart = delegate { };
        public event EventHandler DragEnd = delegate { };
        public event EventHandler DoubleTapped = delegate { };

        public static readonly BindableProperty IsDraggingProperty = BindableProperty.Create(
          propertyName: "IsDragging",
          returnType: typeof(bool),
          declaringType: typeof(DrawableAbsoluteLayout),
          defaultValue: false,
          defaultBindingMode: BindingMode.TwoWay);

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        public static readonly BindableProperty RestorePositionCommandProperty = BindableProperty.Create(nameof(RestorePositionCommand), typeof(ICommand), typeof(DrawableAbsoluteLayout), default(ICommand), BindingMode.TwoWay, null, OnRestorePositionCommandPropertyChanged);

        static void OnRestorePositionCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var source = bindable as DrawableAbsoluteLayout;
            if (source == null)
            {
                return;
            }
            source.OnRestorePositionCommandChanged();
        }

        private void OnRestorePositionCommandChanged()
        {
            OnPropertyChanged("RestorePositionCommand");
        }

        public ICommand RestorePositionCommand
        {
            get
            {
                return (ICommand)GetValue(RestorePositionCommandProperty);
            }
            set
            {
                SetValue(RestorePositionCommandProperty, value);
            }
        }

        public void DragStarted()
        {
            DragStart(this, default(EventArgs));
            IsDragging = true;
        }

        public void DragEnded()
        {
            IsDragging = false;
            DragEnd(this, default(EventArgs));
        }

        public void DoubleTappedEvent(Point point)
        {
            DoubleTapped(this, new TappedEventArgs(point));
        }

        public DrawableAbsoluteLayout() : base()
        {
        }
    }
}
