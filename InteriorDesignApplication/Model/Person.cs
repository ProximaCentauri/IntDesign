using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        
        [Column(TypeName="Date")]
        public DateTime? BirthDate { get; set; }

        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string LandLineNumber { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string BirthPlace { get; set; }      
        public string Status { get; set; }          
        public string ValidId { get; set; }        
        public string Nationality { get; set; }
        public string NumBuilding { get; set; }
        public string Street { get; set; }
        public string VillageDistrict { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        [NotMapped]        
        public string Address
        {
            get
            {
                return NumBuilding + " " +
                    Street + " " +
                    VillageDistrict + " " +
                    City + " " +
                    State + " " +
                    Country + " " +
                    ZipCode + " ";
            }            
        }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " +
                    MiddleName + " " +
                    LastName;
            }
        }
    }
}
