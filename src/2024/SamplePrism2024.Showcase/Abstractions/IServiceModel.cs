namespace SamplePrism2024.Showcase.Abstractions;

internal interface IServiceModel
{
    Task Push(IModel model);
    void Pop(IModel model);
    void Random(IModel model);
    void Clear(IModel model);
    bool Check(IModel model);
}