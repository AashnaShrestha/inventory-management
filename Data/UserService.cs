using System.Text.Json;

namespace Coursework.Data
{
    public static class UserService
    {
        public const string SeedUsername = "admin";
        public const string SeedPassword = "admin";


        public static List<User> GetAllUsers()
        {
            string appUsersFilePath = Utils.GetAppUsersFilePath();
            if (!File.Exists(appUsersFilePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<User>>(json);
        }
        private static void SaveAllUsers(List<User> users)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appUsersFilePath = Utils.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(appUsersFilePath, json);
        }
        public static List<User> CreateUser(Guid userId, string username, string password, Role role)
        {
            List<User> users = GetAllUsers();
            bool usernameExists = users.Any(x => x.Username == username);

            if (usernameExists)
            {
                throw new Exception("Username already exists.");
            }

            users.Add(
                new User
                {
                    Username = username,
                    Password = password,
                    Role = role,
                    CreatedBy = userId
                }
            );
            SaveAllUsers(users);
            return users;
        }
        public static void SeedUsers()
        {
            var users = GetAllUsers().FirstOrDefault(x => x.Role == Role.Admin);

            if (users == null)
            {
                CreateUser(Guid.Empty, SeedUsername, SeedPassword, Role.Admin);
            }
        }

        public static User Login(string username, string password)
        {
            //var loginErrorMessage = "Invalid username or password.";
            List<User> users = GetAllUsers();
            User user = users.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            //bool passwordIsValid = Utils.VerifyHash(password, user.PasswordHash);

            if (password!= user.Password)
            {
              throw new Exception("Incorrect Password");
            }

            return user;
        }
    }
}
