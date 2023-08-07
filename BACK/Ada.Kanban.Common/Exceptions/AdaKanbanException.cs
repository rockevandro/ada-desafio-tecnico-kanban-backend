namespace Ada.Kanban.Common.Exceptions
{
    public class AdaKanbanException : Exception
    {
        public AdaKanbanExceptionType ExceptionType { get; }

        public AdaKanbanException(AdaKanbanExceptionType exceptionType, string message): base(message)
        {
            ExceptionType = exceptionType;
        }
    }
}