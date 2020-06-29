using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Projecte_ScrollWheel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScrollWheel : ContentView
    {
        public int HighlightedItem;
        public IList<View> CollectionChildren;
        public double transY;

        double x, y;

        public ScrollWheel()
        {
            InitializeComponent();

            // Set PanGestureRecognizer.TouchPoints to control the
            // number of touch points needed to pan
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            HighlightedItem = 1;

            var list1 = new List<View>()
            {
                new Label() { Text = "Item" },
                new Label() { Text = "Item_" },
                new Label() { Text = "Item_3" }
            };

            var list2 = new List<View>()
            {
                new Label() { Text = "Item" },
                new Label() { Text = "Item_" },
                new Label() { Text = "Item_3" }
            };

            var list3 = new List<View>()
            {
                new Label() { Text = "Item" },
                new Label() { Text = "Item_" },
                new Label() { Text = "Item_3" }
            };

            foreach (var item in list1)
            {
                Collection.Children.Add(item);
            }

            foreach (var item in list2)
            {
                Collection.Children.Add(item);
            }

            foreach (var item in list3)
            {
                Collection.Children.Add(item);
            }
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    //Content.TranslationX =
                    //  Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - Application.Current.MainPage.Width));
                    Collection.Children.ForEach(i =>
                    {
                        i.TranslationY = y + e.TotalY;
                    });
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = Content.TranslationX;
                    y = Collection.Children.FirstOrDefault().TranslationY;
                    break;
            }
        }
    }
}