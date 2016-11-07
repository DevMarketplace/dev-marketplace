namespace UI.Utilities
{
    public interface IViewRenderer
    {
        string Render<TModel>(string name, TModel model) where TModel : class;
    }
}
