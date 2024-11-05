namespace BussinessLayer.DTOs.Ncfs
{
    public class CreateNcfDto
    {
        public string? NcfType { get; set; }
        public int InitialSequence { get; set; }
        public int FinalSequence { get; set; }
        public int BussinesId { get; set; }
        public int UserId { get; set; }
    }
}
