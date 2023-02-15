namespace SvgCodeGen2;

public partial class SvgPath
{
    public abstract class Command
    {
        public bool IsRelative { get; set; }

        public static List<string> Parse(string text)
        {
            string s = text.Trim();
            var tokens = new List<string>();
            int startIdx = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '-')
                    i++;
                while (i < s.Length && s[i] != '-' && s[i] != ' ' && s[i] != ',')
                    i++;
                int stopIdx = i;
                if (stopIdx > startIdx)
                    tokens.Add(s[startIdx..stopIdx]);
                startIdx = (i < s.Length && s[i] == '-') ? i : i + 1;
            }
            return tokens;
        }

        public abstract void Scale(double factor);
        public abstract void Translate(double deltaX, double deltaY);
    }
}
