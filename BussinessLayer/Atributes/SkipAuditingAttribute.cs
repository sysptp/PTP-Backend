
namespace PTP_API.Attributes
{
    /// <summary>
    /// Excluye un endpoint de la auditoría automática
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SkipAuditingAttribute : Attribute
    {
    }
}
