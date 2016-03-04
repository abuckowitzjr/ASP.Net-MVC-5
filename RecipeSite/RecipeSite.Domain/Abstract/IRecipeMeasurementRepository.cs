using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeSite.Domain.Entities;

namespace RecipeSite.Domain.Abstract
{
    public interface IRecipeMeasurementRepository
    {
        IEnumerable<RecipeMeasurement> RecipeMeasurement { get; } 
    }
}
