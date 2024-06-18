# Bee.Interval
Simple interval implementation

## How use?

```csharp
using Bee.Interval;

static void Main(string[] args)
{
	// 3 seconds
	Interval.start(3, change);
}

private void change()
{
    MessageBox.Show("Changed");
}
```
