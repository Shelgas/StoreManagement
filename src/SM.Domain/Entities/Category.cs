using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Domain.Entities
{
    public class Category
    {

        public int Id { get; set; }

        [DisplayName("Название категории")]
        public string Name { get; set; }

    }
}
