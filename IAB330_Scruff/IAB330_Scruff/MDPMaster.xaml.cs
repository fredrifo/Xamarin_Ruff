using IAB330_Scruff.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAB330_Scruff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MDPMaster : ContentPage
    {
        public ListView ListView;

        public MDPMaster()
        {
            InitializeComponent();
            navigationHeaderLabel.Text = User.currentUser.email;
            BindingContext = new MDPMasterViewModel();
            ListView = MenuItemsListView;

            // 
        }

        public class MDPMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MDPMenuItem> MenuItems { get; set; }
            

            public MDPMasterViewModel()
            {
                
                MenuItems = new ObservableCollection<MDPMenuItem>(new[]
                {
                    new MDPMenuItem { Id = 0, Title = "Home", TargetType = typeof(SearchResults)},
                    new MDPMenuItem { Id = 2, Title = "My Animals", TargetType = typeof(MyAds)},
                    new MDPMenuItem { Id = 3, Title = "Profile", TargetType = typeof(Profile)},
                    new MDPMenuItem { Id = 6, Title = "Settings", TargetType = typeof(Settings)},
                    new MDPMenuItem { Id = 5, Title = "Logout", TargetType = typeof(Login)}

                });

                
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

    }
}