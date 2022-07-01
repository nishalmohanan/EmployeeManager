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

namespace EmployeeManager.Controls
{
    /// <summary>
    /// Interaction logic for PagerUserControl.xaml
    /// </summary>
    public partial class Pager: UserControl
    {
        private static readonly DependencyProperty MoveFirstDependencyProperty = DependencyProperty.Register("MoveFirst", typeof(ICommand), typeof(Pager), new PropertyMetadata(null));
        private static readonly DependencyProperty MoveLastDependencyProperty = DependencyProperty.Register("MoveLast", typeof(ICommand), typeof(Pager), new PropertyMetadata(null));
        private static readonly DependencyProperty MoveNextDependencyProperty = DependencyProperty.Register("MoveNext", typeof(ICommand), typeof(Pager), new PropertyMetadata(null));
        private static readonly DependencyProperty MovePreviousDependencyProperty = DependencyProperty.Register("MovePrevious", typeof(ICommand), typeof(Pager), new PropertyMetadata(null));
        private static readonly DependencyProperty CurrentPageDependencyProperty = DependencyProperty.Register("CurrentPage", typeof(long), typeof(Pager), new PropertyMetadata(null));

        public ICommand MoveFirst { get { return (ICommand)GetValue(MoveFirstDependencyProperty); }  set { SetValue(MoveFirstDependencyProperty, value); } }
        public ICommand MoveLast { get { return (ICommand)GetValue(MoveLastDependencyProperty); } set { SetValue(MoveLastDependencyProperty, value); } }
        public ICommand MoveNext { get { return (ICommand)GetValue(MoveNextDependencyProperty); } set { SetValue(MoveNextDependencyProperty, value); } }
        public ICommand MovePrevious { get { return (ICommand)GetValue(MovePreviousDependencyProperty); } set { SetValue(MovePreviousDependencyProperty, value); } }
        public long  CurrentPage{ get { return (long)GetValue(CurrentPageDependencyProperty); } set { SetValue(CurrentPageDependencyProperty, value); } }
        public Pager()
        {
            InitializeComponent();
        }
    }
}
