using BL;
using Model_Accesse.Model;
using NLog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace UI.ViewModel
{
    public class TourLogViewModel : INotifyPropertyChanged
    {
        Logger log = LogManager.GetCurrentClassLogger();


        private TourLogic DBRequests;
        public TourLogViewModel(TourLogic dBRequests)
        {

            log.Info("{component} starting up", nameof(TourLogViewModel));
            DBRequests = dBRequests;
            DelLogButton = new((_) => { DeleteLogPressed(); });
            ReportLogButton = new((_) => { ReportLogPressed(); });
            AktivTours = new ObservableCollection<Tour>();
            SelectedLogs = new TourLog();
            addLog = new TourLog();
            OnPropertyChanged(nameof(AktivTours));
            Update();
            UpdateTours();
            log.Info("{component} Initialized", nameof(TourLogViewModel));
        }

        private void Update()
        {
            log.Info("Called Update TourLogViewUpdate");
            if (SelectedTourName != null)
            {
                addLog.Name = SelectedTourName;
                string[,] tourLogList = DBRequests.RequestTourLogUpdate(addLog);
                TourLogs = new ObservableCollection<TourLog>();
                for (int i = 0; i < (tourLogList.GetUpperBound(0) + 1); i++)
                {
                    TourLogs.Add(new TourLog()
                    {
                        Name = tourLogList[i, 11],
                        id = Int32.Parse(tourLogList[i, 0]),
                        date = tourLogList[i, 1],
                        report = tourLogList[i, 2],
                        distance = tourLogList[i, 3],
                        totalTime = tourLogList[i, 7],
                        rating = tourLogList[i, 6],
                        avgSpeed = tourLogList[i, 8],
                        burnedCalories = tourLogList[i, 9],
                        startTime = tourLogList[i, 4],
                        endTime = tourLogList[i, 5],
                        comment = tourLogList[i, 10]
                    });
                }
                OnPropertyChanged(nameof(TourLogs));
                addLog = new();
                OnPropertyChanged(nameof(addLog));
            }

        }
        private void UpdateTours()
        {
            log.Info("Called Tours for Combobox");
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
        //DELETING LOG
        public void DeleteLogPressed()
        {
            if (SelectedLogs != null)
            {
                DBRequests.DeleteTourLog(SelectedLogs);
            }
            Update();
        }
        //ADDING Log
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
            UpdateTours();
            IsAddVisible = true;
            log.Info("Open Add Tour Popup");
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
                AddLogPressed();
            }
            catch (InputIsEmptyexception e)
            {
                log.Error(e, "Error during Log Add");
            }

            IsAddVisible = false;
        }
        public void AddLogPressed()
        {
            if (addLog.Name != null)
            {
                if (addLog.date != null)
                {
                    if (addLog.startTime != null && addLog.endTime != null)
                    {
                        DBRequests.AddTourLog(addLog);
                        Update();
                        log.Info("Added new Log");
                        return;
                    }
                    throw (new InputIsEmptyexception("Timeslots may not be empty"));
                }
                throw (new InputIsEmptyexception("Date cannot be empty"));
            }
            throw (new InputIsEmptyexception("Name cannot be empty"));

        }
        //EDITING Log
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
            addLog = SelectedLogs;
            OnPropertyChanged(nameof(addLog));
            UpdateTours();
            IsEditVisible = true;
            log.Info("Open Edit Tour Popup");
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
                SelectedLogs = addLog;
                EditLogPressed();
            }
            catch (InputIsEmptyexception e)
            {
                log.Error(e, "Error during Log Edit");
            }

            IsEditVisible = false;
        }
        public void EditLogPressed()
        {
            if (SelectedLogs.Name != null)
            {
                if (SelectedLogs.date != null)
                {
                    if (SelectedLogs.startTime != null && SelectedLogs.endTime != null)
                    {
                        if (SelectedLogs != null)
                        {
                            DBRequests.EditTourLog(SelectedLogs);
                            Update();
                            log.Info("Edited a Tour");
                            return;
                        }
                        throw (new InputIsEmptyexception("No Log for Change Selected"));
                    }
                    throw (new InputIsEmptyexception("Timeslots may not be empty"));
                }
                throw (new InputIsEmptyexception("Date cannot be empty"));
            }
            throw (new InputIsEmptyexception("Name cannot be empty"));

        }
        //Report generation
        private void ReportLogPressed()
        {
            if (SelectedTourName != null)
            {
                //hand over SelectedLogs, report on this single LOG
                foreach (Tour tour in AktivTours)
                {
                    if (tour.Name == SelectedTourName)
                    {
                        AktivTour = tour;
                        break;
                    }
                }
                SelectedLogs.Name = AktivTour.Name;
                DBRequests.ReportSingleService(SelectedLogs, AktivTour);
                log.Info("Report genereted for {Tour}", SelectedLogs.Name);
            }
            else
            {
                DBRequests.ReportAllService();
                log.Info("Report generated for ALL");
            }
            Update();
        }

        //Fancy managment shit//        

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
        public RelayCommand AddLogButton
        {
            get;
            set;
        }
        public RelayCommand DelLogButton
        {
            get;
            set;
        }
        public RelayCommand ReportLogButton
        {
            get;
            set;
        }
        public RelayCommand EditLogButton
        {
            get;
            set;
        }
        public TourLog addLog
        {
            set;
            get;

        }
        public TourLog SelectedLogs
        {
            get;
            set;
        }
        public ObservableCollection<TourLog> TourLogs { get; set; }
        public Tour AktivTour
        {
            get;
            set;
        }
        public ObservableCollection<Tour> AktivTours { get; set; }
        private string _selectedTourName;
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
    }
}
