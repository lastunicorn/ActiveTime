// ActiveTime
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
using System.Windows.Input;
using DustInTheWind.ActiveTime.Common.Presentation.ShellNavigation;
using DustInTheWind.ActiveTime.Common.Recording;
using DustInTheWind.ActiveTime.Common.Services;
using DustInTheWind.ActiveTime.Presentation.Commands;
using MediatR;

namespace DustInTheWind.ActiveTime.Presentation.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public ICommand OverviewCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand AboutCommand { get; }

        public MainMenuViewModel(IApplicationService applicationService, IShellNavigator shellNavigator, IRecorderService recorder, IMediator mediator)
        {
            if (applicationService == null) throw new ArgumentNullException(nameof(applicationService));
            if (shellNavigator == null) throw new ArgumentNullException(nameof(shellNavigator));
            if (recorder == null) throw new ArgumentNullException(nameof(recorder));
            if (mediator == null) throw new ArgumentNullException(nameof(mediator));

            OverviewCommand = new OverviewCommand(shellNavigator);
            ExitCommand = new ExitCommand(applicationService);
            StartCommand = new StartRecorderCommand(recorder, mediator);
            StopCommand = new StopRecorderCommand(recorder, mediator);
            AboutCommand = new AboutCommand(shellNavigator);
        }
    }
}