using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SharpSaster.Data
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }


        [Required]
        public string Login { get; set; }

        public string Name { get; set; }

        public Account() { }
    }
}
