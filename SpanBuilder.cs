namespace BetterReadLine;

using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

internal ref struct SpanBuilder
{
    private char[] _arrayToReturnToPool;
    private Span<char> _chars;
    private int _pos;

    public SpanBuilder() : this(Span<char>.Empty) { }

    public SpanBuilder(Span<char> initialBuffer)
    {
        _arrayToReturnToPool = Array.Empty<char>();
        _chars = initialBuffer;
        _pos = 0;
    }

    public int Length
    {
        readonly get => _pos;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Length)} can not be smaller than 0");
            if (value > _chars.Length) throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(Length)} can not be greater than {nameof(Capacity)}");
            _pos = value;
        }
    }

    public readonly int Capacity => _chars.Length;

    public ref char this[in int index] => ref _chars[index];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in char c)
    {
        int pos = _pos;
        if ((uint)pos < (uint)_chars.Length)
        {
            _chars[pos] = c;
            _pos = pos + 1;
        }
        else GrowAndAppend(c);
    }

    public void Append(in ReadOnlySpan<char> value)
    {
        int pos = _pos;
        if (pos > _chars.Length - value.Length) Grow(value.Length);

        value.CopyTo(_chars[_pos..]);
        _pos += value.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void GrowAndAppend(in char c)
    {
        Grow(1);
        Append(c);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow(in int additionalCapacityBeyondPos)
    {
        char[] poolArray = ArrayPool<char>.Shared.Rent(Math.Max(_pos + additionalCapacityBeyondPos, _chars.Length * 2));

        _chars[.._pos].CopyTo(poolArray);

        char[] toReturn = _arrayToReturnToPool;
        _chars = _arrayToReturnToPool = poolArray;
        ArrayPool<char>.Shared.Return(toReturn);
    }

    public readonly Span<char> AsSpan() => _chars[.._pos];
    public readonly Span<char> AsSpan(in int start) => _chars[start.._pos];
    public readonly Span<char> AsSpan(in int start, in int length) => _chars.Slice(start, length);

    public readonly ReadOnlySpan<char> AsReadOnlySpan() => _chars[.._pos];
    public readonly ReadOnlySpan<char> AsReadOnlySpan(in int start) => _chars[start.._pos];
    public readonly ReadOnlySpan<char> AsReadOnlySpan(in int start, in int length) => _chars.Slice(start, length);

    public override readonly string ToString() => new(_chars[.._pos]);
}