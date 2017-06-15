using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeBudget.HelpersAndExtensions;

namespace HomeBudget.Models
{
    public enum SheetType
    {
        Monthly,
        Summary,
        YearSpan
    }

    public class Sheet
    {
        [Key]
        public int Id { get; set; }

        [Column("Type")]
        [MaxLength(100)]
        public string TypeString
        {
            get
            {
                return Type.ToString();
            }
            private set
            {
                Type = value.ParseEnum<SheetType>();
            } 
        }

        [NotMapped]
        public SheetType Type { get; set; }

        public int YearSheet_Id { get; set; }

        [ForeignKey("YearSheet_Id")]
        public YearSheet YearSheet { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
    }

    [ExcludeFromCoverage]
    public class YearSheet
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }
        
        public string User_Id { get; set; }

        [ForeignKey("User_Id")]
        public ApplicationUser User { get; set; }

        public List<Sheet> Sheets { get; set; }
    }
}