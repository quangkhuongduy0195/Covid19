using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19.Infrastructure.Networking.Ioc;
using Covid19.Services.Interfaces;
using Covid19.ViewModels;
using Prism.Behaviors;
using Prism.Common;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Xamarin.Forms;

namespace Covid19.Extensions
{
    public static class NavigationExtentions
    {
        static readonly ILogService _logger = Ioc.Current.Resolve<ILogService>();
        #region Push
        /// <summary>
        /// navigate to a viewmodel async without navigation parameters
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<INavigationResult> PushAsync<TViewModel>(this INavigationService navigationService, bool animated = true) where TViewModel : ViewModelBase
        {
            return PushAsync<TViewModel>(navigationService, null, animated);
        }

        /// <summary>
        /// navigate to a view model async
        /// </summary>
        /// <returns>The to async.</returns>
        /// <param name="parameters">Parameters.</param>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<INavigationResult> PushAsync<TViewModel>(this INavigationService navigationService, INavigationParameters parameters, bool animated = true) where TViewModel : ViewModelBase
        {
            return PushAsync(navigationService, typeof(TViewModel).Name, parameters, animated);
        }

        /// <summary>
        /// navigate to a view model async
        /// </summary>
        /// <returns>The to async.</returns>
        /// <param name="parameters">Parameters.</param>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static async Task<INavigationResult> PushAsync(this INavigationService navigationService, string uri, INavigationParameters parameters = null, bool animated = true)
        {
            _logger?.Log(uri);
            var navigationResult = await navigationService.NavigateAsync(uri, parameters, animated: animated);
            return navigationResult;
        }

        #endregion

        #region Push modal
        public static Task<INavigationResult> PushModalAsync<TViewModel>(this INavigationService navigationService, bool animated = true) where TViewModel : ViewModelBase
        {
            return PushModalAsync(navigationService, typeof(TViewModel).Name, null, animated);
        }

        public static Task<INavigationResult> PushModalAsync<TViewModel>(this INavigationService navigationService, INavigationParameters parameters, bool animated = true) where TViewModel : ViewModelBase
        {
            return PushModalAsync(navigationService, typeof(TViewModel).Name, parameters, animated);
        }

        public static Task<INavigationResult> PushModalAsync(this INavigationService navigationService, string uri, INavigationParameters parameters = null, bool animated = true)
        {
            _logger?.Log(uri);
            return navigationService.NavigateAsync(uri, parameters, true, animated);
        }
        #endregion

        #region tabbed - others

        public static Task<INavigationResult> ChangeTabAsync<TViewModel>(this INavigationService navigationService, INavigationParameters parameters = null)
            where TViewModel : ViewModelBase
        {
            _logger?.Log();
            var tabName = typeof(TViewModel).Name;
            return navigationService.SelectTabAsync(tabName, parameters);
        }

        /// <summary>
        /// This is equivalent to INavigationService.GetNavigationUriPath()
        /// </summary>
        /// <returns>The am i.</returns>
        public static string WhereAmI(this INavigationService navigationService)
        {
            _logger?.Log();
            return navigationService.GetNavigationUriPath();
        }
        #endregion

        public static Task<INavigationResult> GoBackToRootInvokeCurrentPageOnNavigatedToAsync(this INavigationService navigationService, INavigationParameters parameters = null)
        {
            return GoBackToRootInvokeCurrentPageOnNavigatedToInternal(parameters);
        }

