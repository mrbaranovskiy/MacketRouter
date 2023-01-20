namespace MacketRouter.DataStructures;

public readonly struct ElParameter<T> where T : struct
{
    public T Value { get; }

    public ElParameter(T value)
    {
        Value = value;
    }

    public static implicit operator ElParameter<T>(T data) => new ElParameter<T>(data);
    public static implicit operator T(ElParameter<T> data) => data.Value;
}