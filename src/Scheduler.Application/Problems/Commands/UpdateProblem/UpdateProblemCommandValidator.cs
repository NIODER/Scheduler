using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Application.Problems.Commands.UpdateProblem
{
    internal class UpdateProblemCommandValidator : AbstractValidator<UpdateProblemCommand>
    {
        public UpdateProblemCommandValidator()
        {

        }
    }
}
