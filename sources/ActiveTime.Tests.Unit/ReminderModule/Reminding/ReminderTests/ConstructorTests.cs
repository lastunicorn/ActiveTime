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
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace DustInTheWind.ActiveTime.UnitTests.ReminderModule.Reminding.ReminderTests
{
    /// <summary>
    /// Initialization - TestCase 1: Initialize -> check status
    /// </summary>
    [TestFixture]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The disposable objects are disposed in the TearDown method.")]
    public class ConstructorTests
    {
        private ReminderJob reminder;

        [SetUp]
        public void SetUp()
        {
            reminder = new Reminder.Module.Reminding.Reminder();
        }

        [TearDown]
        public void TearDown()
        {
            reminder.Dispose();
        }

        [Test]
        public void Initialization()
        {
        }

        [Test]
        public void InitialState_Status()
        {
            Assert.That(reminder.Status, Is.EqualTo(ReminderStatus.NotStarted));
        }

        [Test]
        public void InitialState_StartTime()
        {
            Assert.That(reminder.StartTime, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void InitialState_SnoozeTime()
        {
            Assert.That(reminder.DefaultSnoozeTime, Is.EqualTo(TimeSpan.FromMinutes(1)));
        }
    }
}
