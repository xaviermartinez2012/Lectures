using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Cecs475.War.Model;
using System.Windows.Media.Imaging;

namespace Cecs475.War.Application {
	public class CardImageConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			try {
				Card c = value as Card;
				if (c != null) {
					string src = c.ToString().ToLower().Replace(' ', '_');
					return new BitmapImage(new Uri("/Resources/" + src + ".png", UriKind.Relative));
				}
				else {
					return new BitmapImage(new Uri("/Resources/back.png", UriKind.Relative));
				}
			}
			catch (Exception e) {
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
