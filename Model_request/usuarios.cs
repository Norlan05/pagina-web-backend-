using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CLINICA.Model_request
{
    public class usuarios
    {
        [Key]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime created_at { get; set; }
        public string ResetToken { get; set; }
        public DateTime ResetTokenExpiry { get; set; }
    }
}
