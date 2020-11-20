using System;
using System.Collections.Generic;
using System.Text;
using DbRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace DbRepository.Seeds
{
    public static class Users
    {
        internal static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User { Name = "Me", Age = 26 });
        }
    }
}
