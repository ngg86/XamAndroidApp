using System.Net.Http;
using System.Net.Http.Headers;
using Android.Content;

namespace AnotherApp
{
    public static class LoginActivity
    {
        private static HttpClient _client = new HttpClient();

        public static User AttemptLogin(User user)
        {
            var url = "http://192.168.1.61:8081";
            var uri = url + "/auth/credentials?username=" + user.Name + "&password=" + user.Pass;

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var respone = _client.GetStringAsync(uri).Result;
            user.Client = _client;
            user.Url = url;

            return user;
        }

        public static void SaveUser(User user)
        {
            var savedata = Android.App.Application.Context.GetSharedPreferences("Credentials", FileCreationMode.Private);
            var dataEdit = savedata.Edit();

            dataEdit.PutString("Username", user.Name);
            dataEdit.PutString("Password", user.Pass);
            dataEdit.PutString("Url", user.Url);

            dataEdit.Commit();
        }

        public static User LoadUser()
        {
            var loaddata = Android.App.Application.Context.GetSharedPreferences("Credentials", FileCreationMode.Private);
            if (loaddata == null) return null;
            {
                string user = loaddata.GetString("Username", null);
                string pass = loaddata.GetString("Password", null);

                var usercreds = new User(user, pass) { Save = true };
                return usercreds;
            }
        }

        public static void DeleteUser()
        {
            var creds = Android.App.Application.Context.GetSharedPreferences("Credentials",
                FileCreationMode.Private);
            var credsEdit = creds.Edit();

            credsEdit.Remove("Username");
            credsEdit.Remove("Password");
            credsEdit.Commit();
        }
    }
}
