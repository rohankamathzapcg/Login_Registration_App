using Back_end.Models;

namespace Back_end.DataAccessLayer
{
    public interface AuthInterFace
    {
        public Task<RegistrationResponse> Registration(RegistrationRequest request);
        public Task<LoginResponse> Login(LoginRequests request);
    }
}
