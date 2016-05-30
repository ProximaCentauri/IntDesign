﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Model;
using Model.Controls;
using System.Windows.Media;

namespace ViewModel
{
    public interface IMainViewModel : INotifyPropertyChanged, IDisposable
    {
        ICommand ActionCommand { get; set; }
    
        int SelectedIndex { get; set; }        
       
        void CreateEntity(object obj);

        void LoadEntities();        
        PopupView CurrentPopupView { get; set; }
        ImageSource CustomerImageSource { get; set; }
    }
}
