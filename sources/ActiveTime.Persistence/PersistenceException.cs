﻿// ActiveTime
// Copyright (C) 2011-2017 Dust in the Wind
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
using System.Runtime.Serialization;
using DustInTheWind.ActiveTime.Common;

namespace DustInTheWind.ActiveTime.Persistence
{
    /// <summary>
    /// Exception raised by an Exporter.
    /// </summary>
    [Serializable]
    public class PersistenceException : ActiveTimeException
    {
        private const string DefaultMessage = "Internal error in the persistence layer.";

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class.
        /// </summary>
        public PersistenceException()
            : base(DefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public PersistenceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class with a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public PersistenceException(Exception innerException)
            : base(DefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public PersistenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public PersistenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}