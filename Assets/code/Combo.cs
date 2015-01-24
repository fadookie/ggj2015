public struct Combo {
	public enum Color {
		Color0,
		Color1,
		Color2
	}
	public enum Shape {
		Shape0,
		Shape1,
		Shape2
	}
	public Combo.Color color;
	public Combo.Shape shape;

	public Combo(Combo.Color color, Combo.Shape shape) {
		this.color = color;
		this.shape = shape;
	}

	public override string ToString () {
		return string.Format ("[Combo: color={0}, shape={1}]", color, shape);
	}
}