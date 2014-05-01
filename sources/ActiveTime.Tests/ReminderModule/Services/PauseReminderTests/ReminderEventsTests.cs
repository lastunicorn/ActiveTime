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
using System.Threading;
using DustInTheWind.ActiveTime.Common.Recording;
using DustInTheWind.ActiveTime.Common.UI;
using DustInTheWind.ActiveTime.Common.UI.ShellNavigation;
using DustInTheWind.ActiveTime.ReminderModule.Reminding;
using DustInTheWind.ActiveTime.ReminderModule.Services;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.ActiveTime.UnitTests.ReminderModule.Services.PauseReminderTests
{
    [TestFixture]
    public class ReminderEventsTests
    {
        private Mock<IRecorderService> recorderService;
        private Mock<IShellNavigator> shellNavigator;
        private Mock<IReminder> reminder;
        private PauseReminder pauseReminder;

        [SetUp]
        public void SetUp()
        {
            recorderService = new Mock<IRecorderService>();
            shellNavigator = new Mock<IShellNavigator>();
            reminder = new Mock<IReminder>();

            pauseReminder = new PauseReminder(recorderService.Object, shellNavigator.Object, reminder.Object);
        }

        [Test]
        public void Message_is_displayed_when_reminder_rings()
        {
            pauseReminder.StartMonitoring();
            shellNavigator.Setup(x => x.Navigate(ShellNames.MessageShell, It.IsAny<Dictionary<string, object>>()));

            reminder.Raise(x => x.Ring += null, new RingEventArgs());

            shellNavigator.VerifyAll();
        }

        [Test]
        public void Pause_message_displayed_for_custom_PauseInterval_value()
        {
            pauseReminder.PauseInterval = TimeSpan.FromMilliseconds(100);
            bool messageDisplayed = false;
            shellNavigator.Setup(x => x.Navigate(ShellNames.MessageShell, It.IsAny<Dictionary<string, object>>()))
                .Callback(() => messageDisplayed = true);

            pauseReminder.StartMonitoring();
            reminder.Raise(x => x.Ring += null, new RingEventArgs());

            Thread.Sleep(100 + TestConstants.TimerDelayAccepted);

            if (!messageDisplayed)
                Assert.Fail("The pause message was not displayed");
        }
    }
}
