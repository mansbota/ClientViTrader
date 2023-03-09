using System;
using System.Xml.Serialization;

namespace ClientViTrader.DTOs
{
    [XmlRoot(ElementName = "trade")]
    public class TradeDTO
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }

        [XmlElement(ElementName = "userId")]
        public int userId { get; set; }

        [XmlElement(ElementName = "cryptoId")]
        public int cryptoId { get; set; }

        [XmlElement(ElementName = "tradeTime")]
        public DateTime tradeTime { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal amount { get; set; }

        [XmlElement(ElementName = "tradeTypeId")]
        public int tradeTypeId { get; set; }

        public string cryptoName { get; set; }
        public string tradeType { get; set; }
    }
}
