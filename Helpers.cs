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

        public static ConsoleControl.ConsoleControl ConsoleControl {
            get {
                return (ConsoleControl.ConsoleControl)(Application.OpenForms[0].Controls.Find("consoleControl1", false)[0]);
            }
        }

        private static dynamic _pronouns = new PronounList();
        private static dynamic _maleValidations = new SerializedList();
        private static dynamic _girlValidations = new SerializedList();
        private static dynamic _theyValidations = new SerializedList();
        private static dynamic _petResponses = new SerializedList();
        private static dynamic _birthdays = new BirthdayList();

        public static char Prefix {  get {
                return char.Parse(ConfigurationManager.AppSettings["commandPrefix"]);
            } }

        public static List<string> MaleValidations {
            get {
                return (GetSerializedObject<SerializedList>("Editable\\malevalidations.json", ref _maleValidations)).responses;
            }

        }
        public static List<string> GirlValidations {
            get {
                return (GetSerializedObject<SerializedList>("Editable\\femalevalidations.json", ref _girlValidations)).responses;
            }
        }
        public static List<string> TheyValidations {
            get {
                return (GetSerializedObject<SerializedList>("Editable\\theyvalidations.json", ref _theyValidations)).responses;
            }
        }

        public static List<string> PetResponses {
            get {
                return (GetSerializedObject<SerializedList>("Editable\\petresponses.json", ref _petResponses)).responses;
            }
        }

        public static PronounList Pronouns {
            get {
                return GetSerializedObject<PronounList>("Editable\\pronouns.json",ref _pronouns);
            }
            set {
                SetSerializedObject("Editable\\pronouns.json", ref _pronouns);
            }
        }

        public static BirthdayList Birthdays {
            get {
                return GetSerializedObject<BirthdayList>("Editable\\birthdays.json", ref _birthdays);
            }
        }

        public static int GetAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;
            return age;
        }

        public static T GetSerializedObject<T>(string filename, ref dynamic obj)
        {
            
            if (obj != null && obj.LastModified >= File.GetLastWriteTime(filename))
                return (T) Convert.ChangeType(obj, typeof(T));
            else
            {
                using (StreamReader file = File.OpenText(filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    obj = (T)serializer.Deserialize(file, typeof(T));
                    obj.LastModified = DateTime.Now;
                    return obj;
                }
            }
        }

        public static void SetSerializedObject(string filename, ref dynamic obj)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
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
