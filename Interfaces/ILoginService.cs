using System;
using ex3___ApiLoginECors.Model;
using ex3___ApiLoginECors.DTO;

namespace ex3___ApiLoginECors.Interfaces;

public interface ILoginService
{   
    public Task<ServiceResponse<ResponseDTO>> Login(LoginDTO login);

} 
