//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GamusinosProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Armor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Armor()
        {
            this.Armor_Craft = new HashSet<Armor_Craft>();
            this.Players = new HashSet<Player>();
            this.Inventories = new HashSet<Inventory>();
        }
    
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long vitalityBonus { get; set; }
        public long resistanceBonus { get; set; }
        public long forceBonus { get; set; }
        public long speedBonus { get; set; }
        public long luckBonus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Armor_Craft> Armor_Craft { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Players { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
