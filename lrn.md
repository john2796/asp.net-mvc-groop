# View *cshtml


```cshtml

  @foreach (var category in Html.GetEnumSelectList<ClubCategory>())
                    {
                        <a type="button" class="btn btn-secondary mb-1" asp-action="Index" asp-route-page="1" asp-route-category="@category.Value">@category.Text</a>
                    }
```