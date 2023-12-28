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
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole
            {
                Id = "1663df34-1be4-4a00-a910-d62a32eaced5",
                Name = "Employee",
                NormalizedName = "EMPLOYEE",

            },
            new IdentityRole
            {
                Id = "493db5f2-2154-45f5-8773-0729e3c65ad5",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            }
            );
        }
    }
}
