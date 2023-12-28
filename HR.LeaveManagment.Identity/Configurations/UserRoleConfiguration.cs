using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(

                new IdentityUserRole<string>
                {
                    RoleId = "1663df34-1be4-4a00-a910-d62a32eaced5",
                    UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "493db5f2-2154-45f5-8773-0729e3c65ad5",
                     UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                 }

                );
        }
    }
}
