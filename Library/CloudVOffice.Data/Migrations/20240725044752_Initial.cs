using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloudVOffice.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityLogTypeId = table.Column<int>(type: "int", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LogInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemKeyword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parent = table.Column<int>(type: "int", nullable: true),
                    IsGroup = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IconImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Applications_ApplicationId1",
                        column: x => x.ApplicationId1,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId");
                });

            migrationBuilder.CreateTable(
                name: "CompanyDetails",
                columns: table => new
                {
                    CompanyDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ABBR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfEstablishment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfIncorporation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDetails", x => x.CompanyDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "EmailDomains",
                columns: table => new
                {
                    EmailDomainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncomingServer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncomingPort = table.Column<int>(type: "int", nullable: false),
                    IncomingIsIMAP = table.Column<bool>(type: "bit", nullable: false),
                    IncomingIsSsl = table.Column<bool>(type: "bit", nullable: false),
                    IncomingIsStartTLs = table.Column<bool>(type: "bit", nullable: false),
                    OutingServer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutgoingPort = table.Column<int>(type: "int", nullable: false),
                    OutgoingIsTLs = table.Column<bool>(type: "bit", nullable: false),
                    OutgoingIsSsl = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDomains", x => x.EmailDomainId);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    EmailTemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailTemplateDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultSendingAccount = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.EmailTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    ErrorLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.ErrorLogId);
                });

            migrationBuilder.CreateTable(
                name: "InstalledApplications",
                columns: table => new
                {
                    InstalledApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalledApplications", x => x.InstalledApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "LetterHeads",
                columns: table => new
                {
                    LetterHeadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LetterHeadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadImageHeight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadImageWidth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadAlign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadFooterImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadImageFooterHeight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadImageFooterWidth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetterHeadFooterAlign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterHeads", x => x.LetterHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogLevelId = table.Column<int>(type: "int", nullable: false),
                    ShortMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    PageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferrerUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Refresh_Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: true),
                    LastIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordTokenExpirey = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EmailAccounts",
                columns: table => new
                {
                    EmailAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<int>(type: "int", nullable: false),
                    EmailAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternativeEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefaultSending = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAccounts", x => x.EmailAccountId);
                    table.ForeignKey(
                        name: "FK_EmailAccounts_EmailDomains_Domain",
                        column: x => x.Domain,
                        principalTable: "EmailDomains",
                        principalColumn: "EmailDomainId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleAndApplicationWisePermissions",
                columns: table => new
                {
                    RoleAndApplicationWisePermissionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAndApplicationWisePermissions", x => x.RoleAndApplicationWisePermissionId);
                    table.ForeignKey(
                        name: "FK_RoleAndApplicationWisePermissions_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAndApplicationWisePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMappings",
                columns: table => new
                {
                    UserRoleMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMappings", x => x.UserRoleMappingId);
                    table.ForeignKey(
                        name: "FK_UserRoleMappings_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleMappings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWiseViewMappers",
                columns: table => new
                {
                    UserWiseViewMapperId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWiseViewMappers", x => x.UserWiseViewMapperId);
                    table.ForeignKey(
                        name: "FK_UserWiseViewMappers_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWiseViewMappers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "ApplicationId1", "ApplicationName", "AreaName", "CreatedBy", "CreatedDate", "Deleted", "IconClass", "IconImageUrl", "IsGroup", "Parent", "UpdatedBy", "UpdatedDate", "Url" },
                values: new object[,]
                {
                    { 1, null, "Applications", "Application", 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(3899), false, "icon-th-large-outline", "/appstatic/images/applications.png", true, null, null, null, "/Application/Applications/InstalledApps" },
                    { 2, null, "Setup", "Setup", 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(3909), false, "icon-cogs", "/appstatic/images/setup.png", true, null, null, null, "/Setup/Setup/Dashboard" },
                    { 3, null, "Company Settings", "Setup", 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(3913), false, "icon-office", null, true, 2, null, null, "" },
                    { 4, null, "Company", "Setup", 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(3915), false, null, null, false, 3, null, null, "/Setup/CompanyDetails/CompanyDetailsView" },
                    { 5, null, "Letter Head", "Setup", 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(3918), false, null, null, false, 3, null, null, "/Setup/LetterHead/LetterHeadView" },
                    { 6, null, "User", "Setup", 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(3988), false, "icon-users", null, true, 2, null, null, "" },
                    { 7, null, "User List", "Setup", 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(3992), false, null, null, false, 6, null, null, "/Setup/User/UserList" }
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "EmailTemplateId", "CreatedBy", "CreatedDate", "DefaultSendingAccount", "Deleted", "EmailTemplateDescription", "EmailTemplateName", "Subject", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(6166), null, false, "<div role=\"document\">\r\n    <div class=\"_rp_T4 _rp_U4 ms-font-weight-regular ms-font-color-neutralDark\" style=\"display: none;\"></div>  <div autoid=\"_rp_w\" class=\"_rp_T4\" style=\"display: none;\"></div>  <div autoid=\"_rp_x\" class=\"_rp_T4\" id=\"Item.MessagePartBody\" style=\"\">\r\n        <div class=\"_rp_U4 ms-font-weight-regular ms-font-color-neutralDark rpHighlightAllClass rpHighlightBodyClass\" id=\"Item.MessageUniqueBody\" style=\"font-family: wf_segoe-ui_normal, &quot;Segoe UI&quot;, &quot;Segoe WP&quot;, Tahoma, Arial, sans-serif, serif, EmojiFont;\">\r\n            <div class=\"rps_ad57\">\r\n                <div>\r\n                    <div>\r\n                        <div style=\"margin: 0px; padding: 0px; font-family: Verdana, Helvetica, Arial, sans-serif, serif, EmojiFont; color: rgb(103, 103, 103);\">\r\n                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"padding-top:0px; background-color:#FFFFFF; width:100%; border-collapse:separate\">\r\n                                <tbody>\r\n                                    <tr>\r\n                                        <td align=\"center\">\r\n                                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"padding:0px 24px 10px; background-color:white; border-collapse:separate; border:1px solid #e7e7e7; border-bottom:none\">\r\n                                                <tbody>\r\n                                                    <tr>\r\n                                                        <td></td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td align=\"center\" style=\"min-width:590px\">\r\n                                                            <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"padding:20px 0 0; border-collapse:separate\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td valign=\"middle\">\r\n                                                                            <h1 style=\"color:#676767; font-weight:400; margin:0px\">{%welcometitle%} </h1>\r\n                                                                        </td>\r\n                                                                        <td valign=\"middle\" align=\"right\" width=\"200px\">{%emailogo%}</td>\r\n                                                                    </tr>\r\n                                                                    <tr>\r\n                                                                        <td colspan=\"2\" style=\"text-align:center\">\r\n                                                                            <hr width=\"100%\" style=\"background-color:rgb(204,204,204); border:medium none; clear:both; display:block; font-size:0px; min-height:1px; line-height:0; margin:4px 0px 16px 0px\">\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td style=\"min-width:590px\">\r\n                                                            <table border=\"0\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td>\r\n                                                                            <div style=\"margin-left:1.2rem; margin-bottom:1em\">\r\n                                                                                <h5 style=\"font-weight:400; margin-bottom:0; font-size:16px; color:#676767\"><span style=\"color:rgb(22,123,158); font-size:16px; margin-right:2px; font-weight:600\"></span>{%helloname%}</h5>\r\n                                                                                <p style=\"color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px\">{%accountcreatetionmessage%}</p>\r\n\r\n                                                                                <p style=\"color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px\">{%loginidmessage%}</p>\r\n\r\n\r\n                                                                                <p style=\"color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px\">{%aditionalmessage%}</p>\r\n                                                                                <div style=\"margin:20px 0 0 0; text-align:center\">{%setpasswordlink%}</div>\r\n                                                                                <br />\r\n                                                                                {%copylinkfrommessage%}\r\n                                                                            </div>\r\n                                                                         \r\n                                                                            <div style=\"margin-left:1.2rem; margin-bottom:1em\">\r\n                                                                                <p style=\"color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px\">\r\n                                                                                    {%emailsignature%}\r\n                                                                                </p>\r\n                                                                            </div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td>\r\n                                                            <table border=\"0\" style=\"width:100%\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td>\r\n                                                                            <div style=\"text-align:center; border-top:1px solid rgb(230,230,230); padding-bottom:20px; padding-top:15px; line-height:125%; font-size:11px; margin:20px 20px 0 20px\">\r\n                                                                                <p style=\"color:rgb(115,115,115); font-size:10px\">© Copyright {%companyname%}, {%address%} </p>\r\n                                                                            </div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                    <tr>\r\n                                                                        <td align=\"right\">\r\n                                                                            <div style=\" margin:0 20px\">{%footerletterhera%}</div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td>\r\n                                                            <table border=\"0\" style=\"width:100%\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td>\r\n                                                                            <div style=\"text-align:justify; border-top:1px solid rgb(230,230,230); padding-bottom:10px; padding-top:10px; line-height:125%; font-size:10px; margin:25px 20px 0 20px\">\r\n                                                                                <p style=\"color:rgb(115,115,115); margin:0; font-size:10px\">\r\n                                                                                    The information contained in this e-mail message and/or attachments to it may contain confidential\r\n                                                                                    or privileged information. If you are not the intended recipient, any dissemination,use, review, distribution,\r\n                                                                                    printing or copying of the information contained in this email message and/or attachments to it are strictly prohibited.\r\n                                                                                    If you have received this communication in error, please notify us by reply e-mail or telephone and immediately\r\n                                                                                    and permanently delete the message and any attachments. Thank you.\r\n                                                                                </p>\r\n                                                                            </div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                </tbody>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </tbody>\r\n                            </table>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div> <div class=\"_rp_c5\" style=\"display: none;\"></div>\r\n    </div>  <span class=\"PersonaPaneLauncher\"><div ariatabindex=\"-1\" class=\"_pe_d _pe_62\" aria-expanded=\"false\" tabindex=\"-1\" aria-haspopup=\"false\">  <div style=\"display: none;\"></div> </div></span>\r\n</div>", "WelcomeEmail", "", null, null },
                    { 7, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(6170), null, false, "<div role=\"document\">\r\n    <div class=\"_rp_T4 _rp_U4 ms-font-weight-regular ms-font-color-neutralDark\" style=\"display: none;\"><br></div>  <div autoid=\"_rp_w\" class=\"_rp_T4\" style=\"display: none;\"><br></div>  <div autoid=\"_rp_x\" class=\"_rp_T4\" id=\"Item.MessagePartBody\" style=\"\">\r\n        <div class=\"_rp_U4 ms-font-weight-regular ms-font-color-neutralDark rpHighlightAllClass rpHighlightBodyClass\" id=\"Item.MessageUniqueBody\" style=\"font-family: wf_segoe-ui_normal, &quot;Segoe UI&quot;, &quot;Segoe WP&quot;, Tahoma, Arial, sans-serif, serif, EmojiFont;\">\r\n            <div class=\"rps_ad57\">\r\n                <div>\r\n                    <div>\r\n                        <div style=\"margin: 0px; padding: 0px; font-family: Verdana, Helvetica, Arial, sans-serif, serif, EmojiFont; color: rgb(103, 103, 103);\">\r\n                            <table cellpadding=\"0\" cellspacing=\"0\" style=\"padding-top:0px; background-color:#FFFFFF; width:100%; border-collapse:separate\" class=\"e-rte-table\">\r\n                                <tbody>\r\n                                    <tr>\r\n                                        <td align=\"center\" class=\"\">\r\n                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"padding:0px 24px 10px; background-color:white; border-collapse:separate; border:1px solid #e7e7e7; border-bottom:none\" class=\"e-rte-table\">\r\n                                                <tbody>\r\n                                                    <tr>\r\n                                                        <td><br></td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td align=\"center\" style=\"min-width:590px\">\r\n                                                            <table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"padding:20px 0 0; border-collapse:separate\" class=\"e-rte-table\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td valign=\"middle\" class=\"\">\r\n                                                                            <h1 style=\"color:#676767; font-weight:400; margin:0px\">Password Reset Request</h1>\r\n                                                                        </td>\r\n                                                                        <td valign=\"middle\" align=\"right\" width=\"200px\">{%emailogo%}</td>\r\n                                                                    </tr>\r\n                                                                    <tr>\r\n                                                                        <td colspan=\"2\" style=\"text-align:center\">\r\n                                                                            <hr width=\"100%\" style=\"background-color:rgb(204,204,204); border:medium none; clear:both; display:block; font-size:0px; min-height:1px; line-height:0; margin:4px 0px 16px 0px\">\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td style=\"min-width:590px\">\r\n                                                            <table class=\"e-rte-table\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td class=\"\">\r\n                                                                            <div style=\"margin-left:1.2rem; margin-bottom:1em\">\r\n                                                                                <h5 style=\"font-weight:400; margin-bottom:0; font-size:16px; color:#676767\">Hello {%helloname%}</h5><div><br></div>\r\n                                                                                <p>We have received a request to reset your account password. To proceed with the password reset, please click on the link below:</p>\r\n                                                                                <div style=\"margin:20px 0 0 0; text-align:center\">{%setpasswordlink%}</div>\r\n                                                                                <br>If you did not request a password reset, Please ignore this email. Your account will&nbsp;<span style=\"background-color: transparent; text-align: inherit;\">remain secure, and no action is required.</span></div><div style=\"margin-left:1.2rem; margin-bottom:1em\"><span style=\"background-color: transparent; text-align: inherit;\"><p>For security reasons, this link will expire in 2 hours. If you&nbsp;<span style=\"background-color: transparent; text-align: inherit;\">are unable to reset your password within this time frame,&nbsp;</span><span style=\"background-color: transparent; text-align: inherit;\">please request another password reset.</span></p></span></div>\r\n                                                                         \r\n                                                                            <div style=\"margin-left:1.2rem; margin-bottom:1em\">\r\n                                                                                <p style=\"color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px\">\r\n                                                                                    {%emailsignature%}\r\n                                                                                </p>\r\n                                                                            </div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td>\r\n                                                            <table style=\"width:100%\" class=\"e-rte-table\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td>\r\n                                                                            <div style=\"text-align:center; border-top:1px solid rgb(230,230,230); padding-bottom:20px; padding-top:15px; line-height:125%; font-size:11px; margin:20px 20px 0 20px\">\r\n                                                                                <p style=\"color:rgb(115,115,115); font-size:10px\">© Copyright {%companyname%}, {%address%} </p>\r\n                                                                            </div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                    <tr>\r\n                                                                        <td align=\"right\">\r\n                                                                            <div style=\" margin:0 20px\">{%footerletterhera%}</div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                    <tr>\r\n                                                        <td>\r\n                                                            <table style=\"width:100%\" class=\"e-rte-table\">\r\n                                                                <tbody>\r\n                                                                    <tr>\r\n                                                                        <td>\r\n                                                                            <div style=\"text-align:justify; border-top:1px solid rgb(230,230,230); padding-bottom:10px; padding-top:10px; line-height:125%; font-size:10px; margin:25px 20px 0 20px\">\r\n                                                                                <p style=\"color:rgb(115,115,115); margin:0; font-size:10px\">\r\n                                                                                    The information contained in this e-mail message and/or attachments to it may contain confidential\r\n                                                                                    or privileged information. If you are not the intended recipient, any dissemination,use, review, distribution,\r\n                                                                                    printing or copying of the information contained in this email message and/or attachments to it are strictly prohibited.\r\n                                                                                    If you have received this communication in error, please notify us by reply e-mail or telephone and immediately\r\n                                                                                    and permanently delete the message and any attachments. Thank you.\r\n                                                                                </p>\r\n                                                                            </div>\r\n                                                                        </td>\r\n                                                                    </tr>\r\n                                                                </tbody>\r\n                                                            </table>\r\n                                                        </td>\r\n                                                    </tr>\r\n                                                </tbody>\r\n                                            </table>\r\n                                        </td>\r\n                                    </tr>\r\n                                </tbody>\r\n                            </table>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div> <div class=\"_rp_c5\" style=\"display: none;\"><br></div>\r\n    </div>  <span class=\"PersonaPaneLauncher\"><div ariatabindex=\"-1\" class=\"_pe_d _pe_62\" aria-expanded=\"false\" tabindex=\"-1\" aria-haspopup=\"false\">  <div style=\"display: none;\"><br></div> </div></span>\r\n</div>", "PasswordReset", "Password Reset Request", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedBy", "CreatedDate", "Deleted", "RoleName", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(336), false, "Administrator", null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedBy", "CreatedDate", "DateOfBirth", "Deleted", "Email", "FailedLoginAttempts", "FirstName", "IsActive", "LastActivityDate", "LastIpAddress", "LastLoginDate", "LastName", "MiddleName", "Password", "PhoneNo", "ResetPasswordToken", "ResetPasswordTokenExpirey", "UpdatedBy", "UpdatedDate", "UserType", "UserTypeId" },
                values: new object[] { 1L, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(2465), null, false, "admin@appman.in", null, "Administrator", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "", null, "r9NmU79/NE0x0el2cuI8PeI4GlVCdpOeB875sWPUeJw=", "9583000000", null, null, null, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "RoleAndApplicationWisePermissions",
                columns: new[] { "RoleAndApplicationWisePermissionId", "ApplicationId", "CreatedBy", "CreatedDate", "Deleted", "RoleId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1L, 1, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4899), false, 1, null, null },
                    { 2L, 2, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4903), false, 1, null, null },
                    { 3L, 3, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4906), false, 1, null, null },
                    { 4L, 4, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4908), false, 1, null, null },
                    { 5L, 5, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4910), false, 1, null, null },
                    { 6L, 6, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4912), false, 1, null, null },
                    { 7L, 7, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4914), false, 1, null, null },
                    { 8L, 8, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4916), false, 1, null, null },
                    { 9L, 9, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4918), false, 1, null, null },
                    { 10L, 10, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(4920), false, 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "UserRoleMappings",
                columns: new[] { "UserRoleMappingId", "RoleId", "UserId" },
                values: new object[] { 1, 1, 1L });

            migrationBuilder.InsertData(
                table: "UserWiseViewMappers",
                columns: new[] { "UserWiseViewMapperId", "ApplicationId", "CreatedBy", "CreatedDate", "Deleted", "UpdatedBy", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1L, 1, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5812), false, null, null, 1L },
                    { 2L, 2, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5818), false, null, null, 1L },
                    { 3L, 3, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5820), false, null, null, 1L },
                    { 4L, 4, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5822), false, null, null, 1L },
                    { 5L, 5, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5824), false, null, null, 1L },
                    { 6L, 6, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5826), false, null, null, 1L },
                    { 7L, 7, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5828), false, null, null, 1L },
                    { 8L, 8, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5830), false, null, null, 1L },
                    { 9L, 9, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5832), false, null, null, 1L },
                    { 10L, 10, 1L, new DateTime(2024, 7, 25, 10, 17, 52, 190, DateTimeKind.Local).AddTicks(5835), false, null, null, 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationId1",
                table: "Applications",
                column: "ApplicationId1");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAccounts_Domain",
                table: "EmailAccounts",
                column: "Domain");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAndApplicationWisePermissions_ApplicationId",
                table: "RoleAndApplicationWisePermissions",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAndApplicationWisePermissions_RoleId",
                table: "RoleAndApplicationWisePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMappings_RoleId",
                table: "UserRoleMappings",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMappings_UserId",
                table: "UserRoleMappings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWiseViewMappers_ApplicationId",
                table: "UserWiseViewMappers",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWiseViewMappers_UserId",
                table: "UserWiseViewMappers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "ActivityLogTypes");

            migrationBuilder.DropTable(
                name: "CompanyDetails");

            migrationBuilder.DropTable(
                name: "EmailAccounts");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "InstalledApplications");

            migrationBuilder.DropTable(
                name: "LetterHeads");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RoleAndApplicationWisePermissions");

            migrationBuilder.DropTable(
                name: "UserRoleMappings");

            migrationBuilder.DropTable(
                name: "UserWiseViewMappers");

            migrationBuilder.DropTable(
                name: "EmailDomains");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
