using System;
using System.ComponentModel.DataAnnotations;
namespace ex3___ApiLoginECors.Model;

public class UserModel
{

    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [EmailAddress]
    public required string Email { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }
    
}

