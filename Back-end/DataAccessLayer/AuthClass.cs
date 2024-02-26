using Back_end.Models;
using Microsoft.VisualBasic;
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
                if(!request.Password.Equals(request.ConfirmPassword))
                {
                    registrationResponse.IsSuccess = false;
                    registrationResponse.Message = "Password and Confirm password doesnot match!";
                    return registrationResponse;
                }

                // Checks if MySQL Database Connection is Open or not
                if(mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"insert into crudoperations.userdetails (username,password,role) values (@username, @password, @role)";
                using (MySqlCommand command = new MySqlCommand(SqlQuery,mySqlConnection))
                {
                    command.CommandType=System.Data.CommandType.Text;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue("@username", request.UserName);
                    command.Parameters.AddWithValue("@password", request.Password);
                    command.Parameters.AddWithValue("@role", request.Role);
                    int status=await command.ExecuteNonQueryAsync();
                    if(status <= 0)
                    {
                        registrationResponse.IsSuccess = false;
                        registrationResponse.Message = "Something went wrong";
                        return registrationResponse;
                    }
                }
            }
            catch(Exception e)
            {
                registrationResponse.IsSuccess = false;
                registrationResponse.Message = e.Message; 
            }
            finally
            {
                await mySqlConnection.CloseAsync();
                await mySqlConnection.DisposeAsync();
            }
            return registrationResponse;
        }
    }
}
