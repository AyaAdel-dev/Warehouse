namespace EntityProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class transaction_log
    {
        [Key]
        public int transaction_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime transaction_date { get; set; }

        [Required]
        [StringLength(50)]
        public string transaction_label { get; set; }

        public int? product_id { get; set; }

        public int? entry_id { get; set; }

        public int? product_count { get; set; }

        [Column(TypeName = "date")]
        public DateTime? production_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expiry_date { get; set; }

        [StringLength(50)]
        public string store_name { get; set; }

        [StringLength(50)]
        public string new_store_name { get; set; }

        [StringLength(50)]
        public string supplier_name { get; set; }

        [StringLength(50)]
        public string customer_name { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }

        public virtual Store Store1 { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
