using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

using ex3___ApiLoginECors.Data;
using ex3___ApiLoginECors.DTO;
using ex3___ApiLoginECors.Interfaces;
using ex3___ApiLoginECors.Model;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ex3___ApiLoginECors.Services;

public class LoginService : ILoginService
{

    private readonly AppData _context;
    private readonly IConfiguration _config;

    public LoginService(AppData context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<ServiceResponse<ResponseDTO>> Login(LoginDTO login)
    {
        ServiceResponse<ResponseDTO> serviceResponse = new ServiceResponse<ResponseDTO>();

        try{
            UserModel userToLogin = await _context.User.FirstOrDefaultAsync(x => x.Email.ToLower() == login.Email.ToLower());

            if(userToLogin == null)
            {
                serviceResponse.data = null;
                serviceResponse.message = "User not found!";
                serviceResponse.success = true;
                return serviceResponse;

            }

            using var hmac = new HMACSHA512(userToLogin.PasswordSalt);
            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {
                if(userToLogin.PasswordHash[i] != computedHash[i])
                {
                   serviceResponse.data = null;
                   serviceResponse.message = "Password invaid!";
                   serviceResponse.success = false; 
                   return serviceResponse;

                }
            }


            string tokenKey = _config["TokenKey"];

            if(tokenKey == null)
            {
                return null;

            }

            if(tokenKey.Length < 64)
            {
                return null;

            }


            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, userToLogin.Email), 
                new(ClaimTypes.NameIdentifier, userToLogin.Id), 

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var signedKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signedKey,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            var returnUser = new LoginDTO
            {
                Email = login.Email,
                Password = login.Password,
                Token = token.ToString(),

            };

            serviceResponse.data = returnUser;
            serviceResponse.message = "User valid!";
            serviceResponse.success = true;

        }catch (Exception ex){
            serviceResponse.data = null;
            serviceResponse.message = ex.Message;
            serviceResponse.success = false;

        }

        return serviceResponse;
    }
}