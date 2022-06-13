using CV19.Models.Decanat;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace CV19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GroupsCollection_OnFilter(object sender, FilterEventArgs e)
        {
            Group group = e.Item as Group;
            var filter_text = GroupNameFilterText.Text;
            if (group != null)
            {
                if (group.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase) ||
                   (group.Description != null && group.Description.Contains(filter_text, StringComparison.OrdinalIgnoreCase))) 
                    e.Accepted = true;
                else 
                    e.Accepted = false;
            }

            //if (!(e.Item is Group group)) return;
            //if (group.Name is null) return;

            //var filter_text = GroupNameFilterText.Text;
            //if (filter_text.Length == 0) return;

            //if(group.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
            //if(group.Description != null && group.Description.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

            //e.Accepted = false;
        }

        private void OnGroupsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var text_box = (TextBox)sender;
            var collection = (CollectionViewSource)text_box.FindResource("GroupsCollection");
            collection.View.Refresh();
        }
    }
}
