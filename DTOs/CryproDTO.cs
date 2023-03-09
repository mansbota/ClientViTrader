using System.Xml.Serialization;

namespace ClientViTrader.DTOs
{
    [XmlRoot(ElementName = "crypto")]
    public class CryptoDTO
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }

        [XmlElement(ElementName = "ticker")]
        public string ticker { get; set; }

        [XmlElement(ElementName = "name")]
        public string name { get; set; }
    }
}
