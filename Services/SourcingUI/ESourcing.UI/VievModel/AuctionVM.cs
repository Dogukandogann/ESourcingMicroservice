using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.VievModel
{
    public class AuctionVM
    {
        public AuctionVM()
        {
            IncludedSellers = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Please fill Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please fill Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please fill Product")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Please fill Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please fill Start Date")]
        public DateTime StartedAt { get; set; }

        [Required(ErrorMessage = "Please fill Finish Date")]
        public DateTime FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }

        public string SellerId { get; set; }
        public List<string> IncludedSellers { get; set; }
    }
}