        public static async Task<INavigationResult> GoBackToRootInvokeCurrentPageOnNavigatedToInternal(INavigationParameters parameters)
        {
            _logger?.Log();
            var result = new NavigationResult();
            try
            {
                if (parameters == null)
                    parameters = new NavigationParameters();

                ((INavigationParametersInternal)parameters).Add("__NavigationMode", NavigationMode.Back); // __NavigationMode is hardcode of Prism.Navigation.KnownInternalParameters.NavigationMode

                Page page = PageUtilities.GetCurrentPage(Application.Current.MainPage);
                var canNavigate = await PageUtilities.CanNavigateAsync(page, parameters);
                if (!canNavigate)
                {
                    result.Exception = new NavigationException(NavigationException.IConfirmNavigationReturnedFalse, page);
                    return result;
                }

                List<Page> pagesToDestroy = page.Navigation.NavigationStack.ToList(); // get all pages to destroy
                pagesToDestroy.Reverse(); // destroy them in reverse order
                var root = pagesToDestroy.Last();
                pagesToDestroy.Remove(root); //don't destroy the root page

                await page.Navigation.PopToRootAsync();

                foreach (var destroyPage in pagesToDestroy)
                {
                    PageUtilities.OnNavigatedFrom(destroyPage, parameters);
                    PageUtilities.DestroyPage(destroyPage);
                }

                Page currentPage = PageUtilities.GetCurrentPage(root);

                PageUtilities.OnNavigatedTo(currentPage, parameters);

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                return result;
            }
        }

        public static Task<INavigationResult> GoBackToTargetAsync<T>(this INavigationService navigationService, INavigationParameters parameters = null)
        {
            return GoBackToTargetInternal<T>(parameters);
        }

        public static async Task<INavigationResult> GoBackToTargetInternal<T>(INavigationParameters parameters)
        {
            _logger?.Log();
            var result = new NavigationResult();
            try
            {
                if (parameters == null)
                    parameters = new NavigationParameters();

                ((INavigationParametersInternal)parameters).Add("__NavigationMode", NavigationMode.Back); // __NavigationMode is hardcode of Prism.Navigation.KnownInternalParameters.NavigationMode

                var currentPage = PageUtilities.GetCurrentPage(Application.Current.MainPage);

                NavigationPage navigationPage = null;
                var tmp = currentPage;
                while (tmp.Parent != null)
                {
                    if (tmp.Parent is NavigationPage)
                    {
                        navigationPage = tmp.Parent as NavigationPage;
                        break;
                    }
                    else tmp = tmp.Parent as Page;
                }

                if (navigationPage == null)
                {
                    result.Exception = new NavigationException("Require NavigationPage", currentPage);
                    return result;
                }

                var canNavigate = await PageUtilities.CanNavigateAsync(currentPage, parameters);
                if (!canNavigate)
                {
                    result.Exception = new NavigationException(NavigationException.IConfirmNavigationReturnedFalse, currentPage);
                    return result;
                }

                // Cache the stack to avoid mutation.
                var stack = currentPage.Navigation.NavigationStack.ToList();
                Page root = navigationPage.RootPage; // Initial root value.

                var pagesToDestroy = new List<Page>();

                // For stack, A > B > C > D. We want to navigate from D to B.
                // A > B > D.
                // The remove order is D > C.
                for (int i = stack.Count - 1; i >= 1; i--) // i = 0 is root page. we don't remove it.
                {
                    var page = stack[i];
                    if (page.BindingContext.GetType() == typeof(T)) // Assume target key is ViewModel's name.
                    {
                        root = page; // Root is B.
                        break;
                    }
                    pagesToDestroy.Add(page);
                }

                // Temporary remove prism navigation aware
                var systemGoBackBehavior = navigationPage.Behaviors.FirstOrDefault(p => p is NavigationPageSystemGoBackBehavior);
                if (systemGoBackBehavior != null)
                    navigationPage.Behaviors.Remove(systemGoBackBehavior);

                for (int i = 1; i < pagesToDestroy.Count; i++) // Skip page D, remove page C,
                    currentPage.Navigation.RemovePage(pagesToDestroy[i]);

                await currentPage.Navigation.PopAsync(); // Navigate from D to B.

                foreach (var destroyPage in pagesToDestroy) // D, C OnNavigatedFrom and Destroy
                {
                    PageUtilities.OnNavigatedFrom(destroyPage, parameters);
                    PageUtilities.DestroyPage(destroyPage);
                }
                PageUtilities.OnNavigatedTo(root, parameters); // B OnNavigatedTo 

                // Re-add prism navigation aware
                if (systemGoBackBehavior != null)
                    navigationPage.Behaviors.Add(systemGoBackBehavior);

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                return result;
            }
        }
    }
}
