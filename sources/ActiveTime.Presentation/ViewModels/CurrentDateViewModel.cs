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
using System.Threading.Tasks;
using DustInTheWind.ActiveTime.Application.CurrentDate.ChangeDate;
using DustInTheWind.ActiveTime.Application.CurrentDate.PresentCurrentDate;
using DustInTheWind.ActiveTime.Common;
using DustInTheWind.ActiveTime.Infrastructure.EventModel;
using DustInTheWind.ActiveTime.Presentation.Commands;
using MediatR;

namespace DustInTheWind.ActiveTime.Presentation.ViewModels
{
    public class CurrentDateViewModel : ViewModelBase
    {
        private readonly IMediator mediator;

        private DateTime? date;

        public DateTime? Date
        {
            get => date;
            set
            {
                if (date == value)
                    return;

                date = value;
                OnPropertyChanged();

                if (!IsInitializeMode)
                    UpdateDate(value).Wait();
            }
        }

        public CalendarCommand CalendarCommand { get; }

        public DecrementDayCommand DecrementDayCommand { get; }

        public IncrementDateCommand IncrementDateCommand { get; }

        public CurrentDateViewModel(IMediator mediator, EventBus eventBus,
            CalendarCommand calendarCommand, DecrementDayCommand decrementDayCommand, IncrementDateCommand incrementDateCommand)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            CalendarCommand = calendarCommand ?? throw new ArgumentNullException(nameof(calendarCommand));
            DecrementDayCommand = decrementDayCommand ?? throw new ArgumentNullException(nameof(decrementDayCommand));
            IncrementDateCommand = incrementDateCommand ?? throw new ArgumentNullException(nameof(incrementDateCommand));

            eventBus.Subscribe(EventNames.CurrentDate.CurrentDateChanged, HandleCurrentDateChanged);

            _ = Initialize();
        }

        private void HandleCurrentDateChanged(EventParameters parameters)
        {
            RunInInitializeMode(() => Date = parameters.Get<DateTime>("Date"));
        }

        private async Task Initialize()
        {
            PresentCurrentDateRequest request = new PresentCurrentDateRequest();
            PresentCurrentDateResponse response = await mediator.Send(request);

            RunInInitializeMode(() => Date = response.Date);
        }

        private async Task UpdateDate(DateTime? value)
        {
            ChangeDateRequest request = new ChangeDateRequest
            {
                Date = value
            };
            await mediator.Send(request);
        }
    }
}