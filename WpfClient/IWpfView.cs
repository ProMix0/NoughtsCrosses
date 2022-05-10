using System.Windows.Controls;

namespace WpfClient
{
    public interface IWpfView
    {
        TextBlock StateLine { get; }
        Grid Field { get; set; }
    }
}