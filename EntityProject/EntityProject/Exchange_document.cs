namespace EntityProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exchange_document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exchange_document()
        {
            Exchange_product = new HashSet<Exchange_product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int exchange_id { get; set; }

        [StringLength(50)]
        public string store_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? exchange_date { get; set; }

        [StringLength(50)]
        public string customer_name { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Store Store { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exchange_product> Exchange_product { get; set; }
    }
}
