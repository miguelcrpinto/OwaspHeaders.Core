using System.Text;
using OwaspHeaders.Core.Helpers;

namespace OwaspHeaders.Core.Models
{
    public class XssConfiguration
    {
        public enum XssMode { zero, one, oneBlock, oneReport };

        public XssMode XssSetting { get; set; }
        public string ReportUri { get; set; }

        public XssConfiguration()
        {
            XssSetting = XssMode.oneBlock;
            ReportUri = string.Empty;
        }

        /// <summary>
        /// Builds the HTTP header value
        /// </summary>
        /// <returns>A string representing the HTTP header value</returns>
        public string BuildHeaderValue()
        {
            var stringBuilder = new StringBuilder();

            switch(XssSetting)
            {
                case XssMode.zero:
                    stringBuilder.Append(0);
                    break;
                case XssMode.one:
                    stringBuilder.Append(1);
                    break;
                case XssMode.oneBlock:
                    stringBuilder.Append("1; block");
                    break;
                case XssMode.oneReport:
                    if (string.IsNullOrWhiteSpace(ReportUri))
                    {
                        ArgumentExceptionHelper.RaiseException(nameof(ReportUri));
                    }
                    stringBuilder.Append("1; ");
                    stringBuilder.Append(ReportUri);
                    break;
            }

            return stringBuilder.ToString();
        }

    }
}
