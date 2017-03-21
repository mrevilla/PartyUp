using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PartyUp.Models
{
    public class Invited
    {
        [Key]
        [Column(Order = 1)]
        public string InvitedUserId { get; set; }
        public ApplicationUser InvitedUser { get; set; }

        [Key]
        [Column(Order = 2)]
        public int EventsId { get; set; }
        public Events Events { get; set; }
    }
}