using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EnjoyEat.Models.ViewModel
{
    public class MemberRegisterViewModel
    {
  
      
        public string FirstName { get; set; } = null!;
   
        public string LastName { get; set; } = null!;
      
        public string Gender { get; set; } = null!;
      
        public DateTime Birthday { get; set; }
   
        public string Address { get; set; } = null!;
     
        public string Phone { get; set; } = null!;
 
  
        public string Email { get; set; } = null!;
       
        public string Account { get; internal set; } = null!;
      
        public string Password { get; internal set; } = null!;

        public string LevelName { get; set; } = null!;

        public int MemberId { get; set; }


    }


}

