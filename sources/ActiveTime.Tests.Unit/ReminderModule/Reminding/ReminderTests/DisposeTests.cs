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
using System.Threading;
using DustInTheWind.ActiveTime.Reminder.Module.Reminding;
using NUnit.Framework;

namespace DustInTheWind.ActiveTime.UnitTests.ReminderModule.Reminding.ReminderTests
{
    /// <summary>
    /// Dispose - TestCase 8: Initialize - Start - Dispose -> check if stop, check nothing starts
    /// </summary>
    [TestFixture]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The disposable objects are disposed in the TearDown method.")]
    public class DisposeTests
    {
        private Reminder.Module.Reminding.Reminder reminder;

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

        #region Dispose

        [Test]
        public void DisposeTwice()
        {
            reminder.Dispose();
            reminder.Dispose();
        }

        #endregion

        #region Methods

        [Test]
        public void Dispose_Start_int()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                reminder.Dispose();
                const int miliseconds = 100;
                reminder.Start(miliseconds);
            });
        }

        [Test]
        public void Dispose_Start_long()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                reminder.Dispose();
                const long miliseconds = 100;
                reminder.Start(miliseconds);
            });
        }

        [Test]
        public void Dispose_Start_uint()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                reminder.Dispose();
                const uint miliseconds = 100;
                reminder.Start(miliseconds);
            });
        }

        [Test]
        public void Dispose_Start_TimeSpan()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                reminder.Dispose();
                TimeSpan miliseconds = TimeSpan.FromMilliseconds(100);
                reminder.Start(miliseconds);
            });
        }

        [Test]
        public void Dispose_Stop()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                reminder.Dispose();
                reminder.Stop();
            });
        }

        [Test]
        public void Dispose_Reset()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                reminder.Dispose();
                reminder.Reset();
            });
        }

        [Test]
        public void Dispose_WaitUntilRing()
        {
            Assert.Throws<ObjectDisposedException>(() =>
            {
                reminder.Dispose();
                reminder.WaitUntilRing();
            });
        }

        #endregion

        #region Stop

        [Test]
        public void Dispose_StopAll()
        {
            Reminder.Module.Reminding.Reminder reminder = new Reminder.Module.Reminding.Reminder();

            reminder.Start(100);

            Thread.Sleep(50);

            reminder.Dispose();

            Assert.That(reminder.Status, Is.EqualTo(ReminderStatus.NotStarted));
        }

        #endregion
    }
}
