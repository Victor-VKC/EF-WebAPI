using Server.Entity;

namespace Server.Services.Utils
{
    public static class CustomerAddressExt
    {
        public static bool IsDefault(this CustomerAddress address)
        {
            return (address.AddressType[0] == '*');
        }

        public static void SetDefault(this CustomerAddress address, bool setDefault)
        {
            if (setDefault)
            {
                if (address.IsDefault()) return;
                address.AddressType = '*' + address.AddressType;
            }
            else
            {
                if (!address.IsDefault()) return;
                address.AddressType = address.AddressType.Remove(0, 1);
            }
        }
    }
}
