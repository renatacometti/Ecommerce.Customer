using AutoMapper;
using Domain.DTOs.Profile;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Ecommerce.Customer.Controllers
{

    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private IProfileService _profileService;
    

        public ProfileController(IProfileService profileService) 
        {
            
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<ActionItemResponse<int?>> Add([FromBody] ProfileDTO profile)
        {
            try
            {
                return new ActionItemResponse<int?>(await _profileService.Add(profile));
            }
            catch (Exception ex)
            {
                return new ActionItemResponse<int?>(ex);
            }
        }
    }
}
