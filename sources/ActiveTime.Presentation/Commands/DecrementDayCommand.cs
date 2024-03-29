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
using DustInTheWind.ActiveTime.Application.CurrentDate.DecrementDate;
using MediatR;

namespace DustInTheWind.ActiveTime.Presentation.Commands
{
    public class DecrementDayCommand : CommandBase
    {
        private readonly IMediator mediator;

        public DecrementDayCommand(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _ = IncrementDate();
        }

        private async Task IncrementDate()
        {
            DecrementDateRequest request = new DecrementDateRequest();
            await mediator.Send(request);
        }
    }
}