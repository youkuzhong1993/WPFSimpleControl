﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControl
{
    /// <summary>
    /// Border which allows Clipping to its border.
    /// Useful especially when you need to clip to round corners.
    /// </summary>
    public class ClipBorder2 : Border
    {
        protected override void OnRender(DrawingContext dc)
        {
            OnApplyChildClip();
            base.OnRender(dc);
        }

        public override UIElement Child
        {
            get
            {
                return base.Child;
            }
            set
            {
                if (this.Child != value)
                {
                    if (this.Child != null)
                    {
                        // Restore original clipping of the old child
                        this.Child.SetValue(UIElement.ClipProperty, oldClip);
                    }

                    if (value != null)
                    {
                        // Store the current clipping of the new child
                        oldClip = value.ReadLocalValue(UIElement.ClipProperty);
                    }
                    else
                    {
                        // If we dont set it to null we could leak a Geometry object
                        oldClip = null;
                    }

                    base.Child = value;
                }
            }
        }

        protected virtual void OnApplyChildClip()
        {
            UIElement child = this.Child;
            if (child != null)
            {
                // Get the geometry of a rounded rectangle border based on the BorderThickness and CornerRadius
                clipGeometry = GeometryHelper.GetRoundRectangle(new Rect(Child.RenderSize), this.BorderThickness, this.CornerRadius);
                child.Clip = clipGeometry;
            }
        }

        private Geometry clipGeometry = null;
        private object oldClip;
    }
}
