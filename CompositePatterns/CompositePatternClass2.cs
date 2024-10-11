using System.Collections;
using System.Collections.ObjectModel;

namespace Patterns.CompositePatterns;

public static class NeuronExtensionMethods
{
    public static IEnumerable<Neuron> ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
    {
        if (ReferenceEquals(self, other)) return self;

        foreach (var from in self)
        foreach (var to in other)
        {
            from.Out.Add(to);
            to.In.Add(from);
        }

        return self;
    }
}

public class Neuron : IEnumerable<Neuron>
{
    public readonly List<Neuron> In, Out;

    public float Value { get; init; }

    public Neuron(float value)
    {
        In = [];
        Out = [];
        Value = value;
    }
    
    public IEnumerator<Neuron> GetEnumerator()
    {
        yield return this;
    }

    public override string ToString()
    {
        return $"Neuron value: {Value}\n incoming neurons: {string.Join(", ", In.Select(x => x.Value))}\n" +
               $" outcoming neurons: {string.Join(", ", Out.Select(x => x.Value))}";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class NeuronLayer : Collection<Neuron>
{
    
}

public class NeuronRing : List<Neuron>
{
    
}

public static class CompositePatternClass2
{
    public static void LocalMain()
    {
        var neuron1 = new Neuron(value: 2);
        var neuron2 = new Neuron(value: 4);
        var neuron3 = new Neuron(value: 6);
        neuron1.ConnectTo(neuron2).ConnectTo(neuron3);

        var layer1 = new NeuronLayer();
        var layer2 = new NeuronLayer();

        neuron1.ConnectTo(layer1);

        Console.WriteLine(neuron1);
    }
}