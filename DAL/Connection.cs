using Microsoft.Extensions.Configuration;
using Model_Accesse.Model;
using Npgsql;
using System;
using System.Threading;

namespace DAL
{

    public class Connection
    {

        private static Mutex mut = null;
        public Connection(IConfiguration configuration)
        {
            mut = new Mutex();
            conString = configuration.GetConnectionString("DB");
            NpgsqlConnection con = OpenConnection();
            var test = GetVersion(con);//Test Database connection with version before any real attempt
        }
        //Tours
        public void TourRun(Tour inTour)
        {
            mut.WaitOne();
            tour = inTour;
            NpgsqlConnection con = OpenConnection();
            var cmd = con.CreateCommand();

            switch (switch_CRUD)
            {
                case "UPDATE":
                    UpdateTour(cmd);
                    break;
                case "INSERT":
                    AddTour(cmd);
                    break;
                case "DELETE":
                    DeleteTour(cmd);
                    break;
                default:
                    //Throw shit
                    break;
            }
            switch_CRUD = null;
            mut.ReleaseMutex();
        }
        private void AddTour(NpgsqlCommand cmd)
        {
            cmd.CommandText = "insert into Tour (name,description,source,destination,boundaries,distance) values (@name,@description,@source,@destination,@boundaries,@distance)";

            cmd.Parameters.Add(new NpgsqlParameter("name", tour.Name));
            cmd.Parameters.Add(new NpgsqlParameter("description", tour.description));
            cmd.Parameters.Add(new NpgsqlParameter("source", tour.source));//tour.source));
            cmd.Parameters.Add(new NpgsqlParameter("destination", "Placeholder"));//tour.destination));
            cmd.Parameters.Add(new NpgsqlParameter("boundaries", "Placeholder"));
            cmd.Parameters.Add(new NpgsqlParameter("distance", tour.distance));


            try
            {
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                //Log a non Add
            }
        }
        private void DeleteTour(NpgsqlCommand cmd)
        {
            cmd.CommandText = "delete from Tour where name=@name";

            NpgsqlParameter<string> npgsqlParameterName = new NpgsqlParameter<string>();
            npgsqlParameterName.ParameterName = "name";
            npgsqlParameterName.Value = tour.Name;

            cmd.Parameters.Add(npgsqlParameterName);

            try
            {
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                //Log a non Delete
            }
        }
        private void UpdateTour(NpgsqlCommand cmd)
        {

            cmd.CommandText = "UPDATE Tour SET name = @name, description = @description, source = @source, destination = @destination, boundaries = @boundaries, distance = @distance WHERE name = @oldName";


            cmd.Parameters.Add(new NpgsqlParameter("name", tour.Name));
            cmd.Parameters.Add(new NpgsqlParameter("description", tour.description));
            cmd.Parameters.Add(new NpgsqlParameter("source", tour.source));
            cmd.Parameters.Add(new NpgsqlParameter("destination", tour.destination));
            cmd.Parameters.Add(new NpgsqlParameter("boundaries", "Placeholder"));
            cmd.Parameters.Add(new NpgsqlParameter("distance", tour.distance));
            cmd.Parameters.Add(new NpgsqlParameter("oldName", tour.OldName));
            try
            {
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                //Log a non Edit
            }


        }
        public string[,] TourUpdate()
        {
            mut.WaitOne();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "SELECT COUNT(*) FROM tour";
            int k = cmd.ExecuteNonQuery();
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int count = rdr.GetInt16(0);

            rdr.Close();
            cmd.CommandText = "SELECT * FROM tour";
            k = cmd.ExecuteNonQuery();
            cmd.ExecuteReader();
            int fieldCount = rdr.FieldCount;
            string[,] Tours = new string[count, fieldCount];
            int i = 0;
            while (rdr.Read())
            {
                for (int a = 0; a < rdr.FieldCount - 1; a++)
                {
                    if (!rdr.IsDBNull(a + 1))
                    {
                        Tours[i, a] = rdr.GetString(a + 1);
                    }
                    else Tours[i, a] = "---";


                }

                i++;
            }
            cmd.Connection.Close();
            mut.ReleaseMutex();
            return Tours;
        }
        //Logs
        public string[,] TourLogUpdate(TourLog inTour)
        {
            mut.WaitOne();
            tourLog = inTour;
            using var cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.Parameters.Add(new NpgsqlParameter("name", tourLog.Name));
            cmd.CommandText = "SELECT COUNT(*) FROM tourLog WHERE name=@name";
            int k = cmd.ExecuteNonQuery();
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int count = rdr.GetInt16(0);

            rdr.Close();
            cmd.CommandText = "SELECT * FROM tourLog WHERE name=@name";
            k = cmd.ExecuteNonQuery();
            cmd.ExecuteReader();
            int fieldCount = rdr.FieldCount;
            string[,] Tours = new string[count, fieldCount];
            int i = 0;
            int a = 0;
            while (rdr.Read())
            {
                Tours[i, 0] = rdr.GetInt32(0).ToString();
                for (a = 0; a < rdr.FieldCount - 1; a++)
                {
                    if (!rdr.IsDBNull(a + 1))
                    {
                        Tours[i, a + 1] = rdr.GetString(a + 1);
                    }
                    else Tours[i, a + 1] = "---";


                }

                i++;
            }
            cmd.Connection.Close();
            mut.ReleaseMutex();
            return Tours;
        }
        public string[,] GetAllTourLogs()
        {
            mut.WaitOne();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = OpenConnection();
            cmd.CommandText = "SELECT COUNT(*) FROM tourLog";
            int k = cmd.ExecuteNonQuery();
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int count = rdr.GetInt16(0);
            rdr.Close();
            cmd.CommandText = "SELECT * FROM tourLog";
            k = cmd.ExecuteNonQuery();
            cmd.ExecuteReader();
            int fieldCount = rdr.FieldCount;
            string[,] Tours = new string[count, fieldCount];
            int i = 0;
            int a = 0;
            while (rdr.Read())
            {
                Tours[i, 0] = rdr.GetInt32(0).ToString();
                for (a = 0; a < rdr.FieldCount - 1; a++)
                {
                    if (!rdr.IsDBNull(a + 1))
                    {
                        Tours[i, a + 1] = rdr.GetString(a + 1);
                    }
                    else Tours[i, a + 1] = "---";


                }

                i++;
            }
            cmd.Connection.Close();
            mut.ReleaseMutex();
            return Tours;

        }

