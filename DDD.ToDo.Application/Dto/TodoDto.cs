using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.ToDo.Application.Dto
{
    public sealed record TodoDto(
        Guid Id,
        string Title,
        string Description,
        bool IsDone);
}
