using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Model
{
    [DataContract]
    public class Subdivision : IEquatable<Subdivision>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? CountryId { get; set; }

        public bool Equals(Subdivision other)
        {
            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return (obj is Subdivision) ? Equals((Subdivision)obj) : false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
