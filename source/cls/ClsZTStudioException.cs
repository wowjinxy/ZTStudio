using System;
using Microsoft.VisualBasic;

namespace ZTStudio
{
    public class ZTStudioException : ApplicationException
    {
        private string StrException_Class = "";
        private string StrException_Method = "";
        private ErrObject ObjException_ErrObject = null;

        public string ClassName
        {
            get
            {
                return StrException_Class;
            }

            set
            {
                StrException_Class = value;
            }
        }

        public string MethodName
        {
            get
            {
                return StrException_Method;
            }

            set
            {
                StrException_Method = value;
            }
        }

        public ErrObject ErrObject
        {
            get
            {
                return ObjException_ErrObject;
            }

            set
            {
                ObjException_ErrObject = value;
            }
        }

        public ZTStudioException(string StrClass, string StrMethod, ErrObject ObjError) : base(StrClass + "::" + StrMethod + "() - " + ObjError.Number + " - " + ObjError.Description + " at line " + ObjError.Erl)
        {
            ClassName = StrClass;
            MethodName = StrMethod;
            ErrObject = ObjError;
        }
    }
}