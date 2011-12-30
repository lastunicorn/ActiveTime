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
using System.Threading;
using DustInTheWind.ActiveTime.Common;

namespace DustInTheWind.ActiveTime.MainGuiModule.Services
{
    /// <summary>
    /// A service that stores different status messages.
    /// </summary>
    class StatusInfoService : IStatusInfoService
    {
        #region Event StatusTextChanged

        /// <summary>
        /// Event raised when the status text is changed.
        /// </summary>
        public event EventHandler StatusTextChanged;

        /// <summary>
        /// Raises the <see cref="StatusTextChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected virtual void OnStatusTextChanged(EventArgs e)
        {
            if (StatusTextChanged != null)
            {
                StatusTextChanged(this, e);
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusInfoService"/> class.
        /// </summary>
        public StatusInfoService()
        {
            statusText = DEFAULT_STATUS_TEXT;
            timerStatus = new Timer(ResetStatusTextTh);
        }

        #endregion


        #region Status Text

        /// <summary>
        /// The default Text of the status.
        /// </summary>
        public const string DEFAULT_STATUS_TEXT = "Ready";

        /// <summary>
        /// The text representing the status.
        /// </summary>
        private string statusText;

        /// <summary>
        /// Gets or sets the text representing the status.
        /// </summary>
        public string StatusText
        {
            get { return statusText; }
            set
            {
                statusText = value;
                OnStatusTextChanged(EventArgs.Empty);
            }
        }

        #endregion


        #region Status Reset Timer

        /// <summary>
        /// The default time in miliseconds after which the status text will be reset to its default value.
        /// </summary>
        private const int DEFAULT_STATUS_TIMEOUT = 5000;

        /// <summary>
        /// Timer used to reset the status Text.
        /// </summary>
        private readonly Timer timerStatus;

        /// <summary>
        /// The call-back method of the timer that resets the status to the default Text.
        /// </summary>
        /// <param name="o">Unused</param>
        private void ResetStatusTextTh(object o)
        {
            StatusText = DEFAULT_STATUS_TEXT;
        }

        #endregion

        /// <summary>
        /// Sets the status of the model to the specified Text and
        /// starts the timer that will reset it back to the default one.
        /// </summary>
        /// <param name="text">The Text to be set as status.</param>
        /// <param name="timeout">The Time in miliseconds after which the status will be reset to the default Text. If this Value is 0, the status will never be reset.</param>
        public void SetStatus(string text, int timeout)
        {
            StatusText = text;
            if (timeout > 0)
            {
                timerStatus.Change(timeout, -1);
            }
        }

        public void SetStatus(string text)
        {
            SetStatus(text, DEFAULT_STATUS_TIMEOUT);
        }
    }
}