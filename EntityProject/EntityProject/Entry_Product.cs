namespace EntityProject
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Entry_Product
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Entry_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Product_id { get; set; }

        public int? count_product { get; set; }

        [Column(TypeName = "date")]
        public DateTime? prod_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime expire_date { get; set; }

        public virtual Entry_document Entry_document { get; set; }

        public virtual Product Product { get; set; }
    }
}
