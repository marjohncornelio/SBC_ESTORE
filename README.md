<MudContainer Class="py-3 d-flex justify-center" >
    <MudImage Src="./favicon.png" Style="width: 100px; height: 100px; "/>
</MudContainer>

<MudDivider />

<MudContainer Class="py-3 d-flex gap-2">

        <MudContainer Class="m-0 p-0 w-auto">
            <MudAvatar Style="width: 60px; height: 60px;" Elevation="2">
                <MudImage Src="@AvatarUrl"></MudImage>
            </MudAvatar>
        </MudContainer>


    <MudContainer Class="m-0 p-0 d-flex row align-items-center gap-0">
        <MudContainer Class="p-0 m-0">
            <MudText Typo="Typo.h6" Style="font-size: 15px; line-height: 15px">@Helpers.TruncateName(User.Name, 9)</MudText>
            @if (context.User.IsInRole("User"))
            {
                <MudText Typo="Typo.subtitle1" Style="font-size: 14px">@User.UserName</MudText>
            }
            @if (context.User.IsInRole("SUPERUSER") || context.User.IsInRole("ADMIN"))
            {
                <div style="display: inline-block;">
                    <MudText Typo="Typo.subtitle1" Style="font-size: 10px; border-radius: 3px;" Color="Color.Primary" ><em>@User.Role.ToString()</em></MudText>
                </div>
            }
        </MudContainer>
    </MudContainer>
    <div class="d-flex align-center">
        <MudMenu Icon="@Icons.Material.Filled.Settings" Class="p-0 m-0 icon" AnchorOrigin="Origin.BottomRight">
            @if (context.User?.Identity?.IsAuthenticated == true)
            {
                <MudNavLink Href="/account-settings">
                    Account Settings
                </MudNavLink>
            }
        </MudMenu>
    </div>
</MudContainer>

<MudDivider />
