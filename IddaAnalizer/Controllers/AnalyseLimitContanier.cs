using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IddaAnalyser
{
    public class AnalyseLimitContanier
    {
        public int Id;
        public string LimitType;
        public Dictionary<string, int> textValues;
        public Dictionary<TextBox, string> textMap;
        public AnalyseLimitContanier(int id, string limitType, int maxValue, int minValue, TextBox maxTextBox, TextBox minTextBox)
        {
            textValues = new Dictionary<string, int>()
                {
                    {"Max",maxValue },
                    {"Min",minValue },
                };
            textMap = new Dictionary<TextBox, string>()
                {
                    {maxTextBox,"Max" },
                    {minTextBox,"Min" },
                };
            this.Id = id;
            this.LimitType = limitType;
        }
        public void SetValue(TextBox textBox)
        {
            textValues[textMap[textBox]] = int.Parse(textBox.Text);
        }
        public void ResetValues()
        {
            textValues["Max"] = int.MaxValue;
            textValues["Min"] = 0;
        }
    }
}
