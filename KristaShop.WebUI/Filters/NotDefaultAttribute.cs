using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Filters
{
    public class NotDefaultAttribute : ValidationAttribute
    {
        public const string DefaultErrorMessage = "Поле {0} не должно содержать значение по умолчанию";
        public NotDefaultAttribute() : base(DefaultErrorMessage) { }

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }

            var type = value.GetType();
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return !value.Equals(defaultValue);
            }

            return true;
        }
    }
}
