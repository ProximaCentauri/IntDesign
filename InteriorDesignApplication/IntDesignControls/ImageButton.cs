﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Input;

namespace IntDesignControls
{
    public class ImageButton : Button, ISupportUserInput
    {
        public ImageButton()
        {
            UNav = String.Empty;
            RefreshLayout();
            TouchDown += (sender, eventArgs) =>
            {
                ImageButton button = sender as ImageButton;
            };
            TouchUp += (sender, eventArgs) =>
            {
                ImageButton button = sender as ImageButton;
                if (null != button && button.touchedDown)
                {
                    button.touchedDown = false;
                    TouchPoint tp = eventArgs.GetTouchPoint(button);
                    Rect bounds = new Rect(new Point(0, 0), button.RenderSize);
                    if (bounds.Contains(tp.Position))
                    {
                        button.OnClick();
                    }
                    button.ReleaseTouchCapture(eventArgs.TouchDevice);
                }
            };
        }

        public bool AllowUserInput
        {
            get { return (bool)GetValue(AllowUserInputProperty); }
            set { SetValue(AllowUserInputProperty, value); }
        }

        public string ClickSound
        {
            get
            {
                return GetValue(ClickSoundProperty) as string;
            }
            set
            {
                SetValue(ClickSoundProperty, value);
            }
        }

        public string FocusSound
        {
            get
            {
                return GetValue(FocusSoundProperty) as string;
            }
            set
            {
                SetValue(FocusSoundProperty, value);
            }
        }

        public ImageSource Image
        {
            get
            {
                return GetValue(ImageProperty) as ImageSource;
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }

        public string ImageSize
        {
            get
            {
                return GetValue(ImageSizeProperty) as string;
            }
            set
            {
                SetValue(ImageSizeProperty, value);
            }
        }

        public string ImageMargin
        {
            get
            {
                return GetValue(ImageMarginProperty) as string;
            }
            set
            {
                SetValue(ImageMarginProperty, value);
            }
        }

        public HorizontalAlignment ImageHorizontalAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(ImageHorizontalAlignmentProperty);
            }
            set
            {
                SetValue(ImageHorizontalAlignmentProperty, value);
            }
        }

        public VerticalAlignment ImageVerticalAlignment
        {
            get
            {
                return (VerticalAlignment)GetValue(ImageVerticalAlignmentProperty);
            }
            set
            {
                SetValue(ImageVerticalAlignmentProperty, value);
            }
        }

