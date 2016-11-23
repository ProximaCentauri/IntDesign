using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model
{
    public class Person : IDataErrorInfo
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
        public string ImageSourceLocation { get; set; }

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

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName))
                        result = "Please enter a First Name";
                }
                if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName))
                        result = "Please enter a Last Name";
                }
                
                return result;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
