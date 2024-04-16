using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.ToDo.Application.Dto
{
    public sealed record CreateTodoDto(
        string Title,
        string Description
    );

}
