namespace DemoSchool.WebUI.Pages.Students;

public partial class Students : ComponentBase, IDisposable
{
    #region Members
    #endregion

    #region Properties

    [Inject] private IStudentService _studentService { get; set; } = default!;

    [Inject] private PreloadService _preloadService { get; set; } = default!;

    #endregion

    #region Methods

    private async Task<GridDataProviderResult<Student>> StudentsDataProvider(GridDataProviderRequest<Student> request)
    {
        try
        {
            _preloadService.Show();

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
        finally
        {
            _preloadService.Hide();
        }

        return (new GridDataProviderResult<Student> { Data = new List<Student>(), TotalCount = 0 });
    }

    public void Dispose()
    {
        // dispose
    }

    #endregion
}