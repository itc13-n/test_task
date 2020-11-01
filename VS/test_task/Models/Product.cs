using System;

namespace test_task.Classes
{
    public class Product : IConvertible
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Subscribtion { get; set; }
        public int? Period { get; set; }
        public int? Owner { get; set; }

        public Product()
        {
            this.ID = -2;
            this.Name = "dummy";
            this.Price = -2;
            this.Subscribtion = false;
            this.Period = -2;
            this.Owner = null;
        }

        public Product(int id, string name, decimal price, bool subsc, int? period)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.Subscribtion = subsc;
            this.Period = period;
            this.Owner = null;
        }

        public Product(int id, string name, decimal price, bool subsc, int period, int owner)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.Subscribtion = subsc;
            this.Period = period;
            this.Owner = owner;
        }

        #region IConvertible
        string IConvertible.ToString(IFormatProvider provider)
        {
            string outString = this.ID + "|"
                             + this.Name + "|"
                             + this.Price + "|"
                             + this.Subscribtion + "|"
                             + this.Period;

            return outString;
        }
        
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        Int16 IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        Int32 IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        Int64 IConvertible.ToInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        SByte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        UInt16 IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        UInt32 IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        UInt64 IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        Single IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        Double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        Decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }
        #endregion
    }


}
