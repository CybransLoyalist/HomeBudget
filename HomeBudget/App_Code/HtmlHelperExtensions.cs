using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HomeBudget.App_Code
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString StylishCheckBoxFor<TModel>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, bool>> expression,
            string labelText)
        {
            var name = GetName(expression);

            var result = new StringBuilder();
            result.Append("<div class=\"stylish-checkbox\" style=\"width: 250px\">");
            result.Append(
                "<input data-val=\"true\" data-val-required=\"The "+ labelText + " field is required.\" id=\""+ name + "\" name=\""+ name + "\" type=\"checkbox\" value=\"true\" />");
            result.Append(
                "<label for= \""+ name + "\"></label><span style = \"padding-left: 15px; font-weight: bold\">"+labelText+"</span>");
            result.Append("<input name = \""+ name + "\" type=\"hidden\" value=\"false\"/>");
            result.Append("</div>");
            return new MvcHtmlString(result.ToString());
        }

        private static string GetName<TModel>(Expression<Func<TModel, bool>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }
    }
}

//<div class="stylish-checkbox" style="width: 250px">
//<input data-val="true" data-val-required="The Remember me? field is required." id="RememberMe" name="RememberMe" type="checkbox" value="true" />
//<label for="RememberMe"></label><span style = "padding-left: 10px; font-weight: bold" > Remember me?</span>
//<input name = "RememberMe" type="hidden" value="false" />
//</div>