using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Games;
public class Game
{
    public string GameName { get; set; }
    public string Description { get; set; }
    public string GamePreviewUrl { get; set; }
    public string Author { get; set; }
    public Filters Filters { get; set; }
    public GamePrice Price { get; set; }
 
}
