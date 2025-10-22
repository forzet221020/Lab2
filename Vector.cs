using System;

public class Vector : IComparable<Vector>
{
    public string Color { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public Vector(string color, double x, double y)
    {
        this.Color = color;
        this.X = x;
        this.Y = y;
    }

    public double GetLength()
    {
        return Math.Sqrt(this.X * this.X + this.Y * this.Y);
    }

    public void Increase(double deltaX, double deltaY)
    {
        this.X += deltaX;
        this.Y += deltaY;
    }

    public override string ToString()
    {
        return string.Format(
            "Вектор (Колір: {0}, Координати: [{1:F2}; {2:F2}], Довжина: {3:F2})",
            this.Color,
            this.X,
            this.Y,
            this.GetLength()
        );
    }
    
    public int CompareTo(Vector? other)
    {
        if (other is null) return 1;

        double thisLength = this.GetLength();
        double otherLength = other.GetLength();

        return thisLength.CompareTo(otherLength);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Vector other) return false;
        return this.Color == other.Color && this.X == other.X && this.Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Color, X, Y);
    }
}