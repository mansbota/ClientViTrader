using System;
using System.Xml.Serialization;

namespace ClientViTrader.DTOs
{
    [XmlRoot(ElementName = "user")]
    public class UserDTO
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }

        [XmlElement(ElementName = "username")]
        public string username { get; set; }

        [XmlElement(ElementName = "password")]
        public string password { get; set; }

        [XmlElement(ElementName = "email")]
        public string email { get; set; }

        [XmlElement(ElementName = "dateCreated")]
        public DateTime dateCreated { get; set; }

        [XmlElement(ElementName = "salt")]
        public string salt { get; set; }

        [XmlElement(ElementName = "statusId")]
        public int statusId { get; set; }
    }
}
