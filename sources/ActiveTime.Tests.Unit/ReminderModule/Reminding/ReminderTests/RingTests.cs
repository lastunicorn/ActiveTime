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
    /// Ring - TestCase 3: Initialize - Start - Ring -> check is raised, check time, check status
    /// </summary>
    [TestFixture]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The disposable objects are disposed in the TearDown method.")]
    public class RingTests
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

        #region Start int

        [Test]
        [Description("Checks if the Ring event is raised on time.")]
        [Timeout(200)]
        public void Start_int_RingTime()
        {
            const int ringMiliseconds = 100;

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                DateTime startTime;
                DateTime ringTime;

                reminder.Ring += (sender, e) =>
                {
                    ringTime = DateTime.Now;
                    ringEvent.Set();
                };

                startTime = DateTime.Now;
                ringTime = DateTime.MinValue;

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                    Assert.That(ringTime - startTime, Is.EqualTo(TimeSpan.FromMilliseconds(ringMiliseconds)).Within(TimeSpan.FromMilliseconds(TestConstants.TimeErrorAccepted)));
            }
        }

        [Test]
        [Description("Checks if the Status is correctly set after the Ring event is raised.")]
        [Timeout(200)]
        public void Start_int_Ring_Status()
        {
            const int ringMiliseconds = 100;

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                ReminderStatus status = ReminderStatus.NotStarted;

                reminder.Ring += (sender, e) =>
                {
                    if (sender != null)
                        status = ((Reminder.Module.Reminding.Reminder) sender).Status;

                    ringEvent.Set();
                };

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                    Assert.That(status, Is.EqualTo(ReminderStatus.Stopping));
            }
        }

        #endregion

        #region Start long

        [Test]
        [Description("Checks if the Ring event is raised on time.")]
        [Timeout(200)]
        public void Start_long_RingTime()
        {
            const long ringMiliseconds = 100;

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                DateTime startTime;
                DateTime ringTime;

                reminder.Ring += (sender, e) =>
                {
                    ringTime = DateTime.Now;
                    ringEvent.Set();
                };

                startTime = DateTime.Now;
                ringTime = DateTime.MinValue;

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                {
                    Assert.That(ringTime - startTime, Is.EqualTo(TimeSpan.FromMilliseconds(ringMiliseconds)).Within(TimeSpan.FromMilliseconds(TestConstants.TimeErrorAccepted)));
                }
            }
        }

        [Test]
        [Description("Checks if the Status is correctly set after the Ring event is raised.")]
        [Timeout(200)]
        public void Start_long_Ring_Status()
        {
            const long ringMiliseconds = 100;

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                ReminderStatus status = ReminderStatus.NotStarted;

                reminder.Ring += (sender, e) =>
                {
                    if (sender != null)
                    {
                        status = ((Reminder.Module.Reminding.Reminder)sender).Status;
                    }
                    ringEvent.Set();
                };

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                {
                    Assert.That(status, Is.EqualTo(ReminderStatus.Stopping));
                }
            }
        }

        #endregion

        #region Start uint

        [Test]
        [Description("Checks if the Ring event is raised on time.")]
        [Timeout(200)]
        public void Start_uint_RingTime()
        {
            const uint ringMiliseconds = 100;

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                DateTime startTime;
                DateTime ringTime;

                reminder.Ring += (sender, e) =>
                {
                    ringTime = DateTime.Now;
                    ringEvent.Set();
                };

                startTime = DateTime.Now;
                ringTime = DateTime.MinValue;

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                    Assert.That(ringTime - startTime, Is.EqualTo(TimeSpan.FromMilliseconds(ringMiliseconds)).Within(TimeSpan.FromMilliseconds(TestConstants.TimeErrorAccepted)));
            }
        }

        [Test]
        [Description("Checks if the Status is correctly set after the Ring event is raised.")]
        [Timeout(200)]
        public void Start_uint_Ring_Status()
        {
            const uint ringMiliseconds = 100;

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                ReminderStatus status = ReminderStatus.NotStarted;

                reminder.Ring += (sender, e) =>
                {
                    if (sender != null)
                    {
                        status = ((Reminder.Module.Reminding.Reminder)sender).Status;
                    }
                    ringEvent.Set();
                };

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                    Assert.That(status, Is.EqualTo(ReminderStatus.Stopping));
            }
        }

        #endregion

        #region Start TimeSpan

        [Test]
        [Description("Checks if the Ring event is raised on time.")]
        [Timeout(200)]
        public void Start_TimeSpan_RingTime()
        {
            TimeSpan ringMiliseconds = TimeSpan.FromMilliseconds(100);

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                DateTime ringTime;

                reminder.Ring += (sender, e) =>
                {
                    ringTime = DateTime.Now;
                    ringEvent.Set();
                };

                DateTime startTime = DateTime.Now;
                ringTime = DateTime.MinValue;

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                    Assert.That(ringTime - startTime, Is.EqualTo(ringMiliseconds).Within(TimeSpan.FromMilliseconds(TestConstants.TimeErrorAccepted)));
            }
        }

        [Test]
        [Description("Checks if the Status is correctly set after the Ring event is raised.")]
        [Timeout(200)]
        public void Start_TimeSpan_Ring_Status()
        {
            TimeSpan ringMiliseconds = TimeSpan.FromMilliseconds(100);

            using (ManualResetEvent ringEvent = new ManualResetEvent(false))
            {
                ReminderStatus status = ReminderStatus.NotStarted;

                reminder.Ring += (sender, e) =>
                {
                    if (sender != null)
                    {
                        status = ((Reminder.Module.Reminding.Reminder)sender).Status;
                    }
                    ringEvent.Set();
                };

                reminder.Start(ringMiliseconds);

                if (ringEvent.WaitOne())
                    Assert.That(status, Is.EqualTo(ReminderStatus.Stopping));
            }
        }

        #endregion
    }
}
