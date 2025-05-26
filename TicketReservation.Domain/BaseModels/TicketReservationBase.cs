using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketReservation.Domain.BaseModels
{
    public class TicketReservationBase : IValidatableObject
    {

        [Required]
        [StringLength(80)]
        public string FullName { get; set; }

        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("Date Must be In The Future", new[] { nameof(Date) });
            }
        }
    }
}
