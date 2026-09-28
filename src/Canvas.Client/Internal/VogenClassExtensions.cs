namespace Canvas.Client.Internal;

internal static class VogenClassExtensions
{
    extension<TW, TP>(IVogen<TW, TP>) where TW : class, IVogen<TW, TP> where TP : struct
    {
        public static TW? FromNullable(TP? value) => value is null ? null : TW.From(value.Value);
    }

    extension<TW, TP>(IVogen<TW, TP>) where TW : class, IVogen<TW, TP> where TP : class
    {
        public static TW? FromNullable(TP? value) => value is null ? null : TW.From(value);
    }
}