        public void TourLogRun(TourLog tourLogIn)
        {
            mut.WaitOne();
            tourLog = tourLogIn;
            NpgsqlConnection con = OpenConnection();
            var cmd = con.CreateCommand();

            switch (switch_CRUD)
            {
                case "UPDATE":
                    UpdateTourLog(cmd);
                    break;
                case "INSERT":
                    AddTourLog(cmd);
                    break;
                case "DELETE":
                    DeleteTourLog(cmd);
                    break;
                default:
                    //Throw shit
                    break;
            }
            switch_CRUD = null;
            cmd.Connection.Close();
            mut.ReleaseMutex();
        }

        private void DeleteTourLog(NpgsqlCommand cmd)
        {
            cmd.CommandText = "delete from TourLog where logid=@id";

            cmd.Parameters.Add(new NpgsqlParameter("id", tourLog.id));

            int result = cmd.ExecuteNonQuery();
            if (result != 1)
            {
                // Fail throw exception throw new TourPersistenceException($"Error with updating a tour");
            }
        }

        private void AddTourLog(NpgsqlCommand cmd)
        {
            cmd.CommandText = "insert into TourLog(name,date,report,distance,totalTime,rating,avgSpeed,burnedCalories,startTime,endTime,comment) values (@name,@date,@report,@distance,@totalTime,@rating,@avgSpeed,@burnedCalories,@startTime,@endTime,@comment)";
            TimeSpan timeSpan = DateTime.Parse(tourLog.endTime).Subtract(DateTime.Parse(tourLog.startTime));
            int totalTime = (int)timeSpan.TotalHours;
            int distance = int.Parse(tourLog.distance);
            double avgSpeed = Math.Round((double)distance / (double)totalTime, 2);
            double burnedCalories = Math.Round(avgSpeed * totalTime, 2);


            cmd.Parameters.Add(new NpgsqlParameter("name", tourLog.Name));
            cmd.Parameters.Add(new NpgsqlParameter("date", tourLog.date));
            cmd.Parameters.Add(new NpgsqlParameter("report", tourLog.report));//not set manually
            cmd.Parameters.Add(new NpgsqlParameter("distance", tourLog.distance));
            cmd.Parameters.Add(new NpgsqlParameter("totalTime", totalTime));
            cmd.Parameters.Add(new NpgsqlParameter("rating", tourLog.rating));
            cmd.Parameters.Add(new NpgsqlParameter("avgSpeed", avgSpeed));
            cmd.Parameters.Add(new NpgsqlParameter("burnedCalories", burnedCalories));
            cmd.Parameters.Add(new NpgsqlParameter("startTime", tourLog.startTime));
            cmd.Parameters.Add(new NpgsqlParameter("endTime", tourLog.endTime));
            cmd.Parameters.Add(new NpgsqlParameter("comment", tourLog.comment));


            int result = cmd.ExecuteNonQuery();
            if (result != 1)
            {
                // Fail throw exception
            }
        }

