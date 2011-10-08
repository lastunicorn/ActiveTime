﻿// ActiveTime
// Copyright (C) 2009-2011 Dust in the Wind
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
using System.ComponentModel;
using DustInTheWind.ActiveTime.Common;

namespace DustInTheWind.ActiveTime.Main.ViewModels
{
    public class StatusInfoViewModel : ViewModelBase
    {
        private readonly IStatusInfoService statusInfoService;

        public string StatusText
        {
            get { return statusInfoService.StatusText; }
            set { statusInfoService.StatusText = value; }
        }

        public StatusInfoViewModel(IStatusInfoService statusInfoService)
        {
            if (statusInfoService == null)
                throw new ArgumentNullException("statusInfoService");

            this.statusInfoService = statusInfoService;
            this.statusInfoService.StatusTextChanged += (s, e) => NotifyPropertyChanged("StatusText");
        }
    }
}