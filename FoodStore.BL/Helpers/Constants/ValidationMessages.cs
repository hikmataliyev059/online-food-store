namespace FoodStore.BL.Helpers.Constants;

public static class ValidationMessages
{
    public const string Required = "{PropertyName} cannot be empty or null";
    public const string MinLength = "Enter a minimum of {MinLength} characters";
    public const string MaxLength = "Enter a maximum of {MaxLength} characters";
    public const string InvalidEmail = "Enter the email type correctly";
    public const string PasswordCriteria = "Enter the correct password type";
    public const string PasswordMismatch = "Passwords don't match";
    public const string EmailOrUsernameRequired = "Email or Username cannot be empty or null";
    public const string PasswordRequired = "Password cannot be empty or null";
}