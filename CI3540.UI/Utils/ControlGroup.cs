using System;
using System.Web.Mvc;

namespace CI3540.UI.Utils
{
    public class ControlGroup : IDisposable
    {
        private readonly HtmlHelper _html;
        
        public ControlGroup(HtmlHelper html)
        {
            _html = html;
        }

        public void Dispose()
        {
            _html.ViewContext.Writer.Write(_html.EndControlGroup());
        }
    }
}