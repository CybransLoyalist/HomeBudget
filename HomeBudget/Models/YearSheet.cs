using System.ComponentModel.DataAnnotations;

namespace HomeBudget.Models
{
    [ExcludeFromCoverage]
    public class YearSheet
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public ApplicationUser User { get; set; }
    }
}