using AutoMapper;
using Domain.DTOs.Profile;
using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.ViewModel;

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

        [HttpGet("{id}")]
        public async Task<ActionItemResponse<ProfileGetByIdDTO>> GetById([FromRoute] int id)
        {
            ActionItemResponse<ProfileGetByIdDTO> actionResponse;

            try
            {
                var profile = await _profileService.GetById(id);

                actionResponse = new ActionItemResponse<ProfileGetByIdDTO>(profile);
            }
            catch (Exception ex)
            {
                actionResponse = new ActionItemResponse<ProfileGetByIdDTO>(ex);
            }

            return actionResponse;
        }

    }
}
