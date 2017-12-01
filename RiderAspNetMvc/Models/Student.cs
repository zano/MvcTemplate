using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.WebPages;

namespace RiderAspNetMvc.Models {
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class Student {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name => string.Join(" ", FirstName, LastName).Trim();
        
        public virtual ICollection<Enrollment> Entrollments { get; set; }

    }
}
