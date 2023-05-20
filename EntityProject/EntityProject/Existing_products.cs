namespace EntityProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Existing_products
    {
        [Key]
        public int record_id { get; set; }

        public int? product_id { get; set; }

        public int? product_count { get; set; }

        [StringLength(50)]
        public string store_name { get; set; }

        public int? entry_id { get; set; }

        [StringLength(50)]
        public string supplier_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? production_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expiry_date { get; set; }

        public virtual Entry_document Entry_document { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
