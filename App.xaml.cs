using FoodPlaza.Data;
using FoodPlaza.Models;
using FoodPlaza.Views;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Java.Util;

namespace FoodPlaza
{
    public partial class App : Application
    {
        static TokenDatabaseController tokenDatabase;
        static UserDatabaseController userDatabase;
        static RestService restService;
        private static Label labelscreen;
        private static bool hasInternet;
        private static Page currentpage;
        private static Timer timer;
        private static bool noInterShow;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }
                return userDatabase;
            }

        }
        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                {
                    tokenDatabase = new TokenDatabaseController();
                }
                return tokenDatabase;
            }

        }
        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();

                }
                return restService;
            }

        }

        //------------Internet Connection-----------------------

        public static void StartCheckIfInternet(Label label, Page page)
        {
            labelscreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            hasInternet = true;
            currentpage = page;
            if (timer == null)
            {
                timer = new Timer((e) =>
                  {
                      CheckIfInternetOverTime();

                  }, null, 10, (int)TimeSpan.FromSeconds(10).TotalMilliseconds);
            }

        }

        private static void CheckIfInternetOverTime()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            if(!networkConnection.IsConnected)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (hasInternet)
                    {
                        if (!noInterShow)
                        {
                            hasInternet = false;
                            labelscreen.IsVisible = true;
                            await ShowDisplayAlertAsync();
                        }
                    }
                });
            }
        }

        private static async Task ShowDisplayAlertAsync()
        {
            noInterShow = false;
            await currentpage.DisplayAlert("Internet", "Device has no Internet,please reconnect", "okay");
            noInterShow = false;

        }
    }

    }

