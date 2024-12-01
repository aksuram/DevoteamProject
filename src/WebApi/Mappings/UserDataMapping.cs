using WebApi.Entities;

namespace WebApi.Mappings
{
    public static class UserDataMapping
    {
        public static List<UserData> FromRequestBodyDataToUserDataList(string requestBodyData) 
        {
            //Using "ReplaceLineEndings" fixes issues with different OS line ending conventions ("\r\n", "\n")
            string[] dataLines = requestBodyData
                .ReplaceLineEndings()
                .Split(Environment.NewLine, StringSplitOptions.None);

            List<UserData> userDataList = new List<UserData>();
            foreach (string line in dataLines)
            {
                string[] userData = line.Split(' ', StringSplitOptions.None);

                if (userData == null || userData.Length == 0)
                {
                    userDataList.Add(new UserData());
                    continue;
                }

                if (userData.Length == 1)
                {
                    userDataList.Add(new UserData{ FirstName = userData[0] });
                    continue;
                }

                userDataList.Add(new UserData{ FirstName = userData[0], Code = userData[1] });
            }

            return userDataList;
        }
    }
}
