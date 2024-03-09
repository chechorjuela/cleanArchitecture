using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "1d53e2ee-ba83-4a5e-95ba-867a51907ca1",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Firstname = "Admin",
                    Lastname = "Admin",
                    UserName = "Admin",
                    NormalizedUserName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "891026"),
                    EmailConfirmed = true
                },
                 new ApplicationUser
                 {
                     Id = "b44710cd-690c-4167-8010-83e90bdd023f",
                     Email = "saor@localhost.com",
                     NormalizedEmail = "saor@localhost.com",
                     Firstname = "Sergio",
                     Lastname = "Orjuela",
                     UserName = "saor",
                     NormalizedUserName = "saor",
                     PasswordHash = hasher.HashPassword(null, "891026"),
                     EmailConfirmed = true
                 }
            );
        }
    }
}
