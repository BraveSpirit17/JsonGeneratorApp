using System;
using System.Xml.Serialization;

namespace JsonGeneratorApp.Models
{
    public class Card
    {
        [XmlAttribute("UserId")]
        public int UserId { get; set; }
        
        public long Pan { get; set; }
        
        public DateTime ExpDate { get; set; }
    }
}