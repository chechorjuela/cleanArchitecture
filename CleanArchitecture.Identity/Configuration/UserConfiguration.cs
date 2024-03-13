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
                    Id = "f284b3fd-f2cf-476e-a9b6-6560689cc48c",
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
                     Id = "294d249b-9b57-48c1-9689-11a91abb6447",
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
