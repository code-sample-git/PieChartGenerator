using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class PieChartControl : Form
{
    private List<float> _values;
    private List<string> _labels;
    private List<Color> _colors;

    public PieChartControl()
    {
        this.Text = "Pie Chart";
        this.Size = new Size(600, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Paint += new PaintEventHandler(this.PieChartControl_Paint);
        _colors = new List<Color>()
        {
            Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple,
            Color.Yellow, Color.Pink, Color.Brown, Color.Cyan, Color.Magenta
        };
    }

    public void SetData(List<float> values, List<string> labels)
    {
        if (values == null || labels == null || values.Count != labels.Count)
            throw new ArgumentException("Values and labels must be non-null and of the same length.");

        _values = values;
        _labels = labels;
        this.Invalidate(); // Forces the control to be redrawn
    }

    private void PieChartControl_Paint(object sender, PaintEventArgs e)
    {
        if (_values == null || _values.Count == 0)
            return;

        Graphics g = e.Graphics;
        Rectangle rect = new Rectangle(50, 50, 400, 400);
        float total = _values.Sum();
        float startAngle = 0;

        for (int i = 0; i < _values.Count; i++)
        {
            float sweepAngle = (_values[i] / total) * 360;
            Brush brush = new SolidBrush(_colors[i % _colors.Count]);
            g.FillPie(brush, rect, startAngle, sweepAngle);
            startAngle += sweepAngle;
        }

        DrawLegend(g);
    }

    private void DrawLegend(Graphics g)
    {
        if (_labels == null || _labels.Count == 0)
            return;

        int legendX = 470;
        int legendY = 60;
        int boxSize = 20;
        int spacing = 30;

        for (int i = 0; i < _labels.Count; i++)
        {
            Brush brush = new SolidBrush(_colors[i % _colors.Count]);
            g.FillRectangle(brush, legendX, legendY + (i * spacing), boxSize, boxSize);
            g.DrawString(_labels[i], this.Font, Brushes.Black, legendX + boxSize + 5, legendY + (i * spacing));
        }
    }
}
