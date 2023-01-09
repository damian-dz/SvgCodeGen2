using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public abstract class Command
    {
        public bool IsRelative { get; set; }

        public static List<string> Parse(string text)
        {
            string trimmed = text.Trim();
            var tokens = new List<string>();
            int startIdx = 0;
            for (int i = 0; i < trimmed.Length; i++)
            {
                if (trimmed[i] == '-')
                    i++;
                while (i < trimmed.Length && trimmed[i] != '-' && trimmed[i] != ' ' && trimmed[i] != ',')
                    i++;
                int stopIdx = i;
                if (stopIdx > startIdx)
                    tokens.Add(trimmed[startIdx..stopIdx]);
                startIdx = (i < trimmed.Length && trimmed[i] == '-') ? i : i + 1;
            }
            return tokens;
        }

        public abstract void Scale(double factor);
        public abstract void Translate(double deltaX, double deltaY);
    }
}
