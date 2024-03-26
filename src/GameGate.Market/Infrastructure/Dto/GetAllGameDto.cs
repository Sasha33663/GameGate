using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto;
public class GetAllGameDto
{
    public IEnumerable<GameDto> Games { get; set; }
}
