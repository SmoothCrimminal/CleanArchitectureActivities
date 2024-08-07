﻿using Microsoft.AspNetCore.Components;

namespace Activities.Client.Shared
{
    public partial class Comment
    {
        [Parameter]
        public string CommentContent { get; set; } = default!;     
        
        [Parameter]
        public string Author { get; set; } = default!;

        [Parameter]
        public DateTime TimeOfPosting { get; set; }

        [Parameter]
        public string? AvatarSrc { get; set; }

        [Parameter]
        public string UserName { get; set; } = string.Empty;

        public void MoveToProfile()
        {
            NavigationManager.NavigateTo($"/profile/{UserName}");
        }
    }
}
