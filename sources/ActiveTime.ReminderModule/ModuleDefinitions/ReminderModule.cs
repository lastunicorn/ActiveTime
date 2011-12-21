// ActiveTime
// Copyright (C) 2011 Dust in the Wind
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

using DustInTheWind.ActiveTime.Common.Reminding;
using DustInTheWind.ActiveTime.ReminderModule.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;

namespace DustInTheWind.ActiveTime.ReminderModule.ModuleDefinitions
{
    public class ReminderModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        public ReminderModule(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public void Initialize()
        {
            unityContainer.RegisterType<IReminder, Reminder>();

            PauseReminder pauseReminder = unityContainer.Resolve<PauseReminder>();
            pauseReminder.StartMonitoring();
            unityContainer.RegisterInstance<IPauseReminder>(pauseReminder, new ContainerControlledLifetimeManager());
        }
    }
}
