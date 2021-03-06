﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FunWithFrames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondaryPage : Page
    {
        public SecondaryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // We don't check for null here because we only visit this page if the MainPage
            // brought us here. Get the visual from the window content and pull it back to
            // our current page.
            var visual = 
                ElementCompositionPreview.GetElementChildVisual(
                    Window.Current.Content);
            ElementCompositionPreview.SetElementChildVisual(
                Window.Current.Content, 
                null);
            ElementCompositionPreview.SetElementChildVisual(this, visual);

            // Get the Compositor object.
            var compositor = visual.Compositor;
            // Animate our visual to a different spot on the page.
            var animation = compositor.CreateVector3KeyFrameAnimation();
            animation.InsertKeyFrame(0.0f, visual.Offset);
            animation.InsertKeyFrame(
                1.0f, 
                new Vector3(
                    (float)Frame.ActualWidth - 150, 
                    visual.Offset.Y, 
                    visual.Offset.Z));
            animation.Duration = TimeSpan.FromMilliseconds(1500);

            visual.StartAnimation(nameof(Visual.Offset), animation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Before going back to the MainPage, remove the visual from thsi page and place
            // it on the window content so it survives the page transition.
            var visual = ElementCompositionPreview.GetElementChildVisual(this);
            ElementCompositionPreview.SetElementChildVisual(this, null);
            ElementCompositionPreview.SetElementChildVisual(
                Window.Current.Content, 
                visual);

            // Go back to our MainPage.
            Frame.GoBack();
        }
    }
}
