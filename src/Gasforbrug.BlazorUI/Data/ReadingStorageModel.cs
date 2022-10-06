using System.ComponentModel.DataAnnotations;

namespace Gasforbrug.BlazorUI.Data
{
    public sealed class ReadingStorageModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Skal være et positivt tal.")]
        public decimal Value { get; set; }
    }
}
