using DAL;
using Model_Accesse.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace BL
{
    public class TourLogic
    {
        private Connection connection;
        private MapQuest mapQuest;
        private ReportGeneration reportGeneration;
        public TourLogic(Connection c, MapQuest m, ReportGeneration r)
        {
            reportGeneration = r;
            connection = c;
            mapQuest = m;
        }
        //Tours
        public void AddTour(Tour tour)
        {
            connection.switch_CRUD = "INSERT";
            connection.TourRun(tour);
        }
        public void EditTour(Tour tour)
        {
            connection.switch_CRUD = "UPDATE";
            connection.TourRun(tour);
        }
        public void DeleteTour(Tour tour)
        {
            connection.switch_CRUD = "DELETE";
            connection.TourRun(tour);
        }
        public string[,] RequestTourUpdate()
        {
            return connection.TourUpdate();
        }
        //Logs
        public void AddTourLog(TourLog TourLog)
        {
            connection.switch_CRUD = "INSERT";
            connection.TourLogRun(TourLog);
        }
        public void EditTourLog(TourLog TourLog)
        {
            connection.switch_CRUD = "UPDATE";
            connection.TourLogRun(TourLog);
        }
        public void DeleteTourLog(TourLog TourLog)
        {
            connection.switch_CRUD = "DELETE";
            connection.TourLogRun(TourLog);
        }
        public string[,] RequestTourLogUpdate(TourLog TourLog)
        {
            return connection.TourLogUpdate(TourLog);
        }
        //Routes
        private JObject route;

        public async Task<Tour> GetTourData(Tour tour)
        {
            route = await mapQuest.GetRouteData(tour);
            tour.distance = mapQuest.GetDistance(route).ToString();
            tour.source = mapQuest.GetImage(tour);

            return tour;
        }
        //Reports
        public void ReportSingleService(TourLog selectedLog, Tour tour)
        {
            string[,] tourLogList = RequestTourLogUpdate(selectedLog);
            ObservableCollection<TourLog> TourLogs = new ObservableCollection<TourLog>();
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
            reportGeneration.WriteReport(TourLogs, tour);
        }
        public void ReportAllService()
        {
            string[,] tourLogList = connection.GetAllTourLogs();
            ObservableCollection<TourLog> TourLogs = new ObservableCollection<TourLog>();
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
            Tour Ilazy = new();
            reportGeneration.WriteReport(TourLogs, Ilazy);

        }

        public void ExportTours(ObservableCollection<Tour> tours)
        {
            string fileName = "ExportedTours.json";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            StreamWriter file = File.CreateText(filePath);
            file.AutoFlush = true;
            JsonTextWriter writer = new JsonTextWriter(file);
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, tours);
            writer.Close();
            file.Close();
        }
    }
}
