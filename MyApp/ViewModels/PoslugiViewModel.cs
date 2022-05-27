

using MyApp.Models;
using MyApp.Services;

using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;


namespace MyApp.ViewModels
{
    public class PoslugiViewModel : BindableBase, INavigatedAware, IActiveAware
    {

        private readonly INavigationService _navigationService;
        private readonly IChangeThemeService _changeThemeService;


        public PoslugiViewModel(INavigationService navigationService,
                                IChangeThemeService changeThemeService)
        {
            _navigationService = navigationService;
            _changeThemeService = changeThemeService;

            _colectionView = new ObservableRangeCollection<PoslugiModel>();

            Image_Plitka_List = "plitka.png";
            Text_Plitka_List = "Плитками";
            IconTab = "molnia.png";

            GridItemsLayout grid = new GridItemsLayout(ItemsLayoutOrientation.Vertical) { Span = 1 };
            Layout_Plitka_List = grid;

        }


        public event EventHandler IsActiveChanged;


        #region Public property


        private string _image_Plitka_List;
        public string Image_Plitka_List
        {
            get => _image_Plitka_List;
            set => SetProperty(ref _image_Plitka_List, value);
        }


        private string _text_Plitka_List;
        public string Text_Plitka_List
        {
            get => _text_Plitka_List;
            set => SetProperty(ref _text_Plitka_List, value);
        }


        private GridItemsLayout _layout_Plitka_List;
        public GridItemsLayout Layout_Plitka_List
        {
            get => _layout_Plitka_List;
            set => SetProperty(ref _layout_Plitka_List, value);
        }


        private PoslugiModel _selectClick;
        public PoslugiModel SelectClick
        {
            get => _selectClick;
            set
            {
                SetProperty(ref _selectClick, value);
                SelectedClick(_selectClick);
            }
        }


        private string searchItem;
        public string SearchItem
        {
            get => searchItem;
            set
            {
                SetProperty(ref searchItem, value);
                Search();
            }
        }


        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, IsActiveTabAsync);
        }


        private string _iconTab;
        public string IconTab
        {
            get => _iconTab;
            set => SetProperty(ref _iconTab, value);
        }


        private ObservableCollection<PoslugiModel> _colectionView;
        public ObservableCollection<PoslugiModel> ColectionView
        {
            get => _colectionView;
            set => SetProperty(ref _colectionView, value);
        }

        public DelegateCommand Plitka_List_Btn => new DelegateCommand(Plitka_List);
        public DelegateCommand SearchClick => new DelegateCommand(Search);

        #endregion


        private void SelectedClick(PoslugiModel index)
        {
            Console.WriteLine(index.name);
        }

        private void Plitka_List()
        {
            if (_text_Plitka_List == "Плитками")
            {
                GridItemsLayout grid = new GridItemsLayout(ItemsLayoutOrientation.Vertical)
                {
                    Span = 2,
                };

                Layout_Plitka_List = grid;
                Image_Plitka_List = "list.png";
                Text_Plitka_List = "Списком";
            }
            else
            {
                GridItemsLayout grid = new GridItemsLayout(ItemsLayoutOrientation.Vertical)
                {
                    Span = 1,
                };

                Layout_Plitka_List = grid;
                Image_Plitka_List = "plitka.png";
                Text_Plitka_List = "Плитками";
            }
        }

        private void Search()
        {
            if (SearchItem.Length < 1)
            {
                ColectionView.Clear();
                PoslugiList();
                return;
            }

            string temp = SearchItem.ToLower();
            ObservableCollection<PoslugiModel> collection = new ObservableCollection<PoslugiModel>();
            int m = SearchItem.Length < 2 ? 0 : SearchItem.Length - 1;

            for (int i = 0; i < ColectionView.Count; i++)
            {
                string s = ColectionView[i].name.ToLower();

                for (int j = m; j < temp.Length; j++)
                {
                    if (s[j] == temp[j])
                    {
                        collection.Add(ColectionView[i]);
                        continue;
                    }
                }
            }

            if (collection.Count > 0)
            {
                ColectionView.Clear();
                ColectionView = collection;
            }
            else
            {
                string S = SearchItem.Remove(searchItem.Length - 1, 1);
                Console.WriteLine(S);
                SearchItem = S;
            }
        }

        private void PoslugiList()
        {
            ColectionView.Add(new PoslugiModel { name = "Covid-сертифікати", image = "smile.png" });
            ColectionView.Add(new PoslugiModel { name = "єПідтримка", image = "hands.png" });
            ColectionView.Add(new PoslugiModel { name = "Штрафи ПДР", image = "pliceCar.png" });
            ColectionView.Add(new PoslugiModel { name = "Заміна водійського", image = "car.png" });
            ColectionView.Add(new PoslugiModel { name = "Податки ФОП", image = "portfel.png" });
            ColectionView.Add(new PoslugiModel { name = "Виконавчі провадження", image = "palace.png" });
            ColectionView.Add(new PoslugiModel { name = "Петиції", image = "galochka.png" });
        }

        private async void IsActiveTabAsync()
        {
            if (IsActive)
            {
                _changeThemeService.SetTheme(Enums.Themes.Poslugi);
                IconTab = "molniaBlack.png";
                await Task.Delay(50);
                ColectionView.Clear();
                PoslugiList();
            }
            else
            {
                IconTab = "molnia.png";
                ColectionView.Clear();
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _changeThemeService.SetTheme(Enums.Themes.Poslugi);
        }
    }
}
