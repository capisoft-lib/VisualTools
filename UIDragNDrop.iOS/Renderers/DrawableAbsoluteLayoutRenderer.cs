using System;
using System.Collections.Generic;
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
        CGPoint lastMoveLocation;
        CGPoint originalPosition;
        UIGestureRecognizer.Token panGestureToken;
        UIGestureRecognizer.Token tapGestureToken;
        private List<BoxView> displayedPlots = new List<BoxView>();

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
                CGPoint position = panGesture.LocationInView(this);
                nfloat currentCenterX = 0; //Center.X;
                nfloat currentCenterY = 0; //Center.Y;
                //if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Horizontal)
                {
                    currentCenterX = lastLocation.X + translation.X;
                }

                //if (dragView.DragDirection == DragDirectionType.All || dragView.DragDirection == DragDirectionType.Vertical)
                {
                    currentCenterY = lastLocation.Y + translation.Y;
                }

                if (panGesture.State == UIGestureRecognizerState.Ended)
                {
                    dragView.DragEnded();
                    longPress = false;
                    foreach (var bvi in displayedPlots)
                    {
                        dragView.Children.Remove(bvi);
                    }
                    displayedPlots = new List<BoxView>();
                }

                //Center = new CGPoint(currentCenterX, currentCenterY);
                BoxView bv = new BoxView { Color = Color.DarkRed, HeightRequest = 4, WidthRequest = 4 };
                displayedPlots.Add(bv);
                //Device.BeginInvokeOnMainThread(() =>
                //{
                dragView.Children.Add(bv, new Point(position.X, position.Y));
                if (Math.Abs(lastMoveLocation.X - position.X) > 3 || Math.Abs(lastMoveLocation.Y - position.Y) > 3)
                {
                    double minX = Math.Min(lastMoveLocation.X, position.X);
                    double maxX = Math.Max(lastMoveLocation.X, position.X);
                    double minY = Math.Min(lastMoveLocation.Y, position.Y);
                    double maxY = Math.Max(lastMoveLocation.Y, position.Y);
                    Console.WriteLine($" {minX}->{maxX} : {minY}->{maxY}");
                    for (double i = minX; i < maxX; i += 3)
                    {
                        for (double j = minY; j < maxY; j += 3)
                        {
                            BoxView bvi = new BoxView { Color = Color.DarkRed, HeightRequest = 4, WidthRequest = 4 };
                            displayedPlots.Add(bvi);
                            dragView.Children.Add(bvi, new Point(i, j));
                        }
                    }
                }
                lastMoveLocation = position;
                Console.WriteLine($"Add plot to : {position.X},{position.Y}");
                //});
            }
        }

        public void AddMidPlot(CGPoint p1, CGPoint p2)
        {
            double dist = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            if (dist > 3)
            {
                double minX = Math.Min(p1.X, p2.X);
                double maxX = Math.Max(p1.X, p2.X);
                double minY = Math.Min(p1.Y, p2.Y);
                double maxY = Math.Max(p1.Y, p2.Y);
                double midX = Math.Abs(maxX - minX);
                double midY = Math.Abs(maxY - minY);
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
            lastMoveLocation = (touches.AnyObject as UITouch).LocationInView(this);
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
