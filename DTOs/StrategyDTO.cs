using System.Collections.Generic;
using System.Xml.Serialization;

namespace ClientViTrader.DTOs
{
    [XmlRoot(ElementName = "strategy")]
    public class StrategyDTO
    {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }

        [XmlElement(ElementName = "name")]
        public string name { get; set; }

        [XmlElement(ElementName = "userId")]
        public int userId { get; set; }

        [XmlElement(ElementName = "triggers")]
        public List<TriggerDTO> triggers { get; set; }
    }
}
