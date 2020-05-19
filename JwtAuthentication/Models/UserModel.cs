using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace JwtAuthentication.Models
{
    public class UserModel
    {
        [DataMember(Name = "Id")]
        //[Required(ErrorMessage = "Id is Required")]
        public int Id { get; set; }

        [DataMember(Name = "FirstName")]
        //[Required(ErrorMessage = "FirstName is Required")]
        public string FirstName {get;set;}

        [DataMember(Name = "LastName")]
        //[Required(ErrorMessage = "LastName is Required")]
        public string LastName {get;set;}

        [DataMember(Name = "Email")]
        //[Required(ErrorMessage = "Email is Required")]
        public string Email {get;set;}

        [DataMember(Name = "Gender")]
        //[Required(ErrorMessage = "Gender is Required")]
        public string Gender {get;set;}

        [DataMember(Name = "DateOfBirth")]
        //[Required(ErrorMessage = "DateOfBirth is Required")]
        public string DateOfBirth {get;set;}

        [DataMember(Name = "Password")]
        //[Required(ErrorMessage = "Password is Required")]
        public string Password {get;set;}
    }
}
