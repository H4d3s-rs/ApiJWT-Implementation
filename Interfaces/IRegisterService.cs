using System;
using ex3___ApiLoginECors.DTO;
using ex3___ApiLoginECors.Model;

namespace ex3___ApiLoginECors.Interfaces;

public interface IRegisterService
{
    public Task<ServiceResponse<RegisterDTO>> Register(RegisterDTO register);
}