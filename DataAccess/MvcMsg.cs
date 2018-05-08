namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MvcMsg")]
    public partial class MvcMsg
    {
        [Key]
        public int pkID { get; set; }

        public int? MsgTo { get; set; }

        public int? MsgBy { get; set; }

        [StringLength(50)]
        public string Msg { get; set; }

        public DateTime? MsgDate { get; set; }
    }
}
