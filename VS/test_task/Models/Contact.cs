using System;
using System.Collections.Generic;
using test_task.Models;

namespace test_task.Classes
{
    public class Contact : IConvertible, IInsertNotNull
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

        #region equality
        public override bool Equals(object obj)
        {
            if ((obj as Contact).ID == this.ID 
                & (obj as Contact).FirstName == this.FirstName 
                & (obj as Contact).LastName == this.LastName
                & (obj as Contact).Tel == this.Tel
                & (obj as Contact).Email == this.Email
                & (obj as Contact).Comment == this.Comment
                & (obj as Contact).ClientID == this.ClientID)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = -18682258;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + Tel.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + ClientID.GetHashCode();
            return hashCode;
        }
        #endregion

        public bool Edited()
        {
            if (this.ID != -2)
            {
                return true;
            }
            return false;
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
