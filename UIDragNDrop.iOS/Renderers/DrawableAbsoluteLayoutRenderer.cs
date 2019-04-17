using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using UIDragNDrop.iOS.Renderers;
using UIDragNDrop.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DrawableAbsoluteLayout), typeof(DrawableAbsoluteLayoutRenderer))]
namespace UIDragNDrop.iOS.Renderers
{
    public class DrawableAbsoluteLayoutRenderer : VisualElementRenderer<View>
    {
        bool longPress = false;
        bool firstTime = true;
        double lastTimeStamp = 0f;
        UIPanGestureRecognizer panGesture;
        UITapGestureRecognizer tapGesture;
        CGPoint lastLocation;
        CGPoint originalPosition;
        UIGestureRecognizer.Token panGestureToken;
        UIGestureRecognizer.Token tapGestureToken;
        void DetectPan()
        {
            var dragView = Element as DrawableAbsoluteLayout;
            //if (longPress || dragView.DragMode == DragMode.Touch)
            {
                if (panGesture.State == UIGestureRecognizerState.Began)
                {
                    dragView.DragStarted();
                    if (firstTime)
                    {
                        originalPosition = Center;
                        firstTime = false;
                    }
                }

                CGPoint translation = panGesture.TranslationInView(Superview);
                var currentCenterX = Center.X;
                var currentCenterY = Center.Y;
                //if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Horizontal)
                {
                    currentCenterX = lastLocation.X + translation.X;
                }

                //if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Vertical)
                {
                    currentCenterY = lastLocation.Y + translation.Y;
                }

                Center = new CGPoint(currentCenterX, currentCenterY);

                if (panGesture.State == UIGestureRecognizerState.Ended)
                {
                    dragView.DragEnded();
                    longPress = false;
                }
            }
        }

        void DetectTapped()
        {
            var dragView = Element as DrawableAbsoluteLayout;
            CGPoint location = tapGesture.LocationInView(this.NativeView);
            dragView.DoubleTappedEvent(new Point(location.X, location.Y));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                RemoveGestureRecognizer(panGesture);
                panGesture.RemoveTarget(panGestureToken);
            }
            if (e.NewElement != null)
            {
                var dragView = Element as DrawableAbsoluteLayout;
                panGesture = new UIPanGestureRecognizer();
                panGestureToken = panGesture.AddTarget(DetectPan);
                AddGestureRecognizer(panGesture);

                tapGesture = new UITapGestureRecognizer();
                tapGesture.NumberOfTapsRequired = 2;
                tapGestureToken = tapGesture.AddTarget(DetectTapped);
                AddGestureRecognizer(tapGesture);

                dragView.RestorePositionCommand = new Command(() =>
                {
                    if (!firstTime)
                    {

                        Center = originalPosition;
                    }
                });
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var dragView = Element as DrawableAbsoluteLayout;
            base.OnElementPropertyChanged(sender, e);

        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            lastTimeStamp = evt.Timestamp;
            Superview.BringSubviewToFront(this);
            lastLocation = Center;
        }
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            if (evt.Timestamp - lastTimeStamp >= 0.5)
            {
                longPress = true;
            }
            var location = (touches.AnyObject as UITouch).LocationInView(this.NativeView);
            base.TouchesMoved(touches, evt);
        }
    }
}
