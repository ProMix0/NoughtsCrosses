using System.Windows.Controls;

namespace NoughtsCrosses.WpfClient
{
    public interface IWpfView
    {
        TextBlock StateLine { get; }
        Grid Field { get; set; }
    }
}