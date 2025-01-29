using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Enums;

namespace AcologAPI.src.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string? Placa { get; set; }
        [Required]
        public int? Eixos { get; set; }
        [Required]
        public TypeVehicle? typeVehicle  { get; set; }
    }
}