using System;

namespace JsonGeneratorApp.Models
{
    public class Record
    {
        public int UserId { get; set; }
        
        public long Pan { get; set; }
        
        public DateTime ExpDate { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Phone { get; set; }
    }
}