using System;

namespace ex3___ApiLoginECors.DTO;

public class LoginDTO
{
    public required string Email { get; set; }

    public required string Password { get; set; }

}