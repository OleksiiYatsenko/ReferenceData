﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Windows.Threading;
using Microsoft.Practices.Unity;
using System.Reactive.Subjects;
using ReferenceData.Model;
using VirtualizationCollections;
using ReferenceData.UserServiceReference;
using ReferenceData.CountryServiceReference;
using ReferenceData.SubdivisionServiceReference;
using ReferenceData.LocationServiceReference;
using EmitMapper;
using System.Threading.Tasks;
using ReferenceData.Services.Abstract;


namespace ReferenceData.ViewModel
{
    public class ViewModel : NotificationEntity
    {
        #region Fields
        //private IUsersService userService = App.Container.Resolve<IUsersService>();
        private IUserServiceClient userService = App.Container.Resolve<IUserServiceClient>();
        private ICountryService countryService = App.Container.Resolve<ICountryService>();
        private ISubdivisionService subdivisionService = App.Container.Resolve<ISubdivisionService>();
        private ILocalService locationService = App.Container.Resolve<ILocalService>();

        private ISubject<UserFullInfo> userSubject;

        public ObservableCollection<UserFullInfo> Users { get; private set; }
        public ObservableCollection<Country> Countries { get; private set; }

        private ObservableCollection<Subdivision> _AvailableSubdivisions;
        public ObservableCollection<Subdivision> AvailableSubdivisions 
        {
            get { return _AvailableSubdivisions; }
            private set
            {
                if (_AvailableSubdivisions != value)
                {
                    _AvailableSubdivisions = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Location> _AvailableLocations;
        public ObservableCollection<Location> AvailableLocations {
            get { return _AvailableLocations; }
            private set 
            {
                if(_AvailableLocations != value)
                {
                    _AvailableLocations = value;
                    OnPropertyChanged();
                }
            }
        }

        private UserFullInfo currentUser;
        public UserFullInfo CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser != value)
                {
                    currentUser = value;

                    if (currentUser != null)
                    {
                        currentUser.SubdivisionSubject.Subscribe(x => FillLocations(x));
                        currentUser.CountrySubject.Subscribe(x => FillSubdivisions(x));
                        userSubject.OnNext(currentUser);
                    }

                    OnPropertyChanged();
                }
            }
        }

        //private UserProvider userP;

        private AsyncVirtualizingCollection<UserFullInfo> _AsyncUser;
        public AsyncVirtualizingCollection<UserFullInfo> AsyncUser
        {
            get { return _AsyncUser; }
            set
            {
                if(_AsyncUser != value)
                {
                    _AsyncUser = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CloseComman { get; set; }
        #endregion

        #region Constructor
        public ViewModel()
        {
            Users = new ObservableCollection<UserFullInfo>();

            Countries = new ObservableCollection<Country>(countryService.GetCountries());

            UserProvider userP = new UserProvider();
            //AsyncUser = new AsyncVirtualizingCollection<UserFullInfo>(userP, 100, 30);
            AsyncUser = new AsyncVirtualizingCollection<UserFullInfo>(userP, 100, 30000);

            var usersObservable = userService.GetUsers().ToObservable();
            usersObservable.SubscribeOn(ThreadPoolScheduler.Instance).ObserveOn(DispatcherScheduler.Current).Subscribe(userInfo => Users.Add(userInfo));

            userSubject = new Subject<UserFullInfo>();
            userSubject.Subscribe(x =>
            {
                if (x == null)
                    return;
                x.CountrySubject.OnNext(x.Country);
                x.SubdivisionSubject.OnNext(x.Subdivision);
            });

            //var countrySubj = new Subject<UserFullInfo>();
        
            currentUser = new UserFullInfo();

            NewCommand = new RelayCommand(arg => NewMethod());
            SaveCommand = new RelayCommand(arg => SaveMethod());
            CloseComman = new RelayCommand(arg => CloseMethod());
        }
        #endregion

        #region Private logic
        private void FillSubdivisions(Country country)
        {
            if (country == null) return;
            AvailableSubdivisions = new ObservableCollection<Subdivision>(subdivisionService.GetItemsByCountryId(country.Id));
            if (AvailableLocations != null && AvailableLocations.Count > 0)
                AvailableLocations.Clear();
        }
        private void FillLocations(Subdivision subdivision)
        {
            if (subdivision == null) return;
            AvailableLocations = new ObservableCollection<Location>(locationService.GetLocationsBySubdivisionId(subdivision.Id));
        }
        private void NewMethod()
        {
            CurrentUser = new UserFullInfo();
        }

        private void SaveMethod()
        {
            CurrentUser = userService.AddOrUpdate(CurrentUser);

            if (CurrentUser != null)
            {
                Users.Add(CurrentUser);
            }
        }
        private void CloseMethod()
        {
            Application.Current.MainWindow.Close();
        }
        #endregion

    }
}
