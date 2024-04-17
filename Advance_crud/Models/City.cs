using System.ComponentModel.DataAnnotations;

namespace Advance_crud.Models
{
    public class City
    {
        [Key]
        public int city_id { get; set; }
        public string city_name { get; set; }
        public int state_id {  get; set; }
        public State? State { get; internal set; }
    }
}