        private void UpdateTourLog(NpgsqlCommand cmd)
        {
            cmd.CommandText = "UPDATE TourLog SET date=@date,report=@report,distance=@distance,totalTime=@totalTime,rating=@rating,avgSpeed=@avgSpeed,burnedCalories=@burnedCalories,startTime=@startTime,endTime=@endTime,comment=@comment WHERE logid=@id";
            TimeSpan timeSpan = DateTime.Parse(tourLog.endTime).Subtract(DateTime.Parse(tourLog.startTime));
            int totalTime = (int)timeSpan.TotalHours;
            int distance = int.Parse(tourLog.distance);
            double avgSpeed = Math.Round((double)distance / (double)totalTime, 2);
            double burnedCalories = Math.Round(avgSpeed * totalTime * 3, 2);


            cmd.Parameters.Add(new NpgsqlParameter("id", tourLog.id));
            cmd.Parameters.Add(new NpgsqlParameter("date", tourLog.date));
            cmd.Parameters.Add(new NpgsqlParameter("report", tourLog.report));//not set manually
            cmd.Parameters.Add(new NpgsqlParameter("distance", tourLog.distance));
            cmd.Parameters.Add(new NpgsqlParameter("totalTime", totalTime));
            cmd.Parameters.Add(new NpgsqlParameter("rating", tourLog.rating));
            cmd.Parameters.Add(new NpgsqlParameter("avgSpeed", avgSpeed));
            cmd.Parameters.Add(new NpgsqlParameter("burnedCalories", burnedCalories));
            cmd.Parameters.Add(new NpgsqlParameter("startTime", tourLog.startTime));
            cmd.Parameters.Add(new NpgsqlParameter("endTime", tourLog.endTime));
            cmd.Parameters.Add(new NpgsqlParameter("comment", tourLog.comment));


            int result = cmd.ExecuteNonQuery();
            if (result != 1)
            {
                // Fail throw exception
            }
        }

        private string GetVersion(NpgsqlConnection con)
        {
            mut.WaitOne();
            var sql = "SELECT version()";
            var cmd = new NpgsqlCommand(sql, con);
            string version = cmd.ExecuteScalar().ToString();
            mut.ReleaseMutex();
            return version;

        }
        private NpgsqlConnection OpenConnection()
        {
            var con = new NpgsqlConnection(conString);
            con.Open();
            return con;
        }

        private string conString;
        public string switch_CRUD
        {
            get;
            set;
        } = null;
        public Tour tour;
        public TourLog tourLog;


    }
}
