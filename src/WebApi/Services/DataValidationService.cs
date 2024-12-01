using System.Diagnostics;
using System.Text;
using WebApi.Entities;
using WebApi.Interfaces;
using WebApi.Validators;

namespace WebApi.Services
{
    public class DataValidationService : IDataValidationService
    {
        public DataValidationService()
        {
        }

        public DataFileValidity ValidateUserData(List<UserData> userDataList)
        {
            bool isFileValid = true;
            DataFileValidity dataFileValidity = new DataFileValidity();

            foreach ((int index, var userData) in userDataList.Index())
            {
                var startTime = Stopwatch.GetTimestamp();
                UserDataValidity userDataValidity = UserDataValidator.Validate(userData);
                var stopTimeDifference = Stopwatch.GetElapsedTime(startTime);

                if (!userDataValidity.IsValid)
                {
                    //If at least one line is invalid - invalidate the whole data file
                    isFileValid = false;

                    dataFileValidity.InvalidLines.Add(FormInvalidLineMessage(userDataValidity, index, stopTimeDifference));
                }
            }

            //Invalidate the file if there was no data sent
            if (userDataList == null || userDataList.Count == 0) isFileValid = false;

            dataFileValidity.FileValid = isFileValid;

            return dataFileValidity;
        }

        private string FormInvalidLineMessage(UserDataValidity userDataValidity, int lineIndex, TimeSpan timeTaken)
        {
            StringBuilder invalidLine = new StringBuilder();

            foreach ((int index, var error) in userDataValidity.Errors.Index())
            {
                if (index != 0) invalidLine.Append(", ");
                invalidLine.Append(error);
            }

            invalidLine.Append($" invalid on line {lineIndex + 1} [{userDataValidity.UserData.FirstName} {userDataValidity.UserData.Code}] (Time taken to validate: {timeTaken.Microseconds} μs)");

            return invalidLine.ToString();
        }
    }
}
