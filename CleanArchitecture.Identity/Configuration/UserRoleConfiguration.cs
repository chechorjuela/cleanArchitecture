using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Identity.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
               new IdentityUserRole<string>
               {
                   RoleId = "429e567f-78fd-4cc6-bdc0-a416bee542b0",
                   UserId = "1d53e2ee-ba83-4a5e-95ba-867a51907ca1"
               },
               new IdentityUserRole<string>
               {
                   RoleId = "7b18e054-3a1d-4cf7-b532-7328026a3d9",
                   UserId = "b44710cd-690c-4167-8010-83e90bdd023f"
               }
           );
        }
    }
}
