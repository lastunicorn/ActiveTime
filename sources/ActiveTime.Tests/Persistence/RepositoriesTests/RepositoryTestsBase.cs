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

using DustInTheWind.ActiveTime.Persistence;
using NHibernate;
using NUnit.Framework;

namespace DustInTheWind.ActiveTime.UnitTests.Persistence.RepositoriesTests
{
    public abstract class RepositoryTestsBase
    {
        private ISession currentSession;

        protected ISession CurrentSession
        {
            get
            {
                if (currentSession == null)
                {
                    currentSession = SessionProvider.OpenSession();
                }

                return currentSession;
            }
        }

        [SetUp]
        public void SetUp()
        {
            // Force to create a session.
            ITransaction transaction = CurrentSession.BeginTransaction();

            OnSetUp();
        }

        protected virtual void OnSetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            CurrentSession.Transaction.Rollback();
            CurrentSession.Close();
            currentSession = null;

            OnTearDown();
        }

        protected virtual void OnTearDown()
        {
        }
    }
}
