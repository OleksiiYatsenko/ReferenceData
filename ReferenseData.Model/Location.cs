using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Model
{
    [DataContract]
    public class Location : IEquatable<Location>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int SubdivisionId { get; set; }

        public bool Equals(Location other)
        {
            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return (obj is Location) ? Equals((Location)obj) : false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
