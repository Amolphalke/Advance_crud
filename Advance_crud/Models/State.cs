using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advance_crud.Models
{
    public class State
    {
        [Key]
        public int state_id { get; set; }
        public string state_name { get; set; }
        public int country_id { get; set;}
        public Country? Country { get;  set; }

        public IList<City>? Cities { get; set; }
    }
}
