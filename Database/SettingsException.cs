using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class DatabaseException : Exception
    {
        public enum ExceptionTypes
        {
            DUPLICATE_ENTRY
        }

        ExceptionTypes _exTypes;

        public DatabaseException(ExceptionTypes exception_type)
        {
            this._exTypes = exception_type;
        }

        public ExceptionTypes ExceptionType
        {
            get
            {
                return this._exTypes;
            }
            set
            {
                this._exTypes = value;
            }
        }
    }
}
