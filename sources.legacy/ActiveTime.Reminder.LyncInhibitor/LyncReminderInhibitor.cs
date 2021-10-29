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
using DustInTheWind.ActiveTime.Common.Logging;
using DustInTheWind.ActiveTime.Reminder.Module.Reminding;
using Microsoft.Lync.Model;

namespace DustInTheWind.ActiveTime.Reminder.LyncInhibitor
{
    /// <summary>
    /// Stops the reminder to display messages to the user if the he is not Free on Lync messenger.
    /// </summary>
    internal class LyncReminderInhibitor : IReminderInhibitor
    {
        private readonly ILogger logger;
        private readonly AvailabilityWatcher availabilityWatcher;

        public bool Allow
        {
            get
            {
                ContactAvailability availability = availabilityWatcher.CurrentAvailability;

                bool allow = availability != ContactAvailability.Busy &&
                             availability != ContactAvailability.BusyIdle &&
                             availability != ContactAvailability.DoNotDisturb;

                logger.Log($"LyncReminderInhibitor - availability = {availability}; allow = {allow}");

                return allow;
            }
        }

        public LyncReminderInhibitor(ILogger logger, AvailabilityWatcher availabilityWatcher)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.availabilityWatcher = availabilityWatcher ?? throw new ArgumentNullException(nameof(availabilityWatcher));
        }
    }
}
