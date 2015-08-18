using SageCS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{



    [Serializable()]
    class INIObject
    {
        public static Dictionary<string, Object> objects = new Dictionary<string, Object>();


        //Deserialization constructor.
        public INIObject(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties

            //selectPortrait = (string)info.GetValue("SelectPortrait", typeof(string));
            //buttonImage = (string)info.GetValue("ButtonImage", typeof(string));
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"

            //info.AddValue("SelectPortrait", selectPortrait);
           // info.AddValue("ButtonImage", buttonImage);
        }

    }
}
