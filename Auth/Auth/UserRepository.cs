using LiteDB;

namespace Auth
{
    public class UserRepository
    {
        private string _connString;

        public UserRepository(IConfiguration connString)
        {
            _connString = connString.GetConnectionString("UserDb") ?? throw new Exception("UserDb missing");
        }

        public User GetUser(string username)
        {
            using var db = new LiteDatabase(_connString);

            var col = db.GetCollection<User>("users");

            return col.FindOne(x => x.Username == username);
        }

        internal void Save(User user)
        {
            if (GetUser(user.Username) is not null)
            {
                throw new Exception("User wtih this username already exist");
            }

            using var db = new LiteDatabase(_connString);

            var col = db.GetCollection<User>("users");
            col.Insert(user);
        }
    }
}
