using PhoneNumbers;

namespace Modules.Common.Application.Validators;


public class PhoneNumberValidator
{
    private readonly string? region = null;
    public PhoneNumberValidator()
    {
    }
    public PhoneNumberValidator(string? region)
    {
        this.region = region;
    }
    public bool Must(string phoneNumber)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        try
        {
            PhoneNumber parsedNumber = phoneUtil.Parse(phoneNumber, region);
            return phoneUtil.IsValidNumber(parsedNumber);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
    public static string Message => "invalid phonenumber";
}