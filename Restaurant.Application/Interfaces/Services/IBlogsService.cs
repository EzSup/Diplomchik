using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IBlogsService
    {
        Task<ICollection<Blog>> GetAll();
        Task<Blog> GetById(Guid id);
        Task<Guid> Add(Blog entity);
        Task<bool> Update(Blog entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Blog>> GetByPage(int page, int pageSize);
        Task<ICollection<Blog>> GetByFilter(DateTime after, string? AuthorName = null, string? TitleContains = null);
    }
}
