using System;
using System.Collections.Generic;
using test_task.Models;

namespace test_task.Classes
{
    public class Contact : IConvertible
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? Tel { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int ClientID { get; set; }

        public Contact()
        {
            this.ID = -2;
            this.FirstName = "-";
            this.LastName = "-";
            this.Tel = null;
            this.Email = "@";
            this.Comment = "-";
            this.ClientID = -2;
        }

        public Contact(int id, string firstName, string lastName, long? tel, string email, string comment, int clientID)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Tel = tel;
            this.Email = email;
            this.Comment = comment;
            this.ClientID = clientID;
        }

        public Contact(string firstName, string lastName, long? tel, string email, string comment, int clientID)
        {
            this.ID = -2;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Tel = tel;
            this.Email = email;
            this.Comment = comment;
            this.ClientID = clientID;
        }

        #region IConvertible
        string IConvertible.ToString(IFormatProvider provider)
        {
            string outString = this.ID + "|"
                             + this.FirstName + "|"
                             + this.LastName + "|"
                             + this.Tel + "|"
                             + this.Email + "|"
                             + this.Comment + "|"
                             + this.ClientID;

            return outString;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(ToString(), conversionType);
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