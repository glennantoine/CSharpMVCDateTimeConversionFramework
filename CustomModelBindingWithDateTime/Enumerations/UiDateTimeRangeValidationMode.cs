using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomModelBindingWithDateTime.Enumerations 
{
    public enum UiDateTimeRangeValidationMode 
    {
        StartDateNotInFuture,
        StartDateNotInPast,
        EndDateNotInFuture,
        EndDateNotInPast,
        StartDateNotGreaterThanEndDate
    }
}