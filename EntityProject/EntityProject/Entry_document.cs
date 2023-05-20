namespace EntityProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Entry_document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entry_document()
        {
            Entry_Product = new HashSet<Entry_Product>();
            Existing_products = new HashSet<Existing_products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Entry_id { get; set; }

        [StringLength(50)]
        public string store_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [StringLength(50)]
        public string supplier_name { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entry_Product> Entry_Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Existing_products> Existing_products { get; set; }
    }
}
