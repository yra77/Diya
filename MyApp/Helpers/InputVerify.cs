

using System;


namespace MyApp.Helpers
{

    public interface IInputVerify
    {
        bool EntryVerify_Eng(ref string str);
        bool EntryVerify_Ukr(ref string str);
        bool InputVerify_Data(ref string str);
    }

    class InputVerify: IInputVerify
    {
        public bool EntryVerify_Eng(ref string str)
        {

            bool flag = true;
            string temp = str;

            for (int i = 0; i < temp.Length; i++)
            {

                if ((temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z'))
                {
                    continue;
                }
                else
                {
                    if(temp[i] != ' ')
                    {
                        flag = false;
                    }

                    temp = temp.Remove(i, 1);
                }
            }

            str = temp;
            return flag;
        }

        public bool EntryVerify_Ukr(ref string str)
        {

            bool flag = true;
            string temp = str;
            int codeASCII = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                codeASCII = Char.ConvertToUtf32(temp[i].ToString(), 0);
               // Console.WriteLine(codeASCII);
                if (((codeASCII > 1040 && codeASCII < 1066) || (codeASCII > 1069 && codeASCII < 1098)) || (codeASCII == 1025 || codeASCII == 1028 ||
                    codeASCII == 1068 || codeASCII == 1030 || codeASCII == 1110 || codeASCII == 1100 || codeASCII == 1108 || codeASCII == 1102 ||
                    codeASCII == 1103 || codeASCII == 1111 || codeASCII == 1105 || codeASCII == 1031 || codeASCII == 39))
                {
                    
                    // Console.WriteLine(Char.ConvertToUtf32(str[i].ToString(), 0));
                }
                else
                {
                    if(temp[i] != ' ')
                    {
                        flag = false;
                    }

                    temp = temp.Remove(i, 1);
                }

            }
            str = temp;
            return flag;
        }

        public bool InputVerify_Data(ref string str)
        {
            bool flag = true;
            string temp = str;
            int codeASCII = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                if(i == 0 || i == 1 || i == 3 || i == 4 || i == 6 || i == 7 || i == 9 || i == 8)
                {
                   codeASCII = Char.ConvertToUtf32(temp[i].ToString(), 0);
                    if ((codeASCII > 47 && codeASCII < 58))
                    {
                        continue;
                    }
                    else
                    {
                        temp = temp.Remove(i, 1);
                        flag = false;
                    }
                }
                else if(i == 2 || i == 5)
                {
                    if(temp[i] != '.')
                    {
                        temp = temp.Remove(i, 1);
                        flag = false;
                    }
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                }
            }

                str = temp;
            return flag;
        }

    }
}
