using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAB330_Scruff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDP : MasterDetailPage
    {
        public MDP()
        {
            // test
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected; //Add a listening for when a navigation item is selected
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MDPMenuItem; //Get the selected item
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType); //Create an instance of the selected page
            page.Title = item.Title;

            Detail = new NavigationPage(page); //Navigate to the selected page
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null; //Deselect the item after navigating to the page
        }
    }
}