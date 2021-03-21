using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F20ITONK.ASPNETCore.MicroService.ClassLib.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
        }

        [Key]
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingrediens { get; set; }
    }
}
