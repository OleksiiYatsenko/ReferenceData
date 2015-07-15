using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Reactive.Subjects;
using ReferenceData.Model;

namespace ReferenceData
{
    public class UserFullInfo : ValidationEntity<UserFullInfo>
    {
        #region Filds
        private int _Id;
        private string _FirstName;
        private string _SecondName;
        private Country _Country;
        private Location _Location;
        private Subdivision _Subdivision;

        #endregion

        #region Properties
        public ISubject<Country> CountrySubject;
        public ISubject<Subdivision> SubdivisionSubject;

        public int Id 
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SecondName
        {
            get { return _SecondName; }
            set
            {
                if (_SecondName != value)
                {
                    _SecondName = value;
                    OnPropertyChanged();
                }
            }
        }
        public Country Country
        {
            get 
            {
                return _Country; 
            }
            set
            {
                if (_Country == null || _Country.Id != value.Id)
                {
                    _Country = value;
                    CountrySubject.OnNext(_Country);
                    OnPropertyChanged();
                }
            }
        }
        public Subdivision Subdivision
        {
            get { return _Subdivision; }
            set
            {
                if (_Subdivision == null || value != null && value.Id != _Subdivision.Id)
                {
                    _Subdivision = value;
                    SubdivisionSubject.OnNext(_Subdivision);
                    OnPropertyChanged();
                }
            }
        }
        public Location Location
        {
            get { return _Location; }
            set
            {
                if (_Location == null || value != null && value.Id != _Location.Id)
                {
                    _Location = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructors
        static UserFullInfo() 
        {
            RegisterValidator(x => x.FirstName, x => !string.IsNullOrEmpty(x.FirstName), "Please, enter the first name");
            RegisterValidator(x => x.SecondName, x => !string.IsNullOrEmpty(x.SecondName), "Please, enter the second name");
            RegisterValidator(x => x.Country, x => x.Country != null, "Please, select a country");
        }

        public UserFullInfo()
        {
            CountrySubject = new Subject<Country>();
            SubdivisionSubject = new Subject<Subdivision>();
        }
        #endregion
    }
}
