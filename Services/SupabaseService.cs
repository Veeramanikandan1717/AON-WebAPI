namespace ArulOliNagar.Services
{
    using Supabase;
    using Supabase.Gotrue;

    public class SupabaseService
    {
        private Supabase.Client Client;

        public SupabaseService(IConfiguration appsettingconfig)
        {
            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
              
            };

            Client = new Supabase.Client(
                appsettingconfig["Supabase:ApiUrl"],
                appsettingconfig["Supabase:AnonKey"],
                options
            );
        }

        public async Task<User> SignUpAsync(string email, string password)
        {
            var user = await Client.Auth.SignUp(email, password);
            return user.User;
        }

        public async Task<User> SignInAsync(string email, string password)
        {
            var session = await Client.Auth.SignInWithPassword(email, password);
            return session.User;
        }

        public async Task SignOutAsync()
        {
            await Client.Auth.SignOut();
        }

        public User? GetCurrentUser()
        {
            return Client.Auth.CurrentUser;
        }
    }

}
