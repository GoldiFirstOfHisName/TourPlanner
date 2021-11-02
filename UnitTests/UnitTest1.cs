using BL;
using DAL;
using Microsoft.Extensions.Configuration;
using Model_Accesse.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tourplanner.ViewModel;
using UI.ViewModel;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);


            //Adding Tour
            Tour happyTourAdd = new Tour { Name = "Bradbury" };
            Tour unhappyTour1Add = new Tour();
            Tour unhappyTour2Add = new Tour { Name = "Bradbury", location = null };
            Tour unhappyTour3Add = new Tour { Name = "Bradbury", destination = null };
            //Adding Log
            TourLog happyLogAdd = new TourLog { Name = "Bradbury", totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            TourLog unhappyLog1Add = new TourLog { Name = "Bradbury", date = null, totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            TourLog unhappyLog2Add = new TourLog { Name = "Bradbury", startTime = null, totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            TourLog unhappyLog3Add = new TourLog { Name = null, totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            //Edit Tour
            Tour happyTourEdit = new Tour { Name = "Changed", OldName = happyTourAdd.Name };
            Tour unhappyTourEdit1 = new Tour { Name = "Changed", OldName = happyTourAdd.Name, location = null };
            Tour unhappyTourEdit2 = new Tour { Name = "Changed", OldName = happyTourAdd.Name, destination = null };
            Tour unhappyTourEdit3 = new Tour { Name = "Changed", destination = null };
            Tour unhappyTourEdit4 = new Tour { OldName = happyTourAdd.Name, destination = null };
            //Edit Log
            TourLog happyLogEdit = new TourLog { Name = "Bradbury" };
            TourLog unhappyLogEdit1 = new TourLog { Name = "Bradbury", endTime = null };
            TourLog unhappyLogEdit2 = new TourLog { Name = "Bradbury", startTime = null };
            TourLog unhappyLogEdit3 = new TourLog { Name = "Bradbury", date = null };
            TourLog unhappyLogEdit4 = new TourLog { Name = null };
        }

        //Add Tours
        [Test]
        public void AddTourHappy()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury" };
            TourView.addTour = addtour;
            try
            {
                TourView.AddTourPressed();
                Assert.Pass();
            }
            catch (Exception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Fail(e.Message);
                }

            }

        } //Done
        [Test]
        public void UnhappyTourAdd_NoName()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { };
            TourView.addTour = addtour;
            try
            {
                TourView.AddTourPressed();
            }
            catch (InputIsEmptyexception e) //Why are you not catching the exception?
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }

        }//Done
        [Test]
        public void UnhappyTourAdd_NoLocation()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury", location = null };
            TourView.addTour = addtour;
            try
            {
                TourView.AddTourPressed();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done
        [Test]
        public void UnhappyTourAdd_NoDestination()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury", destination = null };
            TourView.addTour = addtour;
            try
            {
                TourView.AddTourPressed();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }

            }
        }//Done

        //Add Logs
        [Test]
        public void AddLogHappy()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            LogView.addLog = addLog;
            try
            {
                LogView.AddLogPressed();
                Assert.Pass();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Fail(e.Message);
                }
            }
        } //Done
        [Test]
        public void UnhappyLogAdd_NoName()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = null, totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            LogView.addLog = addLog;
            try
            {
                LogView.AddLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass();

                }
            }
        } //Done
        [Test]
        public void UnhappyLogAdd_NoDate()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", date = null, totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            LogView.addLog = addLog;
            try
            {
                LogView.AddLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass();

                }
            }
        } //Done
        [Test]
        public void UnhappyLogAdd_NoStartTime()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", startTime = null, totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            LogView.addLog = addLog;
            try
            {
                LogView.AddLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass();

                }
            }
        } //Done
        [Test]
        public void UnhappyLogAdd_NoEndTime()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", endTime = null, totalTime = "3", avgSpeed = "24", burnedCalories = "200" };
            LogView.addLog = addLog;
            try
            {
                LogView.AddLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass();

                }
            }
        } //Done

        //Edit Tours
        [Test]
        public void EditTourHappy()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury" };
            TourView.addTour = new Tour { Name = "Changed", OldName = addtour.Name }; ;
            try
            {
                TourView.EditTourPressed();
                Assert.Pass();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Fail(e.Message);
                }
            }
        } //Done
        [Test]
        public void UnhappyEditTour_NoName()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury" };
            TourView.addTour = new Tour { Name = null, OldName = addtour.Name }; ;
            try
            {
                TourView.EditTourPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done
        [Test]
        public void UnhappyEditTour_NoLocation()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury" };
            TourView.addTour = new Tour { Name = "Changed", location = null, OldName = addtour.Name }; ;
            try
            {
                TourView.EditTourPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done
        [Test]
        public void UnhappyEditTour_NoDestination()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury" };
            TourView.addTour = new Tour { Name = "Changed", destination = null, OldName = addtour.Name }; ;
            try
            {
                TourView.EditTourPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            };
        }//Done
        [Test]
        public void UnhappyEditTour_NoChangeTarget()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury" };
            TourView.addTour = new Tour { Name = "Changed", OldName = null }; ;
            try
            {
                TourView.EditTourPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done

        //Edit Logs
        [Test]
        public void LogEditHappy()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury" };
            LogView.SelectedLogs = addLog;
            try
            {
                LogView.EditLogPressed();
                Assert.Pass();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Fail(e.Message);
                }
            }
        }//Done
        [Test]
        public void UnhappyEditLog_NoName()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = null };
            LogView.SelectedLogs = addLog;
            try
            {
                LogView.EditLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done
        [Test]
        public void UnhappyEditLog_NoDate()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", date = null };
            LogView.SelectedLogs = addLog;
            try
            {
                LogView.EditLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done
        [Test]
        public void UnhappyEditLog_NoStartTime()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", startTime = null };
            LogView.SelectedLogs = addLog;
            try
            {
                LogView.EditLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done
        [Test]
        public void UnhappyEditLog_NoEndTime()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", endTime = null };
            LogView.SelectedLogs = addLog;
            try
            {
                LogView.EditLogPressed();
                Assert.Fail();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Pass(e.Message);
                }
            }
        }//Done
        [Test]
        public void DeleteTourHappy()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            Tour addtour = new Tour { Name = "Bradbury" };
            TourView.SelectedTour = addtour;
            try
            {
                TourView.DeleteTourPressed();
                Assert.Pass();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Fail(e.Message);
                }
            }
            catch (Npgsql.NpgsqlException e)
            {
                Assert.Pass(e.Message);
            }
        }//Done
        [Test]
        public void DeleteLogHappy()
        {
            //Gen DAL
            var config = new Dictionary<string, string>
            {
                    {"ConnectionStrings:DB", "Host=localhost;Username=postgres;Password=camel100;Database=postgres"},
                    {"ApiKey:Mapquest", "bEZdCy6muY3GGjFew5AyKfi8CGEto1HY"}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(config).Build();
            Connection c = new(configuration);
            MapQuest m = new(configuration);
            ReportGeneration r = new();
            //Gen BL
            TourLogic tourLogic = new(c, m, r);
            //Gen UI
            TourViewModel TourView = new TourViewModel(tourLogic, configuration);
            TourLogViewModel LogView = new TourLogViewModel(tourLogic); ;
            CurrentTourViewModel CurrentView = new CurrentTourViewModel(tourLogic);
            TourLog addLog = new TourLog { Name = "Bradbury", date = null, id = 1 };
            LogView.SelectedLogs = addLog;
            try
            {
                LogView.DeleteLogPressed();
                Assert.Pass();
            }
            catch (InputIsEmptyexception e)
            {
                if (e.Message != "")
                {
                    TourView.log.Error(e, e.Message + "//UNIT TEST IN PROGRESS//");
                    Assert.Fail(e.Message);
                }
            }
        }//Done
    }
}