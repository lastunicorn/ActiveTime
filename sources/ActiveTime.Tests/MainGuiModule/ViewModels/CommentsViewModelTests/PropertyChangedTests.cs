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
using System.Linq.Expressions;
using DustInTheWind.ActiveTime.Application;
using DustInTheWind.ActiveTime.Common.Logging;
using DustInTheWind.ActiveTime.Common.Persistence;
using DustInTheWind.ActiveTime.Common.Recording;
using DustInTheWind.ActiveTime.Common.Services;
using DustInTheWind.ActiveTime.Presentation.Services;
using DustInTheWind.ActiveTime.Presentation.ViewModels;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.ActiveTime.UnitTests.MainGuiModule.ViewModels.CommentsViewModelTests
{
    [TestFixture]
    public class PropertyChangedTests
    {
        private CommentsViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            Mock<IUnitOfWorkFactory> unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            Mock<ILogger> logger = new Mock<ILogger>();
            Mock<IRecorderService> recorderService = new Mock<IRecorderService>();
            Mock<IStatusInfoService> statusInfoService = new Mock<IStatusInfoService>();
            CurrentDay currentDay = new CurrentDay(unitOfWorkFactory.Object, logger.Object, recorderService.Object, statusInfoService.Object);

            viewModel = new CommentsViewModel(currentDay);
        }

        #region CommentTextWrap Property

        [Test]
        public void CommentTextWrap_is_initially_true()
        {
            Assert.That(viewModel.CommentTextWrap, Is.True);
        }

        [Test]
        public void CommentTextWrap_raises_PropertyChanged_event()
        {
            bool eventWasCalled = false;
            viewModel.PropertyChanged += (s, e) => { eventWasCalled = true; };

            viewModel.CommentTextWrap = false;

            Assert.That(eventWasCalled, Is.True);
        }

        [Test]
        public void CommentTextWrap_raises_PropertyChanged_event_with_correct_PropertyName()
        {
            string propertyName = null;
            viewModel.PropertyChanged += (s, e) => { propertyName = e.PropertyName; };

            viewModel.CommentTextWrap = false;

            Assert.That(propertyName, Is.EqualTo(GetNameOfMember(() => viewModel.CommentTextWrap)));
        }

        #endregion

        #region Comment Property

        [Test]
        public void Comment_raises_PropertyChanged_event()
        {
            bool eventWasCalled = false;
            viewModel.PropertyChanged += (s, e) => { eventWasCalled = true; };

            viewModel.Comment = "some comment";

            Assert.That(eventWasCalled, Is.True);
        }

        [Test]
        public void Comment_raises_PropertyChanged_event_with_correct_PropertyName()
        {
            string propertyName = null;
            viewModel.PropertyChanged += (s, e) => { propertyName = e.PropertyName; };

            viewModel.Comment = "some comment";

            Assert.That(propertyName, Is.EqualTo(GetNameOfMember(() => viewModel.Comment)));
        }

        #endregion

        private string GetNameOfMember<T>(Expression<Func<T>> action)
        {
            return ((MemberExpression)action.Body).Member.Name;
        }
    }
}