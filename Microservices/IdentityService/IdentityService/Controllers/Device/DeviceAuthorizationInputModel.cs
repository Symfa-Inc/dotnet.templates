using IdentityService.Controllers.Consent;

namespace IdentityService.Controllers.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}