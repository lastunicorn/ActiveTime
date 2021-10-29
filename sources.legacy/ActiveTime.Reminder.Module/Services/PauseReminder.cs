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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ActiveTime.Common.Logging;
using DustInTheWind.ActiveTime.Common.Presentation;
using DustInTheWind.ActiveTime.Common.Presentation.ShellNavigation;
using DustInTheWind.ActiveTime.Common.Recording;
using DustInTheWind.ActiveTime.Reminder.Module.Reminding;

namespace DustInTheWind.ActiveTime.Reminder.Module.Services
{
    /// <summary>
    /// The recorder is watched and a notification is displayed to the user when the
    /// <see cref="PauseInterval"/> is elapsed.
    /// </summary>
    public class PauseReminder : IPauseReminder
    {
        /// <summary>
        /// The recorder is used to check when the user is working.
        /// </summary>
        private readonly IRecorderService recorderService;

        /// <summary>
        /// It is used to display a message box to the user.
        /// </summary>
        private readonly IShellNavigator shellNavigator;

        /// <summary>
        /// It is used to calculate the time when the user should make a pause.
        /// </summary>
        private readonly IReminder reminder;

        private readonly ILogger logger;

        private bool isMonitoring;

        /// <summary>
        /// Minimum time that the user must use for a pause. It is imposed only when the app
        /// requested him to make a pause. If he makes a pause ahead of time, this limit is not used.
        /// </summary>
        private readonly TimeSpan minimumPauseTime = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Specifies if it was time to make a pause when the session was locked.
        /// This value is used when the user unlocks the session.
        /// </summary>
        private bool wasPauseAnnounced;

        /// <summary>
        /// The interval time after which the "pause" message should be displayed.
        /// This value should be set before calling the <see cref="M:StartMonitoring"/> method.
        /// </summary>
        public TimeSpan PauseInterval { get; set; }

        /// <summary>
        /// The interval time after which the "snooze" message should be displayed.
        /// </summary>
        public TimeSpan SnoozeInterval { get; set; }

        public List<IReminderInhibitor> Inhibitors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseReminder"/> class.
        /// </summary>
        /// <param name="recorderService">The recorder is used to check when the user is working.</param>
        /// <param name="shellNavigator">It is used to display a message box to the user.</param>
        /// <param name="reminder">It is used to calculate the time when the user should make a pause.</param>
        public PauseReminder(IRecorderService recorderService, IShellNavigator shellNavigator, IReminder reminder, ILogger logger)
        {
            this.recorderService = recorderService ?? throw new ArgumentNullException(nameof(recorderService));
            this.shellNavigator = shellNavigator ?? throw new ArgumentNullException(nameof(shellNavigator));
            this.reminder = reminder ?? throw new ArgumentNullException(nameof(reminder));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Inhibitors = new List<IReminderInhibitor>();

            PauseInterval = TimeSpan.FromHours(1);
            SnoozeInterval = TimeSpan.FromMinutes(3);
        }

        public void StartMonitoring()
        {
            if (isMonitoring)
                return;

            recorderService.Started += HandleRecorderServiceStarted;
            recorderService.Stopped += HandleRecorderServiceStopped;
            reminder.Ring += HandleReminderRing;

            if (recorderService.State == RecorderState.Running)
                reminder.Start(PauseInterval);

            wasPauseAnnounced = false;
            isMonitoring = true;
        }

        private void HandleRecorderServiceStarted(object sender, EventArgs e)
        {
            reminder.Start(PauseInterval);

            if (wasPauseAnnounced)
                DisplayScoldIfNecessary();

            wasPauseAnnounced = false;
        }

        private void HandleRecorderServiceStopped(object sender, EventArgs e)
        {
            reminder.Stop();
        }

        private void HandleReminderRing(object sender, RingEventArgs e)
        {
            bool allowReminder = Inhibitors == null || Inhibitors.All(x => x.Allow);

            if (allowReminder)
            {
                wasPauseAnnounced = true;
                RaiseReminder();
            }
            else
            {
                logger.Log("Reminder is inhibited.");
            }

            e.Snooze = true;
            e.SnoozeTime = SnoozeInterval;
        }

        private void RaiseReminder()
        {
            TimeSpan timeFromLastPause = DateTime.Now - reminder.StartTime;

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "Text", $"Time passed: {timeFromLastPause:hh\\:mm\\:ss}\n\nMake a pause NOW!" }
            };

            shellNavigator.Navigate(ShellNames.MessageShell, parameters);
        }

        private void DisplayScoldIfNecessary()
        {
            TimeSpan? timeFromLastStop = recorderService.CalculateTimeFromLastStop();

            if (timeFromLastStop != null && timeFromLastStop < minimumPauseTime)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "Text", "Really?\nDo you think you can trick me?\n\nMake a REAL pause." }
                };

                shellNavigator.Navigate(ShellNames.MessageShell, parameters);
            }
        }
    }
}
