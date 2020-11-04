using System;
using test_task.Models;

namespace test_task.Classes
{
    public class Manager : IConvertible, IInsertNotNull
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public Manager()
        {
            this.ID = -2;
            this.Name = "-";
            this.Comment = "dummy";
        }

        public Manager(int id, string name = null, string comment = null)
        {
            this.ID = id;
            this.Name = name;
            this.Comment = comment;
        }

        public Manager(Manager manager)
        {
            this.ID = manager.ID;
            this.Name = manager.Name;
            this.Comment = manager.Comment;
        }

        public bool Edited()
        {
            return !(Name == "-" || Name == "" || string.IsNullOrEmpty(Name) 
                & (Comment == "dummy" || Comment == "" || string.IsNullOrEmpty(Comment)));
        }

        #region IConvertible
        string IConvertible.ToString(IFormatProvider provider)
        {
            string outString = this.ID + "|" + this.Name + "|" + this.Comment;
            return outString;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType.Name == "Manager")
            {
                return this;
            }
            else
            {
                return Convert.ChangeType(ToString(), conversionType);
            }
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
