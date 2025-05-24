namespace Shared.SeedWork;

public class PagingRequestParameters
{
    private const int MAX_PAGE_SIZE = 50;

    private int _pageNumber = 1;
    private int _pageSize = 10;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > 0)
            {
                _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
            }
        }
    }
}