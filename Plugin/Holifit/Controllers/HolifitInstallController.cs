using CloudVOffice.Core.Domain.Users;
using CloudVOffice.Core.Infrastructure.Applications;
using CloudVOffice.Core.Infrastructure.Http;
using CloudVOffice.Core.Infrastructure.JSON;
using CloudVOffice.Data.DTO.Permission;
using CloudVOffice.Services.Applications;
using CloudVOffice.Services.Plugins;
using CloudVOffice.Services.Roles;
using CloudVOffice.Web.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holifit.Controllers
{
	[Area(AreaNames.Holifit)]
	public class HolifitInstallController : Controller
	{
		private readonly IApplicationInstallationService _applicationInstallationService;
		private readonly IRoleService _roleService;
		private readonly IHttpWebClients _httpClient;
		private readonly string InstationPath = "Holifit";
		public HolifitInstallController(IApplicationInstallationService applicationInstallationService, IRoleService roleService,
			IHttpWebClients httpClient
			)
		{
			_applicationInstallationService = applicationInstallationService;
			_roleService = roleService;
			_httpClient = httpClient;

		}

		public async Task<IActionResult> Install(Int64 CreatedBy)
		{
			string jsonPath = @".\" + CloudVOfficePluginDefaults.PathName + @"\" + InstationPath + @"\";
			string pluginDetails = jsonPath + "plugin.json";
			PluginConfig item = await JsonFileReader.ReadAsync<PluginConfig>(pluginDetails);

			_applicationInstallationService.InstallApplication(item.SystemName, CreatedBy, item.Version);



			string rolesJsonpath = jsonPath + "roles.json";
			RoleConfig roleJson = await JsonFileReader.ReadAsync<RoleConfig>(rolesJsonpath);
			List<Role> roles = new List<Role>();
			for (int i = 0; i < roleJson.Roles.Count; i++)
			{
				Role role = await _roleService.CreateRole(roleJson.Roles[i], CreatedBy);
				roles.Add(role);
			}



			string applicationJsonpath = jsonPath + "Application.json";
			ApplicationConfig applicationJson = await JsonFileReader.ReadAsync<ApplicationConfig>(applicationJsonpath);

			var app = await _applicationInstallationService.CreateApplication(new ApplicationDTO()
			{
				ApplicationName = applicationJson.ApplicationName,
				Parent = null,
				IsGroup = applicationJson.IsGroup,
				Url = applicationJson.Url,
				IconImageUrl = applicationJson.IconImageUrl,
				IconClass = applicationJson.IconClass,
				AreaName = applicationJson.AreaName,
				CreatedBy = CreatedBy

			});

			applicationJson.ServerApplicationId = app.ApplicationId;

			for (int k = 0; k < applicationJson.Roles.Count; k++)
			{
				int roleId = roles.Where(x => x.RoleName == applicationJson.Roles[k]).FirstOrDefault().RoleId;
				_applicationInstallationService.CreateRoleAndApplicationWisePermission(roleId, int.Parse(applicationJson.ServerApplicationId.ToString()), CreatedBy);
			}
			for (int i = 0; i < applicationJson.Children.Count; i++)
			{
				var sapp = await _applicationInstallationService.CreateApplication(new ApplicationDTO()
				{
					ApplicationName = applicationJson.Children[i].ApplicationName,
					Parent = applicationJson.ServerApplicationId,
					IsGroup = applicationJson.Children[i].IsGroup,
					Url = applicationJson.Children[i].Url,
					IconImageUrl = applicationJson.Children[i].IconImageUrl,
					IconClass = applicationJson.Children[i].IconClass,
					AreaName = applicationJson.Children[i].AreaName,
					CreatedBy = CreatedBy
				});


				applicationJson.Children[i].ServerApplicationId = sapp.ApplicationId;
				for (int k = 0; k < applicationJson.Children[i].Roles.Count; k++)
				{
					int roleId = roles.Where(x => x.RoleName == applicationJson.Children[i].Roles[k]).FirstOrDefault().RoleId;
					_applicationInstallationService.CreateRoleAndApplicationWisePermission(roleId, int.Parse(applicationJson.Children[i].ServerApplicationId.ToString()), CreatedBy);
				}

				for (int j = 0; j < applicationJson.Children[i].Children.Count; j++)
				{
					var tapp = await _applicationInstallationService.CreateApplication(new ApplicationDTO()
					{
						ApplicationName = applicationJson.Children[i].Children[j].ApplicationName,
						Parent = applicationJson.Children[i].ServerApplicationId,
						IsGroup = applicationJson.Children[i].Children[j].IsGroup,
						Url = applicationJson.Children[i].Children[j].Url,
						IconImageUrl = applicationJson.Children[i].Children[j].IconImageUrl,
						IconClass = applicationJson.Children[i].Children[j].IconClass,
						AreaName = applicationJson.Children[i].Children[j].AreaName,
						CreatedBy = CreatedBy
					});
					applicationJson.Children[i].Children[j].ServerApplicationId = tapp.ApplicationId;
					for (int k = 0; k < applicationJson.Children[i].Children[j].Roles.Count; k++)
					{
						int roleId = roles.Where(x => x.RoleName == applicationJson.Children[i].Children[j].Roles[k]).FirstOrDefault().RoleId;
						_applicationInstallationService.CreateRoleAndApplicationWisePermission(roleId, int.Parse(applicationJson.Children[i].Children[j].ServerApplicationId.ToString()), CreatedBy);
					}

				}

			}


			return Ok(item);

		}
	}
}
