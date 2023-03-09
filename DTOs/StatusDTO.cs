using System.Xml.Serialization;

namespace ClientViTrader.DTOs
{
    [XmlRoot(ElementName = "status")]
    public class StatusDTO
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }

        [XmlElement(ElementName = "name")]
        public string name { get; set; }
    }
}
