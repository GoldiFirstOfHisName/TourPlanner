using BL;
using Microsoft.Extensions.Configuration;
using Model_Accesse.Model;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UI;
using UI.ViewModel;

namespace Tourplanner.ViewModel
{

    public class TourViewModel : INotifyPropertyChanged
    {
        public Logger log;
        private TourLogic DBRequests;
        public TourViewModel(TourLogic dBRequests, IConfiguration configuration)
        {

            log = LogManager.Setup()
                       .LoadConfigurationFromSection(configuration, "NLog")
                       .GetCurrentClassLogger();

            log.Info("{component} starting up", nameof(TourViewModel));
            DBRequests = dBRequests;

            AddTourButton = new((_) => { AddTourPressed(); });
            DelTourButton = new((_) => { DeleteTourPressed(); });
            EditTourButton = new((_) => { EditTourPressed(); });
            ExportTourButton = new((_) => { ExportTourPressed(); });
            addTour = new Tour();

            SelectedTour = new Tour();
            Update();
            log.Info("{component} Initialized", nameof(TourViewModel));
        }


        private ICommand openAddCommand { get; set; }
        private ICommand closeAddCommand { get; set; }
        private ICommand openEditCommand { get; set; }
        private ICommand closeEditCommand { get; set; }
        private bool isVisible;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update()
        {
            string[,] tourList = DBRequests.RequestTourUpdate();
            Tours = new ObservableCollection<Tour>();
            for (int i = 0; i < (tourList.GetUpperBound(0) + 1); i++)
            {
                Tours.Add(new Tour()
                {
                    Name = tourList[i, 0],
                    description = tourList[i, 1],
                    routeInfo = tourList[i, 2],
                    distance = tourList[i, 3]
                });
            }
            OnPropertyChanged(nameof(Tours));
            addTour = new();
            OnPropertyChanged(nameof(addTour));
        }
        public async void AddTourPressed()
        {
            //handle unwanted input
            if (addTour.Name != null)
            {
                if (addTour.destination != null && addTour.location != null)
                {
                    addTour = await DBRequests.GetTourData(addTour);
                    DBRequests.AddTour(addTour);
                    Update();
                    return;
                }
                throw (new InputIsEmptyexception("location and/or destination cannot be empty"));
            }
            throw (new InputIsEmptyexception("Name cannot be empty"));
        }
        public void DeleteTourPressed()
        {
            if (SelectedTour != null)
            {
                if (SelectedTour.Name != null)
                {
                    DBRequests.DeleteTour(SelectedTour);
                    Update();
                    return;
                }
                throw (new InputIsEmptyexception("No Name in Selected Tour"));
            }
            throw (new InputIsEmptyexception("Need to Select a Tour"));
        }
        public void EditTourPressed()
        {
            if (addTour.Name != null)
            {
                if (addTour.destination != null && addTour.location != null)
                {
                    if (addTour.OldName != null)
                    {
                        DBRequests.EditTour(addTour);
                        Update();
                        addTour = SelectedTour = null;
                        OnPropertyChanged(nameof(SelectedTour));
                        Update();
                        return;
                    }
                    throw (new InputIsEmptyexception("No Tour for Change Selected"));
                }
                throw (new InputIsEmptyexception("location and/or destination cannot be empty"));
            }
            throw (new InputIsEmptyexception("Name cannot be empty"));
        }
        //ADDING TOUR
        public ICommand OpenAddCommand
        {
            get
            {
                if (openAddCommand == null)
                    openAddCommand = new RelayCommand(OpenAdd);
                return openAddCommand;
            }
        }
        private void OpenAdd(object param)
        {
            IsAddVisible = true;
        }
        public ICommand CloseAddCommand
        {
            get
            {

                if (closeAddCommand == null)
                    closeAddCommand = new RelayCommand(CloseAdd);
                return closeAddCommand;
            }
        }
        private void CloseAdd(object param)
        {
            try
            {
                AddTourPressed();
            }
            catch (Exception e)
            {
                log.Error(e, "Error during Tour Add");
            }

            IsAddVisible = false;
        }
        //EDITING TOUR
        public ICommand OpenEditCommand
        {
            get
            {
                if (openEditCommand == null)
                    openEditCommand = new RelayCommand(OpenEdit);
                return openEditCommand;
            }
        }
        private void OpenEdit(object param)
        {
            addTour = new();
            foreach (var tour in Tours)
            {
                if (SelectedTour.Name == tour.Name)
                {
                    addTour = tour;
                    break;
                }

            }
            addTour.OldName = SelectedTour.Name;
            OnPropertyChanged(nameof(addTour));
            IsEditVisible = true;
        }
        public ICommand CloseEditCommand
        {
            get
            {

                if (closeEditCommand == null)
                    closeEditCommand = new RelayCommand(CloseEdit);
                return closeEditCommand;
            }
        }
        private void CloseEdit(object param)
        {
            try
            {
                EditTourPressed();
            }
            catch (Exception e)
            {

                log.Error(e, "Error during Tour Add");
            }
            IsEditVisible = false;
        }
        //Exporting
        private void ExportTourPressed()
        {
            if (Tours.Count <= 0)
            {
                throw (new InputIsEmptyexception("No Tours to Export"));
            }
            DBRequests.ExportTours(Tours);
        }
        public bool IsAddVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible == value)
                    return;
                isVisible = value;
                OnPropertyChanged(nameof(IsAddVisible));
            }
        }
        public bool IsEditVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible == value)
                    return;
                isVisible = value;
                OnPropertyChanged(nameof(IsEditVisible));
            }
        }
        public RelayCommand AddTourButton
        {
            get;
            set;
        }
        public RelayCommand DelTourButton
        {
            get;
            set;
        }
        public RelayCommand EditTourButton
        {
            get;
            set;
        }
        public RelayCommand ExportTourButton
        {
            get;
            set;
        }
        public Tour addTour
        {
            get;
            set;
        }
        public Tour SelectedTour
        {
            get;

            set;

        }
        public ObservableCollection<Tour> Tours
        {
            get;
            set;
        }

    }


}
