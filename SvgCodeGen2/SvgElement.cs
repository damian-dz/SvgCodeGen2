namespace SvgCodeGen2;

public abstract class SvgElement : SvgElementBase, ISvgElement
{
    public bool HasChilden => false;

    public bool AllowsNesting()
    {
        return false;
    }

    public bool CanGenerateValidSvg()
    {
        throw new NotImplementedException();
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

}