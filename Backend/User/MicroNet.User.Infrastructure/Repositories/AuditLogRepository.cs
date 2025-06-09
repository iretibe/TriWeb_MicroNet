using MicroNet.User.Core.Dto.Audit;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Models;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly UserContext _context;
        private readonly IdentityUserContext _usercontext;

        public AuditLogRepository(UserContext context, IdentityUserContext userContext)
        {
            _context = context;
            _usercontext = userContext;
        }

        public async Task<AuditLog> AddAuditLogAsync(AuditLog entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<AuditLogDto>> GetAllAuditLogsAsync()
        {
            var aspNetUsers = await _usercontext.AspNetUsers
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.PhoneNumber
                }).ToListAsync();

            var auditLogs = await _context.AuditLogs
                .Select(al => new
                {
                    al.Id,
                    al.AuditDate,
                    al.UserId,
                    al.Data,
                    al.Method,
                    al.EntityType
                }).ToListAsync();

            var query = from al in auditLogs
                        join u in aspNetUsers on al.UserId equals u.Id
                        select new AuditLogDto
                        {
                            Id = al.Id,
                            AuditDate = al.AuditDate,
                            UserId = al.UserId,
                            FullName = u.FullName,
                            Data = al.Data,
                            Method = al.Method,
                            EntityType = al.EntityType
                        };

            return query;
        }

        public async Task<AuditLogByIdDto> GetAuditLogByIdAsync(Guid Id)
        {
            var aspNetUsers = await _usercontext.AspNetUsers
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.PhoneNumber
                }).ToListAsync();

            var auditLogs = await _context.AuditLogs
                .Select(al => new
                {
                    al.Id,
                    al.AuditDate,
                    al.UserId,
                    al.Data,
                    al.Method,
                    al.EntityType
                }).ToListAsync();

            var query = from al in auditLogs
                        join u in aspNetUsers on al.UserId equals u.Id
                        where al.Id == Id
                        select new AuditLogByIdDto
                        {
                            Id = al.Id,
                            AuditDate = al.AuditDate,
                            UserId = al.UserId,
                            FullName = u.FullName,
                            Data = al.Data,
                            Method = al.Method,
                            EntityType = al.EntityType
                        };

            return query.FirstOrDefault()!;
        }
    }
}
