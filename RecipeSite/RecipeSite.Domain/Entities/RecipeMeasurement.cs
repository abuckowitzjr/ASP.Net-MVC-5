using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSite.Domain.Entities
{
    public class RecipeMeasurement
    {
        [Key]
        public int RecipeMeasurementID { get; set; }
        public string RecipeMeasurementName { get; set; }
        public string RecipeMeasurementNamePlural { get; set; }
        public string RecipeMeasurementType { get; set; }
    }
}
