using System;
using System.ComponentModel.DataAnnotations;
 
/*
 * About - Basic class to be made into an in memory db for the example.
 */
namespace Core2Walkthrough_Basic.Data
{
    public class USERS
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string NAME { get; set; }

        public DateTime DATE_ENTERED { get; set; }

    }
}
