using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualClinic.Migrations
{
    /*
<<<<<<< HEAD:VirtualClinic/Migrations/20210624150400_init.cs
    public partial class init : Migration
=======
    public partial class AAA : Migration
>>>>>>> f15c697ff2d9f8b4210ddb9913950e4a13adb78b:VirtualClinic/Migrations/20210623220003_AAA.cs
    {*/
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VirtualClinic");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "VirtualClinic",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "VirtualClinic",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsDoctor = table.Column<bool>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    IdCard = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsActivated = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Speciality = table.Column<string>(nullable: true),
                    Price = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "VirtualClinic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "VirtualClinic",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
<<<<<<< HEAD:VirtualClinic/Migrations/20210624150400_init.cs
                name: "MedicalFiles",
                schema: "VirtualClinic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<float>(nullable: false),
                    height = table.Column<float>(nullable: false),
                    temperature = table.Column<float>(nullable: false),
                    tension = table.Column<int>(nullable: false),
                    blood_type = table.Column<string>(nullable: true),
                    rhesus_factor = table.Column<string>(nullable: true),
                    health_history = table.Column<string>(nullable: true),
                    patientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalFiles_User_patientId",
                        column: x => x.patientId,
=======
                name: "Ratings",
                schema: "VirtualClinic",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DoctorId = table.Column<string>(nullable: true),
                    PatientId = table.Column<string>(nullable: true),
                    PatientName = table.Column<string>(nullable: true),
                    PatientImage = table.Column<byte[]>(nullable: true),
                    KindoRating = table.Column<double>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_User_ApplicationUserId",
                        column: x => x.ApplicationUserId,
>>>>>>> f15c697ff2d9f8b4210ddb9913950e4a13adb78b:VirtualClinic/Migrations/20210623220003_AAA.cs
                        principalSchema: "VirtualClinic",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "VirtualClinic",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsAllDay = table.Column<bool>(nullable: false),
                    RecurrenceRule = table.Column<string>(nullable: true),
                    RecurrenceID = table.Column<int>(nullable: true),
                    RecurrenceException = table.Column<string>(nullable: true),
                    StartTimezone = table.Column<string>(nullable: true),
                    EndTimezone = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Task1TaskId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<string>(nullable: true),
                    PatientId = table.Column<string>(nullable: true),
                    Identifier = table.Column<int>(nullable: false),
                    state = table.Column<string>(nullable: true),
                    DoctorName = table.Column<string>(nullable: true),
                    Speciality = table.Column<string>(nullable: true),
                    Amout = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_User_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "VirtualClinic",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Tasks_Task1TaskId",
                        column: x => x.Task1TaskId,
                        principalSchema: "VirtualClinic",
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "VirtualClinic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "VirtualClinic",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "VirtualClinic",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "VirtualClinic",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "VirtualClinic",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "VirtualClinic",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "VirtualClinic",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "VirtualClinic",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "VirtualClinic",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "VirtualClinic",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
<<<<<<< HEAD:VirtualClinic/Migrations/20210624150400_init.cs
                    { "2301D884-221A-4E7D-B509-0113DCC043E1", "7b75ebd6-3a8e-480c-b215-ef34048f5592", "Administrator", "ADMINISTRATOR" },
                    { "2301D884-221A-4E7D-B509-0113DCC044E2", "9b8fe747-7ab3-4590-9ab6-2b12ba63f65b", "Doctor", "DOCTOR" },
                    { "2301D884-221A-4E7D-B509-0113DCC045E3", "b853c815-f031-4fbb-a215-361aaa20ed60", "Patient", "PATIENT" }
=======
                    { "2301D884-221A-4E7D-B509-0113DCC043E1", "342627f3-0e3e-4f2f-a2b9-bc6e543d0da6", "Administrator", "ADMINISTRATOR" },
                    { "2301D884-221A-4E7D-B509-0113DCC044E2", "16bef155-351b-4f21-86d4-6017ab8f3c7d", "Doctor", "DOCTOR" },
                    { "2301D884-221A-4E7D-B509-0113DCC045E3", "14c858e5-e168-4acb-bb3f-96fee69ee367", "Patient", "PATIENT" }
>>>>>>> f15c697ff2d9f8b4210ddb9913950e4a13adb78b:VirtualClinic/Migrations/20210623220003_AAA.cs
                });

            migrationBuilder.InsertData(
                schema: "VirtualClinic",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "Birthday", "City", "ConcurrencyStamp", "Country", "CreateDate", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Gender", "IdCard", "Image", "IsActivated", "IsDoctor", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "State", "TwoFactorEnabled", "UserName" },
<<<<<<< HEAD:VirtualClinic/Migrations/20210624150400_init.cs
                values: new object[] { "B22698B8-42A2-4115-9631-1C2D1E2AC5F7", 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5e83aa49-686a-474e-8eea-b89d234b93fb", null, new DateTime(2021, 6, 24, 17, 3, 59, 106, DateTimeKind.Local).AddTicks(1487), "ApplicationUser", "Master@Admin.com", true, "Master", null, null, null, false, false, "Admin", false, null, "MASTER@ADMIN.COM", "MASTERADMIN", "AQAAAAEAACcQAAAAEA+315K4SEgUgZmDkAFHHHr9p7IfQ8qlQO77RuubuLIWgfr93Rg4vN9h3St7AB380w==", "XXXXXXXXXXXXX", true, "00000000-0000-0000-0000-000000000000", null, false, "masteradmin" });
=======
                values: new object[] { "B22698B8-42A2-4115-9631-1C2D1E2AC5F7", 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2864d99b-cabc-4ce3-8104-a6aa2ece403b", null, new DateTime(2021, 6, 23, 23, 0, 2, 912, DateTimeKind.Local).AddTicks(4067), "ApplicationUser", "Master@Admin.com", true, "Master", null, null, null, false, false, "Admin", false, null, "MASTER@ADMIN.COM", "MASTERADMIN", "AQAAAAEAACcQAAAAEB8BbZiqxI09bAE4IVpxUUfrEkLjB3xTIj1gr2YeouTOwsYTUrSUkKBJ7+sm9o+qew==", "XXXXXXXXXXXXX", true, "00000000-0000-0000-0000-000000000000", null, false, "masteradmin" });
>>>>>>> f15c697ff2d9f8b4210ddb9913950e4a13adb78b:VirtualClinic/Migrations/20210623220003_AAA.cs

            migrationBuilder.InsertData(
                schema: "VirtualClinic",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2301D884-221A-4E7D-B509-0113DCC043E1", "B22698B8-42A2-4115-9631-1C2D1E2AC5F7" });

            migrationBuilder.CreateIndex(
<<<<<<< HEAD:VirtualClinic/Migrations/20210624150400_init.cs
                name: "IX_MedicalFiles_patientId",
                schema: "VirtualClinic",
                table: "MedicalFiles",
                column: "patientId");
=======
                name: "IX_Ratings_ApplicationUserId",
                schema: "VirtualClinic",
                table: "Ratings",
                column: "ApplicationUserId");
>>>>>>> f15c697ff2d9f8b4210ddb9913950e4a13adb78b:VirtualClinic/Migrations/20210623220003_AAA.cs

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "VirtualClinic",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "VirtualClinic",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DoctorId",
                schema: "VirtualClinic",
                table: "Tasks",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Task1TaskId",
                schema: "VirtualClinic",
                table: "Tasks",
                column: "Task1TaskId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "VirtualClinic",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "VirtualClinic",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "VirtualClinic",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "VirtualClinic",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "VirtualClinic",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalFiles",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "Ratings",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "VirtualClinic");

            migrationBuilder.DropTable(
                name: "User",
                schema: "VirtualClinic");
        }
    }
}
