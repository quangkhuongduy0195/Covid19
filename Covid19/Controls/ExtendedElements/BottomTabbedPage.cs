using System;
using Covid19.Styles;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Covid19.Controls.ExtendedElements
{
    public class BottomTabbedPage : Xamarin.Forms.TabbedPage
    {
        public BottomTabbedPage()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSwipePagingEnabled(false);
            On<Android>().SetIsLegacyColorModeEnabled(false);
            On<Android>().SetIsSmoothScrollEnabled(false);

            On<iOS>().SetTranslucencyMode(TranslucencyMode.Opaque);

            BarBackgroundColor = Colors.BarBackgroundColor;
            SelectedTabColor = Color.Red;//Colors.SelectedTabColor;
            UnselectedTabColor = Colors.UnselectedTabColor;
        }
    }
}
