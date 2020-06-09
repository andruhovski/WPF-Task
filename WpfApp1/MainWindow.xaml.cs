using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using DataLayer;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CardsCatalogContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new CardsCatalogContext();
            _context.Cards.Load();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var root = BuildTree(-1);
            TreeView1.Items.Add(root);
        }

        private CardItem BuildTree(int i)
        {
            var card = (i>0) 
                ? _context.Cards.Local.First(c => c.Id == i) 
                : new Card
                {
                    Id = i,
                    Header = "Root"
                };
            
            var cardItem = new CardItem
            {
                Id = card.Id,
                Header = card.Header
            };

            var items = _context.Cards.Local.Where(c => c.Parent == cardItem.Id);
            foreach (var item in items)
            {
                cardItem.Items.Add(BuildTree(item.Id));
            }
            return cardItem;
        }
    }

    public class CardItem
    {
        public CardItem()
        {
            Items = new ObservableCollection<CardItem>();
        }

        public int Id { get; set; }
        public string Header { get; set; }

        public ObservableCollection<CardItem> Items { get; set; }
    }
}
