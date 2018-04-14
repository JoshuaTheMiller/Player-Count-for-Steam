using System;
using System.IO;
using Xamarin.Forms;

namespace SteamStatsApp
{
    public class ByteToImageFieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                retSource = ConvertToImageSource(imageAsBytes);
            }
            return retSource;
        }

        private static ImageSource ConvertToImageSource(byte[] imageAsBytes)
        {
            ImageSource source;

            //using (var stream = new MemoryStream(imageAsBytes))
            //{
                //TODO: Determine if this is going to hold on to the stream indefinitely.
                source = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            //}

            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
