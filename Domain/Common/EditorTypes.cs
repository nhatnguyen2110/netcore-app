using System.Reflection;

namespace Domain.Common
{
    public class EditorTypes
    {
        public static readonly EditorType redirect = new EditorType("REDIRECT", "Sends the user somewhere else");
        public static readonly EditorType hidden = new EditorType("HIDDEN", "A hidden field");
        public static readonly EditorType parent = new EditorType("PARENT", "What record is the parent");
        public static readonly EditorType password = new EditorType("PASSWORD", "A password entry");
        public static readonly EditorType input = new EditorType("INPUT", "Standard text entry box");
        public static readonly EditorType read_only = new EditorType("READ_ONLY", "Standard text entry box");
        public static readonly EditorType checkbox = new EditorType("CHECKBOX", "Options, multi-select");
        public static readonly EditorType textarea = new EditorType("TEXTAREA", "Wider and taller text entry box");
        public static readonly EditorType richtexteditor = new EditorType("RICHTEXTEDITOR", "Rich text editor");
        public static readonly EditorType time = new EditorType("TIME", "Time entry");
        public static readonly EditorType date = new EditorType("DATE", "Date entry");
        public static readonly EditorType date_autofill = new EditorType("DATE_AUTOFILL", "Automatically generated date");
        public static readonly EditorType datetime = new EditorType("DATETIME", "Date and Time entry");
        public static readonly EditorType dropdown = new EditorType("DROPDOWN", "List of options, single-select");
        public static readonly EditorType dropdown_null = new EditorType("DROPDOWN_NULL", "List of options, NULL allowed");
        public static readonly EditorType editor_type = new EditorType("EDITOR_TYPE", "Type of field");
        public static readonly EditorType datetime_utc = new EditorType("DATETIME_UTC", "Date and Time UTC entry");
        public static readonly EditorType number = new EditorType("NUMBER", "A Number entry");

        public static List<EditorType> GetAllTypes()
        {
            var ret = new List<EditorType>();
            var tmp = new EditorTypes();
            foreach (FieldInfo fld in tmp.GetType().GetFields())
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                var fft = (EditorType)(fld.GetValue(tmp));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                ret.Add(fft);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            return ret;
        }

        public static EditorType GetTypeByName(string typeName)
        {
            var tmp = new EditorTypes();
            foreach (FieldInfo fld in tmp.GetType().GetFields())
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                var fft = (EditorType)(fld.GetValue(tmp));
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (fft.GetKeyName().ToUpper() == typeName.ToUpper())
                {
                    return fft;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            return new EditorType("UNKNOWN", "Unknown type");
        }
    }

    public class EditorType
    {
        private string key;
        private string description;

        public EditorType(string _key, string _description)
        {
            key = _key;
            description = _description;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetKeyName()
        {
            return key;
        }
    }
}
