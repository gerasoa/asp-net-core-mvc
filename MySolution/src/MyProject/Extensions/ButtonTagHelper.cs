using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data.SqlTypes;
using System.Security.Policy;

namespace MyProject.Extensions
{
    [HtmlTargetElement("*", Attributes = "button-type, route-id")]
    public class ButtonTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public ButtonTagHelper(IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
        {
            _contextAccessor = contextAccessor;
            _linkGenerator = linkGenerator;
        }


        [HtmlAttributeName("button-type")]
        public ButtonType ButtonTypeSelection { get; set; }

        [HtmlAttributeName("route-id")]
        public int RouteId { get; set; }

        private string? actionName;
        private string? ClassName;
        private string? spanIcon;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (ButtonTypeSelection)
            {
                case ButtonType.Detail:
                    actionName = "Details";
                    ClassName = "btn btn-info";
                    spanIcon = "fa fa-search";
                    break;
                case ButtonType.Update:
                    actionName = "Edit";
                    ClassName = "btn btn-warning";
                    spanIcon = "fa fa-pencil-alt";
                    break;
                case ButtonType.Delete:
                    actionName = "Delete";
                    ClassName = "btn btn-danger";
                    spanIcon = "fa fa-trash";
                    break;
            }

            //get the route information and select the controller name
            var controller = _contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

            var host = $"{_contextAccessor.HttpContext.Request.Scheme}://" + 
                $"{_contextAccessor.HttpContext.Request.Host.Value}";

            var indexPath = _linkGenerator.GetPathByAction(
                _contextAccessor.HttpContext,
                actionName,
                controller,
                values:new { id = RouteId }
                );

            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"{indexPath}");
            output.Attributes.SetAttribute("class", ClassName);

            var iconSpan = new TagBuilder("span");
            iconSpan.AddCssClass(spanIcon);

            output.Content.AppendHtml(iconSpan);
        }
    }

    public enum ButtonType
    {
        Detail = 1,
        Update,
        Delete
    }
}
