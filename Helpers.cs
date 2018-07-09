using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace LeftyBotGui
{
    public static class Helpers
    {

        private static readonly ConsoleControl.ConsoleControl _con = (ConsoleControl.ConsoleControl)(Application.OpenForms[0].Controls.Find("consoleControl1", false)[0]);

        public static char Prefix {  get {
                return char.Parse(ConfigurationManager.AppSettings["commandPrefix"]);
            } }

        public static List<string> MaleValidations {
            get {
                SerializedList serializedList = GetSerializedList<SerializedList>("Editable\\malevalidations.json", typeof(SerializedList));
                return serializedList.responses;
            }
        }
        public static List<string> GirlValidations {
            get {
                SerializedList serializedList = GetSerializedList<SerializedList>("Editable\\femalevalidations.json", typeof(SerializedList));
                return serializedList.responses;
            }
        }
        public static List<string> TheyValidations {
            get {
                SerializedList serializedList = GetSerializedList<SerializedList>("Editable\\theyvalidations.json", typeof(SerializedList));
                return serializedList.responses;
            }
        }

        public static List<string> PetResponses {
            get {
                SerializedList serializedList = GetSerializedList<SerializedList>("Editable\\petresponses.json", typeof(SerializedList));
                return serializedList.responses;
            }
        }

        public static PronounList Pronouns {
            get {
                PronounList pronouns = GetSerializedList<PronounList>("Editable\\pronouns.json", typeof(PronounList));
                return pronouns;
            }
        }

        public static ConsoleControl.ConsoleControl ConsoleControl { get {
                return _con;
            } }


        public static int GetAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;
            return age;
        }

        public static T GetSerializedList<T>(string filename, Type type)
        {
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                T list = (T)serializer.Deserialize(file, typeof(T));
                return (T) Convert.ChangeType(list, type);
            }
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
    }
}
