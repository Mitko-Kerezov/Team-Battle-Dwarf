//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SummerOlympiads.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Discipline
    {
        public Discipline()
        {
            this.Events = new HashSet<Event>();
        }
    
        public int DisciplineId { get; set; }
        public string Name { get; set; }
        public Nullable<int> SportId { get; set; }
    
        public virtual Sport Sport { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
