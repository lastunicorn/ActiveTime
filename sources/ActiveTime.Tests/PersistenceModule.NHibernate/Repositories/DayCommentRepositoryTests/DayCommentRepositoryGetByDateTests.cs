// ActiveTime
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
using DustInTheWind.ActiveTime.Common.Persistence;
using DustInTheWind.ActiveTime.PersistenceModule.NHibernate.Repositories;
using NUnit.Framework;

namespace DustInTheWind.ActiveTime.UnitTests.PersistenceModule.NHibernate.Repositories.DayCommentRepositoryTests
{
    [TestFixture]
    public class DayCommentRepositoryGetByDateTests : RepositoryTestsBase
    {
        private DayCommentRepository dayCommentRepository;
        private DayComment dayComment1;
        private DayComment dayComment2;

        protected override void OnSetUp()
        {
            base.OnSetUp();

            dayCommentRepository = new DayCommentRepository(CurrentSession);

            dayComment1 = new DayComment
            {
                Date = new DateTime(2000, 06, 13),
                Comment = "some comment"
            };

            CurrentSession.Save(dayComment1);

            dayComment2 = new DayComment
            {
                Date = new DateTime(1990, 03, 05),
                Comment = "some comment"
            };

            CurrentSession.Save(dayComment2);
        }

        [Test]
        public void TestGetByDate_First()
        {
            DateTime date = new DateTime(2000, 06, 13);

            DayComment actualDayComment = dayCommentRepository.GetByDate(date);

            DayCommentAssertUtil.AssertAreEquals(dayComment1, actualDayComment);
        }

        [Test]
        public void TestGetByDate_Second()
        {
            DateTime date = new DateTime(1990, 03, 05);

            DayComment actualDayComment = dayCommentRepository.GetByDate(date);

            DayCommentAssertUtil.AssertAreEquals(dayComment2, actualDayComment);
        }

        [Test]
        public void TestGetByDate_Inexistent()
        {
            DateTime date = new DateTime(1980, 04, 01);

            DayComment actualDayComment = dayCommentRepository.GetByDate(date);

            Assert.That(actualDayComment, Is.Null);
        }
    }
}