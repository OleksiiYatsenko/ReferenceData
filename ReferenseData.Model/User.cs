using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Model
{
    [DataContract]
    public class User : IEquatable<User>
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SecondName { get; set; }
        [DataMember]
        public int CountryId { get; set; }
        [DataMember]
        public int? SubdivisionId { get; set; }
        [DataMember]
        public int? LocationId { get; set; }

        public bool Equals(User other)
        {
            return this.Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return (obj is User) ? Equals((User)obj) : false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
