using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentDiary.Models.Wrappers
{
    public class StudentWrapper : IDataErrorInfo
    {

        public StudentWrapper()
        {
            Group = new GroupWrapper();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Comments { get; set; }

        public string Math { get; set; }

        public string Technology { get; set; }

        public string Physics { get; set; }

        public string PolishLang { get; set; }

        public string ForeignLang { get; set; }

        public bool Activities { get; set; }

        public GroupWrapper Group { get; set; }



        private bool _IsFirstNameValid;
        private bool _IsLastNameValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole imię jest wymaganę";
                            _IsFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _IsFirstNameValid = true;
                        }
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole nazwisko jest wymaganę";
                            _IsLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _IsLastNameValid = true;
                        }
                        break;
                    default:
                        break;
                }

                return Error;
            }
        }

        public string Error { get; set; }

        public bool IsValid
        { 
            get
            {
                return _IsFirstNameValid && _IsLastNameValid && Group.IsValid;
            }
           
        }


    }
}
