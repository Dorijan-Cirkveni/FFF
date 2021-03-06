﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Mobile.Views;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        string title = string.Empty;
        string message = string.Empty;
        bool _isStudent = false;
        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public string Title 
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public bool Student
        {
            get => _isStudent;
            set => SetProperty(ref _isStudent, value);
        }

        public string HelloMessage
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
