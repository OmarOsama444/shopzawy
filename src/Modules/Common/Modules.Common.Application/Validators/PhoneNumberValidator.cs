using PhoneNumbers;

namespace Modules.Common.Application.Validators;


public class PhoneNumberValidator
{
    private string? region = null;
    private string? countryName = null;
    public PhoneNumberValidator()
    {

    }
    public PhoneNumberValidator(string region, string countryName)
    {
        this.region = region;
        this.countryName = countryName;
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
    public string Message => countryName is null ?
        "invalid phonenumber" :
        $"invalid {countryName} phone number";
}