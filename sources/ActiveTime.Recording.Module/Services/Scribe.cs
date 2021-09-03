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
using DustInTheWind.ActiveTime.Common;
using DustInTheWind.ActiveTime.Common.Infrastructure;
using DustInTheWind.ActiveTime.Common.Persistence;

namespace DustInTheWind.ActiveTime.Recording.Module.Services
{
    /// <summary>
    /// Keeps track of a current record and updates it in the database when requested.
    /// </summary>
    /// <remarks>
    /// The current record can be obtained in two ways: 1) from the database or 2) by creating a new one.
    /// </remarks>
    public class Scribe : IScribe
    {
        private readonly ISystemClock systemClock;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private TimeRecord currentTimeRecord;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scribe"/> class.
        /// </summary>
        /// <param name="systemClock"></param>
        /// <param name="unitOfWorkFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Scribe(ISystemClock systemClock, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.systemClock = systemClock ?? throw new ArgumentNullException(nameof(systemClock));
            this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        /// <summary>
        /// Creates a new record and saves it in the repository.
        /// </summary>
        public void StampNew()
        {
            DateTime now = systemClock.GetCurrentTime();
            CreateNewTimeRecordAndSave(now);
        }

        private void CreateNewTimeRecordAndSave(DateTime now)
        {
            TimeRecord newTimeRecord = new TimeRecord
            {
                RecordType = TimeRecordType.Normal,
                Date = now.Date,
                StartTime = now.TimeOfDay,
                EndTime = now.TimeOfDay
            };

            using (IUnitOfWork unitOfWork = unitOfWorkFactory.CreateNew())
            {
                ITimeRecordRepository timeRecordRepository = unitOfWork.TimeRecordRepository;

                timeRecordRepository.Add(newTimeRecord);
                unitOfWork.Commit();
            }

            currentTimeRecord = newTimeRecord;
        }

        /// <summary>
        /// Updates the current record with the current time and saves it into the
        /// repository. If there is no record a new one is automatically created.
        /// </summary>
        public void Stamp()
        {
            DateTime now = systemClock.GetCurrentTime();

            if (currentTimeRecord == null)
            {
                CreateNewTimeRecordAndSave(now);
            }
            else
            {
                bool isSameDay = currentTimeRecord.Date == now.Date;

                if (isSameDay)
                {
                    UpdateCurrentTimeRecordAndSave(now.TimeOfDay);
                }
                else
                {
                    UpdateCurrentTimeRecordAndSave(new TimeSpan(0, 23, 59, 59, 999));
                    CreateNewTimeRecordAndSave(now.Date);
                    UpdateCurrentTimeRecordAndSave(now.TimeOfDay);
                }
            }
        }

        private void UpdateCurrentTimeRecordAndSave(TimeSpan timeOfDay)
        {
            using (IUnitOfWork unitOfWork = unitOfWorkFactory.CreateNew())
            {
                ITimeRecordRepository timeRecordRepository = unitOfWork.TimeRecordRepository;

                currentTimeRecord.EndTime = timeOfDay;
                timeRecordRepository.Update(currentTimeRecord);

                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Deletes from the database the current time record.
        /// If no time record was created, nothing happens.
        /// </summary>
        public void DeleteCurrentTimeRecord()
        {
            if (currentTimeRecord == null)
                return;

            using (IUnitOfWork unitOfWork = unitOfWorkFactory.CreateNew())
            {
                ITimeRecordRepository timeRecordRepository = unitOfWork.TimeRecordRepository;

                timeRecordRepository.Delete(currentTimeRecord);
            }

            currentTimeRecord = null;
        }

        /// <summary>
        /// Returns the time passed from the last time the current TimeRecord was stamped.
        /// </summary>
        /// <returns>A <see cref="TimeSpan"/> object representing the time passed from the last time the
        /// current TimeRecord was stamped.</returns>
        public TimeSpan? CalculateTimeFromLastStamp()
        {
            if (currentTimeRecord == null)
                return null;

            DateTime now = systemClock.GetCurrentTime();

            bool isSameDay = currentTimeRecord.Date == now.Date;

            if (isSameDay)
            {
                DateTime endDateTime = now.Date.Add(currentTimeRecord.EndTime);
                return now - endDateTime;
            }

            TimeSpan timeInLastDayAfterEndTime = TimeSpan.FromDays(1) - currentTimeRecord.EndTime;
            TimeSpan b = now.Date - currentTimeRecord.Date + TimeSpan.FromDays(1); // what is b?
            TimeSpan timeInCurrentDay = now.TimeOfDay;

            return timeInLastDayAfterEndTime + b + timeInCurrentDay;
        }
    }
}
