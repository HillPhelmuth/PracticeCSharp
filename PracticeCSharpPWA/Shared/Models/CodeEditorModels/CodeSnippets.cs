﻿namespace PracticeCSharpPWA.Shared.Models.CodeEditorModels
{
    public static class CodeSnippets
    {
        public const string ARRAYLIST =
            "ArrayList a1 = new ArrayList();\na1.Add(1);\na1.Add(\"Example\");\na1.Add(true);\nreturn al;";
        
        public const string STACK =
            "Stack st = new Stack();\nst.Push(1);\nst.Push(2);\nst.Push(3);\nreturn st;";

        public const string QUEUE = "Queue qt = new Queue();\nqt.Enqueue(1);\nqt.Enqueue(2);\nqt.Enqueue(3);\nreturn qt;";

        public const string HASHTABLE =
            "Hashtable ht = new Hashtable();\nht.Add(\"001\",\".Net\");\nht.Add(\"002\",\"C#\");\nht.Add(\"003\",\"ASP.Net\");\nreturn ht;";

        public const string LIST = "List<string> list = new List<string>();\nlist.Add(\"item 1\");\nlist.Add(\"item 2\");\nlist.Add(\"item 3\");\nreturn list;";
        public const string DICTIONARY = "Dictionary<int, string> dict = new Dictionary<int, string>();\ndict.Add(1, \"item 1\");\ndict.Add(2, \"item 2\");\ndict.Add(3, \"item 3\");\nreturn dict;";
        public const string CONCATENATION =
            "string nowDateTime = \"Date: \" + DateTime.Now.ToString(\"D\");\n" +
            "string firstName = \"Gob\";\nstring lastName = \"Bluth\";\nstring age = \"33\";\n" + 
            "string authorDetails = firstName + \" \" + lastName + \" is \" + age + \" years old.\";\nreturn authorDetails;";
        public const string FORMAT = "string name = \"George Bluth\";\nint age = 33;\n" +
                                     "string authorInfo = string.Format(\"{0} is {1} years old.\", name, age.ToString());\nreturn authorInfo;";
        public const string INTERPOLATION = "string name = \"George Bluth\";\nint age = 33;\n" +
                                            "string authorInfo = string.Format($\"{name} is {age} years old.\");\nreturn authorInfo";
        public const string SUBSTRING = "string authorInfo = \"Buster Bluth is 33 years old.\";\n" +
                                        "int startPosition = authorInfo.IndexOf(\"is \") + 1;\n" +
                                        "string age = authorInfo.Substring(startPosition +2, 2 );\nreturn age;";

        public const string ARRAYTOSTRING =
            "char[] chars = { 'C', 'S', 'h', 'a', 'r', 'p' };\nstring name = new string(chars);\nreturn name;";

        public const string STRINGTOARRAY = "string sentence = \"Mahesh Chand is an author and founder of C# Corner\";\n" +
                                            "char[] charArr = sentence.ToCharArray();\nreturn charArr;";

        public const string IFCONDITIONAL = "char[] chars = { 'C', 'S', 'h', 'a', 'r', 'p' };\nstring name = new string(chars);\nif (name.Length > 5)\n{\n\tname = $\"{name} is sometimes easy\";\n}\nreturn name;";
        public const string IFELSE = "char[] chars = { 'C', 'S', 'h', 'a', 'r', 'p' };\nstring name = new string(chars);\nif (name.Length > 6)\n{\n\tname = $\"{name} is sometimes easy\";\n}\nelse\n{\n\tname = $\"{name} is sometimes hard\";\n}\nreturn name;\n";
        public const string ELSEIF = "char[] chars = { 'C', 'S', 'h', 'a', 'r', 'p' };\nstring name = new string(chars);\nint nameLength = name.Length;\nif (nameLength > 7)\n{\n\tname = $\"{name} is sometimes easy\";\n}\nelse if (nameLength > 6)\n{\n\tname = $\"{name} is sometimes hard\";\n}\nelse{\n\tname = $\"{name} is always c#\";\n}\nreturn name;";
        public const string SWITCH = "char[] chars = { 'C', 'S', 'h', 'a', 'r', 'p' };\nstring name = new string(chars);\nint nameLength = name.Length;\nswitch (nameLength)\n{\n\tcase 7:\n\t\tname = $\"{name} is sometimes easy\";\n\t\tbreak;\n\tcase 8:\n\t\tname = $\"{name} is sometimes hard\";\n\t\tbreak;\n\tdefault:\n\t\tname = $\"{name} is always c#\";\n\t\tbreak;\n}\nreturn name;";
        public const string FORLOOP = "char[] chars = { 'C', 'S', 'h', 'a', 'r', 'p' };\nstring name = new string(chars);\nfor (int i = 0; i < chars.Length; i++)\n{\n\tname = name + \"!\";\n}\nreturn name;";
        public const string FOREACHLOOP = "char[] chars = { 'C', 'S', 'h', 'a', 'r', 'p' };\nstring name = \"\";\nforeach (var str in chars)\n{\n\tname += str;\n}\nreturn name;\n";
        public const string PUZZLEOFDAY = "public static bool validBraces(String braces)\n{\n\treturn false;\n}";
    }
}
