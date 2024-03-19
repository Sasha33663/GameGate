using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;
public class Game
{
    public string GameName { get; set; }
    public string Description { get; set; }
    public string GamePreviewUrl { get; set; }
    public string GamePreviewId { get; set; }
    public Guid GameId { get; set; }
    public string UserId { get; set; }
    public string Creator { get; set; }
    public string Genre { get; set; }
    public string Kind { get; set; }

}
