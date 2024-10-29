using BussinessLayer.Interfaces.IAutenticacion;
using Microsoft.AspNetCore.Http; // Import this namespace
using System;
using System.Security.Claims;

namespace BussinessLayer.Services.SAutenticacion
{
    public class ClaimsService(IHttpContextAccessor httpContextAccessor) : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string GetClaimValueByType(string claimType)
        {
            // Validate claimType
            if (string.IsNullOrEmpty(claimType))
            {
                throw new ArgumentException("Claim type cannot be null or empty.", nameof(claimType));
            }

            // Check if the current user is authenticated
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User as ClaimsPrincipal;
            if (claimsPrincipal == null || !claimsPrincipal.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Find the claim
            var claim = claimsPrincipal.FindFirst(claimType);

            // Return the claim value or null if it does not exist
            return claim?.Value;
        }
    }
}
