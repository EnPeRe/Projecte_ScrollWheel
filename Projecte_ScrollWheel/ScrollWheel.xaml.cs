using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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

            //transY = Collection.Children.ElementAt(1).Y;
        }

        private async void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            transY = Collection.Children.ElementAt(1).Y;

            //var first = Collection.Children.FirstOrDefault();
            //Collection.Children.Add(new Label() { Text = ((Label)first).Text });

            Collection.Children.ToList().ForEach(async i =>
                {
                    await i.TranslateTo(i.X, -transY, 1000);
                    //if (ind == 1)
                    //{
                    //    var prev = Collection.Children.ElementAt(1);
                    //    await i.TranslateTo(i.X, I, 1000);
                    //}
                });

            await Task.Delay(3000);

            //Collection.Children.Remove(first);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Collection.Children.ToList().ForEach(async i =>
            {
                //await i.TranslateTo(i.X, -transY, 1000);
                var ind = Collection.Children.IndexOf(i);
                if (ind == 1)
                {
                    var prev = Collection.Children.ElementAt(1);
                    await i.TranslateTo(i.X, i.Y-1, 1);
                }
            });
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    //Content.TranslationX =
                    //  Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - Application.Current.MainPage.Width));
                    Content.TranslationY =
                      Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(Content.Height - Application.Current.MainPage.Height));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = Content.TranslationX;
                    y = Content.TranslationY;
                    break;
            }
        }
    }
}