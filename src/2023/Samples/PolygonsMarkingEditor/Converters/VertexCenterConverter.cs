using System;
using System.Globalization;
using MVVM.Converters;
using PolygonsMarkingEditor.Tools;

namespace PolygonsMarkingEditor.Converters;

public class VertexCenterConverter : ConverterBase<VertexCenterConverter>
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not double d) return double.NaN;

        return d + Extensions.ElementSize;
    }
}