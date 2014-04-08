using Data.Models;

namespace Server.Services.Utils
{
    public static class AddressExt
    {
        public static void SetDefault(this Address address, bool setDefault)
        {
            if (setDefault)
            {
                if(address.IsDefault) return;
                address.AddressType = '*' + address.AddressType;
            }
            else
            {
                if(!address.IsDefault) return;
                address.AddressType = address.AddressType;
            }
        }
    }
}
