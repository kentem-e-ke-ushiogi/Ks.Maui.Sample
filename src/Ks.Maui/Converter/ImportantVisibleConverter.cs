using System.Globalization;

namespace Ks.Maui.Converter
{
    internal class ImportantVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return false;
            // モデル値
            bool important = (bool)values[0];
            // 重要なお知らせダイアログか
            bool importantonly = (bool)values[1];
            // 重要なお知らせダイアログでは重要ラベルは表示しない
            return important && !importantonly;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
