using System.Text;
using System.Globalization;

public class Matrix<T> : IComparable<Matrix<T>>, IFormattable where T : struct, IComparable<T>
{
    private readonly T[,] _elements;
    private readonly int _rows;
    private readonly int _cols;

    public Matrix(int rows, int cols)
    {
        this._rows = rows;
        this._cols = cols;
        _elements = new T[rows, cols];
    }

    public int Rows => _rows;
    public int Cols => _cols;

    private void SetElement(int row, int col, T value)
    {
        _elements[row, col] = value;
    }

    public T GetElement(int row, int col)
    {
        return _elements[row, col];
    }

    public Matrix<T> AddMatrix(Matrix<T> m)
    {
        if (m.Cols != Cols || m.Rows != Rows)
        {
            Matrix<T> defaultResult = new Matrix<T>(1, 1);
            defaultResult.SetElement(0, 0, default(T));
            return defaultResult;
        }

        Matrix<T> result = new Matrix<T>(Rows, Cols);

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                T sum = (dynamic)GetElement(i, j) + (dynamic)m.GetElement(i, j);
                result.SetElement(i, j, sum);
            }
        }

        return result;
    }

    public Matrix<T> MultiplyMatrix(Matrix<T> m)
    {
        if (Cols != m.Rows)
            throw new ArgumentException("err");

        Matrix<T> result = new Matrix<T>(Rows, m.Cols);

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < m.Cols; j++)
            {
                dynamic sum = default(T);

                for (int k = 0; k < Cols; k++)
                {
                    sum += (dynamic)GetElement(i, k) * (dynamic)m.GetElement(k, j);
                }

                result.SetElement(i, j, (T)sum);
            }
        }

        return result;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                sb.AppendFormat(formatProvider, $"{{0:{format}}} ", GetElement(i, j));
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        return ToString("G", CultureInfo.CurrentCulture);
    }

    public int CompareTo(Matrix<T>? other)
    {
        if (other == null) 
            return 1;

        long thisSize = (long)_rows * _cols;
        long otherSize = (long)other._rows * other._cols;

        return thisSize.CompareTo(otherSize);
    }

    public void Print()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Console.Write(GetElement(i, j) + " ");
            }
            Console.WriteLine();
        }
    }
}

public class SquareMatrix<T> : Matrix<T> where T : struct, IComparable<T>
{
    public SquareMatrix(int size) : base(size, size) { }

    public bool IsDiagonal()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                if (i != j && !GetElement(i, j).Equals(default(T)))
                {
                    return false;
                }
            }
        }

        return true;
    }
}