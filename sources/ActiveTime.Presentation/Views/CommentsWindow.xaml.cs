﻿// ActiveTime
// Copyright (C) 2011-2020 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows;
using DustInTheWind.ActiveTime.Persistence;
using DustInTheWind.ActiveTime.UI.Controllers;
using DustInTheWind.ActiveTime.UI.IViews;
using DustInTheWind.ActiveTime.UI.Models;
using DustInTheWind.ActiveTime.MainModule.ViewModels;

namespace DustInTheWind.ActiveTime.MainModule.Views
{
    /// <summary>
    /// Interaction logic for CommentsWindow.xaml
    /// </summary>
    public partial class CommentsWindow : Window
    {
        public CommentsWindow(CommentsViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            InitializeComponent();

            Loaded += (s, e) => DataContext = viewModel;

            datePickerDate.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as CommentsViewModel).WindowLoaded();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            presenter.SaveButtonClicked();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            presenter.CancelButtonClicked();
        }

        private void buttonApply_Click(object sender, RoutedEventArgs e)
        {
            presenter.ApplyButtonClicked();
        }

        public void CloseWithCancel()
        {
            DialogResult = false;
        }

        public void CloseWithOk()
        {
            DialogResult = true;
        }

        //private void checkBoxWrap_Checked(object sender, RoutedEventArgs e)
        //{
        //    presenter.WrapChecked();
        //}

        //private void checkBoxWrap_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    presenter.WrapUnhecked();
        //}
    }
}
