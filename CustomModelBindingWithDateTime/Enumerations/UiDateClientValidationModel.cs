
namespace CustomModelBindingWithDateTime.Enumerations 
{
    public enum UiDateClientValidationModes
    {
        DateRequired,
        DateValidation,
        DateGreaterThanAttributeValidation,
        DateNotInFutureValidation,
        DateNotInPastValidation,
    }

    public enum UiTimeClientValidationModes
    {
        TimeRequired,
        TimeValidation,
        TimeGreaterThanAttributeValidation,
        TimeGreaterThanEqualAttributeValidation,
    }

    
}