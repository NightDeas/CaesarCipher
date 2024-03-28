using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    public partial class Form1 : Form
    {
        bool reverse = false;
        enum Language
        {
            rus,
            eng
        }
        static readonly char[] EngChar = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }; // 26 букв
        static readonly char[] RusChar = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };// 33 букв
        public Form1()
        {
            InitializeComponent();
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox_Text.Text;
            int sdvig = (int)numericUpDown1.Value;
            sdvig = reverse ? -sdvig : sdvig;
            string result = "";
            char[] chars = text.ToCharArray(); // массив символов из text

            if (FindNumber(chars) == false) // проверка массива на наличие цифр
            {
                label_Error.Visible = false;
                for (int i = 0; i < chars.Length; i++)
                {
                    bool isLower = char.IsLower(chars[i]);
                    char symbol = char.ToLower(chars[i]);
                    char symbolResult = FindSymbol(symbol, comboBox1.SelectedIndex == 0 ? Language.eng : Language.rus, sdvig);
                    symbolResult = isLower ? symbolResult : char.ToUpper(symbolResult);
                    result += symbolResult;
                }
            }
            else
                label_Error.Visible = true;
            textBox_Result.Text = result;
        }

        static bool FindNumber(char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] >= '0' && chars[i] <= '9')
                    return true;
            }
            return false;
        }

        static char FindSymbol(char a, Language language, int sdvig)
        {
            int? index = null;
            char[] langArr;
            switch (language)
            {
                case Language.rus:
                    langArr = RusChar;
                    break;
                case Language.eng:
                    langArr = EngChar;
                    break;
                default:
                    langArr = RusChar;
                    break;
            }


            for (int i = 0; i < langArr.Length; i++)
            {
                if (langArr[i] == a)
                {
                    index = i;
                    break;
                }
            }
            if (index != null)
            {
                int indexFind = (int)index + sdvig;
                while (indexFind < 0)
                {
                    indexFind = langArr.Length + sdvig + (int)index;

                }
                while (indexFind > langArr.Length - 1)
                {
                    indexFind -= langArr.Length;
                }
                return (char)langArr.GetValue(indexFind);
            }
            return a;
        }
    
        private void CbOperationSelect(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                button_Start.Text = "Зашифровать";
                reverse = false;
            }
            else
            {
                button_Start.Text = "Расшифровать";
                reverse = true;
            }
        }

        private void CbLanguageSelect(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
                numericUpDown1.Maximum = EngChar.Length;
            else
                numericUpDown1.Maximum = RusChar.Length;
            numericUpDown1.Enabled = true;
        }
    }
}
