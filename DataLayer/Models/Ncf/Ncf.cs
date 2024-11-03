namespace DataLayer.Models.Ncf
{
    public class Ncf
    {
        public int Id { get; set; }
        public string? NcfType { get; set; }
        public int InitialSequence { get; set; }
        public int FinalSequence { get; set; }
        public int Sequence { get; set; }
        public int AvailableSequence { get; set; }
        public int BussinesId { get; set; }
        public int UserId {get; set; }
    }
}
