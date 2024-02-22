using Back_end.Models;
using MySql.Data.MySqlClient;

namespace Back_end.DataAccessLayer
{
    public class AuthClass : AuthInterFace
    {
        public readonly IConfiguration configuration;
        public readonly MySqlConnection mySqlConnection;
        public AuthClass(IConfiguration configuration)
        {
            this.configuration = configuration;
            mySqlConnection = new MySqlConnection(configuration["ConnectionStrings:MySqlDBConnection"]);
        }

        public Task<LoginResponse> Login(LoginRequests request)
        {
            throw new NotImplementedException();
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            RegistrationResponse registrationResponse = new RegistrationResponse(); ;
            registrationResponse.IsSuccess = true;
            registrationResponse.Message = "Registered Sucessfully";
            try
            {
                // Checks if MySQL Database Connection is Open or not
                if(mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await mySqlConnection.OpenAsync();
                }

                using (MySqlCommand command = new MySqlCommand()) ;
            }
            catch(Exception e)
            {
                registrationResponse.IsSuccess = false;
                registrationResponse.Message = e.Message; 
            }
        }
    }
}
