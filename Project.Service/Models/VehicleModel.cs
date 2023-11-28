using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
      
        public virtual VehicleMake? Make { get; set; }
    }
}
