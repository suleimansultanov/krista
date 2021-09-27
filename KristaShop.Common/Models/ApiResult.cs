namespace KristaShop.Common.Models
{
    public class ApiResult
    {
        public ApiResult(bool isOk, string description = null)
        {
            IsOK = isOk;
            Description = description;
        }

        public bool IsOK { get; set; }
        public string Description { get; set; }
    }
}
