using System;
using test_task.Models;

namespace test_task.Classes
{
    public class Client : IConvertible, IInsertNotNull
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool PriorClient { get; set; }
        public string Comment { get; set; }

        public Client()
        {
            this.ID = -2;
            this.Name = "-";
            this.PriorClient = false;
            this.Comment = "dummy";
        }

        public Client(int id, string name, bool prior, string comment)
        {
            this.ID = id;
            this.Name = name;
            this.PriorClient = prior;
            this.Comment = comment;
        }

        public Client(Client client)
        {
            this.ID = client.ID;
            this.Name = client.Name;
            this.PriorClient = client.PriorClient;
            this.Comment = client.Comment;
        }

        public bool Edited() => !(Name == "-" || Name == "" || string.IsNullOrEmpty(Name) 
            & (Comment == "dummy" || Comment == "" || string.IsNullOrEmpty(Comment)) 
            & PriorClient == false);

        #region IConvertible
        string IConvertible.ToString(IFormatProvider provider)
        {
            string outString = this.ID + "|"
                             + this.Name + "|"
                             + this.PriorClient + "|"
                             + this.Comment;

            return outString;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType.Name == "Client")
            {
                return this;  
            }
            else
            {
                return Convert.ChangeType(ToString(), conversionType);
            }
            //return Convert.ChangeType(ToString(), conversionType);
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
