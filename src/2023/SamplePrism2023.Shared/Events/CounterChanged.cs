using Prism.Events;

namespace SamplePrism2023.Shared.Events;

public class CounterChanged : PubSubEvent<int> {}