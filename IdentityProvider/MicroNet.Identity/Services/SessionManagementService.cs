using MicroNet.Identity.Data;
using MicroNet.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MicroNet.Identity.Services
{
    public class SessionManagementService
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionManagementService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsUserLoggedInOnAnotherDevice(string userId, string currentSessionId)
        {
            var activeSessions = _dbContext.ActiveSessions
                .Where(s => s.UserId == userId && s.SessionId != currentSessionId)
                .ToList();

            // If there are any active sessions for the user other than the current one, return true
            return activeSessions.Any();
        }

        public void StoreSession(string userId, string sessionId)
        {
            var session = new ActiveSession
            {
                UserId = userId,
                SessionId = sessionId,
                LoginTime = DateTime.UtcNow,
                LastActivityTime = DateTime.UtcNow
            };

            _dbContext.ActiveSessions.Add(session);
            _dbContext.SaveChanges();
        }
    }

    // Middleware to check session on every request
    public class SessionValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, SessionManagementService sessionService)
        {
            try
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionId = context.Session.Id;

                if (userId != null && sessionService.IsUserLoggedInOnAnotherDevice(userId, sessionId))
                {
                    // Log the user out if logged in on another device
                    await context.SignOutAsync();
                    context.Response.Redirect("/Account/Login"); // Redirect to login page
                    return;
                }

                await _next(context);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"InvalidCastException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}
