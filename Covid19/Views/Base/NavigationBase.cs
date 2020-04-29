using System;
using Xamarin.Forms;

namespace Covid19.Views.Base
{
    public class NavigationBase<TView> : NavigationPage
    {
        public NavigationBase(TView root) : base(root as Page)
        {
            var page = root as Page;

            Title = page.Title;
            IconImageSource = page.IconImageSource;
        }
    }
}
