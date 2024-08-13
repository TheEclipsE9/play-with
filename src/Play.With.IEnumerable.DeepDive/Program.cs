using System.Collections;

namespace Play.With.IEnumerable.DeepDive
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> source = Enumerable.Range(0, 1_000).ToArray();

            Console.WriteLine(Enumerable.Select(source, x => x * 2).Sum());
            Console.WriteLine(CustomSelect(source, x => x * 2).Sum());
        }

        static IEnumerable<TResult> CustomSelect<TSource, TResult>(
            IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return new CustomSelectEnumerable<TSource, TResult>(source, selector);
        }

        private class CustomSelectEnumerable<TSource, TResult> : IEnumerable<TResult>
        {
            private IEnumerable<TSource> _source;
            private Func<TSource, TResult> _selector;

            public CustomSelectEnumerable(IEnumerable<TSource> source, Func<TSource, TResult> selector)
            {
                _source = source;
                _selector = selector;
            }

            public IEnumerator<TResult> GetEnumerator()
            {
                return new CustomSelectEnumerator(_source, _selector);
            }

            IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.GetEnumerator();

            private class CustomSelectEnumerator : IEnumerator<TResult>
            {
                private IEnumerable<TSource> _source;
                private Func<TSource, TResult> _selector;

                private TResult _current = default!;
                IEnumerator<TSource> _sourceEnumerator;
                private int _state = 1;

                public CustomSelectEnumerator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
                {
                    _source = source;
                    _selector = selector;
                }

                public TResult Current => _current;

                object IEnumerator.Current => this.Current!;

                public bool MoveNext()
                {
                    switch (_state)
                    {
                        case 1:
                            _sourceEnumerator = _source.GetEnumerator();
                            _state = 2;
                            goto case 2;
                        case 2:
                            try
                            {
                                if (_sourceEnumerator.MoveNext())
                                {
                                    _current = _selector(_sourceEnumerator.Current);
                                    return true;
                                }
                            }
                            catch
                            {
                                Dispose();
                                throw;
                            }
                            break;
                    }

                    Dispose();
                    return false;
                }

                public void Dispose()
                {
                    _state = -1;
                    _sourceEnumerator?.Dispose();
                }

                public void Reset()
                {
                    throw new NotSupportedException();
                }
            }
        }
    }
}
