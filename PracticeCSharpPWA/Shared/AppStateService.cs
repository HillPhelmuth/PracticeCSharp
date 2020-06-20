using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using PracticeCSharpPWA.Shared.Models.CodeEditorModels;
using PracticeCSharpPWA.Shared.Models.VideosModels;

namespace PracticeCSharpPWA.Shared
{
    public class AppStateService
    {
        public Videos Videos { get; private set; }
        public CodeChallenges CodeChallenges { get; private set; }
        public IEnumerable<MetadataReference> References { get; private set; }
        public event Action OnChange;
       

        public void SetInitialAppState(CodeChallenges codeChallenges, Videos videos, IEnumerable<MetadataReference> assemblyReferences)
        {
            CodeChallenges = codeChallenges;
            Videos = videos;
            References = assemblyReferences;
            NotifyStateHasChanged();
        }

        private void NotifyStateHasChanged() => OnChange?.Invoke();
    }
}
