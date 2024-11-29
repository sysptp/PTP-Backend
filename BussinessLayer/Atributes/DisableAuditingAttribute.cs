
namespace BussinessLayer.Atributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class DisableAuditingAttribute : Attribute
    {
        public DisableAuditingAttribute()
        {
        }
    }

}
