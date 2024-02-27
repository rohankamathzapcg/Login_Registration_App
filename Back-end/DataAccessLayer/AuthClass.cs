using Back_end.Models;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data.Common;

namespace Back_end.DataAccessLayer
{
    //For MYSQL
    //"MySqlDBConnection": "Server=localhost;User=root;Password='Rohan@1234';Database=crudoperations; SslMode=None; Port=3306; Allow User Variables=True;"

    //For PostgresSQL
    //"WebApiPostgresDatabase": "Host=localhost; Database=Webapplication; Username=postgres; Password=1234"
    public class AuthClass : AuthInterFace
    {
        public readonly IConfiguration configuration;
        //for PostgresSQL
        private readonly NpgsqlConnection _npgsqlConnection;
        //for MYSQL
        //public readonly MySqlConnection mySqlConnection;
        public AuthClass(IConfiguration configuration)
        {
            this.configuration = configuration;
            _npgsqlConnection = new NpgsqlConnection(configuration["ConnectionStrings:WebApiPostgresDatabase"]);
            
            //mySqlConnection = new MySqlConnection(configuration["ConnectionStrings:MySqlDBConnection"]);
        }

        public async Task<LoginResponse> Login(LoginRequests request)
        {
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.IsSuccess = true;
            loginResponse.Message = "Logged In Successfully";

            try
            {
                // Checks if MySQL Database Connection is Open or not
                //if (mySqlConnection.State != System.Data.ConnectionState.Open)
                //{
                    //await mySqlConnection.OpenAsync();
                //}

                // Checks if PostgreSQL Database Connection is Open or not
                if (_npgsqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _npgsqlConnection.OpenAsync();
                }

                string sqlQuery = @"SELECT * FROM userdetails WHERE username=@username and password=@password and role=@role";
                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, _npgsqlConnection))
                //using (MySqlCommand command = new MySqlCommand(sqlQuery, mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue("@username", request.UserName);
                    command.Parameters.AddWithValue("@password", request.Password);
                    command.Parameters.AddWithValue("@role", request.Role);

                    //Getting the row and stoing it in reader object
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            loginResponse.IsSuccess = true;
                            loginResponse.Message = "Logged In Successfully!";
                        }
                        else
                        {
                            loginResponse.IsSuccess = false;
                            loginResponse.Message = "Invalid username or password";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loginResponse.IsSuccess = false;
                loginResponse.Message = ex.Message;
            }
            finally
            {
                await _npgsqlConnection.CloseAsync();
                await _npgsqlConnection.DisposeAsync();

                //await mySqlConnection.CloseAsync();
                //await mySqlConnection.DisposeAsync();
            }
            return loginResponse;

        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            RegistrationResponse registrationResponse = new RegistrationResponse(); ;
            registrationResponse.IsSuccess = true;
            registrationResponse.Message = "Registered Successfully";

            try
            {
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    registrationResponse.IsSuccess = false;
                    registrationResponse.Message = "Password and Confirm password doesnot match!";
                    return registrationResponse;
                }

                // Checks if MySQL Database Connection is Open or not
                //if (mySqlConnection.State != System.Data.ConnectionState.Open)
                //{
                //await mySqlConnection.OpenAsync();
                //}

                // Checks if PostgreSQL Database Connection is Open or not
                if (_npgsqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _npgsqlConnection.OpenAsync();
                }

                string sqlQuery = @"INSERT INTO userdetails (username,password,role) VALUES (@username, @password, @role)";
                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, _npgsqlConnection))
                //using (MySqlCommand command = new MySqlCommand(sqlQuery, mySqlConnection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandTimeout = 180;
                    command.Parameters.AddWithValue("@username", request.UserName);
                    command.Parameters.AddWithValue("@password", request.Password);
                    command.Parameters.AddWithValue("@role", request.Role);
                    int status = await command.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        registrationResponse.IsSuccess = false;
                        registrationResponse.Message = "Something went wrong";
                        return registrationResponse;
                    }
                }
            }
            catch (Exception e)
            {
                registrationResponse.IsSuccess = false;
                registrationResponse.Message = e.Message;
            }
            finally
            {
                await _npgsqlConnection.CloseAsync();
                await _npgsqlConnection.DisposeAsync();

                //await mySqlConnection.CloseAsync();
                //await mySqlConnection.DisposeAsync();
            }
            return registrationResponse;
        }
    }
}
