
using CloudVOffice.Core.Domain.EmailTemplates;
using CloudVOffice.Core.Domain.Pemission;
using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Core.Security;
using Microsoft.EntityFrameworkCore;

namespace CloudVOffice.Data.Seeding
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(e => e.RoleId);

                b.HasData(
                    new Role
                    {
                        RoleId = 1,
                        RoleName = "Administrator",
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                        Deleted = false,

                    }

                    );
            });


            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(e => e.UserId);

                b.HasData(
                    new User
                    {
                        UserId = 1,
                        FirstName = "Administrator",
                        LastName = "",
                        Email = "admin@appman.in",
                        Password = Encrypt.EncryptPassword("Appman@2019", "admin@appman.in"),
                        PhoneNo = "9583000000",
                        UserTypeId = 1,
                        CreatedBy = 1,
                        CreatedDate = System.DateTime.Now,
                        Deleted = false,
                        IsActive = true

                    }

                    );
            });

            modelBuilder.Entity<UserRoleMapping>(b =>
            {
                b.HasKey(e => e.UserRoleMappingId);

                b.HasData(
                    new UserRoleMapping
                    {
                        UserRoleMappingId = 1,
                        UserId = 1,
                        RoleId = 1
                    }


                    );

            });

            modelBuilder.Entity<Application>(b =>
            {
                b.HasKey(e => e.ApplicationId);
                b.Property(x => x.ApplicationId).ValueGeneratedOnAdd().UseIdentityColumn(10000, 1);
                b.HasData(
                    new Application
                    {
                        ApplicationId = 1,
                        ApplicationName = "Applications",
                        Parent = null,
                        IsGroup = true,
                        Url = "/Application/Applications/InstalledApps",
                        CreatedBy = 1,
                        IconImageUrl = "/appstatic/images/applications.png",
                        CreatedDate = System.DateTime.Now,
                        AreaName = "Application",
                        IconClass="icon-th-large-outline",

                        Deleted = false
                    },
                      new Application
                      {
                          ApplicationId = 2,
                          ApplicationName = "Setup",
                          Parent = null,
                          IsGroup = true,
                          Url = "/Setup/Setup/Dashboard",
                          CreatedBy = 1,
                          IconImageUrl = "/appstatic/images/setup.png",
                          CreatedDate = System.DateTime.Now,
                          Deleted = false,
                          AreaName = "Setup",
                          IconClass = "icon-cogs",


                      },
                       new Application
                       {
                           ApplicationId = 3,
                           ApplicationName = "Company Settings",
                           Parent = 2,
                           IsGroup = true,
                           Url = "",
                           CreatedBy = 1,

                           CreatedDate = System.DateTime.Now,
                           Deleted = false,

                           AreaName = "Setup",
                           IconClass = "icon-office",

                       },

                        new Application
                        {
                            ApplicationId = 4,
                            ApplicationName = "Company",
                            Parent = 3,
                            IsGroup = false,
                            Url = "/Setup/CompanyDetails/CompanyDetailsView",
                            CreatedBy = 1,

                            CreatedDate = System.DateTime.Now,
                            Deleted = false,

                            AreaName = "Setup"

                        },
                        new Application
                        {
                            ApplicationId = 5,
                            ApplicationName = "Letter Head",
                            Parent = 3,
                            IsGroup = false,
                            Url = "/Setup/LetterHead/LetterHeadView",
                            CreatedBy = 1,

                            CreatedDate = System.DateTime.Now,
                            Deleted = false,

                            AreaName = "Setup"

                        },
                        new Application
                        {
                            ApplicationId = 6,
                            ApplicationName = "User",
                            Parent = 2,
                            IsGroup = true,
                            Url = "",
                            CreatedBy = 1,

                            CreatedDate = System.DateTime.Now,
                            Deleted = false,

                            AreaName = "Setup",
                            IconClass = "icon-users",

                        },
                          new Application
                          {
                              ApplicationId = 7,
                              ApplicationName = "User List",
                              Parent = 6,
                              IsGroup = false,
                              Url = "/Setup/User/UserList",
                              CreatedBy = 1,

                              CreatedDate = System.DateTime.Now,
                              Deleted = false,
                              AreaName = "Setup"


                          }


						 // new Application
						 // {
							//  ApplicationId = 8,
							//  ApplicationName = "Email Setup",
							//  Parent = 2,
							//  IsGroup = true,
							//  Url = "",
							//  CreatedBy = 1,

							//  CreatedDate = System.DateTime.Now,
							//  Deleted = false,
							//  AreaName = "Setup",
							//  IconClass = "icon-envelop",


						 // },
							//new Application
							//{
							//	ApplicationId = 9,
							//	ApplicationName = "Domain",
							//	Parent = 8,
							//	IsGroup = true,
							//	Url = "/Setup/EmailDomain/EmailDomainView",
							//	CreatedBy = 1,

							//	CreatedDate = System.DateTime.Now,
							//	Deleted = false,
							//	AreaName = "Setup"


							//},
							//  new Application
							//  {
							//	  ApplicationId = 10,
							//	  ApplicationName = "Email Account",
							//	  Parent = 8,
							//	  IsGroup = true,
							//	  Url = "/Setup/EmailAccount/EmailAccountView",
							//	  CreatedBy = 1,

							//	  CreatedDate = System.DateTime.Now,
							//	  Deleted = false,
							//	  AreaName = "Setup"


							//  }



					);

            });


            modelBuilder.Entity<RoleAndApplicationWisePermission>(b =>
            {
                b.HasKey(e => e.RoleAndApplicationWisePermissionId);
                b.Property(x => x.RoleAndApplicationWisePermissionId).ValueGeneratedOnAdd().UseIdentityColumn(10000, 1);
                b.HasData(
                    new RoleAndApplicationWisePermission
                    {
                        RoleAndApplicationWisePermissionId = 1,
                        ApplicationId = 1,
                        RoleId = 1,
                        CreatedBy = 1,

                        CreatedDate = System.DateTime.Now,
                        Deleted = false
                    },
                      new RoleAndApplicationWisePermission
                      {
                          RoleAndApplicationWisePermissionId = 2,
                          ApplicationId = 2,
                          RoleId = 1,
                          CreatedBy = 1,

                          CreatedDate = System.DateTime.Now,
                          Deleted = false
                      },
                       new RoleAndApplicationWisePermission
                       {
                           RoleAndApplicationWisePermissionId = 3,
                           ApplicationId = 3,
                           RoleId = 1,
                           CreatedBy = 1,

                           CreatedDate = System.DateTime.Now,
                           Deleted = false
                       },
                         new RoleAndApplicationWisePermission
                         {
                             RoleAndApplicationWisePermissionId = 4,
                             ApplicationId = 4,
                             RoleId = 1,
                             CreatedBy = 1,

                             CreatedDate = System.DateTime.Now,
                             Deleted = false
                         },
                         new RoleAndApplicationWisePermission
                         {
                             RoleAndApplicationWisePermissionId = 5,
                             ApplicationId = 5,
                             RoleId = 1,
                             CreatedBy = 1,

                             CreatedDate = System.DateTime.Now,
                             Deleted = false
                         },
                         new RoleAndApplicationWisePermission
                         {
                             RoleAndApplicationWisePermissionId = 6,
                             ApplicationId = 6,
                             RoleId = 1,
                             CreatedBy = 1,

                             CreatedDate = System.DateTime.Now,
                             Deleted = false
                         },
                             new RoleAndApplicationWisePermission
                             {
                                 RoleAndApplicationWisePermissionId = 7,
                                 ApplicationId = 7,
                                 RoleId = 1,
                                 CreatedBy = 1,

                                 CreatedDate = System.DateTime.Now,
                                 Deleted = false
                             },

                             new RoleAndApplicationWisePermission
                             {
                                 RoleAndApplicationWisePermissionId = 8,
                                 ApplicationId = 8,
                                 RoleId = 1,
                                 CreatedBy = 1,

                                 CreatedDate = System.DateTime.Now,
                                 Deleted = false
                             },


                             new RoleAndApplicationWisePermission
                             {
                                 RoleAndApplicationWisePermissionId = 9,
                                 ApplicationId = 9,
                                 RoleId = 1,
                                 CreatedBy = 1,

                                 CreatedDate = System.DateTime.Now,
                                 Deleted = false
                             },


                             new RoleAndApplicationWisePermission
                             {
                                 RoleAndApplicationWisePermissionId = 10,
                                 ApplicationId = 10,
                                 RoleId = 1,
                                 CreatedBy = 1,

                                 CreatedDate = System.DateTime.Now,
                                 Deleted = false
                             }                             
                    );

            });


            modelBuilder.Entity<UserWiseViewMapper>(b =>
            {
                b.HasKey(e => e.UserWiseViewMapperId);
                b.Property(x => x.UserWiseViewMapperId).ValueGeneratedOnAdd().UseIdentityColumn(10000, 1);
                b.HasData(
                    new UserWiseViewMapper
                    {
                        UserWiseViewMapperId = 1,
                        ApplicationId = 1,
                        UserId = 1,
                        CreatedBy = 1,

                        CreatedDate = System.DateTime.Now,
                        Deleted = false
                    },





                     new UserWiseViewMapper
                     {
                         UserWiseViewMapperId = 2,
                         ApplicationId = 2,
                         UserId = 1,
                         CreatedBy = 1,

                         CreatedDate = System.DateTime.Now,
                         Deleted = false
                     },
                     new UserWiseViewMapper
                     {
                         UserWiseViewMapperId = 3,
                         ApplicationId = 3,
                         UserId = 1,
                         CreatedBy = 1,

                         CreatedDate = System.DateTime.Now,
                         Deleted = false
                     },
                     new UserWiseViewMapper
                     {
                         UserWiseViewMapperId = 4,
                         ApplicationId = 4,
                         UserId = 1,
                         CreatedBy = 1,

                         CreatedDate = System.DateTime.Now,
                         Deleted = false
                     },
                     new UserWiseViewMapper
                     {
                         UserWiseViewMapperId = 5,
                         ApplicationId = 5,
                         UserId = 1,
                         CreatedBy = 1,

                         CreatedDate = System.DateTime.Now,
                         Deleted = false
                     },
                     new UserWiseViewMapper
                     {
                         UserWiseViewMapperId = 6,
                         ApplicationId = 6,
                         UserId = 1,
                         CreatedBy = 1,

                         CreatedDate = System.DateTime.Now,
                         Deleted = false
                     },

                     new UserWiseViewMapper
                     {
                         UserWiseViewMapperId = 7,
                         ApplicationId = 7,
                         UserId = 1,
                         CreatedBy = 1,

                         CreatedDate = System.DateTime.Now,
                         Deleted = false
                     },

                     new UserWiseViewMapper
                     {
                         UserWiseViewMapperId = 8,
                         ApplicationId = 8,
                         UserId = 1,
                         CreatedBy = 1,

                         CreatedDate = System.DateTime.Now,
                         Deleted = false
                     },
                      new UserWiseViewMapper
                      {
                          UserWiseViewMapperId = 9,
                          ApplicationId = 9,
                          UserId = 1,
                          CreatedBy = 1,

                          CreatedDate = System.DateTime.Now,
                          Deleted = false
                      },

                       new UserWiseViewMapper
                       {
                           UserWiseViewMapperId = 10,
                           ApplicationId = 10,
                           UserId = 1,
                           CreatedBy = 1,

                           CreatedDate = System.DateTime.Now,
                           Deleted = false
                       }

                    );

            });

            modelBuilder.Entity<EmailTemplate>(x =>
            {
                x.HasKey(e => e.EmailTemplateId);
                x.HasData(
                      new EmailTemplate
                      {
                          EmailTemplateId = 1,
                          EmailTemplateName = "WelcomeEmail",
                          EmailTemplateDescription = @"<div role=""document"">
    <div class=""_rp_T4 _rp_U4 ms-font-weight-regular ms-font-color-neutralDark"" style=""display: none;""></div>  <div autoid=""_rp_w"" class=""_rp_T4"" style=""display: none;""></div>  <div autoid=""_rp_x"" class=""_rp_T4"" id=""Item.MessagePartBody"" style="""">
        <div class=""_rp_U4 ms-font-weight-regular ms-font-color-neutralDark rpHighlightAllClass rpHighlightBodyClass"" id=""Item.MessageUniqueBody"" style=""font-family: wf_segoe-ui_normal, &quot;Segoe UI&quot;, &quot;Segoe WP&quot;, Tahoma, Arial, sans-serif, serif, EmojiFont;"">
            <div class=""rps_ad57"">
                <div>
                    <div>
                        <div style=""margin: 0px; padding: 0px; font-family: Verdana, Helvetica, Arial, sans-serif, serif, EmojiFont; color: rgb(103, 103, 103);"">
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""padding-top:0px; background-color:#FFFFFF; width:100%; border-collapse:separate"">
                                <tbody>
                                    <tr>
                                        <td align=""center"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"" style=""padding:0px 24px 10px; background-color:white; border-collapse:separate; border:1px solid #e7e7e7; border-bottom:none"">
                                                <tbody>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td align=""center"" style=""min-width:590px"">
                                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""padding:20px 0 0; border-collapse:separate"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign=""middle"">
                                                                            <h1 style=""color:#676767; font-weight:400; margin:0px"">{%welcometitle%} </h1>
                                                                        </td>
                                                                        <td valign=""middle"" align=""right"" width=""200px"">{%emailogo%}</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan=""2"" style=""text-align:center"">
                                                                            <hr width=""100%"" style=""background-color:rgb(204,204,204); border:medium none; clear:both; display:block; font-size:0px; min-height:1px; line-height:0; margin:4px 0px 16px 0px"">
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""min-width:590px"">
                                                            <table border=""0"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <div style=""margin-left:1.2rem; margin-bottom:1em"">
                                                                                <h5 style=""font-weight:400; margin-bottom:0; font-size:16px; color:#676767""><span style=""color:rgb(22,123,158); font-size:16px; margin-right:2px; font-weight:600""></span>{%helloname%}</h5>
                                                                                <p style=""color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px"">{%accountcreatetionmessage%}</p>

                                                                                <p style=""color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px"">{%loginidmessage%}</p>


                                                                                <p style=""color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px"">{%aditionalmessage%}</p>
                                                                                <div style=""margin:20px 0 0 0; text-align:center"">{%setpasswordlink%}</div>
                                                                                <br />
                                                                                {%copylinkfrommessage%}
                                                                            </div>
                                                                         
                                                                            <div style=""margin-left:1.2rem; margin-bottom:1em"">
                                                                                <p style=""color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px"">
                                                                                    {%emailsignature%}
                                                                                </p>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border=""0"" style=""width:100%"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <div style=""text-align:center; border-top:1px solid rgb(230,230,230); padding-bottom:20px; padding-top:15px; line-height:125%; font-size:11px; margin:20px 20px 0 20px"">
                                                                                <p style=""color:rgb(115,115,115); font-size:10px"">© Copyright {%companyname%}, {%address%} </p>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align=""right"">
                                                                            <div style="" margin:0 20px"">{%footerletterhera%}</div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border=""0"" style=""width:100%"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <div style=""text-align:justify; border-top:1px solid rgb(230,230,230); padding-bottom:10px; padding-top:10px; line-height:125%; font-size:10px; margin:25px 20px 0 20px"">
                                                                                <p style=""color:rgb(115,115,115); margin:0; font-size:10px"">
                                                                                    The information contained in this e-mail message and/or attachments to it may contain confidential
                                                                                    or privileged information. If you are not the intended recipient, any dissemination,use, review, distribution,
                                                                                    printing or copying of the information contained in this email message and/or attachments to it are strictly prohibited.
                                                                                    If you have received this communication in error, please notify us by reply e-mail or telephone and immediately
                                                                                    and permanently delete the message and any attachments. Thank you.
                                                                                </p>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div> <div class=""_rp_c5"" style=""display: none;""></div>
    </div>  <span class=""PersonaPaneLauncher""><div ariatabindex=""-1"" class=""_pe_d _pe_62"" aria-expanded=""false"" tabindex=""-1"" aria-haspopup=""false"">  <div style=""display: none;""></div> </div></span>
</div>",
                          CreatedBy = 1,
                          CreatedDate = System.DateTime.Now,
                          Deleted = false,
                          Subject = ""


                      },

                      new EmailTemplate
                      {

                          EmailTemplateId = 7,
                          EmailTemplateName = "PasswordReset",
                          EmailTemplateDescription = @"<div role=""document"">
    <div class=""_rp_T4 _rp_U4 ms-font-weight-regular ms-font-color-neutralDark"" style=""display: none;""><br></div>  <div autoid=""_rp_w"" class=""_rp_T4"" style=""display: none;""><br></div>  <div autoid=""_rp_x"" class=""_rp_T4"" id=""Item.MessagePartBody"" style="""">
        <div class=""_rp_U4 ms-font-weight-regular ms-font-color-neutralDark rpHighlightAllClass rpHighlightBodyClass"" id=""Item.MessageUniqueBody"" style=""font-family: wf_segoe-ui_normal, &quot;Segoe UI&quot;, &quot;Segoe WP&quot;, Tahoma, Arial, sans-serif, serif, EmojiFont;"">
            <div class=""rps_ad57"">
                <div>
                    <div>
                        <div style=""margin: 0px; padding: 0px; font-family: Verdana, Helvetica, Arial, sans-serif, serif, EmojiFont; color: rgb(103, 103, 103);"">
                            <table cellpadding=""0"" cellspacing=""0"" style=""padding-top:0px; background-color:#FFFFFF; width:100%; border-collapse:separate"" class=""e-rte-table"">
                                <tbody>
                                    <tr>
                                        <td align=""center"" class="""">
                                            <table cellpadding=""0"" cellspacing=""0"" width=""600"" style=""padding:0px 24px 10px; background-color:white; border-collapse:separate; border:1px solid #e7e7e7; border-bottom:none"" class=""e-rte-table"">
                                                <tbody>
                                                    <tr>
                                                        <td><br></td>
                                                    </tr>
                                                    <tr>
                                                        <td align=""center"" style=""min-width:590px"">
                                                            <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""padding:20px 0 0; border-collapse:separate"" class=""e-rte-table"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td valign=""middle"" class="""">
                                                                            <h1 style=""color:#676767; font-weight:400; margin:0px"">Password Reset Request</h1>
                                                                        </td>
                                                                        <td valign=""middle"" align=""right"" width=""200px"">{%emailogo%}</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan=""2"" style=""text-align:center"">
                                                                            <hr width=""100%"" style=""background-color:rgb(204,204,204); border:medium none; clear:both; display:block; font-size:0px; min-height:1px; line-height:0; margin:4px 0px 16px 0px"">
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""min-width:590px"">
                                                            <table class=""e-rte-table"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="""">
                                                                            <div style=""margin-left:1.2rem; margin-bottom:1em"">
                                                                                <h5 style=""font-weight:400; margin-bottom:0; font-size:16px; color:#676767"">Hello {%helloname%}</h5><div><br></div>
                                                                                <p>We have received a request to reset your account password. To proceed with the password reset, please click on the link below:</p>
                                                                                <div style=""margin:20px 0 0 0; text-align:center"">{%setpasswordlink%}</div>
                                                                                <br>If you did not request a password reset, Please ignore this email. Your account will&nbsp;<span style=""background-color: transparent; text-align: inherit;"">remain secure, and no action is required.</span></div><div style=""margin-left:1.2rem; margin-bottom:1em""><span style=""background-color: transparent; text-align: inherit;""><p>For security reasons, this link will expire in 2 hours. If you&nbsp;<span style=""background-color: transparent; text-align: inherit;"">are unable to reset your password within this time frame,&nbsp;</span><span style=""background-color: transparent; text-align: inherit;"">please request another password reset.</span></p></span></div>
                                                                         
                                                                            <div style=""margin-left:1.2rem; margin-bottom:1em"">
                                                                                <p style=""color:#676767; line-height:145%; margin:10px 0 0 0; font-size:16px"">
                                                                                    {%emailsignature%}
                                                                                </p>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style=""width:100%"" class=""e-rte-table"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <div style=""text-align:center; border-top:1px solid rgb(230,230,230); padding-bottom:20px; padding-top:15px; line-height:125%; font-size:11px; margin:20px 20px 0 20px"">
                                                                                <p style=""color:rgb(115,115,115); font-size:10px"">© Copyright {%companyname%}, {%address%} </p>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align=""right"">
                                                                            <div style="" margin:0 20px"">{%footerletterhera%}</div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style=""width:100%"" class=""e-rte-table"">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <div style=""text-align:justify; border-top:1px solid rgb(230,230,230); padding-bottom:10px; padding-top:10px; line-height:125%; font-size:10px; margin:25px 20px 0 20px"">
                                                                                <p style=""color:rgb(115,115,115); margin:0; font-size:10px"">
                                                                                    The information contained in this e-mail message and/or attachments to it may contain confidential
                                                                                    or privileged information. If you are not the intended recipient, any dissemination,use, review, distribution,
                                                                                    printing or copying of the information contained in this email message and/or attachments to it are strictly prohibited.
                                                                                    If you have received this communication in error, please notify us by reply e-mail or telephone and immediately
                                                                                    and permanently delete the message and any attachments. Thank you.
                                                                                </p>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div> <div class=""_rp_c5"" style=""display: none;""><br></div>
    </div>  <span class=""PersonaPaneLauncher""><div ariatabindex=""-1"" class=""_pe_d _pe_62"" aria-expanded=""false"" tabindex=""-1"" aria-haspopup=""false"">  <div style=""display: none;""><br></div> </div></span>
</div>",
                          CreatedBy = 1,
                          CreatedDate = System.DateTime.Now,
                          Deleted = false,
                          Subject = "Password Reset Request"
                      }




                    );
            });

        }
    }
}
