using AspNedelja3Vezbe.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected EfUseCase(VezbeDbContext context)
        {
            Context = context;
        }

        protected VezbeDbContext Context { get; }
    }
}
