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
using System.Collections.Generic;

namespace DustInTheWind.ActiveTime.Common.Persistence
{
    public interface IDayCommentRepository
    {
        /// <summary>
        /// When implemented in a class, adds a new comment record into the database.
        /// </summary>
        /// <param name="comment">The <see cref="DayComment"/> object containing the data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        void Add(DayComment comment);

        /// <summary>
        ///  When implemented in a class, updates an existing comment into the database.
        /// </summary>
        /// <param name="comment">The <see cref="DayComment"/> object containing the data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        void Update(DayComment comment);

        /// <summary>
        /// If the comment record is new, it is added into the database. If not, the record is updated.
        /// A record is considered new if its id is zero.
        /// </summary>
        /// <param name="comment">The <see cref="DayComment"/> object containing the data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        void AddOrUpdate(DayComment comment);

        /// <summary>
        ///  When implemented in a class, deletes an existing comment record from the database.
        /// </summary>
        /// <param name="comment">The <see cref="DayComment"/> object containing the data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        void Delete(DayComment comment);

        /// <summary>
        ///  When implemented in a class, returns from the database the comment with the specified id.
        /// </summary>
        /// <param name="id">The id of the comment to return.</param>
        /// <returns>An instance of <see cref="DayComment"/> containing the comment from the database.</returns>
        DayComment GetById(int id);

        /// <summary>
        ///  When implemented in a class, returns from the database the comment for the specified date.
        /// </summary>
        /// <param name="date">The date of the comment to return.</param>
        /// <returns>An instance of <see cref="DayComment"/> containing the comment from the database.</returns>
        DayComment GetByDate(DateTime date);

        List<DayComment> GetByDate(DateTime startDate, DateTime endDate);

        /// <summary>
        ///  When implemented in a class, returns from the database all the comments.
        /// </summary>
        /// <returns>A list of <see cref="DayComment"/> containing all the requested comments.</returns>
        IList<DayComment> GetAll();
    }
}