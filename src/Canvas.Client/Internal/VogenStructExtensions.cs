namespace Canvas.Client.Internal;

internal static class VogenStructExtensions
{
    extension<TW, TP>(IVogen<TW, TP>) where TW : struct, IVogen<TW, TP> where TP : struct
    {
        public static TW? FromNullable(TP? value) => value is null ? null : TW.From(value.Value);
    }

    extension<TW, TP>(IVogen<TW, TP>) where TW : struct, IVogen<TW, TP> where TP : class
    {
        public static TW? FromNullable(TP? value) => value is null ? null : TW.From(value);
    }
}
