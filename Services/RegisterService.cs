using System.Text;
using System.Security.Cryptography;
using ex3___ApiLoginECors.Data;
using ex3___ApiLoginECors.DTO;
using ex3___ApiLoginECors.Interfaces;
using ex3___ApiLoginECors.Model;
using Microsoft.EntityFrameworkCore;


namespace ex3___ApiLoginECors.Services;

public class RegisterService : IRegisterService
{
    private readonly AppData _context;

    public RegisterService(AppData context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<RegisterDTO>> Register(RegisterDTO register)
    {
        ServiceResponse<RegisterDTO> serviceResponse = new ServiceResponse<RegisterDTO>();
        using var hmac = new HMACSHA512();

        try{
            var test = await _context.User.FirstOrDefaultAsync(x => x.Email == register.Email);

            if(test != null)
            {
                serviceResponse.data = null;
                serviceResponse.message = "Couldn't be registered";
                serviceResponse.success = false;
                return serviceResponse;

            }


            UserModel userToAdd = new UserModel
            {
                Email = register.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key,

            };

            await _context.User.AddAsync(userToAdd);
            await _context.SaveChangesAsync();

            serviceResponse.data = register;
            serviceResponse.message = "User Added";
            serviceResponse.success = true;

        }catch (Exception ex){
            serviceResponse.data = null;
            serviceResponse.message = ex.Message;
            serviceResponse.success = false;

        }

        return serviceResponse;
    }
}