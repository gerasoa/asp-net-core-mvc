using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace MyProject.Configuration
{
    public class MvcOptionsConfig
    {
        public static void ConfigurarMensagensDeModelBinding(DefaultModelBindingMessageProvider messageProvider)
        {
            messageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
            messageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchidoo.");
            messageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchidooo.");
            messageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
            messageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
            messageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
            messageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico");
            messageProvider.SetUnknownValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
            messageProvider.SetValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
            messageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numérico.");
            messageProvider.SetValueMustNotBeNullAccessor(x => "Este campo precisa ser preenchidoooo.");
        }
    }
}
