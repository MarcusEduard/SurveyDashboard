using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;
public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            // Basis role policies
            options.AddPolicy("MustBeGuest", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Guest"));

            options.AddPolicy("MustBeEnergy", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Energy"));

            options.AddPolicy("MustBeHealth", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Health"));

            options.AddPolicy("MustBeTech", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Tech"));

            options.AddPolicy("MustBeAdmin", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));

            // Guest can only access the survey
            options.AddPolicy("SurveyAccess", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    var roleClaim = context.User.FindFirst("Role");
                    return roleClaim != null;
                }));

            // Everyone except guest can access the dashboard
            options.AddPolicy("NotGuest", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    var roleClaim = context.User.FindFirst("Role");
                    return roleClaim != null && !roleClaim.Value.Equals("Guest", StringComparison.OrdinalIgnoreCase);
                }));

            // Only adnim can see Manage Data
            options.AddPolicy("ManageDataAccess", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Admin", "Tech", "Health", "Energy"));

            // Tech/Health/Energy can see their own sector data - except admin (can see all)
            options.AddPolicy("SectorDataAccess", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    var roleClaim = context.User.FindFirst("Role");
                    if (roleClaim == null) return false;
                    return roleClaim.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                           roleClaim.Value.Equals("Tech", StringComparison.OrdinalIgnoreCase) ||
                           roleClaim.Value.Equals("Health", StringComparison.OrdinalIgnoreCase) ||
                           roleClaim.Value.Equals("Energy", StringComparison.OrdinalIgnoreCase);
                }));

            // Only guest can access the survey
            options.AddPolicy("GuestSurveyOnly", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Guest"));

            // Everyone except admin can see the survey
            options.AddPolicy("NotAdminSurveyAccess", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    var roleClaim = context.User.FindFirst("Role");
                    return roleClaim != null && !roleClaim.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase);
                }));

            options.AddPolicy("SecurityLevel2OrAbove", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    Claim? levelClaim = context.User.FindFirst(claim => claim.Type.Equals("SecurityLevel"));
                    if (levelClaim == null) return false;
                    return int.Parse(levelClaim.Value) >= 2;
                }));
        });
    }
}
