﻿// ActiveTime
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DustInTheWind.ActiveTime.Common;
using DustInTheWind.ActiveTime.Common.Recording;
using DustInTheWind.ActiveTime.Common.ShellNavigation;
using DustInTheWind.ActiveTime.Common.Reminding;

namespace DustInTheWind.ActiveTime.ReminderModule.Services
{
    /// <summary>
    /// This class uses a reminder to deremine when the user should maka a pause. The recorder is
    /// monitorize and when the recorder starts, the reminder is started, too.
    /// </summary>
    class PauseReminder : IPauseReminder
    {
        private IRecorderService recorderService;

        /// <summary>
        /// It is used to display a message box to the user.
        /// </summary>
        private IShellNavigator shellNavigator;

        /// <summary>
        /// It is used to determine the time when the user should make a pause.
        /// </summary>
        private IReminder reminder;

        private bool isMonitoring = false;

        public TimeSpan PauseInterval { get; set; }

        public TimeSpan SnoozeInterval { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseReminder"/> class.
        /// </summary>
        /// <param name="recorderService">The recorder that is monitorized to check when the user is working.</param>
        /// <param name="shellNavigator">It is used to display a message box to the user.</param>
        /// <param name="reminder">It is used to determine the time when the user should make a pause.</param>
        public PauseReminder(IRecorderService recorderService, IShellNavigator shellNavigator, IReminder reminder)
        {
            this.reminder = reminder;
            if (recorderService == null)
                throw new ArgumentNullException("recorderService");

            if (shellNavigator == null)
                throw new ArgumentNullException("shellNavigator");

            if (reminder == null)
                throw new ArgumentNullException("reminder");

            this.recorderService = recorderService;
            this.shellNavigator = shellNavigator;
            this.reminder = reminder;

            PauseInterval = TimeSpan.FromHours(1);
            //PauseInterval = TimeSpan.FromSeconds(5);
            SnoozeInterval = TimeSpan.FromMinutes(3);
        }

        public void StartMonitoring()
        {
            if (isMonitoring) return;

            recorderService.Started += new EventHandler(recorderService_Started);
            recorderService.Stopped += new EventHandler(recorderService_Stopped);
            reminder.Ring += new EventHandler<RingEventArgs>(reminder_Ring);

            if (recorderService.State == RecorderState.Running)
                reminder.Start(PauseInterval);
        }

        private void recorderService_Started(object sender, EventArgs e)
        {
            reminder.Start(PauseInterval);
        }

        private void recorderService_Stopped(object sender, EventArgs e)
        {
            reminder.Stop();
        }

        private void reminder_Ring(object sender, RingEventArgs e)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Text", "Make a pause.");
            shellNavigator.Navigate(ShellNames.MessageShell, parameters);

            e.Snooze = true;
            e.SnoozeTime = SnoozeInterval;
        }
    }
}