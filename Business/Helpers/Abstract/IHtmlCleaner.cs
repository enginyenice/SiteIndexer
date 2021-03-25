using Core.Utilities.Results;

namespace Business.Helpers.Abstract
{
    public interface IHtmlCleaner
    {
        IDataResult<string> RemoveHtmlTags(string StringHtmlPage);
    }
}