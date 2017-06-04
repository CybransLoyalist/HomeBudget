using System.ComponentModel.DataAnnotations;

namespace HomeBudget.Models
{
    [ExcludeFromCoverage]
    public class Sheet
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }
    }
}