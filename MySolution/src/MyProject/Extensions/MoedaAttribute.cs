using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Globalization;

namespace MyProject.Extensions
{
    public class MoedaAttribute : ValidationAttribute
    {

        //private readonly IViewLocalizer _localizer;
        //public MoedaAttribute(IViewLocalizer localizer)
        //{
        //    _localizer = localizer;
        //}

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name));
                
            }
            catch(Exception)
            {
                return new ValidationResult("Moeda em formato invalido");
            }

            return ValidationResult.Success;
        }
    }

    public class MoedaAttributeAdapter : AttributeAdapterBase<MoedaAttribute>
    {
        public MoedaAttributeAdapter(MoedaAttribute attribute, IStringLocalizer stringLocalizer) : base (attribute, stringLocalizer)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-moeda", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "Moeda em formato invalido";
        }
    }

    public class MoedaValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();
        public IAttributeAdapter? GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if(attribute is MoedaAttribute moedaAttribute)
            {
                return new MoedaAttributeAdapter(moedaAttribute, stringLocalizer);
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
