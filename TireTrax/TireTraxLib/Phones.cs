using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TireTraxLib
{
    public class Phones
    {
        private int _PhoneId;

        public int PhoneId
        {
            get { return _PhoneId; }
            set { _PhoneId = value; }
        }
        private int _PhoneTypeId;

        public int PhoneTypeId
        {
            get { return _PhoneTypeId; }
            set { _PhoneTypeId = value; }
        }
        private string _Number;

        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private string _Extension;

        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }
        private Boolean _IsActive;

        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private Boolean _IsAcceptTextMessages;

        public Boolean IsAcceptTextMessages
        {
            get { return _IsAcceptTextMessages; }
            set { _IsAcceptTextMessages = value; }
        }

        public enum PhoneType
        {
            Business = 1,
            Cell = 2
        }

    }
}
