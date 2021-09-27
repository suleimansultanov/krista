using System.Collections.Generic;
using System.Linq;

namespace KristaShop.Common.Models
{
    public class OperationResult
    {
        public OperationResult(bool succeeded, string type, string message)
        {
            IsSuccess = succeeded;
            AlertType = type;
            AlertMessage = message;
        }

        public OperationResult(bool succeeded, IEnumerable<string> errors)
        {
            IsSuccess = succeeded;
            Errors = errors.ToArray();
        }

        public OperationResult()
        {
        }

        public bool IsSuccess { get; set; }
        public string AlertType { get; set; }
        public string AlertMessage { get; set; }
        public string[] Errors { get; set; }

        public static OperationResult Success()
        {
            return new OperationResult(true, new string[] { });
        }

        public static OperationResult Failure(IEnumerable<string> errors)
        {
            return new OperationResult(false, errors);
        }

        public static OperationResult AlertSuccess(string message)
        {
            return new OperationResult(true, "success", message);
        }

        public static OperationResult AlertFailure(string message)
        {
            return new OperationResult(false, "danger", message);
        }
    }
}
