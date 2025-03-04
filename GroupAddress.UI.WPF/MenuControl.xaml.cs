using GroupAddress.UI.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupAddress.UI.WPF
{
    /// <summary>
    /// Interaktionslogik für MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {

        public static readonly DependencyProperty TestProperty =
            DependencyProperty.Register(
                name: "Test",
                propertyType: typeof(string),
                ownerType: typeof(MenuControl),
                typeMetadata: new FrameworkPropertyMetadata(defaultValue: "bbblublubblub"));

        public string Test
        {
            get => (string)GetValue(TestProperty);
            set => SetValue(TestProperty, value);
        }
        public MenuControl()
        {
            InitializeComponent();

            DataContext = new MenuViewModel();

            var bindingViewMode = new Binding("Test") { Mode = BindingMode.TwoWay };
            this.SetBinding(TestProperty, bindingViewMode);

        }


    }
}
