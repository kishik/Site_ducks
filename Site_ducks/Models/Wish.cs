using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Site_ducks.Models
{
    [DataContract]
    public class RandomWish
    {
        [DataMember]
        public string Pic { get; set; }
    }
}
