using Ganss.Excel;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;

using ProfileMatch.Data;
using ProfileMatch.Models.Models;
using ProfileMatch.Repositories;
using ProfileMatch.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMatch.Components.Admin
{
    public partial class AdminMaintenance : ComponentBase
    {
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] IUnitOfWork UnitOfWork { get; set; }
        string _path;
        private async Task LoadSingleFile(InputFileChangeEventArgs e)
{
            var tempDirectory = Path.GetTempPath();
            var fileName = e.File.Name;
            using Stream fileStream = e.File.OpenReadStream();
            _path = @$"{tempDirectory}\{fileName}";
            FileStream fs = File.Create(_path);
            await fileStream.CopyToAsync(fs);
fileStream.Close();
fs.Close();
            await MapCategory();
        }
        private async Task MapCategory()
        {
            var categories = new ExcelMapper(_path).Fetch<Category>();
foreach (var c in categories)
            {
                await UnitOfWork.Categories.Insert(c);
                Snackbar.Add($"{c.Name} " + @L["created."], Severity.Success);
            }
            await InvokeAsync(() => StateHasChanged());
        }
    }
}
