using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.AdminUserConfig
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private const string adminId = "2301D884-221A-4E7D-B509-0113DCC043E1";
        private const string doctorRoleId = "2301D884-221A-4E7D-B509-0113DCC044E2";
        private const string patientRoleId = "2301D884-221A-4E7D-B509-0113DCC045E3";
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            builder.HasData(
                    new IdentityRole
                    {
                        Id = adminId,
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    }
                        ,
                     new IdentityRole
                     {
                         Id = doctorRoleId,
                         Name = "Doctor",
                         NormalizedName = "DOCTOR"
                     }
                     ,
                      new IdentityRole
                      {
                          Id = patientRoleId,
                          Name = "Patient",
                          NormalizedName = "PATIENT"
                      }

                );
            builder.HasKey(i => i.Id);
        }
    }
}
