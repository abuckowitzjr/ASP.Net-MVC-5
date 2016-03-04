using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSite.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustFirstName { get; set; }
        public string CustLastName { get; set; }
        public string CustAddress { get; set; }
        public string CustCity { get; set; }
        public string CustState  { get; set; }
        public int CustZipcode { get; set; }
        public char CustPhoneNumber { get; set; }
        public string CustUsername { get; set; }
        public string CustPassword   { get; set; }
        public string CustEmail { get; set; }

    }
}
