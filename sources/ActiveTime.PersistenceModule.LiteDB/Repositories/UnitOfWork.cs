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
using LiteDB;

namespace DustInTheWind.ActiveTime.PersistenceModule.LiteDB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private const string ConnectionString = Constants.DatabaseFileName;

        private LiteDatabase connection;
        private LiteTransaction transaction;

        public LiteDatabase Connection
        {
            get
            {
                if (disposed)
                    throw new ObjectDisposedException("UnitOfWork");

                if (connection == null)
                    connection = new LiteDatabase(ConnectionString);

                if (transaction == null)
                    transaction = connection.BeginTrans();

                return connection;
            }
        }

        public void Commit()
        {
            if (disposed)
                throw new ObjectDisposedException("UnitOfWork");

            if (transaction == null)
                return;

            transaction.Commit();

            transaction.Dispose();
            transaction = null;
        }

        public void Rollback()
        {
            if (disposed)
                throw new ObjectDisposedException("UnitOfWork");

            if (transaction == null)
                return;

            transaction.Rollback();

            transaction.Dispose();
            transaction = null;
        }

        //public void ExecuteCommand(Action<DbCommand> action)
        //{
        //    using (DbCommand command = Connection.CreateCommand())
        //    {
        //        action(command);
        //    }
        //}

        //public T ExecuteCommand<T>(Func<DbCommand, T> action)
        //{
        //    using (DbCommand command = Connection.CreateCommand())
        //    {
        //        return action(command);
        //    }
        //}

        public void ExecuteAndCommit(Action action)
        {
            try
            {
                action();
                Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        //public void ExecuteCommandAndCommit(Action<DbCommand> action)
        //{
        //    try
        //    {
        //        using (DbCommand command = Connection.CreateCommand())
        //        {
        //            action(command);
        //        }

        //        Commit();
        //    }
        //    catch
        //    {
        //        Rollback();
        //        throw;
        //    }
        //}

        //public T ExecuteCommandAndCommit<T>(Func<DbCommand, T> action)
        //{
        //    try
        //    {
        //        T returnValue;

        //        using (DbCommand command = Connection.CreateCommand())
        //        {
        //            returnValue = action(command);
        //        }

        //        Commit();

        //        return returnValue;
        //    }
        //    catch
        //    {
        //        Rollback();
        //        throw;
        //    }
        //}

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }

                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }
            }

            disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
