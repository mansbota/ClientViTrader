using System.Xml.Serialization;

namespace ClientViTrader.DTOs
{
    [XmlRoot(ElementName = "position")]
    public class PositionDTO
    {
        [XmlElement(ElementName ="id")]
        public int id { get; set; }

        [XmlElement(ElementName = "userId")]
        public int userId { get; set; }

        [XmlElement(ElementName = "cryptoId")]
        public int cryptoId { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal amount { get; set; }

        public decimal value { get; set; }
        public string cryptoName { get; set; }
    }
}
