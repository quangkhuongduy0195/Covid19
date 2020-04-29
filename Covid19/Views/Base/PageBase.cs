using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Covid19.Views.Base
{
    public abstract class PageBase : ContentPage
    {
        protected PageBase()
        {
            /* ==================================================================================================
             * Binding the base property 'Page.IsBusy' with ViewModelBase.IsBusy
             * By default, all of our pages must inherit this.
             * ================================================================================================*/
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
            Prism.Mvvm.ViewModelLocator.SetAutowireViewModel(this, true);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(UseSafeArea);
        }

        #region SelectedIconImageSource

        public static BindableProperty AlternateIconImageSourceProperty = BindableProperty.Create(nameof(AlternateIconImageSource), typeof(ImageSource), typeof(PageBase), default(ImageSource), BindingMode.Default);

        /// <summary>
        /// use for children of tabbed page <para/>
        /// Be careful on android: "AlternateIconImageSource" vs "IconImageSource" are alternated its value together on selected a tab
        /// </summary>
        public ImageSource AlternateIconImageSource
        {
            get { return (ImageSource)GetValue(AlternateIconImageSourceProperty); }
            set { SetValue(AlternateIconImageSourceProperty, value); }
        }

        #endregion

        public virtual bool UseSafeArea => true;

        #region SettleButtonStyle
        public static BindableProperty SettleButtonStyleProperty = BindableProperty.Create(nameof(SettleButtonStyle), typeof(Style), typeof(PageBase), default(Style), BindingMode.OneWay);
        public Style SettleButtonStyle
        {
            get { return (Style)GetValue(SettleButtonStyleProperty); }
            set { SetValue(SettleButtonStyleProperty, value); }
        }
        #endregion

        /* ==================================================================================================
         * implement other base logics of a view, anythings you want.
         * ================================================================================================*/
    }
}
