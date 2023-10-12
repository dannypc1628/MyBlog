namespace MyBlog.Models
{
    public class ResultViewModel
    {
        public bool IsSuccess { get; set; }

        public List<string> Messages { get; set; } = new List<string>();

        public ResultViewModel(bool isSuccess, string messages)
        {
            IsSuccess = isSuccess;
            Messages.Add(messages);
        }

        public ResultViewModel(bool isSuccess, List<string> messages)
        {
            IsSuccess = isSuccess;
            Messages = messages;
        }

        public void AddError(string errorMessage)
        {
            Messages.Add(errorMessage);
            IsSuccess = false;
        }

    }
}
