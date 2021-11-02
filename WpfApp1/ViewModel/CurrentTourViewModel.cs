using BL;
using Model_Accesse.Model;
using NLog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.ViewModel
{
    public class CurrentTourViewModel : INotifyPropertyChanged
    {
        Logger log = LogManager.GetCurrentClassLogger();
        private TourLogic DBRequests;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CurrentTourViewModel(TourLogic dBRequests)
        {
            log.Info("{component} starting up", nameof(CurrentTourViewModel));
            DBRequests = dBRequests;


            AktivTours = new ObservableCollection<Tour>();
            OnPropertyChanged(nameof(AktivTours));
            Update();
            UpdateTours();
            log.Info("{component} initialized", nameof(CurrentTourViewModel));
        }

        private string _selectedTourName;

        public event PropertyChangedEventHandler PropertyChanged;

        private void Update()
        {
            if (SelectedTourName != null)
            {
                string[,] tourList = DBRequests.RequestTourUpdate();
                for (int i = 0; i < (tourList.GetUpperBound(0) + 1); i++)
                {
                    if (tourList[i, 0] == SelectedTourName)
                    {
                        AktivTour = new ObservableCollection<Tour>();
                        AktivTour.Add(new Tour()
                        {
                            Name = tourList[i, 0],
                            description = tourList[i, 1],
                            routeInfo = tourList[i, 2],
                            distance = tourList[i, 3]
                        });
                        ImageSource = tourList[i, 2];
                        break;// Tour Found End search
                    }


                }
                OnPropertyChanged(nameof(AktivTour));
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        private void UpdateTours()
        {
            string[,] tourList = DBRequests.RequestTourUpdate();
            AktivTours = new ObservableCollection<Tour>();
            for (int i = 0; i < (tourList.GetUpperBound(0) + 1); i++)
            {
                AktivTours.Add(new Tour()
                {
                    Name = tourList[i, 0],
                    description = tourList[i, 1],
                    routeInfo = tourList[i, 2],
                    distance = tourList[i, 3]
                });
            }
            OnPropertyChanged(nameof(AktivTours));
        }
        public string SelectedTourName
        {
            get
            {
                return _selectedTourName;

            }
            set
            {
                _selectedTourName = value;
                UpdateTours();
                Update();
            }
        }
        public string ImageSource { get; set; } = "https://www.salonlfc.com/wp-content/uploads/2018/01/image-not-found-scaled-1150x647.png";
        public ObservableCollection<Tour> AktivTour
        {
            get;
            set;
        }
        public ObservableCollection<Tour> AktivTours { get; set; }

    }
}
