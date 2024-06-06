namespace CodeFirstCoches.Models
{
    public class Coche
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Motor { get; set; }
        public int EscuderisrId { get; set; }
    }
}
