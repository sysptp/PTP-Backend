namespace BussinessLayer.DTOs.Ncfs
{
    public class NcfDto
    {
        public string? NcfType { get; set; }
        public int InitialSequence { get; set; }
        public int FinalSequence { get; set; }
        public int Sequence { get; set; }
        public int AvailableSequence { get; set; }
    }
}
