﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public class BillPay
    {
        // BillPay
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int BillPayID { get; set; }

        // AccountNumber FK
        //[Required]
        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        // PayeeID
        public int PayeeID{ get; set; }

        // Amount
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        // Date
        [DataType(DataType.DateTime)]
        public DateTime ScheduleTimeUtc { get; set; }

        // Period
        [Required]
        public string Period { get; set; }

        // Status 
        public string Status { get; set; } 
    }
}
