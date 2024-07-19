using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace FixPro.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private bool _isHandlingFocus;

        public CustomEntryRenderer() : base()
        {
            // Constructor logic if needed
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.EditingDidBegin += OnEditingDidBegin;
                Control.EditingDidEnd += OnEditingDidEnd;
            }
        }

        private void OnEditingDidBegin(object sender, EventArgs e)
        {
            if (_isHandlingFocus) return;
            _isHandlingFocus = true;

            // Handle focus gained event
            (Element as IElementController)?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);

            _isHandlingFocus = false;
        }

        private void OnEditingDidEnd(object sender, EventArgs e)
        {
            if (_isHandlingFocus) return;
            _isHandlingFocus = true;

            // Handle focus lost event
            (Element as IElementController)?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);

            _isHandlingFocus = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.EditingDidBegin -= OnEditingDidBegin;
                Control.EditingDidEnd -= OnEditingDidEnd;
            }
            base.Dispose(disposing);
        }
    }
}