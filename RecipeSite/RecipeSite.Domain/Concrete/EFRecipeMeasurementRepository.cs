using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Concrete
{
    public class EFRecipeMeasurementRepository : IRecipeMeasurementRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<RecipeMeasurement> RecipeMeasurement
        {
            get { return context.RecipeMeasurement; }
        }
    }
}
