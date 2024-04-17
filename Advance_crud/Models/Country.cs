
using System.ComponentModel.DataAnnotations;

namespace Advance_crud.Models
{
    public class Country
    {
        [Key]
        public int country_id {  get; set; }
        public string country_name { get; set;}
       public ICollection<State> States { get; set; }
    }
}
