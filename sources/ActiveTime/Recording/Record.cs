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

namespace DustInTheWind.ActiveTime.Recording
{
    /// <summary>
    /// Represents an interval of time within a day.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// The date for which this record is created.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Gets the date for which this record is created.
        /// </summary>
        public DateTime Date
        {
            get { return date; }
        }


        /// <summary>
        /// The time of day representing the start time.
        /// </summary>
        private DayTimeInterval timeInterval;

        /// <summary>
        /// Gets the time of day representing the start time.
        /// </summary>
        public DayTimeInterval TimeInterval
        {
            get { return timeInterval; }
        }


        /// <summary>
        /// Gets the time of day representing the start time.
        /// </summary>
        public TimeSpan StartTime
        {
            get { return timeInterval.StartTime; }
            set { timeInterval.StartTime = value; }
        }


        /// <summary>
        /// Gets or sets the time of day representing the end time.
        /// </summary>
        public TimeSpan EndTime
        {
            get { return timeInterval.EndTime; }
            set { timeInterval.EndTime = value; }
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        /// <param name="date">The date for which this record is created.</param>
        /// <param name="startTime">The time of day representing the start time.</param>
        public Record(DateTime date, TimeSpan startTime)
            : this(date, startTime, startTime)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        /// <param name="date">The date for which this record is created.</param>
        /// <param name="startTime">The time of day representing the start time.</param>
        /// <param name="endTime">The time of day representing the end time.</param>
        public Record(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            this.date = date;
            this.timeInterval = new DayTimeInterval(startTime, endTime);
        }

        #endregion

        public DateTime GetStartDateTime()
        {
            return Date + StartTime;
        }

        public DateTime GetEndDateTime()
        {
            return Date + EndTime;
        }
    }
}
