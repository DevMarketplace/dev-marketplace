using BusinessLogic.Utilities;

namespace UI.Models
{
    public class ViewModelBase
    {
        public virtual T ToBusinessObject<T>() where T : class, new()
        {
            var businessObject = new T();
            SimplePropertyMapper.Map(this, businessObject);

            return businessObject;
        }
    }
}
