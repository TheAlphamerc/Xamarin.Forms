using System;

namespace Xamarin.Forms
{
	[Flags]
	[TypeConverter(typeof(FontAttributesConverter))]
	public enum FontAttributes
	{
		None = 0,
		Bold = 1 << 0,
		Italic = 1 << 1
	}

	[Xaml.TypeConversion(typeof(FontAttributes))]
	public sealed class FontAttributesConverter : TypeConverter
	{

		public override object ConvertFromInvariantString(string value)
		{
			if (string.IsNullOrEmpty(value))
				return FontAttributes.None;

			FontAttributes attributes = FontAttributes.None;
			for (int i = 0; i < value.Split(',').Length; i++) {
				var part = value.Split(',')[i].Trim();
				if (Enum.TryParse(value, true, out FontAttributes attr)) {
					attributes |= attr;
					continue;
				}
				if (part.Equals("normal", StringComparison.OrdinalIgnoreCase))
					continue;
				if (part.Equals("oblique", StringComparison.OrdinalIgnoreCase)) {
					attributes |= FontAttributes.Italic;
					continue;
				}
				throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(FontAttributes)));
			}
			return attributes;
		}
	}
}