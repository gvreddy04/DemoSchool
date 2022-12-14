namespace WebUI.Pages.Students;

public partial class Students : ComponentBase, IDisposable
{
    #region Members
    #endregion

    #region Properties

    [Inject] private IStudentService _studentService { get; set; } = null!;

    #endregion

    #region Methods

    private async Task<GridDataProviderResult<Student>> StudentsDataProvider(GridDataProviderRequest<Student> request)
    {
        try
        {
            var result = await _studentService.GetStudentsAsync();
            return new GridDataProviderResult<Student> { Data = result, TotalCount = result.Count() };
        }
        catch (NotSupportedException ex)
        {
            var e = ex.Message;
        }
        catch (HttpRequestException)
        {
            // TODO: update this
        }
        catch (Exception ex)
        {
            throw; // TODO: update this
        }

        return (new GridDataProviderResult<Student> { Data = new List<Student>(), TotalCount = 0 });
    }

    public void Dispose()
    {
        // dispose
    }

    #endregion
}