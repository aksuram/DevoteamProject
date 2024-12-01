using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApi.Entities;

namespace WebApi.Validators
{
    public static class UserDataValidator
    {
        public static string AlphabeticRegex { get; } = "^[A-Z][a-z]+$";
        public static string CodeRegex { get; } = "^(3|4)[0-9]{6}p?$";

        public static UserDataValidity Validate(UserData userData)
        {
            UserDataValidity userDataValidity = new UserDataValidity{ UserData = userData };

            Regex alphabeticRegexValidator = new Regex(AlphabeticRegex);
            if (userData.FirstName == null || !alphabeticRegexValidator.IsMatch(userData.FirstName))
            {
                userDataValidity.Errors.Add("Person's name");
            }

            Regex codeRegexValidator = new Regex(CodeRegex);
            if (userData.Code == null || !codeRegexValidator.IsMatch(userData.Code))
            {
                userDataValidity.Errors.Add("Code number");
            }

            return userDataValidity;
        }
    }
}
