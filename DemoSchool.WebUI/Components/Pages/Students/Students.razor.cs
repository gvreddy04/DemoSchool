using BlazorBootstrap;
using DemoSchool.WebUI.Models;
using Microsoft.AspNetCore.Components;

namespace DemoSchool.WebUI.Components.Pages.Students;

public partial class Students : ComponentBase, IDisposable
{
    #region Fields and Constants
    #endregion

    #region Properties, Indexers

    [Inject] private StudentApiClient _studentApiClient { get; set; } = null!;

    [Inject] private PreloadService _preloadService { get; set; } = default!;

    #endregion

    #region Methods

    private async Task<GridDataProviderResult<Student>> StudentsDataProvider(GridDataProviderRequest<Student> request)
    {
        try
        {
            _preloadService.Show();

            var result = await _studentApiClient.GetStudentsAsync(request.PageNumber, request.PageSize);
            return new GridDataProviderResult<Student> { Data = result.Data, TotalCount = result.Count };
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
