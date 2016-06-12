using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace IntDesignControls
{
    public class HyperlinkTextbox : PreviewTextBox, ISupportUserInput
    {
        public HyperlinkTextbox()
        {
            this.textChangeNotifier = new PropertyChangeNotifier(this, "Text");
            this.textChangeNotifier.ValueChanged += new EventHandler(OnTextChanged);            
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var textBox = sender as HyperlinkTextbox;
            Uri uri;
            if (Uri.TryCreate(textBox.Text, UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                UpdateStyle(textBox);
            }
            else
            {
                textBox.TextDecorations = null;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void UpdateStyle(HyperlinkTextbox textBox)
        {
            TextDecorationCollection myCollection = new TextDecorationCollection();
            TextDecoration underLine = new TextDecoration();
            underLine.Location = TextDecorationLocation.Underline;

            // Set the solid color brush.
            underLine.Pen = new Pen(Brushes.Red, 2);
            underLine.PenThicknessUnit = TextDecorationUnit.FontRecommended;

            // Set the underline decoration to the text block.
            myCollection.Add(underLine);
            textBox.TextDecorations = myCollection;
            textBox.Foreground = Brushes.Blue;
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            Uri uri;
            if (Uri.TryCreate(this.Text, UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                Process.Start(new ProcessStartInfo(uri.AbsoluteUri));
            }
        }
        
        public bool AllowUserInput
        {
            get { return (bool)GetValue(AllowUserInputProperty); }
            set { SetValue(AllowUserInputProperty, value); }
        }

        public static readonly DependencyProperty AllowUserInputProperty = DependencyProperty.Register("AllowUserProperty", typeof(bool),
            typeof(HyperlinkTextbox), new PropertyMetadata(true, new PropertyChangedCallback(OnAllowUserInputChanged)));

        private static void OnAllowUserInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(UIElement.IsEnabledProperty);
        }

        private PropertyChangeNotifier textChangeNotifier;
    }
}
