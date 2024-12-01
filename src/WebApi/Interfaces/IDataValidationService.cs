using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IDataValidationService
    {
        public DataFileValidity ValidateUserData(List<UserData> userDataList);
    }
}
