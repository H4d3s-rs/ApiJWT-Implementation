using System;
using ex3___ApiLoginECors.Model;
using Microsoft.EntityFrameworkCore;

namespace ex3___ApiLoginECors.Data;

public class AppData : DbContext
{
    public AppData(DbContextOptions options) : base(options)
    {
        

    }

    public DbSet<UserModel> User { get; set; }

}
