namespace Ks.Maui
{
    internal static class KsMauiUtilty
    {
        /// <summary>カレントページを取得</summary>
        public static Page GetCurrentPage()
        {
            if (Application.Current!.MainPage is Shell shell)
            {
                return shell.CurrentPage;
            }

            if (Application.Current.MainPage is NavigationPage nav)
            {
                return nav.CurrentPage;
            }

            if (Application.Current.MainPage is TabbedPage tabbed)
            {
                return tabbed.CurrentPage;
            }

            return Application.Current.MainPage!;
        }
    }
}
