
namespace BussinessLayer.Atributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class DisableBitacoraAttribute : Attribute
    {
        public DisableBitacoraAttribute()
        {
        }
    }

}