        public System.Windows.Media.ImageSource Sound
        {
            get
            {
                return GetValue(ImageProperty) as ImageSource;
            }
            set
            {
                SetValue(ImageProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return GetValue(TextProperty) as string;
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }        

        public static string UNav { get; set; }

        public static DependencyProperty ClickSoundProperty = DependencyProperty.Register("ClickSound", typeof(string), typeof(ImageButton));
        public static DependencyProperty FocusSoundProperty = DependencyProperty.Register("FocusSound", typeof(string), typeof(ImageButton));
        public static DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton));
        public static DependencyProperty ImageSizeProperty = DependencyProperty.Register("ImageSize", typeof(string), typeof(ImageButton));
        public static DependencyProperty ImageMarginProperty = DependencyProperty.Register("ImageMargin", typeof(string), typeof(ImageButton));
        public static DependencyProperty ImageHorizontalAlignmentProperty = DependencyProperty.Register("ImageHorizontalAlignment", typeof(HorizontalAlignment),
            typeof(ImageButton), new PropertyMetadata(HorizontalAlignment.Center));
        public static DependencyProperty ImageVerticalAlignmentProperty = DependencyProperty.Register("ImageVerticalAlignment", typeof(VerticalAlignment),
            typeof(ImageButton), new PropertyMetadata(VerticalAlignment.Center));
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ImageButton),
            new PropertyMetadata(new PropertyChangedCallback(OnTextChanged)));
        public static readonly DependencyProperty AllowUserInputProperty = DependencyProperty.Register("AllowUserProperty", typeof(bool), typeof(ImageButton),
                new PropertyMetadata(true, new PropertyChangedCallback(OnAllowUserInputChanged)));

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            switch (e.Property.Name)
            {
                case "Image":
                case "ImageMargin":
                case "ImageHorizontalAlignment":
                case "ImageSize":
                case "Padding":
                case "VerticalHorizontalAlignment":
                    RefreshLayout();
                    break;
            }
        }

        protected static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs ea)
        {
            ImageButton button = sender as ImageButton;
            if (null != button.textBlock)
            {
                button.textBlock.Text = button.Text;
            }
        }

        protected void RefreshLayout()
        {
            DockPanel panel;
            if (this.Content is DockPanel)
            {
                panel = this.Content as DockPanel;
            }
            else
            {
                panel = new DockPanel();
                panel.HorizontalAlignment = HorizontalAlignment.Stretch;
                panel.VerticalAlignment = VerticalAlignment.Stretch;
                panel.LastChildFill = true;
                this.image = null;
                this.textBlock = null;
            }
            panel.Margin = this.Padding;
            if (null == this.textBlock)
            {
                this.textBlock = new TextBlock();
                this.textBlock.TextWrapping = TextWrapping.Wrap;
                this.textBlock.TextAlignment = TextAlignment.Center;
                this.textBlock.ClipToBounds = true;
                this.textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                this.textBlock.VerticalAlignment = VerticalAlignment.Center;
            }
            if (null == this.image)
            {
                this.image = new Image();
                this.image.Stretch = Stretch.None;
            }
            if (this.ImageSize != null)
            {
                string[] dim = this.ImageSize.Split(',');
                if (dim.Length == 2)
                {
                    float w, h;
                    if (float.TryParse(dim[0], out w) && float.TryParse(dim[1], out h))
                    {
                        this.image.Width = w;
                        this.image.Height = h;
                        this.image.Stretch = Stretch.Uniform;
                    }
                }
            }
            this.image.Source = this.Image;
            if (null != this.Image)
            {
                if (HorizontalAlignment.Right == this.ImageHorizontalAlignment)
                {
                    this.image.Margin = new System.Windows.Thickness(10, 0, 0, 0);
                    DockPanel.SetDock(this.image, Dock.Right);
                }
                else if (VerticalAlignment.Top == this.ImageVerticalAlignment)
                {
                    this.image.Margin = new System.Windows.Thickness(0, 0, 0, 10);
                    DockPanel.SetDock(this.image, Dock.Top);
                }
                else if (VerticalAlignment.Bottom == this.ImageVerticalAlignment)
                {
                    this.image.Margin = new System.Windows.Thickness(0, 10, 0, 0);
                    DockPanel.SetDock(this.image, Dock.Bottom);
                }
                else if (HorizontalAlignment.Center == this.ImageHorizontalAlignment
                    && VerticalAlignment.Center == this.ImageVerticalAlignment)
                {
                    this.image.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                    DockPanel.SetDock(this.image, Dock.Right);
                }
                else
                {
                    this.image.Margin = new System.Windows.Thickness(0, 0, 10, 0);
                    DockPanel.SetDock(this.image, Dock.Left);
                }
            }
            else
            {
                this.image.Margin = new System.Windows.Thickness(0, 0, 0, 0);
                DockPanel.SetDock(this.image, Dock.Right);
            }
            if (panel.Children.Count < 2)
            {
                panel.Children.Add(this.image as UIElement);
                panel.Children.Add(this.textBlock as UIElement);
            }
            this.Content = panel;
        }

        protected override bool IsEnabledCore
        {
            get
            {
                return base.IsEnabledCore && AllowUserInput;
            }
        }

        protected Image image;
        protected TextBlock textBlock;

        private static void OnAllowUserInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(UIElement.IsEnabledProperty);
        }
       
        private bool touchedDown;
    }
}
