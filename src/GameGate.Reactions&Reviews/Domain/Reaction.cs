using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;
public sealed class Reaction
{
    public string AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public string? Reviews { get; set; }
    public string GameName { get; set; }
    public Guid ReactionId { get; set; }
    public int Stars { get; set; }
    
}
