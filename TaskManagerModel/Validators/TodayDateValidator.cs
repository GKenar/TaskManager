using System;
using FluentValidation;

namespace TaskManagerModel.Validators
{
    class TodayDateValidator : AbstractValidator<DateTime>
    {
        public TodayDateValidator()
        {
            RuleFor(date => date).Must(BeAValidTodayDate); //.WithMessage("should be without time")
        }

        private bool BeAValidTodayDate(DateTime date)
        {
            return date.TimeOfDay == TimeSpan.Zero;
        }
    }
}
