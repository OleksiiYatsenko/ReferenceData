using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Model
{
    [DataContract]
    public class Country : IEquatable<Country>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }

        public bool Equals(Country other)
        {
            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return (obj is Country) ? Equals((Country)obj) : false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
