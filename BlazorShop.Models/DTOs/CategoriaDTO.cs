using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BlazorShop.Models.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
                
        public string Nome { get; set; } = string.Empty;
        
        public string IconCSS { get; set; } = string.Empty;
    }
}
