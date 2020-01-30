using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public long Id { get; set; }
        public Group Group { get; set; }
        public int? GroupId { get; set; }
        public TimeTable TimeTable { get; set; } 
        public bool IsSignedGroup { get; set; }
    }
}
