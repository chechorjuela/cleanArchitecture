using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
               new IdentityRole
               {
                   Id = "429e567f-78fd-4cc6-bdc0-a416bee542b0",
                   Name = "Admin",
                   NormalizedName = "ADMIN"
               },
                new IdentityRole
                {
                    Id = "7b18e054-3a1d-4cf7-b532-7328026a3d9f",
                    Name = "User",
                    NormalizedName = "USER"
                }
           );

        }
    }
}
