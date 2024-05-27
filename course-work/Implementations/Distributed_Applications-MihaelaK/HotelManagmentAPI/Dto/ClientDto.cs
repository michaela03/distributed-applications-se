using System.ComponentModel.DataAnnotations;

namespace HotelManagmentAPI.Dto
{
    public class ClientDto
    {
       
        public int ClientID { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

   
        public string? Email { get; set; }

       
        public string? PhoneNumber { get; set; }

       
        public DateTime DateOfBirth { get; set; }

        public string? Address { get; set; }

    
    }


}

