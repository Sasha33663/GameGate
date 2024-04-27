using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;
public class ReactionRepository : IReactionRepository
{
    public Task<Reaction> CreateReactionAsync(Reaction reaction)
    {
        throw new NotImplementedException();
    }
}
